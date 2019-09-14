using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class _eTable : System.Web.UI.Page
    {
        private eAction action;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = new eAction();
            action.Actioning += action_Actioning;
            action.Listen();
        }
        protected void action_Actioning(string Action)
        {
            eTable etable;
            string sql = "";
            string ID = "";
            switch (Action)
            {
                case "":
                    litBody.Text = "无";
                    break;
                case "add":
                    etable = new eTable("Demo_Persons");
                    etable.Fields.Add("FullName", "测试1");
                    etable.Fields.Add("Account", "test");
                    etable.Fields.Add("PassWord", eBase.GetMD5("123456", 16));
                    etable.Add();
                    litBody.Text = "添加成功,ID=" + etable.ID + "<br>\r\n";
                    litBody.Text += "所影响行：" + etable.Rows.ToString();
                    break;
                case "edit":
                    sql = "select ID from Demo_Persons where delTag=0 order by ID desc"; //修改最后一条记录
                    ID = eOleDB.getValue(sql);

                    etable = new eTable("Demo_Persons");
                    etable.Fields.Add("FullName", "修改后的姓名");
                    etable.Where.Add("ID=" + ID);
                    etable.Update();
                    litBody.Text = "修改成功,ID=" + ID + "<br>\r\n";
                    litBody.Text += "所影响行：" + etable.Rows.ToString();

                    break;
                case "del":
                    sql = "select ID from Demo_Persons where delTag=0 order by ID desc";
                    ID = eOleDB.getValue(sql); //要删除记录的ID

                    etable = new eTable("Demo_Persons");
                    etable.Where.Add("id=" + ID);
                    //etable.DeleteTrue();//真正删除
                    etable.Delete(); //假删除

                    litBody.Text = "删除成功,ID=" + ID + "<br>\r\n";
                    litBody.Text += "所影响行：" + etable.Rows.ToString();
                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eTable类-eFrameWork示例中心";
            }
        }
    }
}