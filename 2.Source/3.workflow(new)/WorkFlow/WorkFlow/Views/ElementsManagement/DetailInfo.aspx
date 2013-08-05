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
 <div class="container">
       
        <div class="row-fluid"><h2>元素名称：<%=ViewData["elementsName"]%></h2></div>
        <div class="row-fluid">元素编码：<%=ViewData["elementsCode"]%></div>
        <div class="row-fluid">备注：<%=ViewData["elementsRemark"]%></div>
        <div class="row-fluid">初始化状态：<%=ViewData["elementsInitstatus_id"]%></div>
        <div class="row-fluid">排序码：<%=ViewData["elementsSeqno"]%></div>
        <div class="row-fluid">所在页面：<%=ViewData["elementsMenu_id"]%></div>
        <div class="row-fluid">系统ID：<%=ViewData["elementsApp_id"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["elementsInvalid"]%></div>

        <div class="row-fluid">记录创建时间：<%=ViewData["elementsCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%=ViewData["elementsCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["elementsCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["elementsUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["elementsUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["elementsUpdated_ip"]%></div>
    </div>
</asp:Content>
