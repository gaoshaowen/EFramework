<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eFrameWork.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>ASPX首页</title>
    <style>
        body
        {
            margin:20px;
        }
        a
        {
            text-decoration:none;display:inline-block;line-height:28px;margin-left:10px;color:#333333;
        }
            a:hover
            {
                color:#ff6a00;
            }
    </style>
</head>
<body>
  <asp:Literal ID="LitDBState" runat="server" />
  <a href="Examples/" target="_blank">示列</a><br />
  <a href="Manage/" target="_blank">开发平台</a><br />
  <a href="system/" target="_blank">系统管理</a><br /> 
  <a href="systemhui/" target="_blank">系统管理(h-ui前端框架)</a><br /> 
</body>
</html>
