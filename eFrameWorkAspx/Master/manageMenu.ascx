<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageMenu.ascx.cs" Inherits="eFrameWork.Master.manageMenu" %>
<a href="Default.aspx"<% =(aspxfile.IndexOf("default.aspx") > -1 ? " class=\"cur\"" :"") %>>首页</a>
<a href="Models.aspx"<% =((aspxfile.IndexOf("models.aspx") > -1 || aspxfile.IndexOf("modelitems") > -1 || aspxfile.IndexOf("modelimport.aspx") > -1) ? " class=\"cur\"" :"") %>>模块</a>
<a href="Menus.aspx"<% =(aspxfile.IndexOf("menus.aspx") > -1 ? " class=\"cur\"" :"") %>>菜单</a>
<a href="Roles.aspx"<% =(aspxfile.IndexOf("roles.aspx") > -1 ? " class=\"cur\"" :"") %>>角色</a>
<a href="Users.aspx"<% =(aspxfile.IndexOf("users.aspx") > -1 ? " class=\"cur\"" :"") %>>用户</a>
<a href="DataViews.aspx"<% =(aspxfile.IndexOf("dataviews.aspx") > -1 ? " class=\"cur\"" :"") %>>视图</a>
<a href="DataContents.aspx"<% =(aspxfile.IndexOf("datacontents.aspx") > -1 ? " class=\"cur\"" :"") %>>文本</a>
<a href="AllDomain.aspx"<% =(aspxfile.IndexOf("alldomain.aspx") > -1 ? " class=\"cur\"" :"") %>>授权</a>
<a href="Cache.aspx"<% =(aspxfile.IndexOf("cache.aspx") > -1 ? " class=\"cur\"" :"") %>>缓存</a>
<a href="ToKens.aspx"<% =(aspxfile.IndexOf("tokens.aspx") > -1 ? " class=\"cur\"" :"") %>>令牌</a>
<a href="TableInfo.aspx"<% =(aspxfile.IndexOf("tableinfo.aspx") > -1 ? " class=\"cur\"" :"") %>>结构</a>
<a href="LoginOut.aspx">退出登录</a>