<%@ webhandler Language="C#" class="Upload" %>
using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Web.SessionState;
using EKETEAM;
using EKETEAM.FrameWork;
using EKETEAM.Data;

public class Upload : IHttpHandler, IReadOnlySessionState
{
    //private String savePath = "../../upload/temp/";             //文件保存目录-物理路径
    private String saveUrl = "upload/temp/";                    //文件URL
    private String fileTypes = "gif,jpg,jpeg,png,bmp,tif";      //定义允许上传的文件扩展名
    private int maxSize = 50 * 1024 * 1024;                     //1000000;//50M,最大文件大小
	private HttpContext context;
	public void ProcessRequest(HttpContext context)
	{
        int MaxWidth = 0;
		this.context = context;
        if (context.Request.UrlReferrer == null) context.Response.End();
        if (context.Request.Url.Host.ToLower() != context.Request.UrlReferrer.Host.ToLower() || context.Request.Url.Port != context.Request.UrlReferrer.Port) context.Response.End();
        
        
        if (context.Request.QueryString["path"] != null) saveUrl = context.Request.QueryString["path"].ToString() + saveUrl;
        if (context.Request.QueryString["MaxWidth"] != null) MaxWidth = Convert.ToInt32(context.Request.QueryString["MaxWidth"]);       

		HttpPostedFile imgFile = context.Request.Files["imgFile"];
		if (imgFile == null) showError("请选择文件。");


        

        String dirPath = context.Server.MapPath("~/");
        dirPath += "upload\\temp\\";
		if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);//showError("上传目录不存在。");
		String fileName = imgFile.FileName;
		String fileExt = Path.GetExtension(fileName).ToLower();
		ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

		if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize) showError("上传文件大小超过限制。");
		if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1) showError("上传文件扩展名是不允许的扩展名。");


        String newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
		String filePath = dirPath + newFileName;
        //string tmp_width = "800";// ConfigurationManager.AppSettings["Image_MaxWidth"];
        //string tmp_width = ConfigurationManager.AppSettings["Image_MaxWidth"];
        //int maxWidth=715;
        //if (tmp_width.Length > 0) maxWidth = Convert.ToInt32(tmp_width);
		imgFile.SaveAs(filePath);

        newFileName = ePicture.AutoHandle(filePath, MaxWidth);
        string newExt = fileExt.Replace(".","");

        if (fileExt == ".bmp" || fileExt == ".tif" || fileExt == ".jpeg" || fileExt == ".png")
        {
            //newExt = "jpg";
            //newFileName = newFileName.ToLower().Replace(fileExt, ".jpg");
            //Picture.SaveToJPG(filePath);
            
            //filePath = filePath.ToLower().Replace(fileExt, ".jpg");
        }

       // Picture.ToWidth(filePath, maxWidth);
        //ekeImage.CreateThumbs(filePath, "W", 0, 0, maxWidth, 600, newExt);

	    String fileUrl = saveUrl + newFileName ;


        eJson json = new eJson();
        json.Add("errcode", "0");
        json.Add("url", fileUrl);
        
		context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(json.ToString());
		context.Response.End();

	}
	private void showError(string message)
	{
        eJson json = new eJson();
        json.Add("errcode", "1");
        json.Add("message", message);
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(json.ToString());
        context.Response.End();       
		
	}
	public bool IsReusable
	{
        get { return true; }
    }
}
