﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    权限管理系统/系统审批
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
   <%-- <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />--%>
    
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "系统审批";
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

  <script type="text/javascript">
    $(document).ready(function () {
        var form = $("#approvalApply");
        form.submit(function () {
            var appID = $("#appID").val();
            alert(appID);
            $.post(form.attr("action"),
                    form.serialize(),
                    function (result, status) {
                        //debugger
                        $("#promptDIV").removeClass("alert alert-error alert-success");
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
    </script>

    <script type="text/javascript">
        $(document).ready(function (result) {
            $("#approvalApply").click(function () {
                var appID = $("#appID").val();
                //alert(appID);
                $.ajax({
                    url: "/AppsManagement/ApprovalAppsApply",
                    type: "POST",
                    dataType: "json",
                    data: { id: appID },
                    success: showResponse
                });
            });

            function showResponse(responseText, statusText) {
                //成功后执行的方法
                //alert(responseText.Id + responseText.Name);
                $("#promptDIV").removeClass("alert alert-error alert-success");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);

                return false;
            }
        });
</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
</div>
<div class="container">
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
    <% WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)ViewData["appInfo"];%>
    <%WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)ViewData["userInfo"];%>
    <div class="form-horizontal">
    <form  method="post" action="" class="form-horizontal">
       <table class="table table-bordered  table-condensed table-hover" width="600">
        <tr><th colspan="2"><center>待审批系统详情</center></th></tr>
         <tr>
         <td width="300">系统名称：</td>
         <td width="300"><%=m_appsModel.name %></td>
         </tr>
         <tr>
         <td width="300">系统编码：</td>
         <td width="300"><%=m_appsModel.code %></td>
         </tr>
         <tr>
         <td width="300">访问链接：</td>
         <td width="300"><%=m_appsModel.url %></td>
         </tr>
         <tr>
         <td width="300">系统备注：</td>
         <td width="300"><%=m_appsModel.remark %></td>
         </tr>
         <tr>
         <td width="300">系统管理员：</td>
         <td width="300"><%=m_userModel.login %></td>
         </tr>
         <tr>
         <td width="300">管理员姓名：</td>
         <td width="300"><%=m_userModel.name %></td>
         </tr>
          <tr>
         <td width="300">管理员工号：</td>
         <td width="300"><%=m_userModel.employee_no %></td>
         </tr>
         <tr>
         <td width="300">手机号码：</td>
         <td width="300"><%=m_userModel.mobile_phone %></td>
         </tr>
         <tr>
         <td width="300">邮件地址：</td>
         <td width="300"><%=m_userModel.mail %></td>
         </tr>
         <tr>
         <td width="300">管理员备注：</td>
         <td width="300"><%=m_userModel.remark %></td>
         </tr>
         <tr>
         <input id="appID" type="hidden" value="<%=m_appsModel.id %>"/>
          <td colspan="2" align="center"><center><input id="approvalApply" class="btn btn-primary btn-info" value="批准申请"/></center></td>
         </tr>
        </table>
     </form>
    </div>
</div>


</asp:Content>


