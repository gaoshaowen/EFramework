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
    public partial class Menus : System.Web.UI.Page
    {
        private DataTable _models;
        protected DataTable Models
        {
            get
            {
                if (_models == null)
                {

                    _models = eOleDB.getDataTable("SELECT ModelID,Type,ParentID,MC,Auto,AspxFile,PX,addTime FROM a_eke_sysModels where delTag=0");
                }
                return _models;
            }
        }
        private string id = eParameters.QueryString("id");
        public string allids = "";
        public eAction Action;
        public eForm eform;
        public eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Manage");
            Action = new eAction();
            eform = new eForm("a_eke_sysModels", user);
            allids = getParentIDS(id);
            //eBase.Writeln(allids);
            LitMenus.Text = getTree("");

            Action = new eAction(user);
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
            
        }
        protected void Action_Actioning(string Actioning)
        {
            string sql = "";
            eform = new eForm("a_eke_sysModels", user);
            eform.ModelID = "1";
            if (Actioning.ToLower() == "view")
            {
                eFormControl uc = new eFormControl("f0");
                uc.Field = "Type";
                eform.Controls.Add("f0",uc);
            }
            if (Actioning.ToLower() == "setsort")
            {
                string ParentID = eParameters.QueryString("pid").Replace("NULL", "");
                int index = Convert.ToInt32(eParameters.QueryString("index"));
                DataRow dr = eOleDB.getDataTable("SELECT * FROM a_eke_sysModels where ModelID='" + id + "'").Select()[0];
                string oldpid = dr["ParentID"].ToString();
                int oldindex = Convert.ToInt32(dr["px"]);


                if (ParentID == oldpid)//父级不变
                {
                    if (oldindex < index) //小变大
                    {
                        sql = "update a_eke_sysModels set PX=PX-1 where delTag=0 " + (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'") + " and PX>" + oldindex.ToString() + " and PX<=" + index.ToString();
                        eOleDB.Execute(sql);
                    }
                    else //大变小
                    {
                        sql = "update a_eke_sysModels set PX=PX+1 where delTag=0 " + (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'") + " and PX>=" + index.ToString() + " and PX<" + oldindex.ToString();
                        eOleDB.Execute(sql);
                    }
                    sql = "update a_eke_sysModels set PX='" + index.ToString() + "' where ModelID='" + id + "'";
                    eOleDB.Execute(sql);
                }
                else
                {
                    sql = "update a_eke_sysModels set PX=PX-1 where delTag=0 " + (oldpid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + oldpid + "'") + " and PX>" + oldindex.ToString();
                    eOleDB.Execute(sql);

                    sql = "update a_eke_sysModels set PX=PX+1 where delTag=0 " + (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'") + " and PX>=" + index.ToString();
                    eOleDB.Execute(sql);

                    sql = "update a_eke_sysModels set PX='" + index.ToString() + "',ParentID=" + (ParentID.Length == 0 ? "NULL" : "'" + ParentID + "'") + " where ModelID='" + id + "'";
                    eOleDB.Execute(sql);
                }
                eBase.clearDataCache("a_eke_sysModels");
                eBase.End();
            }
            if (Actioning.Length > 0)
            {
                eform.onChange += new eFormTableEventHandler(eform_onChange);
                eform.AddControl(eFormControlGroup);
                eform.Handle();
            }
        }
        private void eform_onChange(object sender, eFormTableEventArgs e)
        {
            string sql = "";
            DataRow dr;
            string pid = "";
            string oldpid = "";
            int oldindex = 0;
            string maxpx = "";

            switch (e.eventType)
            {
                case eFormTableEventType.Inserting:
                    #region 添加
                    eform.Fields.Add("Type", "2");
                    string px = eform.Fields["px"].ToString();
                    pid = eform.Fields["ParentID"].ToString();
                    maxpx = eOleDB.getValue("select isnull(max(px),0) + 1 from a_eke_sysModels where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'"));
                    if (px == "" || px == "0" || px == "999999" || Convert.ToInt32(px) > Convert.ToInt32(maxpx))
                    {
                        eform.Fields["px"] = maxpx;
                    }
                    else
                    {
                        sql = "update a_eke_sysModels set PX=PX+1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>=" + px;
                        eOleDB.Execute(sql);
                    }
                    #endregion
                    break;
                case eFormTableEventType.Updating:
                    #region 修改
                    dr = eOleDB.getDataTable("SELECT * FROM a_eke_sysModels where ModelID='" + e.ID + "'").Select()[0];
                    pid = eform.Fields["ParentID"].ToString();
                    oldpid = dr["ParentID"].ToString();
                    oldindex = Convert.ToInt32(dr["px"]);
                    int index = Convert.ToInt32(eform.Fields["px"]);
                    if (pid == oldpid)//父级不变
                    {
                        if (oldindex < index) //小变大
                        {
                            sql = "update a_eke_sysModels set PX=PX-1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>" + oldindex.ToString() + " and PX<=" + index.ToString();
                            eOleDB.Execute(sql);
                        }
                        else //大变小
                        {
                            sql = "update a_eke_sysModels set PX=PX+1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>=" + index.ToString() + " and PX<" + oldindex.ToString();
                            eOleDB.Execute(sql);
                        }
                        maxpx = eOleDB.getValue("select isnull(max(px),0) + 1 from a_eke_sysModels where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'"));
                        if (index > Convert.ToInt32(maxpx))
                        {
                            eform.Fields["px"] = maxpx;
                        }

                    }
                    else
                    {
                        sql = "update a_eke_sysModels set PX=PX-1 where delTag=0 " + (oldpid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + oldpid + "'") + " and PX>" + oldindex.ToString();
                        eOleDB.Execute(sql);

                        sql = "update a_eke_sysModels set PX=PX+1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>=" + index.ToString();
                        eOleDB.Execute(sql);

                        maxpx = eOleDB.getValue("select isnull(max(px),0) + 1 from a_eke_sysModels where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'"));
                        if (index > Convert.ToInt32(maxpx))
                        {
                            eform.Fields["px"] = maxpx;
                        }
                    }
                    #endregion
                    break;
                case eFormTableEventType.Deleting:
                    #region 删除
                    dr = eOleDB.getDataTable("SELECT * FROM a_eke_sysModels where ModelID='" + e.ID + "'").Select()[0];
                    oldpid = dr["ParentID"].ToString();
                    oldindex = Convert.ToInt32(dr["px"]);

                    sql = "update a_eke_sysModels set PX=PX-1 where delTag=0 " + (oldpid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + oldpid + "'") + " and PX>" + oldindex.ToString();
                    eOleDB.Execute(sql);

                    sql = "update a_eke_sysModels set PX='0' where ModelID='" + e.ID + "'";
                    eOleDB.Execute(sql);

                    sql = "update a_eke_sysModels set ParentID=NULL where Type=1 and ParentID='" + e.ID + "'";
                    eOleDB.Execute(sql);

                    sql = "update a_eke_sysModels set ParentID=NULL,delTag=1 where Type=2 and ParentID='" + e.ID + "'";
                    eOleDB.Execute(sql);
                    #endregion
                    break;
                case eFormTableEventType.Deleted:
                    oldpid = eOleDB.getValue("SELECT ParentID FROM Dictionaries where DictionarieID='" + e.ID + "'");
                    string url = "Menus.aspx";
                    if (oldpid.Length > 0) url += "?act=view&id=" + oldpid;
                    Response.Redirect(url, true);
                    break;
            }
        }
        private string getParentIDS(string ID)
        {
            if (ID.Length == 0) return "";
            string _back = "";
            string pid = eOleDB.getValue("select ParentID from a_eke_sysModels where ModelID='" + ID + "'");
            if (pid.Length == 0)
            {
                _back = ID;
            }
            else
            {
                _back = getParentIDS(pid) + "," + ID;
            }
            return _back;
        }
        private string getTree(string ParentID)
        {

            string sql = "select isnull(max(px),0) as maxpx,count(*) as ct from  a_eke_sysModels  where DelTag=0";
            sql += " and ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'");
            sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");

            DataTable tb = eOleDB.getDataTable(sql);
            if (Convert.ToInt32(tb.Rows[0]["ct"]) != Convert.ToInt32(tb.Rows[0]["maxpx"]))
            {
                sql = "update a_eke_sysModels set PX=(";
                sql += "select b.rownum from ";
                sql += "(";
                sql += "select ROW_NUMBER() over(order by px,addtime) as rownum,ModelID,addTime from a_eke_sysModels where delTag=0";
                sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");
                sql += ") as b where b.ModelID=a_eke_sysModels.ModelID";
                sql += ")  where delTag=0";
                sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");
                eOleDB.Execute(sql);
            }

            sql = "SELECT ModelID,Type,ParentID,MC,Auto,AspxFile,PX,addTime FROM a_eke_sysModels where delTag=0 and subModel=0 ";
            sql += " and ServiceID" + (user["ServiceID"].Length == 0 ? " is null" : "='" + user["ServiceID"] + "'");
            sql+=" and " + (ParentID.Length == 0 ? "ParentID is Null" : "ParentID='" + ParentID + "'");
            DataRow[] rows = eOleDB.getDataTable(sql).Select("", "PX,addTime");
            StringBuilder sb=new StringBuilder();
            if (ParentID.Length == 0)
            {
                sb.Append("<ul id=\"etree\" class=\"etree\" PID=\"NULL\">\r\n");
            }
            else
            {
                sb.Append("<ul PID=\"" + ParentID + "\"" + (allids.IndexOf(ParentID) == -1 ? " style=\"display:none;\"" : "") + ">\r\n");
            }
            for (int i = 0; i < rows.Length; i++)
            {
                DataRow[] rs = Models.Select("ParentID='" + rows[i]["ModelID"].ToString() + "' and Type=2");
                sb.Append("<li dataid=\"" + rows[i]["ModelID"].ToString() + "\"");
                sb.Append(" class=\"" + ((rs.Length > 0 || rows[i]["Type"].ToString() == "2") && allids.IndexOf(rows[i]["ModelID"].ToString()) == -1 ? "close" : "") + "\"");
                sb.Append("><div><a href=\"Menus.aspx?act=view&id=" + rows[i]["ModelID"].ToString() + "\">" + rows[i]["mc"].ToString() + "</a></div>");
                if (rows[i]["Type"].ToString() == "2")
                {
                    sb.Append(getTree(rows[i]["ModelID"].ToString()));
                }
                sb.Append("</li>\r\n");
            }
            sb.Append("</ul>\r\n");
            return sb.ToString();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "菜单管理 - " + eConfig.getString("manageName"); 
            }
        }
    }
}