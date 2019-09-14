function ColumnMenu_Esc()
{
	var event = arguments[0]||window.event; 
	var keyCode=event.which || event.keyCode;
	if(keyCode==27){cancelColumnMenu();}
};
function showColumnMenu(obj)
{
	var box=document.getElementById("column_menu");	
	if(!box){return;}

	if(typeof(sortmenu_close)=="function")
	{
		sortmenu_close();
	}
	var Rect = obj.getBoundingClientRect();
	box.style.left="-2000px";
	box.style.top="-2000px";
	showObject(box);
	
	box.style.left=(new eScroll().left +　Rect.right - box.offsetWidth) + "px";
	box.style.top=(new eScroll().top +　Rect.bottom - 1) + "px";
	if (window.addEventListener){document.addEventListener("mouseup",cancelColumnMenu, false);document.addEventListener("keyup",ColumnMenu_Esc, true);}
	else{document.attachEvent("onmouseup",cancelColumnMenu);document.attachEvent("onkeyup",ColumnMenu_Esc);}
};
function cancelColumnMenu()
{
	
	hideObject("column_menu");
	if (document.addEventListener){document.removeEventListener('mouseup', cancelColumnMenu, false);document.removeEventListener('keyup', ColumnMenu_Esc, true);}
	else{document.detachEvent("onmouseup",cancelColumnMenu);document.detachEvent("onkeyup",ColumnMenu_Esc);}
	//alert(3);
};
function showColumn(obj,ajax)
{
	var value=0;
	var ct=$(obj).parent().find(".cur").length;	
	if(ct==1 && obj.className=="cur")
	{
		alert("不能全部取消!");
		return;
	}
	if(obj.className=="cur")
	{
		obj.className="";		
	}
	else
	{
		obj.className="cur";
		value=1;
	}
	hideObject("column_menu");
	
	
	var modelitemid=obj.getAttribute("CellID");


	var url="?";	
	if(typeof(AppItem)=="string" && AppItem.length > 0){url+="AppItem=" + AppItem;}
	if(typeof(ModelID)=="string" && ModelID.length > 0){url+="ModelID=" + ModelID;}
	
	url+="&act=showcolumn&modelitemid=" + modelitemid + "&value=" + value + "&t=" + now();

	//$.ajax({type:"GET",async:false,url:_url,dataType:"html"});	
	if(ajax)
	{
		//document.title=ajax;
		$.ajax({
			   type:"GET",
			   async:false,
			   url:url,
			   dataType:"html",
			   success:function(data)
			   {
				   //var page=$("#page").val();
				   //Search(page);
				   Search();
			   }
		});	
	}
	else
	{
			$.ajax({type:"GET",async:false,url:url,dataType:"html",success:function(data)
			{
				document.location.assign(document.location.href.replace("#",""));
			}
			});
		
	}
};