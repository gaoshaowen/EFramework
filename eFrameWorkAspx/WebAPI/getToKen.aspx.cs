using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.WebAPI
{
    public partial class getToKen : System.Web.UI.Page
    {  
        protected void Page_Load(object sender, EventArgs e)
        {
      

            DataTable tb;
            eJson ErrJson;
            string sql = "";
            #region 帐号密码登录
            if (Request.Form["username"] != null)
            {
                ErrJson = new eJson();
                sql = "Select top 1 * From a_eke_sysUsers Where delTag=0 and YHM='" + Request.Form["username"].ToString() + "'"; // and Active=1
                tb = eOleDB.getDataTable(sql);

                if (tb.Rows.Count == 0)
                {
                    ErrJson.Add("errcode", "1005");
                    ErrJson.Add("message", "登录信息有误!");
                    eBase.WriteJson(ErrJson);
                }
                else
                {
                    #region 禁用处理
                    if (tb.Rows[0]["Active"].ToString().ToLower() == "false")
                    {

                        ErrJson.Add("errcode", "1014");
                        ErrJson.Add("message", "该用户已被禁用!");
                        eBase.WriteJson(ErrJson);
                    }
                    #endregion
                    if (eBase.GetMD5(Request.Form["password"].ToString(), 16) == tb.Rows[0]["mm"].ToString())
                    {
                        eToken token = new eToken();
                        token.Exp = 7 *24 *  60 * 60; //默认为30分钟，根据实际需要修改。单位：秒。 当前为1天，60分钟*60秒 为一小时
                        token.Add("id", tb.Rows[0]["UserID"].ToString());
                        //token.Add("nickname", tb.Rows[0]["nickname"].ToString());

                        string tokenString = token.Create();
                        eJson json = new eJson();
                        json.Add("errcode", "0");
                        json.Add("message", "请求成功!");
                        json.Add("token", tokenString);


                        #region 日志
                        if (1 == 1)
                        {
                            eTable etb = new eTable("a_eke_sysErrors");
                            etb.Fields.Add("Message", "getToken");
                            etb.Fields.Add("StackTrace", tokenString);
                            etb.Add();
                        }
                        #endregion

                        sql = "if exists (select * from a_eke_sysToKens Where UserID='" + tb.Rows[0]["UserID"].ToString() + "')";
                        sql += "update a_eke_sysToKens set ExpireDate='" + token.ExpireDate.ToString() + "' where  UserID='" + tb.Rows[0]["UserID"].ToString() + "'";
                        sql += " else ";
                        sql += "insert into a_eke_sysToKens (UserID,ExpireDate) ";
                        sql += " values ('" + tb.Rows[0]["UserID"].ToString() + "','" + token.ExpireDate.ToString() + "')";
                        eOleDB.Execute(sql);
                        eBase.WriteJson(json);

                        
                    }
                    else
                    {
                        ErrJson.Add("errcode", "1005");
                        ErrJson.Add("message", "登录信息有误!");
                        eBase.WriteJson(ErrJson);
                    }
                }
            }
            #endregion
            #region 微信小程序登录
            if (Request.Form["code"] != null)
            {
                ErrJson = new eJson();

                string code = Request["code"].ToString();
                string url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", "dmConfig.GetAppID()", "dmConfig.GetAppSecret()", code);
                string result = eBase.getRequest(url);
                eJson _json = new eJson(result);
                string openid = _json.GetValue("openid");

                sql = "Select top 1 * From a_eke_sysUsers Where delTag=0 and openid='" + openid + "'";
                tb = eOleDB.getDataTable(sql);
                eToken token = new eToken();
                if (tb.Rows.Count == 0)
                {
                    eTable etb = new eTable("a_eke_sysUsers");
                    etb.Fields.Add("openid", openid);
                    etb.Fields.Add("nickname", Request.Form["nickname"].ToString());
                    etb.Fields.Add("sex", Request.Form["gender"].ToString());
                    etb.Fields.Add("headimgurl", Request.Form["avatarUrl"].ToString());
                    etb.Fields.Add("country", Request.Form["country"].ToString());
                    etb.Fields.Add("province", Request.Form["province"].ToString());
                    etb.Fields.Add("city", Request.Form["city"].ToString());
                    etb.Add();

                    token.Add("id", etb.ID.ToLower());
                    token.Add("nickname", Request.Form["nickname"].ToString());
                }
                else
                {
                    token.Add("id", tb.Rows[0]["UserID"].ToString());
                    token.Add("nickname", tb.Rows[0]["nickname"].ToString());

                }



                ErrJson.Add("errcode", "0");
                ErrJson.Add("message", "登录成功!");
                ErrJson.Add("token", token.Create());
                eBase.WriteJson(ErrJson);
            }
            #endregion
            Response.End();
        }
    }
}