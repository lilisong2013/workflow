<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    权限管理系统/角色权限
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "角色权限";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

    <script type="text/javascript">
        var roleID;
        $(document).ready(function () {
            roleID = $("#roleID").val(); //角色ID
        });

        var mpTotal = 0;//菜单权限数量
        var opTotal = 0; //操作权限数量
        var epTotal = 0;//页面元素权限数量

    </script>
    <script src="../../LigerUI/lib/json2.js" type="text/javascript"></script>
    <%--菜单权限初始化--%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                url: "/RolesManagement/GetMunusPrivilegeList",
                type: "POST",
                dataType: "json",
                data: { roleid: roleID },
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")");
                    mpTotal = parseInt(dataJson.total);//菜单权限数量
                    for (var i = 0; i < dataJson.total; i++) {
                        $("#menusList").append("<label class='checkbox span2'><input id='menusprivilege" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                    }

                    //alert($("#menusprivilege1").val());
                    for (var i = 0; i < dataJson.total; i++) {
                        if (dataJson.List[i].selected == "true") {
                            $("#menusprivilege" + i.toString()).prop("checked", true);
                        } else {
                            $("#menusprivilege" + i.toString()).prop("checked", false);
                        }
                    }

                }
            });
        });
    </script>--%>
    <script type="text/javascript">
        var managerMenuTree;
        //var dataJson=[{name:'个人信息',id:'1',children:[{name:'信息编辑',id:'2'},{name:'密码修改',id:'3'}]},{name:'数据分析',id:'4'}];
        $(document).ready(function () {
            managerMenuTree = $("#menusList").ligerTree({
                checkbox: true,
                autoCheckboxEven: true,
                textFieldName: 'name',
                idFieldName: 'menuID',
                parentIDFieldName: 'parentID'
            });

            //给menusList填充数据
            GetMenusList();

            $("#myButton").click(function () {
                getChecked();
            });
        });
        
        function GetMenusList() {
            $.ajax({
                url: "/RolesManagement/GetMenuPrivilegeTree",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    alert(responseText);
                    var dataMenusJson = eval(responseText);
                    managerMenuTree.clear();
                    managerMenuTree.setData(dataMenusJson);
                    managerMenuTree.loadData();
                }
            });
        }

        function getChecked() {
            var notes = managerMenuTree.getChecked();
            var text = "";
            for (var i = 0; i < notes.length; i++) {
                alert(JSON2.stringify(managerMenuTree.getData()));
                text += notes[i].data.id + ",";
            }
            alert('选择的节点数：' + text);
        }

    </script>

    <%--操作权限初始化--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                url: "/RolesManagement/GetOperationsPrivilegeList",
                type: "POST",
                dataType: "json",
                data: { roleid: roleID },
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")");
                    opTotal = parseInt(dataJson.total); //操作权限数量
                    for (var i = 0; i < dataJson.total; i++) {
                        $("#operationsList").append("<label class='checkbox span2'><input id='operationsprivilege" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                    }

                    for (var i = 0; i < dataJson.total; i++) {
                        if (dataJson.List[i].selected == "true") {
                            $("#operationsprivilege" + i.toString()).prop("checked", true);
                        } else {
                            $("#operationsprivilege" + i.toString()).prop("checked", false);
                        }
                    }
                }
            });
        });
    </script>

    <%--页面元素权限初始化--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                url: "/RolesManagement/GetElementsPrivilegeList",
                type: "POST",
                dataType: "json",
                data: { roleid: roleID },
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")");
                    epTotal = parseInt(dataJson.total); //页面元素权限数量
                    for (var i = 0; i < dataJson.total; i++) {
                        $("#elementsList").append("<label class='checkbox span2'><input id='elementsprivilege" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                    }

                    for (var i = 0; i < dataJson.total; i++) {
                        if (dataJson.List[i].selected == "true") {
                            $("#elementsprivilege" + i.toString()).prop("checked", true);
                        } else {
                            $("#elementsprivilege" + i.toString()).prop("checked", false);
                        }
                    }
                }
            });
        });
    </script>


    <script type="text/javascript">

        $(document).ready(function () {
            var role_privilegesData;

            var role_privilegesStr;

            $("#saveSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    role_privilegesStr = "{"; //JSON数据字符串
                    var rpTotal = 0; //角色权限的数量
                    //alert(rpTotal);

                    var notes = managerMenuTree.getChecked(); //菜单树中被选中的菜单
                    //alert(notes.length);
                    //菜单权限中被选中的项
                    for (var i = 0; i < notes.length; i++) {
                        //alert(notes[i].data.id);
                        role_privilegesStr += "rprivilegeID" + rpTotal.toString() + ":'" + notes[i].data.id + "',";
                        rpTotal++;
                    }

                    //alert(role_privilegesStr);
                    //操作权限中被选中的项
                    for (var i = 0; i < mpTotal; i++) {
                        var checkBoxID = $("#operationsprivilege" + i.toString()); //复选框ID
                        if (checkBoxID.is(":checked")) {
                            role_privilegesStr += "rprivilegeID" + rpTotal.toString() + ":'" + checkBoxID.val() + "',";
                            rpTotal++;
                        } else {

                        }
                    }
                    //页面元素权限中被选中的项
                    for (var i = 0; i < mpTotal; i++) {
                        var checkBoxID = $("#elementsprivilege" + i.toString()); //复选框ID
                        if (checkBoxID.is(":checked")) {
                            role_privilegesStr += "rprivilegeID" + rpTotal.toString() + ":'" + checkBoxID.val() + "',";
                            rpTotal++;
                        } else {

                        }
                    }
                    role_privilegesStr += "rp_total: '" + rpTotal + "',r_ID:'" + $("#roleID").val() + "' }";
                    alert(role_privilegesStr);
                    role_privilegesData = eval("(" + role_privilegesStr + ")");
                    $("#role_privileges").ajaxForm({
                        success: rp_showResponse,  // form提交响应成功后执行的回调函数
                        url: "/RolesManagement/AddRolePrivileges",
                        type: "POST",
                        dataType: "json",
                        data: role_privilegesData
                    });
                }
            });


            //提交role_privileges表单后执行的函数
            function rp_showResponse(responseText, statusText) {

                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
                if (result.success) {
                    location.href = result.toUrl;
                }
            }
        });
    </script>

    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>角色权限管理</h2></div>

    <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/RolesManagement/AppRoles">返回</a></li></ul>
    </div>

    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container">
        <form id="role_privileges" method="post" action="" class="form-horizontal">
            <div class="control-group page-header">
                <label class="control-label">角色名称：</label>
                <div class="controls">
                    <span class="input-xlarge uneditable-input"><%=ViewData["r_Name"]%></span>
                    <input id="roleID" name="roleID" type="hidden" value="<%=ViewData["r_ID"] %>" />
                </div>
            </div>

            <div class="control-group">
                <div class="page-header">
                    <h3>菜单</h3>
                </div>
                <div id="menusList">

                </div>
                <div class="btn btn-primary" id="myButton">
                节点
                </div>
            </div>

            <div class="control-group">
                <div class="page-header">
                    <h3>操作</h3>
                </div>
                <div id="operationsList">
                    
                </div>
            </div>

            <div class="control-group">
                <div class="page-header">
                    <h3>页面元素</h3>
                </div>
                <div id="elementsList">
                
                </div>
            </div>
            <div class="control-group">
                <input id="saveSubmit" type="submit" class="btn btn-primary btn-large span3"  value="保存" />
            </div>
        </form>
    </div>

</asp:Content>


