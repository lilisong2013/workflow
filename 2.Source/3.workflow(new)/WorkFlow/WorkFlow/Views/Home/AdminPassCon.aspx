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
 <%WorkFlow.UsersWebService.usersBLLservice m_usersBllService=new WorkFlow.UsersWebService.usersBLLservice();%>
 <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"]; %>
 <center><h4><font color="red">欢迎<%=m_usersModel.login%>!密码修改成功!</font></h4></center>
 <div class="container">
    <ul class="pager">

    <li class="next">
            <a href="/Home/Login">&gt;&gt;&nbsp;重新登录</a>
    </li>
       
       <li class="next">
         <a href="/Home/AdminPass">&lt;&lt;&nbsp;返回上页</a>
       </li>
        
    </ul>
</div>
</asp:Content>
