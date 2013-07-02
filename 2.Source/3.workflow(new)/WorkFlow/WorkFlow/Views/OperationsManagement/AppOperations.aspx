<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    权限管理系统/操作管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--LigerUI Dialog文件--%>
    <%--<link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css"/>--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
  
    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>
    <%--在Grid中显示operation信息--%>
     <script type="text/javascript">
         var managerListGrid;
         $(document).ready(function () {
             //定义ligerGrid;
             $("#operationsgrid").ligerGrid({
                 width: '90%',
                 height: 400
             });
             managerListGrid = $("#operationsgrid").ligerGetGridManager();
             GetOperationsList(); //获取用户数据列表
             $("#infoTab").click(function () {
                 GetOperationsList(); //获取用户数据列表
             });
         });
         function GetOperationsList() {
             $.ajax({
                 url: "/OperationsManagement/GetOperations_Apply",
                 type: "POST",
                 dataType: "json",
                 data: {},
                 success: function (responseText, statusText) {
                    //alert(responseText);

                     var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据

                     //更新mygrid数据
                     managerListGrid.setOptions({
                         columns: [
                        { display: '操作名称', name: 'name', width: 80, align: 'center' },
                        { display: '操作编码', name: 'code', width: 80, align: 'center' },
                        { display: '操作描述', name: 'description', width: 80, align: 'center' },
                        { display: '备注', name: 'remark', width: 100 ,align:'center'},     
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-lock"></i><a href="/OperationsManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-edit"></i><a href="/OperationsManagement/EditPage?id=' + row.id + '">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteOperation('+ row.id+')">删除</a>';
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
                        GetOperationsList();
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(responseText.css);
                        $("#promptDIV").html(responseText.message);
                    }
                });
             }
          })
        }
    </script>
    <%--添加操作--%>
     <script type="text/javascript">
            $(document).ready(function () {
                var form = $("#add_Operations");
                form.submit(function () {
                    if ($.trim($("#operationsName").val()).length == 0) {
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass("p-errorDIV");
                        $("#promptDIV").html("操作名称不能为空！");

                        return false;
                    }
                    else {
                        $.post(form.attr("action"),
                    form.serialize(),
                    function (result, status) {
                        //debugger
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(result.css);
                        $("#promptDIV").html(result.message);

                        if (result.success) {
                            location.href = result.toUrl;
                        }
                    },
                    "JSON");
                        return false;
                    }
                });
            });
    </script>

 </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>功能管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllOperations" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddOperations" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
   
    <div class="tab-content">
        <%--查看所有操作列表--%>
        <div class="tab-pane active" id="AllOperations">
        <div id="operationsgrid"></div>
        </div>
        <%--添加操作--%>
        <div class="tab-pane" id="AddOperations">
          <form id="add_Operations" class="form-horizontal" method="post" action="/OperationsManagement/AddOperations">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsName">操作名称：</label>
                        <div class="controls">
                            <input type="text" name="operationsName" id="operationsName" class="input-prepend span4"/>                  
                        </div>
                    </div>
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsCode">操作编码：</label>
                        <div class="controls">
                            <input type="text" name="operationsCode" id="operationsCode" class="input-prepend span4" />
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsDescription">操作描述：</label>
                        <div class="controls">
                           <textarea name="operationsDescription" id="operationsDescription" rows="4" cols="5" class="span4"></textarea>
                        </div>
                    </div>              
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsRemark">备注：</label>
                        <div class="controls">
                            <textarea name="operationsRemark" id="operationsRemark" rows="4" cols="5" class="span4"></textarea>
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
                            <input type="submit" value="添加" class="btn btn-primary span1" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="reset" value="重置"  class="btn btn-primary span1" />
                        </div>
                    </div>
                </form>
        </div>
    </div>

</asp:Content>

