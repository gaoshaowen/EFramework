<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Client.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Client" %>
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
"<td width=\"150\">编码</td>" +
"<td width=\"100\">列名</td>" +
"<td width=\"85\">验证类型</td>" +
"<td width=\"70\">最小长度</td>" +
"<td width=\"70\">最大长度</td>" +
"<td width=\"70\">最小值</td>" +
"<td width=\"70\">最大值</td>" +
"<td width=\"60\">必填</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"32\">" + Eval("Code") + "</td>" +
"<td>"+ Eval("MC") + "</td>" +
"<td><select oldvalue=\"" + Eval("datatype").ToString() + "\" onChange=\"setModelItem(this,'" + Eval("ModelItemID") + "','datatype');\" sty3le=\"width:100px;\">" + eBase.getClientType(Eval("datatype").ToString()) + "<select></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ (Eval("minlength").ToString()=="0" ? "" : Eval("minlength").ToString()) + "\" oldvalue=\""+ (Eval("minlength").ToString()=="0" ? "" : Eval("minlength").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','minlength');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ (Eval("maxlength").ToString()=="0" ? "" : Eval("maxlength").ToString()) + "\" oldvalue=\""+ (Eval("maxlength").ToString()=="0" ? "" : Eval("maxlength").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','maxlength');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ (Eval("minvalue").ToString()=="0" ? "" : Eval("minvalue").ToString()) + "\" oldvalue=\""+ (Eval("minvalue").ToString()=="0" ? "" : Eval("minvalue").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','minvalue');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ (Eval("maxvalue").ToString()=="0" ? "" : Eval("maxvalue").ToString()) + "\" oldvalue=\""+ (Eval("maxvalue").ToString()=="0" ? "" : Eval("maxvalue").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','maxvalue');\"></td>" +
"<td><input  type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','notnull');\"" + (Eval("notNull").ToString()=="True" ? " checked" : "") + " /></td>" +
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</table>"%></footertemplate>
</asp:Repeater>
</body>
</html>
