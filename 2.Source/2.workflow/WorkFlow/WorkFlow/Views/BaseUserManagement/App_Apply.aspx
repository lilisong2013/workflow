<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/adminSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    App_Apply
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../bootstrap/js/bootstrap-tab.js" type="text/javascript"></script>
    <script src="../../bootstrap/js/bootstrap-dropdown.js" type="text/javascript"></script>

    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#validgrid").ligerGrid({
                columns: [
                { display: '系统名称', name: 'name', width: 80 },
                { display: '系统编码', name: 'code', width: 80 },
                { display: '访问连接', name: 'url', width: 120 },
                { display: '备注', name: 'remark', width: 200 },
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
                        var html = '<i class="icon-lock"></i><a href="/BaseUserManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/BaseUserManagement/ChangePage?id=' + row.id + '">删除</a>';
                        return html;
                    }
                }
                ],
                dataAction: 'server',
                width: '100%',
                pageSizeOptions: [5, 10, 15, 20, 25, 50],
                pageSize: 15,
                height: '400',
                url: '/BaseUserManagement/GetApps_Apply',
                rownumbers: true,
                usePager: true
            });


        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#invalidTab").click(function () {
                $("#invalidGrid").ligerGrid({
                    columns: [
                    { display: '系统名称', name: 'name', width: 80 },
                    { display: '系统编码', name: 'code', width: 80 },
                    { display: '访问连接', name: 'url', width: 120 },
                    { display: '备注', name: 'remark', width: 200 },
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
                            var html = '<i class="icon-lock"></i><a href="/BaseUserManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/BaseUserManagement/ChangePage?id=' + row.id + '">删除</a>';
                            return html;
                        }
                    }
                    ],
                    dataAction: 'server',
                    width: '100%',
                    pageSizeOptions: [5, 10, 15, 20, 25, 50],
                    pageSize: 15,
                    height: '400',
                    url: '/BaseUserManagement/GetApps_Apply',
                    rownumbers: true,
                    usePager: true
                });
          });

        });

    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--<div id="alertInfo" style="border:1px solid red; width:100%; height:25px;"><label><%=ViewData["DeleteApps"]%></label></div>--%>   
<div class="container">
    <ul class="nav nav-tabs">
            <li class="active"><a id="" href="#Apps_valid" data-toggle="tab">活动的(<%=2 %>)</a></li>
            <li><a id="invalidTab" href="#Apps_Invalid" data-toggle="tab">无效的(<%=9 %>)</a></li>
            <li><a href="#Apps_All" data-toggle="tab">所有的(<%=11 %>)</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="Apps_valid">
                <div id="validgrid">1</div>
            </div>
            <div class="tab-pane" id="Apps_Invalid">
                <div id="invalidGrid">2</div>
            </div>
            <div class="tab-pane" id="Apps_All">
                <div id="allGrid">3</div>
            </div>
        </div>
</div>
</asp:Content>


