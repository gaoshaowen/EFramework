<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eForm.aspx.cs" Inherits="eFrameWork.Examples._eForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eForm类<a id="btn_add" style="float:right;margin-top:10px;<%=( action.Value == "" ? "" : "display:none;" )%>" class="button" href="<%=eform.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
<div style="margin:8px;line-height:26px;">
    <asp:Literal id="litScript" runat="server" />
<%
    if (action.Value == "edit" || action.Value == "add" || action.Value == "view")
{
%>
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<form id="frmaddoredit" name="frmaddoredit" method="post" action="<%=eform.getSaveURL()%>">
<input type="hidden" id="act" name="act" value="save">
<input type="hidden" id="fromurl" name="fromurl" value="<%=eform.FromURL%>">
<input type="hidden" id="ID" name="ID" value="<%=eform.ID%>">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title"><%=(action.Value == "add" || action.Value == "edit" ? "<ins>*</ins>" : "")%>姓名：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F1" Name="F1" ControlType="text" Field="FullName" Width="300px" FieldName="姓名" NotNull="True" MaxLength="50" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%=(action.Value == "add" || action.Value == "edit" ? "<ins>*</ins>" : "")%>性别：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F2" Name="F2" ControlType="radio" Field="Sex" FieldName="性别" NotNull="True" Options="[{text:男,value:1},{text:女,value:2}]" DefaultValue="1" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%=(action.Value == "add" || action.Value == "edit" ? "<ins>*</ins>" : "")%>生日：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F3" Name="F3" ControlType="date" DateFormat="yyyy-mm-dd" FormatString="{0:yyyy-MM-dd}" Field="Birthday" FieldName="生日" NotNull="True" DataType="date" runat="server" /></span></td>
</tr>
<tr>
<td class="title">简介：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F4" Name="F4" ControlType="textarea" Field="Summary" Width="300px" FieldName="简介" NotNull="False" MaxLength="500" runat="server" /></span></td>
</tr>
<tr>
<tr>
<td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">
<%
if(action.Value == "add" || action.Value == "edit")
{
%>
<a class="button" href="javascript:;" onclick="if(frmaddoredit.onsubmit()!=false){frmaddoredit.submit();}"><span><i class="save"><%=(action.Value == "add" ? "添加" : "保存")%></i></span></a><%}%>
<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span><i class="back">返回</i></span></a>
</td>
</tr>
</table>
</form>
</asp:PlaceHolder>
<%
}
    if (action.Value == "")
{
%>
<ev:eListControl ID="eDataTable" LineHeight="32" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" Width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" Field="FullName" FieldName="姓名" runat="server" />
<ev:eListColumn ControlType="text" Field="Sex" FieldName="性别" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn ControlType="date" Field="Birthday" FieldName="生日" FormatString="{0:yyyy-MM-dd}" runat="server" />
<ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
<ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
    <a href="{base:url}act=view&id={data:ID}">查看</a>
    <a href="{base:url}act=edit&id={data:ID}">修改</a>
    <a href="{base:url}act=del&id={data:ID}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>
<div style="margin:8px;"><ev:ePageControl ID="ePageControl1" PageSize="10" PageNum="9" runat="server" /></div>
<%}%>
</div>
</asp:Content>