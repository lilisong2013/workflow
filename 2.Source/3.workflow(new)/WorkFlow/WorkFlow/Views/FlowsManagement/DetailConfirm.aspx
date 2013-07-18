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
 <div class="container">
        <div class="row-fluid">         
            <ul class="pager"><li class="next"><a href="/FlowsManagement/AppFlowsPage?pageCount=<%=ViewData["flowsPageCount"]%>">返回</a></li></ul>          
        </div>
        
        <div class="row-fluid"><h2>流程名称：<%=ViewData["flowsName"]%></h2>
        </div>
        <div class="row-fluid">流程备注：<%=ViewData["flowsRemark"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["flowsInvalid"]%></div>
        <div class="row-fluid">是否删除：<%=ViewData["flowsDeleted"]%></div>
        <div class="row-fluid">应用系统id：<%= ViewData["flowsApp_id"]%></div>
        <div class="row-fluid">记录创建时间：<%=ViewData["flowsCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%= ViewData["flowsCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["flowsCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["flowsUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["flowsUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["flowsUpdated_ip"]%></div>
       
  </div>
</asp:Content>
