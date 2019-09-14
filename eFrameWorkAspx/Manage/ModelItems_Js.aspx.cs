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
    public partial class ModelItems_Js : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:8px;\">");
                Response.Write("<b>JS脚本</b><br>");
                Response.Write("模块扩展功能需要用到的JavaScript脚本(公共为全局脚本)。");
                Response.Write("</div> ");
            }

            DataTable tb = eOleDB.getDataTable("select BaseJs,Addjs,EditJs,ViewJs from a_eke_sysModels where ModelID='" + modelid + "'");
            if (tb.Rows.Count > 0)
            {

                Response.Write("<dl class=\"ePanel\">\r\n");
                Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>公共</h1></dt>\r\n");
                Response.Write("<dd style=\"display:none;\">");
                if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(130);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'basejs');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["BaseJs"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["BaseJs"].ToString()) + "</textarea><br>\r\n");
                Response.Write("</dd>\r\n");
                Response.Write("</dl>\r\n");

               
                

                Response.Write("<dl class=\"ePanel\">\r\n");
                Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>添加</h1></dt>\r\n");
                Response.Write("<dd style=\"display:none;\">");
                if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(131);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'addjs');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["AddJs"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["AddJs"].ToString()) + "</textarea><br>\r\n");
                Response.Write("</dd>\r\n");
                Response.Write("</dl>\r\n");


                Response.Write("<dl class=\"ePanel\">\r\n");
                Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>修改</h1></dt>\r\n");
                Response.Write("<dd style=\"display:none;\">");
                if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(132);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'editjs');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["EditJs"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["EditJs"].ToString()) + "</textarea><br>\r\n");
                Response.Write("</dd>\r\n");
                Response.Write("</dl>\r\n");


                Response.Write("<dl class=\"ePanel\">\r\n");
                Response.Write("<dt><h1 onclick=\"showPanel(this);\"><a href=\"javascript:;\" class=\"cur\" onfocus=\"this.blur();\"></a>查看</h1></dt>\r\n");
                Response.Write("<dd style=\"display:none;\">");
                if (eBase.showHelp()) Response.Write("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(133);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                Response.Write(" <textarea name=\"textarea\" style=\"width:95%;\" cols=\"100\" rows=\"10\"  onBlur=\"setModel(this,'viewjs');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["ViewJs"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(tb.Rows[0]["ViewJs"].ToString()) + "</textarea><br>\r\n");
                Response.Write("</dd>\r\n");
                Response.Write("</dl>\r\n");
            }
            //string value = eOleDB.getValue("select ListFields from a_eke_sysModels where ModelID='" + modelid + "'");
            //Response.Write("自定义列：");
            //Response.Write("<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlEncode(value) + "\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(value) + "\"  class=\"edit\" style=\"width:90%;\" onBlur=\"setModel(this,'listfields');\" />");


            Response.End();
        }
    }
}