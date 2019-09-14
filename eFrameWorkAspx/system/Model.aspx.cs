using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.Data;
using EKETEAM.FrameWork;
using EKETEAM.UserControl;

namespace eFrameWork.system
{
    public partial class Model : System.Web.UI.Page
    {
        public string ModelID = eParameters.Request("modelid");
        public eModel model;
        public string UserArea = "System";
        public bool Ajaxget = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            eUser user = new eUser(UserArea);
            model = new eModel(ModelID, user);
            //model.Ajax = true; //默认为false

            switch (model.Action.Value)
            {
                case "delmore": //批量删除
                    string ids = eParameters.QueryString("ids");
                    ids = "'" + ids.Replace(",", "','") + "'";
                    eTable etb = new eTable(model.eForm.TableName, user);
                    etb.Where.Add(model.eForm.primaryKey + " in (" + ids + ")");
                    etb.DeleteTrue();
                    Response.Redirect(Request.UrlReferrer.PathAndQuery, true);
                    break;
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
                case "print":
                    eBase.Write(model.getPrintHTML());
                    eBase.End();
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
                case "export":
                    model.ExportExcel();
                    break;
                case "getrole":
                    string roleid = eParameters.QueryString("roleid");
                    DataTable rolePower = eBase.getUserPowerDefault(roleid, "","");
                    eJson json = new eJson(rolePower);
                    json.Convert = true;
                    eBase.Write(json.ToString());
                    Response.End();
                    break;
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = model.ModelInfo["mc"].ToString() + " - " + eConfig.getString("systemName");
            }
            lit = (Literal)Master.FindControl("LitJavascript");
            if (lit != null && model.Javasctipt.Length>0)
            {
                lit.Text = model.Javasctipt;
            }
        }
    }
}