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

public class Upload : IHttpHandler, IReadOnlySessionState
{
    //private String savePath = "../../upload/temp/";//文件保存目录路径
    private String saveUrl = "upload/temp/";//文件保存目录URL
    private String fileTypes = "swf,flv,f4v,mp4";//定义允许上传的文件扩展名
    private int maxSize = 50 * 1024 * 1024;//1000000;//50M,最大文件大小
	private HttpContext context;
	public void ProcessRequest(HttpContext context)
	{       
		this.context = context;
        if (context.Request.UrlReferrer == null) context.Response.End();
        if (context.Request.Url.Host.ToLower() != context.Request.UrlReferrer.Host.ToLower() || context.Request.Url.Port != context.Request.UrlReferrer.Port) context.Response.End();
        if (context.Request.QueryString["path"] != null) saveUrl = context.Request.QueryString["path"].ToString() + saveUrl;


		HttpPostedFile flaFile = context.Request.Files["flaFile"];
		if (flaFile== null) showError("请选择文件。");
        String dirPath = context.Server.MapPath("~/");
        dirPath += "upload\\temp\\";
		if (!Directory.Exists(dirPath))   Directory.CreateDirectory(dirPath);//showError("上传目录不存在。");
		String fileName = flaFile.FileName;
		String fileExt = Path.GetExtension(fileName).ToLower();
		ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));
		if (flaFile.InputStream == null || flaFile.InputStream.Length > maxSize) showError("上传文件大小超过限制。");
		if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1) showError("上传文件扩展名是不允许的扩展名。");
        String newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
		String filePath = dirPath + newFileName;
        try
        {
            flaFile.SaveAs(filePath);
        }
        catch// (Exception ex)
        {
            showError("保存附件出错。");
        } 


		String fileUrl = saveUrl + newFileName;
		
		Hashtable hash = new Hashtable();
        hash["errcode"] = 0;
		hash["url"] = fileUrl;
		//context.Response.AddHeader("Content-Type", "text/html; charset=GB2312");
		//context.Response.Write(JsonMapper.ToJson(hash));
		//context.Response.End();
        //////////////////////////////////////////////////////////////////////////////////////////////////
        // 插入附件到kindeditor中
        string id = context.Request["id"].Trim();           //kindeditor控件的id
        string url = fileUrl.Trim();                        //保存文件的相对路径
        //string title = Path.GetFileName(fileName).Trim();   //文件名称（原名陈）
        //string ext = fileExt.Substring(1).ToLower().Trim(); //文件后缀名

	    string w = context.Request["flaWidth"].Trim();
	    string h = context.Request["flaHeight"].Trim();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        context.Response.Charset = "UTF-8";
        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<title>Insert Accessory</title>");
        sb.Append("<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
        sb.Append("</head>");
        sb.Append("<body>");
        sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"newflash\"].insert(\"" + id + "\", \"" + url + "\",\"" + w + "\",\"" + h + "\");</script>");
        sb.Append("</body>");
        sb.Append("</html>");
        context.Response.Write(sb.ToString());
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
		get
		{
			return true;
		}
	}
}
