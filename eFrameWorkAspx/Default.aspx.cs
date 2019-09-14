using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eOleDB.Connection.State.ToString().ToLower() == "open")
            {
                LitDBState.Text = "数据库配置：<font color=\"#00cc00\">正确</font><hr />";
            }
            else
            {
                LitDBState.Text = "数据库配置：<font color=\"#cc0000\">有误</font><hr />";
            }
        }
    }
}