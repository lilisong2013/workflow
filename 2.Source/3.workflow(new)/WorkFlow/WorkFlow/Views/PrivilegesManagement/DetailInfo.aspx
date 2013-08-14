﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "权限信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table border="1" align="center" width="600">
  <tr><th colspan="2">权限列表详情</th></tr>
  <tr>
  <td width="300">权限名称：</td>
  <td width="300"><%=ViewData["name"]%></td>
  </tr>
  <tr>
  <td width="300">权限类型：</td>
  <td width="300"><%=ViewData["privilegetype_id"]%></td>
  </tr>
  <tr>
  <td width="300">权限项目：</td>
  <td width="300"><%=ViewData["privilegeitem_id"]%></td>
  </tr>
  <tr>
  <td width="300">应用系统名称：</td>
  <td width="300"><%=ViewData["app_id"]%></td>
  </tr>
  <tr>
  <td width="300">是否有效：</td>
  <td width="300"><%=ViewData["invalid"]%></td>
  </tr>
  <tr>
  <td width="300">权限备注：</td>
  <td width="300"><%=ViewData["remark"]%></td>
  </tr>
</table>


</asp:Content>
