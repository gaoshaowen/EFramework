<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Model.aspx.cs" Inherits="eFrameWork.Mobile.Model" %>
<%@ Register src="Menu.ascx" tagname="Menu" tagprefix="uc1" %>
<!DOCTYPE html>
<html>
<head>
    <title><%=model.ModelInfo["MC"].ToString()%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<script>var ModelID = "<%=ModelID%>";</script>
<script src="../Scripts/Init.js?beacon=7"></script>
<script src="../Plugins/eMenu/mobile/js/jquery-sliding-menu.js?beacon=7"></script>
<body>
<div class="header"><%=model.ModelInfo["MC"].ToString()%>
<div class="menu-bar" onClick="_showmenu();"></div>
<%
if(model.Action.Value=="" && model.Power["Add"].ToString().ToLower() == "true")
{
%>
<a class="menu-add" href="<%=model.eForm.getAddURL()%>">+</a>
<%
}
else
{
%>
<a class="menu-back" href="javascript:history.back();">返回</a>
<%}%>
</div>
<uc1:Menu ID="Menu1" runat="server" />
<div style="height:50px;"></div>
<asp:Literal ID="LitBody" runat="server" />
</body>
</html>
