<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
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
<table class="table table-bordered  table-condensed table-hover" width="600">
  <tr><th colspan="2"><center>操作详情</center></th></tr>
  <tr>
  <td width="300">操作名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsName"]%></td>
  </tr>
  <tr>
  <td width="300">操作编码：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsCode"]%></td>
  </tr>
  <tr>
  <td width="300">操作描述：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsDescription"]%></td>
  </tr>
  <tr>
  <td width="300">备注信息：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsRemark"]%></td>
  </tr>
  <tr>
  <td width="300">系统名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsApp_id"]%></td>
  </tr>

  <tr>
  <td width="300">创建时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsCreated_at"]%></td>
  </tr>
  <tr>
  <td width="300">创建用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsCreated_by"]%></td>
  </tr>
  <tr>
  <td width="300">创建IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsCreated_ip"]%></td>
  </tr>
  <tr>
  <td width="300">更新时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsUpdated_at"]%></td>
  </tr>
  <tr>
  <td width="300">更新用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsUpdated_by"]%></td>
  </tr>
  <tr>
  <td width="300">更新IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["operationsUpdated_ip"]%></td>
  </tr>
</table>


</asp:Content>
