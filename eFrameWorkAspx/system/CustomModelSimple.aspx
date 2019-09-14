<%@ Page Title="" Language="C#" MasterPageFile="~/Master/systemMain.Master" AutoEventWireup="true" CodeBehind="CustomModelSimple.aspx.cs" Inherits="eFrameWork.system.CustomModelSimple" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> 自定义模块<%if( Action.Value == "" && model.Power["add"].ToString()=="true"){%><a id="btn_add" style="float:right;margin-top:10px;" class="button" href="<%=eform.getAddURL()%>"><span><i class="add">添加</i></span></a><%}%></div>
<%
if(Action.Value == "" )
{
%>
<div style="margin:10px;">
<dl id="eSearchBox" class="ePanel">
<dt><h1 onclick="showPanel(this);" class="search"><a href="javascript:;" class="cur" onfocus="this.blur();"></a>搜索</h1></dt>
<dd style="display:none;">
<form id="frmsearch" name="frmsearch" method="post" onsubmit="return goSearch(this);" action="<%=elist.getSearchURL()%>">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>

<tr>
<td class="title">姓名：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s1" Name="s1" ControlType="text" Field="FullName" Operator="like" FieldName="姓名" DataType="string" runat="server" /></span></td>
</tr>

<tr>
<td colspan="2" class="title" style="text-align:left;padding-left:125px;">
<a class="button" href="javascript:;" onclick="if(frmsearch.onsubmit()!=false){frmsearch.submit();}"><span><i class="search">搜索</i></span></a>
</td>
</tr>
</table>
</form>
</dd>
</dl>
</div>
<div style="margin:10px;overflow-x:auto;overflow-y:hidden;">
<ev:eListControl ID="eDataTable" ShowMenu="true" LineHeight="45" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" Width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" Field="FullName" FieldName="姓名" Move="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Sex" FieldName="性别" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="date" Field="Birthday" FieldName="生日" FormatString="{0:yyyy-MM-dd}" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Education" FieldName="学历" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Hobby" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" runat="server" />
<ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" Width="140" FormatString="{0:yyyy-MM-dd HH:mm:ss}"  OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
    <a href="{base:url}act=view&id={data:ID}">查看</a>
    <a href="{base:url}act=edit&id={data:ID}">修改</a>
    <a href="{base:url}act=del&id={data:ID}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>
</div>
<div style="margin:10px;">
<ev:ePageControl ID="ePageControl1" PageSize="10" PageNum="9" runat="server" />
</div>
<%
}
else
{
%>
<div style="margin:10px;">
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<form id="frmaddoredit" name="frmaddoredit" method="post" tar_get="mainform" action="<%=eform.getSaveURL()%>">
<input type="hidden" id="<%=Action.ActionName%>" name="<%=Action.ActionName%>" value="save">
<input type="hidden" id="fromurl" name="fromurl" value="<%=eform.FromURL%>">
<input type="hidden" id="<%=eform.primaryKeyName%>" name="<%=eform.primaryKeyName%>" value="<%=eform.ID%>">
<input type="hidden" name="eformdata_<%=ModelID.Replace("-","_")%>" id="eformdata_<%=ModelID.Replace("-","_")%>" value="<%=System.Web.HttpUtility.HtmlEncode(eform.getJson())%>">
</form>
<form id="form_<%=ModelID.Replace("-","_")%>" name="form_<%=ModelID.Replace("-","_")%>" method="post" action="">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>姓名：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F4" Name="M2_F4" ControlType="text" Field="FullName" Width="300px" FieldName="姓名" NotNull="True" MaxLength="50" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>性别：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F8" Name="M2_F8" ControlType="radio" Field="Sex" FieldName="性别" NotNull="True" Options="[{text:男,value:1},{text:女,value:2}]" DefaultValue="1" runat="server" /></span></td>

</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>生日：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F7" Name="M2_F7" ControlType="date" Field="Birthday" DateFormat="yyyy-mm-dd" FormatString="{0:yyyy-MM-dd}" FieldName="生日" NotNull="True" DataType="date" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>学历：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F10" Name="M2_F10" ControlType="select" Field="Education" FieldName="学历" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" NotNull="True" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>兴趣爱好：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F9" Name="M2_F9" ControlType="checkbox" Field="Hobby" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" NotNull="True" runat="server" /></span></td>
</tr>

<tr>
<td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">
<%
if(Action.Value == "add" || Action.Value == "edit")
{
%>
<a class="button" href="javascript:;" onclick="packform('<%=ModelID.Replace("-","_")%>');"><span><i class="save"><%=(Action.Value == "add" ? "添加" : "保存")%></i></span></a><%}%>
<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span><i class="back">返回</i></span></a>
</td>
</tr>
</table>
</form>
</asp:PlaceHolder>
</div>
<%}%>
</asp:Content>