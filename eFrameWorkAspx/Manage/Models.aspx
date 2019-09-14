<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="Models.aspx.cs" Inherits="eFrameWork.Manage.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script>
    var act = "<%=Action.Value%>";
window.onload=function()
{
    if (act == "add" || act == "edit") { selType(); }
};
function selectTable(value)
{
        var f2 = getobj("f2");
        if (value.length == 0) {
            f2.value = "";            
            f2.removeAttribute("readOnly");
            //f2.style.display = "";
        }
        else {
            f2.value = value;
            f2.setAttribute("readOnly", 'true');
            //f2.style.display = "none";
        }
};
function selType()
{
    //var md = document.createElement("input");// getobj("f9_1");//模块
    //md.type = "radio";
    //md.checked = true;
	var md = true;// getobj("f9_3");//菜单
    var at = getobj("f5_2");//自定义
    getobj("f2").setAttribute("notnull",(md.checked || at.checked ? "false" : "true"));
    if (md) //模块 !md.checked
    {
        getobj("tr1").style.display = "";//属性
        if (!at.checked) //自定义
        {
            getobj("tr2").style.display = "none";//文件
            getobj("tr3").style.display = "none";//类名
            getobj("tr4").style.display = "";//编码
			getobj("tr5").style.display = "";//上级
			var f7=getobj("f7");
			if(f7.value.length==0)			
			{
				getobj("tr6").style.display = "none";//关系
			}
			else
			{
            	getobj("tr6").style.display = "";//关系
			}
        }
        else //自定义
        {
            getobj("tr2").style.display = "";//文件
            getobj("tr3").style.display = "none";//类名
            getobj("tr4").style.display = "none";//编码
			getobj("tr5").style.display = "none";//上级
            getobj("tr6").style.display = "none";//关系
        }
    }
    else //菜单
    {
        getobj("tr1").style.display = "none";//属性
        getobj("tr2").style.display = "none";//文件
        getobj("tr3").style.display = "none";//类名
        getobj("tr4").style.display = "none";//编码
		getobj("tr5").style.display = "none";//上级
        getobj("tr6").style.display = "none";//关系
    }
};
</script>
<div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 模块管理<a id="btn_add" style="float:right;margin-top:4px;<%=( Action.Value == "" ? "" : "display:none;" )%>" class="button" href="<%=edt.getAddURL()%>"><span><i class="add">添加</i></span></a></div>
<%
if(Action.Value=="edit" || Action.Value=="add")
{
%>

<div style="margin:10px;">
	<form name="frmaddoredit" id="frmaddoredit" method="post" action="<%=edt.getSaveURL()%>">
	<input name="id" type="hidden" id="id" value="<%=edt.ID%>">
    <input name="act" type="hidden" id="act" value="save">
    <input type="hidden" name="f9aa" value="1" />
	<input type="hidden" id="fromurl" name="fromurl" value="<%=edt.FromURL%>">
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="eDataView">
<colgroup>
<col width="126" />
<col />
</colgroup>
      <tr>
        <td class="title"><ins>*</ins>名称：</td>
        <td class="content"><span class="eform"><ev:eFormControl ID="f1" FieldName="名称" field="MC" notnull="true" Attributes="class=&quot;text&quot; style=&quot;width:180px;&quot;" runat="server" /></span></td>
      </tr>
<%if(1==1){ %>
     <tr>
	    <td class="title">类型：</td>
	    <td class="content"><span class="eform"><ev:eFormControl ID="f9" controltype="radio" field="Type" Attributes="onclick=&quot;selType();&quot;" Options="[{text:模块,value:1},{text:数据模块,value:3}]" defaultvalue="1" runat="server" /></span></td>
	    </tr>
<%} %>
	  <tr id="tr1" style="<%=(edt.Fields["Type"].ToString()=="1" ? "" : "display:none;") %>">
	    <td class="title">属性：</td>
	    <td class="content"><span class="eform"><ev:eFormControl ID="f5" controltype="radio" field="Auto" Attributes="onclick=&quot;selType();&quot;" Options="[{text:配置,value:True},{text:自定义,value:False}]" defaultvalue="True" runat="server" /></span></td>
	    </tr>
	  <tr id="tr2" style="<%=(edt.Fields["Type"].ToString()=="1" && edt.Fields["Auto"].ToString()=="False" ? "" : "display:none;") %>">
	    <td class="title">自定义文件名：</td>
	    <td class="content"><span class="eform"><ev:eFormControl ID="f4" field="AspxFile" defaultvalue="" Attributes="class=&quot;text&quot; style=&quot;width:180px;&quot;" runat="server" /></span> 菜单连接到自定义模块的程序文件名 如：test.aspx</td>
	  </tr>
	   <tr id="tr3" style="<%=(edt.Fields["Type"].ToString()=="1" && edt.Fields["Auto"].ToString()=="True" ? "display:none;" : "display:none;") %>">
	    <td class="title">生成类名：</td>
	    <td class="content"><span class="eform"><ev:eFormControl ID="f6" field="ClassName" defaultvalue="" Attributes="class=&quot;text&quot; style=&quot;width:180px;&quot;" runat="server" /></span> 如：/System/CProducts</td>
	  </tr>
	  <tr id="tr4" style="<%=(edt.Fields["Type"].ToString()=="1" && edt.Fields["Auto"].ToString()=="True" ? "" : "display:none;") %>">
	     <td class="title">数据表：</td>
	     <td class="content"><select name="formtable" onchange="selectTable(this.value);">
	       <option value="">新建</option>
		     <optgroup label="数据表">
		   <asp:Literal id="LitTable" runat="server" /></optgroup>
	       </select>
	     <span class="eform"><input type="text" class="text" name="f2" id="f2" value="<%=f2.Value%>"  fieldname="编码" notnull="<%=((edt.Fields["Type"].ToString()=="1" || edt.Fields["Type"].ToString()=="3") && edt.Fields["Auto"].ToString()=="True" ? "true" : "false") %>" autocomplete="off"></span></td>
	     </tr>
		 <tr id="tr5" style="<%=(edt.Fields["Type"].ToString()=="1" && edt.Fields["Auto"].ToString()=="True" ? "" : "display:none;") %>">
	     <td class="title">上级：</td>
	     <td class="content">
		 <span class="eform">
		 <select id="f7" name="f7">
		  <asp:Literal id="LitParent" runat="server" />
		 </select>
		 </span>
		 <!-- <ev:eFormControl ID="f7" controltype="select" field="ParentID" FieldType="uniqueidentifier" Attributes="onchange=&quot;selType();&quot;" BindObject="a_eke_sysModels" BindValue="ModelID" BindText="MC" BindCondition="delTag=0 and Type=1" BindOrderBy="ModelID" DefaultValue="0" runat="server" /> -->
		 </td>
	     </tr>
 		<tr id="tr6" style="<%=(edt.Fields["Type"].ToString()=="1" && edt.Fields["Auto"].ToString()=="True" && edt.Fields["ParentID"].ToString().Length > 0 ? "" : "display:none;") %>">
              <td class="title">与上级关系：</td>             
              <td class="content"><span class="eform"><ev:eFormControl ID="f8" controltype="radio" field="JoinMore" Options="[{text:一对多,value:True},{text:一对一,value:False}]" defaultvalue="True" runat="server" /></span></td>
       </tr>
	   <tr>
	     <td class="title">说明：</td>
	     <td class="content"><span class="eform"><ev:eFormControl ID="f3" controltype="textarea" FieldName="说明" field="SM" notnull="false" runat="server" /></span></td>
	     </tr>
 		<tr>
		
		<td colspan="2" class="title"  style="text-align:left;padding-left:100px;padding-top:10px;padding-bottom:10px;">		
		<a class="button" href="javascript:;" onclick="if(frmaddoredit.onsubmit()!=false){frmaddoredit.submit();}"><span><i class="save">保存</i></span></a>
		<a class="button" href="javascript:;" style="margin-left:30px;" onclick="history.back();"><span><i class="back">返回</i></span></a>
		</td>
	   </tr>	 
    </table>
    </form>
</div>
<%}%>

