<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Data.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Data" %>
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
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"min-width:1620px;\">" +
"<thead>" +
"<tr>" +
"<td width=\"120\">编码</td>" +
"<td width=\"150\">列名</td>" +
"<td width=\"60\">表单Name</td>" +
"<td>绑定" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(121);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"150\">选项" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(122);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"150\">替换" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(123);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"32\">" + Eval("Code") + "</td>" +
"<td>"+ Eval("MC") + "</td>" +
"<td>" + Eval("frmName") + "</td>" +
"<td>"
%>
表 <select reload="true" onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','bindobject');" style="width:100px;">
	<option value="">无</option>
	<asp:Literal id="LitObjects" runat="server" />
	</select>
	行
	<input class="text" type="text" value="<%# (Eval("bindrows").ToString()=="0" ? "" : Eval("bindrows").ToString())%>"  style="width:60px;" onBlur="setModelItem(this,'<%# Eval("ModelItemID")%>','bindrows');" />
	值
	<select onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','bindvalue');" style="width:80px;">
	<option value="">无</option>
	<asp:Literal id="LitValue" runat="server" />
	</select>
	文本
	<select onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','bindtext');" style="width:80px;">
	<option value="">无</option>
	<asp:Literal id="LitText" runat="server" />
	</select>
	条件
	<input class="text" type="text" value="<%# Eval("bindconditions")%>" oldvalue="<%# Eval("bindconditions")%>" style="width:80px;" ondblclick="dblClick(this,'<%# Eval("MC")%>-条件');" onBlur="setModelItem(this,'<%# Eval("ModelItemID")%>','bindconditions');" />
	分组
	<input class="text" type="text" value="<%# Eval("bindgroupby")%>" oldvalue="<%# Eval("bindgroupby")%>" style="width:80px;" ondblclick="dblClick(this,'<%# Eval("MC")%>-分组');" onBlur="setModelItem(this,'<%# Eval("ModelItemID")%>','bindgroupby');" />
	排序
	<input class="text" type="text" value="<%# Eval("bindorderby")%>" oldvalue="<%# Eval("bindorderby")%>" style="width:80px;" ondblclick="dblClick(this,'<%# Eval("MC")%>-排序');" onBlur="setModelItem(this,'<%# Eval("ModelItemID")%>','bindorderby');" />
	自动加载
	 <select onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','bindauto');" style="width:40px;">
	<option value="1"<%# (Eval("bindauto").ToString()=="True" ? " selected" : "")%>>是</option>
	<option value="0"<%# (Eval("bindauto").ToString()=="False" ? " selected" : "")%>>否</option>
	</select><br />
	SQL取值：
	<input class="text" type="text" value="<%# Eval("BindSQL")%>" oldvalue="<%# Eval("BindSQL")%>" style="width:500px;" ondblclick="dblClick(this,'<%# Eval("MC")%>-SQL取值');" onBlur="setModelItem(this,'<%# Eval("ModelItemID")%>','bindsql');" />
	<%if(33==44){%>
	加载
	<input class="text" type="text" value="<%# Eval("BindDataPrar")%>" oldvalue="<%# Eval("BindDataPrar")%>"  style="width:80px;" onBlur="setModelItem(this,'<%# Eval("ModelItemID")%>','binddataprar');" />
	<%}%>
<%#
"</td>" +
"<td><input class=\"text\" reload=\"true\" id=\"data_options_" +  Eval("ModelItemID").ToString().Replace("-","") + "\" jsonformat=\"[{&quot;text&quot;:&quot;文本&quot;,&quot;value&quot;:&quot;text&quot;},{&quot;text&quot;:&quot;值&quot;,&quot;value&quot;:&quot;value&quot;}]\" style=\"display:none;\" type=\"text\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("options").ToString()) + "\" oldvalue=\""+ System.Web.HttpUtility.HtmlEncode(Eval("options").ToString())+ "\" titlea=\"格式：文本，值；文本，值\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-选项');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','options');\" />"+
"<img src=\"images/jsonedit.jpg\" align=\"absmiddle\" style=\"cursor:pointer;margin-right:5px;\" onclick=\"Json_Edit('data_options_" +  Eval("ModelItemID").ToString().Replace("-","") + "');\">" + 
getJsonText(Eval("options").ToString(),"text") +
"</td>" +
"<td><input class=\"text\" reload=\"true\" id=\"data_replace_" +  Eval("ModelItemID").ToString().Replace("-","") + "\" jsonformat=\"[{&quot;text&quot;:&quot;文本&quot;,&quot;value&quot;:&quot;text&quot;},{&quot;text&quot;:&quot;值&quot;,&quot;value&quot;:&quot;value&quot;}]\" style=\"display:none;\" type=\"text\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("replacestring").ToString()) + "\" oldvalue=\""+ System.Web.HttpUtility.HtmlEncode(Eval("replacestring").ToString()) + "\"  titlea=\"格式：文本，值；文本，值\" ondblclick=\"dblClick(this,'"+ Eval("MC") + "-替换');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','replacestring');\" />"+
"<img src=\"images/jsonedit.jpg\" align=\"absmiddle\" style=\"cursor:pointer;margin-right:5px;\" onclick=\"Json_Edit('data_replace_" +  Eval("ModelItemID").ToString().Replace("-","") + "');\">" + 
getJsonText(Eval("replacestring").ToString(),"text") +
"</td>" +
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</table>"%></footertemplate>
</asp:Repeater>
</body>
</html>
