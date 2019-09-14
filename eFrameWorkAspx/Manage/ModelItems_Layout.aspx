<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Layout.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Layout" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
1.选项卡<%=(eBase.showHelp() ? " <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(134);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") %><br />
<asp:Repeater id="RepTags" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable_Tab\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" widt5h=\"100%\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"30\" align=\"center\"><a title=\"添加选项卡\" href=\"javascript:;\" onclick=\"addModelTab(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"150\">名称</td>" +
"<td width=\"150\">自定义程序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(135);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">顺序</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelTabID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"26\" align=\"center\"><a title=\"删除选项卡\" href=\"javascript:;\" onclick=\"delModelTab(this,'" + Eval("ModelTabID") + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +
"<td><input reload=\"true\" class=\"text\" type=\"text\" value=\""+ Eval("mc").ToString() + "\" oldvalue=\""+ Eval("mc").ToString() + "\" onBlur=\"setModelTab(this,'" + Eval("ModelTabID") + "','mc');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("ProgrameFile").ToString() + "\" oldvalue=\""+ Eval("ProgrameFile").ToString() + "\" onBlur=\"setModelTab(this,'" + Eval("ModelTabID") + "','programefile');\"></td>" +
//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("px").ToString()=="999999" ? "" : Eval("px").ToString()) + "\" oldvalue=\""+ (Eval("px").ToString()=="999999" ? "" : Eval("px").ToString()) + "\" onBlur=\"setModelTab(this,'" + Eval("ModelTabID") + "','px');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>

2.面板<%=(eBase.showHelp() ? " <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(136);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") %><br />
<asp:Repeater id="RepGroups" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable_Panel\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" widt5h=\"100%\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"30\" align=\"center\"><a title=\"添加面板\" href=\"javascript:;\" onclick=\"addModelGroup(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"105\">名称</td>" +
"<td width=\"160\">选项卡" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(137);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"105\">默认打开</td>" +
"<td width=\"150\">自定义程序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(138);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">顺序</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelPanelID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"26\" align=\"center\"><a title=\"删除面板\" href=\"javascript:;\" onclick=\"delModelGroup(this,'" + Eval("ModelPanelID") + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +
"<td><input reload=\"true\" class=\"text\" type=\"text\" value=\""+ Eval("mc").ToString() + "\" oldvalue=\""+ Eval("mc").ToString() + "\" onBlur=\"setModelGroup(this,'" + Eval("ModelPanelID") + "','mc');\"></td>" +
"<td>"
%>
<select onChange="setModelGroup(this,'<%# Eval("ModelPanelID")%>','ModelTabID');" style="width:150px;">
	<option value="NULL">无</option>
	<asp:Literal id="LitTags" runat="server" />
</select>
<%#
"</td>" +
"<td height=\"32\"><input type=\"checkbox\" onclick=\"setModelGroup(this,'" + Eval("ModelPanelID") + "','bopen');\"" + (Eval("bOpen").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("ProgrameFile").ToString() + "\" oldvalue=\""+ Eval("ProgrameFile").ToString() + "\" onBlur=\"setModelGroup(this,'" + Eval("ModelPanelID") + "','programefile');\"></td>" +
//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("px").ToString()=="999999" ? "" : Eval("px").ToString()) + "\" oldvalue=\""+ (Eval("px").ToString()=="999999" ? "" : Eval("px").ToString()) + "\" onBlur=\"setModelGroup(this,'" + Eval("ModelPanelID") + "','px');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>

3.所有列<%=(eBase.showHelp() ? " <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(139);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") %><br />

<asp:Repeater id="RepColumns" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable_Items\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" wi6dth=\"100%\">" +
"<thead>" +
"<tr>" +
"<td width=\"180\">模块名称" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(140);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"180\">列名</td>" +
"<td width=\"160\">选项卡" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(141);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"160\">面板" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(142);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">跨行" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(143);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">跨列" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(144);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"85\">顺序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(145);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelItemID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"32\">"+ Eval("modelName") + "</td>" +
"<td>"+ Eval("MC") + "</td>" +
"<td>"
%>
    <select reload="true" onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','ModelTabID');" style="width:150px;">
	<option value="NULL">无</option>
	<asp:Literal id="LitTags" runat="server" />
</select>
<%#
 "</td>" +
"<td>"%>
 <select reload="true" onChange="setModelItem(this,'<%# Eval("ModelItemID")%>','ModelPanelID');" style="width:150px;">
	<option value="NULL">无</option>
	<asp:Literal id="LitGroups" runat="server" />
</select>
<%# 
"</td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("addrowspan") + "\" value=\""+ Eval("addrowspan") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','addrowspan');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("addcolspan") + "\" value=\""+ Eval("addcolspan") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','addcolspan');\"></td>" +
//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("addorder").ToString()=="999999" ? "" : Eval("addorder").ToString()) + "\" oldvalue=\""+ (Eval("addorder").ToString()=="999999" ? "" : Eval("addorder").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','addorder');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>
4.子模块<%=(eBase.showHelp() ? " <img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(146);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") %><br />
<asp:Repeater id="RepModels" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" wi6dth=\"100%\">" +
"<thead>" +
"<tr>" +
"<td width=\"180\">模块名称</td>" +
"<td width=\"160\">选项卡" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(147);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"160\">面板" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(148);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td>"+ Eval("MC") + "</td>" +
"<td>"
%>
    <select onChange="setModel(this,'ModelTabID','<%# Eval("ModelID").ToString()%>');" oldvalue="<%#Eval("ModelTabID").ToString()%>" style="width:150px;">
	<option value="NULL">无</option>
	<asp:Literal id="LitTags" runat="server" />
</select>
<%#
 "</td>" +
"<td>"%>
 <select onChange="setModel(this,'ModelPanelID','<%# Eval("ModelID").ToString()%>');" oldvalue="<%#Eval("ModelPanelID")  %>" style="width:150px;">
	<option value="NULL">无</option>
	<asp:Literal id="LitGroups" runat="server" />
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
