﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/loginform.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "超级管理员登录";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/Home/AdminLoginValidation",
                type: "POST",
                dataType: "json"
            };

            $("#submit").click(function () {
                if ($.trim($("#loginName").val()).length == 0 || $.trim($("#loginPassword").val()).length == 0) {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert alert-error");
                    $("#promptDIV").html("用户名或密码不能为空！");
                    return false;
                } else {
                    $("#adminLogin").ajaxForm(options);
                }
            });
        });

        function showResponse(responseText, statusText) {
            //成功后执行的方法
            //alert(responseText.Id + responseText.Name);
            $("#promptDIV").removeClass("alert alert-error alert-success");
            $("#promptDIV").addClass(responseText.css);
            $("#promptDIV").html(responseText.message);

            if (responseText.success) {
                location.href = responseText.toUrl;
            }
        } 
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container">
        <div class="loginform">
            <div class="row">
                <div class="loginleft">
                    <form id="adminLogin" class="" method="post" action="">
                        <h2>超级管理员登录</h2>

                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on span1">@<i class="icon-user"></i></span>
                                <input type="text" id="loginName" name="loginName" class="span2" placeholder="登录名称"/>
                            </div>
                        </div>

                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on span1">@<i class="icon-lock"></i></span>
                                <input type="password" id="loginPassword" name="loginPassword" class="span2" placeholder="登录密码"/>
                            </div>
                        </div>

                        <div class="controls">
                        <center><input id="submit" type="submit" class="btn btn-primary" value="登录" /></center>
                        </div>
                    </form>
                </div>
                <div class="loginright">
                    <h2>超级管理员</h2>
                    <p>审批系统管理员的使用申请！</p>
                </div>
            </div>
        </div>
    </div>
    <div id="footer" class="navbar-fixed-bottom">
        <div class="container">
            <p class="navbar-text span3 offset3">@山东三龙智能技术有限公司</p>
        </div>
    </div>    

</asp:Content>


