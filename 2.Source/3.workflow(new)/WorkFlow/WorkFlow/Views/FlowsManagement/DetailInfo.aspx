<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
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
<table class="table table-bordered  table-condensed table-hover" width="600">
  <tr><th colspan="2"><center>流程详情</center></th></tr>
  <tr>
  <td width="300">流程名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsName"]%></td>
  </tr>
  <tr>
  <td width="300">流程备注：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsRemark"]%></td>
  </tr>
  <tr>
  <td width="300">系统名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%= ViewData["flowsApp_id"]%></td>
  </tr>
  <tr>
  <td width="300">是否有效:</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsInvalid"]%></td>
  </tr>
  <tr>
  <td width="300">创建时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsCreated_at"]%></td>
  </tr>
  <tr>
  <td width="300">创建用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%= ViewData["flowsCreated_by"]%></td>
  </tr>
  <tr>
  <td width="300">创建IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsCreated_ip"]%></td>
  </tr>
  <tr>
  <td width="300">更新时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsUpdated_at"]%></td>
  </tr>
  <tr>
  <td width="300">更新用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsUpdated_by"]%></td>
  </tr>
  <tr>
  <td width="300">更新IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["flowsUpdated_ip"]%></td>
  </tr>
</table>

</asp:Content>
