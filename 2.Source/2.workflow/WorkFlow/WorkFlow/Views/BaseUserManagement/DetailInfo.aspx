<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/adminSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DetailInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row-fluid">
            <ul class="pager"><li class="next"><a href="/BaseUserManagement/App_Apply">返回</a></li></ul>
        </div>
        <div class="row-fluid"><h2>系统名称：<%=ViewData["appsName"] %></h2></div>
        <div class="row-fluid">系统编码：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">访问链接：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">系统备注：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">系统管理员：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">管理员姓名：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">管理员工号：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">手机号码：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">邮件地址：<%=ViewData["appsName"] %></div>
        <div class="row-fluid">管理员备注：<%=ViewData["appsName"] %></div>
        <div class="row-fluid"><a class="btn btn-primary">激活</a></div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
