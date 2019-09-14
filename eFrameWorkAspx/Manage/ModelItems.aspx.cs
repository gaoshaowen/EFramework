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
    public partial class ModelItems : System.Web.UI.Page
    {
        public string act = eParameters.QueryString("act");
        private string value = eParameters.QueryString("value").Replace("'", "''");
        private string item = eParameters.QueryString("item");
        #region 属性
        private string sql = "";
        private string _modelid = eParameters.QueryString("modelid");
        public string ModelID
        {
            get
            {
                return _modelid;
            }
        }
        private string _objectid = "";
        public string ObjectID
        {
            get
            {
                if (_objectid.Length == 0)
                {
                    _objectid = eOleDB.getValue("SELECT id from sysobjects where name='" + TableName + "'");
                }
                return _objectid;
            }
        }
        private string _parentid = "";
        public string ParentID
        {
            get
            {
                if (_parentid.Length == 0)
                {
                    string temp = eOleDB.getValue("select ParentID FROM a_eke_sysModels where ModelID='" + ModelID + "'");

                    if (temp.Length == 0 || temp == "0") //1级
                    {
                        _parentid = ModelID;
                    }
                    else
                    {
                        string type = eOleDB.getValue("select Type FROM a_eke_sysModels where ModelID='" + temp + "'");

                        if (type == "2")
                        {
                            _parentid = ModelID;
                            return _parentid;
                        }
                        _parentid = temp;
                        while (temp != "0" && temp.Length > 0 && type!="2")
                        {
                          

                            temp = eOleDB.getValue("select ParentID FROM a_eke_sysModels where ModelID='" + temp + "'");
                            type = eOleDB.getValue("select Type FROM a_eke_sysModels where ModelID='" + temp + "'");
                            if (temp != "0" && temp.Length > 0 && type != "2")
                            {
                                _parentid = temp;
                            }


                            if (type == "2")
                            {
                                //temp = "";
                            }
                            else
                            {
                               
                            }
                        }
                    }
                }
                return _parentid;
            }
        }
        private string _tablename = "";
        public string TableName
        {
            get
            {
                if (_tablename.Length == 0)
                {
                    _tablename = eOleDB.getValue("select code from a_eke_sysModels where ModelID='" + ModelID + "'");
                }
                return _tablename;
            }
        }


        private DataTable _columns;//所有列
        public DataTable Columns
        {
            get
            {
                if (_columns == null)
                {
                    sql = "SELECT a.name as code,b.[name] as type,a.length,d.text as [default],e.value as MC,a.colid as PX from syscolumns a";
                    sql += " inner join systypes b on a.xtype=b.xusertype ";
                    sql += " left join sysobjects c on a.cdefault=c.id and a.cdefault>0";
                    sql += " left join syscomments d on c.id=d.id";
                    sql += " left join sys.extended_properties e on e.major_id=a.id and e.minor_id=a.colid";
                    sql += " where a.id='" + ObjectID + "'";
                    sql += " order by a.colorder";
                    _columns = eOleDB.getDataTable(sql);
                    if (_columns.Rows.Count == 0)
                    {
                        sql = "SELECT a.name as code,b.[name] as type,a.length,d.text as [default],e.value as MC,a.colid as PX from syscolumns a";
                        sql += " inner join systypes b on a.xtype=b.xusertype ";
                        sql += " left join sysobjects c on a.cdefault=c.id and a.cdefault>0";
                        sql += " left join syscomments d on c.id=d.id";
                        sql += " left join sysproperties e on a.colid=e.smallid";
                        sql += " where a.id='" + ObjectID + "'";
                        sql += " order by a.colorder";

                        _columns = eOleDB.getDataTable(sql);
                    }

                }
                return _columns;
            }
        }
        #endregion
        public string modelType = "0";
        public string printHTMLStart = "";
        public string printHTML = "";
        public string printHTMLEnd= "";
        public string linkArrys = "";
        private string getModelTree(string pid, string node)
        {
            DataTable dt = eOleDB.getDataTable("select * from a_eke_sysModels where ModelID='" + pid + "'");
            if (dt.Rows.Count == 0) return "";
            string temp = "<a href=\"ModelItems.aspx?ModelID=" + pid + "\" style=\"font-size:12px;" + (ModelID == pid ? "color:#ff0000;" : "color:#333333;") + "\">";
            //temp += eOleDB.getValue("select mc from a_eke_sysModels where ModelID='" + pid + "'") + "</a>&nbsp;&nbsp;";
            temp += dt.Rows[0]["mc"].ToString() + "</a>&nbsp;&nbsp;&nbsp;<font color=\"#666666\">";
            switch (dt.Rows[0]["Type"].ToString())
            {
                case "1"://模块
                    if (dt.Rows[0]["Auto"].ToString() == "False")
                    {
                        temp += "[自定义模块]";
                    }
                    else
                    {
                        if (dt.Rows[0]["subModel"].ToString() == "True")
                        {
                            temp += "[子模块 " + (dt.Rows[0]["JoinMore"].ToString() == "True" ? "1VN" : "1v1") + "]";
                        }
                        else
                        {
                            temp += "[主模块]";
                        }
                    }
                    break;
                case "2"://菜单
                    temp += "[菜单]";
                    break;
                case "3": //数据模块
                    temp += "[数据模块]";
                    break;
            }


            temp += "</font>&nbsp;&nbsp;&nbsp;<a href=\"Models.aspx?act=edit&ID=" + pid + "\" target=\"_blank\">编辑</a><br>";
            if (node.Length > 0) temp = node + "<img src=\"images/left_ico.jpg\" width=\"11\" height=\"11\" align=\"absmiddle\">&nbsp;" + temp;


            DataTable tb = eOleDB.getDataTable("select ModelID,MC from a_eke_sysModels where ParentID='" + pid + "' and (Type=1 or Type=3) and delTag='0' order by JoinMore");
            if (tb.Rows.Count == 0)
            {
                return temp;
            }
            else
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    temp += getModelTree(tb.Rows[i]["ModelID"].ToString(), node + "&nbsp;&nbsp;");
                }
                return temp;
            }

        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            LitMenu.Text = getModelTree(ParentID, "");
            modelType = eOleDB.getValue("select Type from a_eke_sysModels where ModelID='" + ModelID + "'");
            printHTMLStart = eOleDB.getValue("select printHTMLStart from a_eke_sysModels where ModelID='" + ModelID + "'");
            printHTML = eOleDB.getValue("select PrintHTML from a_eke_sysModels where ModelID='" + ModelID + "'");
            printHTMLEnd = eOleDB.getValue("select printHTMLEnd from a_eke_sysModels where ModelID='" + ModelID + "'");
            //DataTable items = eOleDB.getDataTable("select ModelItemID,MC,Code,CustomCode from a_eke_sysModelItems where delTag=0 and ModelID='" + ModelID + "' order by px");
            eTable tb = new eTable(TableName);
            string sql = "select ModelItemID,MC,Code,CustomCode from a_eke_sysModelItems ";
            sql += " where delTag=0 and ModelID='" + ModelID + "' ";
            sql += " and isnull(Code,'') not in ('delTime','delUser','delTag') and MC not in ('序号','操作') ";
            sql += " union ";
            sql += " select b.ModelItemID,b.MC,b.Code,b.CustomCode from a_eke_sysModels a ";
            sql += " inner join a_eke_sysModelItems b on a.ModelID=b.ModelID ";
            sql += " where a.JoinMore=0 and b.delTag=0 and a.ParentID ='" + ModelID + "' ";
            sql += "and isnull( b.Code,'') not in ('" + tb.primaryKey + "','addTime','addUser','editTime','editUser','delTime','delUser','delTag') ";
            sql += " and  b.MC not in ('序号','操作') and b.primaryKey=0";

            DataTable items = eOleDB.getDataTable(sql);
            for (int i = 0; i < items.Rows.Count; i++)
            {
              
                string code = items.Rows[i]["Code"].ToString();
                if (code.Length == 0) code = items.Rows[i]["CustomCode"].ToString();
                if (code.Length > 0)
                {
                    if (linkArrys.Length > 0) linkArrys += ",";
                    linkArrys += "'data," + code + "," + items.Rows[i]["MC"].ToString() + " (" + code + ")" + "'";
                }
            }

            items = eOleDB.getDataTable("SELECT ModelID,MC FROM a_eke_sysModels where ParentID='" + ModelID + "' and delTag=0 and JoinMore=1 and Auto=1");
            foreach (DataRow dr in items.Rows)
            {
                if (linkArrys.Length > 0) linkArrys += ",";
                linkArrys += "'model," + dr["ModelID"].ToString().ToLower() + ",模块：" + dr["MC"].ToString() + "'";
            }
            /*
             print OBJECT_ID('eWeb_Columns') print OBJECT_ID('sys.extended_properties') select * from INFORMATION_SCHEMA.TABLES
             */
            eUser user = new eUser("Manage");
            if (act.Length > 0)
            {
                //清除所有缓存
                eBase.clearDataCache();
                if (value.Length == 0)
                {
                    value = eParameters.Form("value");
                    value = eBase.decode(value);
                    value = value.Replace("'", "''");
                }

                #region 复制还原表单编码
                if (act == "copycode")
                {
                    sql = "update a_eke_sysModelItems set frmName=Code,frmID=Code where ModelID='" + ModelID + "' and LEN(code)>0";
                    eOleDB.Execute(sql);

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "操作成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }
                if (act == "restorecode")
                {
                    sql = "update a_eke_sysModelItems set frmName='M" + ModelID.ToLower().Substring(0, 2) + "_F' + cast(Num as varchar(5)),frmID='M" + ModelID.ToLower().Substring(0, 2) + "_F' + cast(Num as varchar(5)) where ModelID='" + ModelID + "' and LEN(code)>0";
                    eOleDB.Execute(sql);

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "操作成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }                
                #endregion
                #region 数据结构
                #region 移动列顺序

                if (act == "movecolumn")
                {
                    eJson json = new eJson();
                    string tableName = eOleDB.getValue("select Code from a_eke_sysModels where ModelID='" + ModelID + "'");
                    if (tableName.Length == 0)
                    {
                        json.Add("success", "0");
                        json.Add("message", "移动失败!");
                        Response.Write(json.ToString());
                        Response.End();
                    }

                    DataTable Columns = eOleDB.getColumns(tableName);
                    int index = Convert.ToInt32(eParameters.QueryString("index")) -1 ;
                    int nindex = Convert.ToInt32(eParameters.QueryString("nindex")) - 1;
                    Columns = eOleDB.moveColumn(Columns, index, nindex);


                    sql = eOleDB.getTableSql(Columns, tableName);                   
                    eOleDB.Execute(sql);

                    json.Add("success", "1");
                    json.Add("message", "移动成功!");
                    Response.Write(json.ToString());
                    Response.End();

                   

                }
                #endregion
                #region 选择列
                if (act == "selcolumn")
                {

                    if (value == "1") //添加
                    {
                        DataRow[] dr = Columns.Select("Code='" + eParameters.QueryString("code") + "'");
                        if (dr.Length > 0)
                        {
                            sql = "select count(*) from a_eke_sysModelItems Where ModelID='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "'";
                            string ct = eOleDB.getValue(sql);
                            string primaryKey = "0";
                            string zj = eOleDB.getPrimaryKey(TableName);
                            if (zj.ToLower() == eParameters.QueryString("code").ToLower()) primaryKey = "1";
                            string syscolumns = eConfig.getAllSysColumns() + "," + zj.ToLower() + ",";
                            string sys = (syscolumns.IndexOf("," + dr[0]["code"].ToString().ToLower() + ",") > -1 ? "1" : "0");
                            if (ct == "0")//添加
                            {

                                eOleDB.Execute("update a_eke_sysModels set MaxItems=MaxItems+1 where ModelID='" + ModelID + "'");
                                string MaxItems = eOleDB.getValue("select MaxItems from a_eke_sysModels where ModelID='" + ModelID + "'");

                                string _itemid = Guid.NewGuid().ToString();
                                sql = "insert into a_eke_sysModelItems (ModelItemID,Num,listOrder,ModelID,MC,Code,primaryKey,Type,Length,sys,PX) ";
                                sql += " values ('" + _itemid + "','" + MaxItems + "','" + MaxItems + "','" + ModelID + "','" + dr[0]["mc"].ToString() + "','" + dr[0]["code"].ToString() + "','" + primaryKey + "','" + dr[0]["type"].ToString() + "','" + dr[0]["length"].ToString() + "','" + sys + "','" + dr[0]["PX"].ToString() + "')";
                                eOleDB.Execute(sql);
    
                                #region 设置默认值



                                string formName = "M" + ModelID.Substring(0, 2) + "_" + "F";
                                sql = "update a_eke_sysModelItems set sys='" + sys + "'";
                                sql += ",frmName='" + formName + "' + cast(Num as varchar(5))";
                                sql += ",frmID='" + formName + "' + cast(Num as varchar(5))";

                                //sql += ",frmName='M'+ cast(ModelID as varchar(50)) +'_F'+cast(Num as varchar(5))";
                                //sql += ",frmID='M'+  cast(ModelID as varchar(50)) +'_F'+cast(Num as varchar(5))";

                                string submodel = eOleDB.getValue("select subModel from a_eke_sysModels where ModelID='" + ModelID + "'");

                                if (sys == "0") //非系统列
                                {
                                    sql += ",ControlType='text'";
                                    sql += ",addControlType='text'";
                                    sql += ",editControlType='text'";
                                    sql += ",showList='1'";
                                    sql += ",showView='1'";
                                    sql += ",showAdd='1'";
                                    sql += ",showEdit='1'";

                                    if (submodel == "False")
                                    {
                                        sql += ",OrderBy='1'";
                                        sql += ",Move='1'";
                                        sql += ",Size='1'";
                                    }
                                }
                                else
                                {
                                    sql += ",editControlType='text'";
                                }
                                sql += " where ModelItemID ='" + _itemid + "'";

                                eOleDB.Execute(sql);
                                #endregion
                            }
                            else //修改
                            {
                                sql = "update a_eke_sysModelItems set delTag=0,MC='" + dr[0]["mc"].ToString() + "',Type='" + dr[0]["type"].ToString() + "',Length='" + dr[0]["length"].ToString() + "',PX='" + dr[0]["PX"].ToString() + "',SYS='" + sys + "' where ModelID='" + ModelID + "' and Code='" + dr[0]["code"].ToString() + "'";
                                eOleDB.Execute(sql);
                            }
                        }
                    }
                    else//删除
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set delTag=1 Where ModelID='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "'");

                    }

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "选择成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 添加列
                if (act == "addcolumn")
                {
                    string code = "F" + (Columns.Rows.Count + 1).ToString();
                    sql = "alter table [" + TableName + "] add [" + code + "] nvarchar(50)";
                    eOleDB.Execute(sql);

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "添加成功"); // + ModelID + "::" + ObjectID + "::" + TableName + ":::" + Columns.Rows.Count.ToString()
                    //Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 删除列
                if (act == "delcolumn")
                {
                    sql = "select c.name from sysconstraints a ";
                    sql += "inner join syscolumns b on a.id=b.id and a.colid=b.colid ";
                    sql += "inner join sysobjects c on a.constid=c.id ";
                    sql += "where a.id=object_id('" + TableName + "') ";
                    sql += "and b.name='" + eParameters.QueryString("code") + "'";
                    string constraint = eOleDB.getValue(sql);
                    if (constraint.Length > 0) eOleDB.Execute("alter table [" + TableName + "] drop constraint " + constraint);



                    sql = "alter table [" + TableName + "] drop column [" + eParameters.QueryString("code") + "]";
                    eOleDB.Execute(sql);

                    sql = "delete from a_eke_sysModelItems where ModelID='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "'";
                    eOleDB.Execute(sql);

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "删除成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 重命名列
                if (act == "renamecolumn")
                {

                    sql = "EXEC sp_rename '" + TableName + "." + eParameters.QueryString("code") + "','" + eParameters.QueryString("newcode") + "','COLUMN'";
                    eOleDB.Execute(sql);

                    sql = "update a_eke_sysModelItems set code='" + eParameters.QueryString("newcode") + "' where ModelId='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "'";
                    eOleDB.Execute(sql);

                    eOleDB.removePrimaryKeys(); //清除主键缓存


                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "重命名成功!");
                    Response.Write(json.ToString()); 
                    Response.End();
                }
                #endregion
                #region 列说明
                if (act == "columnname")
                {
                    //SQL2008 能识别 sys.sp_addextendedproperty 及 sys.sp_updateextendedproperty SQL2000不能识别sys. 其他一样
                    eOleDB.Execute("EXEC sp_addextendedproperty N'MS_Description',N'" + value + "','user','dbo','table','" + TableName + "','column','" + eParameters.QueryString("code") + "'");
                    eOleDB.Execute("EXEC sp_updateextendedproperty N'MS_Description',N'" + value + "','user','dbo','table','" + TableName + "','column','" + eParameters.QueryString("code") + "'");

                    sql = "update a_eke_sysModelItems set MC='" + value + "' where ModelId='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "' and len(isnull(mc,''))=0";
                    eOleDB.Execute(sql);

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "列说明设置成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 默认值
                if (act == "columndefault")
                {
                    sql = "select c.name from sysconstraints a ";
                    sql += "inner join syscolumns b on a.id=b.id and a.colid=b.colid ";
                    sql += "inner join sysobjects c on a.constid=c.id ";
                    sql += "where a.id=object_id('" + TableName + "') ";
                    sql += "and b.name='" + eParameters.QueryString("code") + "'";
                    string constraint = eOleDB.getValue(sql);
                    if (constraint.Length > 0) eOleDB.Execute("alter table [" + TableName + "] drop constraint " + constraint);
                    value = value.Replace("''", "'");
                    eOleDB.Execute("alter table [" + TableName + "] add default " + value + " for " + eParameters.QueryString("code") + "");// with values


                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "默认值设置成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 数据类型
                if (act == "columntype")
                {
                   


                    #region 默认值
                    sql = "select c.name from sysconstraints a ";
                    sql += "inner join syscolumns b on a.id=b.id and a.colid=b.colid ";
                    sql += "inner join sysobjects c on a.constid=c.id ";
                    sql += "where a.id=object_id('" + TableName + "') ";
                    sql += "and b.name='" + eParameters.QueryString("code") + "'";
                    string constraint = eOleDB.getValue(sql);
                    if (constraint.Length > 0) eOleDB.Execute("alter table [" + TableName + "] drop constraint " + constraint);
                    #endregion

                    eOleDB.removePrimaryKeys();

                    string zj = eOleDB.getPrimaryKey(TableName);
                    bool pk = zj.ToLower() == eParameters.QueryString("code").ToLower() ? true : false;
                    if (pk)
                    {
                        #region 取消主键
                        sql = "select a.name from sysobjects a  ";
                        sql += "inner join syscolumns b on a.parent_obj=b.id ";
                        sql += "where a.parent_obj=object_id('" + TableName + "') and a.xtype='PK' and b.name='" + eParameters.QueryString("code") + "'";
                        constraint = eOleDB.getValue(sql);
                        if (constraint.Length > 0) eOleDB.Execute("alter table [" + TableName + "] drop constraint " + constraint);
                        #endregion

                        #region 重命名 或 删除
                        //sql = "EXEC sp_rename '" + TableName + "." + eParameters.QueryString("code") + "','" + eParameters.QueryString("code") + "OLD','COLUMN'";
                        //eOleDB.Execute(sql);

                        sql = "alter table [" + TableName + "] drop column [" + eParameters.QueryString("code") + "]";
                        eOleDB.Execute(sql);
                        #endregion


                        if (value.ToLower() == "int")
                        {
                            sql = "alter table [" + TableName + "] add  [" + eParameters.QueryString("code") + "] int NOT NULL IDENTITY (1, 1)";
                            eOleDB.Execute(sql);

                            #region 添加主键
                            sql = "alter table [" + TableName + "] add primary key (" + eParameters.QueryString("code") + ")";
                            eOleDB.Execute(sql);
                            #endregion
                        }
                        else
                        {
                            sql = "alter table [" + TableName + "] add  [" + eParameters.QueryString("code") + "] ";
                            if (value.IndexOf("varchar") > -1)
                            {
                                sql += value + " (50)";
                            }
                            else
                            {
                                sql += value;
                            }
                            sql += " not null";
                            if (value.ToLower() == "uniqueidentifier") sql += " default (newid())";
                            eOleDB.Execute(sql);
                        }

                        

                        DataTable Columns = eOleDB.getColumns(TableName);
                        int index = Columns.Rows.Count - 1;
                        int nindex = 0;


                        Columns = eOleDB.moveColumn(Columns, index, nindex);


                        sql = eOleDB.getTableSql(Columns, TableName);                    
                        eOleDB.Execute(sql);
                        #region 添加主键
                        if (value.ToLower() == "uniqueidentifier")
                        {
                            sql = "alter table [" + TableName + "] add primary key (" + eParameters.QueryString("code") + ")";
                            eOleDB.Execute(sql);
                        }
                        #endregion
                    }
                    else
                    {
                        #region 非主键
                        sql = "alter table [" + TableName + "] alter column  [" + eParameters.QueryString("code") + "] ";
                        if (value.IndexOf("varchar") > -1)
                        {
                            sql += value + " (50)";
                        }
                        else
                        {
                            sql += value;
                        }
                        eOleDB.Execute(sql);
                        #endregion
                    }

                    /*
                    sql = "alter table [" + TableName + "] alter column  [" + eParameters.QueryString("code") + "] ";
                    if (value == "varchar")
                    {
                        sql += value + " (50)";
                    }
                    else
                    {
                        sql += value;
                    }
                    if (pk) sql += " not null";
                    eOleDB.Execute(sql);
                    */


                    DataTable dt = eOleDB.getColumns(TableName);
                    DataRow[] rows = dt.Select("Code='" + eParameters.QueryString("code") + "'");
                    if (rows.Length > 0)
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set Type='" + rows[0]["Type"].ToString() + "',Length='" + rows[0]["Length"].ToString() + "' where ModelId='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "'");
                    }

                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "列说明设置成功!");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 列长度
                if (act == "columnlength")
                {

                    sql = "alter table [" + TableName + "] alter column  [" + eParameters.QueryString("code") + "] nvarchar (" + value + ")";
                    eOleDB.Execute(sql);
                    sql = "update a_eke_sysModelItems set Length='" + value + "' where ModelId='" + ModelID + "' and Code='" + eParameters.QueryString("code") + "'";
                    eOleDB.Execute(sql);
                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "列长度设置成功!" );
                    Response.Write(json.ToString());
                    Response.End();
                }

                #endregion
                #endregion

                #region 模块
                string modelitemid = eParameters.QueryString("modelitemid");
                if (act == "setmodel")
                {

                    if (item.ToLower() == "code")
                    {
                        string oldName = eOleDB.getValue("select code from a_eke_sysModels where ModelID='" + ModelID + "'");
                        eOleDB.Execute("exec sp_rename  '" + oldName + "' ,'" + value + "'");
                    }
                    if (item.ToLower() == "modelcondition")
                    {
                        sql = "if exists (select * from a_eke_sysConditions Where ModelID='" + ModelID + "' and RoleID is null and UserID is null)";
                        sql += "update a_eke_sysConditions set CondValue='" + value + "' where ModelID='" + ModelID + "' and RoleID is null and UserID is null";
                        sql += " else ";
                        sql += "insert into a_eke_sysConditions (ModelID,CondValue) ";
                        sql += " values ('" + ModelID + "','" + value + "')";
                        eOleDB.Execute(sql);
                    }
                    else
                    {
                        if (value == "NULL")
                        {
                            eOleDB.Execute("update a_eke_sysModels set " + item + "=" + value + " where ModelID='" + ModelID + "'");
                        }
                        else
                        {
                            eOleDB.Execute("update a_eke_sysModels set " + item + "='" + value + "' where ModelID='" + ModelID + "'");
                        }
                        if (item.ToLower() == "modeltabid")
                        {
                            eOleDB.Execute("update a_eke_sysModels set ModelPanelID=NULL where ModelID='" + ModelID + "'");
                        }
                    }
                    if (item.ToLower() == "addcolumncount")
                    {
                        eOleDB.Execute("update a_eke_sysModels set editcolumncount='" + value + "',viewcolumncount='" + value + "' where ModelID='" + ModelID + "'");
                    }
                    
                    Response.End();
                }
                #endregion
                #region 列
                if (act == "addmodelitem")
                {
                    eOleDB.Execute("update a_eke_sysModels set MaxItems=MaxItems+1 where ModelID='" + ModelID + "'");
                    string MaxItems = eOleDB.getValue("select MaxItems from a_eke_sysModels where ModelID='" + ModelID + "'");
                   // string clientprefix = eOleDB.getValue("select ClientPrefix from a_eke_sysModels where ModelID='" + ModelID + "'");
                    string frmName = "M" + ModelID.Substring(0, 2) + "_" + "F" + MaxItems.ToString(); ;
                    eOleDB.Execute("insert into a_eke_sysModelItems (NUM,listOrder,FrmName,FrmID,ModelID,Custom) values ('" + MaxItems + "','" + MaxItems + "','" + frmName + "','" + frmName + "','" + ModelID + "','1')");
                    Response.End();

                }
                if (act == "delmodelitem")
                {
                    eOleDB.Execute("delete from a_eke_sysModelItems where ModelItemID='" + modelitemid + "'");
                    Response.End();
                }
                if (act == "setmodelitem")
                {
                   
                    //拖动排序
                    if (item.ToLower() == "setorders")
                    {
                        string ids=eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysModelItems set AddOrder='" + value + "',EditOrder='" + value + "',ViewOrder='" + value + "' where ModelID='" + ModelID + "' and ModelItemID='" + arr[i] + "'");
                        }
                        eOleDB.Execute("update a_eke_sysModelItems set AddOrder='999999',EditOrder='999999',ViewOrder='999999' where ModelID='" + ModelID + "' and ModelItemID not in ('" + ids.Replace(",", "','") + "')");
                        Response.End();
                    }
                    //拖动排序-列表
                    if (item.ToLower() == "setlistorders")
                    {
                        string ids = eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysModelItems set ListOrder='" + value + "' where ModelID='" + ModelID + "' and ModelItemID='" + arr[i] + "'");
                        }
                        eOleDB.Execute("update a_eke_sysModelItems set ListOrder='999999' where ModelID='" + ModelID + "' and ModelItemID not in ('" + ids.Replace(",", "','") + "')");
                        Response.End();
                    }
                    //拖动排序-导出
                    if (item.ToLower() == "setexportorders")
                    {
                        string ids = eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysModelItems set ExportOrder='" + value + "' where ModelID='" + ModelID + "' and ModelItemID='" + arr[i] + "'");
                        }
                        eOleDB.Execute("update a_eke_sysModelItems set ExportOrder='999999' where ModelID='" + ModelID + "' and ModelItemID not in ('" + ids.Replace(",", "','") + "')");
                        Response.End();
                    }

                    if (item.ToLower() == "listhtml")
                    {
                        //eOleDB.Execute("update a_eke_sysModelItems set viewhtml='" + value + "' where ModelID='" + ModelID + "' and ModelItemID='" + modelitemid + "'");
                    }
                    if (item.ToLower() == "listorder" && (value.Length == 0 || value == "0")) value = "999999";
                    if (item.ToLower() == "exportorder" && (value.Length == 0 || value == "0")) value = "999999";
                    #region 日期格式
                    if (item.ToLower() == "dateformat")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set FormatString='" + (value.Length > 0 ? "{0:" + value + "}" : "") + "' where ModelItemID='" + modelitemid + "'");
                    }
                    #endregion
                    if (item.ToLower() == "addorder")
                    {
                        if (value.Length == 0 || value == "0") value = "999999";
                        eOleDB.Execute("update a_eke_sysModelItems set addorder='" + value + "',editorder='" + value + "',vieworder='" + value + "' where ModelItemID='" + modelitemid + "'");
                        Response.End();
                    }
                    if (item.ToLower() == "showadd")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set showadd='" + value + "',showedit='" + value + "',showview='" + value + "' where ModelItemID='" + modelitemid + "'");
                        Response.End();
                    }
                    if (item.ToLower() == "controltype")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set controltype='" + value + "',addcontroltype='" + value + "',editcontroltype='" + value + "' where ModelItemID='" + modelitemid + "'");
                        if (value != "text")
                        {
                            eOleDB.Execute("update a_eke_sysModelItems set width='' where ModelItemID='" + modelitemid + "' and width='300'");
                        }
                        if (value == "file")
                        {
                            eOleDB.Execute("update a_eke_sysModelItems set maxlength='0' where ModelItemID='" + modelitemid + "'");
                        }

                        //星级评分
                        if (value == "raty")
                        {
                            string options = "[{\"text\":\"data-number\",\"value\":\"5\"}";
                            options += ",{\"text\":\"data-staroff\",\"value\":\"star-off.png\"}";
                            options += ",{\"text\":\"data-starhalf\",\"value\":\"star-half.png\"}";
                            options += ",{\"text\":\"data-staron\",\"value\":\"star-on.png\"}";
                            options += ",{\"text\":\"data-half\",\"value\":\"false\"}";
                            options += "]";

                            eOleDB.Execute("update a_eke_sysModelItems set Options='" + options + "' where ModelItemID='" + modelitemid + "'");
                        }

                        Response.End();
                    }
                    if (item.ToLower() == "addcolspan")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set addcolspan='" + value + "',editcolspan='" + value + "',viewcolspan='" + value + "' where  ModelItemID='" + modelitemid + "'");
                        Response.End();
                    }
                    if (item.ToLower() == "addrowspan")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set addrowspan='" + value + "',editrowspan='" + value + "',viewrowspan='" + value + "' where ModelItemID='" + modelitemid + "'");
                        Response.End();
                    }
                    if (item.ToLower() == "listwidth" && value!="0")
                    {
                        eOleDB.Execute("update a_eke_sysUserColumns set width='" + value + "' where ModelItemID='" + modelitemid + "' and width=0");
                    }
                    if (value == "NULL")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set " + item + "=" + value + " where ModelItemID='" + modelitemid + "'");

                    }
                    else
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set " + item + "=N'" + value + "' where ModelItemID='" + modelitemid + "'");
                    }
                    if (item.ToLower() == "modeltabid")
                    {
                        eOleDB.Execute("update a_eke_sysModelItems set ModelPanelID=NULL where ModelItemID='" + modelitemid + "'");
                    }
                    Response.End();
                }
                #endregion
                #region 条件

                string modelconditionid = eParameters.QueryString("modelconditionid");
                string modelconditionitemid = eParameters.QueryString("modelconditionitemid");
                if (act == "loadcolumnoptions")
                {

                    sql="update a set a.BindObject=b.BindObject,a.BindRows=b.BindRows,a.BindValue=b.BindValue,a.BindText=b.BindText,a.BindCondition=b.BindConditions,a.BindOrderBy=b.BindOrderBy,a.BindGroupBy=b.BindGroupBy,a.Options=b.Options from a_eke_sysModelConditions a inner join a_eke_sysModelItems b on a.ModelID=b.ModelID and a.Code=b.code ";
                    sql += " where a.ModelConditionID='" + modelconditionid + "'";
                    eOleDB.Execute(sql);
                    Response.End();
                }
                if (act == "addmodelcondition")
                {
                    //string maxnum = eOleDB.getValue("select isnull(MAX(Num),0)+1 from a_eke_sysModelConditions where ModelID='" + ModelID + "'");
                    //string frmname = "s" + maxnum;
                    //eOleDB.Execute("insert into a_eke_sysModelConditions (ModelID,Num,frmName) values ('" + ModelID + "','" + maxnum + "','" + frmname + "')");



                    eOleDB.Execute("update a_eke_sysModels set MaxConds=MaxConds+1 where ModelID='" + ModelID + "'");
                    string MaxConds = eOleDB.getValue("select MaxConds from a_eke_sysModels where ModelID='" + ModelID + "'");



                    eOleDB.Execute("insert into a_eke_sysModelConditions (ModelID,Num) values ('" + ModelID + "','" + MaxConds + "')");
                    Response.End();
                }
                if (act == "setmodelcondition")
                {
                     //拖动排序
                    if (item.ToLower() == "setorders")
                    {
                        string ids=eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysModelConditions set px='" + value + "' where ModelID='" + ModelID + "' and ModelConditionID='" + arr[i] + "'");
                        }
                        eOleDB.Execute("update a_eke_sysModelConditions set px='999999' where ModelID='" + ModelID + "' and ModelConditionID not in ('" + ids.Replace(",", "','") + "')");
                        Response.End();
                    }


                    if (item.ToLower() == "px" && (value.Length == 0 || value == "0")) value = "999999";
                    eOleDB.Execute("update a_eke_sysModelConditions set " + item + "='" + value + "' where ModelID='" + ModelID + "' and ModelConditionID='" + modelconditionid + "'");
                    Response.End();
                }
                if (act == "delmodelcondition")
                {
                    eOleDB.Execute("delete from a_eke_sysModelConditionItems where ModelID='" + ModelID + "' and ModelConditionID='" + modelconditionid + "'");
                    eOleDB.Execute("delete from a_eke_sysModelConditions where ModelID='" + ModelID + "' and ModelConditionID='" + modelconditionid + "'");
                    //eOleDB.Execute("update a_eke_sysModelConditionItems set delTag=1 where ModelID='" + ModelID + "' and ModelConditionID='" + modelconditionid + "'");
                    //eOleDB.Execute("update a_eke_sysModelConditions set delTag=1 where ModelID='" + ModelID + "' and ModelConditionID='" + modelconditionid + "'");
                    Response.End();
                }

                if (act == "addmodelconditionitem")
                {
                    eOleDB.Execute("insert into a_eke_sysModelConditionItems (ModelID,ModelConditionID) values ('" + ModelID + "','" + modelconditionid + "')");
                    Response.End();
                }
                if (act == "setmodelconditionitem")
                {
                    if (item.ToLower() == "px" && (value.Length == 0 || value == "0")) value = "999999";
                    eOleDB.Execute("update a_eke_sysModelConditionItems set " + item + "='" + value + "' where ModelID='" + ModelID + "' and ModelConditionItemID='" + modelconditionitemid + "'");
                    Response.End();
                }
                if (act == "delmodelconditionitem")
                {
                    eOleDB.Execute("delete from a_eke_sysModelConditionItems where ModelID='" + ModelID + "' and ModelConditionItemID='" + modelconditionitemid + "'");
                    Response.End();
                }
                #endregion

                #region 动作
                #region 添加动作
                if (act == "addaction")
                {
                    eOleDB.Execute("insert into a_eke_sysActions (ModelID) values ('" + ModelID + "')");
                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "添加成功" + ModelID);
                    //Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 修改动作
                if (act == "setaction")
                {
                    eOleDB.Execute("update a_eke_sysActions set " + item + "='" + value + "' where ModelID='" + ModelID + "' and ActionID='" + eParameters.QueryString("ActionID") + "'");
                    Response.End();
                }
                #endregion
                #region  删除动作
                if (act == "delaction")
                {
                    eOleDB.Execute("delete from a_eke_sysActions where ActionID='" + eParameters.QueryString("ActionID") + "'");
                    Response.End();
                }
                #endregion
                #endregion

                #region 流程
                #region 添加
                if (act == "addcheckup")
                {
                    eOleDB.Execute("insert into a_eke_sysCheckUps (ModelID) values ('" + ModelID + "')");
                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "添加成功" + ModelID);
                    //Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 修改动作
                if (act == "setcheckup")
                {
                    //拖动排序
                    if (item.ToLower() == "setorders")
                    {
                        string ids = eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysCheckUps set px='" + value + "' where ModelID='" + ModelID + "' and CheckupID='" + arr[i] + "'");
                        }
                        Response.End();
                    }

                    if (item.ToLower() == "px" && (value.Length == 0 || value == "0")) value = "999999";
                    eOleDB.Execute("update a_eke_sysCheckUps set " + item + "='" + value + "' where ModelID='" + ModelID + "' and CheckupID='" + eParameters.QueryString("CheckupID") + "'");
                    Response.End();
                }
                #endregion
                #region  删除
                if (act == "delcheckup")
                {
                    eOleDB.Execute("delete from a_eke_sysCheckUps where CheckupID='" + eParameters.QueryString("CheckupID") + "'");
                    Response.End();
                }
                #endregion
                #endregion

                string modeltabid = eParameters.QueryString("modeltabid");
                #region 选项卡
                #region 添加
                if (act == "addmodeltab")
                {
                    eOleDB.Execute("insert into a_eke_sysModelTabs (ModelID) values ('" + ModelID + "')");
                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "添加成功" + ModelID);
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 修改
                if (act == "setmodeltab")
                {
                    //拖动排序
                    if (item.ToLower() == "setorders")
                    {
                        string ids = eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysModelTabs set px='" + value + "' where ModelID='" + ModelID + "' and ModelTabID='" + arr[i] + "'");
                        }
                        Response.End();
                    }

                    if (item.ToLower() == "px" && (value.Length == 0 || value == "0")) value = "999999";
                    eOleDB.Execute("update a_eke_sysModelTabs set " + item + "='" + value + "' where ModelID='" + ModelID + "' and ModelTabID='" + modeltabid + "'");
                    Response.End();
                }
                #endregion
                #region  删除
                if (act == "delmodeltab")
                {
                    eOleDB.Execute("delete from a_eke_sysModelTabs where ModelTabID='" + modeltabid + "'");

                    eOleDB.Execute("update a_eke_sysModelPanels set ModelTabID=NULL where ModelTabID='" + modeltabid + "'");
                    eOleDB.Execute("update a_eke_sysModelItems set ModelTabID=NULL,ModelPanelID=NULL where ModelTabID='" + modeltabid + "'");
                    eOleDB.Execute("update a_eke_sysModels set ModelTabID=NULL,ModelPanelID=NULL where ModelTabID='" + modeltabid + "'");
                    Response.End();
                }
                #endregion
                #endregion

                string modelpaneid = eParameters.QueryString("modelpaneid");
                #region 面板
                #region 添加
                if (act == "addmodelgroup")
                {
                    eOleDB.Execute("insert into a_eke_sysModelPanels (ModelID) values ('" + ModelID + "')");
                    eJson json = new eJson();
                    json.Add("success", "1");
                    json.Add("message", "添加成功" + ModelID);
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 修改
                if (act == "setmodelgroup")
                {
                    //拖动排序
                    if (item.ToLower() == "setorders")
                    {
                        string ids = eParameters.Form("ids");
                        string[] arr = ids.Split(",".ToCharArray());
                        for (int i = 0; i < arr.Length; i++)
                        {
                            value = (i + 1).ToString();
                            eOleDB.Execute("update a_eke_sysModelPanels set px='" + value + "' where ModelID='" + ModelID + "' and ModelPanelID='" + arr[i] + "'");
                        }
                        Response.End();
                    }


                    if (item.ToLower() == "px" && (value.Length == 0 || value == "0")) value = "999999";
                    if (value == "NULL")
                    {
                        eOleDB.Execute("update a_eke_sysModelPanels set " + item + "=" + value + " where ModelPanelID='" + modelpaneid + "'");
                    }
                    else
                    {
                        eOleDB.Execute("update a_eke_sysModelPanels set " + item + "='" + value + "' where ModelPanelID='" + modelpaneid + "'");
                    }
                    Response.End();
                }
                #endregion
                #region  删除
                if (act == "delmodelgroup")
                {
                    eOleDB.Execute("delete from a_eke_sysModelPanels where ModelPanelID='" + modelpaneid + "'");
                    eOleDB.Execute("update a_eke_sysModelItems set ModelPanelID=NULL where ModelPanelID='" + modelpaneid + "'");
                    eOleDB.Execute("update a_eke_sysModels set ModelPanelID=NULL where ModelPanelID='" + modelpaneid + "'");
                    Response.End();
                }
                #endregion
                #endregion
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