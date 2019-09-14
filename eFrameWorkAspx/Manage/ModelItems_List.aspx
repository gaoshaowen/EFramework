<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_List.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_List" %>
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
"<table id=\"eDataTable_List\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"margin-top:6px;\">" +
"<thead>" +
"<tr>" +
"<td width=\"110\">显示" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(98);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"130\">编码" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(97);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"70\">显示(M)" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(99);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"70\">表单Name</td>" +
"<td width=\"70\">表单ID</td>" +
"<td width=\"80\">宽度(PX)" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(100);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"70\">宽度(M)" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(101);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">开启排序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(102);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">拖动位置" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(103);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">拖动宽度" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(104);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td wid4th=\"200\">自定义查看" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(105);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
//"<td width=\"150\">默认排序</td>" +
//"<td width=\"150\">条件</td>" +
"<td width=\"85\">顺序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(106);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">" : "") + "</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelItemID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"32\"><input reload=\"true\" id=\"showlist_" + Eval("ModelItemID") + "\" type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','showlist');\"" + (Eval("showlist").ToString()=="True" ? " checked" : "") + " /><label for=\"showlist_" + Eval("ModelItemID") + "\">"+ Eval("MC") + "</label></td>" +
"<td>" +(Eval("Custom").ToString() == "True" && Eval("showlist").ToString()=="True" ? "<input class=\"text\" type=\"text\" value=\""+ Eval("CustomCode").ToString() + "\" oldvalue=\""+ Eval("CustomCode").ToString() + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','customcode');\">" : Eval("Code") )+ "</td>" +
"<td><input type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','mshowlist');\"" + (Eval("mshowlist").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td>" + (Eval("Custom").ToString() == "True" && Eval("showlist").ToString()=="True" ? "<input class=\"text\" type=\"text\" value=\""+ Eval("frmname").ToString() + "\" oldvalue=\""+ Eval("frmname").ToString() + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','frmname');\">" : Eval("frmName") ) + "</td>" +
"<td>" + (Eval("Custom").ToString() == "True" && Eval("showlist").ToString()=="True" ? "<input class=\"text\" type=\"text\" value=\""+ Eval("frmid").ToString() + "\" oldvalue=\""+ Eval("frmid").ToString() + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','frmid');\">" : Eval("frmID") ) + "</td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ (Eval("listwidth").ToString()=="0" ? "" : Eval("listwidth").ToString()) + "\" oldvalue=\""+ (Eval("listwidth").ToString()=="0" ? "" : Eval("listwidth").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','listwidth');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ (Eval("mlistwidth").ToString()=="0" ? "" : Eval("mlistwidth").ToString()) + "\" oldvalue=\""+ (Eval("mlistwidth").ToString()=="0" ? "" : Eval("mlistwidth").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','mlistwidth');\"></td>" +

"<td><input type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','orderby');\"" + (Eval("orderby").ToString()=="True" ? " checked" : "") + " style=\"" + (Eval("Custom").ToString()=="True" && Eval("CustomCode").ToString().Length==0 ? "display:none;" : "") + "\" /></td>" +
"<td><input type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','move');\"" + (Eval("move").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td><input type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','size');\"" + (Eval("size").ToString()=="True" ? " checked" : "") + " /></td>" +

"<td><input class=\"text\" type=\"text\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("listhtml").ToString()) + "\" oldvalue=\""+ System.Web.HttpUtility.HtmlEncode(Eval("listhtml").ToString()) + "\" title=\"格式如：{data:id}\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-自定义查看');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','listhtml');\"></td>" +

//"<td><input type=\"checkbox\" name=\"defaultorder\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','defaultorder');\"" + (Eval("defaultorder").ToString()=="0" ? "" : " checked") + " style=\"" + (Eval("Custom").ToString()=="True" || Eval("Type").ToString()=="text" ? "display:none;" : "") + "\" />"+
//"<select onChange=\"setModelItem(this,'" + Eval("ModelItemID") + "','defaultorder');\" style=\"width:60px;" + (Eval("defaultorder").ToString()=="0" ? "display:none;" : "") + "\">" +
//"<option value=\"1\"" + (Eval("defaultorder").ToString()=="1" ? " selected" : "") + ">升序</option>" +
//"<option value=\"2\"" + (Eval("defaultorder").ToString()=="2" ? " selected" : "") + ">降序</option>" +
//"</select>" +
//"<input type=\"text\" value=\""+ Eval("defaultOrderPX") + "\" oldvalue=\""+ Eval("defaultOrderPX") + "\" style=\"width:60px;" + (Eval("defaultorder").ToString()=="0" ? "display:none;":"") + "\" class=\"edit\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','defaultorderpx');\">" + 
//"</td>" +
//"<td>"+
//"<select onChange=\"setModelItem(this,'" + Eval("ModelItemID") + "','condition');\" style=\"width:100px;" + (Eval("Custom").ToString()=="True" ? "display:none;" : "")+ "\">" +     eBase.getOperator(Eval("condition").ToString()) +
//"</select>" +
//"<input type=\"text\" value=\""+ Eval("conditionvalue") + "\" oldvalue=\""+ Eval("conditionvalue") + "\" style=\"width:60px;" + (Eval("condition").ToString().Length==0 ? "display:none;":"") + "\" class=\"edit\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','conditionvalue');\">" + 

"</td>" +
//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("listorder").ToString()=="999999" ? "" : Eval("listorder").ToString()) + "\" oldvalue=\""+ (Eval("listorder").ToString()=="999999" ? "" : Eval("listorder").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','listorder');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>
</body>
</html>
