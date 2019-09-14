using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class _eList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 1.不分页
            eList elist1 = new eList("Demo_Persons");
            elist1.Fields.Add("ID");//不添加则是取所有字段
            elist1.Fields.Add("FullName,Height,addTime");
            elist1.Rows = 3;
            elist1.Where.Add("delTag=0");
            elist1.OrderBy.Add("id desc");
            elist1.Bind(Repeater1);
            #endregion
            #region 2.自定义分页
            string ParaName = "pg";
            int pg = (Request.QueryString["pg"] == null ? 1 : Convert.ToInt32(Request.QueryString["pg"]));
            eList elist2 = new eList("Demo_Persons");
            elist2.OrderBy.Add("id");
            elist2.PageSize = 2;
            elist2.Page = pg;
            elist2.Bind(Repeater2);


            string allkeys = eParameters.getAllKeys(ParaName);
            string filename = eBase.getAspxFileName();
            StringBuilder sb = new StringBuilder();
            sb.Append("共<font color=\"#cc0000\">" + elist2.RecordsCount.ToString() + "</font>条数据&nbsp;");
            sb.Append("分<font color=\"#cc0000\">" + elist2.PageCount.ToString() + "</font>页显示&nbsp;");
            sb.Append("每页显示<font color=\"#cc0000\">" + elist2.PageSize.ToString() + "</font>条&nbsp;");
            sb.Append("当前第<font color=\"#cc0000\">" + elist2.Page.ToString() + "</font>页&nbsp;");
            if (pg < 2)
            {
                sb.Append("首页&nbsp;上一页&nbsp;");
            }
            else
            {
                sb.Append("<a href=\"" + filename + "?" + (allkeys.Length > 0 ? allkeys + "&" : "") + ParaName + "=1\">首页</a>&nbsp;");
                sb.Append("<a href=\"" + filename + "?" + (allkeys.Length > 0 ? allkeys + "&" : "") + ParaName + "=" + (pg - 1).ToString() + "\">上一页</a>&nbsp;");
            }
            if (pg < elist2.PageCount)
            {
                sb.Append("<a href=\"" + filename + "?" + (allkeys.Length > 0 ? allkeys + "&" : "") + ParaName + "=" + (pg + 1).ToString() + "\">下一页</a>&nbsp;");
                sb.Append("<a href=\"" + filename + "?" + (allkeys.Length > 0 ? allkeys + "&" : "") + ParaName + "=" + elist2.PageCount.ToString() + "\">尾页</a>");
            }
            else
            {
                sb.Append("下一页&nbsp;尾页");
            }
            litPage.Text = sb.ToString();
            #endregion

            #region 3.分页控件
            eList elist3 = new eList("Demo_Persons");
            elist3.OrderBy.Add("id");
            elist3.Bind(Repeater3, ePageControl1);
            #endregion

            #region 4.分页eListControl控件
            eList elist4 = new eList("Demo_Persons");
            elist4.OrderBy.Add("id");
            elist4.Bind(eListControl1, ePageControl2);
            #endregion
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eList类-eFrameWork示例中心";
            }
        }
    }
}