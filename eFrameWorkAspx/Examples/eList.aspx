<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eList.aspx.cs" Inherits="eFrameWork.Examples._eList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eList类</div>
<div style="margin:8px;">
<h1>1.不分页</h1>
<asp:Repeater id="Repeater1" runat="server">
<HeaderTemplate>
<table id="eDataTable" class="eDataTable" border="0" cellpadding="0" cellspacing="0" width="460">
<thead>
<tr>
<td width="60">编号</td>
<td width="150">姓名</td>
<td width="100">身高(CM)</td>
<td width="150">添加时间</td>
</tr>
</thead>
<tbody>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td height="35"><%# Eval("ID").ToString() %></td>
<td><%# Eval("FullName").ToString() %></td>
<td><%# Eval("Height","{0:F2}").ToString() %></td>
<td><%# Eval("addTime","{0:yyyy-MM-dd}").ToString() %></td>
</tr>
</ItemTemplate>
<FooterTemplate>
 </tbody>
</table>
</FooterTemplate>
</asp:Repeater>
<h1>2.自定义分页</h1>
<asp:Repeater id="Repeater2" runat="server">
<HeaderTemplate>
<table id="eDataTable" class="eDataTable" border="0" cellpadding="0" cellspacing="0" width="460">
<thead>
<tr>
<td width="60">编号</td>
<td width="150">姓名</td>
<td width="100">身高(CM)</td>
<td width="150">添加时间</td>
</tr>
</thead>
<tbody>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td height="35"><%# Eval("ID").ToString() %></td>
<td><%# Eval("FullName").ToString() %></td>
<td><%# Eval("Height","{0:F2}").ToString() %></td>
<td><%# Eval("addTime","{0:yyyy-MM-dd}").ToString() %></td>
</tr>
</ItemTemplate>
<FooterTemplate>
 </tbody>
</table>
</FooterTemplate>
</asp:Repeater>
 <div style="text-align:center;width:460px;padding-top:8px;"><asp:Literal id="litPage" runat="server" /></div>

<h1>3.分页控件</h1>
<asp:Repeater id="Repeater3" runat="server">
<HeaderTemplate>
<table id="eDataTable" class="eDataTable" border="0" cellpadding="0" cellspacing="0" width="700">
<thead>
<tr>
<td width="60">编号</td>
<td>姓名</td>
<td>身高(CM)</td>
<td>添加时间</td>
</tr>
</thead>
<tbody>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td height="35"><%# Eval("ID").ToString() %></td>
<td><%# Eval("FullName").ToString() %></td>
<td><%# Eval("Height","{0:F2}").ToString() %></td>
<td><%# Eval("addTime","{0:yyyy-MM-dd}").ToString() %></td>
</tr>
</ItemTemplate>
<FooterTemplate>
 </tbody>
</table>
</FooterTemplate>
</asp:Repeater>
 <div style="text-align:center;width:700px;padding-top:8px;"><ev:ePageControl ID="ePageControl1" PageSize="2" PageNum="9" PageName="PGA" runat="server" /></div>
<h1>4.分页eListControl控件</h1>
<ev:eListControl ID="eListControl1" LineHeight="40" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" width="80" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" FieldName="姓名" Field="FullName" runat="server" />
<ev:eListColumn ControlType="text" FieldName="性别" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn ControlType="text" FieldName="学历" Field="Education" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" runat="server" />
<ev:eListColumn ControlType="text" FieldName="身高" Field="Height" FormatString="{0:F2}" runat="server" />
<ev:eListColumn ControlType="text" FieldName="添加时间" Field="addTime" Width="150" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
<ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
<a href="#{base:url}&act=view&id={data:id}">查看</a>
<a href="#{base:url}&act=edit&id={data:id}">修改</a>
<a href="#{base:url}&act=del&id={data:id}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>
 <div style="text-align:center;padding-top:8px;"><ev:ePageControl ID="ePageControl2" PageSize="2" PageNum="9" runat="server" /></div>

</div>
</asp:Content>
