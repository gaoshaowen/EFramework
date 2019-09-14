<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eFrameWork.systemhui.Login" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%= EKETEAM.FrameWork.eConfig.getString("systemName") %></title>
    <META HTTP-EQUIV="imagetoolbar" CONTENT="NO">
</head>
<style>
html,body{height:100%;}
body{margin:0px;}
</style>
<link href="../Plugins/eLogin/default/style.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/eketeam.js"></script>
<script src="../Plugins/eClient/eClient.js"></script>
<script>
    function getrnd() {
        document.getElementById("rndpic").src = "../Plugins/RndPic.aspx?t=" + now();
    };
</script>
<body>
    <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>
	
	
<table border="0" align="center" cellpadding="0" cellspacing="0" class="elogin">
  <tr>
    <td class="t_l"></td>
    <td class="t_c"></td>
    <td class="t_r"></td>
  </tr>
  <tr>
    <td class="c_l"></td>
    <td class="c_c">
		<span class="title"></span>
        <table width="500" border="0" cellspacing="0" cellpadding="0">
          <form name="form1" method="post" action="" onSubmit="return checkfrm(this);">
            <tr>
              <td width="142" height="36" align="right"><span style="font-size:12px;color:#333;">登陆帐号：</span></td>
              <td width="358">
                <input type="text"  name="yhm"  id="yhm" class="text" style="width:160px;" notnull="true" fieldname="登陆帐号" value=""  autocomplete="off" ></td>
            </tr>
            <tr>
              <td height="36" align="right"><span style="font-size:12px;color:#333;">登录密码：</span></td>
              <td><input type="password"  name="mm"  id="mm" class="text" style="width:160px;" notnull="true" fieldname="登录密码" value="" autocomplete="off" ></td>
            </tr>
            <tr>
              <td height="36" align="right"><span style="font-size:12px;color:#333;">验证码：</span></td>
              <td><input type="text"  name="yzm" id="yzm" class="text" style="width:93px;" notnull="true" fieldname="验证码" value="" onfocus="getrnd();" autocomplete="off" >
                  <img id="rndpic" style="cursor:pointer;" onclick="getrnd();" height="18" src="../images/none.gif" width="45" align="absMiddle" border="0"></td>
            </tr>
            <tr>
              <td height="36">&nbsp;</td>
              <td><input type="submit" name="Submit" value=""  class="btn"></td>
            </tr>
          </form>
        </table></td>
    <td class="c_r"></td>
  </tr>
  <tr>
    <td class="b_l"></td>
    <td class="b_c"></td>
    <td class="b_r"></td>
  </tr>
</table>

	
	</td>
  </tr>
</table>
</body>
</html>