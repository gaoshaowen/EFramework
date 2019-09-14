<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="Cache.aspx.cs" Inherits="eFrameWork.Manage.Cache" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 缓存管理</div>
<div style="margin:6px;line-height:25px;font-size:13px;">
    <asp:Literal ID="LitBody" runat="server" />
</div>
</asp:Content>