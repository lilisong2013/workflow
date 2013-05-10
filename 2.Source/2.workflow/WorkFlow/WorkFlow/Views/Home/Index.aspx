<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">

    <script src="../../bootstrap/js/bootstrap-dropdown.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="container">
        <ul class="breadcrumb">
            <li class="active">首页</li>
        </ul>
    </div>

    <div class="container">
    <h2>管理员信息</h2>
    <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
    <label><%=m_usersModel.login %></label>
    <label><%=m_usersModel.name %></label>
    <label><%=m_usersModel.password %></label>
    <label><%=m_usersModel.app_id %></label>
    </div>


</asp:Content>
