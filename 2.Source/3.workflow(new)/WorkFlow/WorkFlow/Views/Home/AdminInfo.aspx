<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "系统管理员信息修改";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
    
    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            GetAppInfo(); //系统信息初始化
            GetAdminInfo(); //管理员信息初始化

            $("#appSave").click(function () {
                if (false) {
                    return false;
                }
                else {
                    ModifyAppInfo(); //修改系统信息
                }

            });

            $("#adminSave").click(function () {
                if (false) {
                    return false;
                }
                else {
                    ModifyAdminInfo(); //修改系统管理员信息
                }

            });
        });

        //获取管理员信息
        function GetAdminInfo() {
            $.ajax({
                url: "/Home/GetAdminInfo",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert();
                    var adminInfoJson = eval("(" + responseText + ")");
                    $("#adminLogin").val(adminInfoJson.adminLogin);
                    $("#adminName").val(adminInfoJson.adminName);
                    $("#adminEmployeNum").val(adminInfoJson.adminEmployeNum);
                    $("#adminPhone").val(adminInfoJson.adminPhone);
                    $("#adminEmail").val(adminInfoJson.adminEmail);
                    $("#adminRemark").val(adminInfoJson.adminRemark);
                }
            });
        }

        //获取系统信息
        function GetAppInfo() {
            $.ajax({
                url: "/Home/GetAppInfo",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var appInfoJson = eval("(" + responseText + ")");
                    $("#appName").val(appInfoJson.appName);
                    $("#appCode").val(appInfoJson.appCode);
                    $("#appUrl").val(appInfoJson.appUrl);
                    $("#appRemark").val(appInfoJson.appRemark);
                }
            });
        }

        //修改系统信息
        function ModifyAppInfo() {
            var options = {
                beforeSubmit: app_showRequest,  // from提交前的响应的回调函数
                success: app_showResponse,  // form提交响应成功后执行的回调函数
                url: "/Home/ModifyAppInfo",
                type: "POST",
                dataType: "json"
            };

            $("#modifyAppInfo").ajaxForm(options);
        }


        function ModifyAdminInfo() {
            var options = {
                beforeSubmit: admin_showRequest,  // from提交前的响应的回调函数
                success: admin_showResponse,  // form提交响应成功后执行的回调函数
                url: "/Home/ModifyAdminInfo",
                type: "POST",
                dataType: "json"
            };

            $("#modifyAdminInfo").ajaxForm(options);
        }

        //from提交前的响应的回调函数
        function app_showRequest() {
            var appName = $("#appName").val();
            var appCode = $("#appCode").val();
            if (appName == "" || appCode == "") {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass("p-errorDIV");
                $("#promptDIV").html("系统名称或系统编码不能为空！");
                return false;
            }
        }

        //from提交前的响应的回调函数
        function admin_showRequest() {
            alert("aaaaa");
            var adminLogin = $("#adminLogin").val();
            var adminName = $("#adminName").val();
            if (adminLogin == "" || adminName == "") {
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass("p-errorDIV");
                $("#promptDIV").html("系统管理员登录名或姓名不能为空！");
                return false;
            }
        }

        //回调响应
        function app_showResponse(responseText, statusText) {
            //alert(responseText);
            var dataJson = eval("(" + responseText + ")");
            //alert(dataJson.success);
            if (dataJson.success) {
                GetAppInfo(); //重新加载系统信息
            }

            //alert(dataJson);
            show_promptDIV(dataJson); //提示信息
           
        }

        //回调响应
        function admin_showResponse(responseText, statusText) {
            //alert(responseText);
            var dataJson = eval("(" + responseText + ")");
            //alert(dataJson.success);
            if (dataJson.success) {
                GetAdminInfo(); //重新加载系统信息
            }

            //alert(dataJson);
            show_promptDIV(dataJson); //提示信息

        }

        function show_promptDIV(data) {
            //alert(data.message);
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").addClass(data.css);
            $("#promptDIV").html(data.message);
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>信息管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>

    <div class="container" style="margin-top:16px;">
        <div class="tabbable">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#AppInfo" data-toggle="tab">系统信息</a></li>
                <li><a href="#AdminInfo" data-toggle="tab">管理员信息</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="AppInfo">
                    <div class="container">
                        <form id="modifyAppInfo" class="form-horizontal" method="post" action="">
                            <div class="control-group">
                                <label class="control-label">系统名称：</label>
                                <div class="controls">
                                    <input type="text" id="appName" name="appName" placeholder="系统名称" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">系统编码：</label>
                                <div class="controls">
                                    <input type="text" id="appCode" name="appCode" placeholder="系统编码" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">访问地址：</label>
                                <div class="controls">
                                    <input type="text" id="appUrl" name="appUrl" placeholder="访问链接" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">系统备注：</label>
                                <div class="controls">
                                   <textarea id="appRemark" name="appRemark" class="m-textarea" rows="6" cols="" placeholder="系统备注"></textarea>
                                </div>
                            </div>
                            <div class="control-group">
                                <input id="appSave" type="submit" class="btn btn-primary span4" value="保存修改" />
                            </div>
                        </form>
                    </div>
                </div>

                <div class="tab-pane" id="AdminInfo">
                    <div class="container">
                        <form id="modifyAdminInfo" class="form-horizontal" method="post" action="">
                            <div class="control-group">
                                <label class="control-label">登录名：</label>
                                <div class="controls">
                                    <input type="text" id="adminLogin" name="adminLogin" placeholder="登录名" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">姓名：</label>
                                <div class="controls">
                                    <input type="text" id="adminName" name="adminName" placeholder="姓名" />
                                </div>
                            </div> 
                            <div class="control-group">
                                <label class="control-label">工号：</label>
                                <div class="controls">
                                    <input type="text" id="adminEmployeNum" name="adminEmployeNum" placeholder="工号" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">电话：</label>
                                <div class="controls">
                                    <input type="text" id="adminPhone" name="adminPhone" placeholder="电话" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">电子邮件：</label>
                                <div class="controls">
                                    <input type="text" id="adminEmail" name="adminEmail" placeholder="电子邮件" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">备注信息：</label>
                                <div class="controls">
                                    <textarea id="adminRemark" name="adminRemark" class="m-textarea" rows="6" cols="" placeholder="管理员备注"></textarea>
                                </div>
                            </div>
                            <div class="control-group">
                                <input id="adminSave" type="submit" class="btn btn-primary span4" value="保存修改" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


