using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.systemhui
{
    public partial class PartMore : System.Web.UI.Page
    {
        public string act = "";
        private string parentModelID = "9e9496ac-7a22-419d-95da-29507ecebe9a";
        private string parentID = "";
        public string modelid = "2";
        public string frmwidth = "450px";
        public string frmheight = "280px";
        public string modelName = "一对多";
        public string aspxFile = "PartMore.aspx";
        private DataTable _data;
        public DataTable Data
        {
            get 
            {
                if (_data == null)
                {
                    _data = eOleDB.getDataTable("SELECT PartMoreID,XM,XB,DH FROM Demo_Customs_PartMore where CustomID='" + parentID + "' and deltag=0 order by addTime");
                }
                return _data;
            }
        }
        private string _json = null;
        public string getJson
        {
            get
            {
                if (_json == null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"eformdata_" + modelid + "\":[");
                    int i = 0;
                    foreach (DataRow dr in Data.Rows)
                    {
                        if(i>0)sb.Append(",");
                        sb.Append("{\"ID\":\"" + dr["PartMoreID"].ToString() + "\",\"Delete\":\"false\",\"m2_f1\":\"" + eBase.encode(dr["xm"].ToString()) + "\",\"m2_f2\":\"" + eBase.encode(dr["xb"].ToString()) + "\",\"m2_f3\":\"" + eBase.encode(dr["dh"].ToString()) + "\"}");
                        i++;
                    }
                    sb.Append("]}");
                    _json = sb.ToString();
                }
                return _json;
            }
        }
        public string UserArea = "System";
        protected void Page_Load(object sender, EventArgs e)
        {
            Type type = HttpContext.Current.Handler.GetType();//CurrentHandler
            System.Reflection.FieldInfo fi = type.GetField("UserArea");
            if (fi != null) UserArea = fi.GetValue(Activator.CreateInstance(type)).ToString(); 

            act = eParameters.QueryString("act");
            if(act.Length==0) act = eParameters.Request("act").ToLower();

            if (act.Length == 0) return;
            parentID = eParameters.QueryString("id");
            eUser user = new eUser(UserArea);
            eTable etb;
            switch (act)
            {
                case "save":
                    #region 保存
                    string jsonstr = eParameters.Form("eformdata_" + parentModelID);
                    eJson json = new eJson(jsonstr);
                    json.Convert = true;
                    json = json.GetCollection("eformdata_" + parentModelID).GetCollection()[0];

                    json = json.GetCollection("eformdata_" + modelid);
                    foreach (eJson jrow in json.GetCollection())
                    {
                        string _ID = jrow.GetValue("ID");
                        string _Delete = jrow.GetValue("Delete");

                        string _xm = jrow.GetValue("m2_f1");
                        string _xb = jrow.GetValue("m2_f2");
                        string _dh = jrow.GetValue("m2_f3");
                        etb = new eTable("Demo_Customs_PartMore", user);
                        if (_Delete.ToLower() == "true")
                        {
                            etb.Where.Add("PartMoreID='" + _ID + "'");
                            etb.Delete();
                        }
                        else
                        {
                            etb.Fields.Add("XM", _xm);
                            etb.Fields.Add("XB", _xb);
                            etb.Fields.Add("DH", _dh);
                            if (_ID.Length == 0) //添加
                            {
                                etb.Fields.Add("CustomID", parentID);
                                etb.Add();
                            }
                            else //修改
                            {
                                etb.Where.Add("PartMoreID='" + _ID + "'");
                                etb.Update();
                            }
                        }
                    }
                    #endregion
                    break;
                case "del":
                    etb = new eTable("Demo_Customs_PartMore", user);
                    etb.Where.Add("CustomID='" + parentID + "'");
                    etb.Delete();
                    break;
            }
        }

    }
}