using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.system
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser("System");
            StringBuilder sb = new StringBuilder();

            
            sb.Append("欢迎登录" + eConfig.getString("systemName") + "!<br>");
            sb.Append("用户名：" + user["Name"] + "<br>");
            sb.Append("用户ID：" + user.ID + "<br>");
            sb.Append("SiteID：" + user["SiteID"] + "<br>");


            DataTable tb = eOleDB.getDataTable("select LoginCount,LastLoginTime from a_eke_sysUsers where UserID='" + user["ID"].ToString() + "'");
            if (tb.Rows.Count > 0)
            {
                string logincount = tb.Rows[0]["LoginCount"].ToString();
                string lastlt = string.Format("{0:yyyy-MM-dd HH:mm:ss}", tb.Rows[0]["LastLoginTime"]);

                sb.Append("登录次数：" + logincount  + "<br>");
                sb.Append("上次登录时间：" + lastlt + "<br>");
            }
            sb.Append("默认密码非常不安全,点此<a href=\"ModifyPass.aspx\">修改密码</a>");
            litBody.Text = sb.ToString();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "首页 - " + eConfig.getString("systemName");
            }
        }
    }
}