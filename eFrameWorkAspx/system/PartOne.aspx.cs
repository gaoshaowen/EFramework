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
    public partial class PartOne : System.Web.UI.Page
    {
        public string dz = "";
        public string gddh = "";
        public string act = "";
        private string parentModelID = "9e9496ac-7a22-419d-95da-29507ecebe9a";
        private string parentID = "";
        public string UserArea = "System";
        protected void Page_Load(object sender, EventArgs e)
        {
            Type type = HttpContext.Current.Handler.GetType();//CurrentHandler
            System.Reflection.FieldInfo fi = type.GetField("UserArea");
            if (fi != null) UserArea = fi.GetValue(Activator.CreateInstance(type)).ToString(); 

            act = eParameters.QueryString("act");
            if (act.Length == 0) act = eParameters.Request("act").ToLower();
            if (act.Length == 0) return;
            parentID = eParameters.QueryString("id");

            eUser user = new eUser(UserArea);
            switch (act)
            {
                case "save":
                    #region 保存
                    eTable tb = new eTable("Demo_Customs_PartOne", user);
                    string json = eParameters.Form("eformdata_" + parentModelID);
                    eJson jmodel = new eJson(json);
                    jmodel.Convert = true;
                    jmodel = jmodel.GetCollection("eformdata_" + parentModelID);
                    eJson jrow = jmodel.Collection[0];
                    tb.Fields.Add("DZ", jrow.GetValue("ma_f1"));
                    tb.Fields.Add("gddh", jrow.GetValue("ma_f2"));
                    string tmp = jrow.GetValue("id");

                    if(tmp.Length >0) parentID=tmp;


                    string id = eOleDB.getValue("select PartOneID from Demo_Customs_PartOne where CustomID='" + parentID + "' and delTag=0");
                    if (id.Length == 0)
                    {
                        tb.Fields.Add("CustomID", parentID);
                        tb.Add();
                    }
                    else
                    {
                        tb.Where.Add("CustomID='" + parentID + "'");
                        tb.Update();
                    }
                    #endregion
                    break;
                case "del":
                    eTable etb = new eTable("Demo_Customs_PartOne", user);
                    etb.Where.Add("CustomID='" + parentID + "'");
                    etb.Delete();
                    break;
                default:
                    #region 读取
                    DataTable dt = eOleDB.getDataTable("select dz,gddh from Demo_Customs_PartOne where CustomID='" + parentID + "' and delTag=0");
                    if (dt.Rows.Count > 0)
                    {
                        dz = dt.Rows[0]["DZ"].ToString();
                        gddh = dt.Rows[0]["GDDH"].ToString();
                    }
                    #endregion
                    break;
            }
        }
    }
}