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
    public partial class ModelItems_List : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            DataRow dr = eOleDB.getDataTable("select b.Type as bType, a.* from a_eke_sysModels a left join a_eke_sysModels b on a.ParentID=b.ModelID where a.ModelID='" + modelid + "'").Select()[0];
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:6px;\">");
                Response.Write("<b>列表</b><br>");
                Response.Write("设置列表相关参数；显示列是否显示、显示顺序、显示宽度、高度、点击排序、自定义显示等相关参数。");
                Response.Write("</div> ");
            }
            if (dr["bType"].ToString() == "1" && dr["JoinMore"].ToString() == "True")
            {
                Response.Write("窗口宽度：");
                Response.Write("<input type=\"text\" value=\"" + dr["width"].ToString() + "\" oldvalue=\"" + dr["width"].ToString() + "\"  class=\"edit\" style=\"width:90px;\" onBlur=\"setModel(this,'width');\" />");
                if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(84);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                Response.Write("&nbsp;窗口高度：");
                Response.Write("<input type=\"text\" value=\"" + dr["height"].ToString() + "\" oldvalue=\"" + dr["height"].ToString() + "\"  class=\"edit\" style=\"width:90px;\" onBlur=\"setModel(this,'height');\" />");
                if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(85);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                Response.Write("&nbsp;最少行数：");
                Response.Write("<input type=\"text\" value=\"" + dr["minCount"].ToString() + "\" oldvalue=\"" + dr["minCount"].ToString() + "\"  class=\"edit\" style=\"width:90px;\" onBlur=\"setModel(this,'mincount');\" />");
                if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(86);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                Response.Write("&nbsp;最多行数：");
                Response.Write("<input type=\"text\" value=\"" + dr["maxCount"].ToString() + "\" oldvalue=\"" + dr["maxCount"].ToString() + "\"  class=\"edit\" style=\"width:90px;\" onBlur=\"setModel(this,'maxcount');\" />");
                if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(87);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                Response.Write("<br>");
            }

            Response.Write("<input type=\"checkbox\" name=\"customheight\" id=\"customheight\" onclick=\"setModel(this,'customheight');\"" + (dr["customheight"].ToString() == "True" ? " checked" : "") + " /><label for=\"customheight\">拖动行高</label>");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(88);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");

            //Response.Write("<input type=\"checkbox\" name=\"customwidth\" id=\"customwidth\" onclick=\"setModel(this,'customwidth');\"" + (dr["customwidth"].ToString() == "True" ? " checked" : "") + " /><label for=\"customwidth\">拖动列宽</label>");
            Response.Write("<input type=\"checkbox\" name=\"customshow\" id=\"customshow\" onclick=\"setModel(this,'customshow');\"" + (dr["customshow"].ToString() == "True" ? " checked" : "") + " /><label for=\"customshow\">列显示隐藏菜单</label>"); //
            //Response.Write("<input type=\"checkbox\" name=\"custommove\" id=\"custommove\" onclick=\"setModel(this,'custommove');\"" + (dr["custommove"].ToString() == "True" ? " checked" : "") + " /><label for=\"custommove\">拖动行位置</label>");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(89);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");

            Response.Write("&nbsp;默认分页大小：<input type=\"text\" value=\"" + dr["PageSize"].ToString() + "\" oldvalue=\"" + dr["PageSize"].ToString() + "\"  class=\"edit\" style=\"width:40px;\" onBlur=\"setModel(this,'pagesize');\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(90);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("&nbsp;默认分页大小(M)：<input type=\"text\" value=\"" + dr["mPageSize"].ToString() + "\" oldvalue=\"" + dr["mPageSize"].ToString() + "\"  class=\"edit\" style=\"width:40px;\" onBlur=\"setModel(this,'mpagesize');\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(91);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("&nbsp;默认行高：<input type=\"text\" value=\"" + dr["LineHeight"].ToString() + "\" oldvalue=\"" + dr["LineHeight"].ToString() + "\"  class=\"edit\" style=\"width:40px;\" onBlur=\"setModel(this,'lineheight');\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(92);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("&nbsp;默认行高(M)：<input type=\"text\" value=\"" + dr["mLineHeight"].ToString() + "\" oldvalue=\"" + dr["mLineHeight"].ToString() + "\"  class=\"edit\" style=\"width:40px;\" onBlur=\"setModel(this,'mlineheight');\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(93);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");

         


            Response.Write("<br>补充自定义列：");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(94);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<br>\r\n");
            Response.Write("<textarea name=\"textarea\" style=\"width:90%;\" rows=\"4\"  onBlur=\"setModel(this,'listfields');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["ListFields"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(dr["ListFields"].ToString()) + "</textarea>");
            Response.Write("<br>\r\n");
            //string value = eOleDB.getValue("select CondValue from a_eke_sysConditions where ModelID='" + modelid + "' and  RoleID is null and UserID is null and delTag=0");           
            //Response.Write("模块条件：<br>");
            //Response.Write("<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlEncode(value) + "\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(value) + "\"  class=\"edit\" style=\"width:90%;\" onBlur=\"setModel(this,'modelcondition');\" /><br>");



            //Response.Write("完整自定义列表查询：<br>");
            //Response.Write("<textarea name=\"textarea\" style=\"width:90%;\" rows=\"4\"  onBlur=\"setModel(this,'listsql');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["ListSQL"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(dr["ListSQL"].ToString()) + "</textarea><br>\r\n");

            Response.Write("默认条件：");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(95);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<br>\r\n");
            Response.Write("<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlEncode(dr["defaultcondition"].ToString()) + "\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["defaultcondition"].ToString()) + "\"  class=\"edit\" style=\"width:90%;\" onBlur=\"setModel(this,'defaultcondition');\" />");
            Response.Write("<br>\r\n");
            Response.Write("默认排序：");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(96);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<br>\r\n");
            Response.Write("<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlEncode(dr["defaultorderby"].ToString()) + "\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["defaultorderby"].ToString()) + "\"  class=\"edit\" style=\"width:90%;\" onBlur=\"setModel(this,'defaultorderby');\" />");
            Response.Write("<br>\r\n");
            

            eList datalist = new eList("a_eke_sysModelItems");
            datalist.Where.Add("ModelID='" + modelid + "' and delTag=0");
            datalist.OrderBy.Add("showlist desc,listorder,px");
            datalist.Bind(Rep);




            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}