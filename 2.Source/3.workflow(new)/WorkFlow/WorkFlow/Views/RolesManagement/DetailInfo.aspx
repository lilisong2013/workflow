﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "角色信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container">
    
        <div class="row-fluid"><h2>角色名称：<%=ViewData["rolesName"]%></h2></div>
        <div class="row-fluid">角色备注：<%=ViewData["rolesRemark"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["rolesInvalid"]%></div>    
        <div class="row-fluid">系统名称：<%=ViewData["rolesApp_id"]%></div>
        <div class="row-fluid">记录创建时间：<%=ViewData["rolesCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%=ViewData["rolesCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["rolesCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["rolesUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["rolesUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["rolesUpdated_ip"]%></div>
    </div>

</asp:Content>
