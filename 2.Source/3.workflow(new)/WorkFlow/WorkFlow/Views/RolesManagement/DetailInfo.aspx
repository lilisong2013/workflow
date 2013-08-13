<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
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

<table border="1" align="center" width="600">
   <tr>
   <th colspan="2">角色详情</th>
   </tr>
   <tr>
    <td>角色名称：</td>
    <td><%=ViewData["rolesName"]%></td>
   </tr>
    <tr>
    <td>角色备注：</td>
    <td><%=ViewData["rolesRemark"]%></td>
   </tr>
   <tr>
    <td>是否有效：</td>
    <td><%=ViewData["rolesInvalid"]%></td>
   </tr>
    <tr>
    <td>系统名称：</td>
    <td><%=ViewData["rolesApp_id"]%></td>
   </tr>
    <tr>
    <td>记录创建时间：</td>
    <td><%=ViewData["rolesCreated_at"]%></td>
   </tr>
    <tr>
    <td>记录创建用户：</td>
    <td><%=ViewData["rolesCreated_by"]%></td>
   </tr>
    <tr>
    <td>记录创建IP：</td>
    <td><%=ViewData["rolesCreated_ip"]%></td>
   </tr>
    <tr>
    <td>记录更新时间：</td>
    <td><%=ViewData["rolesUpdated_at"]%></td>
   </tr>
   <tr>
    <td>记录更新用户：</td>
    <td><%=ViewData["rolesUpdated_by"]%></td>
   </tr>
    <tr>
    <td>记录更新IP：</td>
    <td><%=ViewData["rolesUpdated_ip"]%></td>
   </tr>

</table>


</asp:Content>
