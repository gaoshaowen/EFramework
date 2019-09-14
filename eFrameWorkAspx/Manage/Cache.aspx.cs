using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;


namespace eFrameWork.Manage
{
    public partial class Cache : System.Web.UI.Page
    {
        public string act = eParameters.QueryString("act");
        public string name = eParameters.QueryString("name");
        protected void Page_Load(object sender, EventArgs e)
        {


            System.Web.Caching.Cache cache = HttpRuntime.Cache;

            
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
            #region 删除
            if (act == "del")
            {
                if (name == "all")
                {
                    while (CacheEnum.MoveNext())
                    {
                        cache.Remove(CacheEnum.Key.ToString());
                    }
                }
                else
                {
                    HttpRuntime.Cache.Remove(name);
                }
                Response.Redirect("Cache.aspx", true);
            }
            #endregion
            StringBuilder sb = new StringBuilder();           
            while (CacheEnum.MoveNext())
            {

                //cache.Remove(CacheEnum.Key.ToString());
                sb.Append("<span style=\"display:inline-block;width:350px;\">" + CacheEnum.Key.ToString() + "</span><a href=\"?act=del&name=" + CacheEnum.Key.ToString() + "\">移除</a>&nbsp;&nbsp;<a href=\"?act=view&name=" + CacheEnum.Key.ToString() + "\" target=\"_blank\">查看</a><br>");
                if (act == "view" && CacheEnum.Key.ToString().ToLower()==name.ToLower())
                {
                    object obj = HttpRuntime.Cache[name];

                    eBase.Write("<div>" + name + "：<br>");
                    if (obj.GetType().ToString() == "System.String") eBase.Write(obj.ToString());
                    if (obj.GetType().ToString() == "System.Data.DataTable")
                    {
                        eBase.PrintDataTable((DataTable)obj);
                    }
                    //eBase.Write(obj.GetType().ToString());
                    eBase.Write("</div>");
                    eBase.End();
                }
            }
            if (HttpRuntime.Cache.Count > 1)
            {
                sb.Append("<a href=\"?act=del&name=all\">移除所有</a><br>");
            }
            if (HttpRuntime.Cache.Count == 0)
            {
                sb.Append("<a>暂无缓存</a>");
            }
            LitBody.Text = sb.ToString();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "缓存管理 - " + eConfig.getString("manageName");
            }
        }
    }
}