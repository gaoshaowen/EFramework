<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eFrameWork.Mobile.Default" %>
<%@ Register src="Menu.ascx" tagname="Menu" tagprefix="uc1" %>
<!DOCTYPE html>
<html>
<head>
    <title>首页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<script>var ModelID = "";</script>
<script src="../Scripts/Init.js?beacon=7"></script>
<script src="../Plugins/eMenu/mobile/js/jquery-sliding-menu.js?beacon=7"></script>
<body>
<div class="header">首页
<div class="menu-bar" onClick="_showmenu();"></div>
<a class="menu-add">&nbsp;</a>
</div>
<uc1:Menu ID="Menu1" runat="server" />
<div style="height:50px;"></div>

<div style="margin:10px;line-height:28px;font-size:13px;">    
    <asp:Literal id="litBody" runat="server" />
</div>
</body>
</html>