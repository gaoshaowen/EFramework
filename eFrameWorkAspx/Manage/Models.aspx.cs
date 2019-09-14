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
    public partial class Models : System.Web.UI.Page
    {
        public eList elist;
        public eForm edt;
        public eAction Action;
        public eFormControl f2 = new eFormControl("f2");
        public eUser user;
        eFormControl f7 = new eFormControl("f7");
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            user.Check();
            Action = new eAction();
            
            edt = new eForm("a_eke_sysModels", user);
            //edt.AutoRedirect = false;
            edt.AddControl(f1);//名称
            f2.Field = "Code";
            edt.AddControl(f2);//编码
            edt.AddControl(f3);//简介
            edt.AddControl(f4);//文件
            edt.AddControl(f5);//自动
            edt.AddControl(f6);//类名
            f7.Field = "ParentID";
            f7.FieldType = "uniqueidentifier";
            edt.AddControl(f7);//上级

            edt.AddControl(f8);//关系
            edt.AddControl(f9);//类型
            if (Action.Value == "del")
            {
                string id = eParameters.QueryString("id");              
                eOleDB.Execute("delete from a_eke_sysModels where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysModelItems where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysModelConditions where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysModelConditionItems where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysConditions where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysPowers where ModelID='" + id + "'");            
                eOleDB.Execute("delete from a_eke_sysActions where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysModelTabs where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysModelPanels where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysUserCustoms where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysUserColumns where ModelID='" + id + "'");
                eOleDB.Execute("delete from a_eke_sysCheckUps where ModelID='" + id + "'");
                

                if (Request.ServerVariables["HTTP_REFERER"] == null)
                {
                    Response.Redirect("ClearModel.aspx", true);
                }
                else
                {
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString(), true);
                }
                Response.End();
                //Response.Redirect(Request.UrlReferrer.PathAndQuery, true);
            }
            edt.onChange += new eFormTableEventHandler(edt_onChange);
            edt.Handle();

           
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
        }
        public void edt_onChange(object sender, eFormTableEventArgs e)
        {
            if (e.eventType == eFormTableEventType.Inserting)
            {
                if (user["ServiceID"].Length > 0) edt.Fields.Add("ServiceID", user["ServiceID"]);
            }
            string type = eParameters.Form("f9");
            if (type == "2") return;
            string formtable = eParameters.Form("formtable");
            string tablename = eParameters.Form("f2");           
            string sql = "";
            if (e.eventType == eFormTableEventType.Inserted || e.eventType == eFormTableEventType.Updated)
            {
                sql="update a set a.submodel=(case when isnull(b.type,2)=1 then 1 else 0 end) ";
                sql +=" FROM a_eke_sysModels a ";
                sql +=" left join a_eke_sysModels b on a.ParentID=b.ModelID ";
                sql += " where a.ModelID='" + e.ID + "'";
                eOleDB.Execute(sql);
            }
            #region 添加OK
            if (e.eventType == eFormTableEventType.Inserted)
            {

                #region 新建表

                if (formtable.Length == 0)
                {
                    sql = "Create table [" + tablename + "] (";
                    //sql += "[ID] [int] IDENTITY (1, 1) NOT NULL,";
                    sql += "[ID] [uniqueidentifier] NOT NULL Default (newid()),";
                    sql += "[addTime] [datetime] NULL default getdate(),";
                    sql += "[addUser] nvarchar(50) NULL,";
                    sql += "[editTime] [datetime] NULL,";
                    sql += "[editUser] nvarchar(50) NULL ,";
                    sql += "[delTime] [datetime] NULL,";
                    sql += "[delUser] nvarchar(50) NULL,";
                    sql += "[delTag] [bit] NULL default 0,";
                    sql += "PRIMARY KEY(ID)";
                    sql += ") ON [PRIMARY]";
                    eOleDB.Execute(sql);

                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'" + eParameters.Form("f1") + "','user','dbo','table','" + tablename + "',NULL,NULL");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'编号','user','dbo','table','" + tablename + "','column','ID'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'添加时间','user','dbo','table','" + tablename + "','column','addTime'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'添加用户','user','dbo','table','" + tablename + "','column','addUser'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'修改时间','user','dbo','table','" + tablename + "','column','editTime'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'修改用户','user','dbo','table','" + tablename + "','column','editUser'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'删除时间','user','dbo','table','" + tablename + "','column','delTime'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'删除用户','user','dbo','table','" + tablename + "','column','delUser'");
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'删除标记','user','dbo','table','" + tablename + "','column','delTag'");
                }
                #endregion
                if (f5.Value.ToString() == "True") //自动模块
                {
                    #region 添加模块
                    string ObjectID = eOleDB.getValue("SELECT id from sysobjects where name='" + tablename + "' and xtype='U'");

                    #region 物理数据列
                    sql = "SELECT a.name as code,b.[name] as type,a.length,d.text as [default],e.value as MC,a.colid as PX from syscolumns a";
                    sql += " inner join systypes b on a.xtype=b.xusertype ";
                    sql += " left join sysobjects c on a.cdefault=c.id and a.cdefault>0";
                    sql += " left join syscomments d on c.id=d.id";
                    sql += " left join sys.extended_properties e on e.major_id=a.id and e.minor_id=a.colid";
                    sql += " where a.id='" + ObjectID + "'";
                    sql += " order by a.colorder";
                    DataTable tb = eOleDB.getDataTable(sql);
                    if (tb.Rows.Count == 0)
                    {
                        sql = "SELECT a.name as code,b.[name] as type,a.length,d.text as [default],e.value as MC,a.colid as PX from syscolumns a";
                        sql += " inner join systypes b on a.xtype=b.xusertype ";
                        sql += " left join sysobjects c on a.cdefault=c.id and a.cdefault>0";
                        sql += " left join syscomments d on c.id=d.id";
                        sql += " left join sysproperties e on a.id=e.id and a.colid=e.smallid";
                        sql += " where a.id='" + ObjectID + "'";
                        sql += " order by a.colorder";

                        tb = eOleDB.getDataTable(sql);
                    }
                    #endregion

                    //eBase.PrintDataTable(tb);
                    //eBase.End();
                    string zj = eOleDB.getPrimaryKey(tablename);
                    string syscolumns = eConfig.getAllSysColumns() + "," + zj.ToLower() + ",";
                    int Num = 1;
                    //eBase.Writeln(zj);
                    //eBase.Writeln(syscolumns);
                    //eBase.Writeln(e.ID + "OK");
                    ///eBase.End();
                    #region 序号列
                    string formName = "M" + e.ID.Substring(0, 2) + "_" + "F" + Num.ToString();
                    sql = "insert into a_eke_sysModelItems (frmName,frmID,Num,ListOrder,ModelID,MC,ListHTML,Custom,showList,mShowList,ListWidth,mListWidth,Move,Size) ";
                    sql += " values ('" + formName + "','" + formName + "','" + Num.ToString() + "','" + Num.ToString() + "','" + e.ID + "','序号','{row:index}','1','" + (f7.Value.ToString().Length > 0 ? "0" : "1") + "','1','60','60','1','1')";
                    eOleDB.Execute(sql);
                    #endregion
                    Num++;
                    #region 其他列
                    // eBase.Writeln(tb.Rows.Count.ToString());
                    foreach (DataRow dr in tb.Rows)
                    {
                        string sys = (syscolumns.IndexOf("," + dr["code"].ToString().ToLower() + ",") > -1 ? "1" : "0");
                        string showedit = (sys == "0" ? "1" : "0");
                        string showlist = (sys == "0" ? "1" : "0");

                        formName = "M" + e.ID.Substring(0, 2) + "_" + "F" + Num.ToString();
                        eTable etb = new eTable("a_eke_sysModelItems");



                        etb.Fields.Add("ModelID", e.ID);
                        etb.Fields.Add("Num", Num.ToString());
                        etb.Fields.Add("MC", dr["mc"].ToString());
                        etb.Fields.Add("Code", dr["code"].ToString());
                        etb.Fields.Add("Type", dr["Type"].ToString());
                        etb.Fields.Add("Length", dr["Length"].ToString());
                        etb.Fields.Add("Sys", sys);
                        etb.Fields.Add("PX", dr["PX"].ToString());
                        etb.Fields.Add("primaryKey", (zj.ToLower() == dr["code"].ToString().ToLower() ? "1" : "0"));

                        if (dr["code"].ToString().ToLower() == "addtime")
                        {
                            showlist = "1";
                        }

                        etb.Fields.Add("ShowList", showlist);
                        etb.Fields.Add("ShowView", showedit);
                        etb.Fields.Add("ShowAdd", showedit);
                        etb.Fields.Add("ShowEdit", showedit);

                        if (f7.Value.ToString().Length == 0) //主模块
                        {
                            etb.Fields.Add("OrderBy", showlist);
                            etb.Fields.Add("Move", showlist);
                            etb.Fields.Add("Size", showlist);
                        }

                        if (dr["type"].ToString().ToLower().IndexOf("char") > -1)
                        {
                            etb.Fields.Add("maxLength", dr["length"].ToString());
                            etb.Fields.Add("Width", "300");
                        }
                        if (dr["type"].ToString().ToLower().IndexOf("date") > -1)
                        {

                            etb.Fields.Add("formatstring", (dr["type"].ToString().ToLower().IndexOf("datetime") > -1 ? "{0:yyyy-MM-dd HH:mm:ss}" : "{0:yyyy-MM-dd}"));
                        }
                        if (dr["type"].ToString().ToLower() == "bit")
                        {
                            if (sys == "0") etb.Fields.Add("defaultvalue", "True");
                            etb.Fields.Add("ControlType", "radio");
                            etb.Fields.Add("addControlType", "radio");
                            etb.Fields.Add("editControlType", "radio");
                            //etb.Fields.Add("ReplaceString", "[{text:是,value:True},{text:否,value:False}]");
                            etb.Fields.Add("Options", "[{text:是,value:True},{text:否,value:False}]");


                        }
                        if (dr["type"].ToString().ToLower() == "text")
                        {
                            etb.Fields.Add("ControlType", "html");
                            etb.Fields.Add("addControlType", "html");
                            etb.Fields.Add("editControlType", "html");
                        }
                        if (dr["code"].ToString().ToLower() == "show")
                        {

                            sql = "insert into a_eke_sysActions (ModelID,MC,Action,SQL) values ('" + e.ID + "','是否显示','show','update " + tablename + " set show=''{querystring:value}'' where " + zj + "=''{querystring:id}''')";
                            eOleDB.Execute(sql);
                            etb.Fields.Add("ListHTML", "<a href=\"?act=show&modelid={querystring:modelid}&id={data:id}&value={data:showvalue}\"><img src=\"{base:virtualpath}{data:ShowPIC}\" border=\"0\"></a>");

                            eOleDB.Execute("update a_eke_sysModels set ListFields='CASE WHEN Show=1 THEN ''images/sw_true.gif'' ELSE ''images/sw_false.gif'' END as ShowPIC,CASE WHEN Show=1 THEN ''0'' ELSE ''1'' END as ShowValue' where ModelID='" + e.ID + "'"); //,CASE WHEN ZD=1 THEN ''<img src=\"images/sw_true.gif\" border=\"0\">'' ELSE ''<img src=\"images/sw_false.gif\" border=\"0\">'' END as ZDPIC,CASE WHEN ZD=1 THEN ''0'' ELSE ''1'' END as ZDValue


                            //eOleDB.Execute("insert into a_eke_sysModelConditions (ModelID,MC,ControlType) values ('" + e.ID + "','是否显示','radio')");
                            //string condid = eOleDB.ID;

                            //eOleDB.Execute("insert into a_eke_sysModelConditionItems (ModelID,ModelConditionID,MC,ConditionValue) values ('" + e.ID + "','" + condid + "','是','show=1')");
                            //eOleDB.Execute("insert into a_eke_sysModelConditionItems (ModelID,ModelConditionID,MC,ConditionValue) values ('" + e.ID + "','" + condid + "','否','show=0')");

                            string MaxConds = eOleDB.getValue("select count(*)+1 from a_eke_sysModelConditions where ModelID='" + e.ID + "'");
                            eOleDB.Execute("insert into a_eke_sysModelConditions (ModelID,MC,ControlType,Code,Operator,Options,Num) values ('" + e.ID + "','是否显示','radio','show','=','[{text:是,value:1},{text:否,value:0}]','" + MaxConds + "')");
                            eOleDB.Execute("update a_eke_sysModels set MaxConds='" + MaxConds + "' where ModelID='" + e.ID + "'");
                        }
                        if (dr["code"].ToString().ToLower() == "deltag")
                        {
                            //etb.Fields.Add("Condition", "=");
                            //etb.Fields.Add("ConditionValue", "0");
                        }
                        if (dr["code"].ToString().ToLower() == "addtime")
                        {
                            //etb.Fields.Add("defaultOrder", "2");
                            string MaxConds = eOleDB.getValue("select count(*)+1 from a_eke_sysModelConditions where ModelID='" + e.ID + "'");
                            eOleDB.Execute("insert into a_eke_sysModelConditions (ModelID,MC,ControlType,Code,Operator,DateFormat,Width,Num) values ('" + e.ID + "','添加时间','date','addTime','>=','yyyy-MM-dd','150','" + MaxConds + "')");
                            eOleDB.Execute("insert into a_eke_sysModelConditions (ModelID,MC,ControlType,Code,Operator,DateFormat,Width,Num) values ('" + e.ID + "','添加时间','date','addTime','<=','yyyy-MM-dd','150'," + MaxConds + " + 1)");
                            eOleDB.Execute("update a_eke_sysModels set MaxConds=" + MaxConds + "+1 where ModelID='" + e.ID + "'");
                        }
                        etb.Fields.Add("frmName", formName);
                        etb.Fields.Add("frmID", formName);
                        etb.Fields.Add("notnull", showedit);
                        etb.Fields.Add("ListOrder", Num.ToString());
                        etb.Add();


                        Num++;
                    }
                    #endregion
                    #region 操作列
                    formName = "M" + e.ID.Substring(0, 2) + "_" + "F" + Num.ToString();
                    sql = "insert into a_eke_sysModelItems (frmName,frmID,Num,ListOrder,ModelID,MC,ListHTML,Custom,showList,mShowList,ListWidth,mListWidth,Move,Size) ";
                    sql += " values ('" + formName + "','" + formName + "','" + Num.ToString() + "','" + Num.ToString() + "','" + e.ID + "','操作','<a href=\"{base:url}act=view&id={data:ID}\">查看</a><a href=\"{base:url}act=edit&id={data:ID}\">修改</a><a href=\"{base:url}act=del&id={data:ID}\" onclick=\"javascript:return confirm(''确认要删除吗？'');\">删除</a>','1','" + (f7.Value.ToString().Length > 0 ? "0" : "1") + "','1','130','130','1','1')";
                    eOleDB.Execute(sql);
                    #endregion
                    eOleDB.Execute("update a_eke_sysModels set MaxItems='" + Num.ToString() + "' where ModelID='" + e.ID + "'");
                    if (f7.Value.ToString().Length == 0) //主模块
                    {
                        eOleDB.Execute("update a_eke_sysModels set DefaultCondition='delTag=0',DefaultOrderby='addTime Desc' where ModelID='" + e.ID + "'");
                    }
                    else
                    {
                        eOleDB.Execute("update a_eke_sysModels set DefaultCondition='delTag=0',DefaultOrderby='addTime' where ModelID='" + e.ID + "'");
                    }


                    eOleDB.Execute("update a_eke_sysModels set Power='[{text:列表,value:list},{text:详细,value:view},{text:添加,value:add},{text:编辑,value:edit},{text:删除,value:del},{text:复制,value:copy},{text:打印,value:print},{text:导出,value:export}]' where ModelID='" + e.ID + "'");
                    #endregion
                }
                else
                {
                    eOleDB.Execute("update a_eke_sysModels set Power='[{text:列表,value:list},{text:详细,value:view},{text:添加,value:add},{text:编辑,value:edit},{text:删除,value:del},{text:复制,value:copy},{text:打印,value:print},{text:导出,value:export}]' where ModelID='" + e.ID + "'");
                }
            }
            #endregion
            #region 修改
            if (e.eventType == eFormTableEventType.Updating)
            {
                string oldName = eOleDB.getValue("select code from a_eke_sysModels where ModelID='" + e.ID + "'");
                if (oldName.ToLower() != tablename.ToLower())
                {
                    eOleDB.Execute("exec sp_rename  '" + oldName + "' ,'" + tablename + "'");
                }
            }
            #endregion
        }
        private void List()
        {
            elist = new eList("a_eke_sysModels");
            elist.Fields.Add("*");
            elist.Where.Add("delTag=0 and type=1 and subModel=0");
            elist.Where.Add("ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'"));
            elist.OrderBy.Add("addTime desc");
            elist.Bind(Rep, ePageControl1);
        }
        //来源ModelItems
        private string getModelTree(string pid, string node,string value)
        {
            string sql = "Select ModelID,MC from a_eke_sysModels where delTag=0 and Auto=1 ";
            sql += " and ParentID " + (pid.Length > 0 ? "='" + pid + "'" : "is NULL");
            sql += " order by addTime";

            DataTable dt = eOleDB.getDataTable(sql);
            if (dt.Rows.Count == 0) return "";
            string temp = "";
            if (pid.Length == 0) temp = "<option value=\"\">请选择</option>\r\n";
            foreach (DataRow dr in dt.Rows)
            {
                temp += "<option value=\"" + dr["ModelID"].ToString() + "\"" + (dr["ModelID"].ToString()==value ? " selected" : "") + ">" + node + dr["mc"].ToString() + "</option>\r\n";
                temp += getModelTree(dr["ModelID"].ToString(), node + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;",value);
            }
            return temp;

        }
        protected void Action_Actioning(string Actioning)
        {
            if (Actioning == "add" || Actioning == "edit")
            {
                StringBuilder sb = new StringBuilder();
                //DataTable tb = eOleDB.getDataTable("SELECT id,name FROM sysobjects where category=0 and xtype='U' order by crdate");
                string sql = "SELECT id,name FROM sysobjects where xtype='U' "; //name!='dtproperties' and 
                sql += " and (charindex('a_eke_sys',lower(name))=0 or lower(name)='a_eke_sysusers' or lower(name)='a_eke_syspowers' or lower(name)='a_eke_systokens'   )";
                sql+=" and name not in (" + eBase.getSystemTables() + ")";
                sql += " order by name";//crdate";
                DataTable tb = eOleDB.getDataTable(sql);
                for (int i = 0; i < tb.Rows.Count; i++)
                {

                    sb.Append("<option value=\"" + tb.Rows[i]["name"].ToString() + "\"" + (tb.Rows[i]["name"].ToString() == edt.Fields["code"].ToString() ? " selected" : "") + ">" + tb.Rows[i]["name"].ToString() + "</option>\r\n");
                }
                LitTable.Text = sb.ToString();


                LitParent.Text = getModelTree("", "",f7.Value.ToString());
            }
            #region 复制功能
            if (Actioning == "copy")
            {
                eMTable Model = new eMTable("a_eke_sysModels");
                Model.Where.Add("ModelID='" + Request.QueryString["ID"].ToString() + "'");


                eMTable sModel = new eMTable("a_eke_sysModels");
                Model.AddChild(sModel);

                eMTable sModel1 = new eMTable("a_eke_sysModels");
                sModel.AddChild(sModel1);

                eMTable sModel2 = new eMTable("a_eke_sysModels");
                sModel1.AddChild(sModel2);

                eMTable ModelTabs = new eMTable("a_eke_sysModelTabs");
                Model.AddChild(ModelTabs);
                sModel.AddChild(ModelTabs);
                sModel1.AddChild(ModelTabs);
                sModel2.AddChild(ModelTabs);


                eMTable ModelPanels = new eMTable("a_eke_sysModelPanels");
                Model.AddChild(ModelPanels);
                sModel.AddChild(ModelPanels);
                sModel1.AddChild(ModelPanels);
                sModel2.AddChild(ModelPanels);

                eMTable action = new eMTable("a_eke_sysActions");
                Model.AddChild(action);
                sModel.AddChild(action);
                sModel1.AddChild(action);
                sModel2.AddChild(action);

                string ct = eOleDB.getValue("select count(*) from a_eke_sysCheckUps where ModelID='" + Request.QueryString["ID"].ToString() + "'");
                if (ct.Length > 0 && ct != "0")
                {

                    eMTable CheckUps = new eMTable("a_eke_sysCheckUps");
                    eMTable CheckupRecords = new eMTable("a_eke_sysCheckupRecords");
                    CheckUps.AddChild(CheckupRecords);
                    Model.AddChild(CheckUps);
                }


                eMTable Conditions = new eMTable("a_eke_sysConditions");
                Model.AddChild(Conditions);
                sModel.AddChild(Conditions);
                sModel1.AddChild(Conditions);
                sModel2.AddChild(Conditions);

                eMTable Conds = new eMTable("a_eke_sysModelConditions");
                eMTable CondItems = new eMTable("a_eke_sysModelConditionItems");
                Conds.AddChild(CondItems);
                Model.AddChild(Conds);
                sModel.AddChild(Conds);
                sModel1.AddChild(Conds);
                sModel2.AddChild(Conds);


                eMTable Items = new eMTable("a_eke_sysModelItems");
                Model.AddChild(Items);
                sModel.AddChild(Items);
                sModel1.AddChild(Items);
                sModel2.AddChild(Items);

                   
              


                Model.Copy();
                eOleDB.Execute("update a_eke_sysModels set MC='复件-' + MC where ModelID='" + Model.ID + "'");
                //eBase.Writeln(Model.ID);
                //eBase.End();
               

                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    Response.Redirect(Request.ServerVariables["HTTP_REFERER"].ToString(), true);
                }
                else
                {
                    Response.Redirect("Models.aspx", true);
                }
            }
            #endregion
            switch (Actioning)
            {
                case "":
                    List();
                    break;
                case "add":
                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "模块管理 - " + eConfig.getString("manageName"); 
            }
        }
    }
}