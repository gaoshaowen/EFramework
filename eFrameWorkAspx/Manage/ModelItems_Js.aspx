<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Js.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Js" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<asp:Repeater id="Rep" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"30\" align=\"center\"><a title=\"添加动作\" href=\"javascript:;\" onclick=\"addAction(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"105\">名称</td>" +
"<td width=\"105\">编码</td>" +
"<td>SQL</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr>" +
"<td height=\"26\" align=\"center\"><a title=\"删除动作\" href=\"javascript:;\" onclick=\"delAction(this,'" + Eval("ActionID").ToString() + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +
"<td><input type=\"text\" value=\""+ Eval("MC").ToString() + "\" oldvalue=\""+ Eval("MC").ToString() + "\" class=\"edit\" onBlur=\"setAction(this,'" + Eval("ActionID") + "','mc');\"></td>" +
"<td><input type=\"text\" value=\""+ Eval("Action").ToString() + "\" oldvalue=\""+ Eval("Action").ToString() + "\" class=\"edit\" onBlur=\"setAction(this,'" + Eval("ActionID") + "','action');\"></td>" +
"<td><input type=\"text\" value=\""+ Eval("SQL").ToString() + "\" oldvalue=\""+ Eval("SQL").ToString() + "\" class=\"edit\" onBlur=\"setAction(this,'" + Eval("ActionID") + "','sql');\"></td>" +
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</table>"%></footertemplate>
</asp:Repeater>
</body>
</html>