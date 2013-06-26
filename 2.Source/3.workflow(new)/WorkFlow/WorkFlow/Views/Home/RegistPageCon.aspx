<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
RegistPageCon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<center><h4><font color="red">欢迎!,注册成功.</font></h4></center>
<div class="container">
    <ul class="pager">
       <li class="next">
         <a href="/Home/RegistPage">&rarr;返回上页</a>
       </li>
        <li class="next">
            <a href="/Home/Login">&rarr;退出</a>
        </li>
    </ul>
</div>
</asp:Content>
