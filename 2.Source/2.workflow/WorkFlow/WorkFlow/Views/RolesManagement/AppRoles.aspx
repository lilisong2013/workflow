<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppRoles
</asp:Content>

<%--本页用到的CSS/JS--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
  
    <script type="text/javascript">
        $(document).ready(function () {
            var form = $("#add_Roles");
            form.submit(function () {
                $.post(form.attr("action"),
                form.serialize(),
                function (result, status) {
                    //debugger;
                    $("#promptDIV").addClass(result.Message);
                },
                "JSON");
                return false;
            });
        });
</script>
<style type="text/css">
    .warningDIV{ border:2px solid #CCCC00; height:28px;
    -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;box-shadow: 0 0 15px #222; }
    .successDIV{ border:2px solid #66CC66; height:28px;
    -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;box-shadow: 0 0 15px #222;}
    .errorDIV{ border:2px solid #CC3333; height:28px;
    -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;box-shadow: 0 0 15px #222;}

</style>

<script type="text/javascript">
    $(document).ready(function () {

    });
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#AllRoles").ligerGrid({
                columns: [
                { display: '角色名称', name: 'name', width: 80 },
                { display: '角色备注', name: 'remark', width: 200 },
                { display: '是否有效', name: 'invalid', width: 80,
                    render: function (record, rowindex, value, column) {
                        if (!value) {
                            return "<img src='../../images/grid-checkbox-checked.gif' />";
                        }
                        else {
                            return "<img src='../../images/grid-checkbox.gif' />";
                        }
                    }
                },
                { display: '', width: 100,
                    render: function (row) {
                        var html = '<i class="icon-lock"></i><a href="/RolesManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/RolesManagement/ChangePage?id=' + row.id + '">删除</a>';
                        return html;
                    }
                }
                ],
                dataAction: 'server',
                width: '100%',
                pageSizeOptions: [5, 10, 15, 20, 25, 50],
                pageSize: 15,
                height: '400',
                url: '/RolesManagement/GetRoles_Apply',
                rownumbers: true,
                usePager: true
            });

        });
    </script>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>角色管理</h2></div>
    <div class="container">
        <div></div>
    </div>
    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllRoles" data-toggle="tab"><i class="icon-check"></i>全部<%=10 %></a></li>
            <li><a href="#AddRoles" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
        <div class="tab-pane active" id="AllRoles"></div>
        <div class="tab-pane" id="AddRoles">
          <form id="add_Roles" class="form-horizontal" method="post" action="/RolesManagement/AddRoles">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesName">角色名称：</label>
                        <div class="controls">
                            <input type="text" name="rolesName" id="rolesName" class="input-prepend span4"/>
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesInvalid">是否有效：</label>
                        <div class="controls">
                            <input type="text" name="rolesInvalid" id="rolesInvalid" class="input-prepend span4" />
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesRemark">备注：</label>
                        <div class="controls">
                            <textarea name="rolesRemark" id="rolesRemark" rows="4" cols="5" class="span4"></textarea>
                            <input type="hidden" name="createdBy" id="createdBy" value="<%=1 %>" />
                            <% string ipAddress = Saron.Common.PubFun.IPHelper.GetClientIP(); %>
                            <input type="hidden" name="createdIP" id="createdIP" value="<%= ipAddress %>" />
                        </div>
                    </div>
                    <div class="control-group span6 offset3">
                        <div class="controls">
                            <input type="submit" value="添加" class="btn" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="button" value="重置"  class="btn" />
                        </div>
                    </div>
                </form>
        </div>
    </div>

</asp:Content>
