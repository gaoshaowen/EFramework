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
    public partial class ModelItems_Client : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:6px;\">");
                Response.Write("<b>客户端验证</b><br>");
                Response.Write("设置表单提交前的数据长度、大小、是否必填等信息。");
                Response.Write("</div> ");
            }


            eList datalist = new eList("a_eke_sysModelItems");
            datalist.Where.Add("ModelID='" + modelid + "' and delTag=0 and showAdd=1 and sys=0 and Custom=0");
            datalist.OrderBy.Add("addorder, PX, addTime");
            datalist.Bind(Rep);





            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}