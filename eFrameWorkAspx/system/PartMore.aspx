<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartMore.aspx.cs" Inherits="eFrameWork.system.PartMore" %>

<%
if (act == "getjson")
{
    Response.Write( getJson );
}
else if(act == "subadd" || act == "subview" )
{
%>
<div style="margin:10px;">
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<form id="form_<%=modelid %>" name="form_<%=modelid %>" method="post" action="">
<input type="hidden" id="ID" name="ID" value="">
<input type="hidden" id="Delete" name="Delete" value="false">
<input type="hidden" id="Index" name="Index" value="-1">
<table width="320" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title"><ins>*</ins>姓名：</td>
<td class="content"><span id="span_m2_f1" class="eform"><input name="m2_f1" type="text" id="m2_f1" class="text" value="" /></span></td>
</tr>
<tr>
<td class="title"><ins>*</ins>性别：</td>
<td class="content"><span id="span_m2_f2" class="eform"><input name="m2_f2" type="text" id="m2_f2" class="text" value="" /></span></td>
</tr>
<tr>
<td class="title"><ins>*</ins>电话：</td>
<td class="content"><span id="span_m2_f3" class="eform"><input name="m2_f3" type="text" id="m2_f3" class="text" value="" /></span></td>
</tr>
</table>
</form>
</asp:PlaceHolder>
</div>
<%
}
else
{
%>
<%if(1==13){%>
<textarea name="eformdata_<%=modelid %>" id="eformdata_<%=modelid %>" style="width:90%;"><%=System.Web.HttpUtility.HtmlEncode(getJson) %></textarea>
<%}else{%>
<div style="margin-bottom:10px;color:#555;"><b>说明：</b>以下内容为自定义页面 1 v n</div>
<input type="hidden" id="eformdata_<%=modelid %>" name="eformdata_<%=modelid %>" value="<%=System.Web.HttpUtility.HtmlEncode(getJson) %>">
<%}%>
<table id="eformlist_<%=modelid %>" width="<%=(act == "viewlist" ? "320" : "370")%>" class="eDataTable" cellpadding="0" cellspacing="0">
<thead>
<tr>
<%if(act != "view"){%><td width="50"><a href="javascript:;" onclick="form_add(this,'<%=modelid %>','<%=modelName %>','<%=frmwidth%>','<%=frmheight%>','<%=aspxFile %>?act=subadd');" class="btnadd">&nbsp;</a></td><%}%>
<td width="120">姓名</td>
<td width="80">性别</td>
<td width="120">电话</td>
</tr>
</thead>
<tbody>
</tbody>
<tfoot style="display:none;">
<tr eRowID="" onclick="<%=(act == "view" ? "form_View(this,'" + modelid + "','" + modelName + "','" + frmwidth+ "','" + frmheight + "','" + aspxFile + "?act=subview');" : "form_edit(this,'" + modelid + "','" + modelName + "','" + frmwidth + "','" + frmheight + "','" + aspxFile + "?act=subadd');")%>">
<%if(act != "view"){%><td name=""><a href="javascript:;" onclick="form_delete(this,'<%=modelid %>','删除');" class="btndel">&nbsp;</a></td><%}%>
<td name="m2_f1" height="30">&nbsp;</td>
<td name="m2_f2">&nbsp;</td>
<td name="m2_f3">&nbsp;</td>
</tr>
</tfoot>
</table>
<script>
    var input = getobj("eformdata_<%=modelid %>");
    var model = input.value.toJson();
    model = model.get("eformdata_<%=modelid %>");
    if (model.length > 0)
    {
        JsonToTable(model, "eformlist_<%=modelid %>");
    }
</script>
<%}%>