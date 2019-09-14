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
    public partial class Accessorys : System.Web.UI.Page
    {
        public string sfz = "";
        public string fkb = "";
        public string byz = "";

        public string act = "";
        private string parentModelID = "7254820c-df55-4655-be0a-f14527556493";
        private string parentID = "";
        public string UserArea = "System";

        public eForm eform;
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
            eform = new eForm("Demo_Accessorys", user);
            eform.AutoRedirect = false; //子模块，记得要关闭自动跳转
            eform.AddControl(eFormControlGroup);
            switch (act)
            {
                case "save":
                    #region 保存
                    string json = eParameters.Form("eformdata_" + parentModelID);
                    eJson jmodel = new eJson(json);
                    jmodel.Convert = true;
                    jmodel = jmodel.GetCollection("eformdata_" + parentModelID);
                    eJson jrow = jmodel.Collection[0];                   

                    string tmp = jrow.GetValue("id");
                    if(tmp.Length >0) parentID=tmp;
                    string id = eOleDB.getValue("select AccessoryID from Demo_Accessorys where DemoID='" + parentID + "' and delTag=0");
                    if(id.Length > 0) eform.ID = id;

                    ma_f1.Value = jrow.GetValue("ma_f1");
                    ma_f2.Value = jrow.GetValue("ma_f2");
                    ma_f3.Value = jrow.GetValue("ma_f3");

                    if (id.Length == 0)
                    {
                        eform.Fields.Add("DemoID", parentID);
                        eform.Add();
                    }
                    else
                    {
                        eform.Update();
                    }
                    #endregion
                    break;
                case "del":
                    eTable etb = new eTable("Demo_Accessorys", user);
                    etb.Where.Add("DemoID='" + parentID + "'");
                    etb.Delete();
                    break;
                default:
                    #region 读取Json
                    string accUrl = eConfig.getString("AccessorysURL");
                    string virtualPath = eBase.getVirtualPath();
                    DataTable dt = eOleDB.getDataTable("select sfz,fkb,byz from Demo_Accessorys where DemoID='" + parentID + "' and delTag=0");
                    if (dt.Rows.Count > 0)
                    {

                        string basepath = accUrl.Length > 0 ? accUrl : virtualPath;

                        if (act == "view" && dt.Rows[0]["sfz"].ToString().Length > 0) dt.Rows[0]["sfz"] = basepath + dt.Rows[0]["sfz"].ToString();
                        if (act == "view" && dt.Rows[0]["fkb"].ToString().Length > 0) dt.Rows[0]["fkb"] = basepath + dt.Rows[0]["fkb"].ToString();
                        if (act == "view" && dt.Rows[0]["byz"].ToString().Length > 0) dt.Rows[0]["byz"] = basepath + dt.Rows[0]["byz"].ToString();

                        eform.Data = dt.Rows[0];


                        ma_f1.Value = dt.Rows[0]["sfz"].ToString();
                        ma_f2.Value = dt.Rows[0]["fkb"].ToString();
                        ma_f3.Value = dt.Rows[0]["byz"].ToString();

                        sfz = basepath + dt.Rows[0]["sfz"].ToString();
                        fkb = basepath + dt.Rows[0]["fkb"].ToString();
                        byz = basepath + dt.Rows[0]["byz"].ToString();

                    }
                    #endregion
                    break;
            }
        }
    }
}