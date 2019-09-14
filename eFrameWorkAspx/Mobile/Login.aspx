<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eFrameWork.Mobile.Login" %>
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
</head>
<link href="../Plugins/eLogin/default/style.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/eketeam.js?beacon=7"></script>
<script src="../Plugins/eClient/eClient.js?beacon=7"></script>
<script>
    function getrnd() {
        document.getElementById("rndpic").src = "../Plugins/RndPic.aspx?t=" + now();
    };
</script>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="marg3in:auto;vertical-align: middle; position:fixed;top:30%;">
          <form name="form1" method="post" action="" onSubmit="return checkfrm(this);">
            <tr>
              <td width="100" height="46" align="right"><span style="font-size:12px;color:#333;">登陆帐号：</span></td>
              <td>
                <input type="text"  name="yhm"  id="yhm" class="text" style="width:160px;" notnull="true" fieldname="登陆帐号" value=""  autocomplete="off" ></td>
            </tr>
            <tr>
              <td height="46" align="right"><span style="font-size:12px;color:#333;">登录密码：</span></td>
              <td><input type="password"  name="mm"  id="mm" class="text" style="width:160px;" notnull="true" fieldname="登录密码" value="" autocomplete="off" ></td>
            </tr>
            <tr>
              <td height="46" align="right"><span style="font-size:12px;color:#333;">验证码：</span></td>
              <td><input type="text"  name="yzm" id="yzm" class="text" style="width:93px;" notnull="true" fieldname="验证码" value="" onFocus="getrnd();" autocomplete="off" >
                  <img id="rndpic" style="cursor:pointer;" onClick="getrnd();" height="18" src="../images/none.gif" width="45" align="absMiddle" border="0"></td>
            </tr>
            <tr>
              <td height="46">&nbsp;</td>
              <td><input type="submit" name="Submit" value=""  class="btn"></td>
            </tr>
          </form>
        </table>
</body>
</html>