<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="easyUIDataGrid.aspx.cs" Inherits="eFrameWork.Examples.easyUIDataGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> easyUI DataGrid 应用</div>
<div style="margin:8px;">
<link rel="stylesheet" type="text/css" href="http://www.jeasyui.net/Public/js/easyui/themes/default/easyui.css">
<link rel="stylesheet" type="text/css" href="http://www.jeasyui.net/Public/js/easyui/themes/icon.css">
<script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
<script type="text/javascript" src="http://www.jeasyui.net/Public/js/easyui/jquery.easyui.min.js"></script>





<h3 style="margin-bottom:8px;">示例一</h3>

<ev:eListControl ID="eDataTable" PageSize="5" class="easyui-datagrid" style="width:100%;height:260px;" Attributes="url=&quot;easyUIDataGrid.aspx?obj=edatatable&quot; rownumbers=&quot;true&quot; pagination=&quot;true&quot; method=&quot;get&quot; pageList=&quot;[5,10, 15,20, 30, 40]&quot; singleSelect=&quot;true&quot;" runat="server" >
<ev:eListColumn FieldName="序号" Name="F1" width="80" runat="server">{row:number}</ev:eListColumn>
<ev:eListColumn FieldName="姓名" Name="F2" Field="FullName" runat="server" />
<ev:eListColumn FieldName="性别" Name="F3" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn FieldName="密码" Name="F4" Field="PassWord" ControlType="PassWord" runat="server" />
<ev:eListColumn FieldName="学历" Name="F5" Field="Education" Width="100" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" runat="server" />
<ev:eListColumn FieldName="身高" Name="F6" Field="Height" Width="100" FormatString="{0:F2}" runat="server" />
<ev:eListColumn FieldName="添加时间" Name="F7" Field="addTime" Width="150" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
</ev:eListControl>






<h3 style="margin-top:8px;margin-bottom:8px;">示例二</h3>

<ev:eListControl ID="DGDics" PageSize="5" class="easyui-datagrid" style="width:100%;height:260px;" Attributes="url=&quot;easyUIDataGrid.aspx?obj=DGDics&quot; rownumbers=&quot;true&quot; pagination=&quot;true&quot; method=&quot;get&quot; pageList=&quot;[5,10, 15,20, 30, 40]&quot; singleSelect=&quot;true&quot; loadMsg=&quot;正在加载,请稍侯!&quot; beforePageText=&quot;AA&quot;" runat="server" >
<ev:eListColumn FieldName="序号" Name="F1" width="80" runat="server">{row:number}</ev:eListColumn>
<ev:eListColumn FieldName="名称" Name="F2" width="120" Field="Name" runat="server" />
<ev:eListColumn controltype="px" FieldName="顺序" Name="F6" Field="PX" Width="100" runat="server" />
<ev:eListColumn FieldName="添加时间" Name="F7" Field="addTime" Width="150" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
</ev:eListControl>




</div>
</asp:Content>
