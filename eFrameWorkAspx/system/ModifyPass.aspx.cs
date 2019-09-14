using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;


namespace eFrameWork.system
{
    public partial class ModifyPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser("System");
            if (Request.Form["f1"] != null) //读取内容
            {
                string mm = eOleDB.getValue("select mm from a_eke_sysUsers where  Userid='" + user.ID + "'");
                if (mm != eBase.GetMD5(eParameters.Form("f1"), 16))
                {
                    Response.Write("<script>alert('旧密码不正确，修改失败!');document.location='ModifyPass.aspx';</script>");
                    Response.End();
                }
                string sql = "update a_eke_sysUsers set ";
                sql += "mm='" + eBase.GetMD5(eParameters.Form("f2"), 16) + "'";
                sql += " where Userid='" + user.ID + "'";
                eOleDB.Execute(sql);
                Response.Write("<script>alert('新密码修改成功,请牢记!');document.location='ModifyPass.aspx';</script>");
                Response.End();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "修改密码 - " + eConfig.getString("systemName");
            }
        }
    }
}