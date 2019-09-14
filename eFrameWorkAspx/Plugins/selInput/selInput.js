function set_selText(obj)
{
	var span=obj.parentNode.parentNode;
	var input =span.getElementsByTagName("input")[0];
	input.value=obj.innerHTML;
	
	var ul = obj.parentNode;	
	ul.style.display="none";
	
	if(event.cancelBubble){event.cancelBubble=true;event.returnValue=false;}
	else{event.stopPropagation();event.preventDefault();}
};
function show_selText(obj)
{
	var src = getsrcElement(); 
	var ul =obj.getElementsByTagName("ul")[0];
	if(src.tagName == "INPUT" && ul.style.display != "none"){ul.style.display="none";return;}
	
	if(src.tagName != "SPAN"){return;}
	
	
	if(ul.style.display=="none")
	{
		ul.style.display="";
	}
	else
	{
		ul.style.display="none";
	}
};
function getsrcElement()
{
	var evt,src;
	if(window.event)
	{
		evt=window.event;
		src = evt.srcElement;
	}
	else
	{
		var o = arguments.callee;
		var e;
		while(o != null)
		{
			e = o.arguments[0];
			if(e && (e.constructor == Event || e.constructor == MouseEvent))
			{
				evt=e;src=evt.target; 
				break;
			}
			o = o.caller;
		}
	}
	return src;
};