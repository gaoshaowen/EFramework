using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Plugins
{
    public partial class ProUpload : System.Web.UI.Page
    {
        public string formhost = "";
        private bool writeLog = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string accUrl = eConfig.getString("AccessorysURL");
            #region 安全性检查
            //1.WebAPI用户放行
            //2.同一来源放行
            //3.来源被授权时放行
            if (Request.Headers["auth"] != null) //WebAPI访问
            {
                string auth = Request.Headers["auth"].ToString();
                eToken token = new eToken(auth);
                eUser user = new eUser(token);
            }
            else
            {
                if (Request.UrlReferrer == null) //无来源页面
                {
                    eJson ErrJson = new eJson();
                    ErrJson.Add("errcode", "1012");
                    ErrJson.Add("message", "访问未被许可!");
                    eBase.WriteJson(ErrJson);
                }
                else
                {
                    if (Request.Url.Host.ToLower() != Request.UrlReferrer.Host.ToLower() && accUrl.ToLower().IndexOf(Request.UrlReferrer.Host.ToLower()) == -1) //不是同一站点访问
                    {
                        DataRow[] rows = eBase.a_eke_sysAllowDomain.Select("Domain='" + Request.UrlReferrer.Host + "'");
                        if (rows.Length == 0)
                        {
                            eJson json = new eJson();
                            json.Add("domain", Request.UrlReferrer.Host);

                            eTable tb = new eTable("a_eke_sysErrors");
                            tb.Fields.Add("URL", Request.UrlReferrer.AbsoluteUri);
                            tb.Fields.Add("Message", "未授权访问!");
                            tb.Fields.Add("StackTrace", json.ToString());
                            tb.Add();

                            eJson ErrJson = new eJson();
                            ErrJson.Add("errcode", "1012");
                            ErrJson.Add("message", "访问未被许可!");
                            eBase.WriteJson(ErrJson);
                        }
                    }
                }
            }
            #endregion
            if (Request.UrlReferrer != null)
            {
                if (Request.UrlReferrer.Host.ToLower() != Request.Url.Host.ToLower())
                {
                    formhost = Request.UrlReferrer.Host.ToString();
                }
            }
            int PictureMaxWidth = 0;           
            if (Request.QueryString["PictureMaxWidth"] != null) PictureMaxWidth = Convert.ToInt32(Request.QueryString["PictureMaxWidth"]);
            if (Request.QueryString["MaxWidth"] != null) PictureMaxWidth = Convert.ToInt32(Request.QueryString["MaxWidth"]);

            int ThumbWidth = 0;
            if (Request.QueryString["ThumbWidth"] != null) ThumbWidth = Convert.ToInt32(Request.QueryString["ThumbWidth"]);
            string dirpath = Server.MapPath("~/");
            #region 编辑器上传文件
            if (Request.QueryString["postdata"] != null)
            {
                string postdata = Request.QueryString["postdata"].ToString();
                postdata = HttpUtility.UrlDecode(postdata);
                postdata = postdata.Replace("0x2f", "/").Replace("0x2b", "+").Replace("0x20", " ");
                Response.Write(postdata);
                Response.End();
            }
            if (Request.QueryString["type"] != null)
            {
                #region 附件上传
                if (Request.QueryString["type"].ToLower() == "file")
                {
                    dirpath += "upload\\temp\\";
                    eJson json = new eJson();
                    json.Convert = true;
                    json.Add("errcode", "0");
                    json.Add("message", "请求成功!");


                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile f = Request.Files[i];
                        int pos = f.FileName.LastIndexOf(".");
                        string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();

                        String fileExt = Path.GetExtension(f.FileName).ToLower();
                        string filename = eBase.GetFileName() + postFileName;
                        string pathname = dirpath + filename;
                        while (File.Exists(pathname))
                        {
                            filename = eBase.GetFileName() + postFileName;
                            pathname = dirpath + filename;
                        }
                        if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                        f.SaveAs(pathname);
                        eFileInfo finfo = new eFileInfo(filename);
                        filename = eBase.getBaseURL() + "upload/temp/" + filename;                        
                        eJson js = new eJson();
                        js.Add("name", f.FileName);
                        js.Add("url", filename);
                        json.Add("files", js);
                    }
                    // eBase.WriteJson(json);//IE解析有问题：文档的顶层无效
                    Response.Clear();
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 图片上传
                string allExt = ".gif.jpg.jpeg.bmp.png";
                if (Request.QueryString["type"].ToLower() == "image")
                {
                    if (Request.Files.Count == 0) showError("请选择文件!");
                    dirpath += "upload\\temp\\";
                    #region bak
                    /*
                    HttpPostedFile f = Request.Files["imgFile"];
                    if (f == null) showError("请选择文件。");
                    int pos = f.FileName.LastIndexOf(".");
                    string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();

                    String fileExt = Path.GetExtension(f.FileName).ToLower();
                    string filename = eBase.GetFileName() + postFileName;
                    string pathname = dirpath + filename;
                    while (File.Exists(pathname))
                    {
                        filename = eBase.GetFileName() + postFileName;
                        pathname = dirpath + filename;
                    }
                    if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                    f.SaveAs(pathname);

                    filename = ePicture.AutoHandle(pathname, PictureMaxWidth);
                    //filename = "../upload/temp/" + filename;
                    filename = eBase.getBaseURL() + "upload/temp/" + filename;
                    //if (fileExt == ".bmp" || fileExt == ".tif" || fileExt == ".jpeg" || fileExt == ".png")

                    eJson json = new eJson();
                    json.Add("errcode", "0");
                    json.Add("url", filename);
                    eBase.WriteJson(json);
                    */
                    #endregion


                    eJson json = new eJson();
                    json.Convert = true;
                    json.Add("errcode", "0");
                    json.Add("message", "请求成功!");

                    //string filenames = "";
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile f = Request.Files[i];
                        int pos = f.FileName.LastIndexOf(".");
                        string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();

                        String fileExt = Path.GetExtension(f.FileName).ToLower();
                        string filename = eBase.GetFileName() + postFileName;
                        string pathname = dirpath + filename;
                        while (File.Exists(pathname))
                        {
                            filename = eBase.GetFileName() + postFileName;
                            pathname = dirpath + filename;
                        }
                        if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                        f.SaveAs(pathname);
                        filename = ePicture.AutoHandle(pathname, PictureMaxWidth);
                        eFileInfo finfo = new eFileInfo(filename);
                        #region 缩略图
                        if (ThumbWidth > 0 && allExt.IndexOf("." + finfo.Extension.ToLower()) > -1)
                        {
                            pathname = dirpath + filename;
                            eFileInfo fi = new eFileInfo(dirpath + filename);
                            string thumbpathname = dirpath + fi.Name + "_thumb." + fi.Extension;
                            System.IO.File.Copy(pathname, thumbpathname);
                            ePicture.ToWidth(thumbpathname, ThumbWidth);

                            filename = eBase.getBaseURL() + "upload/temp/" + fi.Name + "_thumb." + fi.Extension;
                        }
                        else
                        {
                            filename = eBase.getBaseURL() + "upload/temp/" + filename;
                        }
                        #endregion
                        #region 日志
                        if (writeLog)
                        {
                            eTable etb = new eTable("a_eke_sysErrors");
                            etb.Fields.Add("Message", "upload");
                            eJson _json = new eJson();
                            _json.Add("filename", f.FileName);
                            _json.Add("size", f.ContentLength.ToString());
                            _json.Add("path", "upload/" + string.Format("{0:yyyy/MM/dd}", DateTime.Now) + "/" + filename);
                            etb.Fields.Add("StackTrace", _json.ToString());
                            etb.Add();
                        }
                        #endregion                        
                        
                        //if (filenames.Length > 0) filenames += ";";
                        //filenames += filename;
                        eJson js = new eJson(); 
                        js.Add("url", filename);
                        json.Add("files", js);
                    }
                    
                    //json.Add("url", HttpUtility.UrlEncode(filenames));
                    if (Request.Url.Host.ToLower() != Request.UrlReferrer.Host.ToLower())
                    {
                        string postdata = json.ToString().Replace("/", "0x2f").Replace("+", "0x2b").Replace(" ", "0x20");
                        postdata = HttpUtility.UrlEncode(postdata);
                        Response.Redirect("http://" + Request.UrlReferrer.Host + "/Plugins/ProUpload.aspx?postdata=" + postdata, true);
                    }
                    else
                    {
                        //eBase.WriteJson(json); //IE解析有问题：文档的顶层无效
                        Response.Clear();
                        Response.Write(json.ToString());
                        Response.End();
                    }
                    Response.End();

                }
                #endregion
                #region Flash上传
                if (Request.QueryString["type"].ToLower() == "flash")
                {
                    HttpPostedFile f = Request.Files["flaFile"];
                    if(f == null) showError("请选择文件。");
                    if(f.InputStream.Length == 0) showError("请选择文件!");// showError(f.InputStream.Length.ToString());

                    dirpath += "upload\\temp\\";
                    int pos = f.FileName.LastIndexOf(".");
                    string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();

                    String fileExt = Path.GetExtension(f.FileName).ToLower();
                    string filename = eBase.GetFileName() + postFileName;
                    string pathname = dirpath + filename;
                    while (File.Exists(pathname))
                    {
                        filename = eBase.GetFileName() + postFileName;
                        pathname = dirpath + filename;
                    }
                    if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                    f.SaveAs(pathname);

                    #region 日志
                    if (writeLog)
                    {
                        eTable etb = new eTable("a_eke_sysErrors");
                        etb.Fields.Add("Message", "upload");
                        eJson _json = new eJson();
                        _json.Add("filename", f.FileName);
                        _json.Add("size", f.ContentLength.ToString());
                        _json.Add("path", "upload/" + string.Format("{0:yyyy/MM/dd}", DateTime.Now) + "/" + filename);
                        etb.Fields.Add("StackTrace", _json.ToString());
                        etb.Add();
                    }
                    #endregion

                    //filename = ePicture.AutoHandle(pathname, PictureMaxWidth);
                    //filename = "../upload/temp/" + filename;
                    filename = eBase.getBaseURL() + "upload/temp/" + filename;
                    //if (fileExt == ".bmp" || fileExt == ".tif" || fileExt == ".jpeg" || fileExt == ".png")

                    string id = Request["id"].Trim();           //kindeditor控件的id
                    //string title = Path.GetFileName(fileName).Trim();   //文件名称（原名陈）
                    //string ext = fileExt.Substring(1).ToLower().Trim(); //文件后缀名

                    string w = Request["flaWidth"].Trim();
                    string h = Request["flaHeight"].Trim();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    Response.Charset = "UTF-8";
                    sb.Append("<html>");
                    sb.Append("<head>");
                    sb.Append("<title>Insert Flash</title>");
                    sb.Append("<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
                    sb.Append("</head>");
                    sb.Append("<body>");
                    sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"newflash\"].insert(\"" + id + "\", \"" + filename + "\",\"" + w + "\",\"" + h + "\");</script>");
                    sb.Append("</body>");
                    sb.Append("</html>");

                    if (Request.Url.Host.ToLower() != Request.UrlReferrer.Host.ToLower())
                    {
                        string postdata = "<script type=\"text/javascript\">parent.KE.plugin[\"newmedia\"].insert(\"" + id + "\", \"" + filename + "\",\"" + w + "\",\"" + h + "\");</script>";
                        postdata = postdata.Replace("/", "0x2f").Replace("+", "0x2b").Replace(" ", "0x20");
                        postdata = HttpUtility.UrlEncode(postdata);
                        Response.Redirect("http://" + Request.UrlReferrer.Host + "/Plugins/ProUpload.aspx?postdata=" + postdata, true);
                    }
                    else
                    {
                        Response.Write(sb.ToString());
                    }
                    Response.End();
                }
                #endregion
                #region 媒体上传
                if (Request.QueryString["type"].ToLower() == "media")
                {
                    HttpPostedFile f = Request.Files["flaFile"];
                    if (f == null) showError("请选择文件。");
                    if (f.InputStream.Length == 0) showError("请选择文件!");
                    dirpath += "upload\\temp\\";
                    int pos = f.FileName.LastIndexOf(".");
                    string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();

                    String fileExt = Path.GetExtension(f.FileName).ToLower();
                    string filename = eBase.GetFileName() + postFileName;
                    string pathname = dirpath + filename;
                    while (File.Exists(pathname))
                    {
                        filename = eBase.GetFileName() + postFileName;
                        pathname = dirpath + filename;
                    }
                    if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                    f.SaveAs(pathname);

                    #region 日志
                    if (writeLog)
                    {
                        eTable etb = new eTable("a_eke_sysErrors");
                        etb.Fields.Add("Message", "upload");
                        eJson _json = new eJson();
                        _json.Add("filename", f.FileName);
                        _json.Add("size", f.ContentLength.ToString());
                        _json.Add("path", "upload/" + string.Format("{0:yyyy/MM/dd}", DateTime.Now) + "/" + filename);
                        etb.Fields.Add("StackTrace", _json.ToString());
                        etb.Add();
                    }
                    #endregion

                    //filename = ePicture.AutoHandle(pathname, PictureMaxWidth);
                    //filename = "../upload/temp/" + filename;
                    filename = eBase.getBaseURL() + "upload/temp/" + filename;
                    //if (fileExt == ".bmp" || fileExt == ".tif" || fileExt == ".jpeg" || fileExt == ".png")

                    string id = Request["id"].Trim();           //kindeditor控件的id
                    //string title = Path.GetFileName(fileName).Trim();   //文件名称（原名陈）
                    //string ext = fileExt.Substring(1).ToLower().Trim(); //文件后缀名

                    string w = Request["flaWidth"].Trim();
                    string h = Request["flaHeight"].Trim();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    Response.Charset = "UTF-8";
                    sb.Append("<html>");
                    sb.Append("<head>");
                    sb.Append("<title>Insert Media</title>");
                    sb.Append("<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
                    sb.Append("</head>");
                    sb.Append("<body>");
                    sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"newmedia\"].insert(\"" + id + "\", \"" + filename + "\",\"" + w + "\",\"" + h + "\");</script>");
                    sb.Append("</body>");
                    sb.Append("</html>");
                    Response.Write(sb.ToString());
                    Response.End();
                }
                #endregion
                #region 附件上传
                if (Request.QueryString["type"].ToLower() == "accessory")
                {
                    HttpPostedFile f = Request.Files["imgFile"];
                    if (f == null) showError("请选择文件。");
                    if (f.InputStream.Length == 0) showError("请选择文件!");
                    dirpath += "upload\\temp\\";
                    int pos = f.FileName.LastIndexOf(".");
                    string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();

                    String fileExt = Path.GetExtension(f.FileName).ToLower();
                    string filename = eBase.GetFileName() + postFileName;
                    string pathname = dirpath + filename;
                    while (File.Exists(pathname))
                    {
                        filename = eBase.GetFileName() + postFileName;
                        pathname = dirpath + filename;
                    }
                    if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                    f.SaveAs(pathname);

                    #region 日志
                    if (writeLog)
                    {
                        eTable etb = new eTable("a_eke_sysErrors");
                        etb.Fields.Add("Message", "upload");
                        eJson _json = new eJson();
                        _json.Add("filename", f.FileName);
                        _json.Add("size", f.ContentLength.ToString());
                        _json.Add("path", "upload/" + string.Format("{0:yyyy/MM/dd}", DateTime.Now) + "/" + filename);
                        etb.Fields.Add("StackTrace", _json.ToString());
                        etb.Add();
                    }
                    #endregion


                    //filename = ePicture.AutoHandle(pathname, PictureMaxWidth);
                    //filename = "../upload/temp/" + filename;
                    filename = eBase.getBaseURL() + "upload/temp/" + filename;
                    //if (fileExt == ".bmp" || fileExt == ".tif" || fileExt == ".jpeg" || fileExt == ".png")

                    string id = Request["id"].Trim();           //kindeditor控件的id
                    string title = Path.GetFileName(filename).Trim();   //文件名称（原名陈）
                    string ext = fileExt.Substring(1).ToLower().Trim(); //文件后缀名
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    Response.Charset = "UTF-8";
                    sb.Append("<html>");
                    sb.Append("<head>");
                    sb.Append("<title>Insert Accessory</title>");
                    sb.Append("<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
                    sb.Append("</head>");
                    sb.Append("<body>");
                    sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"accessory\"].insert(\"" + id + "\", \"" + filename + "\",\"" + title + "\",\"" + ext + "\");</script>");
                    sb.Append("</body>");
                    sb.Append("</html>");

                    if (Request.Url.Host.ToLower() != Request.UrlReferrer.Host.ToLower())
                    {
                        string postdata = "<script type=\"text/javascript\">parent.KE.plugin[\"accessory\"].insert(\"" + id + "\", \"" + filename + "\",\"" + title + "\",\"" + ext + "\");</script>";
                        postdata = postdata.Replace("/", "0x2f").Replace("+", "0x2b").Replace(" ", "0x20");
                        postdata = HttpUtility.UrlEncode(postdata);
                        Response.Redirect("http://" + Request.UrlReferrer.Host + "/Plugins/ProUpload.aspx?postdata=" + postdata, true);
                    }
                    else
                    {
                        Response.Write(sb.ToString());
                    }
                    Response.End();
                }
                #endregion
            }
            #endregion
            if (Request.QueryString["act"] != null)
            {
                #region 获取大小
                if (Request.QueryString["act"].ToLower() == "getsize")
                {
                    string filename = Request.QueryString["file"].ToString();
                    int ow = 0;
                    int oh = 0;
                    if (filename.ToLower().IndexOf("http") > -1)
                    {
                        filename = filename.Replace(eBase.getBaseURL(), "");
                    }
                    string[] arr = filename.Split(".".ToCharArray());
                    string ext = arr[arr.Length - 1].ToLower();
                    string allExt = ".gif.jpg.jpeg.bmp.png";
                    if (allExt.IndexOf(ext) > -1)
                    {
                        filename = dirpath + filename.Replace("../", "").Replace("/", "\\");
                        if (System.IO.File.Exists(filename))
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(filename);
                                ow = img.Width;
                                oh = img.Height;
                                img.Dispose();
                            }
                            catch { }
                        }
                    }
                    eJson json = new eJson();
                    json.Add("width", ow.ToString());
                    json.Add("height", oh.ToString());
                    eBase.WriteJson(json);
                }
                #endregion
                #region 下载网络文件
                if (Request.QueryString["act"].ToLower() == "down")
                {
                    string file = Request.QueryString["file"].ToString();
                    string[] arr = file.Split(".".ToCharArray());
                    string ext = "." + arr[arr.Length - 1];

                    string virtualDir = eConfig.UploadPath();
                    string basePath = HttpContext.Current.Server.MapPath("~/");
                    basePath += virtualDir.Replace("/", "\\");
                    if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

                    string filename = eBase.GetFileName() + ext;
                    string savepath = basePath + filename;

                    eJson json = new eJson();
                    System.Net.WebClient wc = new System.Net.WebClient();
                    try
                    {
                        wc.DownloadFile(file, savepath);
                        wc.Dispose();
                        json.Add("url", eBase.getBaseURL() + virtualDir + filename);

                    }
                    catch
                    {
                        json.Add("url", file);
                    }

                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 删除正式文件
                if (Request.QueryString["act"].ToLower() == "deltrue")
                {
                    string filename = Request.QueryString["file"].ToString();
                    filename = Regex.Replace(filename, eBase.getBaseURL(), "", RegexOptions.IgnoreCase);
                    filename = dirpath + filename.Replace("../", "").Replace("/", "\\");
                    try
                    {
                        System.IO.File.Delete(filename);
                        System.IO.File.Delete(filename.Replace(".", "_sm."));
                    }
                    catch
                    {
                    }
                    Response.End();
                }
                #endregion
                #region 临时文件移动到正式文件夹下
                if (Request.QueryString["act"].ToLower() == "move")
                {
                    string file = Request.QueryString["file"].ToString();
                    file = Regex.Replace(file, eBase.getBaseURL(), "", RegexOptions.IgnoreCase);
                    string basePath = HttpContext.Current.Server.MapPath("~/");
                    string temppath = basePath + file.Replace("/", "\\");
                    eJson json = new eJson();
                    if (File.Exists(temppath) && file.ToLower().IndexOf("/temp/") > -1)
                    {
                        string[] arr = temppath.Split("\\".ToCharArray());
                        string filename = arr[arr.Length - 1];
                        string virtualDir = eConfig.UploadPath();
                        basePath += virtualDir.Replace("/", "\\");
                        if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);
                        string newpath = basePath + filename;
                        File.Move(temppath, newpath);
                        //eBase.Writeln("newpath1:" + virtualDir + filename);
                        json.Add("url", eBase.getBaseURL() + virtualDir + filename);
                    }
                    else
                    {
                        json.Add("url", file);
                    }

                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(json.ToString());
                    Response.End();
                }
                #endregion
                #region 上传完成
                if (Request.QueryString["act"].ToLower() == "finsh")
                {
                    if (Request.QueryString["sub"] != null) Response.Write("<script>﻿try{parent.document.getElementById('" + Request.QueryString["obj"].ToString() + "').value='" + Request.QueryString["file"].ToString() + "';}catch(e){}</script>");


                    Response.Write("<font color='#009900'>上传成功!</font><a style='line-height:22px;display:inline-block;margin-left:10px;margin-right:18px;text-decoration:none;' href='?act=del&obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "&file=" + Request.QueryString["file"].ToString() + "' onclick='return del();'><font color='#FF0000'>删除重新上传?</font></a>");
                    string filename = Request.QueryString["file"].ToString();
                    if (filename.ToLower().IndexOf("http") > -1)
                    {
                        filename = filename.Replace(eBase.getBaseURL(), "");
                    }
                    string[] arr = filename.Split(".".ToCharArray());
                    string ext = arr[arr.Length - 1].ToLower();
                    string allExt = ".gif.jpg.jpeg.bmp.png";
                    //eBase.Write(allExt.IndexOf(ext).ToString());
                    if (allExt.IndexOf(ext) > -1)
                    {
                        int ow = 0;
                        int oh = 0;
                        if (Request.QueryString["ow"] != null) ow = Convert.ToInt32(Request.QueryString["ow"].ToString());
                        if (Request.QueryString["oh"] != null) oh = Convert.ToInt32(Request.QueryString["oh"].ToString());
                        filename = dirpath + filename.Replace("../", "").Replace("/", "\\");
                        if (System.IO.File.Exists(filename))
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(filename);
                                ow = img.Width;
                                oh = img.Height;
                                img.Dispose();
                            }
                            catch { }
                        }
                        else
                        {
                            if (accUrl.Length > 0)
                            {
                                string url = accUrl + "Plugins/ProUpload.aspx?act=getsize&obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "&file=" + Request.QueryString["file"].ToString();
                                string result = eBase.getRequest(url);
                                if (result.StartsWith("{"))
                                {
                                    eJson json = new eJson(result);
                                    ow = Convert.ToInt32(json.GetValue("width"));
                                    oh = Convert.ToInt32(json.GetValue("height"));
                                }
                                
                            }
                        }
                        if (ow > 0)
                        {
                            Response.Write("<img src=\"" + eBase.getAbsolutePath() + "images/view.jpg\" width=\"12\" height=\"12\" style=\"cursor:pointer;\" alt=\"查看图片\" onclick=\"parent.viewImage('" + Request.QueryString["file"].ToString() + "'," + ow.ToString() + "," + oh.ToString() + ");\" align=\"absmiddle\" />");
                        }
                        /*
                    else
                    {
                        ow = 400;
                        oh = 300;
                        Response.Write("<img src=\"" + eBase.getAbsolutePath() + "images/view.jpg\" width=\"12\" height=\"12\" style=\"cursor:pointer;\" alt=\"查看图片\" onclick=\"parent.viewImage('" + Request.QueryString["file"].ToString() + "'," + ow.ToString() + "," + oh.ToString() + ");\" align=\"absmiddle\" />");
                    }
                    */
                    }
                }
                #endregion
                #region 删除临时文件
                if (Request.QueryString["act"].ToLower() == "del")
                {
                   
                    string filename = Request.QueryString["file"].ToString();
                    filename = Regex.Replace(filename, eBase.getBaseURL(), "", RegexOptions.IgnoreCase);
                    //filename = Server.MapPath(filename);
                    filename = dirpath + filename.Replace("../", "").Replace("/", "\\");

                  
                    //只删除临时文件，防止删除正式文件且不保存。
                    if (filename.ToLower().IndexOf("\\temp\\") > -1 && filename.ToLower().IndexOf("http:")==-1)
                    {
                        //System.IO.File.Exists
                        try
                        {
                            System.IO.File.Delete(filename);
                            System.IO.File.Delete(filename.Replace(".", "_sm."));
                            System.IO.File.Delete(filename.Replace("_thumb", ""));
                            
                        }
                        catch
                        {
                        }
                    }
                    if (filename.IndexOf("_thumb") > -1) Response.End();
                    if (accUrl.Length > 0)
                    {
                        string url = accUrl + "Plugins/ProUpload.aspx?act=del&obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "&file=" + Request.QueryString["file"].ToString();
                        string result = eBase.getRequest(url);
                        Response.Write("<script>﻿try{parent.document.getElementById('" + Request.QueryString["obj"].ToString() + "').value='';}catch(e){}\r\ndocument.location='" + accUrl + "Plugins/ProUpload.aspx?obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "';</script>");
                    }
                    else
                    {
                        Response.Write("<script>﻿try{parent.document.getElementById('" + Request.QueryString["obj"].ToString() + "').value='';}catch(e){}\r\ndocument.location='ProUpload.aspx?obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "';</script>");
                    }   
                    Response.End();
                }
                #endregion
            }
            if (Request.Form["act"] != null)
            {
                #region 保存文件
                HttpPostedFile f = imgFile.PostedFile;
                if (f.ContentLength > 0)
                {
                    dirpath += "upload\\temp\\";
                    int pos = f.FileName.LastIndexOf(".");
                    string postFileName = f.FileName.Substring(pos, f.FileName.Length - pos).ToLower();
                    //if (postFileName.IndexOf(".mp4") > -1) postFileName = ".webm";
                    if (1 == 1)//if (".gif.jpg.bmp.flv".IndexOf(postFileName) > -1)
                    {
                        string filename = eBase.GetFileName() + postFileName;
                        string pathname = dirpath + filename;
                        while (File.Exists(pathname))
                        {
                            filename = eBase.GetFileName() + postFileName;
                            pathname = dirpath + filename;
                        }
                        if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                        f.SaveAs(pathname);

                        filename = ePicture.AutoHandle(pathname, PictureMaxWidth);
                        int ow = 0;
                        int oh = 0;
                        string allExt = ".gif.jpg.jpeg.bmp.png";
                        if (allExt.IndexOf(postFileName.ToLower()) > -1)
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(pathname);
                                ow = img.Width;
                                oh = img.Height;
                                img.Dispose();
                            }
                            catch { }
                        }
                        #region 日志
                        if (writeLog)
                        {
                            eTable etb = new eTable("a_eke_sysErrors");
                            etb.Fields.Add("Message", "upload");
                            eJson _json = new eJson();
                            _json.Add("filename", f.FileName);
                            _json.Add("size", f.ContentLength.ToString());
                            _json.Add("path", "upload/" + string.Format("{0:yyyy/MM/dd}", DateTime.Now) + "/" + filename);
                            etb.Fields.Add("StackTrace", _json.ToString());
                            etb.Add();
                        }
                        #endregion

                        //filename = "../upload/temp/" + filename;
                        filename = eBase.getBaseURL() + "upload/temp/" + filename;
                        // OleDB.Execute("insert into a_eke_sysTemp (uid,path) values ('" + SystemClass.getAdminID() + "','" + filename.Replace("../", "") + "')");
                        
                        if (Request.Form["formhost"].ToString().Length > 0)
                        {
                            Response.Redirect("http://" + Request.Form["formhost"].ToString() + "/Plugins/ProUpload.aspx?act=finsh&sub=true&obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "&file=" + filename + "&ow=" + ow.ToString() + "&oh=" + oh.ToString(), true);
                        }
                        else
                        {
                            Response.Write("<script>try{eval(\"parent.document.getElementById('" + Request.QueryString["obj"].ToString() + "').value='" + filename + "';\")}catch(e){}</script>");
                            Response.Write("<script>document.location='?act=finsh&obj=" + Request.QueryString["obj"].ToString() + "&PictureMaxWidth=" + PictureMaxWidth.ToString() + "&file=" + filename + "';</script>");
                        }
                        Response.End();
                    }
                    /*
                else
                {
                    Response.Write("<script>alert('不支持的文件类型!');document.location='?obj=" + Request.QueryString["obj"].ToString() + "';</script>");
                    Response.End();
                }
                */
                }
                #endregion
            }
        }
        private void showError(string message)
        {
            eJson json = new eJson();
            json.Add("errcode", "1");
            json.Add("message", message);
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(json.ToString());
            Response.End();

        }
    }
}