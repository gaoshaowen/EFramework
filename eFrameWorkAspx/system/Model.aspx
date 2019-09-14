<%@ Page Title="" Language="C#" MasterPageFile="~/Master/systemMain.Master" AutoEventWireup="true" CodeBehind="Model.aspx.cs" Inherits="eFrameWork.system.Model" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%if (!Ajaxget)
{ %>
<div class="nav">您当前位置：<a href="Default.aspx">首页</a>&nbsp;->&nbsp;<%=model.ModelInfo["MC"].ToString()%>
<%
if(model.Action.Value=="")
{
    if (model.Power["Add"].ToString().ToLower() == "true")
{
%>
<a id="btn_add" style="float:right;margin-top:10px;margin-left:20px;" class="button" <%=(model.Ajax == false ? " href=\"" + model.eForm.getAddURL() + "\"" :  " href=\"javascript:;\" onclick=\"goLink(this);\" ehref=\"" + model.eForm.getAddURL() + "&ajaxget=true\"") %>><span><i class="add"><%= model.ModelInfo["AddText"].ToString()%></i></span></a>
<%
}
    if (model.Power["Export"].ToString().ToLower() == "true")
{
%>
<a style="float:right;margin-top:10px;margin-left:20px;" class="button" href="<%=eBase.getExportURL()%>" target="_blank"><span><i class="export">导出</i></span></a>
<%
}
}
%></div>
<%}%>
<asp:Literal ID="LitBody" runat="server" />
</asp:Content>