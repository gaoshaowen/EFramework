using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.Manage
{
    public partial class ModelItems_Export : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:8px;\">");
                Response.Write("<b>导出</b><br>");
                Response.Write("设置模块导出到Excel的列及宽度。");
                Response.Write("</div> ");
            }

            eList datalist = new eList("a_eke_sysModelItems");
            datalist.Where.Add("ModelID='" + modelid + "' and delTag=0");
            datalist.OrderBy.Add("showExport desc,ExportOrder,  addTime");
            datalist.Bind(Rep);


            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;
            Response.Write(sw.ToString());
        }
    }
}