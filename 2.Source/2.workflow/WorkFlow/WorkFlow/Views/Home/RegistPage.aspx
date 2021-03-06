﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RegistPage
</asp:Content>

<%--子页面的css、js--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .container{ min-width:700px;}
        .control-group .{}
        .m-group-topborder{border-top:1px solid #ccc; padding-top:10px;}
        .m-group-topborder label{ margin-left:20px;}
        .m-newline{ margin-top:10px;}
        .m-newline label{ margin-top:28px;}
        .m-textarea{ width:280px;}

        .pull-right{ margin-right:12px;}

        #userinfo .m-leftform{border-right:1px solid #ccc; width:300px; position:relative; float:left;}
        #userinfo .m-rightform{ position:relative; float:left;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/Home/RegistUser",
                type: "POST",
                dataType: "json"
            };

            $("#submit").click(function () {
                if (false) {

                    return false;
                } else {
                    $("#appsRegist").ajaxForm(options);
                }
            });

            function showResponse(responseText, statusText) {
                //成功后执行的方法
                //alert(responseText.Id + responseText.Name);
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            } 
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    
    <div class="container">
        <ul class="breadcrumb">
            <li class="active"><a href="#">系统管理员注册</a><span class="divider">/</span></li>
            <li><a href="/Home/Login">登录</a></li>
        </ul>
    </div>
    
    <div class="container">
        <form id="appsRegist" class="form-inline" method="post" action="">
        <input type="reset" class="btn btn-primary pull-right" value="重置信息" />
        <input id="submit" type="submit" class="btn btn-primary pull-right" value="提交申请" />
        <div class="control-group" style="border-style:none">
            <h3>系统信息</h3>
            <div class="m-group-topborder">
                <label class="control-label">系统名称：</label>
                <input name="appsName" type="text" class="span2" />
                <label class="control-label">系统编码：</label>
                <input name="appsCode" type="text" class="span2" />
                <label class="control-label">访问链接：</label>
                <input name="appsUrl" type="text" class="span2" />
                <div class="m-newline">
                    <label class="control-label">备注信息：</label>
                    <textarea name="appsRemark" class="m-textarea" rows="5"></textarea>
                </div>
            </div>
        </div>
        <div id="userinfo" class="control-group">
            <h3>用户信息</h3>
            <div class="m-group-topborder">
                <div class="m-leftform">
                    <div class="m-newline">
                        <label class="control-label">登录名称：</label>
                        <input name="userLogin" type="text" class="span2" />
                    </div>
                    <div class="m-newline">
                        <label class="control-label">登录密码：</label>
                        <input name="userPassword" type="password" class="span2" />
                    </div>
                    <div class="m-newline">
                        <label class="control-label">确认密码：</label>
                        <input name="userPassword2" type="password" class="span2" />
                    </div>
                    <div class="m-newline">
                    <label class="control-label">真实姓名：</label>
                    <input name="userName" type="text" class="span2" />
                    </div>
                </div>
                <div class="m-rightform">
                    <div class="m-newline">
                    <label class="control-label">&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;号：&nbsp;&nbsp;</label>
                    <input name="userEmployeeNo" type="text" class="span2" />
                    </div>
                    <div class="m-newline">
                    <label class="control-label">手&nbsp;&nbsp;机&nbsp;&nbsp;号：</label>
                    <input name="userMobilePhone" type="text" class="span2" />
                    </div>
                    <div class="m-newline">
                    <label class="control-label">电子邮箱：</label>
                    <input name="userMail" type="text" class="span2" />
                    </div>
                </div>
                <div class="m-rightform">
                    <div class="m-newline">
                    <label class="control-label">备注信息：</label>
                    <textarea name="userRemark" class="m-textarea" rows="5"></textarea>
                    <% string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                    <input type="hidden" name="createdIP" id="createdIP" value="<%= ipAddress %>" />
                    <% string apply_at = DateTime.Now.ToString(); %>
                    <input type="hidden" name="apply_at" id="apply_at" value="<%= apply_at %>" />
                </div>
                </div>
            </div>
        </div>
        </form>
    </div>

</asp:Content>


