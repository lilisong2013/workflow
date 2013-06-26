<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
DetailInfo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container">
        <div class="row-fluid">
            <ul class="pager"><li class="next"><a href="/UsersManagement/AppUsers">返回</a></li></ul>
        </div>     
        <div class="row-fluid"><h2>登录名称：<%=ViewData["usersLogin"]%></h2></div>
        <div class="row-fluid">用户姓名：<%=ViewData["usersName"]%></div>
        <div class="row-fluid">工号：<%=ViewData["usersEmployee_no"]%></div>
        <div class="row-fluid">手机号码：<%= ViewData["usersMobile_phone"]%></div>
        <div class="row-fluid">邮件地址：<%=ViewData["usersMail"]%></div>
        <div class="row-fluid">备注：<%=ViewData["usersRemark"]%></div>
        <div class="row-fluid">是否管理员：<%= ViewData["usersAdmin"]%></div>
        <div class="row-fluid">是否有效：<%= ViewData["usersInvalid"]%></div>
        <div class="row-fluid">是否删除：<%=ViewData["usersDeleted"]%></div>
        <div class="row-fluid">记录创建时间：<%= ViewData["usersCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%=ViewData["usersCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["usersCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["usersUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["usersUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["usersUpdated_ip"]%></div>
        <div class="row-fluid">系统ID：<%=ViewData["usersApp_id"]%></div>
    </div>

</asp:Content>
