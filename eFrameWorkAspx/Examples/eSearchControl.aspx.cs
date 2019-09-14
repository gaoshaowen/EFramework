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
    public partial class _eSearchControl : System.Web.UI.Page
    {
        public eList elist;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            elist = new eList("Demo_Persons");
            elist.Fields.Add("CASE WHEN Show=1 THEN 'images/sw_true.gif' ELSE 'images/sw_false.gif' END as ShowPIC,CASE WHEN Show=1 THEN '0' ELSE '1' END as ShowValue");
            elist.Where.Add("delTag=0");
            elist.Where.AddControl(eSearchControlGroup);
            elist.OrderBy.Default = "addTime desc";//默认排序
            elist.Bind(eDataTable, ePageControl1);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eSearchControl控件-eFrameWork示例中心";
            }
        }
    }
}