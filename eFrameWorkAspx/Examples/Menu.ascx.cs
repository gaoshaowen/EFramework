using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class Menu : System.Web.UI.UserControl
    {
        public string filename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            filename = eBase.getAspxFileName().ToLower();
        }
    }
}