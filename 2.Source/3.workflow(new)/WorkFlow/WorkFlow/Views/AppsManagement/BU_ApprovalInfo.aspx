<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 权限管理系统/系统审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
  <% WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)ViewData["appInfo"];%>
  <% WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)ViewData["userInfo"];%>
<table class="table table-bordered  table-condensed table-hover" width="600">
         <tr><th colspan="2"><center>已审批系统详情</center></th></tr>
         <tr>
         <td width="300">系统名称：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_appsModel.name %></td>
         </tr>
         <tr>
         <td width="300">系统编码：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_appsModel.code %></td>
         </tr>
         <tr>
         <td width="300">访问链接：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_appsModel.url %></td>
         </tr>
         <tr>
         <td width="300">系统备注：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_appsModel.remark %></td>
         </tr>
         <tr>
         <td width="300">系统管理员：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_userModel.login %></td>
         </tr>
         <tr>
         <td width="300">管理员姓名：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_userModel.name %></td>
         </tr>
         <tr>
         <td width="300">管理员工号：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_userModel.employee_no %></td>
         </tr>
         <tr>
         <td width="300">手机号码：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_userModel.mobile_phone %></td>
         </tr>
         <tr>
         <td width="300">邮件地址：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_userModel.mail %></td>
         </tr>
         <tr>
         <td width="300">管理员备注：</td>
         <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=m_userModel.remark %></td>
         </tr>        
        </table>
</asp:Content>
