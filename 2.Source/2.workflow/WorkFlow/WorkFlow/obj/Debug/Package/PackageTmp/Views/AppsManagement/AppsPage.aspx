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
                { display: '记录ID', name: 'id', width: 50, type: 'int' },
                { display: '系统名称', name: 'name', width: 80 },
                { display: '系统编码', name: 'code', width: 80 },
                { display: '访问连接', name: 'url', width: 120 },
                { display: '备注', name: 'remark', width: 200 },
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
                url: '/AppsManagement/GetAllAppsList',
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
            <li class="active">系统管理</li>
        </ul>
    </div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li><a href="#queryApps" data-toggle="tab"><i class="icon-search"></i>查看</a></li>
            <li><a href="#addApps" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane" id="addApps">
                <form method="post" action="/AppsManagement/AddApps" class="form-horizontal">
                    <div class="control-group">
                        <label class="control-label" for="appsName">系统名称：</label>
                        <div class="controls">
                            <input type="text" name="appsName" id="appsName" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="appsCode">系统编码：</label>
                        <div class="controls">
                            <input type="text" name="appsCode" id="Text1" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="appsURL">访问连接：</label>
                        <div class="controls">
                            <input type="text" name="appsURL" id="appsURL" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="appsRemark">备注：</label>
                        <div class="controls">
                            <textarea name="appsRemark" id="appsRemark" rows="5" cols="5"></textarea>
                            <input type="hidden" name="createdBy" id="createdBy" value="<%=1 %>" />
                            <% string ipAddress = Saron.Common.PubFun.IPHelper.GetClientIP(); %>
                            <input type="hidden" name="createdIP" id="createdIP" value="<%= ipAddress %>" />
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                            <input type="submit" value="添加" class="btn" /> 
                            &nbsp;&nbsp;&nbsp;
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
