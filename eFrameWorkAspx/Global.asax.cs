using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using EKETEAM.Data;

namespace eFrameWork
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.QueryString["fromURL"] != null && Request.QueryString["fromURL"].ToString().IndexOf("://") > -1)
            {
                Response.Write("<script>alert('请勿非法提交!');history.back();<" + @"/" + "script>");
                Response.End();
            }
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString().Length > 36)
            {
                Response.Write("<script>alert('请勿非法提交!');history.back();<" + @"/" + "script>");
                Response.End();
            }
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Length > 36)
            {
                Response.Write("<script>alert('请勿非法提交!');history.back();<" + @"/" + "script>");
                Response.End();
            }

            try
            {
                string getkeys = "";
                if (HttpContext.Current.Request.QueryString != null)
                {

                    for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
                    {
                        getkeys = HttpContext.Current.Request.QueryString.Keys[i];
                        if (getkeys.ToLower() == "postdata") continue;
                        if (!ProcessSqlStr(getkeys, HttpContext.Current.Request.QueryString[getkeys], 0))
                        {
                            HttpContext.Current.Response.Write("<script>alert('请勿非法提交!');history.back();<" + @"/" + "script>");
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                if (HttpContext.Current.Request.Form != null)
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                    {
                        getkeys = HttpContext.Current.Request.Form.Keys[i];
                        if (!ProcessSqlStr(getkeys, HttpContext.Current.Request.Form[getkeys], 1))
                        {
                            HttpContext.Current.Response.Write("<script>alert('请勿非法提交!');history.back();<" + @"/" + "script>");
                            HttpContext.Current.Response.End();
                        }
                    }
                }
            }
            catch
            {
                // 错误处理: 处理用户提交信息!
                HttpContext.Current.Response.End();
            }
            eOleDB.Start();
        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //Response.Write("END");
            eOleDB.End();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        private static bool ProcessSqlStr(string col, string Str, int type)
        {
            if (Str.Length == 0) { return true; }
            string SqlStr;
            if (type == 0) //QueryString
            {
                SqlStr = "'|exists| and |exec|insert |select |delete |update |count|*|chr|mid|master|truncate |char|declare |script";
            }
            else //Form
            {
                //SqlStr = "'|exec |insert |select |delete |update |count |chr|mid|master |truncate |char |declare |and |script";
                SqlStr = "exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare |and |script";
            }
            string path = HttpContext.Current.Request.ServerVariables["Url"].ToLower();

            if (path.IndexOf("/system/") > -1) return true;//排除目录
            if (path.IndexOf("/manage/") > -1) return true;//排除目录
            if (path.IndexOf("/systemhui/") > -1) return true;//排除目录
            bool ReturnValue = true;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss) >= 0)
                        {
                            ReturnValue = false;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }

    }
}