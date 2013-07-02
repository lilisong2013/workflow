<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    权限管理系统/操作信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container">
        <div class="row-fluid">
            <ul class="pager"><li class="next"><a href="/OperationsManagement/AppOperations">返回</a></li></ul>
        </div>
        <div class="row-fluid"><h2>操作名称：<%=ViewData["operationsName"]%></h2></div>
        <div class="row-fluid">操作编码：<%=ViewData["operationsCode"]%></div>
        <div class="row-fluid">操作描述：<%=ViewData["operationsDescription"]%></div>
        <div class="row-fluid">备注：<%=ViewData["operationsRemark"]%></div>
        <div class="row-fluid">应用系统id：<%=ViewData["operationsApp_id"]%></div>
        <div class="row-fluid">是否有效：<%=ViewData["operationsInvalid"]%></div>
        <div class="row-fluid">记录删除标志：<%=ViewData["operationsDeleted"]%></div>
        <div class="row-fluid">记录创建时间：<%=ViewData["operationsCreated_at"]%></div>
        <div class="row-fluid">记录创建用户：<%=ViewData["operationsCreated_by"]%></div>
        <div class="row-fluid">记录创建IP：<%=ViewData["operationsCreated_ip"]%></div>
        <div class="row-fluid">记录更新时间：<%=ViewData["operationsUpdated_at"]%></div>
        <div class="row-fluid">记录更新用户：<%=ViewData["operationsUpdated_by"]%></div>
        <div class="row-fluid">记录更新IP：<%=ViewData["operationsUpdated_ip"]%></div>
    </div>

</asp:Content>
