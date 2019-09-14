using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.Manage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser("Manage");
            StringBuilder sb = new StringBuilder();
            sb.Append("欢迎登录" + eConfig.getString("manageName") + "!<br>");
            sb.Append("用户名：" + user["Name"] + "<br>");
            sb.Append("用户ID：" + user.ID + "<br>");
            sb.Append("用户SiteID：" + user["SiteID"] + "<br>");
            DataTable tb = eOleDB.getDataTable("select LoginCount,LastLoginTime from a_eke_sysUsers where UserID='" + user["ID"].ToString() + "'");
            if (tb.Rows.Count > 0)
            {
                string logincount = tb.Rows[0]["LoginCount"].ToString();
                string lastlt = string.Format("{0:yyyy-MM-dd HH:mm:ss}", tb.Rows[0]["LastLoginTime"]);

                sb.Append("登录次数：" + logincount + "<br>");
                sb.Append("上次登录时间：" + lastlt + "<br>");
            }
            LitBody.Text = sb.ToString();

            //DataTable dt = eOleDB.getColumns("Demo_BidProjects");
            //eBase.PrintDataTable(dt);

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "首页 - " + eConfig.getString("manageName"); 
            }
        }
    }
}