<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillData.aspx.cs" Inherits="eFrameWork.Plugins.FillData" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="eButton/default/style.css" rel="stylesheet" type="text/css" />
	<link href="ePanel/default/style.css" rel="stylesheet" type="text/css" />
	<link href="ePager/default/style.css" rel="stylesheet" type="text/css" />
	<link href="eDataView/default/style.css" rel="stylesheet" type="text/css" />
	<link href="eDataTable/default/style.css" rel="stylesheet" type="text/css" />
</head>
<script>var ModelID = "<%=ModelID%>";</script>
<script src="../Scripts/Init.js"></script>
<body>
    <asp:Literal ID="LitBody" runat="server" />
</body>
</html>
