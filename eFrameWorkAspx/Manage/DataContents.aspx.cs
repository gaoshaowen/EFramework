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
    public partial class DataContents : System.Web.UI.Page
    {
        public string act = eParameters.Request("act");
        public eForm edt;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("a_eke_sysDataContents", user);
            if (act.Length == 0)
            {
                List();
                return;
            }
            #region 信息添加、编辑
            edt.AddControl(f1);
            edt.AddControl(f2);
            edt.AddControl(f3);
            edt.onChange += new eFormTableEventHandler(edt_onChange);
            edt.Handle();
            #endregion

        }
        public void edt_onChange(object sender, eFormTableEventArgs e)
        {
            if (e.eventType == eFormTableEventType.Inserting)
            {
                if (user["ServiceID"].Length > 0) edt.Fields.Add("ServiceID", user["ServiceID"]);
            }
        }
        private void List()
        {
            eList datalist = new eList("a_eke_sysDataContents");
            datalist.Where.Add("delTag=0");
            datalist.Where.Add("ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'"));
            datalist.OrderBy.Add("addTime desc");
            datalist.Bind(Rep, ePageControl1);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "文本控件 - " + eConfig.getString("manageName"); 
            }
        }
    }
}