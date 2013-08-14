<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
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
<table border="1" width="600" align="center">
  <tr><th colspan="2">元素详情</th></tr>
  <tr>
  <td width="300">元素名称：</td>
  <td width="300"><%=ViewData["elementsName"]%></td>
  </tr>
  <tr>
  <td width="300">元素编码：</td>
  <td width="300"><%=ViewData["elementsCode"]%></td>
  </tr>
  <tr>
  <td width="300">备注：</td>
  <td width="300"><%=ViewData["elementsRemark"]%></td>
  </tr>
  <tr>
  <td width="300">初始化状态：</td>
  <td width="300"><%=ViewData["elementsInitstatus_id"]%></td>
  </tr>
  <tr>
  <td width="300">排序码：</td>
  <td width="300"><%=ViewData["elementsSeqno"]%></td>
  </tr>
  <tr>
  <td width="300">所在页面：</td>
  <td width="300"><%=ViewData["elementsMenu_id"]%></td>
  </tr>
  <tr>
  <td width="300">系统名称：</td>
  <td width="300"><%=ViewData["elementsApp_id"]%></td>
  </tr>
  <tr>
  <td width="300">是否有效：</td>
  <td width="300"><%=ViewData["elementsInvalid"]%></td>
  </tr>
  <tr>
  <td width="300">记录创建时间：</td>
  <td width="300"><%=ViewData["elementsCreated_at"]%></td>
  </tr>
  <tr>
  <td width="300">记录创建用户：</td>
  <td width="300"><%=ViewData["elementsCreated_by"]%></td>
  </tr>
  <tr>
  <td width="300">记录创建IP：</td>
  <td width="300"><%=ViewData["elementsCreated_ip"]%></td>
  </tr>
  <tr>
  <td width="300">记录更新时间：</td>
  <td width="300"><%=ViewData["elementsUpdated_at"]%></td>
  </tr>
  <tr>
  <td width="300">记录更新用户：</td>
  <td width="300"><%=ViewData["elementsUpdated_by"]%></td>
  </tr>
  <tr>
  <td width="300">记录更新IP：</td>
  <td width="300"><%=ViewData["elementsUpdated_ip"]%></td>
  </tr>
</table>


</asp:Content>
