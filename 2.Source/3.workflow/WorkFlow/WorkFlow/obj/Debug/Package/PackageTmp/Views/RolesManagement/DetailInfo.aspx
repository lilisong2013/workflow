<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
DetailInfo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container">
        <div class="row-fluid">
            <ul class="pager"><li class="next"><a href="/RolesManagement/AppRoles">返回</a></li></ul>
        </div>
        <div class="row-fluid"><h2>角色名称：<%=ViewData["rolesName"]%></h2></div>
        <div class="row-fluid">角色备注：<%=ViewData["rolesRemark"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["rolesInvalid"]%></div>
        <div class="row-fluid">是否删除：<%=ViewData["rolesDeleted"]%></div>
        <div class="row-fluid">系统id：<%=ViewData["rolesApp_id"]%></div>
        <div class="row-fluid">记录创建时间：<%=ViewData["rolesCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%=ViewData["rolesCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["rolesCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["rolesUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["rolesUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["rolesUpdated_ip"]%></div>
    </div>

</asp:Content>
