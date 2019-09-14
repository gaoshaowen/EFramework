using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using EKETEAM.Data;
using EKETEAM.FrameWork;


namespace eFrameWork.Manage
{
    public partial class getFrameSQL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = eOleDB.getTableSql("a_eke_sysActions") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysAllowDomain") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysConditions") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysDataContents") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysDataViews") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysErrors") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysModelConditionItems") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysModelConditions") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysModelItems") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysModelPanels") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysModels") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysModelTabs") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysPowers") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysRoles") + "\r\nGO\r\n\r\n";


            sql += eOleDB.getTableSql("a_eke_sysToKens") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysUserColumns") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysUserCustoms") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysUserLog") + "\r\nGO\r\n\r\n";
            sql += eOleDB.getTableSql("a_eke_sysUsers") + "\r\nGO\r\n\r\n";



            sql += "if not exists (select * from a_eke_sysUsers where yhm = 'eketeam')\r\n";           
            sql += "begin\r\n";
            sql += "insert into a_eke_sysUsers (UserType,YHM,MM,XM) values ('3','eketeam','49ba59abbe56e057','默认用户')\r\n";
            sql += "end\r\n";
            sql += "\r\nGO\r\n\r\n";

            string fileName = "eFrameWork.sql";
            if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower().IndexOf("msie") > -1) fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);  //IE需要编码
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");

            Response.Write(sql);
            Response.End();
        }
    }
}