<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eOrderby.aspx.cs" Inherits="eFrameWork.Examples._eOrderby" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eListControl 控件排序</div>
<div style="margin:8px;">
<ev:eListControl ID="eDataTable" LineHeight="32" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" Width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" Field="FullName" FieldName="姓名" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="Sex" FieldName="性别" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="date" Field="Birthday" FieldName="生日" FormatString="{0:yyyy-MM-dd}" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" FormatString="{0:yyyy-MM-dd HH:mm:ss}" OrderBy="true" runat="server" />
</ev:eListControl>
<div style="margin:8px;"><ev:ePageControl ID="ePageControl1" PageSize="10" PageNum="9" runat="server" /></div>
</div>
</asp:Content>
