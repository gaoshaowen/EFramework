using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork
{
    /// <summary>
    /// eFrameWorkService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class eWebService : System.Web.Services.WebService
    {
        //this.Context.Request
        //[WebMethod(Description = "读取信息", EnableSession = true)]  
        private eJson CheckUserInfo(string _json)
        {
            eJson js = new eJson();
            eJson json = new eJson(_json);
            json.Convert = true;

            string UserName = json.GetValue("UserName");
            string PassWord = json.GetValue("PassWord");

            if (UserName.Length == 0 || PassWord.Length == 0)
            {
                js.Add("success", "0");
                js.Add("message", "用户验证信息不完整!");
                return js;
            }

            string sql = "Select top 1 UserID,YHM,MM From a_eke_sysUsers Where delTag=0 and Active=1 and YHM='" + UserName + "'";
            DataTable tb = eOleDB.getDataTable(sql);
            if (tb.Rows.Count == 0)
            {
                js.Add("success", "0");
                js.Add("message", "用户验证信息不正确!");
                return js;
            }
            if (eBase.GetMD5(PassWord, 16) == tb.Rows[0]["mm"].ToString() || PassWord == tb.Rows[0]["mm"].ToString())
            {
                js.Add("success", "1");
                js.Add("message", "验证3成功!");
                eUser user = new eUser("eWebService");
                user["id"] = tb.Rows[0]["UserID"].ToString();
                user["name"] = tb.Rows[0]["YHM"].ToString();
                user.Save();                
                return js;
            }
            else
            {
                js.Add("success", "0");
                js.Add("message", "用户验证信息不正确!");
                return js;
            } 
        }

        [WebMethod(Description = "列表信息", EnableSession = true)]  
        public string list(string _json)
        {
            eUser user = new eUser("eWebService");
            #region 验证
            if (!user.Logined)
            {
                eJson bjs = CheckUserInfo(_json);
                if (bjs.GetValue("success") != "1")
                {
                    return bjs.ToString();
                }
                user = new eUser("eWebService");
            }
            #endregion
            eJson json = new eJson(_json);
            json.Convert = true;
            string ModelID = json.GetValue("ModelID");
            eModelService model = new eModelService(ModelID, user);
            return model.listJson(json);
        }

        [WebMethod(Description = "添加数据", EnableSession = true)]  
        public string addinfo(string _json)
        {
            eUser user = new eUser("eWebService");
            #region 验证
            if (!user.Logined)
            {
                eJson bjs = CheckUserInfo(_json);
                if (bjs.GetValue("success") != "1")
                {
                    return bjs.ToString();
                }
                user = new eUser("eWebService");
            }
            #endregion
            eJson json = new eJson(_json);
            json.Convert = true;
            string ModelID = json.GetValue("ModelID");
            eModelService model = new eModelService(ModelID, user);
            return model.adddata(json);          
        }
        //[WebMethod(CacheDuration = 60,Description = "测试")]

        [WebMethod(Description = "读取信息", EnableSession = true)]  
        public string read(string _json)
        {
          
            eUser user = new eUser("eWebService");

            #region 验证

            if (!user.Logined)
            {               
                eJson bjs = CheckUserInfo(_json);
                if (bjs.GetValue("success") != "1")
                {
                    return bjs.ToString();
                }
                user = new eUser("eWebService");
            }
            #endregion
            eJson json = new eJson(_json);
            json.Convert = true;
            string ModelID = json.GetValue("ModelID");        
            eModelService model = new eModelService(ModelID, user);
            return model.read(json);
        }
        [WebMethod(Description = "保存信息", EnableSession = true)]  
        public string save(string _json) //保存：添加、修改
        {
            eUser user = new eUser("eWebService");
            #region 验证
            if (!user.Logined)
            {
                eJson bjs = CheckUserInfo(_json);
                if (bjs.GetValue("success") != "1")
                {
                    return bjs.ToString();
                }
                user = new eUser("eWebService");
            }
            #endregion
            eJson json = new eJson(_json);
            json.Convert = true;
            string ModelID = json.GetValue("ModelID");         
            eModelService model = new eModelService(ModelID, user);
            return model.save(json);
        }
        [WebMethod(Description = "删除信息", EnableSession = true)]  
        public string remove(string _json)//删除
        {
            eUser user = new eUser("eWebService");
            #region 验证
            if (!user.Logined)
            {
                eJson bjs = CheckUserInfo(_json);
                if (bjs.GetValue("success") != "1")
                {
                    return bjs.ToString();
                }
                user = new eUser("eWebService");
            }
            #endregion
            eJson json = new eJson(_json);
            json.Convert = true;
            string ModelID = json.GetValue("ModelID");           
            eModelService model = new eModelService(ModelID,user);
            return model.remove(json);
        }
    }
}