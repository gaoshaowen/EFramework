<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="LoadAction.aspx.cs" Inherits="eFrameWork.Examples.LoadAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="nav">您当前位置：首页 -> 前台加载数据</div>
    <div style="margin:8px;">
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title">姓名：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F1" Name="F1" ControlType="text" Field="FullName" Width="300px" FieldName="姓名" NotNull="True" MaxLength="50" runat="server" /></span></td>
</tr>
<tr>
<td class="title">密码：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F2" Name="F2" ControlType="password" Field="PassWord" Width="300px" FieldName="密码" NotNull="True" MaxLength="50" runat="server" /></span></td>
</tr>
<tr>
<td class="title">生日：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F3" Name="F3" ControlType="date" FormatString="{0:yyyy-MM-dd}" Field="Birthday" FieldName="生日" NotNull="True" DataType="date" runat="server" /></span></td>
</tr>
<tr>
<td class="title">性别：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F4" Name="F4" ControlType="radio" Field="Sex" FieldName="性别" NotNull="True" Options="[{text:男,value:1},{text:女,value:2}]" DefaultValue="1" runat="server" /></span></td>
</tr>
<tr>
<td class="title">兴趣爱好：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F5" Name="F5" ControlType="checkbox" Field="Hobby" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" NotNull="True" runat="server" /></span></td>
<tr>
</tr>
<td class="title">学历：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F6" Name="F6" ControlType="select" Field="Education" FieldName="学历" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" NotNull="True" runat="server" /></span></td>
</tr>
<tr>
<td class="title">简介：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F7" Name="F7" ControlType="textarea" Field="Summary" Width="300px" FieldName="简介" NotNull="False" MaxLength="500" runat="server" /></span></td>
</tr>
<tr>
<td class="title">照片：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F8" Name="F8" ControlType="file" Field="Photo" Width="300px" FieldName="照片" NotNull="False" MaxLength="100" runat="server"><img src='{data:photo}'></ev:eFormControl></span></td>
</tr>
<tr>
<td class="title">身高：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F9" Name="F9" ControlType="text" Field="Height" FieldName="身高" FormatString="{0:F2}" NotNull="False" DataType="float" Unit="CM" DefaultValue="0" runat="server" /></span></td>
</tr>
<tr>
<td class="title">简历：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F10" Name="F10" ControlType="html" Field="Resume" FieldName="简历" NotNull="False" runat="server" /></span></td>
</tr>
<tr>
<td class="title">是否显示：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F11" Name="F11" ControlType="radio" Field="Show" FieldName="是否显示" NotNull="False" Options="[{text:是,value:True},{text:否,value:False}]" ReplaceString="[{text:是,value:True},{text:否,value:False}]" DefaultValue="True" runat="server" /></span></td>
</tr>
<tr>
<td class="title">显示顺序：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="F12" Name="F12" ControlType="sort" Field="PX" FieldName="显示顺序" NotNull="False" DefaultValue="0" runat="server" /></span></td>
</tr>
</table>
</asp:PlaceHolder>
</div>
</asp:Content>
