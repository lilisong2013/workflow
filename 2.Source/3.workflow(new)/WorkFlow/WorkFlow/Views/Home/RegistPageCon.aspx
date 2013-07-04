<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "信息提示";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<center><h4><font color="red">欢迎!注册成功.</font></h4></center>
<div class="container">
    <ul class="pager">
      <li class="next">
            <a href="/Home/Login">退出&nbsp;&gt;&gt;</a>
        </li>

       <li class="next">
         <a href="/Home/RegistPage">&lt;&lt;&nbsp;返回上页</a>
       </li>
      
    </ul>
</div>
</asp:Content>
