﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppElements
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
   <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
  
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         $(document).ready(function () {
             $("#AllElements").ligerGrid({
                 columns: [
                { display: '元素名称', name: 'name', width: 80 },
                { display: '元素编码', name: 'code', width: 80 },
                { display: '初始化状态ID', name: 'initstatus_id', width: 80 },
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
                        var html = '<i class="icon-lock"></i><a href="/ElementsManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/ElementsManagement/ChangePage?id=' + row.id + '">删除</a><i class="icon-edit"></i><a href="/ElementsManagement/EditPage?id=' + row.id + '">编辑</a>';
                        return html;
                    }
                }
                ],
                 dataAction: 'server',
                 width: '100%',
                 pageSizeOptions: [5, 10, 15, 20, 25, 50],
                 pageSize: 15,
                 height: '400',
                 url: '/ElementsManagement/GetElements_Apply',
                 rownumbers: true,
                 usePager: true
             });

         });
 </script>
 <script type="text/javascript">
     $(document).ready(function () {
         var form = $("#addElements");
         form.submit(function () {
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
         });
     });
 </script>
 <script type="text/javascript">
     $(document).ready(function () {
         BindStatus();
         $("#StatusInfo").html("请选择");
     });
     function BindStatus() {
         $.ajax({
             type: "GET",
             contentType: "application/json",
             url: "ElementsManagement/GetStatusName",
             data: {}, //即使参数为空，也需要设置
             dataType: 'JSON', //返回的类型为XML
             success: function (result, status) {
                 //成功后执行的方法
                 try {
                     if (status == "success") {
                         for (var i = 0; i < result.Total; i++) {
                             $("#StatusParent").append("<option value='" + result.Rows[i].InitStatusID + "'>" + result.Rows[i].InitStatusName + "</option>");
                         }
                     }
                 } catch (e)
               { }
             }
         });
     }
 </script>

 <script type="text/javascript">
     $(document).ready(function () {
         BindMenuName();
         $("#MenuInfo").html("请选择");
     });
     function BindMenuName() {
         $.ajax({
             type: "GET",
             contentType: "application/json",
             url: "ElementsManagement/GetMenusName",
             data: {}, //即使参数为空，也需要设置
             dataType: 'JSON', //返回的类型为XML
             success: function (result, status) {
                 //成功后执行的方法
                 try {
                     if (status == "success") {
                         for (var i = 0; i < result.Total; i++) {
                             $("#MenuParent").append("<option value='" + result.Rows[i].menusID + "'>" + result.Rows[i].menusName + "</option>");
                         }
                     }
                 } catch (e)
               { }
             }
         });
     }
 </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>元素管理</h2></div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllElements" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddElements" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
      <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
      </div>
       <div class="tab-pane active" id="AllElements"></div>
        <div class="tab-pane" id="AddElements">
            <form id="addElements" class="form-horizontal" method="post" action="/ElementsManagement/AddElements">
                <div class="control-group span6 offset2">
                    <label class="control-label">页面元素名称</label>
                    <div class="controls">
                        <input id="elementsName" name="elementsName" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">页面元素编码</label>
                    <div class="controls">
                        <input id="elementsCode" name="elementsCode" type="text" class="input-prepend span4" />
                    </div>
                </div>             
                <div class="control-group span6 offset2">
                    <label class="control-label">初始化状态</label>
                    <div class="controls">
                        <select class="span4" id="elementsInitstatus_id" name="elementsInitstatus_id">
                         <option id="elementsInfo" value="-1"></option>
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">所在页面</label>
                    <div class="controls">
                        <select class="span4">
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                 <label class="control-label">系统ID</label>
                 <div class="controls">
                  <input id="elementsApp_id" name="elementsApp_id" type="text" class="span4"/>
                 </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">排序码</label>
                    <div class="controls">
                        <input id="elementsCode" name="elementsCode" type="text" class="span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea id="elementsRemark" name="elementsRemark" rows="4" cols="5" class="span4"></textarea>
                    </div>
                </div>
                <div class="control-group span6 offset3">
                    <input type="submit" class="btn btn-primary btn-large span4" onclick="" value="添加元素" />
                </div>
            </form>
        </div>
    </div>

</asp:Content>


