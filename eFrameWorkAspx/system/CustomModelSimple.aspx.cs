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
    public partial class CustomModelSimple : System.Web.UI.Page
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
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
        }
        private void List()
        {
            eDataTable.Power = model.Power; //应用模块权限
            elist = new eList("Demo_Persons");
            elist.Where.Add("delTag=0");
            string userCond = eParameters.Replace(model.UserCondition, null, user);//替换用户、URL等参数
            elist.Where.Add(userCond);//用户条件
            elist.Where.AddControl(s1); //按姓名查询
            elist.OrderBy.Default = "addTime desc";//默认排序
            elist.Bind(eDataTable, ePageControl1);
        }
        protected void Action_Actioning(string Actioning)
        {
            eform = new eForm("Demo_Persons", user);
            eform.ModelID = ModelID.Replace("-","_");
            switch (Actioning)
            {
                case "":
                    List();
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