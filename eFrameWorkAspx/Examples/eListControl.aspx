<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eListControl.aspx.cs" Inherits="eFrameWork.Examples._eListControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eListControl控件</div>
<div style="margin:8px;">
<h1>1.不分页:</h1>
<ev:eListControl ID="eListControl1" LineHeight="40" runat="server" >
<ev:eListColumn FieldName="序号" width="80" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn FieldName="姓名" Field="FullName" runat="server" />
<ev:eListColumn FieldName="性别" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn FieldName="学历" Field="Education" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" runat="server" />
<ev:eListColumn FieldName="身高" Field="Height" FormatString="{0:F2}" runat="server" />
<ev:eListColumn FieldName="添加时间" Field="addTime" Width="150" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
<ev:eListColumn FieldName="操作" Width="130" runat="server">
<a href="#{base:url}&act=view&id={data:id}">查看</a>
<a href="#{base:url}&act=edit&id={data:id}">修改</a>
<a href="#{base:url}&act=del&id={data:id}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>

<h1>2.分页:</h1>
<ev:eListControl ID="eListControl2" LineHeight="40" runat="server" >
<ev:eListColumn FieldName="序号" width="80" runat="server">{row:number}</ev:eListColumn>
<ev:eListColumn FieldName="姓名" Field="FullName" runat="server" />
<ev:eListColumn FieldName="性别" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn FieldName="学历" Field="Education" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" runat="server" />
<ev:eListColumn FieldName="身高" Field="Height" FormatString="{0:F2}" runat="server" />
<ev:eListColumn FieldName="添加时间" Field="addTime" Width="150" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
<ev:eListColumn FieldName="操作" Width="130" runat="server">
<a href="#{base:url}&act=view&id={data:id}">查看</a>
<a href="#{base:url}&act=edit&id={data:id}">修改</a>
<a href="#{base:url}&act=del&id={data:id}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>
 <div style="text-align:center;padding-top:8px;"><ev:ePageControl ID="ePageControl1" PageName="PG" PageSize="2" PageNum="9" runat="server" /></div>

<h1>3.搜索:</h1>
<asp:PlaceHolder ID="eSearchControlGroup" runat="server">
<form name="frmsearch" id="frmsearch" method="post" onsubmit="return goSearch(this);" action="<%=elist3.getSearchURL()%>">
  <table wid3th="200" border="0" cellspacing="0" cellpadding="0" class="eDataView" style="margin-top:6px;margin-bottom:8px;">
  <colgroup>
<col width="100" />
<col />
<col width="100" />
<col />
<col width="100" />
<col />
<col />
</colgroup>
    <tr>
      <td class="title">姓名：</td>
      <td class="content"><span class="eform"><ev:eSearchControl ID="s1" ControlType="text" Field="FullName" Operator="like" DataType="string" width="200" runat="server" /></span></td>
      <td class="title">性别：</td>
      <td class="content"><span class="eform"><ev:eSearchControl ID="s2" ControlType="radio" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" Operator="=" DataType="int" runat="server" /></span></td>
      <td class="title">学历：</td>
      <td class="content"><span class="eform"><ev:eSearchControl ID="s3" ControlType="select" Field="Education" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindCondition="delTag=0 and show=1 and PID=1" BindOrderBy="PX" Operator="=" DataType="int" runat="server" /></td>
      <td class="title" style="text-align:center;"><a class="button" href="javascript:;" onclick="if(frmsearch.onsubmit()!=false){frmsearch.submit();}"><span><i class="search">搜索</i></span></a></td>
    </tr>
  </table>
 
</form>
</asp:PlaceHolder>


<ev:eListControl ID="eDataTable" LineHeight="40" runat="server" >
<ev:eListColumn FieldName="序号" width="80" runat="server">{row:number}</ev:eListColumn>
<ev:eListColumn FieldName="姓名" Field="FullName" runat="server" />
<ev:eListColumn FieldName="性别" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn FieldName="密码" Field="PassWord" ControlType="PassWord" runat="server" />
<ev:eListColumn FieldName="学历" Field="Education" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" runat="server" />
<ev:eListColumn FieldName="身高" Field="Height" FormatString="{0:F2}" runat="server" />
<ev:eListColumn FieldName="添加时间" Field="addTime" Width="150" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
<ev:eListColumn FieldName="操作" Width="130" runat="server">
<a href="#{base:url}&act=view&id={data:id}">查看</a>
<a href="#{base:url}&act=edit&id={data:id}">修改</a>
<a href="#{base:url}&act=del&id={data:id}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>
<div style="text-align:center;padding-top:8px;"><ev:ePageControl ID="ePageControl2" PageSize="2" PageNum="9" runat="server" /></div>


</div>
</asp:Content>
