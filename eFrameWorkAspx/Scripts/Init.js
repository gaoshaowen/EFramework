var arrLayerIndex = new Array();
var virtualPath=getVirtualPath();
document.write('<script src="' + virtualPath + 'Scripts/jquery-1.8.3.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Scripts/eketeam.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/eClient/eClient.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/kindeditor351/kindeditor.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/eCalendar/eCalendar.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/eDataTable/eDataTable.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/eColumnMenu/eColumnMenu.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/layui226/layui.all.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/eImages/drag-arrange.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/raty/jquery.raty.min.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/selInput/selInput.js?beacon=7"></script>');
﻿document.write('<script src="' + virtualPath + 'Plugins/eFileList/eFileList.js?beacon=7"></script>');

//帮助中心窗口
function showHelp(ID)
{
	
	var url="http://frame.eketeam.com/HelpContent.aspx?id=" + ID;
	layer.open({
      type: 2,
	  skin: 'layui-layer-rim', //加上边框
      title: "帮助中心",
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
      area : ['1000px' , '65%'],
      content: url,
	  success: function(layero, index)
	  {
		arrLayerIndex.push(index);
  	  }
    });
};

//颜色选择器
function showColor(id)
{	
	var url=virtualPath + "Plugins/eColor/eColor.html?id=" + id;
	layer.open({
      type: 2,
	  skin: 'layui-layer-rim', //加上边框
      title: "颜色选择",
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
      area : ['270px' , '236px'],
      content: url,
	  
	  success: function(layero, index)
	  {
		  arrLayerIndex.push(index);
  	  }
    });
};
//百度地图
function openbdMap(id)
{	
	var url=virtualPath + "Plugins/bdmap/index.html?id=" + id;
	layer.open({
      type: 2,
	  skin: 'layui-layer-rim', //加上边框
      title: "百度地图位置",
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
      area : ['640px' , '580px'],
      content: url,
	  
	  success: function(layero, index)
	  {
		  arrLayerIndex.push(index);
  	  }
    });
};
//返回
function goBack()
{
	//if(top!=window)
	if(top!=self)//Frame打开
	{
		if(parent.arrLayerIndex.length>0)
		{
			parent.layer.close(parent.arrLayerIndex.pop());
		}
		else
		{
			history.back();
		}
	}
	else
	{
		history.back();		
	}
};
function goLink(obj)
{
	var url=obj.getAttribute("ehref");
	if(typeof(AppItem)=="string" && AppItem.length>0){url=url.addquerystring("AppItem",AppItem);}
	if(url.toLowerCase().indexOf("=del")>-1)
	{
		var _back=confirm('确认要删除吗？');
		if(!_back){return;};
	}
	else
	{
		if(url.toLowerCase().indexOf("=add")>-1 || url.toLowerCase().indexOf("=edit")>-1 || url.toLowerCase().indexOf("=view")>-1)
		{
			//if(url.indexOf(".aspx")== -1){url="ModelInfo.aspx" + url;}
			//else{url=url.replace("Model.aspx","ModelInfo.aspx");}
			url=url.addquerystring("ajax","true");
			

			var title=obj.innerText;
			if(title.indexOf("添加")>-1) title="添加";

			var table=getobj("eDataTable");			
			var rect = table.getBoundingClientRect();
			var rc = document.body.getBoundingClientRect();

			layer.open({
			  type: 2
			  ,title: title		
			  ,shadeClose: true //点击遮罩关闭层
			  ,area: [(rect.right-rect.left) + "px",(rc.bottom-rc.top-rect.top-10) + "px"]
			  ,offset: [rect.top + "px", rect.left + "px"]			  
			  ,content: url
			  
			  ,success: function(layero, index)
			  {
				arrLayerIndex.push(index);
			  }
			  ,cancel: function(index, layero)
			  {
				  arrLayerIndex.pop();
			  }				
			});		
			 
			 
			//alert(rect.right + "::" + rect.left);
			
			/*
			var index = layer.open({
			  type: 2
			  ,title: title			
			  ,content: url
				
			});
			//layer.full(index);
			*/
			
			
			//document.location.assign(url);
			
			return;
		}
		else
		{
			//alert(url);
		}
	}
	
	
	$.ajax({
		   type: "get",
		   url:url,
		   dataType: "html",
		   success: function(data)
		   {
			   var page=$("#page").val();
			   Search(page);
		   }
	});
};
//首页
function page_First()
{
	var table=getobj("eDataTable");
	var page=getobj("page");
	if(!table || !page){return;}	
	var ajax = parseBool(table.getAttribute("ajax"));
	
	var num=parseInt(page.value);
	if(num<=1){return;}

	if(ajax)
	{
		Search(1);
	}
	else
	{
		var url=getPageUrl();

		if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}

		url=url.addquerystring("page","1");
		url=url.addquerystring("t",now());
		document.location.assign(url);
	}
};
//上一页
function page_Previous()
{	
	var table=getobj("eDataTable");
	var page=getobj("page");
	if(!table || !page){return;}	
	var ajax = parseBool(table.getAttribute("ajax"));
	var num =parseInt(page.value);
	var maxnum = parseInt(page.getAttribute("maxvalue"));
	if(num<=1) {return;}
	num-=1;
	if(ajax)
	{
		Search(num);
	}
	else
	{
		var url=getPageUrl();
		if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}
		url=url.addquerystring("page",num);
		url=url.addquerystring("t",now());
		document.location.assign(url);
	}
};
//下一页
function page_Next()
{	
	var table=getobj("eDataTable");
	var page=getobj("page");
	if(!table || !page){return;}	
	var ajax = parseBool(table.getAttribute("ajax"));
	
	var num =parseInt(page.value);
	var maxnum = parseInt(page.getAttribute("maxvalue"));
	if(num>=maxnum) {return;}
	num+=1;
	if(ajax)
	{
		Search(num);
	}
	else
	{
		var url=getPageUrl();
		if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}
		url=url.addquerystring("page",num);
		url=url.addquerystring("t",now());
		document.location.assign(url);
	}
};
//尾页
function page_Last()
{
	var table=getobj("eDataTable");
	var page=getobj("page");
	if(!table || !page){return;}	
	var ajax = parseBool(table.getAttribute("ajax"));
	
	var num =parseInt(page.value);
	var maxnum = parseInt(page.getAttribute("maxvalue"));
	if(num>=maxnum) {return;}
	

	if(ajax)
	{
		Search(maxnum);
	}
	else
	{
		var url=getPageUrl();
		if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}
		url=url.addquerystring("page",maxnum);
		url=url.addquerystring("t",now());
		document.location.assign(url);
	}
};
//获取页面URL
function getPageUrl()
{
	var url=$("#frmsearch").attr("action") || document.location.href;	
	if(document.forms["frmsearch"])
	{
		var info=getSearchInfo(frmsearch);		
		var hash=info.hash;
		var keys = hash.keys();
		for (var entry in keys)
		{
			var key=keys[entry];
			var value=hash.get(key);
			url=url.addquerystring(key,value);		
			//alert(key + "=" + value);
		}
	}
	if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}
	return url;
};
//ajax搜索、跳转分页
function Search(page)
{
	page = page || $("#page").val() || 1;
	var url=$("#frmsearch").attr("action") || document.location.href;
	url=url.addquerystring("page",page);
	url=url.addquerystring("ajax","true");
	if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}
	
	if(document.forms["frmsearch"])
	{
		var info=getSearchInfo(frmsearch);		
		var hash=info.hash;
		var keys = hash.keys();
		for (var entry in keys)
		{
			var key=keys[entry];
			var value=hash.get(key);
			url=url.addquerystring(key,value);		
			//alert(key + "=" + value);
		}
	}
	
	url=url.addquerystring("t",now());
	var table=getobj("eDataTable");
	var orderby=table.getAttribute("orderby");
	url=url.removequerystring("orderby");
	
	if(orderby.length>0)
	{
		url=url.addquerystring("orderby",orderby);
	}


	var Loading = getobj("DivLoading");
	if(!Loading)
	{	
		
		//var rect = table.getBoundingClientRect();
		var rect = document.body.getBoundingClientRect();
	
		Loading = document.createElement("div");
		Loading.id="DivLoading";
		Loading.className="eDataTable_Loading";
		Loading.style.zIndex="200";	
		Loading.style.width = (rect.right -  rect.left) + "px";
		Loading.style.height = (rect.bottom -  rect.top) + "px";	
		//Loading.style.top = rect.top + "px";
		//Loading.style.left = rect.left + "px";
		Loading.style.top =  "0px";
		Loading.style.left =  "0px";
		document.body.appendChild(Loading);
	}
	




	//$(".page").val(page);
	
	$.ajax({
		   type: "get",
		   url:url,
		   dataType: "json",
		   success: function(data)
		   {
			   var dt=getobj("divlist");
			   if(dt)
			   {
			   		$("#divlist").html(data.table.decode());
			   }
			   else
			   {
				   
				   $("#eDataTable").prop("outerHTML", data.table.decode());
			   }

			   
			   //$("#eDataTable tbody").html(data.tbody.decode());
			   //$("#epage").html(data.epage.decode());
			   var pg=getobj("epage");
			   if(pg)
			   {
			   		pg.innerHTML=data.epage.decode();
			   }
			   else
			   {
				   $(".epager").prop("outerHTML", data.epage.decode());
			   }
			   deleteObject(Loading);
			   etable_init();
			   
			   var table=document.getElementById("eDataTable");
			   if(table)
			   {
				   /*
				   if(table.parent)
				   {
				   		Table_Init_Tbody(table.parent);
				   }
				   */
			   }
			   
		   }
	});
	

	
};

