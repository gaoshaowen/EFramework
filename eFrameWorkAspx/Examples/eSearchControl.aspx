<%@ Page Title="" Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eSearchControl.aspx.cs" Inherits="eFrameWork.Examples._eSearchControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eSearchControl控件</div>
<div style="margin:8px;">
    <dl id="eSearchBox" class="ePanel">
<dt><h1 onclick="showPanel(this);" class="search"><a href="javascript:;" class="cur" onfocus="this.blur();"></a>搜索</h1></dt>
<dd style="display:none;">
<asp:PlaceHolder ID="eSearchControlGroup" runat="server">
<form id="frmsearch" name="frmsearch" method="post" onsubmit="return goSearch(this);" action="<%=elist.getSearchURL()%>">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title">姓名：</td>
<td class="content" colspan="3"><span class="eform"><ev:eSearchControl ID="s1" Name="s1" ControlType="text" Field="FullName" Operator="like" FieldName="姓名" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">兴趣爱好：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s2" Name="s2" ControlType="checkbox" Field="Hobby" Operator="like" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" DataType="string" runat="server" /></span></td>
<td class="title">学历：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s3" Name="s3" ControlType="select" Field="Education" Operator="=" FieldName="学历" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">性别：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s4" Name="s4" ControlType="radio" Field="Sex" Operator="=" Options="[{text:男,value:1},{text:女,value:2}]" FieldName="性别" DataType="string" runat="server" /></span></td>
<td class="title">是否显示：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s5" Name="s5" ControlType="radio" Field="Show" Operator="=" Options="[{text:是,value:1},{text:否,value:0}]" FieldName="是否显示" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">添加时间：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s6" Name="s6" ControlType="date" Field="addTime" Operator=">=" DateFormat="yyyy-MM-dd" FieldName="添加时间" DataType="string" runat="server" /></span></td>
<td class="title">添加时间：</td>
<td class="content"><span class="eform"><ev:eSearchControl ID="s7" Name="s7" ControlType="date" Field="addTime" Operator="<=" DateFormat="yyyy-MM-dd" FieldName="添加时间" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td colspan="4" class="title" style="text-align:left;padding-left:125px;"><a class="button" href="javascript:;" onclick="if(frmsearch.onsubmit()!=false){frmsearch.submit();}"><span><i class="search">搜索</i></span></a></td>
</tr>
</table>
</form>
</asp:PlaceHolder>
</dd>
</dl>
<ev:eListControl ID="eDataTable" ShowMenu="true" LineHeight="32" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" Width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" Field="FullName" FieldName="姓名" Width="120" runat="server" />
<ev:eListColumn ControlType="password" Field="PassWord" FieldName="密码" Width="90" runat="server" />
<ev:eListColumn ControlType="date" Field="Birthday" FieldName="生日" Width="100" FormatString="{0:yyyy-MM-dd}" runat="server" />
<ev:eListColumn ControlType="text" Field="Sex" FieldName="性别" Width="80" Options="[{text:男,value:1},{text:女,value:2}]" ReplaceString="[{text:不详,value:0}]" runat="server" />
<ev:eListColumn ControlType="text" Field="Hobby" FieldName="兴趣爱好" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=5" runat="server" />
<ev:eListColumn ControlType="text" Field="Education" FieldName="学历" Width="90" BindAuto="True" BindObject="Demo_Dictionaries" BindText="Name" BindValue="ID" BindOrderBy="PX" BindCondition="delTag=0 and show=1 and PID=1" runat="server" />
<ev:eListColumn ControlType="file" Field="Photo" FieldName="照片" runat="server"><div style="height:{row:height}px;"><span></span><p><img  style="max-width:180px;" src="{data:photo}" oner3ror="this.style.display='none';"></p></div></ev:eListColumn>
<ev:eListColumn ControlType="text" Field="Height" FieldName="身高" Width="90" FormatString="{0:F2}" runat="server" />
<ev:eListColumn ControlType="text" Field="Show" FieldName="是否显示" Width="90" Options="[{text:是,value:True},{text:否,value:False}]" runat="server"><img src="{base:VirtualPath}{data:ShowPIC}" border="0"></ev:eListColumn>
<ev:eListColumn ControlType="sort" Field="PX" FieldName="显示顺序" Width="90" runat="server" />
<ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" Width="140" FormatString="{0:yyyy-MM-dd HH:mm:ss}" runat="server" />
</ev:eListControl>
<div style="margin:8px;"><ev:ePageControl ID="ePageControl1" PageSize="10" PageNum="9" runat="server" /></div>
</div>
</asp:Content>
