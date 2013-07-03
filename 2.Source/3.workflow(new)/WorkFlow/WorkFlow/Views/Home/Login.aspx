<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/loginform.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "登录";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

     <script type="text/javascript">
         $(document).ready(function () {
             var options = {
                 //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                 success: showResponse,  // form提交响应成功后执行的回调函数
                 url: "/Home/LoginValidation",
                 type: "POST",
                 dataType: "json"
             };

             $("#submit").click(function () {
                 if ($.trim($("#loginName").val()).length == 0 || $.trim($("#loginPassword").val()).length == 0) {
                     $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                     $("#promptDIV").addClass("p-errorDIV");
                     $("#promptDIV").html("用户名或密码不能为空！");

                     return false;
                 } else {
                     $("#userLogin").ajaxForm(options);
                 }
             });
         });

         function showResponse(responseText, statusText) {
             //成功后执行的方法
             //alert(responseText.Id + responseText.Name);
             $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
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
                    <form id="userLogin" class="" method="post" action="">
                        <h2>应用系统管理员登录</h2>

                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on span1">@<i class="icon-user"></i></span>
                                <input type="text" id="loginName" name="loginName" class="span2" />
                            </div>
                        </div>

                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on span1">@<i class="icon-lock"></i></span>
                                <input type="password" id="loginPassword" name="loginPassword" class="span2" />
                            </div>
                        </div>

                        <div class="controls">
                            <label class="checkbox">
                                <input type="checkbox" /> 下次自动登录
                            </label>
                            <input id="submit" type="submit" class="btn btn-primary" value="登录" />
                        </div>
                    </form>
                </div>
                <div class="loginright">
                    <h2>注册帐号</h2>
                    <p>应用系统如果想要集成通用权限管理功能，请点击下面的注册按钮！<strong><a href="/Home/AdminLogin">超级管理员</a></strong>审批后方可登录！</p>
                    <p><a href="/Home/RegistPage" class="btn">注册</a></p>
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
