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
            alert(appID);
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
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
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
    <% WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)ViewData["appInfo"];%>
    <%WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)ViewData["userInfo"];%>
    <div class="form-horizontal">
    <form id="approvalApply" method="post" action="" class="form-horizontal">
       <table border="1" align="left" width="700">
         <tr>
         <td>系统名称：</td>
         <td><%=m_appsModel.name %></td>
         </tr>
         <tr>
         <td>系统编码：</td>
         <td><%=m_appsModel.code %></td>
         </tr>
          <tr>
         <td>访问链接：</td>
         <td><%=m_appsModel.url %></td>
         </tr>
         <tr>
         <td>系统备注：</td>
         <td><%=m_appsModel.remark %></td>
         </tr>
         <tr>
         <td>系统管理员：</td>
         <td><%=m_userModel.login %></td>
         </tr>
         <tr>
         <td>管理员姓名：</td>
         <td><%=m_userModel.name %></td>
         </tr>
           <tr>
         <td>管理员工号：</td>
         <td><%=m_userModel.employee_no %></td>
         </tr>
         <tr>
         <td>手机号码：</td>
         <td><%=m_userModel.mobile_phone %></td>
         </tr>
         <tr>
         <td>邮件地址：</td>
         <td><%=m_userModel.mail %></td>
         </tr>
         <tr>
         <td>管理员备注：</td>
         <td><%=m_userModel.remark %></td>
         </tr>
         <tr>
         <input id="appID" type="hidden" value="<%=m_appsModel.id %>"/>
          <td colspan="2" align="center"><div id="Div1"  class="btn btn-primary">批准申请</div></td>
         </tr>
        </table>
     </form>
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


