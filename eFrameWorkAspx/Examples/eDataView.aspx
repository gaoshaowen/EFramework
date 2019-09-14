<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eDataView.aspx.cs" Inherits="eFrameWork.Examples.eDataView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eDataView控件</div>
<div style="margin:8px;">
<h1>1.不分页:</h1>
<ev:eDataView ID="eDataView1" DataID="07c0c0e6-3fa7-4646-93c9-5e74447b3611" runat="server" />
<h1>2.分页:</h1>


<ev:eDataView ID="eDataView2" DataID="99a5650a-a71e-47a2-91bb-6b5b9b616c6d" runat="server" />

</div>
</asp:Content>
