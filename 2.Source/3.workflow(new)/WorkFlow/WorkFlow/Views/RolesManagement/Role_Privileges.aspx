﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Role_Privileges
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <script type="text/javascript">
        var roleID;
        $(document).ready(function () {
            roleID = $("#roleID").val(); //角色ID
        });

        var mpTotal = 0;//菜单权限数量
        var opTotal = 0; //操作权限数量
        var epTotal = 0;//页面元素权限数量

    </script>
    <%--菜单权限初始化--%>
    <script type="text/javascript">
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

            var role_privilegesStr = "{";

            $("#saveSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    //var rpTotal = mpTotal + opTotal + epTotal;
                    //alert(rpTotal);

                    for (var i = 0; i < mpTotal; i++) {
                        if ($("#menusprivilege" + i.toString()).attr("checked") == "true") {
                            alert($("#menusprivilege" + i.toString()).val());
                        } else {
                            alert($("#menusprivilege" + i.toString()).val());
                        }
                    }


                    role_privilegesStr += "rp_total: '3' }";
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
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>角色权限管理</h2></div>
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

