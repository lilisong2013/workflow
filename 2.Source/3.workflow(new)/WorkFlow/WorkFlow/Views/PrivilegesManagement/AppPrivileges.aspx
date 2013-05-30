﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

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
        var managerGrid = null;
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
//        $(document).ready(function () {
//            $("#mytree").hide();
//            $("#mygrid").hide();
//            $.ajax({
//                url: "/PrivilegesManagement/GetPrivilegesType",
//                type: "POST",
//                dataType: "json",
//                data: {},
//                success: function (responseText, statusText) {
//                    //成功后执行的方法
//                    try {
//                        if (statusText == "success") {
//                            for (var i = 0; i < responseText.Total; i++) {
//                                $("#PrivilegesType").append("<option  value='" + responseText.Rows[i].id + "'>" + responseText.Rows[i].name + "</option>");
//                            }
//                        }
//                    } catch (e) {

//                    }
//                }
//            });

//            //定义ligerTree
//            $("#mytree").ligerTree({
//                checkbox: false,
//                textFieldName: 'name',
//                onSelect: OnselectMenus
//            });
//            manager = $("#mytree").ligerGetTreeManager();

//            //定义ligerGrid
//            $("#mygrid").ligerGrid({
//                width: '100%'
//            });
//            managerGrid = $("#mygrid").ligerGetGridManager();


//            $("#PrivilegesType").change(function () {
//                var typeName = $("#PrivilegesType").find('option:selected').text();
//                var dataJson;
//                $("#mytree").show();
//                $("#mygrid").hide();
//                switch (typeName) {
//                    case '选择权限类型':
//                        break;
//                    case '菜单':
//                        $.ajax({
//                            url: "/PrivilegesManagement/GetMenus",
//                            type: "POST",
//                            dataType: "json",
//                            data: {},
//                            success: function (responseText, statusText) {
//                                dataJson = eval(responseText); //将json字符串转化为json数据
//                                manager.clear();
//                                manager.setData(dataJson);
//                                manager.loadData();
//                                //manager.collapseAll();//全部收缩
//                            }
//                        });
//                        break;
//                    case '页面元素':
//                        $.ajax({
//                            url: "/PrivilegesManagement/GetMenusOfItem",
//                            type: "POST",
//                            dataType: "json",
//                            data: {},
//                            success: function (responseText, statusText) {
//                                //alert(responseText);
//                                dataJson = eval(responseText); //将json字符串转化为json数据
//                                manager.clear();
//                                manager.setData(dataJson);
//                                manager.loadData();
//                                //manager.collapseAll();//全部收缩
//                            }
//                        });
//                        break;
//                    case '操作':
//                        $("#mytree").hide();
//                        $("#mygrid").show();
//                        $.ajax({
//                            url: "/PrivilegesManagement/GetOperationOfItem",
//                            type: "POST",
//                            dataType: "json",
//                            data: {},
//                            success: function (responseText, statusText) {
//                                //alert(responseText);
//                                var dataOpertionjson = eval("(" + responseText + ")"); //将json字符串转化为json数据
//                                //更新mygrid数据
//                                managerGrid.setOptions({
//                                    columns: [
//                                                { display: '操作名称', name: 'name', width: 120 },
//                                                { display: '操作编码', name: 'code', width: 120 },
//                                                { display: '操作描述', name: 'description', width: 160 },
//                                                { display: '备注信息', name: 'remark', width: 180 }
//                                                ],
//                                    data: dataOpertionjson,
//                                    onSelectRow: OnSelectElements
//                                });
//                                managerGrid.loadData();

//                            }
//                        });
//                        break;
//                }
//            });
//        });

//        //选择添加权限控制的菜单
//        function OnselectMenus(note) {
//            var typeName = $("#PrivilegesType").find('option:selected').text();
//            switch (typeName) {
//                case '选择权限类型':
//                    break;
//                case '菜单':
//                    $.ajax({
//                        url: "/PrivilegesManagement/ExistPrivilegeItemOfPrivilegeType",
//                        type: "POST",
//                        dataType: "json",
//                        data: { privilegeTypeID: $("#PrivilegesType").val(), privilegeItemID: note.data.id },
//                        success: function (responseText, statusText) {
//                            if (responseText.success) {
//                                $("#PrivilegesItemInfo").val(note.data.id);
//                                $("#PrivilegesItemInfo").html(note.data.name);
//                            }
//                            else {
//                                $("#PrivilegesItemInfo").val("-1");
//                                $("#PrivilegesItemInfo").html("选择权限项目");

