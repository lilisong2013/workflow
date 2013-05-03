<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/adminSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    App_Apply
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
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
                height: '95%',
                url: '/BaseUserManagement/GetApps_Apply',
                rownumbers: true,
                usePager: true
            });
        });
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<div class="container">
    <div id="maingrid"></div>
</div>
<div id="changeInfo" class="container">
    <div>
        <ul class="pager">
        <% WorkFlow.AppsWebService.appsBLLservice m_apps = new WorkFlow.AppsWebService.appsBLLservice();
           int m_appCount = m_apps.GetRecordCount("invalid=1");
           if (m_appCount == 0)
           { %>
                <li class="disabled"><a href="#">无数据</a></li>
        <% }
           else
           { %>
                <li class="next"><a href="#">后一条</a></li>
        <% } %>
        </ul>
    </div>
</div>

</asp:Content>


