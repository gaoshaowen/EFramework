<%@ webhandler Language="C#" class="getData" %>
using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using System.Configuration;
using EKETEAM.Data;
using EKETEAM.UserControl;
using EKETEAM.FrameWork;

public class getData : IHttpHandler
{

	public void ProcessRequest(HttpContext context)
	{
        context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
        if (context.Request.UrlReferrer == null)
        {
            context.Response.Write("[]");
            context.Response.End();
        }
        if (context.Request.Url.Host.ToLower() != context.Request.UrlReferrer.Host.ToLower() || context.Request.Url.Port != context.Request.UrlReferrer.Port)
        {
            context.Response.Write("[]");
            context.Response.End();
        }
        string ViewID = eParameters.QueryString("ViewID");
        if (ViewID.Length == 0)
        {
            context.Response.Write("[]");
            context.Response.End();
        }
        eDataView dv = new eDataView();
        dv.DataID = ViewID;
            
        string html = dv.getControlHTML();
        if (html.Length == 0) html = "[]";
        context.Response.Write(html);
	}
    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
