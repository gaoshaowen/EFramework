<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="eFrameWork.Mobile.Menu" %>
<div id="menu" style="display:none;"><asp:Literal ID="LitMenu" runat="server" /></div>
<script type="text/javascript">
    var _menu;
    $(document).ready(function () {
        _menu = $('#menu').menu();
        _menu.hide();
        $(document.body).on('touchstart', function (event) {
            var src = event.srcElement || event.target;
            if (src.tagName == "DIV") return;
            if (!_menu.is(':hidden') && src.tagName!="A") {
                _menu.hide();
            }
        });
        //$(document).mousedown(function () {  alert(3); });
    });
    function _showmenu()
    {
        _menu.toggle();
        /*
        if (_menu.is(':hidden')) {
            _menu.show();
        }
        else {
            _menu.hide();
        }
        */
    };
</script>