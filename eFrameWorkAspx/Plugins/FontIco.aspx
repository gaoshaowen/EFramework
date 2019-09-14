<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FontIco.aspx.cs" Inherits="eFrameWork.Plugins.FontIco" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <%if(1==2){ %>
    <link hre4f="http://netdna.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
	<link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <%}else{ %>
     <link href="font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <%} %>
</head>
<style>
.fa-hover{padding:6px 0px 6px 0px;min-width:30px;color:#111111;border:1px solid #cccccc; display:inline-block; overflow:hidden; text-align:center; }
.fa-hover:hover{ background-color:#1D9D74; color:#ffffff;}
</style>

<body>
<asp:Literal ID="LitBody" runat="server" />
</body>
</html>