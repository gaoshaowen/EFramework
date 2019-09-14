<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accessorys.aspx.cs" Inherits="eFrameWork.system.Accessorys" %>
<%
if(act=="getjson")
{
    Response.Write("{\"ma_f1\":\"" + sfz + "\",\"ma_f2\":\"" + fkb + "\",\"ma_f3\":\"" + byz + "\"}");
}
else{ %>
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title">身份证：</td>
<td class="content f30">
<span class="eform">
<ev:eFormControl ID="ma_f1" Name="ma_f1" ControlType="file" Field="SFZ" FieldName="身份证" NotNull="False" Unit="电子版" Text="<img src='{data:sfz}'>" runat="server" />
</span>
</td>
</tr>
<tr>
<td class="title">户口薄：</td>
<td class="content f30">
<span class="eform">
<ev:eFormControl ID="ma_f2" Name="ma_f2" ControlType="file" Field="FKB" FieldName="户口薄" NotNull="False" Unit="电子版" Text="<img src='{data:FKB}'>" runat="server" />
</span>
</td>
</tr> 

<tr>
<td class="title">毕业证：</td>
<td class="content f30">
<span class="eform">   
<ev:eFormControl ID="ma_f3" Name="ma_f3" ControlType="file" Field="BYZ" FieldName="毕业证" NotNull="False" Unit="电子版" Text="<img src='{data:BYZ}'>" runat="server" />
</span>
</td>
</tr>
</table>
</asp:PlaceHolder>
<%}%>