<div style="margin:6px;">
<%if(Action.Value==""){%><a id="btn_add" style="" class="button" href="ModelImport.aspx"><span><i>导入</i></span></a><%}%>
<asp:Repeater id="Rep" runat="server">
<headertemplate>
<%#
"<table id=\"eDataTable\" class=\"eDataTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\">\r\n"+
"<thead>\r\n"+
  "<tr bgcolor=\"#f2f2f2\">\r\n"+
  	"<td width=\"300\">编号</td>\r\n"+
	"<td>模块名称</td>\r\n"+
//    "<td>类型</td>\r\n"+
	"<td>属性</td>\r\n"+
	"<td>说明</td>\r\n"+
	"<td width=\"150\">添加时间</td>\r\n"+
	"<td width=\"150\">操作</td>\r\n"+
  "</tr>\r\n"+
"</thead>\r\n" %></headertemplate><itemtemplate><%# "<tr" + ((Container.ItemIndex+1) % 2 == 0 ? " class=\"alternating\" eclass=\"alternating\"" : " eclass=\"\"") + ">\r\n"+
    "<td height=\"32\">"+ Eval("ModelID") + "</td>\r\n"+
	"<td>" + (Eval("Type").ToString() == "1" ? "<a href=\"ModelItems.aspx?ModelID=" + Eval("ModelID") + "\">"+ Eval("MC") + "</a>" : Eval("MC"))  + "</td>\r\n"+
//    "<td>"+ Eval("Type").ToString().Replace("1","模块").Replace("2","菜单") + "</td>\r\n"+
	"<td>"+ Eval("Auto").ToString().Replace("True","配置").Replace("False","自定义") + "</td>\r\n"+
	"<td>"+ Eval("SM") + "</td>\r\n"+
	"<td>"+ Eval("addTime","{0:yyyy-MM-dd HH:mm:ss}") + "</td>\r\n"+
	"<td>"+
	"<a href=\""+ edt.getActionURL("copy",Eval("ModelID").ToString())  +"\" onclick=\"javascript:return confirm('确认要复制吗？');\">复制</a>"+
	"<a href=\"" + edt.getActionURL("edit",Eval("ModelID").ToString())  + "\">修改</a>"+
	"<a href=\""+ edt.getActionURL("del",Eval("ModelID").ToString()) +"\" onclick=\"javascript:return confirm('确认要删除吗？');\">删除</a>"+
        (Eval("Auto").ToString() == "True" ? "<a href=\"ModelExport.aspx?ModelID=" + Eval("ModelID").ToString()  + "\" target=\"_blank\">导出</a>" : "") +
	"</td>\r\n"+
"</tr>\r\n"%></itemtemplate>
<footertemplate><%#"</table>\r\n"%></footertemplate>
</asp:Repeater>
</div>
<div style="margin:6px;"><ev:ePageControl ID="ePageControl1" PageSize="20" PageNum="9" runat="server" /></div>

</asp:Content>