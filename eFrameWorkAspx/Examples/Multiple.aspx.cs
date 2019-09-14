using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Examples
{
    public partial class Multiple : System.Web.UI.Page
    {
        public eAction Action;
        public eList elist;
        public eForm eform;
        public eUser user;
        public string ModelID = eParameters.Request("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            user = null;// new eUser();
            //user.Check();
            Action = new eAction();
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
        }
        private void List()
        {
            s4.AddCondition("是", "show=1");
            s4.AddCondition("否", "show=0");
            //elist = new eList("Demo_Persons", user);
            elist = new eList("Demo_Persons");
            elist.Fields.Add("CASE WHEN Show=1 THEN 'images/sw_true.gif' ELSE 'images/sw_false.gif' END as ShowPIC,CASE WHEN Show=1 THEN '0' ELSE '1' END as ShowValue");
            elist.Where.Add("delTag=0");
            elist.Where.AddControl(eSearchControlGroup);
            elist.OrderBy.Default = "addTime desc";//默认排序
            elist.Bind(eDataTable, ePageControl1);
        }
        protected void Action_Actioning(string Actioning)
        {
            //cform = new eForm("Demo_Persons", user);
            eform = new eForm("Demo_Persons");
            eform.ModelID = "2";
            if (Actioning.Length == 0)
            {
                List();
            }
            else
            {
                if (Actioning.ToLower() == "show") //是否显示
                {
                    
                    //string sql = eParameters.replaceParameters("update Demo_Persons set show='{querystring:value}' where ID='{querystring:id}'");
                    string sql = eParameters.Replace("update Demo_Persons set show='{querystring:value}' where ID='{querystring:id}'" ,null,null);
                    eOleDB.Execute(sql);
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"] == null ? "Default.aspx" : Request.ServerVariables["HTTP_REFERER"].ToString(), true);
                    eBase.End();
                }
                eform.AddControl(eFormControlGroup);
                eform.Handle();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "综合示例-eFrameWork示例中心";
            }
        }
    }
}