<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
RegistPageCon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<%WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new WorkFlow.UsersWebService.usersBLLservice(); %>
<%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];%>
<center><h4><font color="red">欢迎<%=m_usersModel.login%>!,注册成功.</font></h4></center>
<div class="container">
    <ul class="pager">
       <li class="next">
         <a href="/Home/RegistPage">返回上页&rarr;</a>
       </li>
        <li class="next">
            <a href="/Home/Login">退出&rarr;</a>
        </li>
    </ul>
</div>
</asp:Content>
