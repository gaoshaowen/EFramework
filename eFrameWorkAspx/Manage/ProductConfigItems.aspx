<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="ProductConfigItems.aspx.cs" Inherits="eFrameWork.Manage.ProductConfigItems" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<%@ Import Namespace="EKETEAM.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 产品配置</div>
<script>
var PId = "<%=PId%>";
//添加审批流程OK
function add_CheckUp(obj)
{
	showloading();
    var _url = "?act=addcheckup&PId=" + PId + "&t=" + now();
	$.ajax({
		   type: "GET", async: true,
           url: _url,
           dataType: "json",
           success: function (data) {
			   hideloading();
               //loadData($(obj).parents("div[dataurl]"));
			   loadData();
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
		   		hideloading();
           }
    });
};	
//修改审批流程OK
function set_CheckUp(obj, CheckupID, Item)
{
	if (obj.getAttribute("oldvalue") == obj.value) { return; }
	var value = getValue(obj);
	if (value == "error") { return; }

	showloading();
	var _url = "?act=setcheckup&PId=" + PId + "&CheckupID=" + CheckupID + "&item=" + Item + "&t=" + now();
    $.ajax({
		   type: "post", 
		   async: true,
		   data:{value:value},
           url: _url,
           dataType: "html",
           success: function (data) {
			    obj.setAttribute("oldvalue", obj.value);
			 	hideloading();
			   
               	loadData();
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
			   hideloading();
           }
    });
};
//删除审批流程OK
function del_CheckUp(obj, CheckupID)
{
	if (!confirm('确认要删除吗？')) { return; }
	showloading();
	var _url = "?act=delcheckup&PId=" + PId + "&CheckupID=" + CheckupID + "&t=" + now();
	$.ajax({
		   type: "GET", async: true,
           url: _url,
           dataType: "json",
           success: function (data) {
			   hideloading();
			   $(getParent(obj,"tr")).remove();
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
			   hideloading();
           }
    });
};



