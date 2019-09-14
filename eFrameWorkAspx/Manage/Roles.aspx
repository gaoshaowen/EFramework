<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="eFrameWork.Manage.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 角色管理<a id="btn_add" style="float:right;margin-top:4px;<%=( Action.Value == "" ? "" : "display:none;" )%>" class="button" href="<%=edt.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
<div style="margin:6px;line-height:25px;font-size:13px;">
 <%
if(Action.Value.Length > 0 )
{
%>
    
       <asp:PlaceHolder ID="eFormControlGroup" runat="server">
    <form name="frmaddoredit" id="frmaddoredit" method="post" action="<%=edt.getSaveURL()%>">
	<input name="id" type="hidden" id="id" value="<%=edt.ID%>">
    <input name="act" type="hidden" id="act" value="save">  
	<input name="fromurl" type="hidden" id="fromurl" value="<%=edt.FromURL%>">  
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="eDataView">
      <tr>
        <td width="126" class="title"><ins>*</ins>角色名称：</td>
        <td class="content"><span class="eform">
		 <ev:eFormControl ID="f1" Field="MC" width="600" notnull="true" fieldname="名称" runat="server" />
		</span></td>
      </tr>
        <tr valign="top">
            <td class="title">模块：</td>
              <td class="content">
                  <asp:Literal id="LitApps" runat="server" />
                  </td>
        </tr>
  		<tr>
          <td class="title">说明：</td>
          <td class="content"><span class="eform">
		   <ev:eFormControl ID="f2" ControlType="textarea" Field="SM" width="600" height="60" runat="server" />
		   </span></td>
        </tr>

        <tr>
       <td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">		
		<a class="button" href="javascript:;" onclick="if(frmaddoredit.onsubmit()!=false){frmaddoredit.submit();}"><span><i class="save">保存</i></span></a>
		<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span><i class="back">返回</i></span></a>
		</td>
	   </tr>
	 
    </table>
	 </form>
</asp:PlaceHolder>
 <%
 }
%>


    <ev:eListControl ID="eDataTable" ShowMenu="true" LineHeight="35" CellSpacing="1" runat="server" >
<ev:eListColumn ControlType="text" FieldName="序号" Width="60" runat="server">{row:index}</ev:eListColumn>
<ev:eListColumn ControlType="text" Field="MC" FieldName="角色名称" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" Field="SM" FieldName="说明" runat="server" />
<ev:eListColumn ControlType="text" Field="addTime" FieldName="添加时间" Width="140" FormatString="{0:yyyy-MM-dd HH:mm:ss}" OrderBy="true" runat="server" />
<ev:eListColumn ControlType="text" FieldName="操作" Width="130" runat="server">
    <a href="{base:url}act=copy&id={data:ID}" onclick="javascript:return confirm('确认要复制吗？');">复制</a>
    <a href="{base:url}act=edit&id={data:ID}">修改</a>
    <a href="{base:url}act=del&id={data:ID}" onclick="javascript:return confirm('确认要删除吗？');">删除</a>
</ev:eListColumn>
</ev:eListControl>
<div style="margin:10px;">
<ev:ePageControl ID="ePageControl1" PageSize="10" PageNum="9" runat="server" />
</div>



</div>
</asp:Content>
