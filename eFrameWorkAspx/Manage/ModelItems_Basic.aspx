<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelItems_Basic.aspx.cs" Inherits="eFrameWork.Manage.ModelItems_Basic" %>
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
"<table id=\"eDataTable_Basic\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"min-width:1640px;\">" +
"<thead>" +
"<tr>" +
"<td width=\"55\">显示" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(66);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"130\">编码</td>" +
"<td width=\"130\">列名" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(67);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
//"<td width=\"60\">主键</td>" +
"<td width=\"220\">输出控件" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(68);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"85\">表单Name" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(69);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"70\">表单ID" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(70);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"70\" title=\"电脑端宽度\">宽(PC)" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(71);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\" title=\"手机端宽度\">宽(M)" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(72);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">高" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(73);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">默认值" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(74);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">格式化" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(75);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">单位" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(76);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"80\">提示" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(77);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"100\">自定义查看" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(78);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">跨行" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(79);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">跨列" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(80);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"110\">HTML扩展属性" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(81);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"70\">分格符" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(82);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"<td width=\"60\">顺序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(83);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;margin-bottom:4px;\">" : "") + "</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("ModelItemID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"32\"><input reload=\"true\" id=\"showadd_" + Eval("ModelItemID") + "\" type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','showadd');\"" + (Eval("showadd").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td>" + Eval("Code") + "</td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("MC") + "\" value=\""+ Eval("MC") + "\" class=\"edit\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','mc');\" /></td>" +
"<td><select reload=\"true\" onChange=\"setModelItem(this,'" + Eval("ModelItemID") + "','controltype');\" sty3le=\"width:100px;\">" + eBase.getControlType(Eval("controltype").ToString()) + "<select>" +
"<select onChange=\"setModelItem(this,'" + Eval("ModelItemID") + "','dateformat');\" style=\"width:100px;"+(Eval("controltype").ToString()=="date" ? "" : "display:none;")+"\">" +
"<option value=\"\">无</option>" +
"<option value=\"yyyy-MM-dd\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd" ? " selected" : "") + ">yyyy-MM-dd</option>" +
"<option value=\"yyyy-MM-dd HH\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd HH" ? " selected" : "") + ">yyyy-MM-dd HH</option>" +
"<option value=\"yyyy-MM-dd HH:mm\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd HH:mm" ? " selected" : "") + ">yyyy-MM-dd HH:mm</option>" +
"<option value=\"yyyy-MM-dd HH:mm:ss\"" + (Eval("dateformat").ToString()=="yyyy-MM-dd HH:mm:ss" ? " selected" : "") + ">yyyy-MM-dd HH:mm:ss</option>" +
"<select>"+
((Eval("controltype").ToString()=="file" || Eval("controltype").ToString()=="html" || Eval("controltype").ToString()=="images") ? "<br>图片宽度:" +
"<input type=\"text\" oldvalue=\""+ Eval("PictureMaxWidth") + "\" value=\""+ Eval("PictureMaxWidth") + "\" class=\"edit\" style=\"width:50px;\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','picturemaxwidth');\">" : "") +
( Eval("controltype").ToString()=="images" ? "<br>缩略图:" + 
"<input id=\"thumbs_" +   Eval("ModelItemID").ToString().Replace("-","") + "\"  jsonformat=\"[{&quot;text&quot;:&quot;宽度&quot;,&quot;value&quot;:&quot;Width&quot;},{&quot;text&quot;:&quot;品质&quot;,&quot;value&quot;:&quot;Quality&quot;},{&quot;text&quot;:&quot;文件名后缀&quot;,&quot;value&quot;:&quot;Ext&quot;}]\" type=\"text\" oldvalue=\""+ System.Web.HttpUtility.HtmlEncode(Eval("Thumbs").ToString()) + "\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("Thumbs").ToString()) + "\" class=\"edit\" style=\"width:150px;display:none;\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','thumbs');\">" +
getJsonText(Eval("Thumbs").ToString(),"Width") +
"<img src=\"images/jsonedit.jpg\" align=\"absmiddle\" style=\"cursor:pointer;\" onclick=\"Json_Edit('thumbs_" +  Eval("ModelItemID").ToString().Replace("-","") + "');\">" + 
"" : "") + 
"<br><select onChange=\"setModelItem(this,'" + Eval("ModelItemID") + "','fillmodelid');\" style=\"width:180px;"+(Eval("controltype").ToString()=="datatext" ? "" : "display:none;")+"\">"+
"<option>无</option>" +
 eOleDB.getOptions("select MC,ModelID from a_eke_sysModels where delTag=0 and Type=3 and ParentID='" + modelid + "'", "MC", "ModelID", Eval("FillModelID").ToString()) + 
