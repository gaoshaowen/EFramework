using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;


namespace eFrameWork.Manage
{
    public partial class Roles : System.Web.UI.Page
    {
        public string act = eParameters.Request("act");
        public eForm edt;
        public eAction Action;
        public string id = eParameters.Request("id");
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("a_eke_sysRoles", user);
            //edt.AutoRedirect = false;
            edt.AddControl(eFormControlGroup);
            edt.onChange += new eFormTableEventHandler(edt_onChange);

            Action = new eAction();
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
            if (act == "add" || act == "edit" || act == "copy")
            {
                eBase.clearDataCache("a_eke_sysPowers");
                LitApps.Text = getApps();
            }
        }
        public void edt_onChange(object sender, eFormTableEventArgs e)
        {
            string sql = "";
            if (e.eventType == eFormTableEventType.Inserting || e.eventType == eFormTableEventType.Updating || e.eventType == eFormTableEventType.Deleting)
            {
                
            }
            if (e.eventType == eFormTableEventType.Inserting)
            {
                if (user["ServiceID"].Length > 0) edt.Fields.Add("ServiceID", user["ServiceID"]);
            }
            if (e.eventType == eFormTableEventType.Deleted)
            {
                sql = "update a_eke_sysPowers set delTag=1 where RoleID='" + e.ID + "' and UserId is null and ApplicationID is not null";
                eOleDB.Execute(sql);
            }
            if (e.eventType == eFormTableEventType.Updated || e.eventType == eFormTableEventType.Inserted)
            {
                sql = "select a.ModelID,a.MC,a.Power,b.Power as userPower,b.canList,b.Condition from a_eke_sysModels a ";
                sql += " left join a_eke_sysPowers b on a.ModelID=b.ModelID and b.delTag=0 and b.UserID is null and b.ApplicationID is null and b.RoleID='" + e.ID + "'";
                sql += " where a.subModel=0 and a.delTag=0 and a.Type=1 order by a.px,a.addTime";


                DataTable tb = eOleDB.getDataTable(sql);
                foreach (DataRow _dr in tb.Rows)
                {
                    string name = "model_list_" + _dr["ModelID"].ToString().Replace("-", "");
                    string temp = eParameters.Form(name);

                    if (temp.Length > 0) //有权限
                    {
                        string canList = "0";
                        string cond = "";
                        string power = "";
                        eJson uPower = new eJson();
                        uPower.Convert = true;
                        #region 基本权限
                        DataTable Power = new eJson(_dr["Power"].ToString()).toRows();
                        foreach (DataRow dr1 in Power.Rows)
                        {
                            temp = eParameters.Form("model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", ""));
                            eJson _power = new eJson();
                            if (temp.Length == 0)
                            {
                                _power.Add(dr1["value"].ToString(), "false");
                                if (dr1["value"].ToString().ToLower() == "list") canList = "0";
                            }
                            else
                            {
                                _power.Add(dr1["value"].ToString(), "true");
                                if (dr1["value"].ToString().ToLower() == "list") canList = "1";
                            }
                            uPower.Add(_power);
                        }
                        #endregion
                        #region 审批权限
                        sql = "SELECT CheckMC as text,LOWER(CheckCode) as value FROM a_eke_sysCheckUps where ModelID='" + _dr["ModelID"].ToString() + "' and delTag=0 and LEN(CheckMC)>0 and LEN(CheckCode)>0 order by px,addTime";
                        Power = eOleDB.getDataTable(sql);
                        foreach (DataRow dr1 in Power.Rows)
                        {
                            temp = eParameters.Form("model_" + dr1["value"].ToString() + "_"  + _dr["ModelID"].ToString().Replace("-", ""));
                            eJson _power = new eJson();
                            if (temp.Length == 0)
                            {
                                _power.Add(dr1["value"].ToString(), "false");
                                if (dr1["value"].ToString().ToLower() == "list") canList = "0";
                            }
                            else
                            {
                                _power.Add(dr1["value"].ToString(), "true");
                                if (dr1["value"].ToString().ToLower() == "list") canList = "1";
                            }
                            uPower.Add(_power);
                        }
                        power = uPower.ToString();


                        #endregion
                        name = "model_cond_" + _dr["ModelID"].ToString().Replace("-", "");
                        cond = eParameters.Form(name);


                        sql = "if exists (select * from a_eke_sysPowers where UserID is Null and ApplicationID is null and ModelID='" + _dr["ModelID"].ToString() + "'  and RoleID='" + e.ID + "')";
                        sql += " update a_eke_sysPowers set delTag=0,canList='" + canList + "',Condition='" + cond + "',power='" + power + "' where UserID is Null and ApplicationID is Null and ModelID='" + _dr["ModelID"].ToString() + "'  and RoleID='" + e.ID + "'";
                        sql += " else ";
                        sql += "insert into a_eke_sysPowers (ApplicationID,ModelID,UserID,RoleID,canList,Condition,Power) ";
                        sql += " values (NULL,'" + _dr["ModelID"].ToString() + "',NULL,'" + e.ID + "','" + canList + "','" + cond + "','" + power + "')";

                        eOleDB.Execute(sql);
                    }
                    else //无权限
                    {
                        sql = "update a_eke_sysPowers set canList=0,Power='',Condition='',delTag=1 where userID is Null and ApplicationID is null and ModelID='" + _dr["ModelID"].ToString() + "' and RoleID='" + e.ID + "'";
                        sql = "delete from a_eke_sysPowers where userID is Null and ApplicationID is null and ModelID='" + _dr["ModelID"].ToString() + "' and RoleID='" + e.ID + "'";
                        eOleDB.Execute(sql);
                    }
                }
                eBase.clearDataCache("a_eke_sysPowers");
            }
        }
        private string getApps()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\">详细权限</a>");
            sb.Append("</div>\r\n");

            sb.Append("<div class=\"powerContent\" style=\"display:none;\">\r\n");
            string sql = "select a.ModelID,a.MC,a.Power,b.Power as userPower,b.canList,b.Condition from a_eke_sysModels a ";
            sql += " left join a_eke_sysPowers b on a.ModelID=b.ModelID and b.delTag=0 and b.UserID is null and b.ApplicationID is null and b.RoleID " + (id.Length == 0 ? "is null" : "='" + id + "'");
            sql += " where a.subModel=0 and a.delTag=0 and a.Type=1 order by a.px,a.addTime";
            DataTable tb = eOleDB.getDataTable(sql);
            //eBase.Writeln( sql);
            //eBase.PrintDataTable(tb);
            //eBase.End();
            //eBase.Writeln(dr["MC"].ToString() +  ":::" + sql);
            foreach (DataRow _dr in tb.Rows)
            {
                sb.Append("<div class=\"powerModel\">");
                sb.Append("<span class=\"modelname\">");
                sb.Append("<input type=\"checkbox\" name=\"model_" +  _dr["ModelID"].ToString().Replace("-", "") + "\" id=\"model_" +  _dr["ModelID"].ToString().Replace("-", "") + "\" value=\"true\" onclick=\"userSelectAll(this);\"" + (_dr["canList"].ToString() == "True" ? " checked" : "") + (act == "view" ? " disabled" : "") + " />");
                sb.Append("<label for=\"model_" + _dr["ModelID"].ToString().Replace("-", "") + "\">" + _dr["mc"].ToString() + "</label>");
                sb.Append("</span>");

                sb.Append("<span class=\"cond\">");
                sb.Append("条件：<input type=\"text\" class=\"text\" name=\"model_cond_" + _dr["ModelID"].ToString().Replace("-", "") + "\" value=\"" + _dr["Condition"].ToString() + "\" />");
                sb.Append("</span>");

                DataTable Power = new eJson(_dr["Power"].ToString()).toRows();
                DataTable UPower = new eJson(_dr["UserPower"].ToString()).toColumn();

                #region 基本权限
                foreach (DataRow dr1 in Power.Rows)
                {
                    bool has = false;
                    if (UPower.Rows.Count > 0 && UPower.Columns.Contains(dr1["value"].ToString()))
                    {
                        has = Convert.ToBoolean(UPower.Rows[0][dr1["value"].ToString()].ToString());
                    }
                    sb.Append("<span class=\"poweritem\">");
                    sb.Append("<input type=\"checkbox\" name=\"model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "") + "\" id=\"model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "") + "\" value=\"true\"" + (has == true ? " checked" : "") + (act == "view" ? " disabled" : ""));
                    if (dr1["value"].ToString().ToLower() == "list") sb.Append(" onclick=\"userCanelAll(this);\"");
                    sb.Append(" />");
                    sb.Append("<label for=\"model_" + dr1["value"].ToString() + "_" +  _dr["ModelID"].ToString().Replace("-", "") + "\">" + dr1["text"].ToString() + "</label>");
                    sb.Append("</span>");
                }
                #endregion
                #region 审批权限
                Power = eOleDB.getDataTable("SELECT CheckMC as text,LOWER(CheckCode) as value FROM a_eke_sysCheckUps where ModelID='" + _dr["ModelID"].ToString() + "' and delTag=0 and LEN(CheckMC)>0 and LEN(CheckCode)>0 order by px,addTime");
                foreach (DataRow dr1 in Power.Rows)
                {
                    bool has = false;
                    if (UPower.Rows.Count > 0 && UPower.Columns.Contains(dr1["value"].ToString()))
                    {
                        has = Convert.ToBoolean(UPower.Rows[0][dr1["value"].ToString()].ToString());
                    }
                    sb.Append("<span class=\"powercheckupitem\">");
                    sb.Append("<input type=\"checkbox\" name=\"model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "") + "\" id=\"model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "") + "\" value=\"true\"" + (has == true ? " checked" : "") + (act == "view" ? " disabled" : "") + " />");
                    sb.Append("<label for=\"model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "") + "\">" + dr1["text"].ToString() + "</label>");
                    sb.Append("</span>");
                }
                #endregion
                sb.Append("</div>");

            }

            sb.Append("</div>");

            return sb.ToString();
        }
        protected void Action_Actioning(string Actioning)
        {
            switch (Actioning)
            {
                case "":
                    eList elist = new eList("a_eke_sysRoles");
                    elist.Fields.Add("RoleID,MC,SM,addTime");
                    elist.Where.Add("delTag=0");
                    elist.Where.Add("ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'"));
                    elist.OrderBy.Default = "addTime";//默认排序
                    elist.Bind(eDataTable, ePageControl1);
                    break;
                default:
                    edt.Handle();
                    break;
            }
            //Response.Write(Actioning + "A");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "角色管理 - " + eConfig.getString("manageName"); 
            }
        }
    }
}