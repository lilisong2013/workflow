<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    系统菜单页
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

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    
    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>

    <%--在Grid中显示menus信息--%>
    <script type="text/javascript">
        var managerListGrid;
        $(document).ready(function () {
            //定义ligerGrid
            $("#menusgrid").ligerGrid({
                width: '99%',
                tree: { columnName: 'name' },
                alternatingRow: false
            });
            managerListGrid = $("#menusgrid").ligerGetGridManager();

            GetMenusList(); //获取数据列表

            $("#infoTab").click(function () {
                GetMenusList(); //获取数据列表
            });
        });

        function GetMenusList() {
            $.ajax({
                url: "/MenusManagement/GetMenusList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据

                    //更新mygrid数据
                    managerListGrid.setOptions({
                        columns: [{ display: '菜单名', name: 'name', width: 150, align: 'center' },
                                  { display: '菜单编码', name: 'code', width: 150, align: 'center' },
                                  { display: '菜单URL', name: 'url', width: 150, align: 'center' },
                                  { display: '备注信息', name: 'remark', width: 180, type: 'int', align: 'center' },
                                  { display: '', width: 100,
                                      render: function (row) {
                                          var html = '<i class="icon-lock"></i><a href="#">详情</a>';
                                          return html;
                                      }
                                  },
                                  { display: '', width: 100,
                                      render: function (row) {
                                          var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteMenu(' + row.id + ')">删除</a>';
                                          return html;
                                      }
                                  }
                                 ],
                         data: dataJson
                    });
                    managerListGrid.loadData();
                }
            });
        }

        function DeleteMenu(id) {
            //alert(id);
            var menuid = id;
            $.ajax({
                url: "/MenusManagement/DeleteMenus",
                type: "POST",
                dataType: "json",
                data: { menuID: menuid },
                success: function (responseText, statusText) {
                    GetMenusList();
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass(responseText.css);
                    $("#promptDIV").html(responseText.message);
                }
            });
        }
    </script>

    <%--添加菜单选项卡：菜单树的显示与隐藏--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#selectMenus").hide();

            $("#MenusParent").click(function () {
                if ($("#selectMenus").is(":hidden")) {
                    $("#selectMenus").show();
                }
                else {
                    $("#selectMenus").hide();
                }
            });
        });
    </script>

    <%--菜单树的数据显示--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var datas;
            var dataJson;

            $.ajax({
                url: "/MenusManagement/GetMenus",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    datas = responseText;//获取到的菜单json格式字符串
                    dataJson = eval(datas); //将json字符串转化为json数据
                    //显示菜单树
                    $("#tree1").ligerTree({ data: dataJson, checkbox: false,textFieldName:'name' , onSelect: onSelect });
                }
            });
            //选择节点将父菜单信息赋给menusInfo
            function onSelect(note) {
                $("#menusInfo").val(note.data.id);
                $("#menusInfo").html(note.data.name);
            }
        });        
    </script>

    <%--添加菜单--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/MenusManagement/AddMenus",
                type: "POST",
                dataType: "json"
            };

            $("#submit").click(function () {
                if (false) {

                    return false;
                } else {
                    $("#add_Menus").ajaxForm(options);
                }
            });

            function showResponse(responseText, statusText) {
                //成功后执行的方法
                //alert(responseText.Id + responseText.Name);
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            }
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>菜单管理</h2></div>
    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="infoTab"><a href="#AllMenus" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddMenus" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
       
        <%--查看所有菜单--%>
        <div class="tab-pane active" id="AllMenus">
            <div id="menusgrid"></div> 
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
                            <option id="menusInfo" value="-1">顶级菜单</option>
                        </select>
                    </div>
                </div>
                <div id="selectMenus" class="control-group span6 offset2">
                    <div class="controls">
                        <ul id="tree1"></ul>
                    </div>
                </div>

                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="MenusRemark" rows="4" cols="5" class="span4"></textarea>
                    </div>
                </div>
                <div class="control-group span6 offset3">
                    <input id="submit" type="submit" class="btn btn-primary btn-large span4"  value="添加菜单" />
                </div>
            </form>
        </div>
    </div>

</asp:Content>


