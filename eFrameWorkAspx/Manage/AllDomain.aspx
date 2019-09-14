<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="AllDomain.aspx.cs" Inherits="eFrameWork.Manage.AllDomain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 域名授权<a id="btn_add" style="float:right;margin-top:4px;<%=(act == "" ? "" : "display:none;" )%>" class="button" href="<%=edt.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
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
        <td width="126" class="title"><font color="#FF0000">*</font> 域名：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="f1" Field="Domain" width="200" FieldName="域名"  notnull="true" runat="server" />
		</span> 不要http://</td>
      </tr>
      <tr>
        <td class="title">说明：</td>
        <td class="content"><span class="eform">
		<ev:eFormControl ID="f3" Field="SM" width="200" FieldName="说明"  notnull="false" runat="server" />
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
    <ev:eListColumn ControlType="text" Field="Domain" FieldName="域名" runat="server" />
    <ev:eListColumn ControlType="text" Field="SM" FieldName="说明" runat="server" />
    <ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
    <ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
    <a href="{base:url}act=edit&id={data:ID}">修改</a>
    <a href="{base:url}act=del&id={data:ID}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>


</div>
<div style="margin:6px;"><ev:ePageControl ID="ePageControl1" PageSize="20" PageNum="9" runat="server" /></div>
</asp:Content>
