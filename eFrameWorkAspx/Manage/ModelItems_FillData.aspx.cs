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
    public partial class ModelItems_FillData : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        public DataRow parent = null;
        public DataRow me = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:8px;\">");
                Response.Write("<b>对应关系</b><br>");
                Response.Write("设置数据子模块回填数据到主模块的对应关系。");
                Response.Write("</div> ");
            }

            me = eOleDB.getDataTable("select * from a_eke_sysModels where modelID='" + modelid + "'").Select()[0];
            parent = eOleDB.getDataTable("select * from a_eke_sysModels where modelID='" + me["ParentID"].ToString() + "'").Select()[0];


            List();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;
            Response.Write(sw.ToString());
            Response.End();
        }
        private void List()
        {
            eList elist = new eList("a_eke_sysModelItems");
            elist.Where.Add("ModelID='" + me["ParentID"].ToString() + "' and Custom=0 and Sys=0 and primaryKey=0 and showAdd=1");
            elist.OrderBy.Add("px");
            Rep.ItemDataBound += new RepeaterItemEventHandler(Rep_ItemDataBound);
            elist.Bind(Rep);
        }
        protected void Rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitColumns");
                if (lit != null)
                {
                    string sql = "select ModelItemID,MC + ' (' + Code + ')' as MC from a_eke_sysModelItems where delTag=0 and ModelID='" + modelid + "' and Custom=0 and (SYS=0 or primaryKey=1) order by addTime";
                    
                    //lit.Text = "<option>aaa1112bbb</option>";
                    lit.Text = eOleDB.getOptions(sql, "MC", "ModelItemID", DataBinder.Eval(e.Item.DataItem, "FillModelItemID").ToString());

                }
            }
        }
    }
}