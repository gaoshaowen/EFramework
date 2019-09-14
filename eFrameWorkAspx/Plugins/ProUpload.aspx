<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProUpload.aspx.cs" Inherits="eFrameWork.Plugins.ProUpload" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>附件上传</title>
</head>
<script>
function $(objName){
	try{
		if (document.getElementById){
			return eval('document.getElementById("'+objName+'")');
		}
		else{
			return eval('document.all.'+objName);
		}
	}
	catch(e){}
};
function del()
{
if(confirm("确认要删除文件吗？"))
	return true;
else
	return false;
}
function checkfrm(frm)
{
    if (frm.imgFile.value.length == 0)
	{
		alert("请选择要上传的文件!");
		frm.imgFile.focus();
		return false;
	}
	return true;
}
</script>
<style>
body{margin:0px;padding-top:0px;font-size:12px;}
#imgFile,#button{font-size:12px;mar3gin-top:-1px;height:22px;l6ine-height:18px;font-family:'宋体';}
#button{border:1px solid #cccccc;}
</style>
<body bgcolor="#ffffff">
<%
if(Request.QueryString["act"]==null)
{
%>
<form method="post" enctype="multipart/form-data" id="Form1" style="margin:0px;padding:0px;" onSubmit="return checkfrm(this);">
<INPUT type="file" id="imgFile" name="imgFile" runat="server" onChange2="fileclick();" style="width:150px;overflow:hidden;">&nbsp;<INPUT name="button" type="submit" id="button" value=" 上 传 ">
<input type="hidden" name="act" id="act" value="save">
<input type="hidden" name="formhost" id="formhost" value="<%=formhost %>">
</form>
<%
}
%>
</body>
</html>