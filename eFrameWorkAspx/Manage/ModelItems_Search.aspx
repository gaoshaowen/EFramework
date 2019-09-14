<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Search.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Search" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

<asp:Repeater id="Rep" runat="server">
<headertemplate>
<%#
"<table id=\"eDataTable_Search\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"min-width:1720px;\">" +
"<thead>" +
"<tr>" +
"<td width=\"30\" bgc6olor=\"#ffffff\" align=\"center\"><a title=\"添加条件\" href=\"javascript:;\" onclick=\"addModelCondition(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"50\">显示" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(108);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">表单Name" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(109);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"90\">条件名称" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(110);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"180\">输出控件" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(111);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">宽" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(112);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">高" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(113);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">条件列" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(114);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">操作符" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(115);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td style=\"min-width:400px;\">选项" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(116);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
//"<td width=\"60\">合并列</td>" +
"<td width=\"60\">跨行" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(117);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">跨列" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(118);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"100\">HTML扩展属性" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(119);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"100\">说明</td>" +
"<td width=\"85\">顺序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(120);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +

"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelConditionID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td align=\"center\"><a title=\"删除条件\" href=\"javascript:;\" onclick=\"delModelCondition(this,'" + Eval("ModelConditionID") + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +
"<td><input reload=\"true\" type=\"checkbox\" onclick=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','show');\"" + (Eval("show").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td>s" + Eval("num") + "</td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("MC") + "\" oldvalue=\""+ Eval("MC") + "\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','mc');\" /></td>" +
"<td>" +"<select reload=\"true\" onChange=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','controltype');\" style=\"width:65px;\">" + eBase.getSearchControlType(Eval("ControlType").ToString())+"<select>"+
"<select onChange=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','multiselect');\" style=\"width:65px;" + (Eval("ControlType").ToString()=="link" ? "" : "display:none;") + "\">" +
"<option value=\"0\"" + (Eval("multiselect").ToString()=="False" ? " selected" : "") + ">单选</option>"+
"<option value=\"1\"" + (Eval("multiselect").ToString()=="True" ? " selected" : "") + ">多选</option>"+
"<select>"+

"<select onChange=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','dateformat');\" style=\"width:100px;"+(Eval("ControlType").ToString()=="date" ? "" : "display:none;")+"\">" +
"<option>无</option>" +
"<option value=\"yyyy-MM-dd\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd" ? " selected" : "") + ">yyyy-MM-dd</option>" +
"<option value=\"yyyy-MM-dd HH\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd HH" ? " selected" : "") + ">yyyy-MM-dd HH</option>" +
"<option value=\"yyyy-MM-dd HH:mm\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd HH:mm" ? " selected" : "") + ">yyyy-MM-dd HH:mm</option>" +
"<option value=\"yyyy-MM-dd HH:mm:ss\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd HH:mm:ss" ? " selected" : "") + ">yyyy-MM-dd HH:mm:ss</option>" +
"<select>"+
"</td>" +
"</td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("width") + "\" value=\""+ Eval("width") + "\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','width');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("height") + "\" value=\""+ Eval("height") + "\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','height');\"></td>" +
"<td>"
%>
<select onChange="setModelCondition(this,'<%# Eval("ModelConditionID")%>','code');" style="width:75px;<%# getDisplay(Eval("ModelConditionID").ToString())%>">
	<option value="">无</option>
	<asp:Literal id="LitColnums" runat="server" />
</select>
<%#
"</td>" +
"<td>"+
"<select reload=\"true\" onChange=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','operator');\" style=\"width:75px;" + getDisplay(Eval("ModelConditionID").ToString()) + "\">" +
eBase.getOperator(Eval("operator").ToString())+"<select>" +
"</td>" +
"<td><a href=\"javascript:;\" onclick=\"loadColumnOptions(this,'" + Eval("ModelConditionID") + "');\">同步</a>"+