"<select></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("frmname") + "\" value=\""+ Eval("frmname") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','frmname');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("frmid") + "\" value=\""+ Eval("frmid") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','frmid');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("width") + "\" value=\""+ Eval("width") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','width');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("mwidth") + "\" value=\""+ Eval("mwidth") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','mwidth');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("height") + "\" value=\""+ Eval("height") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','height');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("defaultvalue") + "\" value=\""+ Eval("defaultvalue") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','defaultvalue');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("formatstring") + "\" value=\""+ Eval("formatstring") + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-格式化');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','formatstring');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("dw") + "\" value=\""+ Eval("dw") + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-单位');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','dw');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("tip") + "\" value=\""+ Eval("tip") + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-提示');\"  onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','tip');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ System.Web.HttpUtility.HtmlEncode(Eval("viewhtml").ToString()) + "\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("viewhtml").ToString()) + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-自定义查看');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','viewhtml');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("addrowspan") + "\" value=\""+ Eval("addrowspan") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','addrowspan');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("addcolspan") + "\" value=\""+ Eval("addcolspan") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','addcolspan');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+  System.Web.HttpUtility.HtmlEncode(Eval("attributes").ToString()) + "\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("attributes").ToString()) + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-HTML扩展属性');\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','attributes');\"></td>" +
"<td><input class=\"text\" type=\"text\" oldvalue=\""+ Eval("splitchar") + "\" value=\""+ Eval("splitchar") + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','splitchar');\"></td>" +
//"<td><input class=\"text\" type=\"text\" reload=\"true\" oldvalue=\""+ (Eval("addorder").ToString()=="999999" ? "" : Eval("addorder").ToString()) + "\" value=\""+ (Eval("addorder").ToString()=="999999" ? "" : Eval("addorder").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','addorder');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>
自定义列：<br />
<asp:Repeater id="RepCustom" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" widt5h=\"100%\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"30\" align=\"center\"><a title=\"添加列\" href=\"javascript:;\" onclick=\"addModelItem(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"150\">名称" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(150);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">" : "") + "</td>" +
"<td width=\"150\">自定义程序" + (eBase.showHelp() ? "<img src=\"images/help.gif\" align=\"absmiddle\" border=\"0\" onclick=\"showHelp(151);\" title=\"查看帮助\" alt=\"查看帮助\" style=\"cursor:pointer;\">" : "") + "</td>" +
"<td width=\"60\">界面</td>" +
//"<td width=\"150\">自定义编码</td>" +
"</tr>" +
"</thead>"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr>" +
"<td height=\"26\" align=\"center\"><a title=\"删除列\" href=\"javascript:;\" onclick=\"delModelItem(this,'" + Eval("ModelItemID") + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("mc").ToString()) + "\" oldvalue=\""+  System.Web.HttpUtility.HtmlEncode(Eval("mc").ToString()) + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','mc');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("ProgrameFile").ToString() + "\" oldvalue=\""+ Eval("ProgrameFile").ToString() + "\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','programefile');\"></td>" +
"<td>"+
//"<input reload=\"false\" name=\"hasui_" + Eval("ModelItemID").ToString().Replace("-","") + "\" id=\"hasui_" + Eval("ModelItemID").ToString().Replace("-","") + "_1\" type=\"radio\" value=\"1\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','hasui');\"" + (Eval("hasui").ToString()=="True" ? " checked" : "") + " /><label for=\"hasui_" + Eval("ModelItemID").ToString().Replace("-","") + "_1\">显示</label>&nbsp;"+
//"<input reload=\"false\" name=\"hasui_" + Eval("ModelItemID").ToString().Replace("-","") + "\" id=\"hasui_" + Eval("ModelItemID").ToString().Replace("-","") + "_2\" type=\"radio\" value=\"0\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','hasui');\"" + (Eval("hasui").ToString()=="False" ? " checked" : "") + " /><label for=\"hasui_" + Eval("ModelItemID").ToString().Replace("-","") + "_2\">不显示</label>"+

"<input reload=\"false\" id=\"hasui_" + Eval("ModelItemID") + "\" type=\"checkbox\" onclick=\"setModelItem(this,'" + Eval("ModelItemID") + "','hasui');\"" + (Eval("hasui").ToString()=="True" ? " checked" : "") + " />" +

"</td>" +
//"<td><input type=\"text\" value=\""+ Eval("CustomCode").ToString() + "\" oldvalue=\""+ Eval("CustomCode").ToString() + "\" class=\"edit\" onBlur=\"setModelItem(this,'" + Eval("ModelItemID") + "','customcode');\"></td>" +
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</table>"%></footertemplate>
</asp:Repeater>
</body>
</html>