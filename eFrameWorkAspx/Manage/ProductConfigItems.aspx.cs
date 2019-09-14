using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Manage
{
    public partial class ProductConfigItems : System.Web.UI.Page
    {
        public string configName = "";
        public string PId = eParameters.QueryString("PId");
        public string act = eParameters.QueryString("act");
        private string item = eParameters.QueryString("item");
        private string value = eParameters.Request("value").Replace("'", "''");
        public string getJsonText(string jsonstr, string name)
        {
            StringBuilder sb = new StringBuilder();
            if (jsonstr.Length > 0)
            {
                eJson json = new eJson(jsonstr);
                foreach (eJson m in json.GetCollection())
                {
                    sb.Append("<span style=\"display:inline-block;margin-right:6px;border:1px solid #ccc;padding:3px 12px 3px 12px;\">" + HttpUtility.HtmlDecode(m.GetValue(name)) + "</span>");
                }
            }
            return sb.ToString();
        }
        private void List()
        {
            #region 列表
            eList elist = new eList("a_eke_sysCheckUps");
            elist.Where.Add("ModelID='" + PId + "' ");
            elist.OrderBy.Add("px,addTime");
            elist.Bind(Rep);
            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            configName = eOleDB.getValue("select configName from ProductConfigs where ProductConfigID='" + PId + "'");
            string sql = "";
            if (act.Length == 0) 
            {
                eList elist = new eList("a_eke_sysCheckUps");
                elist.Where.Add("ModelID='" + PId + "' ");
                elist.OrderBy.Add("px,addTime");
                elist.Bind(Rep);
            }
            else
            {
                #region 获取数据
                if (act == "getdata")
                {
                    Response.Clear();
                    List();
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Rep.RenderControl(htw);
                    Rep.Visible = false;
                    Response.Write(sw.ToString());
                    Response.End();
                }
                #endregion

                #region 流程
                #region 添加
                if (act == "addcheckup")
                {
                    eOleDB.Execute("insert into a_eke_sysCheckUps (ModelID) values ('" + PId + "')");
                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "添加成功");
                    //Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 修改动作
                if (act == "setcheckup")
                {
                    //拖动排序
                    if (item.ToLower() == "setorders")
                    {
                        string ids = eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysCheckUps set px='" + value + "' where ModelID='" + PId + "' and CheckupID='" + arr[i] + "'");
                        }
                        Response.End();
                    }

                    if (item.ToLower() == "px" && (value.Length == 0 || value == "0")) value = "999999";
                    if(item.ToLower()=="backprocess")  value = eBase.decode(value);
                    eOleDB.Execute("update a_eke_sysCheckUps set " + item + "='" + value + "' where ModelID='" + PId + "' and CheckupID='" + eParameters.QueryString("CheckupID") + "'");
                    Response.End();
                }
                #endregion
                #region  删除
                if (act == "delcheckup")
                {
                    eOleDB.Execute("delete from a_eke_sysCheckUps where CheckupID='" + eParameters.QueryString("CheckupID") + "'");
                    Response.End();
                }
                #endregion
                #endregion
       


               
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "产品配置 - " + eConfig.getString("manageName");
            }
        }
    }
}