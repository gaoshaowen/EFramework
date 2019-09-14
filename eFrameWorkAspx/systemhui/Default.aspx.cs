using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.systemhui
{
    public partial class Default : System.Web.UI.Page
    {
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("System");
            user.Check();

        }
    }
}