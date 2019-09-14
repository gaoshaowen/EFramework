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
//双击修改
function dblClick(obj,title)
{
	var type=obj.type;
	var value=obj.value;
	layer.open({
      type: 1,
	  title: title,
	  scrollbar: false,
      area: ['600px', '360px'],
      shadeClose: true, //点击遮罩关闭
      //content: '<div style="width:100%;height:100%;overflow:hidden;padding:10px;"><textarea name="textareabody" id="textareabody" style="padding:5px;font-size:12px;line-height:20px;width:95%;height:85%;border:1px solid #ccc;">' + value + '</textarea></div>',
	  content: '<textarea name="textareabody" id="textareabody" style="margin:10px;padding:5px;font-size:12px;line-height:20px;min-width:90%;min-height:75%;border:1px solid #ccc;">' + value + '</textarea>',
	  btn: ['确定', '取消'],
	  yes: function(index,layero)
	  {
	  	var nvalue=$("#textareabody").val();
		if(obj.tagName=="TEXTAREA"){nvalue=nvalue.replace(/\n/g, "");}
		else{nvalue=nvalue.replace(/\n/g, " ");}
		obj.value=nvalue;
		obj.onblur();
		arrLayerIndex.pop();
		layer.close(index);
	  },
      cancel: function (index, layero) 
	  {
	  	arrLayerIndex.pop();
      },
	  success: function (layero, index)
	  {
	  	arrLayerIndex.push(index);
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
//同步列选项
function loadColumnOptions(obj,ModelConditionID)
{

	var td=$(obj).parent().prev().prev();
	var sel=td.find("select:eq(0)");
	var code=sel.val();
	if(code.length==0)
	{
		alert("请先选择条件列!");
		return;
	}


	showloading();
	var _url = "?act=loadcolumnoptions&modelid=" + ModelID + "&modelconditionid=" + ModelConditionID + "&code=" + code + "&t=" + now();
    $.ajax({
            type: "post", 
			async: true,
            url: _url,
            dataType: "html",
            success: function (data) {
				hideloading();
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
	
	
	
	//$("#divloading").hide();
};
//全选所有列
function selColumnAll(obj)
{
	//alert(obj.checked)
	var inputs=$("#eDataTable [name='selItem']");
	inputs.each(function (i) 
	{
		$(this).attr("checked",(obj.checked ? true : false));
		selColumn($(this).get(0),$(this).get(0).value);
		//alert($(this).get(0).checked);
	});
	//alert(inputs.length);
};
//选择取消列OK
function selColumn(obj,code)
{
	var _reload=parseBool(Attribute(obj,"reload"));		
	showloading();	
	var _url="?act=selcolumn&modelid=" + ModelID  + "&code=" + code + "&t=" + now();
	$.ajax({type: "POST",async: false,
		data:{value:(obj.checked ? "1" : "0")},
		url: _url,
		dataType: "json",
		success: function(data)
		{
			//alert(data.message);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//添加列OK
function addColumn()
{	
	showloading();
	var _url="?act=addcolumn&modelid=" + ModelID  + "&t=" + now();
	$.ajax({type: "POST",async: false,
		url: _url,
		dataType: "json",
		success: function(data)
		{
			//alert(data.message);
			hideloading();
			loadData($(".eTab dd div:eq(0)"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//删除列OK
function delColumn(obj,code)
{
	if(!confirm('确认要删除吗？')){return;}
	showloading();
	var _url="?act=delcolumn&modelid=" + ModelID  + "&code=" + code + "&t=" + now();
	$.ajax({type: "POST",async: false,
		url: _url,
		dataType: "json",
		success: function(data)
		{
			hideloading();
			$(getParent(obj,"tr")).remove();
			//loadData($(".eTab dd div:eq(0)"));
			//alert(data.message);
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//重命名列OK
function ReNameColumn(obj)
{
	if(obj.getAttribute("oldvalue")==obj.value){return;}
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url="?act=renamecolumn&modelid=" + ModelID  + "&code=" + obj.getAttribute("oldvalue") + "&newcode=" + obj.value.toCode()  + "&t=" + now();
	$.ajax({type: "POST",async: false,
		url: _url,
		dataType: "json",
		success: function(data)
		{
			obj.setAttribute("oldvalue",obj.value);
			//alert(data.message);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//列备注OK
function ColumnName(obj,code,value)
{
	
	if(obj.getAttribute("oldvalue")==value){return;}
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url="?act=columnname&modelid=" + ModelID  + "&code=" + code + "&t=" + now();
	$.ajax({type: "POST",async: false,
		data:{value:value.toCode()},
		url: _url,
		dataType: "json",
		success: function(data)
		{
			obj.setAttribute("oldvalue",value);
			//alert(data.message);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//列默认值OK
function ColumnDefault(obj,code,value)
{
	if(obj.getAttribute("oldvalue")==value){return;}
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url="?act=columndefault&modelid=" + ModelID  + "&code=" + code + "&t=" + now();
	$.ajax({type: "POST",async: false,
		data:{value:value.toCode()},
		url: _url,
		dataType: "json",
		success: function(data)
		{
			obj.setAttribute("oldvalue",value);
			//alert(data.message);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//数据类型OK
function ColumnType(obj,code,value)
{
	if(obj.getAttribute("oldvalue")==value){return;}
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url="?act=columntype&modelid=" + ModelID  + "&code=" + code + "&t=" + now();
	$.ajax({type: "POST",async: false,
		data:{value:value.toCode()},
		url: _url,
		dataType: "json",
		success: function(data)
		{
			obj.setAttribute("oldvalue",value);
			//alert(data.message);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//数据长度OK
function ColumnLength(obj,code,value)
{
	if(obj.getAttribute("oldvalue")==value){return;}
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url="?act=columnlength&modelid=" + ModelID  + "&code=" + code + "&t=" + now();
	$.ajax({type: "POST",async: false,
		data:{value:value.toCode()},
		url: _url,
		dataType: "json",
		success: function(data)
		{
			obj.setAttribute("oldvalue",value);
			//alert(data.message);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}		
	});
};
function eDataTable_event(div)
{
	//数据结构顺序
	var tb=getobj("eDataTable_Columns");
	if(tb)
	{
		tb=new eDataTable("eDataTable_Columns",1);
		tb.moveRow=function(index,nindex)
		{
			var url = "?act=movecolumn&modelid=" + ModelID + "&index=" + index + "&nindex=" + nindex + "&t=" + now();
			showloading();
			$.ajax({
				type: "GET", async: true,
				url:url,
				dataType: "html",
				success: function (html) 
				{
					hideloading();
					//loadData(div);
				},
				error: function (XMLHttpRequest, textStatus, errorThrown) {
					hideloading();
				}
			});	
		};
	}
	//基本设置
	var tb1=getobj("eDataTable_Basic");
	if(tb1)
	{
		tb1=new eDataTable("eDataTable_Basic",1);
		tb1.moveRow=function(index,nindex)
		{
			$("#eDataTable_Basic tbody tr td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_Basic tbody tr td:first-child input:checked").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).parent().parent().attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodelitem&modelid=" + ModelID + "&item=setorders&t=" + now();			
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
	//列表
	var tb2=getobj("eDataTable_List");
	if(tb2)
	{
		tb2=new eDataTable("eDataTable_List",1);
		tb2.moveRow=function(index,nindex)
		{
			$("#eDataTable_List tbody tr td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_List tbody tr td:first-child input:checked").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).parent().parent().attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodelitem&modelid=" + ModelID + "&item=setlistorders&t=" + now();			
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
	//导出
	var tb3=getobj("eDataTable_Export");
	if(tb3)
	{
		tb3=new eDataTable("eDataTable_Export",1);
		tb3.moveRow=function(index,nindex)
		{
			$("#eDataTable_Export tbody tr td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_Export tbody tr td:first-child input:checked").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).parent().parent().attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodelitem&modelid=" + ModelID + "&item=setexportorders&t=" + now();			
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
	//搜索
	var tb4=getobj("eDataTable_Search");
	if(tb4)
	{
		tb4=new eDataTable("eDataTable_Search",1);
		tb4.moveRow=function(index,nindex)
		{
			$("#eDataTable_Search tbody > tr > td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_Search tbody > tr > td:nth-child(2) input:checked").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).parent().parent().attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodelcondition&modelid=" + ModelID + "&item=setorders&t=" + now();			
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
	//审核
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
			var _url = "?act=setcheckup&modelid=" + ModelID + "&item=setorders&t=" + now();
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
	//选项卡
	var tb6=getobj("eDataTable_Tab");
	if(tb6)
	{
		tb6=new eDataTable("eDataTable_Tab",1);
		tb6.moveRow=function(index,nindex)
		{
			$("#eDataTable_Tab tbody > tr > td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_Tab tbody > tr").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodeltab&modelid=" + ModelID + "&item=setorders&t=" + now();
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
	//面板
	var tb7=getobj("eDataTable_Panel");
	if(tb7)
	{
		tb7=new eDataTable("eDataTable_Panel",1);
		tb7.moveRow=function(index,nindex)
		{
			$("#eDataTable_Panel tbody > tr > td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_Panel tbody > tr").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodelgroup&modelid=" + ModelID + "&item=setorders&t=" + now();
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
	//布局-列
	var tb8=getobj("eDataTable_Items");
	if(tb8)
	{
		tb8=new eDataTable("eDataTable_Items",1);
		tb8.moveRow=function(index,nindex)
		{
			$("#eDataTable_Items tbody > tr > td:last-child").each(function(index1,obj){
				$(obj).html(1+index1);
			}); 
			var ids="";
			$("#eDataTable_Items tbody > tr").each(function(index1,obj){	
				if(index1>0){ids+=",";}
				ids+=$(obj).attr("erowid");
			}); 
			if(ids.length==0){return;}
			showloading();
			var _url = "?act=setmodelitem&modelid=" + ModelID + "&item=setorders&t=" + now();
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
//选项卡切换
function selecttab(a) 
{
        //alert(a.Index());
        $(a).parent().find("a").attr("class", "");
        $(a).attr("class", "cur");
        var index = $(a).index();
        var dd = $(a).parent().next();//alert(dd.html());
        dd.find("div").hide();
        //var div=dd.find("div:eq(" + index + ")"); //DIV下DIV也算在内
        var div = dd.children("div:eq(" + index + ")");
        var loaded = div.attr("loaded");
        //alert(loaded);
        if (loaded == undefined) {
            loadData(div);
        }
        div.show();
};
//加载层
function loadData(div)
{
	showloading();
    $.ajax({
		   type: "GET", async: true,
           url: div.attr("dataurl") + "&t=" + now(),
           dataType: "html",
           success: function (data) {
			   div.html(data);
			   executeJS(data);
               //div.attr("loaded", "true");
				eDataTable_event(div);
				hideloading();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert("error");
				hideloading();
                div.show();
            }
        });
};
$(document).ready(function () {

        //loadData($(".eTab dd div:eq(0)"));
        loadData($(".eTab dd").find("div").eq(0));
        //alert($(".eTab dd").find("div").eq(0));
});
//复制编码OK
function copyCode()
{
	showloading();
	var _url = "?act=copycode&modelid=" + ModelID + "&t=" + now();
    $.ajax({
		type: "GET", async: true,
		url: _url,
		dataType: "json",
		success: function (data)
		{
			hideloading();
			loadData($(".eTab dd").children("div").eq(1));
			//loadData($(".eTab dd").find("div").eq(1));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
    });
};
//还原编码OK
function restoreCode()
{
	showloading();
	var _url = "?act=restorecode&modelid=" + ModelID + "&t=" + now();
    $.ajax({
		type: "GET", async: true,
		url: _url,
		dataType: "json",
		success: function (data)
		{
			hideloading();
			loadData($(".eTab dd").children("div").eq(1));
			//loadData($(".eTab dd").find("div").eq(1));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
    });
};
//模块OK
function setModel(obj,Item,mid)
{
	if (obj.getAttribute("oldvalue") == obj.value) {hideloading(); return; }
	var value = getValue(obj);
	if (value == "error") {hideloading();  return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _modelid=ModelID;
	if(mid){_modelid=mid;}
	var _url = "?act=setmodel&modelid=" + _modelid + "&item=" + Item + "&t=" + now();
        //ajax:false同步,true异步
    $.ajax({
            type: "POST", async: true,
			data:{value:value},
            url: _url,
            dataType: "json",
            success: function (data) 
			{
                obj.setAttribute("oldvalue", obj.value);
				hideloading();
				if(!_reload){return;}
				loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
   });
};
//添加审批流程OK
function addCheckUp(obj)
{
	showloading();
    var _url = "?act=addcheckup&modelid=" + ModelID + "&t=" + now();
	$.ajax({
		   type: "GET", async: true,
           url: _url,
           dataType: "json",
           success: function (data) {
			   hideloading();
               loadData($(obj).parents("div[dataurl]"));
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
		   		hideloading();
           }
    });
};	
//修改审批流程OK
function setCheckUp(obj, CheckupID, Item)
{
	if (obj.getAttribute("oldvalue") == obj.value) { return; }
	var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setcheckup&modelid=" + ModelID + "&CheckupID=" + CheckupID + "&item=" + Item + "&t=" + now();
    $.ajax({
		   type: "post", 
		   async: true,
		   data:{value:value},
           url: _url,
           dataType: "html",
           success: function (data) {
			    obj.setAttribute("oldvalue", obj.value);
			 	hideloading();
			   	if(!_reload){return;}
               	loadData($(obj).parents("div[dataurl]"));
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
			   hideloading();
           }
    });
};
//删除审批流程OK
function delCheckUp(obj, CheckupID)
{
	if (!confirm('确认要删除吗？')) { return; }
	showloading();
	var _url = "?act=delcheckup&modelid=" + ModelID + "&CheckupID=" + CheckupID + "&t=" + now();
	$.ajax({
		   type: "GET", async: true,
           url: _url,
           dataType: "json",
           success: function (data) {
			   hideloading();
			   $(getParent(obj,"tr")).remove();
               //loadData($(obj).parents("div[dataurl]"));
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
			   hideloading();
           }
    });
};
//添加动作OK
function addAction(obj)
{
	showloading();
	var _url = "?act=addaction&modelid=" + ModelID + "&t=" + now();
	$.ajax({
		   type: "GET", async: true,
           url: _url,
           dataType: "json",
           success: function (data) {
			   hideloading();
               loadData($(obj).parents("div[dataurl]"));
           },
           error: function (XMLHttpRequest, textStatus, errorThrown) {
			   hideloading();
           }
    });
};
//修改动作OK
function setAction(obj, ActionID, Item)
{
	if (obj.getAttribute("oldvalue") == obj.value) { return; }	
	var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setaction&modelid=" + ModelID + "&actionid=" + ActionID + "&item=" + Item + "&t=" + now();
    $.ajax({
            type: "post", 
			async: true,
			data:{value:value},
            url: _url,
            dataType: "html",
            success: function (data) {
				obj.setAttribute("oldvalue", obj.value);
				hideloading();
				if(!_reload){return;}
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//删除动作OK
function delAction(obj, ActionID)
{
	if (!confirm('确认要删除吗？')) { return; }
	showloading();
	var _url = "?act=delaction&modelid=" + ModelID + "&actionid=" + ActionID + "&t=" + now();
    $.ajax({
            type: "GET", async: true,
            url: _url,
            dataType: "json",
            success: function (data) {
				hideloading();
				$(getParent(obj,"tr")).remove();
                //loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//添加模块-列OK
function addModelItem(obj)
{
	var _url = "?act=addmodelitem&modelid=" + ModelID + "&t=" + now();
	showloading();
	$.ajax({
            type: "GET", async: true,
            url: _url,
            dataType: "json",
            success: function (data) {
				hideloading();
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//修改模块-列OK
function setModelItem(obj, ModelItemID, Item)
{
	if (obj.getAttribute("oldvalue") == obj.value) { return; }
	var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setmodelitem&modelid=" + ModelID + "&modelitemid=" + ModelItemID + "&item=" + Item + "&t=" + now();
    $.ajax({
            type: "post", 
			async: true,
			data:{value:value},
            url: _url,
            dataType: "html",
            success: function (data) {
				obj.setAttribute("oldvalue", obj.value);
				hideloading();
				if(!_reload){return;}
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//删除模块-列OK
function delModelItem(obj, ModelItemID)
{
	if (!confirm('确认要删除吗？')) { return; }
	showloading();
	var _url = "?act=delmodelitem&modelid=" + ModelID + "&modelitemid=" + ModelItemID + "&t=" + now();
    $.ajax({
            type: "GET", async: true,
            url: _url,
            dataType: "json",
            success: function (data) {
				hideloading();
				$(getParent(obj,"tr")).remove();
                //loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//添加模块条件OK
function addModelCondition(obj)
{
	var _url = "?act=addmodelcondition&modelid=" + ModelID + "&t=" + now();
	showloading();
	$.ajax({
            type: "GET", async: true,
            url: _url,
            dataType: "json",
            success: function (data) {
				hideloading();
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//修改模块条件OK
function setModelCondition(obj, ModelConditionID, Item)
{
	var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setmodelcondition&modelid=" + ModelID + "&modelconditionid=" + ModelConditionID + "&item=" + Item + "&t=" + now();
    $.ajax({
            type: "post", 
			async: true,
			data:{value:value},
            url: _url,
            dataType: "json",
            success: function (data) {	
				obj.setAttribute("oldvalue", obj.value);
				hideloading();
				if(!_reload){return;}
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//删除模块条件OK
function delModelCondition(obj, ModelConditionID)
{
	if (!confirm('确认要删除吗？')) { return; }
	showloading();
	var _url = "?act=delmodelcondition&modelid=" + ModelID + "&modelconditionid=" + ModelConditionID + "&t=" + now();
    $.ajax({
            type: "GET", async: true,
            url: _url,
            dataType: "json",
            success: function (data) {
				hideloading();
				$(getParent(obj,"tr")).remove();
                //loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//添加模块条件-选项 OK
function addModelConditionItem(obj, ModelConditionID)
{
	showloading();
	var _url = "?act=addmodelconditionitem&modelid=" + ModelID + "&modelconditionid=" + ModelConditionID + "&t=" + now();
	$.ajax({
            type: "GET", async: true,
            url: _url,
            dataType: "json",
            success: function (data) {
				hideloading();
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//修改模块条件-选项OK;
function setModelConditionItem(obj, ModelConditionItemID, Item)
{
	var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setmodelconditionitem&modelid=" + ModelID + "&modelconditionitemid=" + ModelConditionItemID + "&item=" + Item + "&t=" + now();
    $.ajax({
            type: "post", 
			async: true,
			data:{value:value},
            url: _url,
            dataType: "json",
            success: function (data) {
				obj.setAttribute("oldvalue", obj.value);
				hideloading();
				if(!_reload){return;}
                loadData($(obj).parents("div[dataurl]"));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//删除模块条件-选项OK
function delModelConditionItem(obj, ModelConditionItemID)
{
	if (!confirm('确认要删除吗？')) { return; }
	showloading();
	var _url = "?act=delmodelconditionitem&modelid=" + ModelID + "&modelconditionitemid=" + ModelConditionItemID + "&t=" + now();
	$.ajax({
		   type: "GET", async: true,
           url: _url,
           dataType: "json",
           success: function (data) {
			   hideloading();
			   $(getParent(obj,"tr")).remove();
               //loadData($(obj).parents("div[dataurl]"));
           },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
				hideloading();
            }
    });
};
//取控件值
function getValue(obj)
{
    if (obj.type.toLowerCase() == "checkbox")
	{
		return (obj.checked ? "1" : "0");
	}
	else if(obj.type.toLowerCase() == "radio")
	{
		return obj.value;
	}
	else 
	{
		return obj.value.encode();
	}
};
//添加模块-选项卡
function addModelTab(obj)
{
	showloading();
	var _url = "?act=addmodeltab&modelid=" + ModelID + "&t=" + now();
    $.ajax({
		type: "GET", async: true,
		url: _url,
		dataType: "json",
		success: function (data) 
		{
			hideloading();
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			hideloading();
		}
	});
};
//修改模块-选项卡OK
function setModelTab(obj, ModelTabID, Item)
{
	if (obj.getAttribute("oldvalue") == obj.value) {return;}
    var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setmodeltab&modelid=" + ModelID + "&modeltabid=" + ModelTabID + "&item=" + Item + "&t=" + now();
	$.ajax({
		type: "post", 
		async: true,
		data:{value:value},
		url: _url,		
		dataType: "html",
		success: function (data)
		{
			obj.setAttribute("oldvalue", obj.value);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown)
		{
			hideloading();
		}
	});
};
//删除模块-选项卡
function delModelTab(obj, ModelTabID)
{
	if (!confirm('确认要删除吗？')) {return;}
	showloading();
	var _url = "?act=delmodeltab&modelid=" + ModelID + "&modeltabid=" + ModelTabID + "&t=" + now();
    $.ajax({
		type: "GET", async: true,
		url: _url,
		dataType: "json",
		success: function (data)
		{
			hideloading();
			$(getParent(obj,"tr")).remove();
			//loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown)
		{
			hideloading();
		}
	});
};
//添加模块-面板 OK
function addModelGroup(obj)
{
	showloading();
	var _url = "?act=addmodelgroup&modelid=" + ModelID + "&t=" + now();
    $.ajax({
		type: "GET", async: true,
		url: _url,
		dataType: "json",
		success: function (data) 
		{
			hideloading();
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown)
		{
			hideloading();
		}
	});
};
//修改模块-面板OK
function setModelGroup(obj, ModelPanelID, Item)
{
	if (obj.getAttribute("oldvalue") == obj.value) {return;}
    var value = getValue(obj);
	if (value == "error") { return; }
	var _reload=parseBool(Attribute(obj,"reload"));
	showloading();
	var _url = "?act=setmodelgroup&modelid=" + ModelID + "&modelpaneid=" + ModelPanelID + "&item=" + Item + "&t=" + now();
	$.ajax({
		type: "post",
		async: true,
		data:{value:value},
		url: _url,
		dataType: "html",
		success: function (data)
		{
			obj.setAttribute("oldvalue", obj.value);
			hideloading();
			if(!_reload){return;}
			loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown)
		{
			hideloading();
		}
	});
};
//删除模块-面板
function delModelGroup(obj, ModelPanelID)
{
	if (!confirm('确认要删除吗？')) {return;}
	showloading();
	var _url = "?act=delmodelgroup&modelid=" + ModelID + "&modelpaneid=" + ModelPanelID + "&t=" + now();
    $.ajax({
		type: "GET", async: true,
		url: _url,
		dataType: "json",
		success: function (data)
		{
			hideloading();
			$(getParent(obj,"tr")).remove();
			//loadData($(obj).parents("div[dataurl]"));
		},
		error: function (XMLHttpRequest, textStatus, errorThrown)
		{
			hideloading();
		}
	});
};
//打印设置
function savePrintHTML()
{	
	setModel($("#printHTMLStart").get(0), 'printhtmlstart');	
    setModel($("#printHTML").get(0), 'printhtml');
	setModel($("#printHTMLEnd").get(0), 'printhtmlend');
};