<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "操作信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table border="1" align="center" width="600">
  <tr><th colspan="2">操作详情</th></tr>
  <tr>
  <td>操作名称：</td>
  <td><%=ViewData["operationsName"]%></td>
  </tr>
  <tr>
  <td>操作编码：</td>
  <td><%=ViewData["operationsCode"]%></td>
  </tr>
  <tr>
  <td>操作描述：</td>
  <td><%=ViewData["operationsDescription"]%></td>
  </tr>
  <tr>
  <td>备注：</td>
  <td><%=ViewData["operationsRemark"]%></td>
  </tr>
  <tr>
  <td>应用系统名称：</td>
  <td><%=ViewData["operationsApp_id"]%></td>
  </tr>
  <tr>
  <td>是否有效：</td>
  <td><%=ViewData["operationsInvalid"]%></td>
  </tr>
  <tr>
  <td>记录创建时间：</td>
  <td><%=ViewData["operationsCreated_at"]%></td>
  </tr>
  <tr>
  <td>记录创建用户：</td>
  <td><%=ViewData["operationsCreated_by"]%></td>
  </tr>
  <tr>
  <td>记录创建IP：</td>
  <td><%=ViewData["operationsCreated_ip"]%></td>
  </tr>
  <tr>
  <td>记录更新时间：</td>
  <td><%=ViewData["operationsUpdated_at"]%></td>
  </tr>
  <tr>
  <td>记录更新用户：</td>
  <td><%=ViewData["operationsUpdated_by"]%></td>
  </tr>
  <tr>
  <td>记录更新IP：</td>
  <td><%=ViewData["operationsUpdated_ip"]%></td>
  </tr>
</table>


</asp:Content>
