using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.system
{
    public partial class Organizationals : System.Web.UI.Page
    {
        public eAction Action;
        public eList elist;
        public eForm eform;
        public eUser user;
        public string ModelID = eParameters.Request("modelid");
        public string eTree="";
        private string id = eParameters.QueryString("id");
        public string pid = eParameters.QueryString("pid");
        public string allids = "";
        string sql = "";
        public bool Ajaxget = false;
        public eModel model;
        protected void Page_Load(object sender, EventArgs e)
        {
            allids = getParentIDS(pid);
            user = new eUser("System");
            model = new eModel(ModelID, user);

            Action = new eAction(user);
            Action.Actioning += new eActionHandler(Action_Actioning);
            Action.Listen();
            
        }
        private string getParentIDS(string ID)
        {
            if (ID.Length == 0) return "";
            string _back = "";
            string pid = eOleDB.getValue("select ParentID from Organizationals where OrganizationalID='" + ID + "'");
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
            StringBuilder sb = new StringBuilder();
           

            sql = "select isnull(max(px),0) as maxpx,count(*) as ct from Organizationals where DelTag=0";
            sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");
            DataTable tb = eOleDB.getDataTable(sql);
            if (Convert.ToInt32(tb.Rows[0]["ct"]) != Convert.ToInt32(tb.Rows[0]["maxpx"]))
            {
                sql = "update Organizationals set PX=(";
                sql += "select b.rownum from ";
                sql += "(";
                sql += "select ROW_NUMBER() over(order by px,addtime) as rownum,OrganizationalID,addTime from Organizationals where delTag=0";
                sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");
                sql += ") as b where b.OrganizationalID=Organizationals.OrganizationalID";
                sql += ")  where delTag=0";
                sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");
                eOleDB.Execute(sql);
            }

            sql = "select OrganizationalID,ParentID,MC,PX from Organizationals where DelTag=0";
            sql += (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'");
            sql += " Order by PX,addTime";
            tb = eOleDB.getDataTable(sql);
           
            if (ParentID.Length == 0)
            {
                sb.Append("<ul id=\"etree\" class=\"etree\" PID=\"NULL\" oncontextmenu=\"return false;\">\r\n");
            }
            else
            {
                sb.Append("<ul PID=\"" + ParentID + "\"" + (allids.IndexOf(ParentID) == -1 ? " style=\"display:none;\"" : "") + " oncontextmenu=\"return false;\">\r\n");
            }
            foreach (DataRow dr in tb.Rows)
            {
                string ct = eOleDB.getValue("select count(*) from  Organizationals where DelTag=0 and ParentID='" + dr["OrganizationalID"].ToString() + "'");
                sb.Append("<li dataid=\"" + dr["OrganizationalID"].ToString() + "\"");
                if (allids.ToLower().IndexOf(dr["OrganizationalID"].ToString().ToLower()) == -1 || ct=="0" )
                {
                    sb.Append(" dataurl=\"" + (ct == "0" ? "" : "Organizationals.aspx?act=gethtml&modelid=" + ModelID + "&pid=" + dr["OrganizationalID"].ToString()) + "\"");
                    sb.Append(" class=\"" + (ct == "0" ? "" : "close") + "\">");
                    sb.Append("<div oncontextmenu=\"return false;\" onmousedown=\"div_contextmenu(event,this);\"><a dataid=\"" + dr["OrganizationalID"].ToString() + "\" href=\"?modelid=" + ModelID + "&pid=" + dr["OrganizationalID"].ToString() + "\" oncontextmenu=\"return false;\" onmousedown=\"contextmenu(event,this);\">" + dr["MC"].ToString() + " (" + ct + ")</a>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append(" dataurl=\"\"");
                    sb.Append(" class=\"\">");
                    sb.Append("<div oncontextmenu=\"return false;\" onmousedown=\"div_contextmenu(event,this);\"><a dataid=\"" + dr["OrganizationalID"].ToString() + "\" href=\"?modelid=" + ModelID + "&pid=" + dr["OrganizationalID"].ToString() + "\" oncontextmenu=\"return false;\" onmousedown=\"contextmenu(event,this);\">" + dr["MC"].ToString() + " (" + ct + ")</a>");
                    sb.Append("</div>");
                    sb.Append(getTree(dr["OrganizationalID"].ToString()));
                }

                sb.Append("</li>\r\n");
            }
            sb.Append("</ul>\r\n");
            return sb.ToString();
        }
        protected void Action_Actioning(string Actioning)
        {
            
            eform = new eForm("Organizationals", user);
            eform.ModelID = "1";
            if (Actioning.ToLower() == "gethtml")
            {
                //Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
                Response.Write(getTree(eParameters.QueryString("pid")));
                Response.End();
                return;
            }
            if (Actioning.ToLower() == "setsort")
            {
                #region 位置
                string ParentID = eParameters.QueryString("pid").Replace("NULL", "");
                int index=Convert.ToInt32( eParameters.QueryString("index"));
                DataRow dr = eOleDB.getDataTable("SELECT * FROM Organizationals where OrganizationalID='" + id + "'").Select()[0];
                string oldpid = dr["ParentID"].ToString();
                int oldindex = Convert.ToInt32(dr["px"]);


                if (ParentID == oldpid)//父级不变
                {
                    if (oldindex < index) //小变大
                    {
                        sql = "update Organizationals set PX=PX-1 where delTag=0 " + (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'") + " and PX>" + oldindex.ToString() + " and PX<=" + index.ToString();
                        eOleDB.Execute(sql);
                    }
                    else //大变小
                    {
                        sql = "update Organizationals set PX=PX+1 where delTag=0 " + (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'") + " and PX>=" + index.ToString() + " and PX<" + oldindex.ToString();
                        eOleDB.Execute(sql);
                    }
                    sql = "update Organizationals set PX='" + index.ToString() + "' where OrganizationalID='" + id + "'";
                    eOleDB.Execute(sql);
                }
                else
                {
                    sql = "update Organizationals set PX=PX-1 where delTag=0 " + (oldpid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + oldpid + "'") + " and PX>" + oldindex.ToString();
                    eOleDB.Execute(sql);

                    sql = "update Organizationals set PX=PX+1 where delTag=0 " + (ParentID.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + ParentID + "'") + " and PX>=" + index.ToString();
                    eOleDB.Execute(sql);

                    sql = "update Organizationals set PX='" + index.ToString() + "',ParentID=" + (ParentID.Length == 0 ? "NULL" : "'" + ParentID + "'") + " where OrganizationalID='" + id + "'";
                    eOleDB.Execute(sql);
                }
                eBase.End();
                #endregion
            }

            if (Actioning.Length > 0)
            {
                eform.onChange += new eFormTableEventHandler(eform_onChange);
                eform.AddControl(eFormControlGroup);
                if (Actioning == "add" && pid.Length > 0) M1_F2.Value = pid;
                eform.Handle();
            }
            else
            {
                eTree = getTree("");
                if (Request.QueryString["ajax"] != null)
                {
                    Response.Clear();
                    eJson json = new eJson();
                    json.Add("body", eBase.encode(eTree));
                    HttpContext.Current.Response.Write(json.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }
        private void eform_onChange(object sender, eFormTableEventArgs e)
        {
            DataRow dr;
            string pid = "";
            string oldpid = "";
            int oldindex = 0;
            string maxpx = "";

            switch (e.eventType)
            {
                case eFormTableEventType.Inserting:
                    #region 添加
                    string px = eform.Fields["px"].ToString();
                    pid = eform.Fields["ParentID"].ToString();
                    maxpx = eOleDB.getValue("select isnull(max(px),0) + 1 from Organizationals where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'"));
                    if (px == "" || px == "0" || px == "999999" || Convert.ToInt32(px) > Convert.ToInt32(maxpx))
                    {                        
                        eform.Fields["px"] = maxpx;
                    }
                    else
                    {
                        sql = "update Organizationals set PX=PX+1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>=" + px;
                        eOleDB.Execute(sql);
                    }
                    #endregion
                    break;
                case eFormTableEventType.Updating:
                    #region 修改
                    dr = eOleDB.getDataTable("SELECT * FROM Organizationals where OrganizationalID='" + e.ID + "'").Select()[0];
                    pid = eform.Fields["ParentID"].ToString();
                    oldpid = dr["ParentID"].ToString();
                    oldindex = Convert.ToInt32(dr["px"]);
                    int index = Convert.ToInt32(eform.Fields["px"]);
                    if (pid == oldpid)//父级不变
                    {
                        if (oldindex < index) //小变大
                        {
                            sql = "update Organizationals set PX=PX-1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>" + oldindex.ToString() + " and PX<=" + index.ToString();
                            eOleDB.Execute(sql);
                        }
                        else //大变小
                        {
                            sql = "update Organizationals set PX=PX+1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>=" + index.ToString() + " and PX<" + oldindex.ToString();
                            eOleDB.Execute(sql);
                        }
                        maxpx = eOleDB.getValue("select isnull(max(px),0) + 1 from Organizationals where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'"));
                        if (index > Convert.ToInt32(maxpx))
                        {
                            eform.Fields["px"] = maxpx;
                        }

                    }
                    else
                    {
                        sql = "update Organizationals set PX=PX-1 where delTag=0 " + (oldpid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + oldpid + "'") + " and PX>" + oldindex.ToString();
                        eOleDB.Execute(sql);

                        sql = "update Organizationals set PX=PX+1 where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'") + " and PX>=" + index.ToString();
                        eOleDB.Execute(sql);

                        maxpx = eOleDB.getValue("select isnull(max(px),0) + 1 from Organizationals where delTag=0 " + (pid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + pid + "'"));
                        if (index > Convert.ToInt32(maxpx))
                        {
                            eform.Fields["px"] = maxpx;
                        }
                    }
                    #endregion
                    break;
                case eFormTableEventType.Deleting:
                    #region 删除
                    dr = eOleDB.getDataTable("SELECT * FROM Organizationals where OrganizationalID='" + e.ID + "'").Select()[0];
                    oldpid = dr["ParentID"].ToString();
                    oldindex = Convert.ToInt32(dr["px"]);

                    sql = "update Organizationals set PX=PX-1 where delTag=0 " + (oldpid.Length == 0 ? " and ParentID IS NULL" : " and ParentID='" + oldpid + "'") + " and PX>" + oldindex.ToString();
                    eOleDB.Execute(sql);

                    sql = "update Organizationals set PX='0' where OrganizationalID='" + e.ID + "'";
                    eOleDB.Execute(sql);

                  
                    #endregion
                    break;
                case eFormTableEventType.Deleted:
                    oldpid = eOleDB.getValue("SELECT ParentID FROM Organizationals where OrganizationalID='" + e.ID + "'");

                    if (Request.QueryString["ajaxget"] != null)
                    {
                        eJson json = new eJson();
                        json.Add("success", "1");
                        json.Add("message", "删除成功!");
                        Response.Clear();
                        Response.Write(json.ToString());
                        Response.End();
                    }
                    else
                    {

                        string url = "Organizationals.aspx?modelid=" + ModelID;
                        if (oldpid.Length > 0) url += "&act=view&id=" + oldpid;
                        Response.Redirect(url, true);

                    }



                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = model.ModelInfo["mc"].ToString() + " - " + eConfig.getString("systemName");
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["ajaxget"] != null) Ajaxget = Convert.ToBoolean(Request.QueryString["ajaxget"]);

            if (!Ajaxget)
            {
                MasterPageFile = "~/Master/systemMain.Master";
            }
            else
            {
                MasterPageFile = "~/Master/systemMainNone.Master";
            }
        }
    }
}