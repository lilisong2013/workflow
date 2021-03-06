﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage" %>
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

<table class="table table-bordered  table-condensed table-hover" width="600">
   <tr >
   <th colspan="2"><center>角色详情</center></th>
   </tr>
   <tr>
    <td width="300">角色名称：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesName"]%></td>
   </tr>
    <tr>
    <td width="300">角色备注：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesRemark"]%></td>
   </tr>
   <tr>
    <td width="300">是否有效：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesInvalid"]%></td>
   </tr>
    <tr>
    <td width="300">系统名称：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesApp_id"]%></td>
   </tr>
    <tr>
    <td width="300">创建时间：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesCreated_at"]%></td>
   </tr>
    <tr>
    <td width="300">创建用户：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesCreated_by"]%></td>
   </tr>
    <tr>
    <td width="300">创建IP：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesCreated_ip"]%></td>
   </tr>
    <tr>
    <td width="300">更新时间：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesUpdated_at"]%></td>
   </tr>
   <tr>
    <td width="300">更新用户：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesUpdated_by"]%></td>
   </tr>
    <tr>
    <td width="300">更新IP：</td>
    <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["rolesUpdated_ip"]%></td>
   </tr>

</table>


</asp:Content>
