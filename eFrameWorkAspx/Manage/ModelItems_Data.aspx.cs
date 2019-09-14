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
    public partial class ModelItems_Data : System.Web.UI.Page
    {
        private DataTable _alltables;//所有表
        public DataTable AllTables
        {
            get
            {
                if (_alltables == null)
                {
                    string sql = "SELECT id,name FROM sysobjects where (xtype='U' or xtype='V') "; //name!='dtproperties' and 
                    sql += " and (charindex('a_eke_sys',lower(name))=0 or lower(name)='a_eke_sysusers' or lower(name)='a_eke_sysroles' or lower(name)='a_eke_sysmodels')";
                    sql += " and (name not in (" + eBase.getSystemTables() + ") or  lower(name)='a_eke_sysmodels' or lower(name)='a_eke_sysroles')";

                    //sql += " and (name not in (" + eBase.getSystemTables() + ") or  lower(name)='a_eke_sysmodels' or lower(name)='a_eke_sysusers')";
                    sql += " order by name";//crdate";
                    _alltables = eOleDB.getDataTable(sql);
                }
                return _alltables;
            }
        }
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
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:8px;\">");
                Response.Write("<b>数据</b><br>");
                Response.Write("设置列(单选框、复选框、下拉框)的选项数据来源。<br>");
                Response.Write("</div> ");
            }

            eList datalist = new eList("a_eke_sysModelItems");
            datalist.Where.Add("ModelID='" + modelid + "' and delTag=0");
            //datalist.Where.Add("(Sys=0 or Code like '%User') and (showAdd=1 or showList=1) and (Code like '%User' or (Custom=1 or ControlType='searchselect' or ControlType='radio' or ControlType='checkbox' or ControlType='select' or ControlType='autoselect'))");//自定义列也要可以取值 Custom=0 and 
            datalist.Where.Add("(showAdd=1 or showList=1)");
            //datalist.Where.Add("sys=0");
            datalist.OrderBy.Add("px");
            Rep.ItemDataBound += new RepeaterItemEventHandler(Rep_ItemDataBound);
            datalist.Bind(Rep);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Rep.RenderControl(htw);
            Rep.Visible = false;//不输出，要在获取后设，不然取不到内容。
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void Rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitObjects");
                if (lit != null)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < AllTables.Rows.Count; i++)
                    {
                        sb.Append("<option value=\"" + AllTables.Rows[i]["name"].ToString() + "\"" + (DataBinder.Eval(e.Item.DataItem, "BindObject").ToString().ToLower() == AllTables.Rows[i]["name"].ToString().ToLower() ? " selected" : "") + " title=\"" + AllTables.Rows[i]["name"].ToString() + "\">" + AllTables.Rows[i]["name"].ToString() + "</option>\r\n");
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
            }
        }
    }
}