//提交搜索
function goSearch(frm)
{
	var info=getSearchInfo(frm);
	document.location=info.url;
	return false;
};

function getEasyUISearch(frm)
{
	var obj=new Object();
	var info=getSearchInfo(frm);
	var hash=info.hash;
	var keys = hash.keys();
	for (var entry in keys)
	{
		var key=keys[entry];
		var value=hash.get(key);
		obj[key] = value;
	}	
	return obj;
};


function showPower(obj)
{
	var div=obj.parentNode;
	div=div.nextSibling;
	while(div.tagName!="DIV")
	{
		div=div.nextSibling;
	}
	//if(div.nodeType==3){div=div.nextSibling;}
	if(obj.className=="close")
	{
		obj.className="open";
		div.style.display="";
	}
	else
	{
		obj.className="close";
		div.style.display="none";
	}
};
//角色选择、取消选择
function selectRoles(obj)
{
	//var url="?act=getrole&roleid=" + obj.value + "&t=" + now();

	$(".powerContent input[type='checkbox']").attr("checked",false);
	var div=$(obj).parent().parent();
	var inputs=div.find("input:checked");
	if(inputs.length==0){return;}
	var values="";
	inputs.each(function(i,elem){
		if(i>0){values+=",";}			 
		values+=elem.value;
    });

	var url=document.location.href;	
	if(typeof(AppItem)=="string" && AppItem.length > 0){url=url.addquerystring("AppItem",AppItem);}
	url=url.addquerystring("act","getrole");
	//url=url.addquerystring("roleid",obj.value);
	url=url.addquerystring("roleid",values);	
	url=url.addquerystring("t", now());
	
	//alert(url);
	//if(obj.type=="radio"){$(".powerContent input[type='checkbox']").prop("checked",false);}

	$.ajax({
		   type: "get",
		   url:url,
		   dataType: "json",
		   success: function(data)
		   {
			   //var json=new eJson(data);
			   //alert(json.tostring());
			   //$("#json").val( now() + "\n" +  json.tostring());
			   if(!data){return;}
			  // alert(JSON.stringify(data));
			   for(var i=0;i<data.length;i++)
			   {
				   for (p in data[i])
				   {
					   
					   if(p.toLowerCase()=="convert"){continue;}
					   if(typeof(data[i][p])=="function"){continue;}
					   if(p.toLowerCase() == "modelid" || p.toLowerCase() == "applicationitemid" || p.toLowerCase() == "applicationid"){continue;}
						//if(inputs.length==1) alert(p + "::" + data[i][p]);
						if(data[i][p].toLowerCase() == "true")
						{
							var sel=(data[i]["ApplicationItemID"] ?  getobj("model_" + p.toLowerCase() + "_" + data[i]["ApplicationItemID"].replace(/-/g,"") + "_" + data[i]["ModelID"].replace(/-/g,"")) : getobj("model_" + p.toLowerCase() + "_" +  data[i]["ModelID"].replace(/-/g,"")) );
							if(sel)
							{
								sel.checked=true;//obj.checked;
							}
							if(p.toLowerCase()=="list")
							{
								sel=(data[i]["ApplicationItemID"] ? getobj("model_" + data[i]["ApplicationItemID"].replace(/-/g,"") + "_" + data[i]["ModelID"].replace(/-/g,"")) : getobj("model_" + data[i]["ModelID"].replace(/-/g,"")));

								if(sel)
								{
									sel.checked=true;//obj.checked;
								}
								
							}
						}
				   }
			   }
		   }
	});
	//alert(url + "::" + obj.checked);	
};
//用户权限-全选
function userSelectAll(obj)
{
	var div=obj.parentNode.parentNode;	
	var inputs=div.getElementsByTagName("input");
	for(i=0;i < inputs.length;i++)
	{
		if(inputs[i]==obj || inputs[i].type.toLowerCase()!="checkbox"){continue;}
		inputs[i].checked=obj.checked;
	}
};
//用户权限-列表权限
function userCanelAll(obj)
{
	var div=obj.parentNode.parentNode;	
	var inputs=div.getElementsByTagName("input");
	if(obj.checked)
	{
		inputs[0].checked=true;
		return;
	}
	for(i=0;i < inputs.length;i++)
	{
		if(inputs[i]==obj || inputs[i].type.toLowerCase()!="checkbox"){continue;}
		inputs[i].checked=false;
	}
};

//$(function (){ });
//$(function (){
	//$(document).keydown(function(e){
//document.title=e.keyCode;
//});
//});
//回填到表单
function FillData(values)
{
	//.replace("m&#179;","m³")
	//alert(parent.arrLayerIndex.length + "::" + str);

//alert(window.opener);
	var arr=values.split("~");
	for(var i=0;i<arr.length;i++)
	{

		var objvalue=arr[i].split("=");

		if(objvalue.length<2){continue;}
		//if(objvalue[1].length==0){continue;}
		var objs=parent.document.getElementsByName(objvalue[0]);
		
		if(objs)
		{
			//document.title+=":" + objvalue[1].decode();
			// alert(objvalue[1].decode() + "::" + objs.length);
			if(objs.length==1)
			{			
				objs[0].value=objvalue[1].decode().replace(/%u003d/gi,"=");
				
				var etype=objs[0].getAttribute("etype");
				if(etype == "html")
				{
					try{parent.KE.html(objvalue[0],objs[0].value);}catch(e){}
					try{parent.KE.setFullHtml(objvalue[0],objs[0].value);}catch(e){}
				}
			}
			else
			{
				var _vs="," + objvalue[1].decode() + ",";
				for(var j=0;j < objs.length;j++)
				{
					if(_vs.indexOf("," + objs[j].value + ",")>-1)
					{
						objs[j].checked=true;
					}
					else
					{
						objs[j].checked=false;
					}
				}
			}
		}
	}	
	if(parent.arrLayerIndex.length>0)
	{
		parent.layer.close(parent.arrLayerIndex.pop());
	}
};
//回填窗口
function OpenFillDlg(modelID,title,width,height,area)
{
	
	var url="../Plugins/FillData.aspx?modelid=" + modelID + (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "") ;
	if(area){url+="&area=" + area;}
	

	layer.open({
      type: 2,
	  skin: 'layui-layer-rim', //加上边框
      title: title,
      maxmin: false,
      shadeClose: true, //点击遮罩关闭层
	  area: [width,height],
      //area : ['800px' , '520px'],
      content: url,
	  success: function(layero, index)
	  {
		arrLayerIndex.push(index);
  	  }
    });

	/*
	$.ajax({
			type: 'get',
			url: url,	
			dataType: "html",
			success: function(data)
			{
				alert(data);
			}
		});
	*/
};

