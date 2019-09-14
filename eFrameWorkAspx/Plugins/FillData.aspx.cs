using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Plugins
{
    public partial class FillData : System.Web.UI.Page
    {
        public string ModelID = eParameters.Request("modelid");
        public string UserArea = "System";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["area"] != null) UserArea = Request.QueryString["area"].ToString();
            eUser user = new eUser(UserArea);
            eModel model = new eModel(ModelID, user);
            LitBody.Text = model.getListHTML();
        }
    }
}