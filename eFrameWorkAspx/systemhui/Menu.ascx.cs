using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.systemhui
{

    public partial class Menu : System.Web.UI.UserControl
    {
        private DataTable _models;
        protected DataTable Models
        {
            get
            {
                if (_models == null)
                {
                    string roleids = "";
                    if (eBase.readFast)
                    {
                        DataRow[] rows = eBase.a_eke_sysUsers.Select("UserID='" + user.ID + "'");
                        if (rows.Length > 0) roleids = rows[0]["RoleID"].ToString();
                        _models = eBase.a_eke_sysModels.Clone();
                        rows = eBase.a_eke_sysPowers.Select("canList=1 and (UserID='" + user.ID + "'" + (" or Convert(RoleID, 'System.String') in ('" + roleids.Replace(",", "','") + "')") + ") and ApplicationID is Null");
                        string ids = "";
                        int idx = 0;
                        foreach (DataRow dr in rows)
                        {
                            if (idx > 0) ids += ",";
                            ids += "'" + dr["ModelID"].ToString() + "'";
                            idx++;
                            DataRow[] rowsa = eBase.a_eke_sysModels.Select("ModelID='" + dr["ModelID"].ToString() + "'");
                            if (rowsa.Length > 0)
                            {
                                _models.Rows.Add(rowsa[0].ItemArray);
                            }

                        }
                    }
                    else
                    {
                        roleids = eOleDB.getValue("select RoleID from a_eke_sysUsers where UserID='" + user.ID + "'");
                        string sql = "SELECT ModelID,Type,ParentID,MC,Auto,AspxFile,PX,addTime FROM a_eke_sysModels where delTag=0";
                        sql += " and ModelID in (select ModelID from a_eke_sysPowers where delTag=0 and canList=1 and (UserID='" + user.ID + "'" + (roleids.Length > 0 ? " or RoleID in ('" + roleids.Replace(",", "','") + "')" : "") + ") and ApplicationID is Null)";
                        _models = eOleDB.getDataTable(sql);
                    }
                }
                return _models;
            }
        }
        private DataTable _allmodels;
        protected DataTable allModels
        {
            get
            {
                if (_allmodels == null)
                {
                    string sql = "SELECT ModelID,Type,ParentID,MC,Auto,AspxFile,PX,addTime,IcoHTML FROM a_eke_sysModels where delTag=0 and subModel=0 order by px";                   
                    _allmodels = eOleDB.getDataTable(sql);
                }
                return _allmodels;
            }
        }
        public string ModelID = eParameters.Request("modelid");
        public string aspfile = eBase.getAspxFileName().ToLower();
        private string allids = "";
        private eUser user;
        private List<string> ItemIDS = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
         
            user = new eUser("System");
            allids = getParentIDS(ModelID);

            foreach (DataRow dr in Models.Rows)
            {
                string itemid = dr["ModelID"].ToString();                
                addParentID(ItemIDS, itemid);
            }
            litMenu.Text = getMenus("");
        }
        private void addParentID(List<string> keys, string ItemID)
        {
            DataRow[] rows = allModels.Select("ModelID='" + ItemID + "'");
            if (rows.Length > 0)
            {
                keys.Add(ItemID);
                string ParentID = rows[0]["ParentID"].ToString();
                if (ParentID.Length > 0)
                {
                    addParentID(keys, ParentID);
                }
            }
        }
        private string getMenus(string ParentID)
        {
            StringBuilder sb = new StringBuilder();


            DataRow[] rows = allModels.Select(ParentID.Length == 0 ? "ParentID is Null" : "ParentID='" + ParentID + "'", "PX,addTime");
            for (int i = 0; i < rows.Length; i++)
            {
                if (!ItemIDS.Contains(rows[i]["ModelID"].ToString())) continue;
                if (rows[i]["Type"].ToString() == "2") //父栏目
                {
                    sb.Append("<dl>\r\n");
                    sb.Append("<dt>" + rows[i]["IcoHTML"].ToString() + rows[i]["mc"].ToString() + "<i class=\"Hui-iconfont menu_dropdown-arrow\">&#xe6d5;</i></dt>\r\n");
                    sb.Append("<dd>\r\n");
                    sb.Append(getMenus(rows[i]["ModelID"].ToString()));
                    sb.Append("</dd>\r\n");
                    sb.Append("</dl>\r\n");
                }
                else
                {
                    sb.Append("<ul>\r\n");
                    sb.Append("<li>");
                    if (rows[i]["AspxFile"].ToString().Length > 0 && rows[i]["Auto"].ToString() == "False")
                    {
                        sb.Append("<a href=\"javascript:;\" data-href=\"" + rows[i]["AspxFile"].ToString() + "?modelid=" + rows[i]["ModelID"].ToString() + "\"");
                    }
                    else
                    {
                        sb.Append("<a href=\"javascript:;\" data-href=\"Model.aspx?modelid=" + rows[i]["ModelID"].ToString() + "\"");
                    }
                    sb.Append(" onfocus=\"this.blur();\" data-title=\"" + rows[i]["MC"].ToString() + "\" style=\"font-weight:normal;\">");
                    sb.Append(rows[i]["IcoHTML"].ToString());
                    sb.Append(rows[i]["mc"].ToString());
                    sb.Append("</a>");
                    sb.Append("</li>\r\n");
                    sb.Append("</ul>\r\n");
                }
            }


            return sb.ToString();
        }
        private string getMenus2(string ParentID)
        {
            StringBuilder sb = new StringBuilder();

           
            DataRow[] rows = allModels.Select(ParentID.Length == 0 ? "ParentID is Null" : "ParentID='" + ParentID + "'", "PX,addTime");
            for (int i = 0; i < rows.Length; i++)
            {
                if (!ItemIDS.Contains(rows[i]["ModelID"].ToString())) continue;

                if (ParentID.Length == 0) //一级
                {
                    sb.Append("<dl>\r\n");

                    sb.Append("</dl>\r\n");
                }
                else
                {
                    sb.Append("<dd>\r\n");
                    sb.Append("<ul>\r\n");
                }

                if (rows[i]["Type"].ToString() == "2") //父栏目
                {

                }
                else
                {
                }

                if (ParentID.Length == 0)
                {
                    sb.Append("</dl>\r\n");
                }
                else
                {
                    sb.Append("</ul>\r\n");
                    sb.Append("</dd>\r\n");
                }
                
            }

           
            return sb.ToString();
        }
        private string getMenus1(string ParentID)
        {
            StringBuilder sb = new StringBuilder();
            if (ParentID.Length == 0)
            {
                sb.Append("<ul>\r\n");
               // sb.Append("<li" + (aspfile == "default.aspx" ? " class=\"cur\"" : "") + "><a href=\"Default.aspx\" onfocus=\"this.blur();\"" + (aspfile == "default.aspx" ? " class=\"cur\"" : "") + " style=\"font-weight:normal;\">首页</a></li>\r\n");

            }
            else
            {
                sb.Append("<ul" + (allids.IndexOf(ParentID) == -1 ? " style=\"display:none;\"" : "") + ">\r\n");
            }
            DataRow[] rows = allModels.Select(ParentID.Length == 0 ? "ParentID is Null" : "ParentID='" + ParentID + "'", "PX,addTime");
            for (int i = 0; i < rows.Length; i++)
            {
                if (!ItemIDS.Contains(rows[i]["ModelID"].ToString())) continue;

                sb.Append("<li" + (allids.IndexOf( rows[i]["ModelID"].ToString())>-1 ? " class=\"cur\"" : "") + ">");
                if (rows[i]["Type"].ToString() == "2")
                {
                    sb.Append("<a href=\"javascript:;\" onclick=\"showmenu(this);\"");
                    sb.Append(allids.IndexOf(rows[i]["ModelID"].ToString()) > -1 ? " class=\"open\"" : " class=\"close\"");
                }
                else
                {
                    if (rows[i]["AspxFile"].ToString().Length > 0 && rows[i]["Auto"].ToString() == "False")
                    {
                        sb.Append("<a href=\"javascript:;\" data-href=\"" + rows[i]["AspxFile"].ToString() + "?modelid=" + rows[i]["ModelID"].ToString() + "\"");
                    }
                    else
                    {
                        sb.Append("<a href=\"javascript:;\" data-href=\"Model.aspx?modelid=" + rows[i]["ModelID"].ToString() + "\"");
                    }
                }
                sb.Append(" onfocus=\"this.blur();\" data-title=\"" + rows[i]["MC"].ToString() + "\" style=\"font-weight:normal;\">");
                sb.Append(rows[i]["IcoHTML"].ToString());
                sb.Append(rows[i]["mc"].ToString());
                sb.Append("</a>");
                if (rows[i]["Type"].ToString() == "2")
                {
                    sb.Append(getMenus(rows[i]["ModelID"].ToString()));
                }
                sb.Append("</li>\r\n");
            }
            sb.Append("</ul>\r\n");
            return sb.ToString();
        }
        private string getParentIDS(string ID)
        {
            if (ID.Length == 0) return "";
            string _back = "";
            //string pid = eOleDB.getValue("select ParentID from a_eke_sysModels where ModelID='" + ID + "'");
            DataRow[] rows = allModels.Select("ModelID='" + ID + "'");
            if (rows.Length == 0) return "";
            string pid = rows[0]["ParentID"].ToString();
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
        protected void Page_Load_Bak(object sender, EventArgs e)
        {
            Response.Charset = "UTF-8";

            eUser user = new eUser("System");
            string sql = "SELECT b.ModelID,b.MC,b.AspxFile,b.Auto  FROM a_eke_sysPowers a ";
            sql += " inner join a_eke_sysModels b on a.ModelID=b.ModelID and b.delTag=0 ";
            sql += " where a.delTag=0 and a.canList=1 and a.UserID='" + user.ID + "' ";
            sql += " order by a.PX,b.addTime ";

            StringBuilder sb = new StringBuilder();
            sb.Append("<a href=\"Default.aspx\" onfocus=\"this.blur();\"" + (aspfile == "default.aspx" ? " class=\"cur\"" : "") + ">首3页</a>\r\n");
            DataTable tb = eOleDB.getDataTable(sql);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                sb.Append("<a href=\"");
                if (tb.Rows[i]["AspxFile"].ToString().Length > 0 && tb.Rows[i]["Auto"].ToString() == "False")
                {
                    sb.Append(tb.Rows[i]["AspxFile"].ToString() + "?modelid=" + tb.Rows[i]["ModelID"].ToString());
                }
                else
                {
                    sb.Append("Model.aspx?modelid=" + tb.Rows[i]["ModelID"].ToString());
                }

                sb.Append("\" " + (tb.Rows[i]["ModelID"].ToString() == ModelID ? " class=\"cur\"" : "") + " onfocus=\"this.blur();\">" + tb.Rows[i]["MC"].ToString() + "</a>\r\n");
            }
            litMenu.Text = sb.ToString();
        }
    }
}