<%@ Page Language="C#" MasterPageFile="~/Master/systemMain.Master" AutoEventWireup="true" CodeBehind="Organizationals.aspx.cs" Inherits="eFrameWork.system.Organizationals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%if (!Ajaxget)
{ %>
<script>
var menu_a=null;
var win_width="650px";
var win_height="250px";
function postback(data)
{
	var url=document.location.href;
	url=url.addquerystring("ajax","true");
	
	$.ajax({
		   type: "get",
		   url:url,
		   dataType: "json",
		   success: function(data)
		   {
		       if (parent.arrLayerIndex.length > 0) {
		           parent.layer.close(parent.arrLayerIndex.pop());
		       }
		       else
		       {
		           if (arrLayerIndex.length > 0)
		           {
		               layer.close(arrLayerIndex.pop());
		           }
		       }
				
				var tree=getobj("etree");
				if(tree)
				{
					$(tree).parent().html(data.body.decode());					
					bindTree();
				}		   		
		   }
	});	
};
function view()
{
	var url="?ModelID=" + ModelID + "&ajaxget=true&act=view";
	var dataid=menu_a.getAttribute("dataid");
	url+="&id=" + dataid;
	layer.open({
      type: 2,
	  //skin: 'layui-layer-rim', //加上边框
      title: "查看",
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
      area : [win_width , win_height],
	  //scrollbar: false, 
	  //move: false,		
	  content: [url,'no'], 
	  //content: url,
	  success: function(layero, index)
	  {
		arrLayerIndex.push(index);
  	  }
    });	
}
function edit()
{
	var url="?ModelID=" + ModelID + "&ajaxget=true&act=edit";
	var dataid=menu_a.getAttribute("dataid");
	url+="&id=" + dataid;
	layer.open({
      type: 2,
	  //skin: 'layui-layer-rim', //加上边框
      title: "编辑",
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
      area : [win_width , win_height],
	  //scrollbar: false, 
	  //move: false,		
	  content: [url,'no'], 
	  //content: url,
	  success: function(layero, index)
	  {
		arrLayerIndex.push(index);
  	  }
    });	
}
function add(obj)
{
	if(obj){menu_a=obj;}
	var url="?ModelID=" + ModelID + "&ajaxget=true&act=add";
	if(menu_a)
	{
		var dataid=menu_a.getAttribute("dataid");
		
		if(dataid.length>0)
		{
			url+="&pid=" + dataid;
		}
	}
	layer.open({
      type: 2,
	  //skin: 'layui-layer-rim', //加上边框
      title: "添加",
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
      area : [win_width , win_height],
	  //scrollbar: false, 
	  //move: false,		
	  content: [url,'no'], 
	  //content: url,
	  success: function(layero, index)
	  {
		arrLayerIndex.push(index);
  	  }
    });	
};
function del()
{
	var _back=confirm('确认要删除吗？');
	if(!_back){return;};
	var url="?ModelID=" + ModelID + "&ajaxget=true&act=del&id=" + menu_a.getAttribute("dataid");
	
	$.ajax({
		   type: "get",
		   url:url,
		   dataType: "json",
		   success: function(data)
		   {
		   		//$(menu_a).parent().remove();
				menu_a=null;
				postback();
		   }
	});
};
function bodyck(e)
{
	hide_etreeMenu();
};
function bodykd(e)
{
	e=window.event||e; 
	if(e.keyCode==27){hide_etreeMenu();}
};
function show_etreeMenu()
{
	var menu=getobj("etreeMenu");
	menu.style.display="";
	if(document.body.addEventListener){document.body.addEventListener("keydown",bodykd, false);}else{document.body.attachEvent("onkeydown",bodykd);}
	if(document.body.addEventListener){document.body.addEventListener("click",bodyck, false);}else{document.body.attachEvent("onclick",bodyck);}
};
function hide_etreeMenu()
{
	var menu=getobj("etreeMenu");
	menu.style.display="none";
	if (document.body.addEventListener){document.body.removeEventListener('keydown', bodykd, false);}else{document.body.detachEvent("onkeydown",bodykd);}
	if (document.body.addEventListener){document.body.removeEventListener('click', bodyck, false);}else{document.body.detachEvent("onclick",bodyck);}
};
function div_contextmenu(evt,div)
{	
	if(evt.button!=2){return;}
	var obj=div.getElementsByTagName("a")[0];
	
	menu_a=obj;	
	var oRect = obj.getBoundingClientRect();	
	var top = eScroll().top + oRect.top + obj.offsetHeight;
	var left = eScroll().left + oRect.right - obj.offsetWidth;	
	top= eScroll().top + evt.clientY;
	left= eScroll().left + evt.clientX;
	var menu=getobj("etreeMenu");
	menu.style.top=top + "px";
	menu.style.left=left + "px";
	show_etreeMenu();	
};
function contextmenu(evt,obj)
{
	if(evt.button!=2){return;}
	menu_a=obj;
	var oRect = obj.getBoundingClientRect();	
	var top = eScroll().top + oRect.top + obj.offsetHeight;
	var left = eScroll().left + oRect.right - obj.offsetWidth;	
	top= eScroll().top + evt.clientY;
	left= eScroll().left + evt.clientX;
	var menu=getobj("etreeMenu");
	menu.style.top=top + "px";
	menu.style.left=left + "px";
	show_etreeMenu();	
};
function bindTree()
{
	var tree = new eTree('etree');
    tree.moveout = function (oul, nul, li)
    {
        var _url = "Organizationals.aspx?modelid=" + ModelID + "&act=setsort&id=" + li.attr("dataid") + "&pid=" + nul.attr("PID") + "&index=" + (li.index() + 1) + "&t=" + now();
        $.ajax({
            type: "GET",
            async: false,
            url: _url,
            dataType: "html",
            success: function (data) {
               // alert(data);
            }
        });
    };
    var etree = getobj("etree");
    etree.oncontextmenu = function () { return false; };
};
$(document).ready(function()
{
	document.body.oncontextmenu=function(){return false;};	
    bindTree();	
});
</script>
<div class="nav">您当前位置：首页 -> <%=model.ModelInfo["mc"].ToString()%><a id="btn_add" style="float:right;margin-top:10px;<%=( (Action.Value == "" || Action.Value == "view" ) && model.Power["Add"].ToString().ToLower()=="true" ? "" : "display:none;" )%>" class="button" href="javascript:;" dataid="<%=pid%>" onclick="add(this);"><span><i class="add">添加</i></span></a></div>
<div id="etreeMenu" class="etreeMenu" style="display:none;">
<a href="javascript:;" onclick="view();">查看</a>
<a href="javascript:;" onclick="add();">添加</a>
<a href="javascript:;" onclick="edit();">修改</a>
<a href="javascript:;" onclick="del();">删除</a>
</div>
<%}%>
<div style="margin:10px;">
<%
if(Action.Value.Length>0)
{
%>
<asp:PlaceHolder ID="eFormControlGroup" runat="server">
<form id="frmaddoredit" name="frmaddoredit" method="post" tar_get="mainform" action="<%=eform.getSaveURL()%>">
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
<td class="title"><%if(Action.Value == "add" || Action.Value == "edit"){Response.Write("<ins>*</ins>");}%>名称：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M1_F1" Name="M1_F1" ControlType="text" Field="MC" Width="300px" FieldName="名称" NotNull="True" runat="server" /></span></td>
</tr>
<tr>
<td class="title">上级机构：</td>
<td class="content" id="padd"><span class="eform"><ev:eFormControl ID="M1_F2" Name="M1_F2" ControlType="autoselect" Field="ParentID" FieldName="上级分类" FieldType="uniqueidentifier" BindAuto="True" BindObject="Organizationals" BindText="MC" BindValue="OrganizationalID" BindCondition="delTag=0 and (ParentID is Null)" BindOrderBy="PX,AddTime" NotNull="False" DataType="string" runat="server" /></span></td>
</tr>


<tr>
<td class="title">显示顺序：</td>
<td class="content"><span class="eform"><ev:eFormControl ID="M1_F3" Name="M1_F3" ControlType="sort" Field="PX" FieldName="显示顺序" NotNull="False" ReplaceString="[{text:&quot;&quot;,value:0}]" DefaultValue="0" runat="server" /></span></td>
</tr>
<tr>
<td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">
<%
if(Action.Value == "add" || Action.Value == "edit")
{
%>

<a class="button" href="javascript:;" onclick="packform('1',true);"><span><i class="save"><%=(Action.Value == "add" ? "添加" : "保存")%></i></span></a>

<%}%>
<a class="button" href="javascript:;" style="margin-left:30px;" onclick="goBack();"><span><i class="back">返回</i></span></a>
</td>
</tr>
</table>
</form>
</asp:PlaceHolder>
<%
}
else
{
Response.Write(eTree);
}
%>
</div>
</asp:Content>