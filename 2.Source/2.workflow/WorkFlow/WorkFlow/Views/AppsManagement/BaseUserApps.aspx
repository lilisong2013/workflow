﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BaseUserApps
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        var options = {
            //beforeSubmit: showRequest,  // from提交前的响应的回调函数
            success: showResponse,  // form提交响应成功后执行的回调函数
            url: "/AppsManagement/ModifyAdminPassword",
            type: "POST",
            dataType: "json"
        };

        $("#submit").click(function () {
            if ($.trim($("#oldpassword").val()).length == 0 || $.trim($("#newpassword").val()).length == 0) {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass("p-warningDIV");
                $("#promptDIV").html("原密码或新密码不能为空！");

                return false;
            } else {
                $("#modifypassword").ajaxForm(options);
            }
        });
    });

    function showResponse(responseText, statusText) {
        //成功后执行的方法
        //alert(responseText.Id + responseText.Name);
        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
        $("#promptDIV").addClass(responseText.css);
        $("#promptDIV").html(responseText.message);

        return false;
    } 
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var gridID;
            var urlstr;
            var toUrl;
            gridID = $("#validgrid");
            urlstr = "/AppsManagement/AppsValidDSToJSON";
            toUrl = "BU_AppsInfo";
            BindGrid();

            //切换“有效系统”标签
            $("#validTab").click(function () {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").html("");
                gridID = $("#validgrid");
                urlstr = "/AppsManagement/AppsValidDSToJSON";
                toUrl = "BU_AppsInfo";
                BindGrid();
            });

            //切换“系统申请”标签
            $("#invalidTab").click(function () {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").html("");
                gridID = $("#invalidGrid");
                urlstr = "/AppsManagement/AppsInvalidDSToJSON";
                toUrl = "BU_ApprovalApps";
                BindGrid();
            });

            function BindGrid() {
                gridID.ligerGrid({
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
                        var html = '<i class="icon-lock"></i><a href="/AppsManagement/' + toUrl + '?id=' + row.id + '">详情</a>';
                        return html;
                    }
                }
                ],
                    dataAction: 'server',
                    width: '100%',
                    pageSizeOptions: [5, 10, 15, 20, ],
                    pageSize: 15,
                    height: '400',
                    url: urlstr,
                    rownumbers: true,
                    usePager: true
                });
            }
        });

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
    <div class="row">
        <h2 class="span3">系统管理</h2>
        <div class="btn-group pull-right">
            <div class="btn btn-large btn-info">欢迎：<%=m_baseuserModel.login %></div>
            <div class="btn btn-large btn-info dropdown-toggle" data-toggle="dropdown">
                <span class="caret"></span>
            </div>
            <ul class="dropdown-menu">
                <li><a href="/AppsManagement/QuitSys">退出</a></li>
                <li><a href="/AppsManagement/LoginAgain">重新登录</a></li>
            </ul>
        </div>
    </div>
    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
</div>
<div class="container">
    <ul class="nav nav-tabs">
            <li class="active"><a id="validTab" href="#Apps_valid" data-toggle="tab">有效系统</a></li>
            <li><a id="invalidTab" href="#Apps_Invalid" data-toggle="tab">系统申请</a></li>
            <li><a href="#Apps_Password" data-toggle="tab">修改密码</a></li>
     </ul>
        <div class="tab-content">
            <%--系统中运行中的系统--%>
            <div class="tab-pane active" id="Apps_valid">
                <div id="validgrid"><%=m_baseuserModel.password %></div>
            </div>

            <%--审批中的系统申请--%>
            <div class="tab-pane" id="Apps_Invalid">
                <div id="invalidGrid"></div>
            </div>

            <%--修改超级管理员密码--%>
            <div class="tab-pane" id="Apps_Password">
                <div class="container">
                    <form id="modifypassword" method="post" action="" class="form-horizontal">
                        <div class="control-group">
                            <label class="control-label" for="oldpassword">原密码：</label>
                            <div class="controls">
                                <input name="oldpassword" id="oldpassword" type="password" class="span3" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="newpassword">新密码：</label>
                            <div class="controls">
                                <input name="newpassword" id="newpassword" type="password" class="span3" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="newpassword2">确认密码：</label>
                            <div class="controls">
                                <input name="newpassword2" id="newpassword2" type="password" class="span3" />
                            </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <input id="submit" type="submit" class="btn btn-primary span3" value="修改" />
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
</div>

</asp:Content>


