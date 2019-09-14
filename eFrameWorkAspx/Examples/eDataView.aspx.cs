using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eFrameWork.Examples
{
    public partial class eDataView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //eDataView2.Where.Add("");//添加搜索条件
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eDataView控件-eFrameWork示例中心";
            }
        }
    }
}