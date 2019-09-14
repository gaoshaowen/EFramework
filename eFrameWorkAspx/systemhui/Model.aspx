<%@ Page Title="" Language="C#" MasterPageFile="~/Master/huiMain.Master" AutoEventWireup="true" CodeBehind="Model.aspx.cs" Inherits="eFrameWork.systemhui.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%if (!Ajaxget){ %>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> <%=model.ModelInfo["MC"].ToString()%>
<%
if(model.Action.Value=="")
{
    if (model.Power["Add"].ToString().ToLower() == "true")
{
%>
<a class="btn btn-primary radius r" style="margin-top:4px;" <%=(model.Ajax == false ? " href=\"" + model.eForm.getAddURL() + "\"" :  " href=\"javascript:;\" onclick=\"goLink(this);\" ehref=\"" + model.eForm.getAddURL() + "\"") %>><i class="Hui-iconfont">&#xe600;</i> 添加</a>
<%
}
    if (model.Power["Export"].ToString().ToLower() == "true")
{
%>
<a class="btn btn-default radius r" style="margin-top:4px;margin-right:20px;"  href="<%=EKETEAM.FrameWork.eBase.getExportURL()%>" target="_blank"><i class="Hui-iconfont">&#xe644;</i> 导出</a>
<%
}
}
%>
</nav>
<div style="margin-left:10px;">
<%}%>
<asp:Literal ID="LitBody" runat="server" />
<%if (!Ajaxget){ %></div><%}%>
</asp:Content>
