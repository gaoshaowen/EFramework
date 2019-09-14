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
    public partial class createModel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = eParameters.QueryString("name");
            DataTable tb = eOleDB.getTables();
            StringBuilder sb = new StringBuilder();
            DataRow[] rows = name.Length == 0 ? tb.Select() : tb.Select("name='" + name + "'");

            foreach (DataRow dr in rows)
            {
                //说明
                eBase.Writeln("<font color=\"#0000FF\">public class</font> <font color=\"#2B91AF\">" + dr["name"].ToString() + "</font>");
                eBase.Writeln("{");
                //#
                DataTable dt = eOleDB.getColumns(dr["name"].ToString());
                foreach (DataRow _dr in dt.Rows)
                {
                    eBase.Write("<font color=\"#008000\">");
                    eBase.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
                    eBase.Writeln("/// &lt;summary&gt;");
                    eBase.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
                    eBase.Writeln("/// " + _dr["MC"].ToString());
                    eBase.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
                    eBase.Writeln("/// &lt;/summary&gt;");
                    eBase.Write("</font>");
                    if (_dr["PrimaryKey"].ToString() == "1") eBase.Writeln("&nbsp;&nbsp;&nbsp;&nbsp;[Key]");
                    eBase.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
                    eBase.Write("<font color=\"#0000FF\">public ");
                    switch (_dr["type"].ToString())
                    {
                        case "uniqueidentifier":
                            eBase.Write("Guid");
                            break;
                        case "datetime":
                            eBase.Write("DateTime?");
                            break;
                        case "datetime2":
                            eBase.Write("DateTime?");
                            break;
                        case "smalldatetime":
                            eBase.Write("DateTime?");
                            break;
                        case "bit":
                            eBase.Write("bool");
                            break;
                        case "float":
                            eBase.Write("double");
                            break;
                        case "money":
                            eBase.Write("double");
                            break;
                        case "numeric":
                            eBase.Write("double");
                            break;
                        case "int":
                            eBase.Write("int");
                            break;
                        case "bigint":
                            eBase.Write("int");
                            break;
                        case "tinyint":
                            eBase.Write("int");
                            break;
                        case "smallint":
                            eBase.Write("int");
                            break;
                        default:
                            eBase.Write("string");
                            break;
                    }
                    eBase.Write("</font> <font color=\"#2B91AF\">" + _dr["code"].ToString() + "</font>");
                    eBase.Writeln(" { <font color=\"#0000FF\">get</font>; <font color=\"#0000FF\">set</font>; }");
                    //eBase.Writeln(_dr["length"].ToString() + "::" +  "::" + _dr["isnull"].ToString());
                }
                eBase.Writeln("}");
            }
        }
    }
}