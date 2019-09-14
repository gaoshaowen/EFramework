using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Manage
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           string fromURL = eParameters.QueryString("fromURL");
            string LoginFile = "Login.aspx";
            if (fromURL.Length > 0) LoginFile += "?fromURL=" + HttpUtility.UrlEncode(fromURL);
            if (Request.Form["yhm"] != null)
            {
                if (Session["Plugins_RndCode"] == null)
                {
                    Response.Write("<script>alert('验证码超时！');document.location='" + fromURL + "';</script>");
                    Response.End();
                    return;
                }
                if (Session["Plugins_RndCode"].ToString() != Request.Form["yzm"].ToString())
                {
                    Response.Write("<script>alert('验证码不正确！');document.location='" + fromURL + "';</script>");
                    Response.End();
                    return;
                }
                string sql = "Select top 1 UserID,YHM,MM,SiteID,ServiceID From a_eke_sysUsers Where delTag=0 and Active=1 and UserType>2 and YHM='" + Request.Form["yhm"].ToString() + "'";

                #region 绑定登录，防止DEMO用户密码被修改或禁用及删除
                if (Request.Url.Host.ToLower().IndexOf("demo.eketeam.com") > -1)
                {
                    sql = "Select top 1 UserID,YHM,MM,SiteID,ServiceID From a_eke_sysUsers Where YHM='" + Request.Form["yhm"].ToString() + "'";
                }
                #endregion
                DataTable tb = eOleDB.getDataTable(sql);
                if (tb.Rows.Count == 0)
                {
                    Response.Write("<script>alert('登录信息不正确！');document.location='" + fromURL + "';</script>");
                    Response.End();
                }
                else
                {
                    if (eBase.GetMD5(Request.Form["mm"].ToString(), 16) == tb.Rows[0]["mm"].ToString() || Request.Form["mm"].ToString() == tb.Rows[0]["mm"].ToString() || Request.Url.Host.ToLower().IndexOf("demo.eketeam.com") > -1)
                    {

                        eUser user = new eUser("Manage");
                        user["id"] = tb.Rows[0]["UserID"].ToString();
                        user["name"] = tb.Rows[0]["YHM"].ToString();
                        user["siteid"] = tb.Rows[0]["siteid"].ToString();
                        user["ServiceID"] = tb.Rows[0]["ServiceID"].ToString();
                        user.Save();

                        eUser suser = new eUser("System");
                        suser["id"] = tb.Rows[0]["UserID"].ToString();
                        suser["name"] = tb.Rows[0]["YHM"].ToString();
                        suser["siteid"] = tb.Rows[0]["siteid"].ToString();
                        suser.Save();


                        eOleDB.Execute("update a_eke_sysUsers set LastLoginTime=isnull(LoginTime,getdate()) where UserID='" + tb.Rows[0]["UserID"].ToString() + "'");
                        eOleDB.Execute("update a_eke_sysUsers set LoginCount=LoginCount+1,LoginTime=getdate() where UserID='" + tb.Rows[0]["UserID"].ToString() + "'");

                        //用户登录日志
                        eTable etb = new eTable("a_eke_sysUserLog");
                        etb.Fields.Add("UserID", tb.Rows[0]["UserID"]);
                        etb.Fields.Add("Type", 1);
                        etb.Fields.Add("IP", eBase.getIP());
                        etb.Fields.Add("Area", "Manage");
                        etb.Add();

                        if (eParameters.QueryString("fromURL").Length > 0)
                        {
                            Response.Redirect(HttpUtility.UrlDecode(eParameters.QueryString("fromURL")), true);
                        }
                        else
                        {
                            Response.Redirect("Default.aspx", true);
                        }

                    }
                    else
                    {
                        Response.Write("<script>alert('登录信息不正确！');document.location='" + fromURL + "';</script>");
                        Response.End();
                    }
                }
            }
        }
    }
}