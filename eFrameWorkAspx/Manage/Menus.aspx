<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="eFrameWork.Manage.Menus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 菜单管理<a id="btn_add" style="float:right;margin-top:4px;<%=(Action.Value == "" || Action.Value == "view"  ? "" : "display:none;" )%>" class="button" href="<%=eform.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
<div style="margin:10px;">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr valign="top">
    <td valign="top" height="400" width="300"><asp:Literal ID="LitMenus" runat="server" /></td>
      <td valign="top">
<div style="margin:10px;">
<%
if(Action.Value.Length>0)
{
%>
<script>
    function SelectICO()
    {
        var url = "../Plugins/FontIco.aspx";
        layer.open({
            type: 2
          , title: "选择图标"
          , shadeClose: true //点击遮罩关闭层
          , area: [ "750px", "500px"]
         
          , content: url

          , success: function (layero, index) {
              arrLayerIndex.push(index);
          }
          , cancel: function (index, layero) {
              arrLayerIndex.pop();
          }

        });

    };
    function setIco(obj)
    {
        var span = getobj("spanico");
        span.innerHTML = obj.innerHTML.replace("<i></i>","");
        var input = getobj("M1_F4");
        input.value = obj.innerHTML;
    };
</script>
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<form id="frmaddoredit" name="frmaddoredit" method="post" action="<%=eform.getSaveURL()%>">
<input type="hidden" id="act" name="act" value="save">
<input type="hidden" id="fromurl" name="fromurl" value="<%=eform.FromURL%>">
<input type="hidden" id="ID" name="ID" value="<%=eform.ID%>">
<input type="hidden" name="eformdata_1" id="eformdata_1" value="<%=System.Web.HttpUtility.HtmlEncode(eform.getJson())%>">
</form>
<form id="form_1" name="form_1" method="post" tar_get="mainform" action="">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="eDataView">
<colgroup>
<col width="120" />
<col />
</colgroup>
<tr>
<td class="title"><%=(Action.Value == "add" || Action.Value == "edit" ? "<ins>*</ins>" : "")%>名称：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M1_F1" Name="M1_F1" ControlType="text" Field="MC" Width="360px" FieldName="名称" NotNull="True" runat="server" /></span></td>
</tr>
<tr>
<td class="title">上级分类：</td>
<td class="content" id="padd"><span class="eform"><ev:eFormControl ID="M1_F2" Name="M1_F2" ControlType="autoselect" Field="ParentID" FieldName="上级分类" FieldType="uniqueidentifier" BindAuto="True" BindObject="a_eke_sysModels" BindText="MC" BindValue="ModelID" BindCondition="delTag=0 and Type=2 and (ParentID is Null)" BindOrderBy="PX,AddTime" NotNull="False" DataType="string" runat="server" /></span></td>
</tr>
<tr>
<td class="title">图标：</td>
<td class="content"><ev:eFormControl ID="M1_F4" Name="M1_F4" ControlType="hidden" Field="IcoHTML" FieldName="图标" NotNull="False" runat="server" /><%="<span id=\"spanico\">"+M1_F4.Value.ToString() + "</span>" %> 
   <%
       if (Action.Value == "edit")
{
%>
     <a href="javascript:;" onclick="SelectICO();">选择</a>
   <%
   }
%>
</td>
</tr>
<tr>
<td class="title">显示顺序：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M1_F3" Name="M1_F3" ControlType="sort" Field="PX" FieldName="显示顺序" NotNull="False" ReplaceString="[{text:&quot;&quot;,value:0}]" DefaultValue="0" runat="server" /></span></td>
</tr>

<tr>
<td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">
<%
    if (Action.Value == "add" || Action.Value == "edit")
    {
    
%>

<a class="button" href="javascript:;" onclick="packform('1');"><span><i class="save"><%=(Action.Value == "add" ? "添加" : "保存")%></i></span></a>

<%}
    else
    {

        if (eform.Fields["Type"].ToString() == "2")
        {    %>

<a class="button" style="margin-left:30px;" href="<%=eform.getActionURL("del", eform.ID)%>" onclick="javascript:return confirm('确认要删除吗？')"><span><i class="edit">删除</i></span></a>

<%}%>
  <a class="button"style="margin-left:30px;" href="<%=eform.getActionURL("edit", eform.ID)%>"><span><i class="edit">编辑</i></span></a>
<%}%>

<a class="button"style="margin-left:30px;" href="javascript:;" onclick="history.back();"><span><i class="back">返回</i></span></a>
</td>
</tr>
</table>
</form>
</asp:PlaceHolder>
<%}%>

    </div>

      </td>
  </tr>
</table>
</div>



<script>
    var tree = new eTree("etree");
    tree.moveout = function (oul, nul, li) {
        var _url = "Menus.aspx?act=setsort&id=" + li.attr("dataid") + "&pid=" + nul.attr("PID") + "&index=" + (li.index() + 1) + "&t=" + now();
        //alert(_url);
        //return;
        $.ajax({
            type: "GET",
            async: false,
            url: _url,
            dataType: "html",
            success: function (data) {
                 //alert(data);
            }
        });

    };
</script>

</asp:Content>
