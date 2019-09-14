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
    public partial class AllDomain : System.Web.UI.Page
    {
        public string id = eParameters.Request("id");
        public string act = eParameters.Request("act");
        public string sql = "";
        public eForm edt;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("a_eke_sysAllowDomain", user);
            //edt.AutoRedirect = false;
            if (act.Length == 0)
            {

                List();
                return;
            }
            edt.AddControl(eFormControlGroup);
            edt.onChange += new eFormTableEventHandler(edt_onChange);
            edt.Handle();         
        }
        public void edt_onChange(object sender, eFormTableEventArgs e)
        {
            if (e.eventType == eFormTableEventType.Inserted || e.eventType == eFormTableEventType.Updated || e.eventType == eFormTableEventType.Deleted)
            {
                eBase.clearDataCache("a_eke_sysAllowDomain");
            }
        }
        private void List()
        {
            eDataTable.CanEdit = true;
            eDataTable.CanDelete = true;
            eList datalist = new eList("a_eke_sysAllowDomain");
            datalist.Where.Add("delTag=0");
            datalist.OrderBy.Add("addTime desc");
            //datalist.Bind(Rep, ePageControl1);
            datalist.Bind(eDataTable, ePageControl1);
        }
        protected void Rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitBM");
                if (lit != null)
                {
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "域名授权 - " + eConfig.getString("manageName"); 
            }
        }
    }
}