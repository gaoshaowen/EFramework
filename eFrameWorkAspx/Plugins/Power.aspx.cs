using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Plugins
{
    public partial class Power : System.Web.UI.Page
    {
        public string act = "";
        private string parentModelID = "";
        private string parentID = "";
        string sql = "";
        public string UserArea = "System";
        protected void Page_Load(object sender, EventArgs e)
        {
            Type type = HttpContext.Current.Handler.GetType();//CurrentHandler
            System.Reflection.FieldInfo fi = type.GetField("UserArea");
            if (fi != null) UserArea = fi.GetValue(Activator.CreateInstance(type)).ToString(); 


            act = eParameters.QueryString("act");
            if (act.Length == 0) act = eParameters.Request("act").ToLower();
            parentModelID = eParameters.QueryString("modelid");
            parentID = eParameters.QueryString("id");
            eUser user = new eUser(UserArea);
            switch (act)
            { 
                case "save":
                    #region 保存
                    string jsonstr = eParameters.Form("eformdata_" + parentModelID);
                    eJson json = new eJson(jsonstr);
                    json.Convert = true;
                    json = json.GetCollection("eformdata_" + parentModelID).GetCollection()[0];

                    string Roles = json.GetValue("Roles");
                    eOleDB.Execute("update a_eke_sysUsers set RoleID='" + Roles + "' where UserID='" + parentID + "'");
                    DataTable rolePower = eBase.getUserPowerDefault(Roles, "","");
                    string name = "";
                    string value = "";
                    //eBase.Writeln(Roles);
                    //eBase.PrintDataTable(rolePower);
                    
                    //eBase.Writeln(json.ToString());
  

                    sql = "select ModelID,MC,Power from a_eke_sysModels where subModel=0 and delTag=0 and Type=1 order by px,addTime";
                    DataTable tb = eOleDB.getDataTable(sql);
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
                        DataTable PowerItems = new eJson(_dr["Power"].ToString()).toRows();

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
                            value = json.GetValue(name);
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
                            value = json.GetValue(name);
                            if (value.Length == 0) value = "false";
                            eJson _power = new eJson();
                            _power.Add(dr1["value"].ToString(), value);
                            selPower.Add(_power);
                        }
                        #endregion
                        //eBase.Writeln(selPower.ToString());
                        #endregion

                        name = "model_cond_" + _dr["ModelID"].ToString().Replace("-", "");
                        cond = json.GetValue(name);

                        if (selPower.ToString() == rolePowerJson.ToString() && cond.Length == 0) //与角色的相同
                        {
                            sql = " delete from a_eke_sysPowers where RoleID is Null and ApplicationID is Null  and ModelID='" + _dr["ModelID"].ToString() + "' and UserID='" + parentID + "'";
                            eOleDB.Execute(sql);
                            //eBase.Writeln( _dr["ModelID"].ToString() + "::" +  ":::" + cond.Length.ToString());
                        }
                        else
                        {
                            sql = "if exists (select * from a_eke_sysPowers Where RoleID is Null and ApplicationID is Null  and ModelID='" + _dr["ModelID"].ToString() + "'  and UserID='" + parentID + "')";
                            sql += " update a_eke_sysPowers set delTag=0,canList='" + canList + "',Condition='" + cond + "',power='" + selPower.ToString() + "' where RoleID is Null and ApplicationID is Null  and ModelID='" + _dr["ModelID"].ToString() + "' and UserID='" + parentID + "'";
                            sql += " else ";
                            sql += "insert into a_eke_sysPowers (ApplicationID,ModelID,UserID,canList,Condition,Power) ";
                            sql += " values (NULL,'" + _dr["ModelID"].ToString() + "','" + parentID + "','" + canList + "','" + cond + "','" + selPower.ToString() + "')";
                            eOleDB.Execute(sql);

                        }
                        #endregion
                    }
                    //eBase.End();
                    #endregion
                    eBase.clearDataCache("a_eke_sysPowers");
                    break;
                case "del":
                    eOleDB.Execute("Update a_eke_sysPowers set delTag=1 where UserId='" + parentID + "' and ApplicationID is null");
                    eBase.clearDataCache("a_eke_sysPowers");
                    break;
                default:
                    string selRoles = eOleDB.getValue("Select RoleID from a_eke_sysUsers Where UserId='" + parentID + "'");
                    //eBase.Writeln(selRoles);
                    //if (selRoles.Length == 0) selRoles = "";//默认角色
                    LitRoles.Text = getRoles(selRoles);
                    
                    break;
            }
           

        }
        private string getRoles(string selRoles)
        {
            string id = parentID;
            string userRoles = eOleDB.getValue("SELECT RoleID FROM a_eke_sysUsers where UserID='" + id + "'");
            //eBase.Writeln(userRoles);
            //if (userRoles.Length == 0) userRoles = "784eea07-47d1-4c28-af6a-b9419570b0b5";
            sql = "select a.RoleID,a.MC from a_eke_sysRoles a where a.delTag=0 order by addTime";
            DataTable tb = eOleDB.getDataTable(sql);
            StringBuilder sb = new StringBuilder();
            #region 角色
            sb.Append("<div>\r\n");
            foreach (DataRow dr in tb.Rows)
            {
                sb.Append("<span class=\"rolename\">");
                // radio checkbox
                sb.Append("<input type=\"checkbox\" name=\"Roles\" id=\"Roles_" + dr["RoleID"].ToString() + "\" value=\"" + dr["RoleID"].ToString() + "\"" + (userRoles.IndexOf(dr["RoleID"].ToString()) > -1 ? " checked" : "") + " onclick=\"selectRoles(this);\"" + (act == "view" ? " disabled" : "") + " />");
                sb.Append("<label for=\"Roles_" + dr["RoleID"].ToString() + "\">" + dr["MC"].ToString() + "</label>");
                sb.Append("</span>");
            }
            sb.Append("</div>\r\n");
            #endregion
            #region 权限
            string name = "";
            DataTable rolePower = eBase.getUserPowerDefault(userRoles,id, "");
           // eBase.PrintDataTable(rolePower);

            sb.Append("<div class=\"powerico\">\r\n");
            sb.Append("<a href=\"javascript:;\" class=\"close\" onclick=\"showPower(this);\">详细权限</a>");
            sb.Append("</div>\r\n");
            sb.Append("<div class=\"powerContent\" style=\"display:none;\">\r\n");
            sql = "select ModelID,MC,Power from a_eke_sysModels where subModel=0 and delTag=0 and Type=1 order by px,addTime";
            tb = eOleDB.getDataTable(sql);
            string cond = "";
            foreach (DataRow _dr in tb.Rows) //所有应用
            {
                #region 模块
                sb.Append("<div class=\"powerModel\">");
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
                //eBase.PrintDataRow(row);


                sb.Append("<span class=\"modelname\">");

                name = "model_" + _dr["ModelID"].ToString().Replace("-", "");

                sb.Append("<input type=\"checkbox\" name=\"" + name + "\" id=\"" + name + "\" value=\"true\" onclick=\"userSelectAll(this);\"" + (row["List"].ToString() == "true" ? " checked" : "") + (act == "view" ? " disabled" : "") + " />");
                sb.Append("<label for=\"" + name + "\">" + _dr["mc"].ToString() + "</label>");
                sb.Append("</span>");

                if (id.Length > 0)
                {
                    sql = "select Condition from a_eke_sysPowers where ModelID='" + _dr["ModelID"].ToString() + "' and UserID='" + id + "' and RoleID is Null and ApplicationID is Null and delTag=0 ";
                    cond = eOleDB.getValue(sql);
                }

                name = "model_cond_" + _dr["ModelID"].ToString().Replace("-", "");


                #region 条件-不开放给非开发人员使用
                /*
                     sb.Append("<span class=\"cond\">");
                    sb.Append("条件：<input type=\"text\" class=\"text\" name=\"" + name + "\" value=\"" + cond + "\"" + (act == "view" ? " disabled" : "") + " />");
                    sb.Append("</span>");
                    */
                sb.Append("<input type=\"hidden\" name=\"" + name + "\" value=\"" + cond + "\" />");
                #endregion

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

                    name = "model_" + dr1["value"].ToString() +  "_" + _dr["ModelID"].ToString().Replace("-", "");
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
           // sb.Append("<textarea name=\"json\" id=\"json\" style=\"width:1240px;height:200px;\"></textarea>");
            #endregion
            return sb.ToString();
        }
    }
}