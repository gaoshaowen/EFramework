function eFileList_setjson(ol)
{
	var json='[';
	ol.find("li").each(function(index, element){		
		if(index>0){json+=',';}
		json+='{"name":"' + $(this).find("a:eq(0)").text() + '","url":"' + $(this).find("a:eq(0)").attr("href") + '"}';
    });
	json+=']';
	if(json.length>2)
	{
		ol.parent().find("[type='hidden']").val(json);
	}
	else
	{
		ol.parent().find("[type='hidden']").val("");
	}
};
function eFileList_del(obj)
{
	if(!confirm("确认要删除吗？")){return;}	
	var ol=$(obj).parent().parent();
	$(obj).parent().remove();
	eFileList_setjson(ol);	
};
function eFileList_upload(obj)
{	
	var _url=$(obj).attr("vpath") + "Plugins/ProUpload.aspx?type=file";
	if(obj.files)
	{
		var formData = new FormData();
		for(var i=0;i<obj.files.length;i++)
		{
			formData.append('files', obj.files[i], obj.files[i].name);
		}		
		$(obj).hide();
		$.ajax({
            type: "POST", 
			async: true,
			data:formData,
            url: _url,           
			contentType: false,
			//dataType: "formData",
			cache: false,//上传文件无需缓存
            processData: false,//用于对data参数进行序列化处理 这里必须false
			dataType: "json",
            success: function (data) 
			{
				//alert(JSON.stringify(data));
				for(var i=0;i < data.files.length; i++)
				{		
					$(obj).parent().parent().find("ol:eq(0)").append('<li><a href="' + data.files[i].url + '" target="_blank">' + data.files[i].name + '</a><a href="javascript:;" class="del" onclick="eFileList_del(this);">&nbsp;</a></li>');					
				}
				
				var ol=$(obj).parent().parent().find("ol");			
				obj.value="";
				eFileList_setjson(ol);				
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
			}
        });
		$(obj).show();	
	}
	else
	{
		 $.ajaxFileUpload ({
		 	type: "post",
			url: _url, //用于文件上传的服务器端请求地址 
			secureuri: false, //一般设置为false 
			fileElementId: $(obj).attr("id"), //文件上传空间的id属性  <input type="file" id="file" name="file" /> 
			dataType: "json", //返回值类型 一般设置为json 			
			success: function (data, status)  //服务器成功响应处理函数
			{
				//alert(JSON.stringify(data));
				for(var i=0;i < data.files.length; i++)
				{
					$(obj).parent().parent().find("ol:eq(0)").append('<li><a href="' + data.files[i].url + '" target="_blank">' + data.files[i].name + '</a><a href="javascript:;" class="del" onclick="eFileList_del(this);">&nbsp;</a></li>');			
				}
				var ol=$(obj).parent().parent().find("ol");
				obj.value="";
				eFileList_setjson(ol);
			}, 
			error: function (data, status, e)//服务器响应失败处理函数
			{
			} 
		});		 
	}
};