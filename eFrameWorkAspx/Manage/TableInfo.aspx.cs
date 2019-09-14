using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;


namespace eFrameWork.Manage
{
    public partial class TableInfo : System.Web.UI.Page
    {
        public string id = eParameters.Request("id");
        public string act = eParameters.Request("act");
        public string value = eParameters.Request("value");
        public string tbname = eParameters.Request("tbname");
        
        public string sql = "";
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            user.Check();
            if (act == "settabledescription")
            {
                eOleDB.Execute("EXEC sys.sp_addextendedproperty N'MS_Description',N'" + value + "','user','dbo','table','" + tbname + "',NULL,NULL");
                eOleDB.Execute("EXEC sys.sp_updateextendedproperty N'MS_Description',N'" + value + "','user','dbo','table','" + tbname + "',NULL,NULL");               
                eBase.End();
            }
            if (act == "setcolumndescription")
            {
                string column = Request.QueryString["column"].ToString();
                eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'" + value + "','user','dbo','table','" + tbname + "','column','" + column + "'");
                eOleDB.Execute("EXEC sp_updateextendedproperty N'MS_Description',N'" + value + "','user','dbo','table','" + tbname + "','column','" + column + "'");

                eBase.End();
            }




            DataTable tb = eOleDB.getTables();
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (DataRow dr in tb.Rows)
            {

                sql = "select top 1 value from sys.extended_properties where major_id=(SELECT id from sysobjects where name='" + dr["name"].ToString() + "' and  xtype = 'U') and minor_id=0";
                sql = "select top 1 value from sys.extended_properties where major_id=" +  dr["id"].ToString()+ " and minor_id=0";
                string sm = eOleDB.getValue(sql);
                
                sb.Append("<div>\r\n");
                sb.Append("<span style=\"display:inline-block;width:30px;\">" + i.ToString().PadLeft(3, '0') + "</span>");
                sb.Append("<span onclick=\"show(" + i.ToString() + ",this);\" style=\"display:inline-block;width:300px;" + (sm.Length == 0 ? "color:#ff0000;" : "") + "\" class=\"close\">" + dr["name"].ToString());
                sb.Append("</span>");
                sb.Append("<input type=\"text\" class=\"edit\" value=\"" + sm + "\" onBlur=\"setTableDescription(this,'" + dr["name"].ToString() + "');\" />");
                sb.Append(" <a href=\"javascript:;\" style=\"display:inline-block;margin-left:8px;color:#222;\" onclick=\"createModel('" + dr["name"].ToString() + "');\">生成实体</a>");
                sb.Append(" <a href=\"javascript:;\" style=\"display:inline-block;margin-left:8px;color:#222;\" onclick=\"createTable('" + dr["name"].ToString() + "');\">生成脚本</a>");
                sb.Append("</div>\r\n");
                
                DataTable dt = eOleDB.getColumns(dr["name"].ToString());

                sb.Append("<div id=\"div_" + i.ToString() + "\" style=\"display:none;margin-left:80px;\">\r\n");
                foreach (DataRow _dr in dt.Rows)
                {
                    sb.Append("<span style=\"display:inline-block;width:270px;_width:250px;" + (_dr["MC"].ToString().Length == 0 ? "color:#ff0000;" : "") + "\">" +_dr["code"].ToString() + "</span>");
                    sb.Append("<input type=\"text\" class=\"edit\" value=\"" + _dr["MC"].ToString() + "\"  onBlur=\"setColumnDescription(this,'" + dr["name"].ToString() + "','" + _dr["code"].ToString()  + "');\" /><br>\r\n");
                }
                sb.Append("</div>\r\n");
                i++;
                //eBase.PrintDataTable(dt);
            }
            //eBase.PrintDataTable(tb);
            LitBody.Text = sb.ToString();
        }



        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "数据结构 - " + eConfig.getString("manageName"); 
            }
        }
    }
}