//                                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
//                                $("#promptDIV").addClass(responseText.css);
//                                $("#promptDIV").html(responseText.message);
//                            }
//                        }
//                    });
//                    break;
//                case '页面元素':
//                    $.ajax({
//                        url: "/PrivilegesManagement/ExistChildreMenus",
//                        type: "POST",
//                        dataType: "json",
//                        data: { menusID: note.data.id },
//                        success: function (responseText, statusText) {
//                            if (responseText.success) {
//                                $("#mytree").hide();
//                                $("#mygrid").show();
//                                //获取菜单元素列表信息
//                                var dataofelements;
//                                $.ajax({
//                                    url: "/PrivilegesManagement/GetElementOfItem",
//                                    type: "POST",
//                                    dataType: "json",
//                                    data: { menusID: note.data.id },
//                                    success: function (responseText, statusText) {
//                                        //alert(responseText);
//                                        dataofelements = eval("(" + responseText + ")"); //将json字符串转化为json数据
//                                        //更新mygrid数据
//                                        managerGrid.setOptions({
//                                                columns: [
//                                                { display: '页面元素名称', name: 'name', width: 120 },
//                                                { display: '页面元素编码', name: 'code', width: 120 }
//                                                ],
//                                                data: dataofelements,
//                                                onSelectRow: OnSelectElements
//                                        });
//                                        managerGrid.loadData();
//                                     }
//                                }); 
//                            }
//                            else {
//                                $("#PrivilegesItemInfo").val("-1");
//                                $("#PrivilegesItemInfo").html("选择权限项目");
//                            }
//                        }
//                    });
//                    break;
//                case '操作':
//                    break;
//            }
//        }

//        function OnSelectElements(rowdata, rowindex, rowDomElement) {
//            //alert(rowdata.id);
//            $.ajax({
//                url: "/PrivilegesManagement/ExistPrivilegeItemOfPrivilegeType",
//                type: "POST",
//                dataType: "json",
//                data: { privilegeTypeID: $("#PrivilegesType").val(), privilegeItemID: rowdata.id },
//                success: function (responseText, statusText) {
//                    if (responseText.success) {
//                        $("#PrivilegesItemInfo").val(rowdata.id);
//                        $("#PrivilegesItemInfo").html(rowdata.name);
//                    }
//                    else {
//                        $("#PrivilegesItemInfo").val("-1");
//                        $("#PrivilegesItemInfo").html("选择权限项目");

