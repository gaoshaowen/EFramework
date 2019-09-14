<%@ Page Language="C#" MasterPageFile="~/Examples/Main.Master" AutoEventWireup="true" CodeBehind="eJson.aspx.cs" Inherits="eFrameWork.Examples._eJson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="nav">您当前位置：首页 -> Json处理</div>
<div style="margin:8px;line-height:22px;">
<h1>一、Asp.Net</h1>
    <asp:Literal id="litSingle1" runat="server" />
<h1>二、Javascript</h1>
<script>
    function Example1()
    {
        var json = new eJson();
        json.Convert = true;
        json.append("Name", "eFrameWork");
        json.append("Version", "V1.0");
        json["TEAM"] = "EKETEAM";
        alert(json.tostring());
        alert(json["Name"] + " or " + json.Name);

        json.foreach(function (e)
        {
            alert(e + " = " + json[e]);
        });


    };
    function Example2()
    {
        var str = '{"Name":"eFrameWork","Version":"V1.0"}';
        var json = str.toJson();

        alert(str);
    };
    function Example3()
    {
        var item1 = new eJson();
        item1.append("Pos", "100");
        item1.append("Width", "200");

        var item2 = new eJson();
        item2.append("Pos", "150");
        item2.append("Width", "300");

        var json = new eJson();
        json.append(item1);
        alert(json.tostring());
        json.append( item2);

        alert(json.tostring());
    };
    function Example4()
    {
        var json = new eJson();
        json.Convert = true;
        json.append("Name", "eFrameWork");
        json.append("Version", "V1.0");
        json["TEAM"] = "EKETEAM";
        // alert(json.tostring());
        //alert(json["Name"] + " or " + json.Name);

        json.foreach(function (e) {
            //alert(e + " = " + json[e]);
        });

        var item = new eJson();
        item.append("Ext", "JPG");
        item.append("Size", "200K");
        json.append("FileInfo", item);

        var item1 = new eJson();
        item1.append("Pos", "100");
        item1.append("Width", "200");

        var item2 = new eJson();
        item2.append("Pos", "150");
        item2.append("Width", "300");

        json.append("Items", item1);
        json.append("Items", item2);

        alert(json.tostring());
    };
</script>
    <a class="button" href="javascript:;" onclick="Example1();" style="margin:10px;"><span><i>例1</i></span></a>
    <a class="button" href="javascript:;" onclick="Example2();" style="margin:10px;"><span><i>例2</i></span></a>
    <a class="button" href="javascript:;" onclick="Example3();" style="margin:10px;"><span><i>例3</i></span></a>
    <a class="button" href="javascript:;" onclick="Example4();" style="margin:10px;"><span><i>例4</i></span></a>
</div>
</asp:Content>