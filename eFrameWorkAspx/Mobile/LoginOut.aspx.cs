using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Mobile
{
    public partial class LoginOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser("Mobile");

            //用户退出日志
            eTable etb = new eTable("a_eke_sysUserLog");
            etb.Fields.Add("UserID", user.ID);
            etb.Fields.Add("Type", 2);
            etb.Fields.Add("IP", eBase.getIP());
            etb.Fields.Add("Area", "Mobile");
            etb.Add();

            user.Remove();
            Response.Redirect("Login.aspx", true);
        }
    }
}