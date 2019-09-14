<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartOne.aspx.cs" Inherits="eFrameWork.systemhui.PartOne" %>
<%
if(act=="getjson")
{
    Response.Write("{\"ma_f1\":\"" + dz + "\",\"ma_f2\":\"" + gddh + "\"}");
}
else if(act=="add" || act=="edit"){ %>
<table width="300" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="80" />
<col />
</colgroup>
<tr>
<td class="title">地址：</td>
<td class="content f30"><span class="eform"><input class="text" name="ma_f1" type="text" id="ma_f1" value="<%=dz %>" /></span></td>
</tr>
<tr>
<td class="title">电话：</td>
<td class="content f30"><span class="eform"><input class="text" name="ma_f2" type="text" id="ma_f2" value="<%=gddh %>" /></span></td>
</tr>
</table>
<%}else{ %>
<table width="300" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="80" />
<col />
</colgroup>
<tr>
<td class="title">地址：</td>
<td class="content f30"><span class="eform"><%=dz %></span></td>
</tr>
<tr>
<td class="title">电话：</td>
<td class="content f30"><span class="eform"><%=gddh %></span></td>
</tr>
</table>
<%}%>
<div style="margin-top:10px;color:#555;"><b>说明：</b>自定义页面 1 v 1</div>