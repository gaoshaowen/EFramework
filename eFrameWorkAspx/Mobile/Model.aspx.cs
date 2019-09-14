using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;
using System.Text;

namespace eFrameWork.Mobile
{
    public partial class Model : System.Web.UI.Page
    {
      
        public string ModelID = eParameters.Request("modelid");
        public eModel model;
        private eUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new eUser("Mobile");
            user.Check();
            model = new eModel(ModelID, user);
            model.Mode = "mobile";

            switch (model.Action.Value)
            {
                case "":
                    LitBody.Text = model.getListHTML();
                    break;
                case "add":
                    LitBody.Text = model.getAddHTML();
                    break;
                case "edit":
                    LitBody.Text = model.getEditHTML();
                    break;
                case "copy":
                    LitBody.Text = model.getEditHTML();
                    break;
                case "view":
                    LitBody.Text = model.getViewHTML();
                    break;
                case "save":
                    model.Save();
                    break;
                case "del":
                    model.Delete();
                    break;
                case "addsub":
                    eBase.Write(model.getAddHTML());
                    eBase.End();
                    break;
                case "viewsub":
                    eBase.Write(model.getViewHTML());
                    eBase.End();
                    break;
            }
            
        }
        
    }
}