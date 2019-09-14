<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Export.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Export" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<%@ Import Namespace="EKETEAM.Data" %>
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
"<table id=\"eDataTable_Export\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" wi6dth=\"100%\">" +
"<thead>" +
"<tr>" +
"<td width=\"70\">显示" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(149);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"180\">列名</td>" +
"<td width=\"180\">编码</td>" +
"<td width=\"60\">宽(PX)</td>" +
"<td width=\"60\">顺序</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelItemID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"32\"><input reload=\"true\" type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','showexport');\"" + (Eval("showexport").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td>"+ Eval("MC") + "</td>" +
"<td>"+ Eval("Code") + "</td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+  (Eval("ExportWidth").ToString()=="0" ? "" : Eval("ExportWidth").ToString()) + "\" value=\""+  (Eval("ExportWidth").ToString()=="0" ? "" : Eval("ExportWidth").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','exportwidth');\"></td>" +
//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("ExportOrder").ToString()=="999999" ? "" : Eval("ExportOrder").ToString()) + "\" oldvalue=\""+ (Eval("ExportOrder").ToString()=="999999" ? "" : Eval("ExportOrder").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','exportorder');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>
</body>
</html>