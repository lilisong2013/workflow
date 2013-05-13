<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppMenus
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/MicrosoftAjax.debug.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.debug.js" type="text/javascript"></script>
    <script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>

    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            var form = $("#add_Menus");
            form.submit(function () {
                $.post(form.attr("action"),
                    form.serialize(),
                    function (result, status) {
                        //debugger;
                        $("#promptDIV").addClass(result.css);
                        $("#promptDIV").html(result.message);
                    },
                    "JSON");
                return false;
            });
        });
    </script>

    <style type="text/css">

    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            BindUser();
            $("#menusInfo").html("一级菜单");
        });

        function BindUser() {
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "/MenusManagement/GetMenusName",
                data: {}, //即使参数为空，也需要设置
                dataType: 'JSON', //返回的类型为XML  
                success: function (result, status) {
                    //成功后执行的方法
                    try {
                        if (status == "success") {
                            //alert(result.Total);
                            for (var i = 0; i < result.Total; i++) {
                                $("#MenusParent").append("<option value='" + result.Rows[i].menusID + "'>" + result.Rows[i].menusName + "</option>");
                            }
                        }
                    } catch (e) {

                    }
                }
            });
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var TreeDeptData = { Rows: [
                { id: '01', name: "企划部", remark: "1989-01-12",
                    children: [{ id: '0101', name: "企划分部一", remark: "企划分部一"},
                               { id: '0102', name: "企划分部二", remark: "企划分部二", 
                                   children:[{ id: '010201', name: "企划分部二 A组", remark: "企划分部二 A组" },
                                             { id: '010202', name: "企划分部二 B组", remark: "企划分部二 B组" }]},
                               { id: '0103', name: "企划分部三", remark: "企划分部三" }]},
                { id: '02', name: "研发部", remark: "研发部" },
                { id: '03', name: "产品部", remark: "产品部"}]};

                $("#AllMenus").ligerGrid({
                columns: [
                    { display: '部门名', name: 'name', width: 250, align: 'left', editor: { type: 'text'} },
                { display: '部门标示', name: 'id', width: 250, type: 'int', align: 'left' },

                { display: '部门描述', name: 'remark', width: 250, align: 'left', editor: { type: 'text'} }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '97%',
                onSelectRow: function (rowdata, rowindex) {
                    $("#txtrowindex").val(rowindex);
                },
                data: TreeDeptData, alternatingRow: false, tree: { columnName: 'name' }, checkbox: false,
                autoCheckChildren: false
            });

        });
    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>菜单管理</h2></div>
    <div class="container">
        <div></div>
    </div>
    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllMenus" data-toggle="tab"><i class="icon-check"></i>全部<%=10 %></a></li>
            <li><a href="#AddMenus" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
       
        <%--查看所有菜单--%>
        <div class="tab-pane active" id="AllMenus">
        </div>
        
        <%--添加菜单--%>
        <div class="tab-pane" id="AddMenus">
            <form id="add_Menus" class="form-horizontal" method="post" action="/MenusManagement/AddMenus">
                <div class="control-group span6 offset2">
                    <label class="control-label">菜单名称</label>
                    <div class="controls">
                        <input name="MenusName" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">菜单编码</label>
                    <div class="controls">
                        <input name="MenusCode" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">菜单URL</label>
                    <div class="controls">
                        <input name="MenusUrl" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">父菜单</label>
                    <div class="controls">
                        <select id="MenusParent" name="MenusParent" class="span4">
                            <option id="menusInfo" value="-1"></option>
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="MenusRemark" rows="4" cols="5" class="span4"></textarea>
                    </div>
                </div>
                <div class="control-group span6 offset3">
                    <input type="submit" class="btn btn-primary btn-large span4" onclick="" value="添加菜单" />
                </div>
            </form>
        </div>
    </div>

</asp:Content>


