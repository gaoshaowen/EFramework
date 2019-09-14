using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Manage
{
    public partial class ModelItems_Columns : System.Web.UI.Page
    {
        public string act = eParameters.QueryString("act");
        public string modelid = eParameters.QueryString("modelid");
        #region 属性
        private string sql = "";
        private string _modelid = eParameters.QueryString("modelid");
        public string ModelID
        {
            get
            {
                return _modelid;
            }
        }
        private string _objectid = "";
        public string ObjectID
        {
            get
            {
                if (_objectid.Length == 0)
                {
                    _objectid = eOleDB.getValue("SELECT id from sysobjects where name='" + TableName + "'");
                }
                return _objectid;
            }
        }
        private string _tablename = "";
        public string TableName
        {
            get
            {
                if (_tablename.Length == 0)
                {
                    _tablename = eOleDB.getValue("select code from a_eke_sysModels where ModelID='" + ModelID + "'");
                }
                return _tablename;
            }
        }


        private DataTable _columns;//所有列
        public DataTable Columns
        {
            get
            {
                if (_columns == null)
                {
                    sql = "SELECT a.name as code,b.[name] as type,a.length,d.text as [default],e.value as MC,a.colid as PX from syscolumns a";
                    sql += " inner join systypes b on a.xtype=b.xusertype ";
                    sql += " left join sysobjects c on a.cdefault=c.id and a.cdefault>0";
                    sql += " left join syscomments d on c.id=d.id";
                    sql += " left join sys.extended_properties e on e.major_id=a.id and e.minor_id=a.colid";
                    sql += " where a.id='" + ObjectID + "'";
                    sql += " order by a.colorder";
                    _columns = eOleDB.getDataTable(sql);
                    if (_columns.Rows.Count == 0)
                    {
                        sql = "SELECT a.name as code,b.[name] as type,a.length,d.text as [default],e.value as MC,a.colid as PX from syscolumns a";
                        sql += " inner join systypes b on a.xtype=b.xusertype ";
                        sql += " left join sysobjects c on a.cdefault=c.id and a.cdefault>0";
                        sql += " left join syscomments d on c.id=d.id";
                        sql += " left join sysproperties e on a.id=e.id and a.colid=e.smallid";
                        sql += " where a.id='" + ObjectID + "'";
                        sql += " order by a.colorder";

                        _columns = eOleDB.getDataTable(sql);
                    }

                }
                return _columns;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\">");
                Response.Write("<b>数据结构</b><br>管理数据库物理结构<br>");
                Response.Write("</div> ");
            }
            List();
            DataRow dr = eOleDB.getDataTable("select * from a_eke_sysModels where ModelID='" + modelid + "'").Select()[0];

            if (dr["Code"].ToString().Length > 0)
            {
                Response.Write("<div style=\"margin:6px 0px 8px 0px;\">");
                Response.Write("&nbsp;表名：<input type=\"text\" value=\"" + dr["Code"].ToString() + "\" oldvalue=\"" + dr["Code"].ToString() + "\"  class=\"edit\" style=\"width:180px;\" onBlur=\"setModel(this,'code');\" />");
                if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(56);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                Response.Write("</div> ");
            }
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            LitBody.RenderControl(htw);
            LitBody.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());
            Response.End();

        }
        private void List()
        {
            StringBuilder sb = new StringBuilder();
            string ct = "";
            if (Columns.Rows.Count > 0)
            {
                ct = "0";// eOleDB.getValue("select count(*) from a_eke_sysModelItems where Custom=0 and ModelID='" + ModelID + "'");
                sb.Append("<table id=\"eDataTable_Columns\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" wi3dth=\"100%\" style=\"margin-top:0px;\">\r\n");
                sb.Append("<thead>\r\n");
                sb.Append("<tr>\r\n");
                sb.Append("<td width=\"50\" align=\"center\"><a title=\"添加列\" href=\"javascript:;\" style=\"margin:0px;\" onclick=\"addColumn();\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>\r\n");
                
                sb.Append("<td width=\"60\" align=\"center\"><input type=\"checkbox\" onclick=\"selColumnAll(this);\" name=\"selall\" id=\"selall\" style=\"margin-right:8px;\" value=\"0\"" + (ct == "0" ? "" : " checked") + " />");
                if (eBase.showHelp()) sb.Append("<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(57);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">");
                sb.Append("</td>\r\n");
                sb.Append("<td wid4th=\"150\">编码");
                if (eBase.showHelp()) sb.Append(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(58);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                sb.Append("</td>\r\n");
                sb.Append("<td wid4th=\"180\">说明");
                if (eBase.showHelp()) sb.Append(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(59);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                sb.Append("</td>\r\n");
                sb.Append("<td wid4th=\"180\">数据类型</td>\r\n");
                sb.Append("<td wid4th=\"60\">长度</td>\r\n");
                sb.Append("<td wid4th=\"100\">默认值");
                if (eBase.showHelp()) sb.Append(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(60);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
                sb.Append("</td>\r\n");
                sb.Append("<td wid4th=\"60\">主键</td>\r\n");
                //sb.Append("<td width=\"60\" align=\"center\">顺序</td>\r\n");
                sb.Append("<td width=\"50\">顺序</td>\r\n");
                sb.Append("</tr>\r\n");
                sb.Append("</thead>\r\n");
                sb.Append("<tbody eSize=\"false\" eMove=\"true\">\r\n");

                string zj = eOleDB.getPrimaryKey(TableName);

                string syscolumns = eConfig.getAllSysColumns() + zj.ToLower() + ",";

                for (int i = 0; i < Columns.Rows.Count; i++)
                {
                    sb.Append("<tr" + ((i + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + ">\r\n");

                    sb.Append("<td height=\"32\" align=\"center\">");
                    if (syscolumns.IndexOf("," + Columns.Rows[i]["code"].ToString().ToLower() + ",") > -1)
                    {
                        sb.Append("&nbsp;");
                    }
                    else
                    {
                        sb.Append("<a title=\"删除列\" style=\"margin:0px;" + ("sys" == "True" ? "display:none;" : "") + "\" href=\"javascript:;\" onclick=\"delColumn(this,'" + Columns.Rows[i]["code"].ToString() + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a>");
                    }
                    sb.Append("</td>\r\n");
                    sb.Append("<td align=\"center\">\r\n");
                    ct = eOleDB.getValue("select count(*) from a_eke_sysModelItems where ModelID='" + ModelID + "' and Code='" + Columns.Rows[i]["code"].ToString() + "' and delTag=0");

                    sb.Append("<input type=\"checkbox\" onclick=\"selColumn(this,'" + Columns.Rows[i]["code"].ToString() + "');\" name=\"selItem\" value=\"" + Columns.Rows[i]["code"].ToString() + "\"" + (ct == "0" ? "" : " checked") + " />\r\n");
                    sb.Append("</td>\r\n");
                    sb.Append("<td><input reload=\"true\" type=\"text\" oldvalue=\"" + Columns.Rows[i]["code"].ToString() + "\" value=\"" + Columns.Rows[i]["code"].ToString() + "\" class=\"edit\" onBlur=\"ReNameColumn(this);\" /></td>\r\n");
                    sb.Append("<td><input type=\"text\" oldvalue=\"" + Columns.Rows[i]["mc"].ToString() + "\" value=\"" + Columns.Rows[i]["mc"].ToString() + "\"  class=\"edit\" onBlur=\"ColumnName(this,'" + Columns.Rows[i]["code"].ToString() + "',this.value);\" /></td>\r\n");
                    sb.Append("<td>");

                    sb.Append("<select reload=\"true\" oldvalue=\"" + Columns.Rows[i]["type"].ToString() + "\" onChange=\"ColumnType(this,'" + Columns.Rows[i]["code"].ToString() + "',this.value);\" style=\"width:160px;\">\r\n");

                    sb.Append("<option value=\"uniqueidentifier\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "uniqueidentifier" ? " selected" : "") + ">GUID(uniqueidentifier)</option>\r\n");
                    if (zj.ToLower() != Columns.Rows[i]["code"].ToString().ToLower()) sb.Append("<option value=\"nvarchar\"" + (Columns.Rows[i]["type"].ToString().ToLower().IndexOf("varchar") > -1 ? " selected" : "") + ">文本(nvarchar)</option>\r\n");
                    sb.Append("<option value=\"int\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "int" ? " selected" : "") + ">数字(int)</option>\r\n");
                    if (zj.ToLower() != Columns.Rows[i]["code"].ToString().ToLower()) sb.Append("<option value=\"float\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "float" ? " selected" : "") + ">小数(float)</option>\r\n");
                    if (zj.ToLower() != Columns.Rows[i]["code"].ToString().ToLower()) sb.Append("<option value=\"datetime\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "datetime" ? " selected" : "") + ">时间(datetime)</option>\r\n");
                    if (zj.ToLower() != Columns.Rows[i]["code"].ToString().ToLower()) sb.Append("<option value=\"bit\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "bit" ? " selected" : "") + ">是/否(bit)</option>\r\n");
                    if (zj.ToLower() != Columns.Rows[i]["code"].ToString().ToLower()) sb.Append("<option value=\"text\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "text" ? " selected" : "") + ">备注(text)</option>\r\n");
                    sb.Append("<select>\r\n");


                    sb.Append("</td>\r\n");
                    sb.Append("<td>");

                    if (Columns.Rows[i]["type"].ToString().ToLower().IndexOf( "varchar") > -1)
                    {
                        sb.Append("<input type=\"text\" oldvalue=\"" + Columns.Rows[i]["length"].ToString() + "\" value=\"" + (Columns.Rows[i]["type"].ToString().ToLower() == "nvarchar" ? (Convert.ToInt32(Columns.Rows[i]["length"]) / 2).ToString() : Columns.Rows[i]["length"].ToString()) + "\" class=\"edit\" onBlur=\"ColumnLength(this,'" + Columns.Rows[i]["code"].ToString() + "',this.value);\" />");
                    }
                    else
                    {
                        sb.Append(Columns.Rows[i]["type"].ToString().ToLower() == "nvarchar" ? (Convert.ToInt32( Columns.Rows[i]["length"])/2).ToString() : Columns.Rows[i]["length"].ToString());
                    }
                    sb.Append("</td>\r\n");

                    string _default = Columns.Rows[i]["default"].ToString();
                    if (_default.Length > 0)
                    {
                        if (_default.IndexOf("((") > -1)
                        {
                            _default = _default.Substring(2, _default.Length - 4);
                        }
                        else
                        {
                            _default = _default.Substring(1, _default.Length - 2);
                        }
                    }
                    sb.Append("<td><input type=\"text\" oldvalue=\"" + _default + "\" value=\"" + _default + "\" class=\"edit\" onBlur=\"ColumnDefault(this,'" + Columns.Rows[i]["code"].ToString() + "',this.value);\" /></td>\r\n");
                    sb.Append("<td align=\"center\">" + (zj.ToLower() == Columns.Rows[i]["code"].ToString().ToLower() ? "是" : "&nbsp;") + "</td>\r\n");

                    //sb.Append("<td align=\"center\">" + (i + 1).ToString() + "</td>\r\n");
                   


                    sb.Append("<td align=\"center\" style=\"cursor:move;\" title=\"鼠标按下拖动改变列位置!\">" + (i + 1).ToString() + "</td>\r\n");
                    sb.Append("</tr>\r\n");
                }
                sb.Append("</tbody>\r\n");
                sb.Append("</table>");
            }

            LitBody.Text = sb.ToString();
        }
    }
}