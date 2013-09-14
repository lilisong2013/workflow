<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "步骤信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<table class="table table-bordered  table-condensed table-hover" width="600">
  <tr><th colspan="2"><center>步骤详情</center></th></tr>
  <tr>
  <td width="300">步骤名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["s_name"]%></td>
  </tr>
  <tr>
  <td width="300">流程名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["f_name"]%></td>
  </tr>
  <tr>
  <td width="300">步骤类型：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["step_type_name"]%></td>
  </tr>
  <tr>
  <td width="300">排序编码：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["order_no"]%></td>
  </tr>
  <tr>
  <td width="300">系统名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["appsName"]%></td>
  </tr>
  <tr>
  <td width="300">创建时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"></td>
  </tr>
  <tr>
  <td width="300">创建用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"></td>
  </tr>
  <tr>
  <td width="300">创建IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"></td>
  </tr>
  <tr>
  <td width="300">更新时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"></td>
  </tr>
  <tr>
  <td width="300">更新用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"></td>
  </tr>
  <tr>
  <td width="300">更新IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"></td>
  </tr>
</table>

</asp:Content>
