using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Manage
{
    public partial class TableInfo_Export : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            #region 头部
            sb.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n");
            sb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\r\n");
            sb.Append("xmlns=\"http://www.w3.org/TR/REC-html40\">\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<title>数据结构</title>\r\n");
            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
            sb.Append("<meta name=ProgId content=Excel.Sheet>\r\n");
            sb.Append("<meta name=Generator content=\"Microsoft Excel 10\">\r\n");
            sb.Append("<style>\r\n");
            sb.Append("@page\r\n");
            sb.Append("{margin:.59in .59in .59in .59in;\r\n");
            sb.Append("mso-header-margin:.51in;\r\n");
            sb.Append("mso-footer-margin:.51in;\r\n");
            sb.Append("mso-page-orientation:landscape;}\r\n");
            sb.Append(".style0\r\n");
            sb.Append("{mso-number-format:General;\r\n");
            sb.Append("text-align:general;\r\n");
            sb.Append("vertical-align:middle;\r\n");
            sb.Append("white-space:nowrap;\r\n");
            sb.Append("mso-rotate:0;\r\n");
            sb.Append("mso-background-source:auto;\r\n");
            sb.Append("mso-pattern:auto;\r\n");
            sb.Append("color:windowtext;\r\n");
            sb.Append("font-size:12px;\r\n");
            sb.Append("font-weight:400;\r\n");
            sb.Append("font-style:normal;\r\n");
            sb.Append("text-decoration:none;\r\n");
            sb.Append("font-family:宋体;\r\n");
            sb.Append("mso-generic-font-family:auto;\r\n");
            sb.Append("mso-font-charset:134;\r\n");
            sb.Append("border:none;\r\n");
            sb.Append("mso-protection:locked visible;\r\n");
            sb.Append("mso-style-name:常规;\r\n");
            sb.Append("mso-style-id:0;}\r\n");
            sb.Append("br{mso-data-placement:same-cell;}\r\n");
            sb.Append("td{\r\n");
            sb.Append("mso-style-parent:style0;\r\n");
            sb.Append("padding-top:1px;\r\n");
            sb.Append("padding-right:1px;\r\n");
            sb.Append("padding-left:1px;\r\n");
            sb.Append("padding-bottom:3px;\r\n");
            sb.Append("mso-ignore:padding;\r\n");
            sb.Append("mso-number-format:\"\\@\";\r\n");
            sb.Append("border:.5pt solid black;\r\n");
            sb.Append("bac3kground:white;\r\n");
            sb.Append("mso-pattern:black none;\r\n");
            sb.Append("white-space:normal;\r\n");
            sb.Append("word-wrap:break-word;word-break:break-all;\r\n");
            sb.Append("}\r\n");
            sb.Append(".xl1\r\n");
            sb.Append("{mso-style-parent:style0;\r\n");
            sb.Append("border:none;\r\n");
            sb.Append("backgr4ound:white;\r\n");
            sb.Append("mso-pattern:black none;\r\n");
            sb.Append("white-space:normal;}\r\n");
            sb.Append(".xl2\r\n");
            sb.Append("{mso-style-parent:style0;\r\n");
            sb.Append("mso-number-format:\"\\@\";\r\n");
            sb.Append("border-top:none;\r\n");
            sb.Append("border-right:none;\r\n");
            sb.Append("border-bottom:.5pt solid black;\r\n");
            sb.Append("border-left:none;\r\n");
            sb.Append("backg4round:white;\r\n");
            sb.Append("mso-pattern:black none;\r\n");
            sb.Append("white-space:normal;}\r\n");
            sb.Append("</style>\r\n");
            sb.Append("<!--[if gte mso 9]><xml>\r\n");
            sb.Append("<x:ExcelWorkbook>\r\n");
            sb.Append("<x:ExcelWorksheets>\r\n");
            sb.Append("<x:ExcelWorksheet>\r\n");
            sb.Append("<x:Name>Sheet1</x:Name>\r\n");
            sb.Append("<x:WorksheetOptions>\r\n");
            sb.Append("<x:DefaultRowHeight>225</x:DefaultRowHeight>\r\n");
            sb.Append("<x:Print>\r\n");
            sb.Append("<x:ValidPrinterInfo/>\r\n");
            sb.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>\r\n");
            sb.Append("<x:Scale>82</x:Scale>\r\n");
            sb.Append("<x:HorizontalResolution>1200</x:HorizontalResolution>\r\n");
            sb.Append("<x:VerticalResolution>1200</x:VerticalResolution>\r\n");
            sb.Append("</x:Print>\r\n");
            sb.Append("<x:Selected/>\r\n");
            sb.Append("<x:DoNotDisplayGridlines/>\r\n");
            sb.Append("<x:Panes>\r\n");
            sb.Append("<x:Pane>\r\n");
            sb.Append("<x:Number>3</x:Number>\r\n");
            sb.Append("<x:ActiveRow>2</x:ActiveRow>\r\n");
            sb.Append("<x:ActiveCol>1</x:ActiveCol>\r\n");
            sb.Append("<x:RangeSelection>$B$3:$B$4</x:RangeSelection>\r\n");
            sb.Append("</x:Pane>\r\n");
            sb.Append("</x:Panes>\r\n");
            sb.Append("<x:ProtectContents>False</x:ProtectContents>\r\n");
            sb.Append("<x:ProtectObjects>False</x:ProtectObjects>\r\n");
            sb.Append("<x:ProtectScenarios>False</x:ProtectScenarios>\r\n");
            sb.Append("</x:WorksheetOptions>\r\n");
            sb.Append("<x:PageBreaks>\r\n");
            sb.Append("<x:RowBreaks>\r\n");
            //sb.Append("<asp:Literal id=\"LitBreaks\" runat=\"server\"></asp:Literal>\r\n");
            sb.Append("</x:RowBreaks>\r\n");
            sb.Append("</x:PageBreaks>\r\n");
            sb.Append("</x:ExcelWorksheet>\r\n");
            sb.Append("</x:ExcelWorksheets>\r\n");
            sb.Append("<x:WindowHeight>9000</x:WindowHeight>\r\n");
            sb.Append("<x:WindowWidth>14940</x:WindowWidth>\r\n");
            sb.Append("<x:WindowTopX>240</x:WindowTopX>\r\n");
            sb.Append("<x:WindowTopY>15</x:WindowTopY>\r\n");
            sb.Append("<x:ProtectStructure>False</x:ProtectStructure>\r\n");
            sb.Append("<x:ProtectWindows>False</x:ProtectWindows>\r\n");
            sb.Append("</x:ExcelWorkbook>\r\n");
            sb.Append("</xml><![endif]-->\r\n");
            sb.Append("</head>\r\n");
            sb.Append("<body>\r\n");
            #endregion
           
            DataTable tb = eOleDB.getTables();
            foreach (DataRow dr in tb.Rows)
            {
                appendTableInfo(sb, dr);
            }        
          
            #region 尾部
            sb.Append("</body>\r\n");
            sb.Append("</html>\r\n");
            #endregion

            HttpContext.Current.Response.Clear();
            if (1 == 1)
            {
                string fileName = "数据结构.xls";
                if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower().IndexOf("msie") > -1) fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);  //IE需要编码
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
            }
            eBase.Write(sb.ToString());
            eBase.End();
        }
        private void appendTableInfo(StringBuilder sb,DataRow dr)
        {
            string Width = "920";
            string sql = "select top 1 value from sys.extended_properties where major_id=" + dr["id"].ToString() + " and minor_id=0";
            string sm = eOleDB.getValue(sql);

            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"5\" width=\"920\">\r\n");
            sb.Append("<colgroup>\r\n");
            sb.Append("<col width=\"60\" />\r\n");
            sb.Append("<col width=\"120\" />\r\n");
            sb.Append("<col width=\"150\" />\r\n");
            sb.Append("<col width=\"150\" />\r\n");
            sb.Append("<col width=\"80\" />\r\n");
            sb.Append("<col width=\"80\" />\r\n");
            sb.Append("<col width=\"80\" />\r\n");
            sb.Append("<col width=\"120\" />\r\n");
            sb.Append("</colgroup>\r\n");
            sb.Append("<thead>\r\n");
            sb.Append("<tr>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" height=\"40\" width=\"60\"><b>序号</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"120\"><b>编码</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>名称</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"150\"><b>数据类型</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"80\"><b>长度</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"80\"><b>主键</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"80\"><b>允许为空</b></td>\r\n");
            sb.Append("<td bgcolor=\"#E7E7E7\" width=\"120\"><b>默认值</b></td>\r\n");
            sb.Append("</tr>\r\n");
            sb.Append("</thead>\r\n");
            sb.Append("<tbody>");

            sb.Append("<tr>\r\n");
            sb.Append("<td height=\"40\" bgcolor=\"#FAFAFA\" colspan=\"8\">" + dr["name"].ToString() + (sm.Length > 0 ? " (" + sm + ")" : "") + "</td>\r\n");
            sb.Append("</tr>\r\n");

            DataTable dt = eOleDB.getColumns(dr["name"].ToString());
            int row = 0;
            foreach (DataRow _dr in dt.Rows)
            {
                row++;
                sb.Append("<tr>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\" height=\"30\">" + _dr["px"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["code"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["mc"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["type"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["length"].ToString() + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["PrimaryKey"].ToString().Replace("1", "True").Replace("0", "False") + "</td>\r\n");
                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\">" + _dr["isnull"].ToString().Replace("1", "True").Replace("0", "False") + "</td>\r\n");
                string defaultvalue = _dr["default"].ToString();
                if (defaultvalue.Length > 0) defaultvalue = defaultvalue.Substring(1, defaultvalue.Length - 2);

                sb.Append("<td bgcolor=\"" + (row % 2 == 0 ? "#FAFAFA" : "#FFFFFF") + "\" width=\"90\">" + defaultvalue + "</td>\r\n");
                sb.Append("</tr>\r\n");
            }

            sb.Append("</tbody>\r\n");
            sb.Append("</table>");
        }

    }
}