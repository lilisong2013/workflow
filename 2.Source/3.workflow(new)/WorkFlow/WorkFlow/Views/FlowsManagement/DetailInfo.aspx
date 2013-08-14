<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
  <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "流程信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table border="1" width="600" align="center">
  <tr><th colspan="2">流程详情</th></tr>
  <tr>
  <td width="300">流程名称：</td>
  <td width="300"><%=ViewData["flowsName"]%></td>
  </tr>
  <tr>
  <td width="300">流程备注：</td>
  <td width="300"><%=ViewData["flowsRemark"]%></td>
  </tr>
  <tr>
  <td width="300">应用系统名称：</td>
  <td width="300"><%= ViewData["flowsApp_id"]%></td>
  </tr>
  <tr>
  <td width="300">记录创建时间：</td>
  <td width="300"><%=ViewData["flowsCreated_at"]%></td>
  </tr>
  <tr>
  <td width="300">记录创建用户：</td>
  <td width="300"><%= ViewData["flowsCreated_by"]%></td>
  </tr>
  <tr>
  <td width="300">记录创建IP：</td>
  <td width="300"><%=ViewData["flowsCreated_ip"]%></td>
  </tr>
  <tr>
  <td width="300">记录更新时间：</td>
  <td width="300"><%=ViewData["flowsUpdated_at"]%></td>
  </tr>
  <tr>
  <td width="300">记录更新用户：</td>
  <td width="300"><%=ViewData["flowsUpdated_by"]%></td>
  </tr>
  <tr>
  <td width="300">记录更新IP：</td>
  <td width="300"><%=ViewData["flowsUpdated_ip"]%></td>
  </tr>
</table>

</asp:Content>
