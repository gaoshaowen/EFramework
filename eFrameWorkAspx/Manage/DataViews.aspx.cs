using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Manage
{
    public partial class DataViews : System.Web.UI.Page
    {
        public string id = eParameters.Request("id");
        public string act = eParameters.Request("act");
        public string sql = "";
        public eForm edt;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("a_eke_sysDataViews", user);
            if (act.Length == 0)
            {

                List();
                return;
            }
            if (act == "copy")
            {
                sql = "Insert into a_eke_sysDataViews (ServiceID,MC,SQL,HeaderTemplate,ItemTemplate,SplitTemplate,FooterTemplate,SM,Condition,GroupBy,OrderBy,PageSize,PageNum,PageMode) SELECT ServiceID,MC + ' 复件',SQL,HeaderTemplate,ItemTemplate,SplitTemplate,FooterTemplate,SM,Condition,GroupBy,OrderBy,PageSize,PageNum,PageMode  FROM a_eke_sysDataViews where DataViewID='" + id + "'";

                eOleDB.Execute(sql);
                if (Request.ServerVariables["HTTP_REFERER"] == null)
                {
                    Response.Redirect("DataViews.aspx", true);
                }
                else
                {
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString(), true);
                }
                Response.End();
                return;
            }
            #region 信息添加、编辑

            /*
        foreach (Control control in Controls)
        {
            if (control is eFormControl)
            {
                eFormControl ec = control as eFormControl;
                Response.Write(ec.Field + "::" + ec.ID + " AA<br>");
            }
            if (control is MasterPage)
            {
                foreach (Control ctrl in control.Controls)
                {
                    if (ctrl is ContentPlaceHolder)
                    {
                        foreach (Control ct in ctrl.Controls)
                        {
                            if (ct is eFormControl)
                            {
                                eFormControl ec = ct as eFormControl;
                                Response.Write(ec.Field + "::" + ec.ID + " VV<br>");
                            }
                        }
                    }
                }
            }
        }
        */

            edt.AddControl(eFormControlGroup);
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
            eList datalist = new eList("a_eke_sysDataViews");
            datalist.Where.Add("delTag=0");
            datalist.Where.Add("ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'"));
            datalist.OrderBy.Add("addTime desc");
            datalist.OrderBy.Default = "addTime desc";
            datalist.Bind(Rep, ePageControl1);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "视图控件 - " + eConfig.getString("manageName"); 
            }
        }
    }
}