<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eUser.aspx.cs" Inherits="eFrameWork.Examples._eUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eUser类</div>
<div style="margin:8px;">
<%if(user.Logined){ %>
<a class="button" href="?act=loginout" style="margin:10px;"><span><i>登录成功,点此退出!</i></span></a>
<%}else{ %>
<a class="button" href="?act=login" style="margin:10px;"><span><i>尚未登录,点此登录!</i></span></a>
<%} %>
<h1>登录结果：</h1>
    <p style="padding:10px;line-height:25px;"><asp:Literal id="litBody" runat="server" /></p>
</div>
</asp:Content>
