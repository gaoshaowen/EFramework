using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.Examples
{
    public partial class _eForm : System.Web.UI.Page
    {
        public eAction action;
        public eList elist;
        public eForm eform;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = new eAction();
            action.Actioning += action_Actioning;
            action.Listen();
        }
        private void List()
        {
            elist = new eList("Demo_Persons");
            elist.Where.Add("delTag=0");
            elist.OrderBy.Add("addTime desc");
            elist.Bind(eDataTable, ePageControl1);
        }
        protected void action_Actioning(string Action)
        {
            eform = new eForm("Demo_Persons");
            eform.AutoRedirect = false;//关闭自动跳转
            eform.onChange += eform_onChange;
            if (Action.Length == 0)
            {
                List();
            }
            else
            {
                eform.AddControl(eFormControlGroup);//一次添加
                eform.Handle();
            }
        }

        protected void eform_onChange(object sender, eFormTableEventArgs e)
        {
            switch (e.eventType)
            {
                case eFormTableEventType.Inserting:
                    litScript.Text += "添加前事件已调用<br>";
                    break;
                case eFormTableEventType.Inserted:
                    litScript.Text += "添加成功,ID为：" + e.ID + "<br>5秒后返回<br><script>setTimeout(function(){document.location='" + eform.FromURL + "';},5000);</script>";
                    break;
                case eFormTableEventType.Updating:
                    litScript.Text += "更新前事件已调用，即将修改数据的ID为：" + e.ID + "<br>";
                    break;
                case eFormTableEventType.Updated:
                    litScript.Text += "修改成功,ID为：" + e.ID + "<br>5秒后返回<br><script>setTimeout(function(){document.location='" + eform.FromURL + "';},5000);</script>";
                    break;
                case eFormTableEventType.Deleting:
                    litScript.Text += "删除前事件已调用，即将删除数据的ID为：" + e.ID + "<br>";
                    break;
                case eFormTableEventType.Deleted:
                    litScript.Text += "删除成功,ID为：" + e.ID + "<br>5秒后返回<br><script>setTimeout(function(){document.location='" + eform.FromURL + "';},5000);</script>";
                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eForm类-eFrameWork示例中心";
            }
        }
    }
}