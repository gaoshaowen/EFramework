<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="DataContents.aspx.cs" Inherits="eFrameWork.Manage.DataContents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 文本控件<a id="btn_add" style="float:right;margin-top:4px;<%=(act == "" ? "" : "display:none;" )%>" class="button" href="<%=edt.getAddURL()%>"><span><i class="add">添加</i></span></a></div>

<style>
.divd4q a{display:inline-block;margin-right:25px;}
</style>
<%
if(act=="edit" || act=="add")
{
%>
<div style="margin:6px;">
	<form name="frmaddoredit" id="frmaddoredit" method="post" action="<%=edt.getSaveURL()%>">
	<input name="id" type="hidden" id="id" value="<%=edt.ID%>">
    <input name="act" type="hidden" id="act" value="save">  
	<input name="fromurl" type="hidden" id="fromurl" value="<%=edt.FromURL%>">  
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="eDataView">

      <tr>
        <td width="126" class="title"><ins>*</ins>名称：</td>
        <td class="content"><span class="eform">
		 <ev:eFormControl ID="f1" Field="MC" width="600" notnull="true" fieldname="名称" runat="server" />
		</span></td>
      </tr>
	 
        <tr>
          <td class="title">内容：</td>
          <td class="content"><span class="eform">
		  <ev:eFormControl ID="f2" ControlType="html" Field="NR" width="95%" height="500" runat="server" />
		  </span></td>
        </tr>
  		<tr>
          <td class="title">说明：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f3" ControlType="textarea" Field="BZ" width="600" height="60" runat="server" />
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
"<td height=\"32\">"+ Eval("DataContentID")+"</td>\r\n" +
"<td>"+ Eval("MC").ToString()+"</td>\r\n" +
"<td>"+ Eval("BZ").ToString()+"</td>\r\n" +
"<td>"+ Eval("addTime","{0:yyyy-MM-dd}")+"</td>\r\n" +
"<td>"+
"<a href=\"" + edt.getActionURL("edit",Eval("DataContentID").ToString())  + "\">修改</a>"+
"<a href=\""+ edt.getActionURL("del",Eval("DataContentID").ToString()) +"\" onclick=\"javascript:return confirm('确认要删除吗？');\">删除</a>"+
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