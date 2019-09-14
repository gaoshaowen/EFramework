<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="ePicture.aspx.cs" Inherits="eFrameWork.Examples._ePicture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> ePicture图片处理类</div>
<div style="margin:8px;">
<h1>1.存为JPG</h1>
<a class="button" href="?act=step1" style="margin:10px;"><span><i>保存</i></span></a>

<h1>2.按宽缩放</h1>
<a class="button" href="?act=step2" style="margin:10px;"><span><i>缩放</i></span></a>

<h1>3.按高缩放</h1>
<a class="button" href="?act=step3" style="margin:10px;"><span><i>缩放</i></span></a>

<h1>4.指定尺寸</h1>
<a class="button" href="?act=step4" style="margin:10px;"><span><i>缩放</i></span></a>

<h1>5.剪切图片</h1>
<a class="button" href="?act=step5" style="margin:10px;"><span><i>剪切</i></span></a>

<h1>6.缩略图</h1>
<a class="button" href="?act=step6" style="margin:10px;"><span><i>生成</i></span></a>

<h1>执行结果：</h1>
    <p style="padding:10px;line-height:25px;"><asp:Literal id="litBody" runat="server" /></p>
</div>
</asp:Content>