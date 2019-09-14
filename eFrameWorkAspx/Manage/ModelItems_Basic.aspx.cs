using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.Manage
{
    public partial class ModelItems_Basic : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        public string getJsonText(string jsonstr,string name)
        {
            StringBuilder sb = new StringBuilder();
            if (jsonstr.Length > 0)
            {
                eJson json = new eJson(jsonstr);
                foreach (eJson m in json.GetCollection())
                {
                    sb.Append("<span style=\"display:inline-block;margin-right:6px;border:1px solid #ccc;padding:3px 12px 3px 12px;\">" + HttpUtility.HtmlDecode(m.GetValue(name)) + "</span>");
                }
            }
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            DataRow dr = eOleDB.getDataTable("select * from a_eke_sysModels where ModelID='" + modelid + "'").Select()[0];


            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:6px;\">");
                Response.Write("<b>基本设置</b><br>管理列在表单的添加、编辑、查看的基本参数。");
                Response.Write("</div> ");
            }


            Response.Write("&nbsp;表单列数：<input type=\"text\" value=\"" + dr["AddColumnCount"].ToString() + "\" oldvalue=\"" + dr["AddColumnCount"].ToString() + "\"  class=\"edit\" style=\"width:40px;\" onBlur=\"setModel(this,'addcolumncount');\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(61);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<br>");
            //Response.Write("&nbsp;保存文本：<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlEncode(dr["TextFields"].ToString()) + "\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["TextFields"].ToString()) + "\"  class=\"edit\" style=\"width:540px;\" onBlur=\"setModel(this,'textfields');\" />");
            //Response.Write("&nbsp;模块权限：<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlEncode(dr["Power"].ToString()) + "\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["Power"].ToString()) + "\"  class=\"edit\" style=\"width:540px;\" onBlur=\"setModel(this,'power');\" />"); 

            Response.Write("&nbsp;保存文本：");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(62);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<br>");
            Response.Write("<textarea reload=\"true\" id=\"model_textfields\" jsonformat=\"[{&quot;text&quot;:&quot;表单Name&quot;,&quot;value&quot;:&quot;frmName&quot;},{&quot;text&quot;:&quot;字段编码&quot;,&quot;value&quot;:&quot;Field&quot;}]\" name=\"textarea\" style=\"width:80%;display:none;\" rows=\"2\"  onBlur=\"setModel(this,'textfields');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["TextFields"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(dr["TextFields"].ToString()) + "</textarea>");
            Response.Write("<img src=\"images/jsonedit.jpg\" style=\"cursor:pointer;margin-right:5px;\" align=\"absmiddle\" onclick=\"Json_Edit('model_textfields');\">");
            Response.Write(getJsonText(dr["TextFields"].ToString(), "Field")); 
            
            
            Response.Write("<br>\r\n");
            Response.Write("&nbsp;模块权限：");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(63);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<br>");
            Response.Write("<textarea reload=\"true\" id=\"model_power\" jsonformat=\"[{&quot;text&quot;:&quot;文本&quot;,&quot;value&quot;:&quot;text&quot;},{&quot;text&quot;:&quot;值&quot;,&quot;value&quot;:&quot;value&quot;}]\" name=\"textarea\" style=\"width:80%;display:none;\" rows=\"2\"  onBlur=\"setModel(this,'power');\" oldvalue=\"" + System.Web.HttpUtility.HtmlEncode(dr["Power"].ToString()) + "\">" + System.Web.HttpUtility.HtmlEncode(dr["Power"].ToString()) + "</textarea>");


            Response.Write("<img src=\"images/jsonedit.jpg\" style=\"cursor:pointer;margin-right:5px;\" align=\"absmiddle\" onclick=\"Json_Edit('model_power');\">");
            Response.Write(getJsonText(dr["Power"].ToString(), "text"));
            Response.Write("<br>\r\n");

            Response.Write("<div style=\"padding:10px 0px 10px 0px;\">");
            Response.Write("<input type=\"button\" name=\"Submit\" value=\"同步编码\" onclick=\"copyCode();\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(64);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("<input type=\"button\" name=\"Submit\" value=\"还原编码\" onclick=\"restoreCode();\" style=\"margin-left:20px;\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(65);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("</div>");
            
            eList datalist = new eList("a_eke_sysModelItems");
            datalist.Where.Add("ModelID='" + modelid + "' and delTag=0 and (Custom=0 or len(ProgrameFile)>0)");
            datalist.OrderBy.Add("showadd desc,addorder, PX, addTime");
            datalist.Bind(Rep);


            eList elist = new eList("a_eke_sysModelItems");
            elist.Where.Add("ModelID='" + modelid + "' and Custom=1");
            elist.OrderBy.Add("px,addTime");
            elist.Bind(RepCustom);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());

            Response.Write("<strong>自定义列：</strong><br />");
            sw = new System.IO.StringWriter();
            htw = new HtmlTextWriter(sw);
            RepCustom.RenderControl(htw);
            RepCustom.Visible = false;
            Response.Write(sw.ToString());

            Response.End();
        }
    }
}