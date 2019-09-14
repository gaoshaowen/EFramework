<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyPass.aspx.cs" Inherits="eFrameWork.Mobile.ModifyPass" %>
<%@ Register src="Menu.ascx" tagname="Menu" tagprefix="uc1" %>
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<script>var ModelID = "";</script>
<script src="../Scripts/Init.js"></script>
<script src="../Plugins/eMenu/m1/js/jquery-sliding-menu.js"></script>
<body>
<div class="header">修改密码
<div class="menu-bar" onClick="_showmenu();"></div>
<a class="menu-back" href="javascript:history.back();">返回</a>
</div>
<uc1:Menu ID="Menu1" runat="server" />
<div style="height:50px;"></div>

<div style="margin:10px;">
<form name="frmaddoredit" id="frmaddoredit" method="post" enctype="multipart/form-data" action="" onsubmit="return checkfrm(this);">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="100" />
<col />
</colgroup>
    <tr>
      <td width="120" class="title"><font color="#FF0000"> *</font>&nbsp;旧密码：</td>
      <td class="content"><span class="eform"><input name="f1" type="password" class="text" id="f1" value="" fieldname="旧密码" notnull="true" style="width:150px;" /></span></td>
    </tr>
    <tr>
      <td class="title"><font color="#FF0000"> *</font>&nbsp;新密码：</td>
      <td class="content"><span class="eform"><input name="f2" type="password" class="text" id="f2" value="" fieldname="新密码" notnull="true" style="width:150px;" /></span></td>
    </tr>
    <tr>
      <td class="title"><font color="#FF0000"> *</font>&nbsp;确认密码：</td>
      <td class="content"><span class="eform"><input name="f3" type="password" class="text" id="f3" value="" fieldname="确认密码" notnull="true" equal="f2" style="width:150px;" /></span></td>
    </tr>
	<tr>
		<td colspan="2" class="title" style="text-align:left;padding-left:105px;">
		<a class="button" href="javascript:;" onclick="submitForm(frmaddoredit);"><span>修改</span></a>
		<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span>返回</span></a>
		</td>
	</tr>
</table>
</form>
</div>
</body>
</html>