function viewImage(src,w,h)
{
	
	var ieVer = 0;
	var isIE =  window.navigator.userAgent.indexOf("compatible") > -1 &&  window.navigator.userAgent.indexOf("MSIE") > -1;
	if(isIE)
	{
		var reg = new RegExp("MSIE (\\d+\\.\\d+);");
		reg.test(window.navigator.userAgent);
		ieVer = parseFloat(RegExp["$1"]);
	}
	var maxw= 1000;//screen.width - 200;
	if(w>maxw)
	{
		var bl=w/maxw;
		w=maxw;
		h=parseInt(h/bl);
	}
	layer.open({
  	type: 1
	,area:[(w+20) + 'px',(h+20) + 'px']
	//,shade: 0.2 //透明度
  	,title: false //不显示标题
	,closeBtn: 0
	,anim: (isIE && ieVer < 9 ? -1 : 4) //0-6的动画形式，-1不开启 IE9以下不支持动画
	,shadeClose: true //开启遮罩关闭
  	,content:'<div style="padding:10px;"><img src="' + src + '" width="'+ w +'"></div'
	,end: function()
	{
		arrLayerIndex.pop();
	}
	,success: function(layero, index)
	{
		setTimeout(function(){ document.focus();},1000);
		arrLayerIndex.push(index);
  	}
	
	});
};

