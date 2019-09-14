using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Master
{
    public partial class huiMain : System.Web.UI.MasterPage
    {
        public bool Ajaxget = false;
        public string ModelID = eParameters.Request("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ajaxget"] != null) Ajaxget = Convert.ToBoolean(Request.QueryString["ajaxget"]);
        }
    }
}