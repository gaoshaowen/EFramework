<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="TableInfo.aspx.cs" Inherits="eFrameWork.Manage.TableInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style>
input.edit{font-size:12px;border:1px solid #ccc;height:18px;line-height:18px;width:300px;}
body,td,span{font-size:12px;line-height:20px;}
span.close,span.open{display:inline-block;padding-left:20px; cursor:pointer;border:0px solid #000;}
span.close{background:url(../images/close.gif) no-repeat scroll 3px 6px transparent;}
span.open{background:url(../images/open.gif) no-repeat scroll 3px 6px transparent;}
</style>
<script>
function show(id,obj)
{
        if(obj.className.toLowerCase()=="open")
        {
            obj.className="close";
            getobj("div_" + id).style.display="none";
        }
        else
        {
            obj.className="open";
            getobj("div_" + id).style.display="";
        }
};
function setTableDescription(obj,tbname)
{
        var url = "?act=settabledescription&tbname=" + tbname + "&value=" + obj.value.toCode() + "&t=" + now();
        var html=PostURL(url);
        obj.previousSibling.style.color=(obj.value.length>0 ? "#000000" : "#ff0000");
};
function setColumnDescription(obj,tbname,column)
{
        var url = "?act=setcolumndescription&tbname=" + tbname + "&column=" + column + "&value=" + obj.value.toCode() + "&t=" + now();
        var html=PostURL(url);
        obj.previousSibling.style.color=(obj.value.length>0 ? "#000000" : "#ff0000");
};
function showAll(obj, show)
{
        var divs = obj.parentNode.getElementsByTagName("div");
        for (i = 0; i < divs.length; i++) {
            divs[i].style.display = show;
        }
};
function createModel(name)
{	
	layer.open({
          type: 2
          , title: "生成实体"
          , shadeClose: false //点击遮罩关闭层
          , area: [ "50%", "70%"]   
		  ,scrollbar: false    
          //, content: ["createModel.aspx","no"]
		  , content: "createModel.aspx" + (name.length>0 ? "?name=" + name : "")
		  , btn: ['关闭'] //只是为了演示
          , success: function (layero, index) {
		 
             // arrLayerIndex.push(index);
          }
		  ,yes: function(index,layero)
		  { 
			//arrLayerIndex.pop();
			layer.close(index);
		  }	
          ,cancel: function (index, layero) 
		  {
             // arrLayerIndex.pop();
			 //alert(4);
          }

        });
};
function createTable(name) {
    layer.open({
        type: 2
          , title: "生成脚本"
          , shadeClose: false //点击遮罩关闭层
          , area: ["50%", "70%"]
		  , scrollbar: false
        //, content: ["createModel.aspx","no"]
		  , content: "createTable.aspx" + (name.length > 0 ? "?name=" + name : "")
		  , btn: ['关闭'] //只是为了演示
          , success: function (layero, index) {

              // arrLayerIndex.push(index);
          }
		  , yes: function (index, layero) {
		      //arrLayerIndex.pop();
		      layer.close(index);
		  }
          , cancel: function (index, layero) {
              // arrLayerIndex.pop();
              //alert(4);
          }

    });
};
</script>
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 数据结构<a style="float:right;margin-top:4px;" class="button" href="TableInfo_Export.aspx" target="_blank"><span><i class="export">导出</i></span></a><a style="float:right;margin-top:4px;margin-right:20px;" class="button" href="javascript:;" onclick="createModel('');"><span style="letter-spacing:0px;">生成实体</span></a><a style="float:right;margin-top:4px;margin-right:20px;" class="button" href="javascript:;" onclick="createTable('');"><span style="letter-spacing:0px;">生成脚本</span></a></div>
    <div style="margin:6px;">
<asp:Literal ID="LitBody" runat="server" />
        </div>
</asp:Content>