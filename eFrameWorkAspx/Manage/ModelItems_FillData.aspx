<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_FillData.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_FillData" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
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
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" widt5h=\"100%\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"180\">" + parent["mc"].ToString() +  (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(154);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"280\">" + me["mc"].ToString() + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(155);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr>" +
"<td height=\"26\">" + Eval("mc").ToString() + " (" + Eval("code").ToString() + ")</td>" +
"<td>"
%>
<select onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','FillModelItemID');" style="width:270px;">
	<option value="NULL">无</option>
	<asp:Literal id="LitColumns" runat="server" />
</select>
<%#
"</td>" +
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</table>"%></footertemplate>
</asp:Repeater>
</body>
</html>