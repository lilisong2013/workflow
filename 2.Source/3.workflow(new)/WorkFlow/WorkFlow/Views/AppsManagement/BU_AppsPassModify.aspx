﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/supersite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
  
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "密码修改";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

     <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("alert alert-error alert-success");
            $("#promptDIV").html("");
        });
    </script>

    <%--提交表单信息--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#saveSubmit").ready(function () {
                if (false) {
                    return false;
                } else {
                    modifyAppsPass(); //修改应用系统超级管理员密码
                }
            });

            //修改应用系统超级管理员密码
            function modifyAppsPass() {
                var options = {
                    beforeSubmit: appsadmin_showRequest, //form提交前的响应回调函数
                    success: appsadmin_showResponse, //form提交响应成功后执行的回调函数
                    url: "/AppsManagement/ModifyAdminPassword",
                    type: "POST",
                    dataType: "json"
                };
                $("#modifyAppsPass").ajaxForm(options);
            }

            //form提交前的响应回调函数
            function appsadmin_showRequest() {
                var oldpassword = $("#oldpassword").val();
                if (oldpassword == "") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert alert-error");
                    $("#promptDIV").html("原密码不能为空!");
                    return false;
                }
            }

            //form提交响应成功后执行的回调函数
            function appsadmin_showResponse(responseText, statusText) {
                var dataJson = eval("(" + responseText + ")");
                show_promptDIV(dataJson);
            }

            //提示信息
            function show_promptDIV(data) {
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(data.css);
                $("#promptDIV").html(data.message);
            }
        });
    </script>

   <%--  <script type="text/javascript">
         $(document).ready(function () {
             var form = $("#modifyAppsPass");
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
    </script>--%>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
       
<div class="container">
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
    <div class="row">
        <h2 class="span4">修改超级管理员密码</h2>
     
    </div>
    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
</div>
    <%--修改超级管理员密码--%>
<div class="container" style="margin-top:16px;">                
        <form  id="modifyAppsPass" method="post" action="" class="form-horizontal">
            <div class="control-group">
                <label class="control-label" for="oldpassword">原密码：</label>
                <div class="controls">
                    <input name="oldpassword" id="oldpassword" type="password" class="span3" placeholder="原密码"/>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="newpassword">新密码：</label>
                <div class="controls">
                    <input name="newpassword" id="newpassword" type="password" class="span3" placeholder="新密码"/>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="newpassword2">确认密码：</label>
                <div class="controls">
                    <input name="newpassword2" id="newpassword2" type="password" class="span3" placeholder="确认密码"/>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <input id="saveSubmit" type="submit" class="btn btn-primary span3" value="修改" />
                </div>
            </div>
        </form>
               
</div>
</asp:Content>
