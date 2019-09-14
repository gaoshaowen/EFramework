using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.WebAPI
{
    public partial class Model : System.Web.UI.Page
    {
        public string ModelID = eParameters.Request("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            string auth = "";
            if (1 == 1)
            {
                if (Request.Headers["auth"] == null)
                {
                    eJson ErrJson = new eJson();
                    ErrJson.Add("errcode", "1");
                    ErrJson.Add("message", "未携带eToKen");
                    eBase.WriteJson(ErrJson);
                }
                auth = Request.Headers["auth"].ToString();

                #region 日志
                if (1 == 1)
                {
                    eTable etb = new eTable("a_eke_sysErrors");
                    etb.Fields.Add("Message", "auth");
                    etb.Fields.Add("StackTrace", auth);
                    etb.Add();
                }
                #endregion
            }
            else
            {
                auth = "06841095ADDB705B76053D24C19BF707ED46C19D50CD552953B29A5B52A40AAD35CD1D9B25C640205D2767CA2C4A97F0A2B98CC48242454E00040655734261D6BFBE7CED29CA8AF766721AEA61ED411113CA840E5959FBCC0002BAB64731467E8705BAB1267CE992C2785DAE905F84C8B54C352C3D87C5D20D2C09DE7DC3F542E2AC6A66D8D1021E4E58051851C86871";
            }
            


            eToken token = new eToken(auth);
            eUser user = new eUser(token);

            eModel model = new eModel(ModelID, user);

            model.Ajax = true;
            model.Mode = "WebAPI";
            switch (model.Action.Value)
            {
                case "":
                    model.WebAPIList();
                    break;
                case "edit":
                    model.WebAPIEdit();
                    break;
                case "view":
                    model.WebAPIView();
                    break;
                case "save":
                    model.WebAPISave();
                    break;
                case "del":
                    model.WebAPIDelete();
                    break;
            }
            eBase.End();
        }
    }
}