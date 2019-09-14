<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="DataViews.aspx.cs" Inherits="eFrameWork.Manage.DataViews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 视图控件<a id="btn_add" style="float:right;margin-top:4px;<%=(act == "" ? "" : "display:none;" )%>" class="button" href="<%=edt.getAddURL()%>"><span><i class="add">添加</i></span></a></div>

<style>
.divd4q a{display:inline-block;margin-right:25px;}
</style>
<%
if(act=="edit" || act=="add")
{
%>
<div style="margin:6px;">
 <asp:PlaceHolder ID="eFormControlGroup" runat="server">
	<form name="frmaddoredit" id="frmaddoredit" method="post" action="<%=edt.getSaveURL()%>">
	<input name="id" type="hidden" id="id" value="<%=id%>">
    <input name="act" type="hidden" id="act" value="save">  
	<input name="fromurl" type="hidden" id="fromurl" value="<%=edt.FromURL%>">  
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="eDataView">
      <tr>
        <td width="126" class="title">视图名称：</td>
        <td class="content"><span class="eform">
		 <ev:eFormControl ID="f1" Field="mc" width="300" HtmlEncode="false" runat="server" />
		 </span></td>
      </tr>
	   <tr>
          <td class="title">说明：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f7" ControlType="textarea" Field="SM" width="600" height="50" HtmlEncode="false" runat="server" />
		   </span></td>
        </tr>
	  <tr>
	     <td class="title">SQL语句：</td>
	     <td class="content"><span class="eform">
		 <ev:eFormControl ID="f2" ControlType="textarea" Field="sql" width="800" height="120" HtmlEncode="false" runat="server" />
		 </span></td>
	     </tr>
        <tr>
          <td class="title">条件：</td>
          <td class="content"><span class="eform">
		  <ev:eFormControl ID="f10" Field="Condition" width="300" HtmlEncode="false" runat="server" />
		  </span></td>
        </tr>
        <tr>
          <td class="title">分组：</td>
          <td class="content"><span class="eform">
		  <ev:eFormControl ID="f11" Field="GroupBy" width="300" HtmlEncode="false" runat="server" />
		  </span></td>
        </tr>
        <tr>
          <td class="title">排序：</td>
          <td class="content"><span class="eform">
		  <ev:eFormControl ID="f12" Field="OrderBy" width="300" HtmlEncode="false" runat="server" />
		  </span></td>
        </tr>
        <tr>
          <td class="title">数据头：</td>
          <td class="content"><span class="eform">
		    <ev:eFormControl ID="f3" ControlType="textarea" Field="HeaderTemplate" width="600" height="60" HtmlEncode="false" runat="server" /></span></td>
        </tr>
        <tr>
          <td class="title">数据项：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f4" ControlType="textarea" Field="ItemTemplate" width="600" height="60" HtmlEncode="false" runat="server" /></span></td>
        </tr>
        <tr>
          <td class="title">间格：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f5" ControlType="textarea" Field="SplitTemplate" width="600" height="60" HtmlEncode="false" runat="server" /></span></td>
        </tr>
        <tr>
          <td class="title">数据尾：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f6" ControlType="textarea" Field="FooterTemplate" width="600" height="60" HtmlEncode="false" runat="server" /></span></td>
        </tr>		
		 <tr>
          <td class="title">分页方式： </td>
          <td class="content"><span class="eform"><ev:eFormControl ID="f13" Name="f13" ControlType="radio" Field="PageMode" Options="[{text:PC端,value:pc},{text:手机端,value:mobile}]" DefaultValue="pc" runat="server" /></span></td>
        </tr>
        <tr>
          <td class="title">分页大小： </td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f8" Field="Pagesize" width="160" DefaultValue="0" runat="server" />
		   </span></td>
        </tr>
        <tr>
          <td class="title">页码数量：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f9" Field="PageNum" width="160" DefaultValue="0" runat="server" />
		   </span></td>
        </tr>
        <tr>
       <td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">		
		<a class="button" href="javascript:;" onclick="if(frmaddoredit.onsubmit()!=false){frmaddoredit.submit();}"><span><i class="save">保存</i></span></a>
		<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span><i class="back">返回</i></span></a>
		</td>
	   </tr>
	 
    </table>
	 </form>
	  </asp:PlaceHolder>
	 </div>
	<%}%>
<div style="margin:6px;overflow-x:auto;overflow-y:hidden;">
<asp:Repeater id="Rep" runat="server">
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\">\r\n" +
"<thead>\r\n" +
"<tr bgcolor=\"#f2f2f2\">\r\n" +
"<td width=\"300\">编号</td>\r\n" +
"<td>名称</td>\r\n" +
"<td>说明</td>\r\n" +
"<td width=\"80\">添加时间</td>\r\n" +
"<td width=\"120\">操作</td>\r\n" +
"</tr>\r\n" +
"</thead>\r\n"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex+1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + ">\r\n" +
"<td height=\"32\">"+ Eval("DataViewID")+"</td>\r\n" +
"<td>"+ Eval("MC").ToString()+"</td>\r\n" +
"<td>"+ Eval("SM").ToString()+"</td>\r\n" +
"<td>"+ Eval("addTime","{0:yyyy-MM-dd}")+"</td>\r\n" +
"<td>"+
"<a href=\""+ edt.getActionURL("copy",Eval("DataViewID").ToString())  +"\" onclick=\"javascript:return confirm('确认要复制吗？');\">复制</a>"+
"<a href=\"" + edt.getActionURL("edit",Eval("DataViewID").ToString())  + "\">修改</a>"+
"<a href=\""+ edt.getActionURL("del",Eval("DataViewID").ToString()) +"\" onclick=\"javascript:return confirm('确认要删除吗？');\">删除</a>"+
"</td>\r\n" +
"</tr>\r\n" 
%>
</itemtemplate>
<footertemplate><%#"</table>\r\n"%></footertemplate>
</asp:Repeater>
</div>
<div style="margin:6px;"><ev:ePageControl ID="ePageControl1" PageSize="20" PageNum="9" runat="server" /></div>
<asp:Literal id="litTip" runat="server" />
<iframe style="display:none;" width="500" height="500" id="fra" src="about:blank" name="fra"></iframe>
</asp:Content>