﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eOleDB.aspx.cs" Inherits="eFrameWork.Examples._eOleDB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eOleDB类</div>
<div style="margin:8px;">
<h1>1.添加记录</h1>
<a class="button" href="?act=add" style="margin:10px;"><span><i>添加</i></span></a>
<h1>2.修改记录</h1>
<a class="button" href="?act=edit" style="margin:10px;"><span><i>修改</i></span></a>
<h1>3.删除记录</h1>
<a class="button" href="?act=del" style="margin:10px;" onclick="javascript:return confirm('确认要删除吗？');"><span><i>删除</i></span></a>

<h1>4.读取记录（不分页）</h1>
<a class="button" href="?act=list" style="margin:10px;"><span><i>读取</i></span></a>

<h1>5.读取记录（分页）</h1>
<a class="button" href="?act=page" style="margin:10px;"><span><i>读取</i></span></a>

<h1>执行结果：</h1>
    <p style="padding:10px;line-height:25px;"><asp:Literal id="litBody" runat="server" /></p>
</div>
</asp:Content>
