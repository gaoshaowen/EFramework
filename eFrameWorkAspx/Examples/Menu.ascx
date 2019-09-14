<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="eFrameWork.Examples.Menu" %>
<ul class="emenu">
<li<%if(filename.IndexOf("default.aspx")>-1){Response.Write(" class=\"cur\"");}%>><a href="Default.aspx">首页</a></li>
<li<%=(filename.IndexOf("eoledb.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eOleDB.aspx">eOleDB 类</a></li>
<li<%=(filename.IndexOf("etable.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eTable.aspx">eTable 类</a></li>
<li<%=(filename.IndexOf("emtable.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eMTable.aspx" style="display:none;">eMTable 类</a></li>
<li<%=(filename.IndexOf("elist.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eList.aspx">eList 类</a></li>
<li<%=(filename.IndexOf("euser.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eUser.aspx">eUser 类</a></li>
<li<%=(filename.IndexOf("eform.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eForm.aspx">eForm 类</a></li>
<li<%=(filename.IndexOf("elistcontrol.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eListControl.aspx">eListControl 控件</a></li>
<li<%=(filename.IndexOf("eorderby.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eOrderby.aspx">eListControl 控件排序</a></li>
<li<%=(filename.IndexOf("easyuidatagrid.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="easyUIDataGrid.aspx">easyUI DataGrid 应用</a></li>
<li<%=(filename.IndexOf("easyuidatagridsearch.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="easyUIDataGridSearch.aspx?s1=4">easyUI DataGrid 查询</a></li>
<li<%=(filename.IndexOf("eformcontrol.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eFormControl.aspx">eFormControl 控件</a></li>
<li<%=(filename.IndexOf("esearchcontrol.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eSearchControl.aspx">eSearchControl 控件</a></li>
<li<%=(filename.IndexOf("edatacontent.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eDataContent.aspx">eDataContent 控件</a></li>
<li<%=(filename.IndexOf("edataview.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eDataView.aspx">eDataView 控件</a></li>
<li<%=(filename.IndexOf("epicture.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="ePicture.aspx">ePicture图片处理类</a></li>
<li<%=(filename.IndexOf("ejson.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eJson.aspx">Json处理</a></li>
<li<%=(filename.IndexOf("edatatable.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eDataTable.aspx">eDataTable表格插件</a></li>
<li<%=(filename.IndexOf("eclient.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="eClient.aspx">客户端数据验证</a></li>
<li<%=(filename.IndexOf("loadaction.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="LoadAction.aspx?id=1">前台加载数据</a></li>
<li<%=(filename.IndexOf("multiple.aspx")>-1 ? " class=\"cur\"" : "")%>><a href="Multiple.aspx">综合示例</a></li>
</ul>