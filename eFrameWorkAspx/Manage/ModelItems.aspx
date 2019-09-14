<%@ Page Title="" Language="C#" MasterPageFile="~/Master/manageMain.Master" AutoEventWireup="true" CodeBehind="ModelItems.aspx.cs" Inherits="eFrameWork.Manage.ModelItems" %>
<%@ Import Namespace="EKETEAM.FrameWork" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nav">您当前位置：<a href="Default.aspx">首页</a> -> 模块管理</div>
<script>
    var ModelID = "<%=ModelID%>";
</script>
<script src="javascript/columns.js?beacon=7"></script>
<style>
    input.edit {
        border:1px solid #ccc;font-size:12px;height:23px;line-height:23px;padding-left:5px;
    }
    textarea {
          border:1px solid #ccc;
          font-size:12px;line-height:20px;
          padding-left:5px;
    }
   .text{display:inline-block;width:100%;border:1px solid #ccc;font-size:12px;height:23px;line-height:23px;}

#eDataTable .edit{padding-left:3px;background-color:transparent;border:0px solid #ccc;height:26px;width:100%;font-size:12px;padding:0px; over4flow:hidden; background-color:#f2f2f2;}
.divloading{filter:alpha(opacity=50);-moz-opacity:0.5;-khtml-opacity: 0.5; opacity: 0.5; position:fixed;width:100%;height:100%;top:0px;left:0px;background:#cccccc url(images/loading.gif) no-repeat center center;}
</style>

<div id="divloading" class="divloading" style="display:none;">&nbsp;</div>
<div style="margin:6px;margin-top:0px;border:0px solid #ff0000;">
<div><asp:Literal ID="LitMenu" runat="server" /></div>
<dl class="eTab">
<dt>
<a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();" class="cur">数据结构</a>
<a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">基本设置</a>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">客户端验证</a><%}%>
<a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">列表</a>
<a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">搜索</a>
<a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">数据</a>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">动作</a><%}%>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">JS</a><%}%>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">布局</a><%}%>
<%if(modelType=="33"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">生成代码</a><%}%>
<%if(modelType=="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">对应关系</a><%}%>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">导出</a><%}%>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">打印</a><%}%>
<%if(modelType!="3"){%><a href="javascript:;" onclick="selecttab(this);" onfocus="this.blur();">WebAPI</a><%}%>
</dt>
<dd>
<div style="height:100%;"  dataurl="ModelItems_Columns.aspx?modelid=<%=ModelID%>"><!--数据结构--></div>
<div style="display:none;" dataurl="ModelItems_Basic.aspx?modelid=<%=ModelID%>"><!--基本设置--></div>
<%if(modelType!="3"){%><div style="display:none;" dataurl="ModelItems_Client.aspx?modelid=<%=ModelID%>"><!--客户端验证--></div><%}%>
<div style="display:none;" dataurl="ModelItems_List.aspx?modelid=<%=ModelID%>"><!--列表--></div>
<div style="display:none;" dataurl="ModelItems_Search.aspx?modelid=<%=ModelID%>"><!--搜索--></div>
<div style="display:none;" dataurl="ModelItems_Data.aspx?modelid=<%=ModelID%>"><!--数据--></div>
<%if(modelType!="3"){%><div style="display:none;" dataurl="ModelItems_Action.aspx?modelid=<%=ModelID%>"><!--动作--></div><%}%>
<%if(modelType!="3"){%><div style="display:none;" dataurl="ModelItems_JS.aspx?modelid=<%=ModelID%>"><!--JS--></div><%}%>
<%if(modelType!="3"){%><div style="display:none;" dataurl="ModelItems_Layout.aspx?modelid=<%=ModelID%>"><!--布局--></div><%}%>
<%if(modelType=="33"){%><div style="display:none;" dataurl="ModelItems_Bulid.aspx?modelid=<%=ModelID%>"><!--生成代码--></div><%}%>
<%if(modelType=="3"){%><div style="display:none;" dataurl="ModelItems_FillData.aspx?modelid=<%=ModelID%>"><!--对应关系--></div><%}%>
<%if(modelType!="3"){%><div style="display:none;" dataurl="ModelItems_Export.aspx?modelid=<%=ModelID%>"><!--导出--></div><%}%>


<%if(modelType!="3"){%>
<div style="display:none;" dataurl="" loaded="true">    
<!--打印-->
<%if (eBase.showHelp())
  { %>
<h1 class="tips" style="margin-bottom:6px;">打印</h1>

<%}%>
<textarea name="printHTMLStart" class="input" id="printHTMLStart" style="width:95%;height:200px;"><%=System.Web.HttpUtility.HtmlEncode(printHTMLStart)%></textarea>
<textarea name="printHTML" class="input" id="printHTML" style="display:none;"><%=System.Web.HttpUtility.HtmlEncode(printHTML)%></textarea>
<script>
    var linkArrys = [<%=linkArrys%>];
    KE.show({
        id: 'printHTML',
        width: '100%',
        height: '400',
        newlineTag: 'br',
        cssPath: 'index.css',
        items: ['source', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyfull', '|', 'title', 'fontname', 'fontsize', '|', 'bold', 'italic', 'underline', '|', 'advtable', 'ekecontrol'],

        afterCreate: function (id) {
            KE.event.ctrl(document, 13, function () {
                KE.util.setData(id);
                document.forms['example'].submit();
            });
            KE.event.ctrl(KE.g[id].iframeDoc, 13, function () {
                KE.util.setData(id);
                document.forms['example'].submit();
            });
        }
    });
</script>
<textarea name="printHTMLEnd" class="input" id="printHTMLEnd" style="width:95%;height:100px;"><%=System.Web.HttpUtility.HtmlEncode(printHTMLEnd)%></textarea><br />
<input type="button" name="Submit" onclick="savePrintHTML();" value=" 保  存 ">

</div>
<%}%>
<%if(modelType!="3"){%><div style="display:none;" dataurl="ModelItems_WebAPI.aspx?modelid=<%=ModelID%>"><!--WebAPI--></div><%}%>
</dd>
</dl>
</div>
</asp:Content>