<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="eFrameWork.Manage.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 用户管理<a id="btn_add" style="float:right;margin-top:4px;<%=(act == "" ? "" : "display:none;" )%>" class="button" href="<%=edt.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
<script>
function checkUser(frm)
{
	//frm.elements["id"]
	//if(frm.id.value.length>0){return true;}
	if("<%=act%>" != "add"){return true;}
	var url="?act=getuser&value=" + frm.f1.value + "&t=" + now();
	var html=PostURL(url);
	if(html=="false")
	{
		alert("该用户名已存在!");
		frm.f1.focus();
		return false;
	}
	return true;
};
</script>
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
        <td width="126" class="title"><font color="#FF0000">*</font> 用户名：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="f1" Field="yhm" width="200" FieldName="用户名"  notnull="true" runat="server" />
		</span></td>
      </tr>
      <tr>
        <td class="title"><font color="#FF0000">*</font> 密码：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="f2" ControlType="password" Field="MM" width="200" FieldName="密码"  notnull="true" runat="server" />
		</span></td>
      </tr>
      <tr>
        <td class="title"><font color="#FF0000">*</font> 姓名：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="f3" Field="xm" width="200" FieldName="姓名"  notnull="true" runat="server" />
		</span></td>
      </tr>  
     <tr>
        <td class="title"><font color="#FF0000">*</font> 用户类型：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="F4" ControlType="select" Field="UserType" width="200" FieldName="用户类型" Options="[{text:开发人员,value:3},{text:系统用户,value:2}]" notnull="true" runat="server" />
		</span></td>
      </tr>  
	   <tr valign="top">
        <td class="title">角色：</td>
        <td class="content"><asp:Literal id="LitRoles" runat="server" /></td>
      </tr> 
     <tr>
        <td class="title">用户状态：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="F5" ControlType="radio" Field="Active" FieldName="用户状态" Options="[{text:启用,value:True},{text:停用,value:False}]" DefaultValue="True" notnull="true" runat="server" />
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
<ev:eListControl ID="eDataTable" Class="eDataTable" CellSpacing="1" LineHeight="32" runat="server" >
    <ev:eListColumn ControlType="text" FieldName="编号" Width="300" runat="server">{data:ID}</ev:eListColumn>
    <ev:eListColumn ControlType="text" Field="YHM" FieldName="用户名" runat="server" />
    <ev:eListColumn ControlType="text" Field="XM" FieldName="姓名" runat="server" />
    <ev:eListColumn ControlType="text" Field="UserType" FieldName="用户类型" Options="[{text:开发人员,value:3},{text:系统用户,value:2}]" runat="server" />
    <ev:eListColumn ControlType="text" Field="Active" FieldName="用户状态" Options="[{text:启用,value:True},{text:停用,value:False}]" runat="server">
        <a href="?act=active&modelid=2&id={data:id}&value={data:showvalue}"><img src="{base:VirtualPath}{data:ShowPIC}" border="0"></a>
    </ev:eListColumn>
    <ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
    <ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
    <a href="{base:url}act=edit&id={data:ID}">修改</a>
    <a href="{base:url}act=del&id={data:ID}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>

<asp:Repeater id="Rep" runat="server">
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\">\r\n"+
"<thead>\r\n"+
  "<tr bgcolor=\"#f2f2f2\">\r\n"+
  	"<td width=\"250\">编号</td>\r\n"+
	"<td>用户名</td>\r\n"+
	"<td>姓名</td>\r\n"+
	"<td width=\"80\">添加时间</td>\r\n"+
	"<td width=\"120\">操作</td>\r\n"+
  "</tr>\r\n"+
"</thead>\r\n"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex+1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + ">\r\n"+
    "<td height=\"32\">"+ Eval("UserID") + "</td>\r\n"+
	"<td>"+ Eval("YHM") + "</td>\r\n"+
	"<td>"+ Eval("XM") + "</td>\r\n"+
	"<td>"+ Eval("addTime","{0:yyyy-MM-dd}") + "</td>\r\n"+
	"<td>"+
	"<a href=\"" + edt.getActionURL("edit",Eval("UserID").ToString())  + "\">修改</a>"+
	"<a href=\""+ edt.getActionURL("del",Eval("UserID").ToString()) +"\" onclick=\"javascript:return confirm('确认要删除吗？');\">删除</a>"+
	"</td>\r\n"+
"</tr>\r\n"
%>
</itemtemplate>
<footertemplate>
<%#"</table>\r\n"%>
</footertemplate>
</asp:Repeater>

</div>
<div style="margin:6px;"><ev:ePageControl ID="ePageControl1" PageSize="20" PageNum="9" runat="server" /></div>
</asp:Content>
