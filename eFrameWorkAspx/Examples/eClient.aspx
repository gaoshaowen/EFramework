<%@ Page Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eClient.aspx.cs" Inherits="eFrameWork.Examples.eClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> 客户端数据验证</div>
<div style="margin:8px;">
<script>
function check(frm)
{
	alert("框架验证完毕且全部通过!现在是自定义补充验证!");
    return false;
};
</script>
	<form id="form1" name="form1" method="post" action="" onSubmit="return check(this);" >
	<table width="95%" border="0" cellpadding="0" cellspacing="0" id="tbb" class="eDataView">

  <tr>
    <td class="title" width="150"><font color="#ff0000">*</font> 字符串(长度:2-5)：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f1" class="text" fieldname="字符串2-5" notnull="true" datatype="string" minlength="2" maxlength="5" autocomplete="off" value="" /></span>
    <span class="spantip">如：abc</span></td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 密码：</td>
    <td class="content">
    <span class="eform"><input type="password" name="f2" class="text" fieldname="密码" notnull="true" datatype="string" minlength="6" maxlength="20" autocomplete="off" value="" />
    </span>
    </td>
  </tr>
   <tr>
    <td class="title"><font color="#ff0000">*</font> 确认密码：</td>
    <td class="content">
    <span class="eform"><input type="password" name="f3" equal="f2" class="text" fieldname="确认密码" notnull="true" datatype="string" minlength="6" maxlength="20" autocomplete="off" value="" /></span>
    </td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 整型5-10：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f4" class="text" fieldname="整型5-10" notnull="true" datatype="int" minvalue="5" maxvalue="10" autocomplete="off" value="" /></span>
	<span class="spantip">如：6</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 小数1.1-9.9：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f5" class="text" fieldname="小数1.1-9.9" notnull="true" datatype="float" minvalue="1.1" maxvalue="9.9" autocomplete="off" value="" /></span>
	<span class="spantip">如：5.5</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 生效日期：</td>
    <td class="content">
    <span class="eform"><input type="text" id="f6" name="f6" onfocus="this.blur();"  class="text date" onclick="eCalendar.show('yyyy-mm-dd');" fieldname="生效日期" notnull="true" datatype="date" minvalue="2017-3-12" maxvalue="2017-12-20" autocomplete="off" value="" readOnly>
    </span>
	<span class="spantip">范围：2017-3-12 至 2017-12-20</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 邮编：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f7" class="text" fieldname="邮编" notnull="true" datatype="int" maxlength="6" minlength="6" autocomplete="off" value="" /></span>
	<span class="spantip">如：650000</span></td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 邮箱：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f8" class="text" fieldname="邮箱" notnull="true" datatype="email" autocomplete="off" value="" /></span>
	<span class="spantip">如：abc@qq.com</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 网址：</td>
    <td class="content">
   <span class="eform"><input type="text" name="f9" class="text" fieldname="网址" notnull="true" datatype="url" style="width:360px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：http://www.eketeam.com/</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 固定电话：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f10" class="text" fieldname="固定电话" notnull="true" datatype="tel" style="width:150px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：0871-68888888</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 手机：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f11" class="text" fieldname="手机" notnull="true" datatype="mobile" style="width:150px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：13800881305</span>	</td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 身份证号：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f12" class="text" fieldname="身份证号" notnull="true" datatype="cert" style="width:150px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：532128198012053658</span></td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 姓名(纯中文)：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f13" class="text" fieldname="姓名" notnull="true" datatype="cn" style="width:150px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：王先生</span></td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> 用户名(字母数字)：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f14" class="text" fieldname="用户名" notnull="true" datatype="en" style="width:150px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：abcd</span></td>
  </tr>
  <tr>
    <td class="title"><font color="#ff0000">*</font> IP地址：</td>
    <td class="content">
    <span class="eform"><input type="text" name="f15" class="text" fieldname="IP地址" notnull="true" datatype="ip" style="width:150px;" autocomplete="off" value="" /></span>
	<span class="spantip">如：192.168.0.123</span></td>
  </tr>
  <tr>
    <td class="title" style="text-align:left;padding-left:60px;" colspan="2">
	<a class="button" href="javascript:;" onclick="if(form1.onsubmit()!=false){form1.submit();}"><span>提交</span></a>	</td>
  </tr>
</table>
</form>

</div>
</asp:Content>