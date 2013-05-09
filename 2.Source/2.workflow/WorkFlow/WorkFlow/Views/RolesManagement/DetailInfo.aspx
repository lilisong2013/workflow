<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/adminSite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
DetailInfo
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
        <div class="row-fluid">
            <ul class="pager"><li class="next"><a href="/RolesManagement/AppRoles">返回</a></li></ul>
        </div>
        <div class="row-fluid"><h2>角色名称：<%=ViewData["rolesName"] %></h2></div>
        <div class="row-fluid">角色备注：<%=ViewData["rolesRemark"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["rolesInvalid"]%></div>      
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>