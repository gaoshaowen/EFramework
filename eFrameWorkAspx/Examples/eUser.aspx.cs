using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class _eUser : System.Web.UI.Page
    {
        public eUser user;
        private eAction action;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("TestArea");
            //user.URL = "Login.aspx";//用户未登录跳转登录地址，默认为Login.aspx
            action = new eAction();
            action.Actioning += action_Actioning;
            action.Listen();
        }
        protected void action_Actioning(string Action)
        {
            switch (Action)
            {
                case "":
                    if (user.Logined)
                    {
                        litBody.Text = "用户ID：" + user["ID"] + "<br>用户名：" + user["Name"];
                    }
                    else
                    {
                        litBody.Text = "无";
                    }
                    break;
                case "login": //登录                    
                    user["id"] = "111";//添加登录信息：方式一
                    user.Add("name", "eketeam");//添加登录信息：方式二
                    user.Save();
                    Response.Redirect("eUser.aspx", true);
                    break;
                case "loginout": //退出登录
                    user.Remove();//删除登录信息
                    Response.Redirect("eUser.aspx", true);
                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eUser类-eFrameWork示例中心";
            }
        }
    }
}