function showloading()
{
	$("#divloading").show();
};
function hideloading()
{
	$("#divloading").hide();
};
function getValue(obj)
{
	if (obj.type.toLowerCase() == "radio" || obj.type.toLowerCase() == "checkbox") 
	{
		return (obj.checked ? "1" : "0");
    }
	else
	{
		return obj.value.encode();
    }
};
//Json编辑-添加
function Json_Add(objid)
{
	var obj=$("#" + objid);
	var jsonstr=obj.attr("jsonformat");
	var json = jsonstr.toJson();
	var html='<tr>';
		html+='<td height="30" align="center"><img src="images/del.jpg" style="cursor:pointer;" onclick="Json_Delete(this);" /></td>';
		json.foreach(function (e)
		{
			html+='<td><input type="text" name="' + json[e].value + '" value="" style="border:0px;background-color:transparent;width:100%;" /></td>';
		});
		var len= $("#JsonTable tbody > tr").length + 1;
		html+='<td style="cursor:move;">' + len + '</td>';
		html+='</tr>';
		//alert(obj.find("tbody").length);

	//alert(json.length);
	$("#JsonTable tbody").append(html);
	
	var	tb=new eDataTable("JsonTable",1);
	
	
};
//Json编辑-删除
function Json_Delete(obj)
{
	if(!confirm('确认要删除吗？')){return;}
	$(obj).parent().parent().remove();
	$("#JsonTable tbody > tr > td:last-child").each(function(index1,obj){
		$(obj).html(1+index1);
	}); 	
};
//Json编辑
function Json_Edit(objid)
{
	
	var obj=$("#" + objid);	
	var jsonstr=obj.attr("jsonformat");
	var json = jsonstr.toJson();
	

	
	var valuestr=obj.val();

	if(valuestr.length>0)
	{
		valuestr=valuestr.replace(/{/g,"{\"");
		valuestr=valuestr.replace(/:/g,"\":\"");
		valuestr=valuestr.replace(/,/g,"\",\"");
		valuestr=valuestr.replace(/}/g,"\"}");
		valuestr=valuestr.replace(/}\",\"{/g,"},{");
		valuestr=valuestr.replace(/\"\"/g,"\"");
	}
	else
	{
		valuestr="[]";
	}
	
	var values=valuestr.toJson();
	//alert(valuestr);
	//alert(json.length);
	json.foreach(function (e)
    {
		//alert(json[e].text + "::" + json[e].value);
    });
	
	var width= 180;

	
	var html='<table id="JsonTable" class="eDataTable" border="0" cellpadding="0" cellspacing="1" width="' + ((json.length*(width-10)) + 35 + 35) +  '" style="margin:10px;">';
	
	html+='<colgroup>';
	html+='<col width="35" />';
	for(var i=0;i<json.length;i++)
	{
		html+='<col width="' + (width-10) + '" />';
	}
	html+='<col width="35" />';
	html+='</colgroup>';	
	html+='<thead>';
	html+='<tr>';
	html+='<td height="30" align="center"><img src="images/add.jpg" style="cursor:pointer;" onclick="Json_Add(\'' + objid + '\');" /></td>';
	json.foreach(function (e)
    {
		html+='<td height="30">' + json[e].text + '</td>';
	});
	html+='<td>顺序</td>';
	html+='</tr>';
	html+='</thead>';
	html+='<tbody eMove="true">';
	for(var j=0;j<values.length;j++)
	{
		html+='<tr>';
		html+='<td height="30" align="center"><img src="images/del.jpg" style="cursor:pointer;" onclick="Json_Delete(this);" /></td>';
		json.foreach(function (e)
		{
			//html+='<td height="30">' + values[j][json[e].value] + '</td>';
			html+='<td><input type="text" name="' + json[e].value + '" value="' + values[j][json[e].value] + '" style="border:0px;background-color:transparent;width:100%;" /></td>';
		});
		html+='<td style="cursor:move;">' + (j+1) + '</td>';
		html+='</tr>';
	}
	html+='</tbody>';
	html+='</table>';
	layer.open({
      type: 1,
	  title: "选项",
	  scrollbar: false,
      area: [(json.length*width + 20 + 35 + 35 + 20) + 'px', '60%'],
      shadeClose: true, //点击遮罩关闭
	  content: html,
	  btn: ['确定', '取消'],
	  yes: function(index,layero)
	  {
		  var hasnull=false;
		  var _json='[';
		  var _html='';
		 $("#JsonTable tbody tr").each(function(index1,obj){
			if(index1>0){_json+=',';}
			_json+='{';
			$(obj).find("input").each(function(index2,input){
				if(input.value.length==0){hasnull=true;}
				if(index2>0){_json+=',';} 
				if(input.name=="text"){_html += '<span style="display:inline-block;margin-right:6px;border:1px solid #ccc;padding:3px 12px 3px 12px;">' + input.value + '</span>';}
				
				_json += '"' + input.name +  '":"' + input.value.replace(/\"/g,"&quot;") + '"';
			});	
			_json+='}';
		});
		if(hasnull){alert("数据不能为空!");return;}
		_json+=']';
		if(_json.length==0){_json='';}
		
		obj.parent().find("span").remove();
		obj.next().after(_html);
		/*
		var next=obj.next();
		while(next.next()[0].tagName == "SPAN")
		{
			//alert(next.next()[0].tagName);
			next.next().remove();
		}		
		next.after(_html);
		*/


		obj.val(_json);	
		obj.get(0).onblur();
		layer.close(index);
		//alert();
	  },
      cancel: function (index, layero) 
	  {
	  	//arrLayerIndex.pop();
      },
	  success: function (layero, index)
	  {
	  	//arrLayerIndex.push(index);
		var tb=getobj("JsonTable");
		if(tb)
		{
			tb=new eDataTable("JsonTable",1);
			tb.moveRow=function(index,nindex)
			{
				$("#JsonTable tbody > tr > td:last-child").each(function(index1,obj){
					$(obj).html(1+index1);
				}); 
			};		
		}
      }
    });
	
};
function loadData()
{
	showloading();
	var url = "ProductConfigItems.aspx?PId=" + PId + "&act=getdata&t=" + now();
	$.ajax({
		type: "GET", async: true,
        url: url,
        dataType: "html",
        success: function (data) {
			hideloading();
            $("#content").html(data);
			bindEvent();
        },
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
    });
};
function bindEvent()
{
	var tb5=getobj("eDataTable_CheckUp");
	if(tb5)
	{
		tb5=new eDataTable("eDataTable_CheckUp",1);
		tb5.moveRow=function(index,nindex)
		{
			$("#eDataTable_CheckUp tbody > tr > td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_CheckUp tbody > tr").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).attr("erowid");
			}); 
			if(ids.length==0){return;}
			var _url = "?act=setcheckup&PId=" + PId + "&item=setorders&t=" + now();
			showloading();
			$.ajax({
				type: "POST", async: true,
				data:{ids:ids},
				url: _url,
				dataType: "json",
				success: function (data) 
				{
					hideloading();
				},
				error: function (XMLHttpRequest, textStatus, errorThrown) {
					hideloading();
				}
   			});
		};
	}
	
};
$(document).ready(function () {
	bindEvent();
});
</script>
<style>
.text{display:inline-block;width:100%;border:1px solid #ccc;font-size:12px;height:23px;line-height:23px;}
.divloading{filter:alpha(opacity=50);-moz-opacity:0.5;-khtml-opacity: 0.5; opacity: 0.5; position:fixed;width:100%;height:100%;top:0px;left:0px;background:#cccccc url(images/loading.gif) no-repeat center center;}
</style>
<div id="divloading" class="divloading" style="display:none;">&nbsp;</div>
<div style="margin:6px;line-height:23px;"><%=configName%> - 审批流程：</div>

<div id="content" style="margin:6px;border:0px solid #ff0000;">

<asp:Repeater id="Rep" runat="server" >
<headertemplate>
<%#
"<table id=\"eDataTable_CheckUp\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" style=\"margin-bottom:8px;\">" +
"<thead>" +
"<tr>" +
"<td height=\"25\" width=\"35\" align=\"center\"><a title=\"添加流程\" href=\"javascript:;\" onclick=\"add_CheckUp(this);\"><img width=\"16\" height=\"16\" src=\"images/add.jpg\" border=\"0\"></a></td>" +
"<td width=\"80\">选择状态" + "</td>" +
"<td width=\"80\">填写意见" + "</td>" +
"<td width=\"120\">状态名称" +  "</td>" +
"<td width=\"120\">状态编码" + "</td>" +
"<td width=\"120\">流程名称" + "</td>" +
"<td width=\"120\">审批编码"  + "</td>" +
"<td width=\"80\">按钮名称" + "</td>" +
"<td>返回流程" +  "</td>" +
"<td width=\"80\">流程顺序"  + "</td>" +
"</tr>" +
"</thead>"+
"<tbody eSize=\"false\" eMove=\"true\">"
%>
</headertemplate>
<itemtemplate>
<%#
"<tr" + ((Container.ItemIndex + 1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + " erowid=\"" + Eval("CheckupID") + "\" onmouseover=\"this.className='cur';\" onmouseout=\"this.className=this.getAttribute('eclass');\" >" +
"<td height=\"26\" align=\"center\"><a title=\"删除流程\" href=\"javascript:;\" onclick=\"del_CheckUp(this,'" + Eval("CheckupID") + "');\"><img width=\"16\" height=\"16\" src=\"images/del.jpg\" border=\"0\"></a></td>" +

"<td><input type=\"checkbox\" onclick=\"set_CheckUp(this,'" + Eval("CheckupID") + "','showstate');\"" + (Eval("showState").ToString()=="True" ? " checked" : "") + " /></td>" +
"<td><input type=\"checkbox\" onclick=\"set_CheckUp(this,'" + Eval("CheckupID") + "','showidea');\"" + (Eval("showIdea").ToString()=="True" ? " checked" : "") + " /></td>" +

"<td><input class=\"text\" type=\"text\" value=\""+ Eval("MC").ToString() + "\" oldvalue=\""+ Eval("MC").ToString() + "\" onBlur=\"set_CheckUp(this,'" + Eval("CheckupID") + "','mc');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("Code").ToString() + "\" oldvalue=\""+ Eval("Code").ToString() + "\" onBlur=\"set_CheckUp(this,'" + Eval("CheckupID") + "','code');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("CheckMC").ToString() + "\" oldvalue=\""+ Eval("CheckMC").ToString() + "\" onBlur=\"set_CheckUp(this,'" + Eval("CheckupID") + "','checkmc');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("CheckCode").ToString() + "\" oldvalue=\""+ Eval("CheckCode").ToString() + "\" onBlur=\"set_CheckUp(this,'" + Eval("CheckupID") + "','checkcode');\"></td>" +
"<td><input class=\"text\" type=\"text\" value=\""+ Eval("Text").ToString() + "\" oldvalue=\""+ Eval("Text").ToString() + "\" onBlur=\"set_CheckUp(this,'" + Eval("CheckupID") + "','text');\"></td>" +
"<td><input class=\"text\" reload=\"true\" id=\"backprocess_" + Eval("CheckupID").ToString().Replace("-","") + "\" jsonformat=\"[{&quot;text&quot;:&quot;流程名称&quot;,&quot;value&quot;:&quot;text&quot;},{&quot;text&quot;:&quot;状态编码&quot;,&quot;value&quot;:&quot;value&quot;}]\" style=\"display:none;\" type=\"text\" value=\""+ System.Web.HttpUtility.HtmlEncode(Eval("BackProcess").ToString()) + "\" oldvalue=\""+ System.Web.HttpUtility.HtmlEncode(Eval("BackProcess").ToString())  + "\" ondblclick=\"dblClick(this,'" + Eval("MC") + "-返回流程');\" onBlur=\"set_CheckUp(this,'" + Eval("CheckupID") + "','backprocess');\">"+
"<img src=\"images/jsonedit.jpg\" align=\"absmiddle\" style=\"cursor:pointer;margin-right:5px;\" onclick=\"Json_Edit('backprocess_" +  Eval("CheckupID").ToString().Replace("-","") + "');\">" + 
getJsonText(Eval("BackProcess").ToString(),"text") +
"</td>" +
//"<td><input class=\"text\" reload=\"true\" type=\"text\" value=\""+ (Eval("PX").ToString()=="999999" ? "" : Eval("PX").ToString()) + "\" oldvalue=\""+ (Eval("PX").ToString()=="999999" ? "" : Eval("PX").ToString()) + "\" onBlur=\"setCheckUp(this,'" + Eval("CheckupID") + "','px');\"></td>"+
"<td style=\"cursor:move;\">" + (Container.ItemIndex + 1) + "</td>"+
"</tr>"
%>
</itemtemplate>
<footertemplate><%#"</tbody></table>"%></footertemplate>
</asp:Repeater>


</div>
</asp:Content>
