<%@ Page Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="ModelImport.aspx.cs" Inherits="eFrameWork.Manage.ModelImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> <a href="Models.aspx">模块管理</a></div>
<div style="margin:10px;">
<form method="post" enctype="multipart/form-data" id="Form1">
    <INPUT type="file" id="imgFile" name="imgFile" style="width:160px;overflow:hidden;" runat="server">&nbsp;<INPUT name="button" type="submit" id="button" value=" 导 入 ">
	<input name="act" type="hidden" id="act" value="save">
</form>
</div>
</asp:Content>