using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;
using System.Text;


namespace eFrameWork.Mobile
{
    public partial class Menu : System.Web.UI.UserControl
    {
        private DataTable _models;
        protected DataTable Models
        {
            get
            {
                string roleids = "";
                if (_models == null)
                {
                    roleids = eOleDB.getValue("select RoleID from a_eke_sysUsers where UserID='" + user.ID + "'");
                    string sql = "SELECT ModelID,Type,ParentID,MC,Auto,AspxFile,PX,addTime FROM a_eke_sysModels where delTag=0";
                    sql += " and (TYPE=2 or ModelID in (select ModelID from a_eke_sysPowers where delTag=0 and canList=1 and (UserID='" + user.ID + "'" + (roleids.Length > 0 ? " or RoleID in ('" + roleids.Replace(",", "','") + "')" : "") + ") and ApplicationID is Null))";
                    _models = eOleDB.getDataTable(sql);
                }
                return _models;
            }
        }
        private eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Mobile");
            user.Check();
            LitMenu.Text = getMenus("");
        }
        private string getMenus(string ParentID)
        {
            StringBuilder sb = new StringBuilder();
            if (ParentID.Length == 0)
            {
                sb.Append("<ul>\r\n");
                sb.Append("<li><a href=\"Default.aspx\">首页</a></li>\r\n");
            }
            else
            {
                sb.Append("<ul>\r\n");
            }
            DataRow[] rows = Models.Select(ParentID.Length == 0 ? "ParentID is Null" : "ParentID='" + ParentID + "'", "PX,addTime");
            for (int i = 0; i < rows.Length; i++)
            {
                sb.Append("<li>");
                if (rows[i]["Type"].ToString() == "2")
                {
                    sb.Append("<a href=\"javascript:;\" onclick=\"showmenu(this);\"");

                }
                else
                {
                    if (rows[i]["AspxFile"].ToString().Length > 0 && rows[i]["Auto"].ToString() == "False")
                    {
                        sb.Append("<a href=\"" + rows[i]["AspxFile"].ToString() + "?modelid=" + rows[i]["ModelID"].ToString() + "\"");
                    }
                    else
                    {
                        sb.Append("<a href=\"Model.aspx?modelid=" + rows[i]["ModelID"].ToString() + "\"");
                    }
                }

                sb.Append(" onfocus=\"this.blur();\">");

                sb.Append(rows[i]["mc"].ToString());
                sb.Append("</a>");
                if (rows[i]["Type"].ToString() == "2")
                {
                    sb.Append(getMenus(rows[i]["ModelID"].ToString()));
                }
                sb.Append("</li>\r\n");
            }
            if (ParentID.Length == 0)
            {
                sb.Append("<li><a href=\"ModifyPass.aspx\">修改密码</a></li>\r\n");
                sb.Append("<li><a href=\"LoginOut.aspx\">退出登录</a></li>\r\n");
            }
            sb.Append("</ul>\r\n");
            return sb.ToString();
        }
    }
}