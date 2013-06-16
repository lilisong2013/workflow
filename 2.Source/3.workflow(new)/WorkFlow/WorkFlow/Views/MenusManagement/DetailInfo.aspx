﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
DetailInfo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container">
        <div class="row-fluid">
            <ul class="pager"><li class="next"><a href="/MenusManagement/AppMenus">返回</a></li></ul>
        </div>
        <div class="row-fluid"><h2>菜单名称：<%=ViewData["name"]%></h2></div>
        <div class="row-fluid">菜单编码：<%=ViewData["code"]%></div>
        <div class="row-fluid">菜单URL：<%=ViewData["url"]%></div>
        <div class="row-fluid">应用系统ID：<%=ViewData["app_id"]%></div>
        <div class="row-fluid">父菜单ID：<%=ViewData["parent_id"]%></div>
        <div class="row-fluid">备注：<%=ViewData["remark"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["invalid"]%></div>
    
    </div>

</asp:Content>