"<span style=\""+(Eval("operator").ToString()=="custom" ? "" : "display:none;")+"\">自定义："+
"<input class=\"text\" type=\"text\" oldvalue=\""+ Eval("Custom") + "\" value=\""+ Eval("Custom") + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-自定义');\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','custom');\" style=\"width:300px;\"><br></span>"
%>
<span<%# ((Eval("ControlType").ToString()=="text" || Eval("ControlType").ToString()=="date" || Eval("ControlType").ToString()=="quick" || Eval("ControlType").ToString()=="") ? " style=\"display:none;\"" : "")%>>
<span id="spanbind" runat="server">
绑定： <select reload="true" onChange="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindobject');" style="width:80px;">
	<option value="">无</option>
	<asp:Literal id="LitObjects" runat="server" />
	</select>
	行
	<input class="text" type="text" value="<%# (Eval("bindrows").ToString()=="0" ? "" : Eval("bindrows").ToString())%>" style="width:60px;" onBlur="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindrows');" />
	值
	<select onChange="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindvalue');" style="width:80px;">
	<option value="">无</option>
	<asp:Literal id="LitValue" runat="server" />
	</select>
	文本
	<select onChange="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindtext');" style="width:80px;">
	<option value="">无</option>
	<asp:Literal id="LitText" runat="server" />
	</select><br />
	条件
	<input class="text" type="text" value="<%# Eval("bindcondition")%>" style="width:80px;" onDblClick="dblClick(this,'<%# Eval("MC")%>-条件');" onBlur="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindcondition');" />
	分组
	<input class="text" type="text" value="<%# Eval("bindgroupby")%>" style="width:80px;" onDblClick="dblClick(this,'<%# Eval("MC")%>-分组');" onBlur="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindgroupby');" />
	排序
	<input class="text" type="text" value="<%# Eval("bindorderby")%>" style="width:80px;" onDblClick="dblClick(this,'<%# Eval("MC")%>-排序');" onBlur="setModelCondition(this,'<%# Eval("ModelConditionID")%>','bindorderby');" />
	<br />
	选项：<input id="search_options_<%# Eval("ModelConditionID").ToString().Replace("-","") %>"  jsonformat="[{&quot;text&quot;:&quot;文本&quot;,&quot;value&quot;:&quot;text&quot;},{&quot;text&quot;:&quot;值&quot;,&quot;value&quot;:&quot;value&quot;}]" type="text" value="<%# System.Web.HttpUtility.HtmlEncode(Eval("options").ToString())%>" class="edit" style="width:300px;display:none;" onDblClick="dblClick(this,'<%# Eval("MC")%>-选项');" onBlur="setModelCondition(this,'<%# Eval("ModelConditionID")%>','options');" />
    <img src="images/jsonedit.jpg" align="absmiddle" style="cursor:pointer;" onClick="Json_Edit('search_options_<%# Eval("ModelConditionID").ToString().Replace("-","") %>');">
     <%# getJsonText(Eval("options").ToString(),"text")%>
</span>
<span id="spanoptions"<%# (Eval("bindobject").ToString().Length>0 ? " style=\"display:none;\"" : "")%>><br />自定义条件：<asp:Literal id="LitOptions" runat="server" /></span>
</span>
<%#
"</td>" +
//"<td><input type=\"text\" value=\""+ (Eval("colspan").ToString()=="0" ? "" : Eval("colspan").ToString()) + "\" oldvalue=\""+ (Eval("colspan").ToString()=="0" ? "" : Eval("colspan").ToString()) + "\" style=\"border:1px solid #ccc;width:55px;\" onBlur=\"setModelCondition(this,"+ Eval("ModelConditionID")+",'colspan');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("rowspan") + "\" value=\""+ Eval("rowspan") + "\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','rowspan');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("colspan") + "\" value=\""+ Eval("colspan") + "\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','colspan');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+  System.Web.HttpUtility.HtmlEncode(Eval("attributes").ToString()) + "\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("attributes").ToString()) + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-HTML扩展属性');\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','attributes');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("SM").ToString() + "\" oldvalue=\""+ Eval("SM").ToString() + "\"  ondblclick=\"dblClick(this,'" + Eval("MC") + "-说明');\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','sm');\" /></td>" +

//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("px").ToString()=="999999" ? "" : Eval("px").ToString()) + "\" oldvalue=\""+ (Eval("px").ToString()=="999999" ? "" : Eval("px").ToString()) + "\" onBlur=\"setModelCondition(this,'" + Eval("ModelConditionID") + "','px');\" /></td>" +
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>
</body>
</html>
