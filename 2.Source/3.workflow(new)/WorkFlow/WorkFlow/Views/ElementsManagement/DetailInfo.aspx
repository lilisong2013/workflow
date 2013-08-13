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
  <td>元素名称：</td>
  <td><%=ViewData["elementsName"]%></td>
  </tr>
  <tr>
  <td>元素编码：</td>
  <td><%=ViewData["elementsCode"]%></td>
  </tr>
  <tr>
  <td>备注：</td>
  <td><%=ViewData["elementsRemark"]%></td>
  </tr>
  <tr>
  <td>初始化状态：</td>
  <td><%=ViewData["elementsInitstatus_id"]%></td>
  </tr>
  <tr>
  <td>排序码：</td>
  <td><%=ViewData["elementsSeqno"]%></td>
  </tr>
  <tr>
  <td>所在页面：</td>
  <td><%=ViewData["elementsMenu_id"]%></td>
  </tr>
  <tr>
  <td>系统名称：</td>
  <td><%=ViewData["elementsApp_id"]%></td>
  </tr>
  <tr>
  <td>是否有效：</td>
  <td><%=ViewData["elementsInvalid"]%></td>
  </tr>
  <tr>
  <td>记录创建时间：</td>
  <td><%=ViewData["elementsCreated_at"]%></td>
  </tr>
  <tr>
  <td>记录创建用户：</td>
  <td><%=ViewData["elementsCreated_by"]%></td>
  </tr>
  <tr>
  <td>记录创建IP：</td>
  <td><%=ViewData["elementsCreated_ip"]%></td>
  </tr>
  <tr>
  <td>记录更新时间：</td>
  <td><%=ViewData["elementsUpdated_at"]%></td>
  </tr>
  <tr>
  <td>记录更新用户：</td>
  <td><%=ViewData["elementsUpdated_by"]%></td>
  </tr>
  <tr>
  <td>记录更新IP：</td>
  <td><%=ViewData["elementsUpdated_ip"]%></td>
  </tr>
</table>

 <%--<div class="container">
       
        <div class="row-fluid"><h2>元素名称：<%=ViewData["elementsName"]%></h2></div>
        <div class="row-fluid">元素编码：<%=ViewData["elementsCode"]%></div>
        <div class="row-fluid">备注：<%=ViewData["elementsRemark"]%></div>
        <div class="row-fluid">初始化状态：<%=ViewData["elementsInitstatus_id"]%></div>
        <div class="row-fluid">排序码：<%=ViewData["elementsSeqno"]%></div>
        <div class="row-fluid">所在页面：<%=ViewData["elementsMenu_id"]%></div>
        <div class="row-fluid">系统名称：<%=ViewData["elementsApp_id"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["elementsInvalid"]%></div>

        <div class="row-fluid">记录创建时间：<%=ViewData["elementsCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%=ViewData["elementsCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["elementsCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["elementsUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["elementsUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["elementsUpdated_ip"]%></div>
    </div>--%>
</asp:Content>
