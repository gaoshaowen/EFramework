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
    public partial class _eOleDB : System.Web.UI.Page
    {
        public eAction action;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = new eAction();
            action.Actioning += action_Actioning;
            action.Listen();
        }
        protected void action_Actioning(string Action)
        {
            string sql = "";
            string ID = "";
            switch (Action)
            {
                case "":
                    litBody.Text = "无";
                    break;
                case "add":
                    sql = "insert into Demo_Persons (FullName,Account,PassWord) values ('测试','test','" + eBase.GetMD5("123456",16) +"')";
                    eOleDB.Execute(sql);
                    litBody.Text = "添加成功,ID=" + eOleDB.ID;
                    break;
                case "edit":
                    //sql = "update Demo_Persons set FullName=FullName + 'a' where ID=3";//修改指定条件记录
                    sql = "select ID from Demo_Persons where delTag=0 order by ID desc"; //修改最后一条记录
                    ID = eOleDB.getValue(sql);
                    sql = "update Demo_Persons set FullName=FullName + 'a' where ID=" + ID;
                    eOleDB.Execute(sql);
                    litBody.Text = "修改成功,ID=" + ID;
                    break;
                case "del":
                    sql = "select ID from Demo_Persons where delTag=0 order by ID desc";
                    ID = eOleDB.getValue(sql); //要删除记录的ID

                    //sql = "update Demo_Persons set delTag=1 where ID=" + ID;//假删除
                    sql = "delete from Demo_Persons where ID=" + ID;//真删除
                    eOleDB.Execute(sql);
                    litBody.Text = "删除成功,ID=" + ID;
                    break;
                case "list":
                    sql = "select top 5 ID,FullName from Demo_Persons where delTag=0 order by NewID()";//随机取
                    DataTable tb = eOleDB.getDataTable(sql);
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow dr in tb.Rows)
                    {
                        sb.Append("ID=" + dr["ID"].ToString() + ",FullName=" + dr["FullName"].ToString() + "<BR>\r\n");
                    }
                    litBody.Text = sb.ToString();
                    break;
                case "page":
                    sql = "select ID,FullName from Demo_Persons where delTag=0 order by ID desc";
                    int page = 1;
                    int pagesize = 2;
                    int pagecount = 0;
                    int recordscount = 0;
                    DataTable dt = eOleDB.getDataTable(sql,pagesize,page,out recordscount,out pagecount);
                    StringBuilder sbr = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        sbr.Append("ID=" + dr["ID"].ToString() + ",FullName=" + dr["FullName"].ToString() + "<BR>\r\n");
                    }
                    sbr.Append("共" + recordscount.ToString() + "条信息,分" + pagecount.ToString() + "页显示,每页显示" + pagesize.ToString() + "条,当前显示第" + page.ToString() + "页");
                    litBody.Text = sbr.ToString();
                    break;
            }
            
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "eOleDB类-eFrameWork示例中心";
            }
        }
    }
}