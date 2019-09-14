<%@ Page Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eDataTable.aspx.cs" Inherits="eFrameWork.Examples._eDataTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> eDataTable表格插件</div>
<div style="margin:8px;">
<h1>1.移动行、拖行高（鼠标在行上按下后拖动移动行，内容行的序号列底边框线改变行高）</h1>
<table width="300" class="eDataTable" id="eDataTable1" cellpadding="0" cellspacing="0">
<thead>
  <tr>
    <th width="50">序号</th>
    <th width="150">产品名称</th>
    <th width="100">单价(元)</th>
	</tr>
  </thead>
  <tbody eSize="true" eMove="true">
   <tr>
    <td height="40">1</td>
    <td>笔记本</td>
    <td>10.00</td>
    </tr>
  <tr>
    <td height="40">2</td>
    <td>钢笔</td>
    <td>12.00</td>
    </tr>
   <tr>
    <td height="40">3</td>
    <td>尺子</td>
    <td>6.00</td>
    </tr>
   <tr>
    <td height="40">4</td>
    <td>订书机</td>
    <td>8.00</td>
    </tr>
  </tbody>
</table>
<script>
 var eDataTable1 = new eDataTable("eDataTable1");
 eDataTable1.moveRow = function (index, new_index)
 {
     //重新设置编号
     var trs = eDataTable1.tbody.getElementsByTagName("tr");
     for (var i = 0; i < trs.length; i++)
     {
         var td = trs[i].getElementsByTagName("td")[0];
         var p = td.getElementsByTagName("p");
         if (p)
         {
             p[0].innerHTML = (i + 1);
         }
     }
 };
 eDataTable1.setHeight = function (td, height)
 {
     alert("新行高为：" + height);
 };
</script>
<h1>2.列宽(总宽度增加，列分隔处按下鼠标拖动)</h1>
<table width="300" class="eDataTable" id="eDataTable2" cellpadding="0" cellspacing="0">
<thead>
  <tr>
    <th width="50">序号</th>
    <th width="150" eSize="true">产品名称</th>
    <th width="100" eSize="true">单价(元)</th>
	</tr>
  </thead>
  <tbody>
   <tr>
    <td height="40">1</td>
    <td>笔记本</td>
    <td>10.00</td>
    </tr>
  <tr>
    <td height="40">2</td>
    <td>钢笔</td>
    <td>12.00</td>
    </tr>
  </tbody>
</table>
<script>
var eDataTable2 = new eDataTable("eDataTable2"); //或 new eDataTable("eDataTable2",2);
eDataTable2.setWidth=function(td,widtharr)
{
	var arr=widtharr.split("_");
	alert("列ID：" + arr[0] + "，宽度变为：" + arr[1]);	
};
</script>

<h1>3.列宽(总宽度不变,在相临列间改变宽度)</h1>
<table width="400" class="eDataTable" id="eDataTable3" cellpadding="0" cellspacing="0">
<thead>
  <tr>
    <th width="50">序号</th>
    <th width="150" eCellID="71" eSize="true">产品名称</th>
    <th width="200" eCellID="72" eSize="true">单价(元)</th>
	</tr>
  </thead>
  <tbody>
   <tr>
    <td height="40">1</td>
    <td>尺子</td>
    <td>6.00</td>
    </tr>
   <tr>
    <td height="40">2</td>
    <td>订书机</td>
    <td>8.00</td>
    </tr>
  </tbody>
</table>
<script>
var eDataTable3 = new eDataTable("eDataTable3",1);
eDataTable3.setWidth=function(td,widtharr)
{
	var str=widtharr.split("|");
	for(var i=0;i<str.length;i++)
	{
		var arr=str[i].split("_");
		alert("列ID：" + arr[0] + "，宽度变为：" + arr[1]);	
	}
};
</script>
<h1>4.列移动(鼠标按标题后拖动)</h1>
<table width="300" class="eDataTable" id="eDataTable4" cellpadding="0" cellspacing="0">
<thead>
  <tr>
    <th width="50" eCellID="70" eMove="true">序号</th>
    <th width="150" eCellID="71" eMove="true">产品名称</th>
    <th width="100" eCellID="72" eMove="true">单价(元)</th>
	</tr>
  </thead>
  <tbody>
   <tr>
    <td height="40">1</td>
    <td>笔记本</td>
    <td>10.00</td>
    </tr>
  <tr>
    <td height="40">2</td>
    <td>钢笔</td>
    <td>12.00</td>
    </tr>
  </tbody>
</table>
<script>
var eDataTable4 = new eDataTable("eDataTable4");
eDataTable4.moveColumn=function(index,new_index)
{
	alert("列号为" + index + "，移动到" + new_index);
};
</script>
<h1>5.开启排序(点击标题进行排序)</h1>
<table width="300" class="eDataTable" id="eDataTable5" cellpadding="0" cellspacing="0">
<thead>
  <tr>
    <th width="50">序号</th>
    <th width="150" eCellID="71" eOrderByName="o1" eOrderByValue="<%=(Request.QueryString["o1"]==null ? "0" : Request.QueryString["o1"].ToString())%>">产品名称</th>
    <th width="100" eCellID="72" eOrderByName="o2" eOrderByValue="<%=(Request.QueryString["o2"]==null ? "0" : Request.QueryString["o2"].ToString())%>">单价(元)</th>
	</tr>
  </thead>
  <tbody>
   <tr>
    <td height="40">1</td>
    <td>笔记本</td>
    <td>10.00</td>
    </tr>
  <tr>
    <td height="40">2</td>
    <td>钢笔</td>
    <td>12.00</td>
    </tr>
  </tbody>
</table>
<script>
var eDataTable5 = new eDataTable("eDataTable5");
</script>
</div>
</asp:Content>