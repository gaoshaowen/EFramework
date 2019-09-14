using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class LoadAction : System.Web.UI.Page
    {
        public eForm eform;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = eParameters.QueryString("id");
            eform = new eForm("Demo_Persons");

            eform.AddControl(eFormControlGroup);
            //eform.Action = "view";
            //eform.ID = id;
            //eform.Handle();

            eform.LoadAction("view", id);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "前台加载数据-eFrameWork示例中心";
            }
        }
    }
}