function getVirtualPath()
{
	var elements = document.getElementsByTagName('script');
	var element=elements[elements.length-1];
	var src=element.getAttribute("src");
	var virtualPath="";
	if(src.indexOf("../")>-1)
	{
		for(var i=0;i<src.match(/\.\.\//ig).length;i++)
		{
			virtualPath+="../";
		}
	}
	return virtualPath;
};
//加载数据
function FillSelect(objId,Prars,Pid)
{	
	var url= virtualPath + "Plugins/getData.ashx?Pid=" + Pid;
	if(typeof(AppItem)=="string" && AppItem.length>0){url=url.addquerystring("AppItem",AppItem);}
	if(Prars.indexOf("&")!=0){url += "&";}
	url += Prars;	
	url += "&t=" + now(),

	$.ajax({
	type: "GET",
	async: false,
	url: url,
	dataType: "json",
	success: function(data)
	{
		$("#" + objId).get(0).length=1;
		if(!data){return;}
		//$("#" + ObjID).empty();
		 $.each(data, function()
		 {
			$("#" + objId).append('<option value="' + this["value"] + '">' + this["text"] + '</option>'); 
		 });
	}
	});
};
function form_view(obj,ModelID,title,width,height,ID) //自动模块
{
	if(!ID){ID=obj.getAttribute("eRowID");}
	var url="?modelid=" + ModelID + (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "") + "&act=viewsub&id=" + ID;
	var html=PostURL(url);
	layer.open({
  	type: 1 //此处以iframe举例
  	,title: title
	//,skin: 'layui-layer-molv' //加上边框 layui-layer-rim  layui-layer-lan layui-layer-molv layer-ext-moon
	,shadeClose: false //点击遮罩关闭层
  	,area: [width,height]
	,shade: 0.2 //透明度
	,maxmin: false
	,id: ModelID.replace(/-/gi,"") //设定一个id，防止重复弹出
	,resize: true
	,btnAlign: 'l' //lcr
	,moveType: 0 //拖拽模式，0或者1
	,anim: 0 //0-6的动画形式，-1不开启
	,content: html
	,btn: ['返回'] //只是为了演示
	,yes: function(index,layero)
	{ 
		arrLayerIndex.pop();
		layer.close(index);
	}	
	,cancel: function(index, layero)
	{
		//document.title="Close";
		arrLayerIndex.pop();
	}
	,success: function(layero, index)
	{
		arrLayerIndex.push(index);
		eDataTable_Init();
  	}
    });
	
};
function form_View(obj,ModelID,title,width,height,url) //自定义模块
{
	var html=PostURL(url);	
	layer.open({
  	type: 1 //此处以iframe举例
  	,title: title
	//,skin: 'layui-layer-molv' //加上边框 layui-layer-rim  layui-layer-lan layui-layer-molv layer-ext-moon
	,shadeClose: false //点击遮罩关闭层
  	,area: [width,height]
	,shade: 0.2 //透明度
	,maxmin: false
	,id: ModelID.replace(/-/gi,"") //设定一个id，防止重复弹出
	,resize: true
	,btnAlign: 'l' //lcr
	,moveType: 0 //拖拽模式，0或者1
	,anim: 0 //0-6的动画形式，-1不开启
	,content: html
	,btn: ['返回'] //只是为了演示
	,yes: function(index,layero)
	{ 
		arrLayerIndex.pop();
		layer.close(index);
	}	
	,cancel: function(index, layero)
	{
		arrLayerIndex.pop();
		//document.title="Close";
	}
	,success: function(layero, index)
	{
		arrLayerIndex.push(index);		
  	}
	
    });

	var tr= obj; //getParent(obj,"tr");
	var index=tr.Index();
	var form=getobj("form_" + ModelID);
	var input=getobj("eformdata_" + ModelID);
	var model=input.value.toJson();
	model=model.get("eformdata_" + ModelID);
	var data=model.get(index);
	data.foreach(function(e)
	{
		if(typeof(data[e])=="object")
		{			

			var ctrl = form[e];			
			//ctrl.value= "{\"" + p + "\":" + data[e].tostring() + "}";
			var node=data[e];			
			if(!node.append){node=new eJson(node);}
			node.Convert=true;
			ctrl.value= "{\"" + e + "\":" + node.tostring() + "}";
			JsonToTable(data[e],e.replace("eformdata_","eformlist_"));
		}
		else
		{
			if(e.toLowerCase()=="id"){return;}
			if(e.toLowerCase()=="delete"){return;}
			if(e.indexOf("_Text")>-1){return;}
			var ctrl=document.getElementById("span_" + e);
			if(!ctrl){return;}
			if(data[e+"_Text"])
			{
				ctrl.innerHTML=data[e+"_Text"].decode();
			}
			else
			{
				ctrl.innerHTML=data[e].decode();
			}
			//alert(e);
		}
	});
	eDataTable_Init();
};
function Check_CheckUp()
{
	
	var tr=getobj("checkup_trback");
	if(tr)
	{		
		//tr.style.display= (frmCheckUp["f1"].value == "1" ? "none" : "");
		if(frmCheckUp["f1"].value == "1")
		{
			
			tr.style.display = "none";
			frmCheckUp["f3"].setAttribute("notnull","false");
		}
		else
		{
			tr.style.display = "";
			frmCheckUp["f3"].setAttribute("notnull","true");
		}
	}	
};
function CheckUp()
{
	var url="CheckUp.aspx?modelid=" + ModelID +  (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "")  + "&id=" + document.location.href.getquerystring("id");	
	$.ajax({
			type: 'get',
			url: url,	
			dataType: "html",
			success: function(data)
			{
				var json=data.toJson();
				if(parseBool(json.success))
				{
					var html = json.html.decode();
					//alert(json.html.decode());
					//alert(data);
					layer.open({
					type: 1 //此处以iframe举例
					,title: json.title // + "窗口"
					//,skin: 'layui-layer-molv' //加上边框 layui-layer-rim  layui-layer-lan layui-layer-molv layer-ext-moon
					,shadeClose: false //点击遮罩关闭层
					//,area: ["500px",json.width + "300px"]
					,area: ["500px",json.height + "px"]
					,shade: 0.2 //透明度
					,maxmin: false
					,id: ModelID.replace(/-/gi,"") //设定一个id，防止重复弹出
					,resize: true
					,btnAlign: 'l' //lcr
					,moveType: 0 //拖拽模式，0或者1
					,anim: 0 //0-6的动画形式，-1不开启
					,content: html
					,btn: ['确定', '返回'] //只是为了演示
					,yes: function(index,layero)
					{ 
						var _back=eCheckform(frmCheckUp);	
						
						if(!_back){return;}
						if(_back){arrLayerIndex.pop();layer.close(index);}
						
						url= $("#frmCheckUp").attr("action");
						url=url.addquerystring("act","checkup");
						if(frmCheckUp["f1"])
						{
							var chk="0";
							if(frmCheckUp["f11"].checked){chk="1";}
							if(frmCheckUp["f12"].checked){chk="2";}
							if(frmCheckUp["f13"].checked){chk="3";}
							url=url.addquerystring("f1",chk);
						}
						if(frmCheckUp["f2"]){url=url.addquerystring("f2",frmCheckUp["f2"].value.encode());}
						if(frmCheckUp["f3"]){url=url.addquerystring("f3",frmCheckUp["f3"].value.encode());}


						if(typeof(AppItem)=="string" && AppItem.length>0){url=url.addquerystring("AppItem",AppItem);}
						$.ajax({
							type: 'get',
							url:url,
							dataType: "json",
							success: function(data)
							{
								if(parseBool(data.success))
								{
									url=document.location.href;
									
									$.ajax({
									type: 'get',
									url:url,
									dataType: "html",
									success: function(data)
									{
										//alert(data);
										var table=data;
										var sstr='<div id="content" style="margin:10px;">'
										var estr='</table>\r\n</div>';

										var s=table.indexOf(sstr);
										if(s>-1)
										{
											s+=sstr.length;
											var e=table.indexOf(estr,s);
											if(e>-1)
											{
												e+=estr.length;
												$("#content").html(table.substring(s,e) + "");
												layer.msg(json.title + "成功!");
											}
										}

									}
									});
										   
								}
							}
								
							});
	
	
				
					}
					,btn2: function(index, layero)
					{
						//document.title="关闭";
						arrLayerIndex.pop();
					}
					,cancel: function(index, layero)
					{
						//document.title="Close";
						arrLayerIndex.pop();
					}
					,success: function(layero, index)
					{
						arrLayerIndex.push(index);
					}
					});
				}
				else
				{
					layer.msg(json.message);
				}
			}
		});
};
function form_load(ModelID)
{

	var input=getobj("eformdata_" + ModelID);
	
	var model=input.value.toJson();
	model=model.get("eformdata_" + ModelID);
	if(model.length==0){return;}
	var data=model.get(0);
	var form=getobj("form_" + ModelID);
	data.foreach(function(e)
	{
		
		var ctrl = form[e];
		if(!ctrl)
		{
			//if(e.indexOf("_Text")>-1){return;}
			ctrl=document.getElementById("span_" + e);
			if(!ctrl){return;}
			if(ctrl.innerHTML.length>0) {return;}
			
			if(data[e+"_Text"])
			{
				ctrl.innerHTML=data[e+"_Text"].decode();
			}
			else
			{
				ctrl.innerHTML=data[e].decode();
			}
		}
		if(!ctrl){return;}
		
		if(typeof(data[e])=="object")
		{
			var table=getobj(ctrl.name.replace("eformdata_","eformlist_"));
			if(table)
			{
				if(table.getAttribute("reinit")!="false")
				{
					var node=data[e];
					if(!node.append){node=new eJson(node);}			
					node.Convert=true;
					//alert(node.tostring());
					ctrl.value="{\"" + e + "\":" + node.tostring() + "}";
					JsonToTable(data[e],e.replace("eformdata_","eformlist_"));
				}
			}
		}
		else
		{
			
			if(typeof(e)=="string")
			{
				if(ctrl.tagName)//一个
				{
					var etype = ctrl.getAttribute("etype");					
					ctrl.value= data[e].decode();
					if(etype=="select")
					{
						if(ctrl.selectedIndex==-1){ctrl.selectedIndex=0;}
					}
					if(etype=="file")
					{
						var frame=getobj("frm" + ctrl.name);
						var url=frame.getAttribute("src");
						if(ctrl.value.length > 0)
						{
							url=url.addquerystring("act","finsh");
							url=url.addquerystring("file",ctrl.value);
						}
						else
						{
							url=url.removequerystring("act");
							url=url.removequerystring("file");
						}
						frame.src=url;
					}
					if(etype=="html")
					{

						KE.html(ctrl.name,data[e].decode());
					}
				}
				else//多个
				{
					var type=Attribute(ctrl[0],"type").toLowerCase();
					var _tmp="," + data[e].decode() + ",";
					for(var i=0;i<ctrl.length;i++)
					{
						if(_tmp.indexOf("," + ctrl[i].value + ",")>-1)
						{
							ctrl[i].checked=true;
						}
					}
				}

			}
		}		
	});	
	eDataTable_Init();
};
//OK
function form_add(obj,ModelID,title,width,height,url)
{
	
	var html=getAddHTML(ModelID,url);
	//alert(html);
	layer.open({
  	type: 1 //此处以iframe举例
  	,title: title
	//,skin: 'layui-layer-molv' //加上边框 layui-layer-rim  layui-layer-lan layui-layer-molv layer-ext-moon
	,shadeClose: false //点击遮罩关闭层
  	,area: [width,height]
	,shade: 0.2 //透明度
	,maxmin: false
	,id: ModelID.replace(/-/gi,"") //设定一个id，防止重复弹出
	,resize: true
	,btnAlign: 'l' //lcr
	,moveType: 0 //拖拽模式，0或者1
	,anim: 0 //0-6的动画形式，-1不开启
	,content: html
	,btn: ['确定', '关闭'] //只是为了演示
	,yes: function(index,layero)
	{ 
		var _back=form_save(ModelID);
		if(_back){arrLayerIndex.pop();layer.close(index);}
	}
	,btn2: function(index, layero)
	{
		//document.title="关闭";
		arrLayerIndex.pop();
	}
	,cancel: function(index, layero)
	{
		//document.title="Close";
		arrLayerIndex.pop();
	}
	,success: function(layero, index)
	{
		arrLayerIndex.push(index);
		$('.layui-layer-content textarea[etype*="html"]').each(function(i,elem){
			KE.init({
				id : elem,
				width :  elem.getAttribute("width") ,
				height : elem.getAttribute("height")
			});
			KE.create(elem.id);
		});
  	}
    });
	
	var form=getobj("form_" + ModelID);	
	if(form){form["Index"].value="-1";}
};
//OK
function form_edit(obj,ModelID,title,width,height,url)
{
	if(getEventObject().tagName.toLowerCase()=="a"){return;}
	var Prefix="M" + ModelID + "_";
	Prefix="";
	var rect=obj.getBoundingClientRect();
	var html=getAddHTML(ModelID,url);
	
	//var frm=new eDialog("model_form_" + ModelID.replace(/-/gi,""),"修改",rect.left,Scroll().top + rect.bottom,500,300);
	//frm.setHTML(html);
	layer.open({
  	type: 1 //此处以iframe举例
  	,title: title
	//,skin: 'layui-layer-molv' //加上边框 layui-layer-rim  layui-layer-lan layui-layer-molv layer-ext-moon
	,shadeClose: false //点击遮罩关闭层
  	,area: [width,height]
	,shade: 0.2 //透明度
	,maxmin: false
	,id: ModelID.replace(/-/gi,"") //设定一个id，防止重复弹出
	,resize: true
	,btnAlign: 'l' //lcr
	,moveType: 0 //拖拽模式，0或者1
	,anim: 0 //0-6的动画形式，-1不开启
	,content: html
	,btn: ['确定', '关闭'] //只是为了演示
	,yes: function(index,layero)
	{ 
		var _back=form_save(ModelID);
		if(_back){arrLayerIndex.pop();layer.close(index);}
	}
	,btn2: function(index, layero)
	{
		arrLayerIndex.pop();
		//document.title="关闭";
	}
	,cancel: function(index, layero)
	{
		arrLayerIndex.pop();
		//document.title="Close";
	}
	,success: function(layero, index)
	{
		arrLayerIndex.push(index);
		$('.layui-layer-content textarea[etype*="html"]').each(function(i,elem){
			//弹出层有问题
			KE.init({
				id : elem,
				width :  elem.getAttribute("width") ,
				height : elem.getAttribute("height")
			});
			KE.create(elem.id);
			//KE.html(elem.id, '<h3>Hello KindEditor</h3>');

		});
  	}

    });
	
	
	
	var tr=obj;//getParent(obj,"tr");
	//var index=0;
	//for(i=0;i<trs.length;i++){if(trs[i]== tr){index= i;}}
	
	var index=tr.Index();
	var form=getobj("form_" + ModelID);
	if(form[Prefix + "Index"])
	{
		form[Prefix + "Index"].value=index;
	}
	
	var input=getobj("eformdata_" + ModelID);
	var model=input.value.toJson();
	model.Convert=true;	
	model=model.get("eformdata_" + ModelID)	
	var data=model.get(index);	
	data.foreach(function(e)
	{
		var ctrl = form[e];
		if(!ctrl){return;}
		if(typeof(data[e])=="object")
		{
			var node=data[e];			
			if(!node.append){node=new eJson(node);}
			node.Convert=true;
			ctrl.value= "{\"" + e + "\":" + node.tostring() + "}";
			JsonToTable(data[e],e.replace("eformdata_","eformlist_"));
		}		
		else
		{
			if(typeof(e)=="string")
			{
				if(ctrl.tagName)
				{
					
					ctrl.value= data[e].decode();
					
					var etype=Attribute(ctrl,"etype").toLowerCase();
					if(etype=="label" && ctrl.value.length>0)
					{
						var onext = ctrl.nextElementSibling||ctrl.nextSibling;						
						onext.innerHTML=ctrl.value;
					}
					if(etype=="html" && ctrl.value.length>0)
					{
						KE.html(ctrl.id, ctrl.value);
						//alert(etype + "::" + ctrl.value + "::" + ctrl.id);
					}
					//alert(etype + "::" + ctrl.value);
					if(etype=="file" && ctrl.value.length>0)
					{
						var frame=getobj("frm" + ctrl.id );
						if(frame)
						{
							var src=frame.getAttribute("src");
							if(src.toLowerCase().indexOf("act=")==-1)
							{
								src+="&act=finsh";
							}
							src+="&file=" + ctrl.value;
							//alert(src);
							//alert(ctrl.id + "::" + data[e]);
							frame.src=src;
						}
					}
				}
				else
				{
					var type=Attribute(ctrl[0],"type").toLowerCase();
					var _tmp="," + data[e].decode() + ",";
					for(var i=0;i<ctrl.length;i++)
					{
						if(_tmp.indexOf("," + ctrl[i].value + ",")>-1)
						{
							ctrl[i].checked=true;
						}
					}
				}
			}
		}
	});
	
	eDataTable_Init();
};

//OK
function form_delete(obj,ModelID,delText)
{
	if(!delText){delText="删除";}
	var Prefix="M" + ModelID + "_";
	Prefix="";
	if(!confirm("确认要" + delText + "吗？")){return;}
	
	var tr=getParent(obj,"tr");
	var tbody=getParent(obj,"tbody");
	var trs=tbody.getElementsByTagName("tr");
	//var index=0;
	//for(i=0;i<trs.length;i++){if(trs[i]== tr){index= i;}}
	var index=tr.Index();
	
	var input=getobj("eformdata_" + ModelID);
	
	var model=input.value.toJson();
	model.Convert=true;

	
	var _model=model.get("eformdata_" + ModelID);
	_model=_model[index];

	if(_model[Prefix + "ID"].length==0)
	{
		_model=model.get("eformdata_" + ModelID)
		_model.remove(index);	
		tbody.removeChild(tr);
	}
	else
	{
		_model[Prefix + "Delete"]="true";
		tr.style.display="none";
	}
	var count=getobj("count_" + ModelID);

	if(count)
	{
		
		count.value= $(tbody).find("tr:visible").length;// tbody.children.length;
	}
	input.value=model.tostring();	

	
};
//OK
function form_save(ModelID)
{
	
	var frm= document.forms["form_" + ModelID];
	if(!frm){alert("表单form_" + ModelID + "不存在!");return false;}
	var Prefix="M" + ModelID + "_";
	Prefix="";
	var _back=eCheckform(frm);
	if(!_back){return false;}
	var input=getobj("eformdata_" + ModelID);
	var model=input.value.toJson();	
	model.Convert=true;
	
	//alert(model.tostring());
	//return;
	var _data=model.get("eformdata_" + ModelID);

	var index=parseInt(frm[Prefix + "Index"].value);
	var table=getobj("eformlist_" + ModelID);
	var tbody=table.getElementsByTagName("tbody")[0];

	var obj= formToJson(frm);
	//alert(new eJson(obj).tostring());
	
	var tfoot=table.getElementsByTagName("tfoot")[0];
	var otr=tfoot.getElementsByTagName("tr")[0].cloneNode(true);
	var otds=otr.getElementsByTagName("td");
	
	if(index>-1)//替换
	{
		_data[index]=obj;
		var tr=tbody.getElementsByTagName("tr")[index];
		//tr,td 不可以使用getElementsByName  getElementById 所以要从TD 去读JSON数据，而不是从Json数据到TD
		//alert(tr.outerHTML);
		var tds=tr.getElementsByTagName("td");
		
		for(var i=0;i<tds.length;i++)
		{
			var name=tds[i].getAttribute("name").replace("td_","");//正
			
			if(name)
			{				
				if(obj[name+"_Text"]){tds[i].innerHTML=obj[name+"_Text"].decode();continue;}
				
				if(obj[name])
				{
					if(otds[i].innerHTML.toLowerCase().indexOf("data:")==-1)
					{
						tds[i].innerHTML=obj[name].decode();
					}
					else
					{
						tds[i].innerHTML=otds[i].innerHTML.replace(/{data:.*?}/gi,obj[name].decode());
					}
				}
				else
				{
					tds[i].innerHTML="";
				}
			}
		}	
	}
	else //添加
	{
		_data.append(obj);
		for(var i=0;i<otds.length;i++)
		{
			var name=otds[i].getAttribute("name").replace("td_","");
			if(name)
			{
				if(obj[name+"_Text"]){otds[i].innerHTML=obj[name+"_Text"].decode();continue;}
				if(obj[name])
				{
					
					if(otds[i].innerHTML.toLowerCase().indexOf("data:")==-1)
					{
						
						
						otds[i].innerHTML=  obj[name].decode();
					}
					else
					{
						otds[i].innerHTML=otds[i].innerHTML.replace(/{data:.*?}/gi,obj[name].decode());
					}
				}
				else
				{
					otds[i].innerHTML="";
				}
			}
		}
		tbody.appendChild(otr);
		//alert(table.parent);
		if(table.parent){Table_Init_Tbody(table.parent);}
		
	}
	//alert(model.tostring());
	input.value=model.tostring();
	//form_cancel(ModelID);
	var count=getobj("count_" + ModelID);
	if(count)
	{
		count.value=$(tbody).find("tr:visible").length;// tbody.children.length;
	}
	
	/*
	
	setTimeout(function(){
	eDataTable_Init();
	if(table.parent){Table_Init_Tbody(table.parent);}
	//alert(table.id);
	},500);
	
	if(!table.parent)
	{
		eDataTable_Init();
	}
	else
	{
		Table_Init_Tbody(table.parent);
	}
	*/
	return true;
};
//OK
function getAddHTML(ModelID,_url)
{


	var html="";	
	
	//eval("var modelhtml=window.ModelHTML_" + ModelID + ";");
	var modelhtml=eval("window.ModelHTML_" + ModelID.replace(/-/gi,"") );
	if(modelhtml)
	{
		html=modelhtml;
		//document.title="get";
	}
	else
	{
		//var url="?modelid=" + ModelID + (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "")  + "&act=addsub";
		
		var url="?modelid=" + ModelID + "&act=addsub";
		//alert(url);
		if(_url){url=_url;}
		html=PostURL(url);
		eval('window.ModelHTML_'+ ModelID.replace(/-/gi,"") +'=html;');
		//document.title="set";
	}
	return html;
};

function form_init(ModelID)
{
	var frm= document.forms["form_" + ModelID];
	if(!frm){alert("表单form_" + ModelID + "不存在!");return false;}
	
	var executenames="";
	var elements=frm.elements;
	
	for(var i=0; i < elements.length; i++)
	{
		if(executenames.indexOf(elements[i].name.toLowerCase())>-1){continue;}
		if(elements[i].getAttribute("Exclude")!=null){continue;}
		if(elements[i].tagName.toLowerCase()=="fieldset"){continue;}
		if(elements[i].name.toLowerCase()=="index"){continue;}
		if(elements[i].name.toLowerCase().indexOf("_index") > -1 ){continue;}
		if(elements[i].name.toLowerCase()=="page"){continue;}
		
		//var fieldname = elements[i].getAttribute("fieldname");
		
		var defaultvalue = elements[i].getAttribute("defaultvalue");
		
		if(defaultvalue==null){defaultvalue="";}
		if(elements[i].name.indexOf("eformdata_")>-1)
		{
			
			var table=getobj(elements[i].name.replace("eformdata_","eformlist_"));
			if(table)
			{
				if(table.getAttribute("reinit")=="false"){continue;}
				var tbody=table.getElementsByTagName("tbody")[0];			
				var trs=tbody.getElementsByTagName("tr");					
				for(var j=trs.length;j > 0;j--)
				{
					trs[0].parentNode.removeChild(trs[0]);
				}
			}
			elements[i].value='{"' + elements[i].name + '":[]}';
		}
		else
		{
			var etype = elements[i].getAttribute("etype");
			if(etype==null){etype="";}
			//alert(etype + defaultvalue);
			if(defaultvalue.length>0 && etype != "checkbox" && etype!="radio")
			{
				elements[i].value = defaultvalue;
			}
			else
			{
				if(etype != "checkbox" && etype!="radio")
				{
					elements[i].value = "";
				}
			}
			if(etype=="autoselect" && elements[i].name.indexOf("all")==-1)
			{
				
				var tmp;
				for(var j=2;j<10;j++)
				{
					tmp=getobj(elements[i].name + "_" + j);
					

					if(!tmp){break;}
					tmp.parentNode.removeChild(tmp);
				}
				tmp=getobj(elements[i].name + "_1");
				tmp.value="";
			}
			if(etype=="file")
			{
				var frame=getobj("frm" + elements[i].name);
				var url=frame.getAttribute("src");
				url=url.removequerystring("act");
				url=url.removequerystring("file");
				
				frame.src=url;
			}
			if(etype=="html"){KE.html(elements[i].name,defaultvalue);}
			if(etype=="checkbox" || etype=="radio")
			{
				executenames+="," + elements[i].name.toLowerCase() + ",";
				var chks=document.getElementsByName(elements[i].name);
				for(var j=0; j < chks.length; j++)
				{
					chks[j].checked=(defaultvalue.length > 0 && defaultvalue.indexOf(chks[j].value) > -1 ? true : false);
				}

			}
			
		}
	}
};
//OK
function submitform(frm)
{	
	if(typeof(frm)=="string"){frm= document.forms[frm];}
	if(!frm){return;}
	if(frm.onsubmit()!=false)
	{
		frm.submit();
		var loading= document.createElement("div");
		loading.className="eform_loading";
		document.body.appendChild(loading);
	}
};
function packform(ModelID,ajax)
{
	if(Attribute(frmaddoredit,"finsh")=="true"){return;}
	var frm= document.forms["form_" + ModelID];
	if(!frm){alert("表单form_" + ModelID + "不存在!");return;}
	var _back=eCheckform(frm);
	if(!_back){return;}	
	var obj= formToJson(frm);	
	//alert(ModelID);
	//return;
	
	//var str='{"eformdata_' + ModelID + '":[]}';
	//var model=str.toJson();	
	//model["eformdata_"+ ModelID]=obj;

	//alert(obj.tostring());
	//return;
	
	var model=new eJson();
	model.Convert=true;
	model.append("ModelID",ModelID);
	model.append("eformdata_" + ModelID,obj);

	var input=getobj("eformdata_" + ModelID);
	input.value=model.tostring();
	//alert(model.tostring());
	//alert(ajax);
	if(ajax)
	{
	
		//alert("ajax" + $("#frmaddoredit").serialize());
		//if(!frmaddoredit["ajax"]){var input= document.createElement("input");input.type="hidden";input.name="ajax";input.value="true";frmaddoredit.appendChild(input);}
		$.ajax({
			type: 'post',
			url: $("#frmaddoredit").attr("action"),			
			data: "ajax=true&" + $("#frmaddoredit").serialize(),
			dataType: "json",
			success: function(data)
			{

				//layer.alert(data,{title:"提示" ,area: ['800px', '600px']});
				//alert(data);
				var _json=eJson(data);
				//alert(_json.tostring());
				//return;
				if(typeof(parent.postback)=="function")
				{
					parent.postback(data);
					return;
				}

				//data=eJson(data);
				if(parseBool(data.success))
				{
					var json=eJson(data);
					json.Convert=true;
					if(frmaddoredit["id"] && frmaddoredit["id"].value){frmaddoredit["id"].value = json["id"];}
					if(frm["id"] && frm["id"].value){frm["id"].value = json["id"];}
					//layer.alert( json["id"] + "::" +  json.tostring(),{title:"提示" ,area: ['800px', '600px']});
					//alert(json.tostring());
				
					var data=json.get("data").get(0);
					data.Convert=true;		
					
					
					
					//form_init(ModelID);
					//input.value=data.tostring();
					//form_load(ModelID);
					
					//layer.msg("保存成功!");
					layer.msg("保存成功!", {
					time: 0 //20s后自动关闭
					,shade: 0.2 //透明度
					,shadeClose: false //点击遮罩关闭层
					
					,btn: ["添加新记录","继续修改","返回"]
					,yes:function(index,layero)
					{
						
						//layer.msg("添加" + ModelID);
						input.value="";
						form_init(ModelID);

						frm["id"].value="";
						layer.close(index);
					}
				    ,btn2:function(index,layero)
					{
						frm["id"].value=json.id;
						//layer.msg("修改");
					}
					,btn3:function(index,layero)
					{
						
						//layer.msg("返回" + frmaddoredit["fromurl"].value);
						//document.location.assign(frmaddoredit["fromurl"].value);		
						
						if(top!=self)//Frame打开
						{	
							if(parent.arrLayerIndex.length>0)
							{
								parent.layer.close(parent.arrLayerIndex.pop());
								parent.Search();
							}
						}
						
						
						
					}
				  	});
				}
				else
				{
					
					//alert("失败!");
				}

				
			}
		});
	}
	else
	{
		//var btn=getEventObject();
		
		frmaddoredit.setAttribute("finsh","true");
		frmaddoredit.submit();
		
		var loading= document.createElement("div");
		loading.className="eform_loading";
		document.body.appendChild(loading);
	}
	return;
};
//OK
function TableToJson(ModelID)
{
	var model=new eJson();
	model.Convert=true;
	var table=getobj("eformlist_" + ModelID);
	var tbody=table.getElementsByTagName("tbody")[0];
	var trs=tbody.getElementsByTagName("tr");
	
	for(var j=0;j < trs.length;j++)
 	{
  		model.append("eformdata_" + ModelID,nodeToJson(trs[j]));
	}
	var input=getobj("eformdata_" + ModelID);
	input.value=model.tostring();
};
//OK
function nodeToJson(node)
{
	var json=new eJson();
	var inputs=node.getElementsByTagName("input");
 	for(var i=0;i < inputs.length;i++)
 	{
		if(inputs[i].name.length==0){continue;}
		var type= inputs[i].getAttribute("type").toString().toLowerCase();
		//if(inputs[i].getAttribute("Exclude")!=null){continue;}
		if(type == "submit" || type == "button" || type == "reset"){continue;}
		var value=inputs[i].value.trim().encode();
		if((type == "radio" || type=="checkbox") && inputs[i].checked==false){value="";}
		if(!json[inputs[i].name])
		{
			json[inputs[i].name] = value;
		}
		else
		{
			json[inputs[i].name] += "," + value;
		}
 	}

 	var selects=node.getElementsByTagName("select");
	for(var i=0;i < selects.length;i++)
	{
		var value=selects[i].value.trim().encode();
		json[selects[i].name] = value;
	}
	var textareas=node.getElementsByTagName("textarea");
	for(var i=0;i < textareas.length;i++)
	{
		var value=textareas[i].value.trim().encode();
		json[textareas[i].name] = value;
	}
	return json;
};
function formToJson(frm)
{
	if(typeof(frm)=="string"){frm= document.forms[frm];}
	var elements=frm.elements;
	//var json=new Object();
	var json=new eJson();
	for(var i=0; i < elements.length; i++)
	{
		
		if(elements[i].getAttribute("Exclude")!=null){continue;}		
		if(elements[i].tagName.toLowerCase()=="fieldset"){continue;}
		if(elements[i].name.toLowerCase()=="index"){continue;}
		if(elements[i].name.toLowerCase().indexOf("_index") > -1 ){continue;}
		if(elements[i].name.toLowerCase()=="page"){continue;}
		if(elements[i].name.length==0){continue;}
		
		
		var type= "";
		if(elements[i].getAttribute("type")){type=elements[i].getAttribute("type").toString().toLowerCase();};
		
		if(type.length==0)
		{
			if(elements[i].tagName.toLowerCase()!="select" && elements[i].tagName.toLowerCase()!="textarea")
			{
				continue;
			}
		}		
		if(type == "submit" || type == "button" || type == "reset"){continue;}
		if((type == "radio" || type=="checkbox") && elements[i].checked==false){;continue;}
	
		var value=elements[i].value;
		//alert(elements[i].name + "::" + value);
			
		if(type=="text" ||  elements[i].tagName.toLowerCase()=="textarea")
		{
			//if(elements[i].name.toLowerCase().indexOf("eformdata_") == -1) value = value.encode64();
		}

		if(elements[i].name.toLowerCase().indexOf("eformdata_")>-1) //子模块对象 
		{
			var _json=value.toJson();
			_json=_json.get(elements[i].name);
			json[elements[i].name]=_json;
		}
		else
		{
			value=value.trim().encode();
			if(!json[elements[i].name])
			{
				json[elements[i].name] = value;				
			}
			else
			{
				json[elements[i].name] += "," + value;
			}
			if(elements[i].getAttribute("etype")=="autoselect" && type=="hidden" && elements[i].name.toLowerCase().indexOf("_all")==-1 )
			{
				var txt="";
				for(var x=1;x<100;x++)
				{
					var tmp=getobj(elements[i].name + "_" + x);
					if(!tmp){break;}
					if(txt.length>0){txt+=",";}
					txt+=tmp.options[tmp.selectedIndex].text;
					
				}
				json[elements[i].name + "_Text"] = txt.encode();
			}
			if(elements[i].tagName.toLowerCase()=="select")
			{

				json[elements[i].name + "_Text"] = elements[i].options[elements[i].selectedIndex].text.encode();
			}
			if(type == "radio" || type=="checkbox")
			{
				if(elements[i].getAttribute("text"))
				{
					if(!json[elements[i].name + "_Text"])
					{
						json[elements[i].name + "_Text"] = elements[i].getAttribute("text").encode();
					}
					else
					{
						json[elements[i].name + "_Text"] += "," + elements[i].getAttribute("text").encode();
					}
				}
			}
		}		
	}
	return json;
};
function JsonToTable(data,tableID)
{
	var table=getobj(tableID);
	var tbody=table.getElementsByTagName("tbody")[0];
	var tfoot=table.getElementsByTagName("tfoot")[0];
	data.foreach(function(e)
	{
		if(data[e]["Delete"]=="true") {return;}
		var tr=tfoot.getElementsByTagName("tr")[0].cloneNode(true);		
		var tds=tr.getElementsByTagName("td");
		for(var j=0;j<tds.length;j++)
		{
			var name=tds[j].getAttribute("name");
			if(name)
			{
				if(data[e][name+"_Text"])
				{
					tds[j].innerHTML=data[e][name+"_Text"].decode();
					continue;
				}
				if(data[e][name])
				{
					tds[j].innerHTML=data[e][name].decode();
				}
			}
		}
		tbody.appendChild(tr);
		//alert(tr.outerHTML);
	});
	
};
//OK
function form_cancel(ModelID)
{
	var form=getobj("form_" + ModelID);
	if(form){form["Index"].value="-1";}
	var frm=getobj("model_form_" + ModelID.replace(/-/gi,""));	
	if(frm){frm.parentNode.removeChild(frm);}
};
//OK
//$(document).ready(function() { }); 
//$("#id").unbind("click").click(function(e){  });

var quick_input;
var quick_show=false;
var quick_clip=null;
var quick_keys="";
function quick_search()
{
	quick_keys = quick_input.value;
	document.onkeydown=function(e)
	{
		e=window.event||e;
		var src=e.srcElement||e.target;
		if(src.tagName != "BODY"){return true;}
		if(e.keyCode == 116 ){return true;}
		//document.title=e.keyCode;
		if(e.keyCode == 8 && quick_show )
		{
			if(quick_keys.length>0)
			{
				quick_keys=quick_keys.substring(0,quick_keys.length-1);
				quick_clip.innerHTML = quick_keys;
			}			
			if (e.stopPropagation){e.stopPropagation();}else{event.cancelBubble = true;}
			if (e.preventDefault){e.preventDefault();}else{e.cancelBubble = true;}
			return false;
		}
		if(e.keyCode<65 || e.keyCode>90){return false;}
		
		return true;
	};

	quick_clip = document.createElement("span");
	quick_clip.id = "quick_clip";
	quick_clip.className="quick_clip";
	quick_clip.style.display = "none";
	
	quick_clip.innerHTML = quick_keys;// 'ABC';
	document.body.appendChild(quick_clip);

	
	
	$("body").bind("keydown",function(e){  
		e=window.event||e;
		var src=e.srcElement||e.target;
		if(src.tagName != "BODY"){return true;}
		//document.title+=e.keyCode;// String.fromCharCode(e.keyCode);
		if(e.keyCode==27)//ESC
		{
			quick_keys="";
			quick_show=false;
			quick_clip.style.display = "none";
			quick_clip.innerHTML = quick_keys;
			return false;  
		}
		if(e.keyCode==13)//回车
		{
			if(quick_show)
			{
				quick_show=false;
				quick_clip.style.display = "none";
				if(quick_keys.length>0 || quick_keys.length == 0)
				{
					quick_input.value=quick_keys;
					var tb=getobj("eDataTable");
					var ajax = parseBool(tb.getAttribute("ajax"));
					if(ajax)
					{
						Search(1);
					}
					else
					{
						goSearch(document.forms["frmsearch"]);
					}
				}
			}
			else
			{
				quick_show=true;
				quick_clip.style.display = "";
			}
			e.keyCode = 0;
			return false;  
		}
		//if(quick_show && e.keyCode>= 65&& e.keyCode<=90)
		if(quick_show && e.keyCode>= 65 )
		{
			
			quick_keys+=String.fromCharCode(e.keyCode);
			quick_clip.innerHTML = quick_keys;
		}
		
	});
};
//星级评分初始
function raty_init()
{
	 $('.raty').each(function(i,elem){ 
		
		var dataid=$(this).attr('data-id');
		var staroff = $(this).attr('data-staroff');	
		var starhalf = $(this).attr('data-starhalf');
		var staron = $(this).attr('data-staron');
		var _readonly = $(this).attr('data-readonly');
		var readonly=false;
		if(_readonly=="true"){readonly=true;}
		var _half = $(this).attr('data-half');
		var half=false;
		if(_half=="true"){half=true;}
		
		if(dataid)
		{
			$(this).raty({ 
				  width:'100%',
				  hints: ['','','','',''],
				  readOnly: readonly, 
				  targetType: 'number',
				  targetKeep: true,
				  half: half,
				  halfShow: half,
				  target    : '#' + dataid,
				  score: function() { return $(this).attr('data-score');},
				  number: function() {return $(this).attr('data-number'); },  
				  starOff : staroff,
				  starHalf  : starhalf,
				  starOn  : staron			  
			  });
		}
		else
		{
			$(this).raty({ 
				  width:'100%',
				  hints: ['','','','',''],
				  readOnly: readonly, 
				  targetType: 'number',
				  targetKeep: true,
				  half: half,
				  halfShow: half,
				  score: function() { return $(this).attr('data-score');},
				  number: function() {return $(this).attr('data-number'); },  
				  starOff : staroff,
				  starHalf  : starhalf,
				  starOn  : staron			  
			  });
		}
	});
	
};
function init()
{
	raty_init();
	//document.title=":";
	if(getobj("eSearchBox"))
	{
		var sbox=getobj("eSearchBox");
		var inputs=sbox.getElementsByTagName("input");		
		for(var i=0;i<inputs.length;i++)
		{
			if(inputs[i].getAttribute("etype")=="quick")
			{
				quick_input=inputs[i];
				quick_search();
				break;
			}
		}
	}
					  
	//$("body").bind("contextmenu", function(event) {return false; }); 
	$("body").bind("keydown",function(e){    
  		e=window.event||e; 
		//document.title+= e.keyCode;
		if(e.keyCode==37)//上一页
		{
			page_Previous();
			return;
		}
		if(e.keyCode==38)//首页
		{
			page_First();
			return;
		}
		if(e.keyCode==39)//下一页
		{
			page_Next();
			return;
		}
		if(e.keyCode==40)//尾页
		{
			page_Last();
			return;
		}
		if(e.ctrlKey && e.keyCode==70)
		{ 
			//document.title="ctrl + f";
    		e.keyCode = 0;  
			var sbox=getobj("eSearchBox");
			if(sbox)
			{
				var h1=sbox.children[0];
				showPanel(h1);
			}
    		return false;  
 		} 
		//document.title=e.keyCode;
		//屏蔽F5刷新键  
		var tb=getobj("eDataTable");
		var ajax = parseBool(tb.getAttribute("ajax"));
	   if(e.keyCode==116 && ajax)
	   { 
	   		
			e.keyCode = 0; //IE下需要设置为keyCode为false
			Search();
			return false;  
	   }  
	   //Esc
	   if(e.keyCode==27)
		{
			if(arrLayerIndex.length>0)
			{
				layer.close(arrLayerIndex.pop());
			}
			else
			{
				if(parent.arrLayerIndex.length>0)
				{
					parent.layer.close(parent.arrLayerIndex.pop());
				}	
			}
		}	
   
   
    });
	/*
	$(document).keydown(function(e){	
	if(e.keyCode==27)
	{
		if(arrLayerIndex.length>0)
		{
			layer.close(arrLayerIndex.pop());
		}
		if(parent.arrLayerIndex.length>0)
		{
			parent.layer.close(parent.arrLayerIndex.pop());
		}		
	}
	});	
	*/
	var keyevent=new eKeyEvent();
	keyevent.onkeydown=function()
	{
		if(this.keyCode==2700)
		{
			if(arrLayerIndex.length>0)
			{
				layer.close(arrLayerIndex.pop());
			}
			else
			{
				if(parent.arrLayerIndex.length>0)
				{
					parent.layer.close(parent.arrLayerIndex.pop());
				}	
			}
		}		
		if(this.keyCode==116000)
		{
			if(1==13)
			{
				keyevent.clearEvent=true;
				Search();
			}
			
			//document.location.assign(url);
		}
		if(this.keyCode==7000000 && this.ctrlKey)//F 70
		{
			keyevent.clearEvent=true;
			var sbox=getobj("eSearchBox");
			if(sbox)
			{
				var h1=sbox.children[0];
				showPanel(h1);
			}
			//if(this.Node.tagName=="INPUT"){return;}
			//if(this.ctrlKey && this.keyCode ==65 && this.Node.tagName!="INPUT")
			//if(this.keyCode==46)//DELETE
			//if(this.keyCode==69 && this.ctrlKey)//E
			//if(this.keyCode==86 && this.ctrlKey)//V
			//if(this.keyCode==67 && this.ctrlKey)//C
			//if(this.keyCode==27)//ESC
		}
	};
	//alert("init_init");
	etable_init();
};
function etable_init()
{
	var eTable=new eDataTable("eDataTable",1);
	eTable.rowover=function(tr){};
	eTable.rowout=function(tr){};
	eTable.setWidth=function(td,widtharr)
	{
		var url="?";	
		if(typeof(AppItem)=="string" && AppItem.length > 0){url+="AppItem=" + AppItem;}
		if(typeof(ModelID)=="string" && ModelID.length > 0){url+="ModelID=" + ModelID;}
		url+="&act=setwidths&value=" + widtharr + "&t=" + now();
	

		//var url="?act=setwidths&modelid="+ ModelID + (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "") + "&value=" + widtharr + "&t=" + now();

		//$.ajax({type:"GET",async:false,url:_url,dataType:"html"});	
		$.ajax({type: "GET",async: false,url:url,dataType: "html",success: function(data)
		{

		},
		error: function(XMLHttpRequest, textStatus, errorThrown)
		{
  				// 状态码
                    //console.log(XMLHttpRequest.status);
                    // 状态
                    //console.log(XMLHttpRequest.readyState);
                    // 错误信息   
                   //console.log(textStatus);
		}
		});	
	};
	eTable.setHeight=function(td,height)
	{
		
		var url="?";	
		if(typeof(AppItem)=="string" && AppItem.length > 0){url+="AppItem=" + AppItem;}
		if(typeof(ModelID)=="string" && ModelID.length > 0){url+="ModelID=" + ModelID;}
		url+="&act=setlineheight&value=" + height + "&t=" + now();
		
		//var url="?act=setlineheight&modelid="+ ModelID +  (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "") + "&value=" + height + "&t=" + now();
		$.ajax({type:"GET",async:false,url:url,dataType:"html",success:function(data)
		{
		}
		});		
	};
	eTable.sort5=function(td,value)
	{
		//document.title= "::" + value + "_";
		//return false;
		var cancelAutoUrl = true;
		return cancelAutoUrl;
	};
	eTable.ColumnMoved=function(tr)
	{
		return;
		var value="";
		for(var i=0;i<tr.children.length;i++)
		{
			if(Attribute(tr.children[i],"eCellID").length==0){continue;}
			value+= (value.length == 0 ? "" : "|") + Attribute(tr.children[i],"eCellID") + "_" + (parseInt(tr.children[i].cellIndex) + 1);
		}
		if(value.length==0){return;}
		var _url="?act=setsortall&modelid="+ ModelID + (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "") + "&value=" + value + "&t=" + now();
		//$.ajax({type:"GET",async:false,url:_url,dataType:"html"});	
	};
	eTable.moveColumn=function(tr,index,new_index)
	{
		var td=tr.children[new_index];
		var value=new_index+1;
		//var url="?act=setsort&modelid="+ ModelID + (typeof(AppItem)=="string" && AppItem.length > 0 ? "&AppItem=" + AppItem : "") + "&ModelItemID=" + Attribute(td,"eCellID") + "&value=" + value + "&t=" + now();
		
		
		var url="?";	
		if(typeof(AppItem)=="string" && AppItem.length > 0){url+="AppItem=" + AppItem;}
		if(typeof(ModelID)=="string" && ModelID.length > 0){url+="ModelID=" + ModelID;}
		url+="&act=setsort&ModelItemID=" + Attribute(td,"eCellID") + "&value=" + value + "&t=" + now();
		
		//document.title=Attribute(td,"eCellID") + ":::" + value;
		$.ajax({type:"GET",async:false,url:url,dataType:"html"});	
		//alert(tr.children.length + ":::" + index + "::::" + new_index);
		//outText("moveColumn:" +  index + "::" + new_index);
		//document.title= index + "::" + new_index;
	};
	//eTable.sort=function(td){document.title=td.innerText;};
	eTable.moveRow=function(index,new_index)
	{
		//document.title= index + "::" + new_index;
	};
};
if(window.addEventListener){window.addEventListener("load",init, false);}else{window.attachEvent("onload",init);}

/*
*/