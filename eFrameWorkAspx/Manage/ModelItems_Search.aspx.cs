using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.Manage
{
    public partial class ModelItems_Search : System.Web.UI.Page
    {
        private DataTable _alltables;//所有列
        public DataTable AllTables
        {
            get
            {
                if (_alltables == null)
                {
                    string sql = "SELECT id,name FROM sysobjects where (xtype='U' or xtype='V') "; //name!='dtproperties' and 
                    sql += " and charindex('a_eke_sys',lower(name))=0 and name not in (" + eBase.getSystemTables() + ")";
                    sql += " order by name";//crdate";
                    _alltables = eOleDB.getDataTable(sql);
                }
                return _alltables;
            }
        }

        public string modelid = eParameters.QueryString("modelid");
        public string getJsonText(string jsonstr, string name)
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
        DataRow dr;

        private DataTable _columns;
        public DataTable getColumns(string tableName)
        {
            if (_columns == null)
            {
                _columns = eOleDB.getColumns(tableName);               
                DataTable dt = eOleDB.getDataTable("select Code,MC from a_eke_sysModelItems where ModelID='" + modelid + "' and delTag=0 and Custom=0");   
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow[] rows = _columns.Select("Code='" + dr["code"].ToString() + "'");
                    if (rows.Length > 0) rows[0]["mc"] = dr["mc"].ToString();
                }                
            }
            return _columns;
        }
        protected void Page_Load(object sender, EventArgs e)
        { 
            dr = eOleDB.getDataTable("select * from a_eke_sysModels where ModelID='" + modelid + "'").Select()[0];
            
            

           
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:6px;\">");
                Response.Write("<b>搜索</b><br>");
                Response.Write("设置列表搜索条件。");
                Response.Write("</div> ");
            }

            Response.Write("<div style=\"margin:6px 0px 8px 0px;\">");
            Response.Write("&nbsp;搜索列数：<input type=\"text\" value=\"" + dr["searchcolumncount"].ToString() + "\" oldvalue=\"" + dr["searchcolumncount"].ToString() + "\"  class=\"edit\" style=\"width:40px;\" onBlur=\"setModel(this,'searchcolumncount');\" />");
            if (eBase.showHelp()) Response.Write(" <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(107);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">");
            Response.Write("</div> ");

            eList datalist = new eList("a_eke_sysModelConditions");
            datalist.Where.Add("delTag=0 and ModelID='" + modelid + "'");
            datalist.OrderBy.Add("show desc,px,addTime");
            Rep.ItemDataBound += new RepeaterItemEventHandler(Rep_ItemDataBound);
            datalist.Bind(Rep);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());
            Response.End();
        }
        public string getDisplay(string ModelConditionID)
        {
            string count = eOleDB.getValue("select count(*) from a_eke_sysModelConditionItems where ModelConditionID='" + ModelConditionID + "' and deltag=0");
            if (count == "0")
            {
                return "";
            }
            else
            {
                return "display:none;";
            }            
        }
        protected void Rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StringBuilder sb = new StringBuilder();
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitColnums");
                if (lit != null)
                {
                   
                   // sql = "select Code,MC from a_eke_sysModelItems where ModelID='" + modelid + "' and delTag=0 and Custom=0 order by PX";
                    //lit.Text = eOleDB.getOptions(sql, "MC", "Code", DataBinder.Eval(e.Item.DataItem, "Code").ToString());
                    DataTable dt = getColumns(dr["code"].ToString());
                    StringBuilder _sb = new StringBuilder();
                    foreach (DataRow _dr in dt.Rows)
                    {
                        _sb.Append("<option value=\"" + _dr["code"].ToString() + "\"" + (_dr["code"].ToString().ToLower() == DataBinder.Eval(e.Item.DataItem, "Code").ToString().ToLower() ? " selected" : "") + ">" + _dr["mc"].ToString() + "</option>");
                    }
                    lit.Text = _sb.ToString();
                }

                lit = (Literal)ctrl.FindControl("LitObjects");
                #region 绑定对象
                if (lit != null)
                {

                    for (int i = 0; i < AllTables.Rows.Count; i++)
                    {
                        sb.Append("<option value=\"" + AllTables.Rows[i]["name"].ToString() + "\"" + (DataBinder.Eval(e.Item.DataItem, "BindObject").ToString() == AllTables.Rows[i]["name"].ToString() ? " selected" : "") + " title=\"" + AllTables.Rows[i]["name"].ToString() + "\">" + AllTables.Rows[i]["name"].ToString() + "</option>\r\n");
                    }
                    lit.Text = sb.ToString();
                    if (DataBinder.Eval(e.Item.DataItem, "BindObject").ToString().Length > 0)
                    {
                        lit = (Literal)ctrl.FindControl("LitValue");
                        if (lit != null)
                        {
                            sql = "select b.name from sysobjects a inner join  syscolumns b on a.id=b.id where a.name='" + DataBinder.Eval(e.Item.DataItem, "BindObject").ToString() + "' order by b.colid";//b.colid";
                            lit.Text = eOleDB.getOptions(sql, "name", "name", DataBinder.Eval(e.Item.DataItem, "BindValue").ToString());

                            lit = (Literal)ctrl.FindControl("LitText");
                            if (lit != null)
                            {
                                lit.Text = eOleDB.getOptions(sql, "name", "name", DataBinder.Eval(e.Item.DataItem, "BindText").ToString());
                            }
                        }
                    }
                }
                #endregion
                #region 选项
                lit = (Literal)ctrl.FindControl("LitOptions");
                if (lit != null)
                {
                    //HtmlControl hc = (HtmlControl)ctrl.FindControl("spanbind");
                    HtmlGenericControl hc = (HtmlGenericControl)ctrl.FindControl("spanbind");
                    if (hc != null)
                    {
                        //hc.Attributes.Add("style","border:1px solid #ff0000");
                        //Response.Write("has<br>");
                    }
                    sb = new StringBuilder();
                    sql = "select ModelConditionItemID,mc,conditionvalue,px from a_eke_sysModelConditionItems where ModelConditionID='" + DataBinder.Eval(e.Item.DataItem, "ModelConditionID").ToString() + "' and delTag=0 order by px,addTime ";
                    DataTable tb = eOleDB.getDataTable(sql);
                    // sb.Append("<a href=\"?act=addconditem&modelid=" + modelid + "&cid=" + DataBinder.Eval(e.Item.DataItem, "ModelConditionID").ToString() + "\">添加选项</a><br>");


                    sb.Append("<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" widt5h=\"100%\">");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td height=\"25\" width=\"30\" bgc3olor=\"#ffffff\" align=\"center\"><a title=\"添加选项\" href=\"javascript:;\" onclick=\"addModelConditionItem(this,'" + DataBinder.Eval(e.Item.DataItem, "ModelConditionID").ToString() + "');\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>");
                    sb.Append("<td width=\"110\">&nbsp;选项名称</td>");
                    sb.Append("<td width=\"150\">&nbsp;条件</td>");
                    sb.Append("<td width=\"60\">&nbsp;显示顺序</td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    if (tb.Rows.Count > 0)
                    {
                        if (hc != null)
                        {
                            hc.Attributes.Add("style", "display:none;");
                        }
                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            sb.Append("<tr" + ((i + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\">");
                            sb.Append("<td height=\"32\" align=\"center\"><a title=\"删除选项\" href=\"javascript:;\" onclick=\"delModelConditionItem(this,'" + tb.Rows[i]["ModelConditionItemID"].ToString() + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>");
                            sb.Append("<td><input type=\"text\" value=\"" + tb.Rows[i]["mc"].ToString() + "\" oldvalue=\"" + tb.Rows[i]["mc"].ToString() + "\" class=\"edit\"  onBlur=\"setModelConditionItem(this,'" + tb.Rows[i]["ModelConditionItemID"].ToString() + "','mc');\" /></td>");
                            sb.Append("<td><input type=\"text\" value=\"" + tb.Rows[i]["conditionvalue"].ToString() + "\" oldvalue=\"" + tb.Rows[i]["conditionvalue"].ToString() + "\" class=\"edit\"  onBlur=\"setModelConditionItem(this,'" + tb.Rows[i]["ModelConditionItemID"].ToString() + "','conditionvalue');\" /></td>");
                            sb.Append("<td><input reload=\"true\" type=\"text\" value=\"" + (tb.Rows[i]["px"].ToString() == "999999" || tb.Rows[i]["px"].ToString() == "0" ? "" : tb.Rows[i]["px"].ToString()) + "\" oldvalue=\"" + (tb.Rows[i]["px"].ToString() == "999999" || tb.Rows[i]["px"].ToString() == "0" ? "" : tb.Rows[i]["px"].ToString()) + "\" class=\"edit\"  onBlur=\"setModelConditionItem(this,'" + tb.Rows[i]["ModelConditionItemID"].ToString() + "','px');\" /></td>");
                            sb.Append("</tr>");
                        }
                    }
                    sb.Append("</table>");

                    lit.Text = sb.ToString();
                }
                #endregion
            }
        }
    }
}