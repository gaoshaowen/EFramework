using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.system
{
    public partial class CustomModel : System.Web.UI.Page
    {        
        public eModel model;
        public eList elist;
        public eForm eform; 
        public eAction Action;
        public eUser user;
        public string UserArea = "System";
        public string ModelID = eParameters.Request("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {            
            user = new eUser(UserArea);
            user.Check();
            model = new eModel(ModelID, user);


            Action = new eAction();
            Action.User = user;
            Action.ModelID = ModelID;            
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
        }
        private void List()
        {
            eDataTable.User = user;
            eDataTable.ModelID = ModelID;
            eDataTable.Power = model.Power; //应用模块权限
            eDataTable.ShowMenu = true; //显示列的显示、隐藏菜单
            eDataTable.BodySize = true; //允许设置行高
            eDataTable.LineHeight = model.LineHeight; //模块默认或用户自定义的行高

            s4.AddCondition("是", "show=1"); //该方式可添加多个条件
            s4.AddCondition("否", "show=0"); //该方式可添加多个条件
            elist = new eList("Demo_Persons");
            elist.Fields.Add("CASE WHEN Show=1 THEN 'images/sw_true.gif' ELSE 'images/sw_false.gif' END as ShowPIC,CASE WHEN Show=1 THEN '0' ELSE '1' END as ShowValue");
            elist.Where.Add("delTag=0");

            elist.Where.AddControl(eSearchControlGroup);
            elist.OrderBy.Default = "addTime desc";//默认排序
            string userCond = eParameters.Replace(model.UserCondition,null,user);//替换用户、URL等参数
            elist.Where.Add(userCond);//用户条件
            ePageControl1.User = user;
            ePageControl1.showPageSize = true; //显示自定义分页大小下拉
            ePageControl1.PageSize = model.PageSize; //模块默认或用户自定义后的分页大小

            elist.Bind(eDataTable, ePageControl1);
        }
        protected void Action_Actioning(string Actioning)
        {
            eform = new eForm("Demo_Persons", user);
            eform.ModelID = ModelID.Replace("-","_");
            eJson js;
            switch (Actioning)
            {
                case "":
                    List();
                    break;
                case "show"://是否显示
                    if (!Convert.ToBoolean(model.Power["show"]))
                    {
                        eBase.Write("<script>alert('没有权限!');history.back();</script>");
                        eBase.End();
                    }
                    string sql = eParameters.Replace("update Demo_Persons set show='{querystring:value}' where ID='{querystring:id}'", null, null);
                    eOleDB.Execute(sql);
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"] == null ? "Default.aspx" : Request.ServerVariables["HTTP_REFERER"].ToString(), true);
                    eBase.End();
                    break;
                case "removesearch":
                    #region 删除搜索条件
                    sql = "delete from a_eke_sysUserCustoms where UserCustomID='" + eParameters.QueryString("removeid") + "'";
                    eOleDB.Execute(sql);
                    eBase.clearDataCache("a_eke_sysUserCustoms");
                    js = new eJson();
                    js.Add("success", "1");
                    js.Add("message", "删除成功!");
                    js.Add("html", eBase.encode(model.getSearchFilter()));
                    HttpContext.Current.Response.Clear();
                    eBase.Write(js.ToString());
                    eBase.End();
                    #endregion
                    break;
                case "setsearch":
                    #region 保存搜索条件
                    string ApplicationID = eParameters.QueryString("appid");
                    sql = "if exists (select * from a_eke_sysUserCustoms Where " + (ApplicationID.Length == 0 ? "ApplicationID is null" : "ApplicationID='" + ApplicationID + "'") + " and ModelID='" + ModelID + "' and  UserID='" + user.ID + "' and MC='" + eParameters.QueryString("mc") + "' and parName='search')";
                    sql += "update a_eke_sysUserCustoms set parValue='" + eParameters.QueryString("value") + "' where " + (ApplicationID.Length == 0 ? "ApplicationID is null" : "ApplicationID='" + ApplicationID + "'") + " and ModelID='" + ModelID + "' and UserID='" + user.ID + "' and MC='" + eParameters.QueryString("mc") + "'";
                    sql += " else ";
                    sql += "insert into a_eke_sysUserCustoms (ApplicationID,ModelID,UserID,parName,MC,parValue) ";
                    sql += " values (" + (ApplicationID.Length == 0 ? "NULL" : "'" + ApplicationID + "'") + ",'" + ModelID + "','" + user.ID + "','search','" + eParameters.QueryString("mc") + "','" + eParameters.QueryString("value") + "')";
                    eOleDB.Execute(sql);
                    eBase.clearDataCache("a_eke_sysUserCustoms");
                    js = new eJson();
                    js.Add("success", "1");
                    js.Add("message", "保存成功!");
                    js.Add("html", eBase.encode(model.getSearchFilter()));
                    HttpContext.Current.Response.Clear();
                    eBase.Write(js.ToString());
                    eBase.End();
                    #endregion
                    break;
                default:
                    eform.AddControl(eFormControlGroup);
                    eform.Handle();
                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = model.ModelInfo["mc"].ToString() + " - " + eConfig.getString("systemName");
            }
        }
    }
}