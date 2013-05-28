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

            $("#PrivilegesType").change(function () {
                //var str = $("#PrivilegesType").val();
                var typeName = $("#PrivilegesType").find('option:selected').text();
                $("#mygrid1").html('<ul id="mytree"></div>');
                var dataJson;

                var manager = $("#mytree").ligerGetTreeManager();
                alert(manager);
                //alert($("#mygrid1").html());
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
                                //alert(responseText);
                                dataJson = eval(responseText); //将json字符串转化为json数据
                                //alert(dataJson);
                                $("#mytree").ligerTree({
                                    data: dataJson,
                                    checkbox: false,
                                    textFieldName: 'name',
                                    onBeforeAppend: function (parentNode, newdata) {
                                        //alert("shujujiazaiqian");
                                        //alert(newdata);
                                    },
                                    onSelect:OnselectMenus
                                });
                            },
                            complete: function (XHR, TS) { XHR = null }
                        });
                        //$("#mytree").html("caidan");
                        break;
                    case '页面元素':
                        //alert(typeName);
                        //                        $.ajax({
                        //                            url: "/PrivilegesManagement/GetMenusOfItem",
                        //                            type: "POST",
                        //                            dataType: "json",
                        //                            data: {},
                        //                            success: function (responseText, statusText) {
                        //                                //alert("caozuoyemian");
                        //                                dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                        //                                BindMenusOfElements(dataJson);
                        //                            },
                        //                            complete: function (XHR, TS) { XHR = null; alert(XHR); }
                        //                        });
                        $("#mytree").html("yemianyuansu");
                        break;
                    case '操作':
                        //alert(typeName);
                        $("#mytree").html("caozuo");
                        break;
                }
            });

            function OnselectMenus() {
                var manager = $("#mytree").ligerGetTreeManager();
                //alert(manager);
                alert("nihao");
            }

            //mygrid绑定菜单信息
            function BindMenus(dataJson) {
                alert(dataJson);
                $("#mygrid").ligerGrid({
                    data: dataJson,
                    tree: { columnName: 'name' },
                    checkbox: false,
                    alternatingRow: false,
                    autoCheckChildren: false,
                    onSelectRow: function (rowdata, rowindex, rowDomElement) {
                        //alert(rowdata.id);
                        $("#PrivilegesItemInfo").val(rowdata.id);
                        $("#PrivilegesItemInfo").html(rowdata.name);
                    },
                    columns: [{ display: '菜单名', name: 'name', width: 250, align: 'center' },
                              { display: '是否创建权限', name: 'invalid', width: 120, align: 'center',
                                  render: function (record, rowindex, value, column) {
                                      if (value != "True") {
                                          return "<img src='../../images/grid-checkbox-checked.gif' />";
                                      }
                                      else {
                                          return "<img src='../../images/grid-checkbox.gif' />";
                                      }
                                  }
                              },
                              { display: '菜单编码', name: 'code', width: 250, align: 'center' },
                              { display: '菜单URL', name: 'url', width: 250, align: 'center' },
                              { display: '备注信息', name: 'remark', width: 250, type: 'int', align: 'center'}]
                });
            }
            //mygrid绑定菜单信息(无“是否创建权限”)
            function BindMenusOfElements(dataJson) {
                alert("nihao");
                $("#mygrid").ligerGrid({
                    data: dataJson,
                    tree: { columnName: 'name' },
                    checkbox: false,
                    alternatingRow: false,
                    autoCheckChildren: false,
                    onSelectRow: function (rowdata, rowindex, rowDomElement) {
                        //alert(rowdata.id);
                        $.ajax({
                            url: "/PrivilegesManagement/GetElementOfItem",
                            type: "POST",
                            dataType: "json",
                            data: {},
                            success: function (responseText, statusText) {
                                //alert("caozuoyemian");
                                var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                                BindElements(dataJson);
                            }
                        });
                    },
                    columns: [{ display: '菜单名', name: 'name', width: 250, align: 'center' },
                              { display: '菜单编码', name: 'code', width: 250, align: 'center' },
                              { display: '菜单URL', name: 'url', width: 250, align: 'center' },
                              { display: '备注信息', name: 'remark', width: 250, type: 'int', align: 'center'}]
                });
            }
            //mygrid绑定页面元素信息
            function BindElements(dataJson) {
                $("#mygrid").ligerGrid({
                    data: dataJson,
                    checkbox: false,
                    onSelectRow: function (rowdata, rowindex, rowDomElement) {
                        //alert(rowdata.id);
                        $("#PrivilegesItemInfo").val(rowdata.id);
                        $("#PrivilegesItemInfo").html(rowdata.name);
                    },
                    columns: [{ display: '页面元素名称', name: 'name', width: 250, align: 'center' },
                              { display: '是否创建权限', name: 'invalid', width: 120, align: 'center',
                                  render: function (record, rowindex, value, column) {
                                      if (value != "True") {
                                          return "<img src='../../images/grid-checkbox-checked.gif' />";
                                      }
                                      else {
                                          return "<img src='../../images/grid-checkbox.gif' />";
                                      }
                                  }
                              },
                              { display: '页面元素编码', name: 'code', width: 250, align: 'center' },
                              { display: '初始话状态', name: 'url', width: 250, align: 'center' },
                              { display: '备注信息', name: 'remark', width: 250, type: 'int', align: 'center'}]
                });
            }
            //mygrid绑定操作信息
            function BindOperations() {
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
                        <%--<div id="mygrid"></div>--%>
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


