using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Examples
{
    public partial class _eOrderby : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eList elist = new eList("Demo_Persons");
            elist.Where.Add("delTag=0");
            elist.OrderBy.Default = "addTime desc";//默认排序
            elist.Bind(eDataTable, ePageControl1);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eListControl 控件排序-eFrameWork示例中心";
            }
        }
    }
}