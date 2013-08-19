<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
  
    <link href="../../LigerUI/lib/ligerUI/skins/ligerui-icons.css" rel="Stylesheet" type="text/css"/>
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--LigerUI Dialog文件--%>
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
        var PageName = "操作管理";
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
   
    <%--在Grid中分页显示operation信息--%>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#infoTab").click(function () {//切换Tab标签时获取操作列表
                GetOperationsList();
            })

            GetOperationsList();//获取操作列表
            function GetOperationsList() {
                window['t'] = $("#operationsgrid").ligerGrid({
                    columns: [
                        { display: '操作ID', name: 'id', width: 80, align: 'center' },
                        { display: '操作名称', name: 'name', align: 'center' },
                        { display: '操作编码', name: 'code', align: 'center' },
                        { display: '操作描述', name: 'description', align: 'center' },
                        { display: '备注信息', name: 'remark', align: 'center' },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                return html;
                            }
                        }, { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteOperation(' + row.id + ')">删除</a>';
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
                    url: "/OperationsManagement/GetOperations_List"
                });
                t.loadData();
            }
        });
    </script>
    <%--编辑信息弹出框--%>
    <script type="text/javascript">
        function EditDialog(id) {

            if (id) {
                var m = $.ligerDialog.open({

                    title: '更新操作信息',
                    width: 900,
                    height: 600,
                    isDrag: true,
                    url: '/OperationsManagement/EditPage?id=' + id,
                    buttons:
                    [
                    { text: '返回', onclick: function (item, dialog) { t.loadData(); dialog.close(); } }

                    ]
                });
            }
        }
    </script>

    <%--详情信息弹出框--%>
    <script type="text/javascript">
        function DetailDialog(id) {

            if (id) {
                $.ligerDialog.open({
                  title:'详情('+id+')信息',
                  width:700,
                  height: 600,
                  isDrag: true,
                  url:'/OperationsManagement/DetailInfo?id='+id
                });
            }
        }
    </script>

    <%--删除提示信息的函数--%>
    <script type="text/javascript">
        function DeleteOperation(id) {
           // alert(id);
            var operationid = id;
            $.ligerDialog.confirm('确定要删除吗?', function (yes) {
                //return true;
                if (yes) {
                    $.ajax({
                        url: "/OperationsManagement/DeleteOperation",
                        type: "POST",
                        dataType: "json",
                        data: { operationID: operationid },
                        success: function (responseText, statusText) {
                            var dataJson = eval("(" + responseText + ")");
                            show_DIV(dataJson);
                            t.loadData();
                        }


                    });

                    //删除提示信息
                    function show_DIV(data) {
                        $("#promptDIV").removeClass("alert alert-error alert-success");
                        $("#promptDIV").addClass(data.css);
                        $("#promptDIV").html(data.message);
                    }
                }
            })
        }
    </script>

    <%--添加操作信息--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#saveSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    AddOperations(); //添加操作信息
                }
            });

            //添加操作信息
            function AddOperations() {
                var options = {
                    beforeSubmit: operation_showRequest, //form提交前的响应的回调函数
                    success: operation_showResponse, //form提交相应成功后执行的回调函数
                    url: "/OperationsManagement/AddOperations",
                    type: "POST",
                    dataType: "json"
                };
                $("#add_Operations").ajaxForm(options);
            }

            //form提交前的响应的回调函数
            function operation_showRequest() {
                var operationName = $("#operationsName").val();
 
                if (operationName == "") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert alert-error");
                    $("#promptDIV").html("操作名称不能为空!");
                    return false;
                }
            }

            //form提交相应成功后执行的回调函数
            function operation_showResponse(responseText, statusText) {
                var dataJson = eval("(" + responseText + ")");
                show_promptDIV(dataJson);
                t.loadData();
            }

            //提示信息
            function show_promptDIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
            }
        });
    </script>
   
   <%--查询操作--%>
   <script type="text/javascript">
       var key;
       function search() {
           key = $("#txtKey").val();
          // alert(key);
           $.ajax({
               url: "/OperationsManagement/GetListByOperationName?operationName=" + key,
               type: "POST",
               dataType: "json",
               data: {},
               success: function (responseText, statusText) {
                   //alert(responseText);
                   var dataSearchJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                   //alert(dataSearchJson2);
                   $("#operationsgrid").ligerGrid({
                       columns: [
                        { display: '操作ID', name: 'id', width: 80, align: 'center' },
                        { display: '操作名称', name: 'name', align: 'center' },
                        { display: '操作编码', name: 'code', align: 'center' },
                        { display: '操作描述', name: 'description', align: 'center' },
                        { display: '备注信息', name: 'remark', align: 'center' },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                return html;
                            }
                        }, { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteOperation(' + row.id + ')">删除</a>';
                                return html;
                            }
                        }
                       ],
                       data: dataSearchJson,
                       newPage: 1

                   });

                   $("#operationsgrid").ligerGetGridManager().loadData();

               }
           });
          
       }
   </script>
 </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>操作管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllOperations" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddOperations" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
   
    <div class="tab-content">
        
        <div class="tab-pane active" id="AllOperations">
        <%--查询按钮--%> 
        <b>操作名称:</b><input id="txtKey" type="text" class="input-medium search-query span3"/>
        <input id="btnOK" type="button" value="查询" onclick="search()" class="btn btn-primary"/> 
        <hr />   
        <%--查看所有操作列表--%>
        <div id="operationsgrid"></div>
        </div>
        <%--添加操作--%>
        <div class="tab-pane" id="AddOperations">
          <form id="add_Operations" class="form-horizontal" method="post" action="">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsName">操作名称：</label>
                        <div class="controls">
                            <input type="text" name="operationsName" id="operationsName" class="input-prepend span4" placeholder="操作名称"/>                  
                        </div>
                    </div>
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsCode">操作编码：</label>
                        <div class="controls">
                            <input type="text" name="operationsCode" id="operationsCode" class="input-prepend span4" placeholder="操作编码" />
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsDescription">操作描述：</label>
                        <div class="controls">
                           <textarea name="operationsDescription" id="operationsDescription" rows="4" cols="5" class="span4" placeholder="操作描述" maxlength="200"></textarea>
                        </div>
                    </div>              
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsRemark">备注信息：</label>
                        <div class="controls">
                            <textarea name="operationsRemark" id="operationsRemark" rows="4" cols="5" class="span4" placeholder="备注信息" maxlength="80"></textarea>
                            <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
                            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                            <%string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                            <%DateTime t = Convert.ToDateTime(s); %>
                            <input type="hidden" name="operationsApp_id" id="operationsApp_id" value="<%=m_usersModel.app_id%>"/>                  
                            <input type="hidden" name="createdBy" id="createdBy" value="<%=m_usersModel.id%>" />
                            <input type="hidden" name="createdAt" id="createdAt" value="<%=t%>"/>
                            <input type="hidden" name="createdIP" id="createdIP" value="<%= ipAddress %>" />
                        </div>
                    </div>
                    <div class="control-group span6 offset3">
                        <div class="controls">
                            <input  id="saveSubmit" type="submit" value="添加" class="btn btn-primary span1" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="reset" value="重置"  class="btn btn-primary span1" />
                        </div>
                    </div>
                </form>
        </div>
    </div>

</asp:Content>

