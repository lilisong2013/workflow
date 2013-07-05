<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/supersite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "系统信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel= (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
    <div class="row">
        <h2 class="span3">系统信息</h2>
  <div class="container">
    <ul class="pager">
        <li class="next">
            <a href="/AppsManagement/ReturnBaseUserApps">&lt;&lt;&nbsp;返回</a>
        </li>
    </ul>
 </div>  
 </div>
</div>
<div class="container">
    <% WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)ViewData["appInfo"];
       WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)ViewData["userInfo"];%>   
    <div class="form-horizontal">
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
        </table>
    </div>
</div>


</asp:Content>


