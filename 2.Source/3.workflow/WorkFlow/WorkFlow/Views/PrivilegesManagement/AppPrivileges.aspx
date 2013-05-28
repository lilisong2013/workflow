<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppPrivileges
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    
   <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <%--权限类型--%>
    <script type="text/javascript">
        var manager = null;
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
        $(document).ready(function () {
            $.ajax({
                url: "/PrivilegesManagement/GetPrivilegesType",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //成功后执行的方法
                    try {
                        if (statusText == "success") {
                            for (var i = 0; i < responseText.Total; i++) {
                                $("#PrivilegesType").append("<option  value='" + responseText.Rows[i].id + "'>" + responseText.Rows[i].name + "</option>");
                            }
                        }
                    } catch (e) {

                    }
                }
            });

            //定义ligerTree
            $("#mytree").ligerTree({
                checkbox: false,
                textFieldName: 'name',
                onSelect: OnselectMenus
            });
            manager = $("#mytree").ligerGetTreeManager();

            $("#PrivilegesType").change(function () {
                var typeName = $("#PrivilegesType").find('option:selected').text();
                var dataJson;

                switch (typeName) {
                    case '选择权限类型':
                        break;
                    case '菜单':
                        $.ajax({
                            url: "/PrivilegesManagement/GetMenus",
                            type: "POST",
                            dataType: "json",
                            data: {},
                            success: function (responseText, statusText) {
                                dataJson = eval(responseText); //将json字符串转化为json数据
                                manager.clear();
                                manager.setData(dataJson);
                                manager.loadData();
                                //manager.collapseAll();//全部收缩
                            },
                            onSelect: OnselectMenus
                        });
                        break;
                    case '页面元素':
                        $.ajax({
                            url: "/PrivilegesManagement/GetMenusOfItem",
                            type: "POST",
                            dataType: "json",
                            data: {},
                            success: function (responseText, statusText) {
                                dataJson = eval(responseText); //将json字符串转化为json数据
                                manager.clear();
                                manager.setData(dataJson);
                                manager.loadData();
                                //manager.collapseAll();//全部收缩
                            },
                            onSelect: OnselectElements
                        });
                        break;
                    case '操作':
                        $.ajax({
                            url: "/PrivilegesManagement/GetMenus",
                            type: "POST",
                            dataType: "json",
                            data: {},
                            success: function (responseText, statusText) {
                                dataJson = eval(responseText); //将json字符串转化为json数据
                                manager.clear();
                                manager.setData(dataJson);
                                manager.loadData();
                                //manager.collapseAll();//全部收缩
                            },
                            onSelect: OnselectElements
                        });
                        break;
                }
            });

            //选择添加权限控制的菜单
            function OnselectMenus(note) {
                $.ajax({
                    url: "/PrivilegesManagement/ExistPrivilegeItemOfPrivilegeType",
                    type: "POST",
                    dataType: "json",
                    data: { privilegeTypeID: $("#PrivilegesType").val(), privilegeItemID: note.data.id },
                    success: function (responseText, statusText) {
                        if (responseText.success) {
                            $("#PrivilegesItemInfo").val(note.data.id);
                            $("#PrivilegesItemInfo").html(note.data.name);
                        }
                        else {
                            $("#PrivilegesItemInfo").val("-1");
                            $("#PrivilegesItemInfo").html("选择权限项目");

                            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                            $("#promptDIV").addClass(responseText.css);
                            $("#promptDIV").html(responseText.message);
                        }
                    }
                });
            }

            function OnselectElements() {
            }
        });        
    </script>
    
    <%--添加权限--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/PrivilegesManagement/AddPrivileges",
                type: "POST",
                dataType: "json"
            };

            $("#submit").click(function () {
                if (false) {

                    return false;
                } else {
                    $("#add_Privileges").ajaxForm(options);
                }
            });

            function showResponse(responseText, statusText) {
                //成功后执行的方法
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>权限管理</h2></div>
    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllPrivileges" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddPrivileges" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>

    <div class="tab-content">
       
        <%--查看所有权限--%>
        <div class="tab-pane active" id="AllPrivileges">
            <div id="privilegesgrid"></div> 
        </div>
        
        <%--添加权限--%>
        <div class="tab-pane" id="AddPrivileges">
            <form id="add_Privileges" class="form-horizontal" method="post" action="/PrivilegesManagement/AddPrivileges">
                <div class="control-group span6 offset2">
                    <label class="control-label">权限名称</label>
                    <div class="controls">
                        <input name="PrivilegesName" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">权限类型</label>
                    <div class="controls">
                        <select id="PrivilegesType" name="PrivilegesType" class="span4">
                            <option value="-1">选择权限类型</option>
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">权限项目</label>
                    <div class="controls">
                        <select id="PrivilegesItem" name="PrivilegesItem" class="span4">
                            <option id="PrivilegesItemInfo" value="-1">选择权限项目</option>
                        </select>
                    </div>
                </div>
                <div class="control-group span9 offset2">
                    <div id="mygrid1" class="controls">
                        <div id="mytree"></div>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="PrivilegesRemark" rows="4" cols="5" class="span4"></textarea>
                    </div>
                </div>
                <div class="control-group span6 offset3">
                    <input id="submit" type="submit" class="btn btn-primary btn-large span4"  value="添加权限" />
                </div>
            </form>
        </div>
    </div>
</asp:Content>


