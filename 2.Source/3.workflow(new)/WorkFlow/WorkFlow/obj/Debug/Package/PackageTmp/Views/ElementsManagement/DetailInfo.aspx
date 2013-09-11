<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "元素信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table class="table table-bordered  table-condensed table-hover" width="600">
  <tr><th colspan="2"><center>元素详情</center></th></tr>
  <tr>
  <td width="300">元素名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsName"]%></td>
  </tr>
  <tr>
  <td width="300">元素编码：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsCode"]%></td>
  </tr>
  <tr>
  <td width="300">备注信息：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsRemark"]%></td>
  </tr>
  <tr>
  <td width="300">初始化状态：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsInitstatus_id"]%></td>
  </tr>
  <tr>
  <td width="300">排序编码：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsSeqno"]%></td>
  </tr>
  <tr>
  <td width="300">所在页面：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsMenu_id"]%></td>
  </tr>
  <tr>
  <td width="300">系统名称：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsApp_id"]%></td>
  </tr>

  <tr>
  <td width="300">创建时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsCreated_at"]%></td>
  </tr>
  <tr>
  <td width="300">创建用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsCreated_by"]%></td>
  </tr>
  <tr>
  <td width="300">创建IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsCreated_ip"]%></td>
  </tr>
  <tr>
  <td width="300">更新时间：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsUpdated_at"]%></td>
  </tr>
  <tr>
  <td width="300">更新用户：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsUpdated_by"]%></td>
  </tr>
  <tr>
  <td width="300">更新IP：</td>
  <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["elementsUpdated_ip"]%></td>
  </tr>
</table>


</asp:Content>
