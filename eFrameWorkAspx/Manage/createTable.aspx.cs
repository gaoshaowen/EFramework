using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Manage
{
    public partial class createTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = eParameters.QueryString("name");
            DataTable tb = eOleDB.getTables();
            StringBuilder sb = new StringBuilder();
            DataRow[] rows = name.Length == 0 ? tb.Select() : tb.Select("name='" + name + "'");

            foreach (DataRow dr in rows)
            {
                string sql = eOleDB.getTableSql(dr["name"].ToString());
                sql = Regex.Replace(sql, "if ", "<font color='#0000FF'>if</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " not ", " <font color='#666666'>not</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " exists ", " <font color='#666666'>exists</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, @"select \* from ", "<font color='#0000FF'>select</font> <font color='#666666'>*</font> <font color='#0000FF'>from</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, "dbo\\.", "<font color='#008080'>dbo</font>.", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, "sysobjects ", "<font color='#3AFF90'>sysobjects</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " where ", " <font color='#0000FF'>where</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " xtype ", " <font color='#008080'>xtype</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " = ", " <font color='#666666'>=</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " and ", " <font color='#666666'>and</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " name ", " <font color='#008080'>name</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, "begin\r\n", "<font color='#0000FF'>begin</font>\r\n", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, "CREATE TABLE ", "<font color='#0000FF'>CREATE TABLE</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " IDENTITY", " <font color='#0000FF'>IDENTITY</font>", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " Default ", " <font color='#0000FF'>Default</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, @"newid\(\)", "<font color='#FF00FF'>newid</font>()", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, @"getdate\(\)", "<font color='#FF00FF'>getdate</font>()", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, " NULL", " <font color='#666666'>NULL</font>", RegexOptions.IgnoreCase);           
             
                sql = Regex.Replace(sql, "\\.", "<font color='#666666'>.</font>", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, ",", "<font color='#666666'>,</font>", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, ";", "<font color='#666666'>;</font>", RegexOptions.IgnoreCase);

                sql = Regex.Replace(sql, " ON ", " <font color='#0000FF'>ON</font> ", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, "PRIMARY KEY", "<font color='#0000FF'>PRIMARY KEY</font>", RegexOptions.IgnoreCase);
              
                sql = Regex.Replace(sql, "EXECUTE ", "<font color='#0000FF'>EXECUTE</font> ", RegexOptions.IgnoreCase);              
                sql = Regex.Replace(sql, " sp_addextendedproperty ", " <font color='#800000'>sp_addextendedproperty</font> ", RegexOptions.IgnoreCase);             

                sql = Regex.Replace(sql, @"\(", "<font color='#666666'>(</font>", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, @"\)", "<font color='#666666'>)</font>", RegexOptions.IgnoreCase);


                sql = Regex.Replace(sql, @"\[", @"<font color='#008080'>[", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, @"\]", @"]</font>", RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, "\r\nend\r\n", "\r\n<font color='#0000FF'>end</font>\r\n", RegexOptions.IgnoreCase);
       
                sql = sql.Replace("\r\n", "<br>");
                sql += "<br><font color='#0000FF'>GO</font><br><br>";
                eBase.Write(sql);
            }
        }
    }
}