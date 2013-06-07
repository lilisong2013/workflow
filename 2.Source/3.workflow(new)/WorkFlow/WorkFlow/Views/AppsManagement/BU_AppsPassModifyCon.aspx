<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
BU_AppsPassModifyCon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

  <%WorkFlow.Base_UserWebService.base_userBLLservice m_base_userBllService = new WorkFlow.Base_UserWebService.base_userBLLservice(); %>
  <%WorkFlow.Base_UserWebService.base_userModel m_base_userModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
  <center><h4><font color="red">欢迎<%=m_base_userModel.login%>!,密码修改成功!</font></h4></center> 
 
 <div class="container">
    <ul class="pager">
       <li class="next">
         <a href="/AppsManagement/BU_AppsPassModify">返回上页&rarr;</a>
       </li>
        <li class="next">
            <a href="/Home/AdminLogin">重新登录 &rarr;</a>
        </li>
    </ul>
</div>
</asp:Content>
