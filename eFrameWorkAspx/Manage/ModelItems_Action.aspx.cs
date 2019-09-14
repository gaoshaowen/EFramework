using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.Manage
{
    public partial class ModelItems_Action : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:8px;\">");
                Response.Write("<b>动作</b><br>");

                Response.Write("模块扩展功能需要执行的SQL语句。<br>");
                Response.Write("</div> ");
            }

            eList elist = new eList("a_eke_sysActions");
            elist.Where.Add("ModelID='" + modelid + "' ");
            elist.OrderBy.Add("addTime");
            elist.Bind(Rep);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());

            DataTable tb = eOleDB.getDataTable("select AddSQL,EditSQL,DeleteSQL from a_eke_sysModels where ModelID='" + modelid + "'");
             if (tb.Rows.Count > 0)
             {
                 Response.Write("<dl class=\"ePanel\">\r\n");
                 Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>添加完成执行SQL</h1></dt>\r\n");
                 Response.Write("<dd style=\"display:none;\">");
                 if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(127);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                 Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'addsql');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["AddSQL"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["AddSQL"].ToString()) + "</textarea><br>\r\n");
                 Response.Write("</dd>\r\n");
                 Response.Write("</dl>\r\n");

                 Response.Write("<dl class=\"ePanel\">\r\n");
                 Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>修改完成执行SQL</h1></dt>\r\n");
                 Response.Write("<dd style=\"display:none;\">");
                 if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(128);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                 Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'editsql');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["EditSQL"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["EditSQL"].ToString()) + "</textarea><br>\r\n");
                 Response.Write("</dd>\r\n");
                 Response.Write("</dl>\r\n");

                 Response.Write("<dl class=\"ePanel\">\r\n");
                 Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>删除完成执行SQL</h1></dt>\r\n");
                 Response.Write("<dd style=\"display:none;\">");
                 if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(129);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                 Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'deletesql');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["DeleteSQL"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["DeleteSQL"].ToString()) + "</textarea><br>\r\n");
                 Response.Write("</dd>\r\n");
                 Response.Write("</dl>\r\n");
             }
            Response.End();
        }
    }
}