using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;

namespace eFrameWork.Examples
{
    public partial class _eListControl : System.Web.UI.Page
    {
        public eList elist3;
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataTable tb = eOleDB.getDataTable("select top 5 * from Demo_Persons");
            //eListControl1.DataSource = tb;


            eList elist1 = new eList("Demo_Persons");
            elist1.Rows = 3;
            elist1.OrderBy.Add("NewID()");
            elist1.Bind(eListControl1);


            


            eList elist2 = new eList("Demo_Persons");
            elist2.OrderBy.Add("ID Desc");
            elist2.Bind(eListControl2, ePageControl1);//绑定

           

            elist3 = new eList("Demo_Persons");
            elist3.OrderBy.Add("ID Desc");
            elist3.Where.AddControl(eSearchControlGroup);//添加搜索条件控件组
            elist3.Bind(eDataTable, ePageControl2);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eListControl控件-eFrameWork示例中心";
            }
        }
    }
}