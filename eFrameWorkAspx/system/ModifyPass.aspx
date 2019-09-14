<%@ Page Title="" Language="C#" MasterPageFile="~/Master/systemMain.Master" AutoEventWireup="true" CodeBehind="ModifyPass.aspx.cs" Inherits="eFrameWork.system.ModifyPass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nav">您当前位置：<a href="Default.aspx">首页</a>&nbsp;->&nbsp;修改密码</div>

<div style="margin:10px;">
<form name="frmaddoredit" id="frmaddoredit" method="post" enctype="multipart/form-data" action="" onsubmit="return checkfrm(this);">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
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
		<td colspan="2" class="title" style="text-align:left;padding-left:125px;">
		<a class="button" href="javascript:;" onclick="submitForm(frmaddoredit);"><span>修改</span></a>
		<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span>返回</span></a>
		</td>
	</tr>
</table>
</form>
</div>
</asp:Content>
