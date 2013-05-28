<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TreeTest
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">

    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mybutton").click(function () {
                $("#mydiv").html('<ul id="mytree"></ul>');
                bindtree();
            });
        });

        function bindtree() {
            var dataJson = [{ text: '部门1', children: [{ text: '部门1-1'}] }, { text: '部门2'}];
            $("#mytree").ligerTree({
                data: dataJson,
                checkbox: false
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>TreeTest</h2>
<div id="mybutton" class="btn btn-primary">点击</div>
<div id="mydiv">
    
</div>
</asp:Content>
