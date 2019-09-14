using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Manage
{
    public partial class ModelImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            #region 安全性检查
            if (Request.UrlReferrer == null) Response.End();
            if (Request.Url.Host.ToLower() != Request.UrlReferrer.Host.ToLower() || Request.Url.Port != Request.UrlReferrer.Port) Response.End();
            #endregion
            #region 保存文件
            if (Request.Form["act"] != null)
            {
                HttpPostedFile f = imgFile.PostedFile;
                if (f.ContentLength > 0)
                {
                    string dirpath = Server.MapPath("~/") +"upload\\temp\\";
                    
                    int pos = f.FileName.LastIndexOf(".");
                    string Ext = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();
                    if (Ext.ToLower().IndexOf("efw") == -1)
                    {
                        Response.Write("<script>alert('文件格式不正确!');document.location='Models.aspx';</script>");
                        Response.End();
                    }

                    string filename = eBase.GetFileName() + Ext;
                    string pathname = dirpath + filename;
                    if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                    f.SaveAs(pathname);

                    string _json = eBase.ReadFile(pathname);

                    try
                    {
                        System.IO.File.Delete(pathname);
                    }
                    catch
                    {
                    }

                    eJson json = new eJson(_json);       
                    eJson model = json.GetCollection("a_eke_sysModels").Collection[0];
                    string file=model.GetValue("AspxFile");
                    string aspxFile = Server.MapPath("~/System/") + file + ".log";
                    if (json.IsValue("aspxFile"))
                    {
                        string text = json.GetValue("aspxFile");
                        eBase.WriteFile(aspxFile, text);
                    }
                    string csFile = Server.MapPath("~/System/") + file + ".cs.log";
                    if (json.IsValue("csFile"))
                    {
                        string text = json.GetValue("csFile");
                        eBase.WriteFile(csFile, text);
                    }
                    string desFile = Server.MapPath("~/System/") + file + ".designer.cs.log";
                    if (json.IsValue("desFile"))
                    {
                        string text = json.GetValue("desFile");
                        eBase.WriteFile(desFile, text);
                    }
                    eOleDB.ImportJson(_json,false);


                    Response.Write("<script>alert('导入成功!');document.location='Models.aspx';</script>");
                    Response.End();
          
                }

            }
            #endregion
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "模块 - " + eConfig.getString("manageName"); 
            }
        }
    }
}