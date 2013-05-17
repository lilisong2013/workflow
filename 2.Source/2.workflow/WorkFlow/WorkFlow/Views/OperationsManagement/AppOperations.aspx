<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppOperations
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
   <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
  
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         var tem = "";
         function checkAll(e, thisvalue) {
             tem = document.getElementById("operationsInvalid").value;
             var bb = document.getElementById("operationsInvalid");
             if (e.checked == true) {
                 tem = thisvalue;
             }
             else {
                 tem = tem.replace(thisvalue,"False");
             }
             bb.value = tem;
         }
    </script>
        <script type="text/javascript">
            $(document).ready(function () {
                var form = $("#add_Operations");
                form.submit(function () {
                    if ($.trim($("#operationsName").val()).length == 0) {
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass("p-warningDIV");
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

        <script type="text/javascript">
            $(document).ready(function () {
                $("#AllOperations").ligerGrid({
                    columns: [
                { display: '操作名称', name: 'name', width: 80 },
                { display: '操作编码', name: 'code', width: 80 },
                { display: '操作描述', name: 'description', width: 80 },
                { display: '备注', name: 'remark', width: 180 },
                { display: '是否有效', name: 'invalid', width: 80,
                      render: function (record, rowindex, value, column) {
                          if (!value) {
                              return "<img src='../../images/grid-checkbox.gif' />";
                          }
                          else {
                              return "<img src='../../images/grid-checkbox-checked.gif' />";
                          }
                      }
                 },            
                { display: '', width: 200,
                    render: function (row) {
                        var html = '<i class="icon-lock"></i><a href="/OperationsManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/OperationsManagement/ChangePage?id=' + row.id + '">删除</a><i class="icon-edit" ></i><a href="/OperationsManagement/EditPage?id=' + row.id + '">编辑</a> ';
                        return html;
                    }
                }
                ],
                    dataAction: 'server',
                    width: '100%',
                    pageSizeOptions: [5, 10, 15, 20, 25, 50],
                    pageSize: 15,
                    height: '400',
                    url: '/OperationsManagement/GetOperations_Apply',
                    rownumbers: true,
                    usePager: true
                });

            });
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>功能管理</h2></div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllOperations" data-toggle="tab"><i class="icon-check"></i>全部<%=10 %></a></li>
            <li><a href="#AddOperations" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
   <div class="container">
     <%--操作提示DIV--%>
     <div id="promptDIV" class="row"></div>
    </div>
    <div class="tab-content">
        <div class="tab-pane active" id="AllOperations"></div>
        <div class="tab-pane" id="AddOperations">
          <form id="add_Operations" class="form-horizontal" method="post" action="/OperationsManagement/RegisterOperations">
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
                        <label class="control-label" for="operationsApp_id">应用系统ID：</label>
                        <div class="controls">
                            <input type="text" name="operationsApp_id" id="operationsApp_id" class="input-prepend span4" />
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="operationsInvalid">是否有效：</label>
                        <div class="controls">
                        <input type="checkbox" id="osInvalid" name="osInvalid" checked="checked" onclick="checkAll(this,'true')"/>
                        <input type="hidden" name="operationsInvalid" id="operationsInvalid"  value="true" class="input-prepend span4"/>
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
                            <input type="hidden" name="createdBy" id="createdBy" value="<%=32%>" />
                            <% string ipAddress = Saron.Common.PubFun.IPHelper.GetClientIP(); %>
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

