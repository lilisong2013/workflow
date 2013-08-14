<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "菜单信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table border="1" align="center" width="600">
   <tr><th colspan="2">菜单详情</th></tr>
   <tr>
   <td>菜单名称：</td>
   <td><%=ViewData["name"]%></td>
   </tr>
   <tr>
   <td>菜单编码：</td>
   <td><%=ViewData["code"]%></td>
   </tr>
   <tr>
   <td>菜单URL：</td>
   <td><%=ViewData["url"]%></td>
   </tr>
   <tr>
   <td>应用系统名称：</td>
   <td><%=ViewData["app_id"]%></td>
   </tr>
   <tr>
   <td>父菜单名称：</td>
   <td><%=ViewData["parent_id1"]%></td>
   </tr>
   <tr>
   <td>备注：</td>
   <td><%=ViewData["remark"]%></td>
   </tr>
   <tr>
   <td>是否有效：</td>
   <td><%=ViewData["invalid"]%></td>
   </tr>
</table>
</asp:Content>
