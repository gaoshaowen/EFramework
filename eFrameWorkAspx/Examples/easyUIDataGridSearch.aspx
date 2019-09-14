<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="easyUIDataGridSearch.aspx.cs" Inherits="eFrameWork.Examples.easyUIDataGridSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> easyUI DataGrid 带查询应用</div>
<div style="margin:8px;">
<link rel="stylesheet" type="text/css" href="http://www.jeasyui.net/Public/js/easyui/themes/default/easyui.css">
<link rel="stylesheet" type="text/css" href="http://www.jeasyui.net/Public/js/easyui/themes/icon.css">
<script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
<script type="text/javascript" src="http://www.jeasyui.net/Public/js/easyui/jquery.easyui.min.js"></script>

 <dl id="eSearchBox" class="ePanel">
<dt><h1 onclick="showPanel(this);" class="search"><a href="javascript:;" class="cur" onfocus="this.blur();"></a>搜索</h1></dt>
<dd style="display:none;">
<asp:PlaceHolder ID="eSearchControlGroup" runat="server">
<form name="frmsearch" id="frmsearch" method="post" onsubmit="return false;" action="">
  <table width="100%" border="0" cellspacing="0" cellpadding="0" class="eDataView" style="margin-top:6px;margin-bottom:8px;">
  <colgroup>
<col width="100" />
<col />
</colgroup>
    <tr>
      <td class="title">姓名：</td>
      <td class="content"><span class="eform"><ev:eSearchControl ID="s1" ControlType="text" Field="FullName" Operator="like" DataType="string" width="200" runat="server" /></span></td>
     </tr>
     <tr>
      <td class="title">性别：</td>
      <td class="content"><span class="eform"><ev:eSearchControl ID="s2" ControlType="radio" Field="Sex" Options="[{text:男,value:1},{text:女,value:2}]" Operator="=" DataType="int" runat="server" /></span></td>
    </tr>  
      <tr>
         <td class="title">学历：</td>
      <td class="content"><span class="eform"><ev:eSearchControl ID="s3" ControlType="select" Field="Education" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindCondition="delTag=0 and show=1 and PID=1" BindOrderBy="PX" Operator="=" DataType="int" runat="server" /></td>
     </tr>
         <tr>
           <td class="title" colspan="2" style="text-align:left;padding-left:100px;"><a class="button" id="abtnsearch" href="javascript:;"><span><i class="search">搜索</i></span></a></td>
    </tr>
  </table> 
</form>
</asp:PlaceHolder>

</dd>
</dl>

    

<ev:eListControl ID="eDataTable"  class="easyui-datagrid" style="width:100%;height:480px;" Attributes="" runat="server" >
<ev:eListColumn FieldName="序号" Name="F1" width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn FieldName="姓名" Name="F2" Field="FullName" Width="260" runat="server" />
<ev:eListColumn FieldName="性别" Name="F3" Field="Sex" Width="80" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn FieldName="密码" Name="F4" Field="PassWord" Width="90" ControlType="PassWord" runat="server" />
<ev:eListColumn FieldName="学历" Name="F5" Field="Education" Width="100" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" runat="server" />
<ev:eListColumn FieldName="身高" Name="F6" Field="Height" Width="100" FormatString="{0:F2}" runat="server" />
<ev:eListColumn FieldName="添加时间" Name="F7" Field="addTime" Width="350" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
</ev:eListControl>



<script>
    var searchKeys = null;
    $(document).ready(function ()  
    {
		  
		  
		  
        searchKeys = getEasyUISearch(frmsearch);
		
        $("#abtnsearch").bind("click", function ()
        {           
            searchKeys = getEasyUISearch(frmsearch);
            $('#eDataTable').datagrid('load',searchKeys);
        });

    $("#eDataTable").datagrid({
        url: "easyUIDataGridSearch.aspx?obj=eDataTable",
        method: "get",
        rownumbers: true,
        pagination: true,         //分页属性设置
        singleSelect: true,
        pageNumber: 1,
        pageSize: 15,
        pageList: [10, 15, 20, 30, 40],
        queryParams: searchKeys,
    }).datagrid("getPager").pagination({
		loadMsg:"正在加载，请稍等!",
        beforePageText: '第',
        afterPageText: '页/{pages}页',
        displayMsg: '共{total}条记录',
        onBeforeRefresh: function () {
            return true;
        }

    });

			
    });
</script>
</div>
</asp:Content>