//                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
//                        $("#promptDIV").addClass(responseText.css);
//                        $("#promptDIV").html(responseText.message);
//                    }
//                }
//            });
//        }
              
    </script>

    <%--权限列表--%>
    <script type="text/javascript">
        var managerListGrid;
        $(document).ready(function () {

            //定义ligerGrid
            $("#privilegesgrid").ligerGrid({
                width: '99%',
                height: '400'
            });
            managerListGrid = $("#privilegesgrid").ligerGetGridManager();

            GetPrivilegeList(); //获取数据列表

            $("#infoTab").click(function () {
                GetPrivilegeList(); //获取数据列表
            });

        });

        function GetPrivilegeList() {
            $.ajax({
                url: "/PrivilegesManagement/GetAllPrivilegesList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataprivilegejson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新mygrid数据
                    managerListGrid.setOptions({
                        columns: [
                            { display: '权限名称', name: 'name', width: 120 },
                            { display: '权限类型', name: 'privilegetype_id', width: 120 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 160 },
                            { display: '备注信息', name: 'remark', width: 180 },
                            { display: '', width: 100,
                                render: function (row) {
                                    var html = '<i class="icon-lock"></i><a href="/MenusManagement/DeleteMenus?id=' + row.id + '">详情</a>';
                                    return html;
                                }
                            }
                            ],
                        data: dataprivilegejson
                    });
                    managerListGrid.loadData();

                }
            });
        }
    </script>
    
    <%--添加操作权限(数据)--%>
    <script type="text/javascript">
        var oManagerGrid;
        $(document).ready(function () {
            $("#oMyGrid").hide(); //初始化隐藏eMyGride表格
            //初始化ligerGrid
            $("#oMyGrid").ligerGrid({
                width: '99%',
                height: '300'
            });
            oManagerGrid = $("#oMyGrid").ligerGetGridManager();

            //切换Tab页面时重载oMyGrid数据
            $("#operationsTab").click(function () {
                BindOperationsList(); //oMyGrid绑定数据
            });

            //下拉列表控件的点击事件
            $("#oPrivilegesItem").click(function () {
                if ($("#oMyGrid").is(":hidden")) {
                    $("#oMyGrid").show();
                } else {
                    $("#oMyGrid").hide();
                }
            });
        });

        //oMyGrid绑定数据
        function BindOperationsList() {
            $.ajax({
                url: "/PrivilegesManagement/GetOperationOfItem",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataOperationsJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新oMyGrid数据
                    oManagerGrid.setOptions({
                        columns: [
                            { display: '操作名称', name: 'name', width: 120 },
                            { display: '操作编码', name: 'code', width: 120 },
                            { display: '操作描述', name: 'description', width: 160 },
                            { display: '备注信息', name: 'remark', width: 180 }
                            ],
                        data: dataOperationsJson,
                        onSelectRow: OnSelectOperations
                    });
                    //重载oMyGrid数据
                    oManagerGrid.loadData();
                }
            });
        }
    </script>

    <%--添加操作权限(按钮点击)--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var o_options = {
                beforeSubmit: o_showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/PrivilegesManagement/AddPrivilegesOfOperations",
                type: "POST",
                dataType: "json"
            };

            $("#oSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    $("#add_OperationsPrivileges").ajaxForm(o_options);
                }
            });

            //提交add_OperationsPrivileges表单前执行的函数
            function o_showRequest() {
                var oPrivilegesName = $.trim($("#oPrivilegesName").val());
                var operationsID = $("#oPrivilegesItem").val();
                //alert(oPrivilegesName);
                if (oPrivilegesName == "") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("权限名称不能为空");

                    return false;
                }

                if (operationsID == "-1") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("请选择权限项目！");

                    return false;
                }
            }

            //提交add_OperationsPrivileges表单后执行的函数
            function showResponse(responseText, statusText) {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            }
        });
       
    </script>

    <%--添加菜单权限(数据)--%>
    <script type="text/javascript">
        var mManagerTree;
        $(document).ready(function () {
            $("#mMyTree").hide(); //初始化隐藏mMyTree树
            //初始化ligerTree
            $("#mMyTree").ligerTree({
                checkbox: false,
                textFieldName: 'name',
                onSelect: OnSelectMenus
            });
            mManagerTree = $("#mMyTree").ligerGetTreeManager();

            //切换Tab页面时重载mMyTree数据
            $("#menusTab").click(function () {
                //alert("1");
                BindMenusList(); //mMyTree绑定数据
            });

            //下拉列表控件的点击事件
            $("#mPrivilegesItem").click(function () {
                if ($("#mMyTree").is(":hidden")) {
                    $("#mMyTree").show();
                } else {
                    $("#mMyTree").hide();
                }
            });
        });

        //oMyGrid绑定数据
        function BindMenusList() {
            $.ajax({
                url: "/PrivilegesManagement/GetMenusOfItem",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataMenusJson = eval(responseText); //将json字符串转化为json数据
                    mManagerTree.clear();
                    mManagerTree.setData(dataMenusJson);
                    mManagerTree.loadData();
                }
            });
        }
    </script>

    <%--添加菜单权限(按钮点击)--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var m_options = {
                beforeSubmit: m_showRequest,  // from提交前的响应的回调函数
                success: m_showResponse,  // form提交响应成功后执行的回调函数
                url: "/PrivilegesManagement/AddPrivilegesOfMenus",
                type: "POST",
                dataType: "json"
            };

            $("#mSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    $("#add_MenusPrivileges").ajaxForm(m_options);
                }
            });

            //提交add_MenusPrivileges表单前执行的函数
            function m_showRequest() {
                var mPrivilegesName = $.trim($("#mPrivilegesName").val());
                var menusID = $("#mPrivilegesItem").val();
                //alert(oPrivilegesName);
                if (mPrivilegesName == "") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("权限名称不能为空");

                    return false;
                }

                if (menusID == "-1") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("请选择权限项目！");

                    return false;
                }
            }

            //提交add_MenusPrivileges表单后执行的函数
            function m_showResponse(responseText, statusText) {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            }
        });
       
    </script>

    <%--添加页面元素权限(数据)--%>
    <script type="text/javascript">
        var eManagerTree;
        var eManagerGrid
        $(document).ready(function () {
            $("#eMyTree").hide(); //初始化隐藏eMyTree树
            $("#eMyGrid").hide(); //初始化隐藏eMyGrid表格
            //初始化ligerTree
            $("#eMyTree").ligerTree({
                checkbox: false,
                textFieldName: 'name',
                onSelect: OnSelectMenusOfElements
            });
            eManagerTree = $("#eMyTree").ligerGetTreeManager();

            //初始化ligerGrid
            $("#eMyGrid").ligerGrid({
                width: '99%',
                height: '300'
            });
            eManagerGrid = $("#eMyGrid").ligerGetGridManager();

            //切换Tab页面时重载mMyTree数据
            $("#elementsTab").click(function () {
                //alert("1");
                BindMenusListOfElements(); //mMyTree绑定数据
            });

            //下拉列表控件的点击事件
            $("#eElementPage").click(function () {
                if ($("#eMyTree").is(":hidden")) {
                    $("#eMyTree").show();
                } else {
                    $("#eMyTree").hide();
                }
            });
            $("#ePrivilegesItem").click(function () {
                if ($("#eMyGrid").is(":hidden")) {
                    $("#eMyGrid").show();
                } else {
                    $("#eMyGrid").hide();
                }
            });
        });

        //选择元素所在页面后重载eMyGrid数据
        function OnSelectMenusOfElements(note) {
            //alert(note.data.id);
            $("#ePrivilegesItemInfo").val("-1");
            $("#ePrivilegesItemInfo").html("选择权限项目（页面元素）");

            //清空eMyGrid的数据
            eManagerGrid.setOptions({
                data: { },
            });
            //重载eMyGrid数据
            eManagerGrid.loadData();

            $.ajax({
                url: "/PrivilegesManagement/ExistChildreMenus",
                type: "POST",
                dataType: "json",
                data: { menusID: note.data.id },
                success: function (responseText, statusText) {
                    //alert(responseText.success);
                    if (responseText.success) {
                        $("#eElementPageInfo").val("-1");
                        $("#eElementPageInfo").html("选择页面");
                    }
                    else {
                        $("#eElementPageInfo").val(note.data.id);
                        $("#eElementPageInfo").html(note.data.name);
                        BindElementsList(note.data.id); //重载eMyGrid的数据
                    }
                }
            });
        }

        //加载eMyTree树的数据
        function BindMenusListOfElements() {
            $.ajax({
                url: "/PrivilegesManagement/GetMenusOfItem",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataMenusJson = eval(responseText); //将json字符串转化为json数据
                    eManagerTree.clear();
                    eManagerTree.setData(dataMenusJson);
                    eManagerTree.loadData();
                }
            });
        }

        //加载表格eMyGrid的数据
        function BindElementsList(pageID) {
            $.ajax({
                url: "/PrivilegesManagement/GetElementOfItem",
                type: "POST",
                dataType: "json",
                data: {menusID:pageID},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataElementsJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新eMyGrid数据
                    eManagerGrid.setOptions({
                        columns: [
                            { display: '页面元素名称', name: 'name', width: 120 },
                            { display: '页面元素编码', name: 'code', width: 120 },
                            { display: '备注信息', name: 'remark', width: 180 }
                            ],
                        data: dataElementsJson,
                        onSelectRow: OnSelectElements
                    });
                    //重载oMyGrid数据
                    eManagerGrid.loadData();
                }
            });
        }
    </script>

    <%--添加页面元素权限(按钮点击)--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var e_options = {
                beforeSubmit: e_showRequest,  // from提交前的响应的回调函数
                success: e_showResponse,  // form提交响应成功后执行的回调函数
                url: "/PrivilegesManagement/AddPrivilegesOfElements",
                type: "POST",
                dataType: "json"
            };

            $("#eSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    $("#add_ElementsPrivileges").ajaxForm(e_options);
                }
            });

            //提交add_MenusPrivileges表单前执行的函数
            function e_showRequest() {
                var ePrivilegesName = $.trim($("#ePrivilegesName").val());//权限名称
                var elementID = $("#ePrivilegesItem").val(); //元素ID
                //alert(oPrivilegesName);
                if (ePrivilegesName == "") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("权限名称不能为空");

                    return false;
                }

                if (elementID == "-1") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("请选择权限项目！");

                    return false;
                }
            }

            //提交add_MenusPrivileges表单后执行的函数
            function e_showResponse(responseText, statusText) {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            }
        });
       
    </script>
    
    <%--判断所选项目是否已经创建权限--%>
    <script type="text/javascript">
        //选择项目(操作)后判断该操作是否已经创建权限
        function OnSelectOperations(rowdata, rowindex, rowDomElement) {
            //alert(rowdata.id);
            $.ajax({
                url: "/PrivilegesManagement/ExistPrivilegeItemOfOperations",
                type: "POST",
                dataType: "json",
                data: {operationsID: rowdata.id },
                success: function (responseText, statusText) {
                    if (responseText.success) {
                        $("#oPrivilegesItemInfo").val(rowdata.id);
                        $("#oPrivilegesItemInfo").html(rowdata.name);
                    }
                    else {
                        $("#oPrivilegesItemInfo").val("-1");
                        $("#oPrivilegesItemInfo").html("选择权限项目（操作）");

                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(responseText.css);
                        $("#promptDIV").html(responseText.message);
                    }
                }
            });
        }

        //选择项目(菜单)后判断该操作是否已经创建权限
        function OnSelectMenus(note) {
            //alert(note.data.id);
            $.ajax({
                url: "/PrivilegesManagement/ExistPrivilegeItemOfMenus",
                type: "POST",
                dataType: "json",
                data: { menusID: note.data.id },
                success: function (responseText, statusText) {
                    if (responseText.success) {
                        $("#mPrivilegesItemInfo").val(note.data.id);
                        $("#mPrivilegesItemInfo").html(note.data.name);
                    }
                    else {
                        $("#mPrivilegesItemInfo").val("-1");
                        $("#mPrivilegesItemInfo").html("选择权限项目（菜单）");

                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(responseText.css);
                        $("#promptDIV").html(responseText.message);
                    }
                }
            });
        }

        //选择项目(页面元素)后判断该操作是否已经创建权限
        function OnSelectElements(rowdata, rowindex, rowDomElement) {
            //alert(rowdata.id);
            $.ajax({
                url: "/PrivilegesManagement/ExistPrivilegeItemOfElements",
                type: "POST",
                dataType: "json",
                data: { elementID: rowdata.id },
                success: function (responseText, statusText) {
                    if (responseText.success) {
                        $("#ePrivilegesItemInfo").val(rowdata.id);
                        $("#ePrivilegesItemInfo").html(rowdata.name);
                    }
                    else {
                        $("#ePrivilegesItemInfo").val("-1");
                        $("#ePrivilegesItemInfo").html("选择权限项目（页面元素）");

                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(responseText.css);
                        $("#promptDIV").html(responseText.message);
                    }
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>权限管理</h2></div>
    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>

    <%--Tab标签--%>
    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="infoTab"><a href="#AllPrivileges" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li id="menusTab"><a href="#AddMenusPrivileges" data-toggle="tab"><i class="icon-plus"></i>菜单</a></li>
            <li id="operationsTab"><a href="#AddOperationsPrivileges" data-toggle="tab"><i class="icon-plus"></i>操作</a></li>
            <li id="elementsTab"><a href="#AddElementsPrivileges" data-toggle="tab"><i class="icon-plus"></i>页面元素</a></li>
        </ul>
    </div>

    <div class="tab-content">
       
        <%--查看所有权限--%>
        <div class="tab-pane active" id="AllPrivileges">
            <div id="privilegesgrid"></div> 
        </div>
        
        <%--添加菜单权限--%>
        <div class="tab-pane" id="AddMenusPrivileges">
            <form id="add_MenusPrivileges" class="form-horizontal" method="post" action="">
                <div class="control-group">
                    <label class="control-label">权限名称</label>
                    <div class="controls">
                        <input id="mPrivilegesName" name="mPrivilegesName" type="text" class="input-prepend span9" />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">权限类型</label>
                    <div class="controls">
                        <label class="span9 input-xlarge uneditable-input">菜单</label>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">权限项目</label>
                    <div class="controls">
                        <select id="mPrivilegesItem" name="mPrivilegesItem" class="span9">
                            <option id="mPrivilegesItemInfo" value="-1">选择权限项目（菜单）</option>
                        </select>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <div id="mMyTree"></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="mPrivilegesRemark" rows="4" cols="5" class="span9"></textarea>
                    </div>
                </div>
                <div class="control-group offset1">
                    <input id="mSubmit" type="submit" class="btn btn-primary btn-large span10"  value="添加菜单权限" />
                </div>
            </form>
        </div>

        <%--添加操作权限--%>
        <div class="tab-pane" id="AddOperationsPrivileges">
            <form id="add_OperationsPrivileges" class="form-horizontal" method="post" action="">
                <div class="control-group">
                    <label class="control-label">权限名称</label>
                    <div class="controls">
                        <input id="oPrivilegesName" name="oPrivilegesName" type="text" class="input-prepend span9" />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">权限类型</label>
                    <div class="controls">
                        <label class="span9 input-xlarge uneditable-input">操作</label>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">权限项目</label>
                    <div class="controls">
                        <select id="oPrivilegesItem" name="oPrivilegesItem" class="span9">
                            <option id="oPrivilegesItemInfo" value="-1">选择权限项目（操作）</option>
                        </select>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls span9">
                        <div id="oMyGrid"></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="oPrivilegesRemark" rows="4" cols="5" class="span9"></textarea>
                    </div>
                </div>
                <div class="control-group offset1">
                    <input id="oSubmit" type="submit" class="btn btn-primary btn-large span10"  value="添加操作权限" />
                </div>
            </form>
        </div>

        <%--添加页面元素权限--%>
        <div class="tab-pane" id="AddElementsPrivileges">
            <form id="add_ElementsPrivileges" class="form-horizontal" method="post" action="">
                <div class="control-group">
                    <label class="control-label">权限名称</label>
                    <div class="controls">
                        <input id="ePrivilegesName" name="ePrivilegesName" type="text" class="input-prepend span9" />
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">权限类型</label>
                    <div class="controls">
                        <label class="span9 input-xlarge uneditable-input">页面元素</label>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">所在页面</label>
                    <div class="controls">
                        <select id="eElementPage" name="eElementPage" class="span9">
                            <option id="eElementPageInfo" value="-1">选择页面</option>
                        </select>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls span9">
                        <div id="eMyTree"></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">权限项目</label>
                    <div class="controls">
                        <select id="ePrivilegesItem" name="ePrivilegesItem" class="span9">
                            <option id="ePrivilegesItemInfo" value="-1">选择权限项目（页面元素）</option>
                        </select>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls span9">
                        <div id="eMyGrid"></div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="ePrivilegesRemark" rows="4" cols="5" class="span9"></textarea>
                    </div>
                </div>
                <div class="control-group offset1">
                    <input id="eSubmit" type="submit" class="btn btn-primary btn-large span10"  value="添加页面元素权限" />
                </div>
            </form>
        </div>

    </div>
</asp:Content>


