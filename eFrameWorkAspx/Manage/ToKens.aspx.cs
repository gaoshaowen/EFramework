using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;


namespace eFrameWork.Manage
{
    public partial class ToKens : System.Web.UI.Page
    {
        public string id = eParameters.Request("id");
        public string act = eParameters.Request("act");
        public string sql = "";
        public eForm edt;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("a_eke_sysToKens", user);
            if (act.Length == 0)
            {
                List();
                return;
            }
            if (edt.Action.ToLower() == "del")
            {
                edt.DeleteTrue();
            }
        }
        private void List()
        {
            eDataTable.CanEdit = true;
            eDataTable.CanDelete = true;
            eList datalist = new eList("a_eke_sysToKens");
            datalist.Where.Add("delTag=0");
            datalist.OrderBy.Add("addTime desc");
            //datalist.Bind(Rep, ePageControl1);
            datalist.Bind(eDataTable, ePageControl1);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "令牌管理 - " + eConfig.getString("manageName"); 
            }
        }
    }
}