using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Master
{
    public partial class systemMainNone : System.Web.UI.MasterPage
    {
        public string ModelID = eParameters.Request("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser("System");
            user.Check();//检测用户是否登录,未登录则跳转到登录页
        }
    }
}