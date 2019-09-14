using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class easyUIDataGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            eList elist = new eList("Demo_Persons");
            elist.OrderBy.Add("ID Desc");
            elist.EasyUIDataGrid(eDataTable);


            eList elista = new eList("Demo_Dictionaries");
            elista.OrderBy.Add("PID,addTime Desc");
            elista.EasyUIDataGrid(DGDics);

      
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "easyUI DataGrid 应用-eFrameWork示例中心";
            }
        }
    }
}