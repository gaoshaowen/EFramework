using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;

namespace eFrameWork.Master
{
    public partial class manageMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser("Manage");//Manage为设定的登录区域
            user.Check();//检测用户是否登录,未登录则跳转到登录页

           
        }
    }
}