<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
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
  <td>权限名称：</td>
  <td><%=ViewData["name"]%></td>
  </tr>
  <tr>
  <td>权限类型：</td>
  <td><%=ViewData["privilegetype_id"]%></td>
  </tr>
  <tr>
  <td>权限项目：</td>
  <td><%=ViewData["privilegeitem_id"]%></td>
  </tr>
  <tr>
  <td>应用系统名称：</td>
  <td><%=ViewData["app_id"]%></td>
  </tr>
  <tr>
  <td>是否有效：</td>
  <td><%=ViewData["invalid"]%></td>
  </tr>
  <tr>
  <td>权限备注：</td>
  <td><%=ViewData["remark"]%></td>
  </tr>
</table>
<%--<div class="container">
      
        <div class="row-fluid"><h2>权限名称：<%=ViewData["name"]%></h2></div>
        <div class="row-fluid">权限类型：<%=ViewData["privilegetype_id"]%></div>
        <div class="row-fluid">权限项目：<%=ViewData["privilegeitem_id"]%></div>      
        <div class="row-fluid">应用系统名称：<%=ViewData["app_id"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["invalid"]%></div>
        <div class="row-fluid">权限备注：<%=ViewData["remark"]%></div>
    </div>--%>

</asp:Content>
