/**
 * drag-shift
 * Copyright (c) 2014 Vishal Kumar
 * Licensed under the MIT License.
 */
﻿document.write('<script src="' + virtualPath + 'Plugins/eImages/ajaxfileupload.js?beacon=7"></script>');
function eImage_setjson(pbody)
{
		var json='[';
		pbody.find("div").each(function(index, element){
			if(index>0){json+=',';}
			json+='{"url":"' + $(this).attr("url") + '"}';
        });
		json+=']';
		if(json.length>2)
		{
			pbody.find("input:eq(0)").val(json);
		}
		else
		{
			pbody.find("input:eq(0)").val("");
		}
};
function checkext(path)
{
	var exts=".gif.jpg.jpeg.png.bmp.tif";
	var arr=path.toLowerCase().split(".");
	var ext="." + arr[arr.length-1];	
	if(exts.indexOf(ext)>-1)
	{
		return true;
	}
	else
	{
		return false;
	}
};
function eImage_change(obj)
{
	//var form = new FormData(obj.parentNode);
	//alert(FormData);
	//alert(eImage_setjson);
	//return;
	//alert(formData);
	//return;
	var _back=true;
	var _url=$(obj).attr("vpath") + "Plugins/ProUpload.aspx?type=image&ThumbWidth=120";
	if($(obj).attr("PictureMaxWidth").length>1) {_url+="&PictureMaxWidth=" + $(obj).attr("PictureMaxWidth");}
	
	if(obj.files)
	{
		var formData = new FormData();
		for(var i=0;i<obj.files.length;i++)
		{
			if(!checkext(obj.files[i].name))
			{
				alert("文件类型不支持,只支持以下格式\n(jpg|jpeg|gif|bmp|png|tif)");
				obj.value="";
				return;
			}
			formData.append('files', obj.files[i], obj.files[i].name);
		}
		
		$(obj).hide();
		$.ajax({
            type: "POST", async: true,
			data:formData,
            url: _url,
            dataType: "json",
			contentType: false,
			//dataType: "formData",
			cache: false,//上传文件无需缓存
            processData: false,//用于对data参数进行序列化处理 这里必须false
            success: function (data) 
			{
				for(var i=0;i < data.files.length; i++)
				{					
					$(obj).parent().before('<div onclick="viewImage(\'' +  data.files[i].url.replace("_thumb","") + '\',800,600);" url="' + data.files[i].url + '" style="background-image:url(' + data.files[i].url + ');"><img src="'+ $(obj).attr("vpath") +'images/none.gif" /></div>');					
				}
				var pbody=$(obj).parent().parent();
				
				pbody.find("div").arrangeable();
				obj.value="";
				eImage_setjson(pbody);
				
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {}
        });
		$(obj).show();
		
		
	}
	else
	{
		_back=checkext(obj.value);
		if(!_back)
		{
			alert("文件类型不支持,只支持以下格式\n(jpg|jpeg|gif|bmp|png|tif)");
			obj.value="";
			return;
		}

		
		 $.ajaxFileUpload ({
						   type: "post",
						   url: _url, //用于文件上传的服务器端请求地址 
						   secureuri: false, //一般设置为false 
						   fileElementId: $(obj).attr("id"), //文件上传空间的id属性  <input type="file" id="file" name="file" /> 
						   dataType: "json", //返回值类型 一般设置为json 
						   success: function (data, status)  //服务器成功响应处理函数
                		   {
							  
							   for(var i=0;i < data.files.length; i++)
							   {
								   //alert(data.files[i].url.replace("_thumb",""));
									$(obj).parent().before('<div onclick="viewImage(\'' +  data.files[i].url.replace("_thumb","") + '\',800,600);" url="' + data.files[i].url + '" style="background-image:url(' + data.files[i].url + ');"><img src="'+ $(obj).attr("vpath") +'images/none.gif" /></div>');					
								}
								var pbody=$(obj).parent().parent();								
								pbody.find("div").arrangeable();
								obj.value="";
								eImage_setjson(pbody);
						   }, 
						   error: function (data, status, e)//服务器响应失败处理函数
                		   {
								//alert(e); 
						   } 
				});		 
	}
};
'use strict';
(function (factory) {
  if (typeof define === 'function' && define.amd) {
    // AMD. Register as an anonymous module.
    define(['jquery'], factory);
  } else {
    // Browser globals
    factory(jQuery);
  }
}(function ($) {
  var IS_TOUCH_DEVICE = ('ontouchstart' in document.documentElement);
  /**
   * mouse move threshold (in px) until drag action starts
   * @type {Number}
   */
  var DRAG_THRESHOLD = 5;
  /**
   * to generate event namespace
   * @type {Number}
   */
  var counter = 0;

  /**
   * Javascript events for touch device/desktop
   * @return {Object}
   */
  var dragEvents = (function () {
    if (IS_TOUCH_DEVICE) {
      return {
        START: 'touchstart',
        MOVE: 'touchmove',
        END: 'touchend'
      };
    }
    else {
      return {
        START: 'mousedown',
        MOVE: 'mousemove',
        END: 'mouseup'
      };
    }
  }());

  $.fn.arrangeable = function(options) {
	var t=null;
    var dragging = false;
    var $clone;
    var dragElement;
    var originalClientX, originalClientY; // client(X|Y) position before drag starts
    var $elements;                        // list of elements to shift between
    var touchDown = false;
    var leftOffset, topOffset;
    var eventNamespace;

    if (typeof options === "string") {
      // check if want to destroy drag-arrange
      if (options === 'destroy') {
        if (this.eq(0).data('drag-arrange-destroy')) {
          this.eq(0).data('drag-arrange-destroy')();
        }

        return this;
      }
    }

    options = $.extend({
      "dragEndEvent": "drag.end.arrangeable"
    }, options);

    var dragEndEvent = options["dragEndEvent"];

    $elements = this;
    eventNamespace = getEventNamespace();

    this.each(function() {

      // bindings to trigger drag on element
	  
      var dragSelector = options.dragSelector;
      var self = this;
      var $this = $(this);
	  var $body= $this.parent();
	$(this).unbind().mouseover(function (){
		$this.find("img").show();
    }).mouseout(function () {
		$this.find("img").hide();
    });
	
	 $this.find("img").unbind().click(function (e) {
		e.stopPropagation();
		var _back=confirm('确认要删除吗？');
		if(!_back){return;};
		var $div=$(this).parent();		
		var $url=$div.attr("url");
		if($url.toLowerCase().indexOf("/temp/") > -1)
		{
			var $file=$div.parent().find(".file");			
			var url=$file.attr("vpath") + "Plugins/ProUpload.aspx?act=del&file=" + $url;			
			$.ajax({
		   		type: "GET", async: true,
           		url: url,
           		dataType: "html",
           		success: function (data) {}
       	 	});		
		}
		$this.remove();
		eImage_setjson($body);
    });


      if (dragSelector) {
        $this.on(dragEvents.START + eventNamespace, dragSelector, dragStartHandler);
      } else {
        $this.on(dragEvents.START + eventNamespace, dragStartHandler);
      }
	  self.onmouseup=function(e)
	  {
		  clearInterval(t);
		  dragging=false;
		  touchDown=false;
		  dragElement.style.cursor="pointer";
	  };
	  var draw=function(){
		  clearInterval(t);
		  touchDown=true;
		  
		  dragElement.style.cursor="move";
	  };	
      function dragStartHandler(e) {
        // a mouse down/touchstart event, but still drag doesn't start till threshold reaches
        // stopPropagation is compulsory, otherwise touchmove fires only once (android < 4 issue)
        e.stopPropagation();
		t=setInterval(draw,350);
		
        //touchDown = true;
		//document.title="true";
        originalClientX = e.clientX || e.originalEvent.touches[0].clientX;
        originalClientY = e.clientY || e.originalEvent.touches[0].clientY;
        dragElement = self;
      }
    });

    // bind mouse-move/touchmove on document
    // (as it is not compulsory that event will trigger on dragging element)
    $(document).on(dragEvents.MOVE + eventNamespace, dragMoveHandler)
      .on(dragEvents.END + eventNamespace, dragEndHandler);

    function dragMoveHandler(e) {
      if (!touchDown) { return; }

      var $dragElement = $(dragElement);
      var dragDistanceX = (e.clientX  || e.originalEvent.touches[0].clientX) - originalClientX;
      var dragDistanceY = (e.clientY || e.originalEvent.touches[0].clientY) - originalClientY;

      if (dragging) {
        e.stopPropagation();

        $clone.css({
          left: leftOffset + dragDistanceX,
          top: topOffset + dragDistanceY
        });

        shiftHoveredElement($clone, $dragElement, $elements);

      // check for drag threshold (drag has not started yet)
      } else if (Math.abs(dragDistanceX) > DRAG_THRESHOLD ||
          Math.abs(dragDistanceY) > DRAG_THRESHOLD) {
        $clone = clone($dragElement);

        // initialize left offset and top offset
        // will be used in successive calls of this function
        leftOffset = dragElement.offsetLeft - parseInt($dragElement.css('margin-left')) - 
          parseInt($dragElement.css('padding-left'));
        topOffset = dragElement.offsetTop - parseInt($dragElement.css('margin-top')) - 
          parseInt($dragElement.css('padding-top'));

        // put cloned element just above the dragged element
        // and move it instead of original element
        $clone.css({
          left: leftOffset,
          top: topOffset
        });
        $dragElement.parent().append($clone);

        // hide original dragged element
        $dragElement.css('visibility', 'hidden');

        dragging = true;
      }
    };
	
    function dragEndHandler(e) {
      if (dragging) {
        // remove the cloned dragged element and
        // show original element back
        e.stopPropagation();
        dragging = false;
        $clone.remove();
        dragElement.style.visibility = 'visible';

        $(dragElement).parent().trigger(dragEndEvent, [$(dragElement)]);
		eImage_setjson($(dragElement).parent());
		//alert($(dragElement).html());

		if(options.dragEnd !== undefined)
		{
        	if(typeof options.dragEnd === "function")
			{
          		//options.dragEnd(dragElement);
			}
			else
			{
          		//eval(options.dragEnd + '(' + dragElement + ');');
		  	}
		}
      }
      touchDown = false;
    };

    function destroy() {
      $elements.each(function() {
        // bindings to trigger drag on element
        var dragSelector = options.dragSelector;
        var $this = $(this);

        if (dragSelector) {
          $this.off(dragEvents.START + eventNamespace, dragSelector);
        } else {
          $this.off(dragEvents.START + eventNamespace);
        }
      });

      $(document).off(dragEvents.MOVE + eventNamespace)
        .off(dragEvents.END + eventNamespace);

      // remove data
      $elements.eq(0).data('drag-arrange-destroy', null);

      // clear variables
      $elements = null;
      dragMoveHandler = null;
      dragEndHandler = null;
    }

    this.eq(0).data('drag-arrange-destroy', destroy);
  };

  function clone($element) {
    var $clone = $element.clone();

    $clone.css({
      position: 'absolute',
      width: $element.width(),
      height: $element.height(),
      'z-index': 100000 // very high value to prevent it to hide below other element(s)
    });

    return $clone;
  };

  /**
   * find the element on which the dragged element is hovering
   * @return {DOM Object} hovered element
   */
  function getHoveredElement($clone, $dragElement, $movableElements) {
    var cloneOffset = $clone.offset();
    var cloneWidth = $clone.width();
    var cloneHeight = $clone.height();
    var cloneLeftPosition = cloneOffset.left;
    var cloneRightPosition = cloneOffset.left + cloneWidth;
    var cloneTopPosition = cloneOffset.top;
    var cloneBottomPosition = cloneOffset.top + cloneHeight;
    var $currentElement;
    var horizontalMidPosition, verticalMidPosition;
    var offset, overlappingX, overlappingY, inRange;

    for (var i = 0; i < $movableElements.length; i++) {
      $currentElement = $movableElements.eq(i);

      if ($currentElement[0] === $dragElement[0]) { continue; }

      offset = $currentElement.offset();

      // current element width and draggable element(clone) width or height can be different
      horizontalMidPosition = offset.left + 0.5 * $currentElement.width();
      verticalMidPosition = offset.top + 0.5 * $currentElement.height();

      // check if this element position is overlapping with dragged element
      overlappingX = (horizontalMidPosition < cloneRightPosition) &&
        (horizontalMidPosition > cloneLeftPosition);

      overlappingY = (verticalMidPosition < cloneBottomPosition) &&
        (verticalMidPosition > cloneTopPosition);

      inRange = overlappingX && overlappingY;

      if (inRange) {
        return $currentElement[0];
      }
    }
  };

  function shiftHoveredElement($clone, $dragElement, $movableElements) {
    var hoveredElement = getHoveredElement($clone, $dragElement, $movableElements);

    if (hoveredElement !== $dragElement[0]) {
      // shift all other elements to make space for the dragged element
      var hoveredElementIndex = $movableElements.index(hoveredElement);
      var dragElementIndex = $movableElements.index($dragElement);
      if (hoveredElementIndex < dragElementIndex) {
        $(hoveredElement).before($dragElement);
      } else {
        $(hoveredElement).after($dragElement);
      }

      // since elements order have changed, need to change order in jQuery Object too
      shiftElementPosition($movableElements, dragElementIndex, hoveredElementIndex);
    }
  };

  function shiftElementPosition(arr, fromIndex, toIndex) {
    var temp = arr.splice(fromIndex, 1)[0];
    return arr.splice(toIndex, 0, temp);
  };

  function getEventNamespace() {
    counter += 1;

    return '.drag-arrange-' + counter;
  };

}));
