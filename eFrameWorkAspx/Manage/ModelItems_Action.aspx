<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Action.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Action" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<asp:Repeater id="Rep" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"margin-bottom:8px;\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"35\" align=\"center\"><a title=\"添加动作\" href=\"javascript:;\" onclick=\"addAction(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"105\">名称" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(124);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"105\">编码" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(125);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td>SQL" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(126);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr>" +
"<td height=\"26\" align=\"center\"><a title=\"删除动作\" href=\"javascript:;\" onclick=\"delAction(this,'" + Eval("ActionID") + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("MC").ToString() + "\" oldvalue=\""+ Eval("MC").ToString() + "\" onBlur=\"setAction(this,'" + Eval("ActionID") + "','mc');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("Action").ToString() + "\" oldvalue=\""+ Eval("Action").ToString() + "\" onBlur=\"setAction(this,'" + Eval("ActionID") + "','action');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("SQL").ToString() + "\" oldvalue=\""+ Eval("SQL").ToString() + "\" onBlur=\"setAction(this,'" + Eval("ActionID") + "','sql');\"></td>" +
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</table>"%></footertemplate>
</asp:Repeater>
</body>
</html>
