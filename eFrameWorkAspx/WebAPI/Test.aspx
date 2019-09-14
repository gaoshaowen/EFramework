<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="eFrameWork.WebAPI.Test" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Auth测试</title>
</head>
<script src="../Scripts/jquery-1.8.3.js"></script>
<script>
var token="<%=tokenstr%>";
    var eServer = "http://demo.eketeam.com/WebAPI/";
    //eServer = "http://192.168.1.8/ekeframev8/WebAPI/";
	eServer = "http://localhost/ekeframev8/WebAPI/";
function gettoken()
{
    $.ajax({
        type: "POST",
        url: eServer + "getToKen.aspx",
        data: { username: 'eketeam', password: '123456' },
        dataType: "json",
        success: function (data, textStatus, request)
		{
			//alert(data);
			//return;
			if(data.errcode==0)
			{
				 token = data.token;
				$("#txttoken").val(token);
			}
			else
			{
				alert(data.message);
			}          
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // 状态码
            //alert(XMLHttpRequest.status);
            // 状态
            //alert(XMLHttpRequest.readyState);
            // 错误信息   
            //alert(textStatus);
            alert("error");
        }
    });
};
function insert()
{
	$.ajax({
		type: "POST",
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5",
		//contentType: "application/x-www-form-urlencoded", //post默认，可以不要
		headers: {"auth": token},
		data:{"act":"save","id":"","XM":"姓名","URL":"http://www.eketeam.com/"},
		dataType: "json",
		success: function (data,textStatus,request)
		{			
			//alert(request.responseText);
			if(data.errcode==0)
			{
				alert(data.id);
			}
			else
			{
				alert(data.message);
			}
        }
    });
};
function update()
{
	$.ajax({
		type: "POST",
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5",
		headers: {"auth": token},
		data:{"act":"save","id":"1b4ec37b-03b4-41cf-887a-66588b456a80","M0c_F3":"姓名1","M0c_F4":"http://www.eketeam.com/"},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
};
function del()
{
	$.ajax({
		type: "POST",
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5",
		headers: {"auth": token},
		data:{"act":"del","id":"1b4ec37b-03b4-41cf-887a-66588b456a80"},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
};
function view()
{
	$.ajax({
		type: "GET",
        
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5",
		//url: "Model.aspx?modelid=69603eab-f54c-45dd-9ff1-1e37b6d1865e&act=edit&id=73",
		data:{"act":"view","id":"64f21c0c-048e-4bb0-ab55-5f14b6d7fbbb"},
		headers: {"auth": token},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
	/*
	$.ajax({
		type: "GET",
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5&act=view&id=914fbd45-2b05-44aa-81ed-b198572198f5",
		// url: "Model.aspx?modelid=69603eab-f54c-45dd-9ff1-1e37b6d1865e&act=view&id=73",
		headers: {"auth": token},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
	*/
};
function read()
{
	$.ajax({
		type: "GET",
        
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5",
		//url: "Model.aspx?modelid=69603eab-f54c-45dd-9ff1-1e37b6d1865e&act=edit&id=73",
		data:{"act":"edit","id":"64f21c0c-048e-4bb0-ab55-5f14b6d7fbbb"},
		headers: {"auth": token},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
	/*
	$.ajax({
		type: "GET",
        
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5&act=edit&id=914fbd45-2b05-44aa-81ed-b198572198f5",
		//url: "Model.aspx?modelid=69603eab-f54c-45dd-9ff1-1e37b6d1865e&act=edit&id=73",
		headers: {"auth": token},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
	*/
};
function list()
{
	$.ajax({
		type: "GET",
		url: eServer + "Model.aspx?modelid=0c617e48-c19a-416b-a3db-793a5339cee5",
		headers: {"auth": token},
		dataType: "html",
		success: function (data,textStatus,request)
		{
			alert(data);
        }
    });
};
var DataID = "";
var eModel=function(modelid)
{
	this.modelid=modelid;
	this.insert=function(data)
	{
		var result=false;
		data["act"]="save";
		data["id"]="";
		$.ajax({
			type: "POST",
			url: eServer + "Model.aspx?modelid=" + modelid,
			headers: {"auth": token},
			data:data,
			dataType: "json",
			async:false,
			success: function (data,textStatus,request)
			{
				if(data.errcode==0)
				{
				    DataID=data.id;
					result = true;
				}
				else
				{
					result = false;
				}
			}
		});
		return result;
	};
	this.update=function(id,data)
	{
		var result=false;
		data["act"]="save";
		data["id"]=id;
		$.ajax({
			type: "POST",
			url: eServer + "Model.aspx?modelid=" + modelid,
			headers: {"auth": token},
			data:data,
			dataType: "json",
			async:false,
			success: function (data,textStatus,request)
			{
				if(data.errcode==0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
		});
		return result;
	};
	this.delete=function(id)
	{
		var result=false;
		var data={"act":"del","id":id};
		$.ajax({
			type: "POST",
			url: eServer + "Model.aspx?modelid=" + modelid,
			headers: {"auth": token},
			data:data,
			dataType: "json",
			async:false,
			success: function (data,textStatus,request)
			{
				if(data.errcode==0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
		});
		return result;
	};	
	this.view=function(id)
	{
		var result = {};
		$.ajax({
			type: "GET",
			url: eServer + "Model.aspx?modelid=" + modelid + "&act=view&id=" + id,
			headers: {"auth": token},
			dataType: "json",
			async:false,
			success: function (data,textStatus,request)
			{
				//alert(JSON.stringify(data));
				if(data.errcode==0)
				{
					result = data;
				}
				else
				{
					result = {};
				}
			}
		});
		return result;
	};
	this.read=function(id)
	{
		var result = {};
		$.ajax({
			type: "GET",
			url: eServer + "Model.aspx?modelid=" + modelid + "&act=edit&id=" + id,
			headers: {"auth": token},
			dataType: "json",
			async:false,
			success: function (data,textStatus,request)
			{
				//alert(JSON.stringify(data));
				if(data.errcode==0)
				{
					result = data;
				}
				else
				{
					result = {};
				}
			}
		});
		return result;
	};
	this.list=function()
	{
		var result = {};
		$.ajax({
			type: "GET",
			url: eServer + "Model.aspx?modelid=" + modelid,
			headers: {"auth": token},
			dataType: "json",
			async:false,
			success: function (data,textStatus,request)
			{
				if(data.errcode==0)
				{
					result = data;
				}
				else
				{
					result = {};
				}
			}
		});
		return result;
	};
	return this;
};
var model=new eModel("0c617e48-c19a-416b-a3db-793a5339cee5");
function model_insert()
{
	var data={"XM":"姓名eModel","URL":"http://www.eketeam.com/"};	
	alert(model.insert(data));	
};
function model_update()
{
	var data={"XM":"姓名eModel3","URL":"http://www.eketeam.com/"};	
	alert(model.update(DataID, data));
};
function model_delete()
{
    alert(model.delete(DataID));
};
function model_view()
{
    var data = model.view(DataID);
	alert(JSON.stringify(data));	
};
function model_read()
{
    var data = model.read(DataID);
	alert(JSON.stringify(data));	
};
function model_list()
{
	var data=model.list();	
	alert(JSON.stringify(data));	
};

</script>
<style>
button{width:160px;height:28px;disp3lay:block;margin-top:10px;}
</style>
<body>
<div style="padding:50px;">
<textarea id="txttoken" style="width:800px;height:130px;padding:6px;"><%=tokenstr%></textarea><br>
    <button onClick="gettoken();">获取令牌</button>
	<button onClick="insert();">WebAPI-保存添加</button>
	<button onClick="update();">WebAPI-保存修改</button>
	<button onClick="del();">WebAPI- 删除</button>
	<button onClick="view();">WebAPI- 查看Json</button>
	<button onClick="read();">WebAPI- 编辑Json</button>
	<button onClick="list();">WebAPI- 列表Json</button>
	<hr>
	<button onClick="model_insert();">Model添加</button>
	<button onClick="model_update();">Model修改</button>
	<button onClick="model_delete();">Model删除</button>
	<button onClick="model_view();">Model查看</button>
	<button onClick="model_read();">Model读取</button>
	<button onClick="model_list();">Model列表</button>
</div>
</body>
</html>
