<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
     <link href="../../LigerUI/lib/ligerUI/skins/ligerui-icons.css" rel="Stylesheet" type="text/css"/>
     <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
     <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
    
   <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
    <%--<link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css"/>--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
     <%--LigerUI ToolBar文件--%>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerFilter.js" type="text/javascript"></script>
   <script src="../../Scripts/ligerGrid.showFilter.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "权限管理";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
  
   <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("alert alert-error alert-success");
            $("#promptDIV").html("");
        });
    </script>

    <%--菜单权限--%>
    <script type="text/javascript">
        $(document).ready(function () {
           
            GetMPrivilegesList(); //获取数据列表
            $("#menusTab").click(function () {
                GetMPrivilegesList(); //获取数据列表
            });
        });
        //获取菜单数据列表
        function GetMPrivilegesList() {
            window['m'] = $("#Mprivilegesgrid").ligerGrid({
                columns: [
                            { display: '权限名称', name: 'name', width: 200 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 200 },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="MEditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeletePrivileges(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }
                            ],
                dataAction: 'server',
                width: '99%',
                pageSizeOptions: [5, 10, 15, 20, 25, 50],
                pageSize: 10,
                height: '400',
                rownumbers: true,
                usePager: true,
                url: "/PrivilegesManagement/GetMPrivilegesList2"

            });
            m.loadData();
        }
    </script>
    <%--菜单权限列表--%>
   <%-- <script type="text/javascript">
        var MmanagerListGrid;
        $(document).ready(function () {
            //alert("Mprivilegesgrid");
            //定义ligerGrid
            $("#Mprivilegesgrid").ligerGrid({
                width: '90%',
                height: '400'
            });
            MmanagerListGrid = $("#Mprivilegesgrid").ligerGetGridManager();

            GetMPrivilegesList(); //获取数据列表

            $("#MinfoTab").click(function () {
                GetMPrivilegesList(); //获取数据列表
            });

        });

        function GetMPrivilegesList() {
            $.ajax({
                url: "/PrivilegesManagement/GetMPrivilegesList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataprivilegejson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新mygrid数据
                    //alert(dataprivilegejson);
                    MmanagerListGrid.setOptions({
                        columns: [
                            { display: '权限名称', name: 'name', width: 200 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 200 },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog('+row.id+')">详情</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="MEditDialog('+row.id+')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeletePrivileges(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }

                            ],
                        data: dataprivilegejson,
                        alternatingRow: true
                    });
                    MmanagerListGrid.loadData();

                }
            });
        }
    </script>--%>

    <%--元素权限--%>
    <script type="text/javascript">
        var EmanagerListGrid;
        $(document).ready(function () {
            // alert("Eprivilegesgrid");
            //定义ligerGrid
            $("#Eprivilegesgrid").ligerGrid({
                width: '90%',
                height: '400'
            });
            EmanagerListGrid = $("#Eprivilegesgrid").ligerGetGridManager();

            GetEPrivilegesList(); //获取数据列表

            $("#EinfoTab").click(function () {
                GetMPrivilegesList(); //获取数据列表
            });

        });

        function GetEPrivilegesList() {
            $.ajax({
                url: "/PrivilegesManagement/GetEPrivilegesList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataprivilegejson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新mygrid数据
                    //alert(dataprivilegejson);
                    window['e'] = $("#Eprivilegesgrid").ligerGrid({
                        columns: [
                            { display: '权限名称', name: 'name', width: 200 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 200 },
                                  { display: '', width: 60,
                                      render: function (row) {
                                          var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                          return html;
                                      }
                                  },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="MEditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeletePrivileges(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }

                            ],
                        data: dataprivilegejson,
                        alternatingRow: true
                    });
                    // EmanagerListGrid.loadData();
                    e.loadData();
                }
            });
        }
    </script>

   <%--元素权限列表--%>
    <%--<script type="text/javascript">
        var EmanagerListGrid;
        $(document).ready(function () {
            // alert("Eprivilegesgrid");
            //定义ligerGrid
            $("#Eprivilegesgrid").ligerGrid({
                width: '90%',
                height: '400'
            });
            EmanagerListGrid = $("#Eprivilegesgrid").ligerGetGridManager();

            GetEPrivilegesList(); //获取数据列表

            $("#EinfoTab").click(function () {
                GetMPrivilegesList(); //获取数据列表
            });

        });

        function GetEPrivilegesList() {
            $.ajax({
                url: "/PrivilegesManagement/GetEPrivilegesList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataprivilegejson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新mygrid数据
                    //alert(dataprivilegejson);
                    EmanagerListGrid.setOptions({
                        columns: [
                            { display: '权限名称', name: 'name', width: 200 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 200 },
                                  { display: '', width: 60,
                                      render: function (row) {
                                          var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                          return html;
                                      }
                                  },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="MEditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeletePrivileges(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }

                            ],
                        data: dataprivilegejson,
                        alternatingRow: true
                    });
                    EmanagerListGrid.loadData();

                }
            });
        }
    </script>--%>

    <%--操作权限--%>
    <script type="text/javascript">
        var OmanagerListGrid;
        $(document).ready(function () {
            // alert("Eprivilegesgrid");
            //定义ligerGrid
            $("#Oprivilegesgrid").ligerGrid({
                width: '90%',
                height: '400'
            });
            OmanagerListGrid = $("#Oprivilegesgrid").ligerGetGridManager();

            GetOPrivilegesList(); //获取数据列表

            $("#OinfoTab").click(function () {
                GetMPrivilegesList(); //获取数据列表
            });

        });

        function GetOPrivilegesList() {
            $.ajax({
                url: "/PrivilegesManagement/GetOPrivilegesList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataprivilegejson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新mygrid数据
                    //alert(dataprivilegejson);
                    window['o'] = $("#Oprivilegesgrid").ligerGrid({
                        columns: [
                            { display: '权限名称', name: 'name', width: 200 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 200 },
                                  { display: '', width: 60,
                                      render: function (row) {
                                          var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                          return html;
                                      }
                                  },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="MEditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeletePrivileges(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }

                            ],
                        data: dataprivilegejson,
                        alternatingRow: true
                    });
                    //OmanagerListGrid.loadData();
                    o.loadData();
                }
            });
        }
    </script>
    <%--操作权限列表--%>
   <%-- <script type="text/javascript">
         var OmanagerListGrid;
         $(document).ready(function () {
             // alert("Eprivilegesgrid");
             //定义ligerGrid
             $("#Oprivilegesgrid").ligerGrid({
                 width: '90%',
                 height: '400'
             });
             OmanagerListGrid = $("#Oprivilegesgrid").ligerGetGridManager();

             GetOPrivilegesList(); //获取数据列表

             $("#OinfoTab").click(function () {
                 GetMPrivilegesList(); //获取数据列表
             });

         });

         function GetOPrivilegesList() {
             $.ajax({
                 url: "/PrivilegesManagement/GetOPrivilegesList",
                 type: "POST",
                 dataType: "json",
                 data: {},
                 success: function (responseText, statusText) {
                     //alert(responseText);
                     var dataprivilegejson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                     //更新mygrid数据
                     //alert(dataprivilegejson);
                     OmanagerListGrid.setOptions({
                         columns: [
                            { display: '权限名称', name: 'name', width: 200 },
                            { display: '权限项目', name: 'privilegeitem_id', width: 200 },
                                  { display: '', width: 60,
                                      render: function (row) {
                                          var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                          return html;
                                      }
                                  },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="MEditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 60,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeletePrivileges(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }

                            ],
                         data: dataprivilegejson,
                         alternatingRow: true
                     });
                     OmanagerListGrid.loadData();

                 }
             });
         }
    </script>--%>

   <%--详情弹出框--%>
   <script type="text/javascript">
       function DetailDialog(id) {

           if (id) {
               $.ligerDialog.open({
                   title: '详情(' + id + ')信息',
                   width: 700,
                   height: 600,
                   url: '/PrivilegesManagement/DetailInfo?id=' + id
               });
           }
       }
   </script>

   <%--菜单编辑弹出框--%>
   <script type="text/javascript">
       function MEditDialog(id) {

           if (id) {
               var pm = $.ligerDialog.open({
                   title: '更新流程信息',
                   width: 800,
                   height: 500,
                   showMax: true,
                   showMin: true,
                   url: '/PrivilegesManagement/EditPage?id=' + id,
                   buttons:
                    [
                    { text: '返回', onclick: function (item, dialog) { m.loadData();dialog.close(); } }

                    ]
               });

           }
       }
   </script>

   <%--操作编辑弹出框--%>
   <script type="text/javascript">
       function OEditDialog(id) {

           if (id) {
               var pm = $.ligerDialog.open({
                   title: '更新流程信息',
                   width: 800,
                   height: 500,
                   showMax: true,
                   showMin: true,
                   url: '/PrivilegesManagement/EditPage?id=' + id,
                   buttons:
                    [
                    { text: '返回', onclick: function (item, dialog) {OmanagerListGrid.loadData() ; dialog.close(); } }

                    ]
               });

           }
       }
   </script>

    <%--删除提示信息的函数--%>
    <script type="text/javascript">
        function DeletePrivileges(id) {
            //alert(id);
            var privilegeid = id;
            $.ligerDialog.confirm('确定要删除吗?', function (yes) {
                if (yes) {
                    $.ajax({
                        url: "/PrivilegesManagement/DeletePrivileges",
                        type: "POST",
                        dataType: "json",
                        data: { privilegeID: privilegeid },
                        success: function (responseText, statusText) {
                            var dataJson = eval("(" + responseText + ")");
                            //删除提示信息
                            show_DIV(dataJson);
                            m.loadData();
                        }
                    });

                    //删除提示信息
                    function show_DIV(data) {
                        $("#promptDIV").removeClass("alert alert-error alert-success");
                        $("#promptDIV").addClass(data.css);
                        $("#promptDIV").html(data.message);
                    }
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
                            { display: '操作名称', name: 'name', width: 80 },
                            { display: '操作编码', name: 'code', width: 80 },
                            { display: '操作描述', name: 'description', width: 80 },
                            { display: '备注信息', name: 'remark', width: 100 }
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
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert alert-error");
                    $("#promptDIV").html("权限名称不能为空");

                    return false;
                }

                if (operationsID == "-1") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert alert-error");
                    $("#promptDIV").html("请选择权限项目！");

                    return false;
                }
            }

            //提交add_OperationsPrivileges表单后执行的函数
            function showResponse(responseText, statusText) {
                var dataJson = eval("(" + responseText + ")");
//                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
//                $("#promptDIV").addClass(responseText.css);
                //                $("#promptDIV").html(responseText.message);
                 show_DIV(dataJson)
//                if (responseText.success) {
//                    location.href = responseText.toUrl;
//                }
            }

            //提示信息
            function show_DIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
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
                nodeWidth:'auto',
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
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert-error");
                    $("#promptDIV").html("权限名称不能为空");

                    return false;
                }

                if (menusID == "-1") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert-error");
                    $("#promptDIV").html("请选择权限项目！");

                    return false;
                }
            }

            //提交add_MenusPrivileges表单后执行的函数
            function m_showResponse(responseText, statusText) {
                var dataJson = eval("(" + responseText + ")");
                show_DIV(dataJson)
//                if (responseText.success) {
//                    location.href = responseText.toUrl;
//                }
            }

            //提示信息
            function show_DIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
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
                data: {}
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
            //alert(pageID);
            $.ajax({
                url: "/PrivilegesManagement/GetElementOfItem",
                type: "POST",
                dataType: "json",
                data: { menusID: pageID },
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataElementsJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新eMyGrid数据
                    eManagerGrid.setOptions({
                        columns: [
                            { display: '页面元素名称', name: 'name', width: 80 },
                            { display: '页面元素编码', name: 'code', width: 80 },
                            { display: '备注信息', name: 'remark', width: 100 }
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
                var ePrivilegesName = $.trim($("#ePrivilegesName").val()); //权限名称
                var elementID = $("#ePrivilegesItem").val(); //元素ID
                //alert(oPrivilegesName);
                if (ePrivilegesName == "") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert-error");
                    $("#promptDIV").html("权限名称不能为空");

                    return false;
                }

                if (elementID == "-1") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert-error");
                    $("#promptDIV").html("请选择权限项目！");

                    return false;
                }
            }

            //提交add_MenusPrivileges表单后执行的函数
            function e_showResponse(responseText, statusText) {

                var dataJson = eval("(" + responseText + ")");
                //提示信息
                show_DIV(dataJson);
               
//                if (responseText.success) {
//                    location.href = responseText.toUrl;
//                }
            }
            //删除提示
            function show_DIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
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
                data: { operationsID: rowdata.id },
                success: function (responseText, statusText) {
                    var dataJson = eval("(" + responseText + ")");
                    if (responseText.success) {
                        $("#oPrivilegesItemInfo").val(rowdata.id);
                        $("#oPrivilegesItemInfo").html(rowdata.name);
                    }
                    else {
                        $("#oPrivilegesItemInfo").val("-1");
                        $("#oPrivilegesItemInfo").html("选择权限项目（操作）");

                        show_DIV(dataJson);
//                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
//                        $("#promptDIV").addClass(responseText.css);
//                        $("#promptDIV").html(responseText.message);
                    }
                }
            });
            function show_DIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
            }

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
                    var dataJson = eval("(" + responseText + ")");
                    if (responseText.success) {
                        $("#mPrivilegesItemInfo").val(note.data.id);
                        $("#mPrivilegesItemInfo").html(note.data.name);
                    }
                    else {
                        $("#mPrivilegesItemInfo").val("-1");
                        $("#mPrivilegesItemInfo").html("选择权限项目（菜单）");
                        show_DIV(dataJson);
//                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
//                        $("#promptDIV").addClass(responseText.css);
//                        $("#promptDIV").html(responseText.message);
                    }
                }
            });

            //提示信息
            function show_DIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
            }
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
                    var dataJson = eval("("+responseText+")");
                    if (responseText.success) {
                        $("#ePrivilegesItemInfo").val(rowdata.id);
                        $("#ePrivilegesItemInfo").html(rowdata.name);
                    }
                    else {
                        $("#ePrivilegesItemInfo").val("-1");
                        $("#ePrivilegesItemInfo").html("选择权限项目（页面元素）");
                        show_DIV(dataJson)
                        //                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        //                        $("#promptDIV").addClass(responseText.css);
                        //                        $("#promptDIV").html(responseText.message);
                    }
                }
            });
            function show_DIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
            }
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
                
    <div class="tabbable">
    
    <ul class="nav nav-tabs">                 
            <li class="active"><a href="#AllPrivileges" data-toggle="tab">全部</a></li>           
            <li id="menusTab"><a href="#AddMenusPrivileges" data-toggle="tab"><i class="icon-plus"></i>菜单</a></li>
            <li id="operationsTab"><a href="#AddOperationsPrivileges" data-toggle="tab"><i class="icon-plus"></i>操作</a></li>
            <li id="elementsTab"><a href="#AddElementsPrivileges" data-toggle="tab"><i class="icon-plus"></i>页面元素</a></li>
    </ul>
   
    <div class="tab-content">

             <div class="tab-pane active" id="AllPrivileges">
              <div class="tabbable tabs-left">
              <ul class="nav nav-tabs">
               <li class="active"><a href="#MPrivileges" data-toggle="tab">菜单权限</a></li>
               <li><a href="#OPrivileges" data-toggle="tab">操作权限</a></li>
               <li><a href="#EPrivileges" data-toggle="tab">页面元素</a></li>
              </ul>
              <div class="tab-content">
                <div class="tab-pane active" id="MPrivileges">
                   <div id="Mprivilegesgrid"></div> 
                </div>

                <div class="tab-pane" id="OPrivileges">
                    <div id="Oprivilegesgrid"></div> 
                </div>

                <div class="tab-pane" id="EPrivileges">
                    <div id="Eprivilegesgrid"></div> 
                </div>
               
               </div> 

              </div>
               
           
             </div>
     
        <%--添加菜单权限--%>
        <div class="tab-pane" id="AddMenusPrivileges">
            <form id="add_MenusPrivileges" class="form-horizontal" method="post" action="">
                <div class="control-group">
                    <label class="control-label">权限名称</label>
                    <div class="controls">
                        <input id="mPrivilegesName" name="mPrivilegesName" type="text" class="input-prepend span9" placeholder="权限名称"/>
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
                        <textarea name="mPrivilegesRemark" rows="4" cols="5" class="span9" placeholder="备注信息"></textarea>
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
                        <input id="oPrivilegesName" name="oPrivilegesName" type="text" class="input-prepend span9" placeholder="权限名称"/>
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
                        <textarea name="oPrivilegesRemark" rows="4" cols="5" class="span9" placeholder="备注信息"></textarea>
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
                        <input id="ePrivilegesName" name="ePrivilegesName" type="text" class="input-prepend span9" placeholder="权限名称"/>
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
                        <textarea name="ePrivilegesRemark" rows="4" cols="5" class="span9" placeholder="备注信息"></textarea>
                    </div>
                </div>
                <div class="control-group offset1">
                    <input id="eSubmit" type="submit" class="btn btn-primary btn-large span10"  value="添加页面元素权限" />
                </div>
            </form>
        </div>
    </div>
     
    </div>

    </div>
</asp:Content>


