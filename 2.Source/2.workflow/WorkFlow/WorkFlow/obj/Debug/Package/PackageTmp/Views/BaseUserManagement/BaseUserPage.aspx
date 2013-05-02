<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppsPage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#maingrid").ligerGrid({
                columns: [
                { display: '主键', name: 'id', width: 50, type: 'int' },
                { display: '用户名', name: 'login', width: 80 },
                { display: '姓名', name: 'name', width: 80 },
                { display: '员工号', name: 'employee_no', width: 120 },
                { display: '手机号', name: 'mobile_phone', width: 150 },
                { display: '电子邮箱', name: 'mail', width: 150 },
                { display: '备注', name: 'remark', width: 200 },
                { display: '是否管理员', name: 'admin', width: 120,
                    render: function (record, rowindex, value, column) {
                        if (value) {
                            return "<img src='../../images/grid-checkbox-checked.gif' />";
                        }
                        else {
                            return "<img src='../../images/grid-checkbox.gif' />";
                        }
                    }
                },
                { display: '是否有效', name: 'invalid', width: 80,
                    render: function (record, rowindex, value, column) {
                        if (value) {
                            return "<img src='../../images/grid-checkbox-checked.gif' />";
                        }
                        else {
                            return "<img src='../../images/grid-checkbox.gif' />";
                        }
                    }
                },
                { display: '创建者', name: 'created_by', width: 50 }
                ],
                dataAction: 'server',
                width: '100%',
                pageSizeOptions: [5, 10, 15, 20, 25, 50],
                pageSize: 15,
                height: '100%',
                url: '/BaseUserManagement/GetAllBase_UserList',
                rownumbers: true,
                usePager: true
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <ul class="breadcrumb">
            <li><a href="/Home/Index">首页</a> <span class="divider">/</span></li>
            <li><a href="/Home/OrganizationStruct">组织架构</a> <span class="divider">/</span></li>
            <li class="active">用户管理</li>
        </ul>
    </div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li><a href="#queryApps" data-toggle="tab"><i class="icon-search"></i>查看</a></li>
            <li><a href="#addApps" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane" id="addApps">
                <form method="post" action="" class="form-horizontal">
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">登录名：</label>
                        <div class="controls">
                            <input type="text" id="intputLogin" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="inputPassword">密码：</label>
                        <div class="controls">
                            <input type="password" id="inputPassword" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="inputPassword">确认密码：</label>
                        <div class="controls">
                            <input type="password" id="inputPassword2" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">用户姓名：</label>
                        <div class="controls">
                            <input name="realName" type="text" id="Text1" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">工号：</label>
                        <div class="controls">
                            <input name="txtEmployeeNum" type="text" id="txtEmployeeNum" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">手机号：</label>
                        <div class="controls">
                            <input name="txtPhone" type="text" id="txtPhone" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">Email：</label>
                        <div class="controls">
                            <input name="txtEmail" type="text" id="txtEmail" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">是否管理员：</label>
                        <div class="controls">
                             <input type="checkbox" id="is_admin" name="admin1" value="0" />
                             <input type="hidden" name="admin2" id="admin" value="1" />
                             <input type="hidden" name="createdBy" id="createdBy" value="<%=1 %>" />
                             <input type="hidden" name="createdIP" id="createdIP" value="<%=0 %>" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="intputLogin">备注：</label>
                        <div class="controls">
                            <textarea name="address" rows="5"></textarea>
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                            <input type="submit" value="添加" class="btn" /> 
                            <input type="button" value="重置"  class="btn" />
                        </div>
                    </div>
                </form>
            </div>
            <div class="tab-pane active" id="queryApps">
                <div id="maingrid"></div>
            </div>
        </div>
    </div>

</asp:Content>
