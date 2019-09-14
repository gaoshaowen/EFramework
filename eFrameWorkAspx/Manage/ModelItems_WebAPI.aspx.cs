using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;
using EKETEAM.Data;
using System.Text;

namespace eFrameWork.Manage
{
    public partial class ModelItems_WebAPI : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:6px;\">");
                Response.Write("<b>WebAPI</b><br>");
                Response.Write("该模块提供的WebAPI接口说明。<br>");
                Response.Write("</div> ");
            }

            StringBuilder sb;
            DataTable dt;
            int row = 0;
            DataRow dr = eOleDB.getDataTable("select * from a_eke_sysModels where ModelID='" + modelid + "'").Select()[0];



            eBase.Writeln("<input type=\"checkbox\" name=\"apisplitpage\" id=\"apisplitpage\" onclick=\"setModel(this,'apisplitpage');\"" + (dr["apisplitpage"].ToString() == "True" ? " checked" : "") + " /><label for=\"apisplitpage\">启用分页</label>");



            #region 令牌接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>令牌接口</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/getToKen.aspx</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">application/x-www-form-urlencoded</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">username</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">用户帐号</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">password</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">帐号密码</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"token\":\"06841095ADDB705...\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1005\",<br>");
            sb.Append("\"message\":\"登录信息有误!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
             #endregion
            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion
            #region 添加接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>添加接口</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");           
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/Model.aspx?modelid=" + modelid + "</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">application/x-www-form-urlencoded</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">act</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">save</td>\r\n");
            sb.Append("</tr>\r\n");
            dt = eOleDB.getDataTable("SELECT MC,frmName FROM a_eke_sysModelItems where ModelID='" + modelid + "' and delTag=0 and showAdd=1 order by addorder, PX, addTime");
            row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                row++;
                sb.Append("<tr>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FFFFFF" : "#FAFAFA") + "\">" + _dr["frmName"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FFFFFF" : "#FAFAFA") + "\">data</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FFFFFF" : "#FAFAFA") + "\">" + _dr["MC"].ToString() + "</td>\r\n");
                sb.Append("</tr>\r\n");
            }
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"id\":\"07677fb8-d2...\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion
            #region 更新接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>更新接口</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/Model.aspx?modelid=" + modelid + "</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">application/x-www-form-urlencoded</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">act</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">save</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">id</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">07677fb8-d2...</td>\r\n");
            sb.Append("</tr>\r\n");
            dt = eOleDB.getDataTable("SELECT MC,frmName FROM a_eke_sysModelItems where ModelID='" + modelid + "' and delTag=0 and showAdd=1 order by addorder, PX, addTime");
            row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                row++;
                sb.Append("<tr>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["frmName"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">data</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["MC"].ToString() + "</td>\r\n");
                sb.Append("</tr>\r\n");
            }
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"id\":\"07677fb8-d2...\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion

            #region 编辑数据接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>编辑数据接口(编辑状态数据)</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/Model.aspx?modelid=" + modelid + "</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">application/x-www-form-urlencoded</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">act</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">edit</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">id</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">07677fb8-d2...</td>\r\n");
            sb.Append("</tr>\r\n");


            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"data\":{");
            row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                if (row > 0) sb.Append(",");
                sb.Append("\"" + _dr["frmName"].ToString() + "\":\"...\"");
                row++;
            }
            sb.Append("}<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion

            #region 查看数据接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>查看数据接口(查看状态数据)</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/Model.aspx?modelid=" + modelid + "</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">application/x-www-form-urlencoded</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">act</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">view</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">id</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">07677fb8-d2...</td>\r\n");
            sb.Append("</tr>\r\n");


            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"data\":{");
            row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                if (row > 0) sb.Append(",");
                sb.Append("\"" + _dr["frmName"].ToString() + "\":\"...\"");
                row++;
            }
            sb.Append("}<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion

            

            #region 数据集合接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>数据集合接口(列表)</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/Model.aspx?modelid=" + modelid + "</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Get</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");

            sb.Append("\"page\":\"1\",<br>");
            sb.Append("\"pagesize\":\"10\",<br>");
            sb.Append("\"pagecount\":\"3\",<br>");
            sb.Append("\"recordscount\":\"30\",<br>");


            sb.Append("\"data\":[");
            sb.Append("{");
            row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                if (row > 0) sb.Append(",");
                sb.Append("\"" + _dr["frmName"].ToString() + "\":\"...\"");
                row++;
            }
            sb.Append("},");
            sb.Append("{");
            row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                if (row > 0) sb.Append(",");
                sb.Append("\"" + _dr["frmName"].ToString() + "\":\"...\"");
                row++;
            }
            sb.Append("}");
            sb.Append("]<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion


            #region 删除接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>删除接口</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "WebAPI/Model.aspx?modelid=" + modelid + "</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">application/x-www-form-urlencoded</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">act</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">del</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">id</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">07677fb8-d2...</td>\r\n");
            sb.Append("</tr>\r\n");


            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"id\":\"07677fb8-d2...\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion

            #region 文件上传接口
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>文件上传接口</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");
            #region 接口地址
            sb.Append("1. 接口地址<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>描述</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">接口地址</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">" + eBase.getBaseURL() + "Plugins/ProUpload.aspx?type=image</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">请求方式</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">Post</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 请求参数
            sb.Append("2. 请求参数<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">auth</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">token</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">contentType</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">header</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">multipart/form-data</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">imgFile</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">data</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">form-data</td>\r\n");
            sb.Append("</tr>\r\n");



            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion
            #region 返回结果
            sb.Append("3. 返回结果(JSON格式)<br>\r\n");
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>说明</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">成功</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"0\",<br>");
            sb.Append("\"message\":\"请求成功!\",<br>");
            sb.Append("\"files\":[{\"url\":\"a.jpg\"}]<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">失败</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">");
            sb.Append("{<br>");
            sb.Append("\"errcode\":\"1004\",<br>");
            sb.Append("\"message\":\"toKen已过期!\"<br>");
            sb.Append("}<br>");
            sb.Append("</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion

            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion


            #region 错误代码
            sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\"><b>错误代码</b></a>");
            sb.Append("</div>\r\n");
            sb.Append("<div style=\"margin-left:20px;display:none;\">\r\n");

            #region 接口地址
            sb.Append("<table border=\"0\" cellpadding=\"8\" cellspacing=\"1\" width=\"100%\">\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>errcode</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\"><b>message</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">0</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">请求成功!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1001</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">toKen不能为空!</td>\r\n");
            sb.Append("</tr>\r\n");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1002</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">toKen已失效!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1003</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">toKen验签失败!</td>\r\n");
            sb.Append("</tr>\r\n");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1004</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">toKen已过期!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1005</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">登录信息错误!</td>\r\n");
            sb.Append("</tr>\r\n");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1006</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">没有添加权限!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1007</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">没有修改权限!</td>\r\n");
            sb.Append("</tr>\r\n");


            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1008</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">没有删除权限!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1009</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">没有查看权限!</td>\r\n");
            sb.Append("</tr>\r\n");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1010</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">没有列表权限!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1011</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">信息不存在或已删除!</td>\r\n");
            sb.Append("</tr>\r\n");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1012</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">访问未被许可!</td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">1013</td>\r\n");
            sb.Append("<td bgcolor=\"#FAFAFA\">该toKen不是本系统颁发!</td>\r\n");
            sb.Append("</tr>\r\n");

            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">1014</td>\r\n");
            sb.Append("<td bgcolor=\"#FFFFFF\">该用户已被禁用!</td>\r\n");
            sb.Append("</tr>\r\n");
            //sb.Append("<tr>\r\n");
            //sb.Append("<td bgcolor=\"#FAFAFA\"></td>\r\n");
            //sb.Append("<td bgcolor=\"#FAFAFA\"></td>\r\n");
            //sb.Append("</tr>\r\n");

            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
            #endregion



            sb.Append("</div>");
            eBase.Write(sb.ToString());
            #endregion

            Response.End();
        }
       
    }
}