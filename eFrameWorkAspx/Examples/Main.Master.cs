using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public string ModelID = eParameters.Request("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}