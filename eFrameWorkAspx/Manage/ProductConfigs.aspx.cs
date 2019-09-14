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
    public partial class ProductConfigs : System.Web.UI.Page
    {
        public string act = eParameters.Request("act");
        public eForm edt;
        public eAction Action;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("ProductConfigs", user);
            edt.AddControl(eFormControlGroup);
            Action = new eAction();
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
        }
        protected void Action_Actioning(string Actioning)
        {
            switch (Actioning)
            {
                case "":
                    eList elist = new eList("ProductConfigs");
                    elist.Where.Add("delTag=0");
                    elist.OrderBy.Default = "addTime";//默认排序
                    elist.Bind(eDataTable, ePageControl1);

                    break;
                default:
                    edt.onChange += new eFormTableEventHandler(edt_onChange);
                    edt.Handle();
                    break;
            }
            //Response.Write(Actioning + "A");
        }
        public void edt_onChange(object sender, eFormTableEventArgs e)
        {
            if (e.eventType == eFormTableEventType.Inserting)
            {

            }
            if (e.eventType == eFormTableEventType.Deleted)
            {
                //string sql = "update a_eke_sysApplicationItems set delTag=1 where ApplicationID='" + e.ID + "'";
                //eOleDB.Execute(sql);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "产品配置 - " + eConfig.getString("manageName"); 
            }
        }
    }
}