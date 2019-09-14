<%@ Page Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeFile="Multiple.aspx.cs" Inherits="eFrameWork.Examples.Multiple" Title="Untitled_Page" %>
<%@ Import Namespace="System.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="nav">您当前位置：首页 -> 综合示例<a id="btn_add" style="float:right;margin-top:10px;<%=( Action.Value == "" ? "" : "display:none;" )%>" class="button" href="<%=eform.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
<%
if(Action.Value == "" )
{
%>

<div style="margin:10px;">
<dl id="eSearchBox" class="ePanel">
<dt><h1 onclick="showPanel(this);" class="search"><a href="javascript:;" class="cur" onfocus="this.blur();"></a>搜索</h1></dt>
<dd style="display:none;">
<asp:PlaceHolder ID="eSearchControlGroup" runat="server">
<form id="frmsearch" name="frmsearch" method="post" onsubmit="return goSearch(this);" action="<%=elist.getSearchURL()%>">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title">姓名：</td>
<td class="content" colspan="3"><span class="eform"><ev:eSearchControl ID="s10" Name="s10" ControlType="text" Field="FullName" Operator="like" FieldName="姓名" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">兴趣爱好：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s8" Name="s8" ControlType="checkbox" Field="Hobby" Operator="like" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" DataType="string" runat="server" /></span></td>
<td class="title">学历：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s9" Name="s9" ControlType="select" Field="Education" Operator="=" FieldName="学历" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">性别：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s7" Name="s7" ControlType="radio" Field="Sex" Operator="=" Options="[{text:男,value:1},{text:女,value:2}]" FieldName="性别" DataType="string" runat="server" /></span></td>
<td class="title">是否显示：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s4" Name="s4" ControlType="radio" Field="" FieldName="是否显示" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">添加时间：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s5" Name="s5" ControlType="date" Field="addTime" Operator=">=" DateFormat="yyyy-MM-dd" FieldName="添加时间" DataType="string" runat="server" /></span></td>
<td class="title">添加时间：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s6" Name="s6" ControlType="date" Field="addTime" Operator="<=" DateFormat="yyyy-MM-dd" FieldName="添加时间" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td colspan="4" class="title" style="text-align:left;padding-left:125px;"><a class="button" href="javascript:;" onclick="if(frmsearch.onsubmit()!=false){frmsearch.submit();}"><span><i class="search">搜索</i></span></a></td>
</tr>
</table>
</form>
</asp:PlaceHolder>
</dd>
</dl>
</div>
<div style="margin:10px;overflow-x:auto;overflow-y:hidden;">
<ev:eListControl ID="eDataTable" ShowMenu="true" LineHeight="45" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" Width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" Field="FullName" Width="120" FieldName="姓名" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="password" Field="PassWord" FieldName="密码" Width="90" runat="server" />
<ev:eListColumn ControlType="date" Field="Birthday" FieldName="生日" Width="100" FormatString="{0:yyyy-MM-dd}" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Sex" FieldName="性别" Width="80" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Hobby" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Education" FieldName="学历" Width="90" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="file" Field="Photo" FieldName="照片" OrderBy="true" runat="server"><div style="height:{row:height}px;"><span></span><p><img style="max-width:180px;" src="{data:photo}" oner3ror="this.style.display='none';"></p></div></ev:eListColumn>
<ev:eListColumn ControlType="text" Field="Height" FieldName="身高" Width="90" FormatString="{0:F2}" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Show" FieldName="是否显示" Width="90" Options="[{text:是,value:True},{text:否,value:False}]" OrderBy="true" runat="server">
<a href="?act=show&modelid=2&id={data:id}&value={data:showvalue}"><img src="{base:VirtualPath}{data:ShowPIC}" border="0"></a>
</ev:eListColumn>
<ev:eListColumn ControlType="sort" Field="PX" FieldName="显示顺序" Width="90" runat="server" />
<ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" Width="140" FormatString="{0:yyyy-MM-dd HH:mm:ss}" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
    <a href="{base:url}modelid=2&act=view&id={data:ID}">查看</a>
    <a href="{base:url}modelid=2&act=edit&id={data:ID}">修改</a>
    <a href="{base:url}modelid=2&act=del&id={data:ID}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>

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
<input type="hidden" name="eformdata_2" id="eformdata_2" value="<%=System.Web.HttpUtility.HtmlEncode(eform.getJson())%>">
</form>
<form id="form_2" name="form_2" method="post" action="">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>姓名：</td>
<td class="content" colspan="3"><span class="eform"><ev:eFormControl ID="M2_F4" Name="M2_F4" ControlType="text" Field="FullName" Width="300px" FieldName="姓名" NotNull="True" MaxLength="50" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>帐号：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F5" Name="M2_F5" ControlType="text" Field="Account" Width="300px" FieldName="帐号" NotNull="True" MaxLength="50" runat="server" /></span></td>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>密码：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F6" Name="M2_F6" ControlType="password" Field="PassWord" Width="300px" FieldName="密码" NotNull="True" MaxLength="50" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>生日：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F7" Name="M2_F7" ControlType="date" Field="Birthday" DateFormat="yyyy-mm-dd" FormatString="{0:yyyy-MM-dd}" FieldName="生日" NotNull="True" DataType="date" runat="server" /></span></td>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>性别：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F8" Name="M2_F8" ControlType="radio" Field="Sex" FieldName="性别" NotNull="True" Options="[{text:男,value:1},{text:女,value:2}]" DefaultValue="1" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>兴趣爱好：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F9" Name="M2_F9" ControlType="checkbox" Field="Hobby" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" NotNull="True" runat="server" /></span></td>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>学历：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F10" Name="M2_F10" ControlType="select" Field="Education" FieldName="学历" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" NotNull="True" runat="server" /></span></td>
</tr>
<tr>
<td class="title">简介：</td>
<td class="content" colspan="3"><span class="eform"><ev:eFormControl ID="M2_F11" Name="M2_F11" ControlType="textarea" Field="Summary" Width="300px" FieldName="简介" NotNull="False" MaxLength="500" runat="server" /></span></td>
</tr>
<tr>
<td class="title">照片：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F12" Name="M2_F12" ControlType="file" Field="Photo" Width="300px" FieldName="照片" NotNull="False" MaxLength="100" runat="server"><img src='{data:photo}'></ev:eFormControl></span></td>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>电子邮件：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F13" Name="M2_F13" ControlType="text" Field="Email" Width="300px" FieldName="电子邮件" NotNull="True" MaxLength="150" DataType="email" runat="server" /></span></td>
</tr>
<tr>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>手机：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F14" Name="M2_F14" ControlType="text" Field="Mobile" Width="300px" FieldName="手机" NotNull="True" MaxLength="20" DataType="mobile" runat="server" /></span></td>
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit" ){Response.Write("<ins>*</ins>");}%>固定电话：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F15" Name="M2_F15" ControlType="text" Field="Telephone" Width="300px" FieldName="固定电话" NotNull="True" MaxLength="50" DataType="tel" runat="server" /></span></td>
</tr>
<tr>
<td class="title">地址：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F16" Name="M2_F16" ControlType="text" Field="Addr" Width="300px" FieldName="地址" NotNull="False" MaxLength="300" runat="server" /></span></td>
<td class="title">邮编：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F17" Name="M2_F17" ControlType="text" Field="Post" Width="300px" FieldName="邮编" NotNull="False" MaxLength="6" runat="server" /></span></td>
</tr>
<tr>
<td class="title">身高：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F18" Name="M2_F18" ControlType="text" Field="Height" FieldName="身高" NotNull="False" DataType="float" Unit="CM" DefaultValue="0" runat="server" /></span></td>
<td class="title">体重：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F19" Name="M2_F19" ControlType="text" Field="Weight" FieldName="体重" NotNull="False" DataType="float" Unit="KG" DefaultValue="0" runat="server" /></span></td>
</tr>
<tr>
<td class="title">简历：</td>
<td class="content" colspan="3"><span class="eform"><ev:eFormControl ID="M2_F20" Name="M2_F20" ControlType="html" Field="Resume" FieldName="简历" NotNull="False" runat="server" /></span></td>
</tr>
<tr>
<td class="title">是否显示：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F21" Name="M2_F21" ControlType="radio" Field="Show" FieldName="是否显示" NotNull="False" Options="[{text:是,value:True},{text:否,value:False}]" ReplaceString="[{text:是,value:True},{text:否,value:False}]" DefaultValue="True" runat="server" /></span></td>
<td class="title">显示顺序：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M2_F22" Name="M2_F22" ControlType="sort" Field="PX" FieldName="显示顺序" NotNull="False" DefaultValue="0" runat="server" /></span></td>
</tr>
<tr>
<td colspan="4" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">
<%
if(Action.Value == "add" || Action.Value == "edit")
{
%>
<a class="button" href="javascript:;" onclick="packform('2');"><span><i class="save"><%=(Action.Value == "add" ? "添加" : "保存")%></i></span></a><%}%>
<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span><i class="back">返回</i></span></a>
</td>
</tr>
</table>
</form>
</asp:PlaceHolder>
</div>
<%}%>
</asp:Content>
