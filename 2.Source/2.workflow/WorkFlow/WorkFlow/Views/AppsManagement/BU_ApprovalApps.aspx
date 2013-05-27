<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BU_ApprovalApps
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function (result) {
        $("#approvalApply").click(function () {
            var appID = $("#appID").val();
            //alert(appID);
            $.ajax({
                url: "/AppsManagement/ApprovalAppsApply",
                type: "POST",
                dataType: "json",
                data:{id:appID},
                success: showResponse
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
    });
</script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <% workflow.Base_UserWebService.base_userModel m_baseuserModel= (workflow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
    <div class="row">
        <h2 class="span3">系统审批</h2>
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
    <% workflow.AppsWebService.appsModel m_appsModel = (workflow.AppsWebService.appsModel)ViewData["appInfo"];
       workflow.UsersWebService.usersModel m_userModel = (workflow.UsersWebService.usersModel)ViewData["userInfo"];%>
    <div class="form-horizontal">
        <div class="control-group">
            <label class="control-label">系统名称：</label>
            <div class="controls">
                <input id="appID" type="hidden" value="<%=m_appsModel.id %>" />
                <span class="input-xlarge uneditable-input"><%=m_appsModel.name %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">系统编码：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_appsModel.code %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">访问链接：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_appsModel.url %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">系统备注：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_appsModel.remark %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">系统管理员：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_userModel.login %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">管理员姓名：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_userModel.name %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">管理员工号：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_userModel.employee_no %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">手机号码：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_userModel.mobile_phone %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">邮件地址：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_userModel.mail %></span>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">管理员备注：</label>
            <div class="controls">
                <span class="input-xlarge uneditable-input"><%=m_userModel.remark %></span>
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <div id="approvalApply"  class="btn btn-primary">批准申请</div>
            </div>
        </div>
    </div>
</div>
<div class="container">
    <ul class="pager">
        <li class="next">
            <a href="/AppsManagement/ReturnBaseUserApps">返回 &rarr;</a>
        </li>
    </ul>
</div>

</asp:Content>


