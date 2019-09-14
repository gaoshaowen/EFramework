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
    public partial class Users : System.Web.UI.Page
    {
        public string id = eParameters.Request("id");
        public string act = eParameters.Request("act").ToLower();
        public string sql = "";
        public eForm edt;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            edt = new eForm("a_eke_sysUsers", user);
            //edt.AutoRedirect = false;
            if (act.Length == 0)
            {
                List(user);
                return;
            }
            if (act == "getrole")
            {
                string roleid = eParameters.QueryString("roleid");
                DataTable rolePower = eBase.getUserPowerDefault(roleid, "","");
                eJson json = new eJson(rolePower);
                eBase.Write(json.ToString());
                Response.End();
            }
            #region 信息添加、编辑
            if (act == "active") //是否显示
            {
                string sql = eParameters.Replace("update a_eke_sysUsers set Active='{querystring:value}' where UserID='{querystring:id}'", null, null);
                eOleDB.Execute(sql);
                Response.Redirect(Request.ServerVariables["HTTP_REFERER"] == null ? "Default.aspx" : Request.ServerVariables["HTTP_REFERER"].ToString(), true);
                eBase.End();
            }
            if (act == "getuser")
            {
                sql = "select count(*) from a_eke_sysUsers where yhm='" + eParameters.QueryString("value") + "'";
                string temp = eOleDB.getValue(sql);
                if (temp == "0")
                {
                    Response.Write("true");
                }
                else
                {
                    Response.Write("false");
                }
                Response.End();
            }
            if (act == "edit")
            {
                f1.Attributes = " readOnly";
            }

            edt.AddControl(eFormControlGroup);
            eFormControl _roles = new eFormControl("Roles");
            _roles.Field = "RoleID";
            edt.AddControl(_roles);
            edt.onChange += new eFormTableEventHandler(edt_onChange);
            edt.Handle();
            #endregion
            if (act == "add" || act == "edit")
            {
                eBase.clearDataCache("a_eke_sysPowers");
                LitRoles.Text = getRoles(_roles.Value.ToString()); //edt.Fields["RoleID"].ToString() 
            }
        }

        public void edt_onChange(object sender, eFormTableEventArgs e)
        {
    
            if (e.eventType == eFormTableEventType.Inserting)
            {
                if (user["ServiceID"].Length > 0) edt.Fields.Add("ServiceID", user["ServiceID"]);
            }
            string parentID = e.ID;
            if (e.eventType == eFormTableEventType.Deleted)
            {
                sql = "update a_eke_sysPowers set delTag=1 where UserId='" + parentID + "' and ApplicationID is not null";
                eOleDB.Execute(sql);
            }
            if (e.eventType == eFormTableEventType.Updated || e.eventType == eFormTableEventType.Inserted)
            {
              
                string Roles = edt.Fields["RoleID"].ToString();
                DataTable rolePower = eBase.getUserPowerDefault(Roles, "","");
                //eBase.Writeln("rolePower：角色的权限");
                //eBase.PrintDataTable(rolePower);
               
                string name = "";
                string value = "";  


                sql = "select ApplicationID,MC from a_eke_sysApplications where delTag=0 order by px,addTime";
                sql = "select ModelID,MC,Power from a_eke_sysModels where subModel=0 and delTag=0 and Type=1 order by px,addTime";
                DataTable tb = eOleDB.getDataTable(sql);
                //eBase.Writeln("tb：应用下所有模块");
                //eBase.PrintDataTable(tb);
                
                //continue;
                foreach (DataRow _dr in tb.Rows) //应用下所有模块
                {
                    #region 模块
                    DataRow row = rolePower.NewRow();
                    //row["ApplicationID"] = _dr["ApplicationID"].ToString();
                    row["ModelID"] = _dr["ModelID"].ToString();
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        if (row.Table.Columns[i].ColumnName.ToLower() != "modelid")
                        {
                            row[row.Table.Columns[i].ColumnName] = "false";
                        }
                    }
                    DataRow[] rows = rolePower.Select("ModelID='" + _dr["ModelID"].ToString() + "'");
                    if (rows.Length > 0) row = rows[0];

                    //eBase.Writeln("row");
                    //eBase.PrintDataRow(row);
                    DataTable PowerItems = new eJson(_dr["Power"].ToString()).toRows();
                    //eBase.Writeln("PowerItems");
                    //eBase.PrintDataTable(PowerItems);

                    #region 角色的权限
                    eJson rolePowerJson = new eJson();
                    rolePowerJson.Convert = true;
                    #region 基本权限
                    foreach (DataRow dr1 in PowerItems.Rows)
                    {
                        if (row.Table.Columns.Contains(dr1["value"].ToString()))
                        {
                            value = row[dr1["value"].ToString()].ToString();
                            eJson _power = new eJson();
                            _power.Add(dr1["value"].ToString(), value);
                            rolePowerJson.Add(_power);
                        }
                    }
                    #endregion
                    #region 审批权限
                    DataTable PowerCheckUpItems = eOleDB.getDataTable("SELECT CheckMC as text,LOWER(CheckCode) as value FROM a_eke_sysCheckUps where ModelID='" + _dr["ModelID"].ToString() + "' and delTag=0 and LEN(CheckMC)>0 and LEN(CheckCode)>0 order by px,addTime");
                    foreach (DataRow dr1 in PowerCheckUpItems.Rows)
                    {
                        value = row[dr1["value"].ToString()].ToString();
                        eJson _power = new eJson();
                        _power.Add(dr1["value"].ToString(), value);
                        rolePowerJson.Add(_power);
                    }
                    #endregion
                    //eBase.Writeln(rolePowerJson.ToString());
                    #endregion

                    string canList = "0";
                    string cond = "";

                    #region 用户自定义权限
                    eJson selPower = new eJson();
                    selPower.Convert = true;
                    #region 基本权限
                    foreach (DataRow dr1 in PowerItems.Rows)
                    {
                        name = "model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "");
                        value = eParameters.Form(name);
                        if (value.Length == 0) value = "false";
                        eJson _power = new eJson();
                        _power.Add(dr1["value"].ToString(), value);
                        selPower.Add(_power);
                        if (dr1["value"].ToString().ToLower() == "list") canList = value;
                        // eBase.Writeln(dr1["value"].ToString() + "::" +  value);
                    }
                    #endregion
                    #region 审批权限
                    foreach (DataRow dr1 in PowerCheckUpItems.Rows)
                    {
                        name = "model_" + dr1["value"].ToString() + "_" + _dr["ModelID"].ToString().Replace("-", "");
                        value = eParameters.Form(name);
                        if (value.Length == 0) value = "false";
                        eJson _power = new eJson();
                        _power.Add(dr1["value"].ToString(), value);
                        selPower.Add(_power);
                    }
                    #endregion
                    // eBase.Writeln(selPower.ToString());
                    #endregion


                    name = "model_cond_" + _dr["ModelID"].ToString().Replace("-", "");
                    cond = eParameters.Form(name);


                    if (selPower.ToString() == rolePowerJson.ToString() && cond.Length == 0) //与角色的相同
                    {
                        sql = " delete from a_eke_sysPowers where RoleID is Null and ApplicationID is Null and ModelID='" + _dr["ModelID"].ToString() + "' and UserID='" + parentID + "'";
                        eOleDB.Execute(sql);
                        //eBase.Writeln( _dr["ModelID"].ToString() + "::" +  ":::" + cond.Length.ToString());
                    }
                    else
                    {
                        sql = "if exists (select * from a_eke_sysPowers Where RoleID is Null and ApplicationID is Null and ModelID='" + _dr["ModelID"].ToString() + "'  and UserID='" + parentID + "')";
                        sql += " update a_eke_sysPowers set delTag=0,canList='" + canList + "',Condition='" + cond + "',power='" + selPower.ToString() + "' where RoleID is Null and ApplicationID  is Null and ModelID='" + _dr["ModelID"].ToString() + "' and UserID='" + parentID + "'";
                        sql += " else ";
                        sql += "insert into a_eke_sysPowers (ApplicationID,ModelID,UserID,canList,Condition,Power) ";
                        sql += " values (Null,'" + _dr["ModelID"].ToString() + "','" + parentID + "','" + canList + "','" + cond + "','" + selPower.ToString() + "')";
                        eOleDB.Execute(sql);

                    }



                    // eBase.Writeln(_dr["ModelID"].ToString() + "::" + value);
                    
                    #endregion
                }
                eBase.clearDataCache("a_eke_sysPowers");
                //eBase.End();
            }
        }
        private string getRoles(string selRoles)
        {

            string userRoles = eOleDB.getValue("SELECT RoleID FROM a_eke_sysUsers where UserID='" + id + "'");
            sql = "select a.RoleID,a.MC from a_eke_sysRoles a where a.delTag=0 order by addTime";
            DataTable tb = eOleDB.getDataTable(sql);
            StringBuilder sb = new StringBuilder();
            #region 角色
            sb.Append("<div>\r\n");
            foreach (DataRow dr in tb.Rows)
            {
                sb.Append("<span class=\"rolename\">");
                sb.Append("<input type=\"checkbox\" name=\"Roles\" id=\"Roles_" + dr["RoleID"].ToString() + "\" value=\"" + dr["RoleID"].ToString() + "\"" + (userRoles.IndexOf(dr["RoleID"].ToString()) > -1 ? " checked" : "") + " onclick=\"selectRoles(this);\"" + (act == "view" ? " disabled" : "") + " />");
                sb.Append("<label for=\"Roles_" + dr["RoleID"].ToString() + "\">" + dr["MC"].ToString() + "</label>");
                sb.Append("</span>"); 
            }
            sb.Append("</div>\r\n");
            #endregion
            #region 权限
            string name = "";
            DataTable rolePower = eBase.getUserPowerDefault(selRoles, id, "");
            //eBase.Writeln("<hr>");
            //eBase.PrintDataTable(rolePower);
           // eBase.End();
            //eBase.Writeln("<hr>");
            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\">详细权限</a>");
            sb.Append("</div>\r\n");



            sql = "select ModelID,MC,Power from a_eke_sysModels where subModel=0 and delTag=0 and Type=1 order by px,addTime";
            tb = eOleDB.getDataTable(sql);
            //eBase.PrintDataTable(tb);
            //eBase.End();
            string cond = "";
            sb.Append("<div class=\"powerContent\" style=\"display:none;\">\r\n");
            foreach (DataRow _dr in tb.Rows) //应用下所有模块
            {
                #region 模块
                sb.Append("<div class=\"powerModel\">");
                DataRow row = rolePower.NewRow();
                //row["ApplicationID"] = _dr["ApplicationID"].ToString();
                row["ModelID"] = _dr["ModelID"].ToString();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (row.Table.Columns[i].ColumnName.ToLower() != "applicationid" && row.Table.Columns[i].ColumnName.ToLower() != "modelid")
                    {
                        row[row.Table.Columns[i].ColumnName] = "false";
                    }
                }
                DataRow[] rows = rolePower.Select("ModelID='" + _dr["ModelID"].ToString() + "'");
                if (rows.Length > 0) row = rows[0];
                //eBase.PrintDataRow(row);


                sb.Append("<span class=\"modelname\">");

                name = "model_"  + _dr["ModelID"].ToString().Replace("-", "");

                sb.Append("<input type=\"checkbox\" name=\"" + name + "\" id=\"" + name + "\" value=\"true\" onclick=\"userSelectAll(this);\"" + (row["List"].ToString() == "true" ? " checked" : "") + (act == "view" ? " disabled" : "") + " />");
                sb.Append("<label for=\"" + name + "\">" + _dr["mc"].ToString() + "</label>");
                sb.Append("</span>");

                if (id.Length > 0)
                {
                    sql = "select Condition from a_eke_sysPowers where ModelID='" + _dr["ModelID"].ToString() + "' and UserID='" + id + "' and RoleID is Null and ApplicationID is Null and delTag=0 ";
                    cond = eOleDB.getValue(sql);
                }

                name = "model_cond_" +  _dr["ModelID"].ToString().Replace("-", "");

                sb.Append("<span class=\"cond\">");
                sb.Append("条件：<input type=\"text\" class=\"text\" name=\"" + name + "\" value=\"" + cond + "\"" + (act == "view" ? " disabled" : "") + " />");
                sb.Append("</span>");

                DataTable Power = new eJson(_dr["Power"].ToString()).toRows();


                #region 基本权限
                foreach (DataRow dr1 in Power.Rows)
                {
                    name = "model_" + dr1["value"].ToString() + "_"  + _dr["ModelID"].ToString().Replace("-", "");
                    sb.Append("<span class=\"poweritem\">"); 
                    sb.Append("<input type=\"checkbox\" name=\"" + name + "\" id=\"" + name + "\" value=\"true\"" + (row[dr1["value"].ToString()].ToString() == "true" ? " checked" : "") + (act == "view" ? " disabled" : ""));
                    if (dr1["value"].ToString().ToLower() == "list") sb.Append(" onclick=\"userCanelAll(this);\"");
                    sb.Append(" />");
                    sb.Append("<label for=\"" + name + "\">" + dr1["text"].ToString() + "</label>");
                    sb.Append("</span>");
                }
                #endregion
                #region 审批权限
                Power = eOleDB.getDataTable("SELECT CheckMC as text,LOWER(CheckCode) as value FROM a_eke_sysCheckUps where ModelID='" + _dr["ModelID"].ToString() + "' and delTag=0 and LEN(CheckMC)>0 and LEN(CheckCode)>0 order by px,addTime");
                foreach (DataRow dr1 in Power.Rows)
                {
                    if (!row.Table.Columns.Contains(dr1["value"].ToString()))
                    {
                        row.Table.Columns.Add(dr1["value"].ToString(), typeof(string));
                        row[dr1["value"].ToString()] = "false";
                    }
                }

                //eBase.PrintDataRow(row);
                foreach (DataRow dr1 in Power.Rows)
                {
                    //eBase.Writeln(dr1["value"].ToString() + "::" + _dr["ModelID"].ToString());

                    name = "model_" + dr1["value"].ToString() + "_"  + _dr["ModelID"].ToString().Replace("-", "");
                    sb.Append("<span class=\"powercheckupitem\">");
                    sb.Append("<input type=\"checkbox\" name=\"" + name + "\" id=\"" + name + "\" value=\"true\"" + (row[dr1["value"].ToString()].ToString() == "true" ? " checked" : "") + (act == "view" ? " disabled" : "") + " />");
                    sb.Append("<label for=\"" + name + "\">" + dr1["text"].ToString() + "</label>");
                    sb.Append("</span>");

                }
                #endregion
                sb.Append("</div>");
                #endregion
            }
            sb.Append("</div>\r\n");
            #endregion
            return sb.ToString();
        }

        private void List(eUser user)
        {
            eDataTable.CanEdit = true;
            eDataTable.CanDelete = true;
            eList elist = new eList("a_eke_sysUsers");
            elist.Fields.Add("CASE WHEN Active=1 THEN 'images/sw_true.gif' ELSE 'images/sw_false.gif' END as ShowPIC,CASE WHEN Active=1 THEN '0' ELSE '1' END as ShowValue");
            elist.Where.Add("delTag=0");
            //elist.Where.Add("ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'"));
            elist.Where.Add("UserType>1");
            elist.OrderBy.Add("addTime");
            //elist.Bind(Rep, ePageControl1);
            elist.Bind(eDataTable, ePageControl1);
        }
        protected void Rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitBM");
                if (lit != null)
                {
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "用户管理 - " + eConfig.getString("manageName"); 
            }
        }
    }
}