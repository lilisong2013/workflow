<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
EditPageCon
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
<script type="text/javascript">
    setTimeout("location.href='/RolesManagement/Approles'",3000);
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <center><h4><font color="blue">恭喜你！修改成功</font></h4></center>
 <div class="container">
    <ul class="pager">
       <li class="next">
         <a href="/RolesManagement/Approles">返回上页&rarr;</a>
       </li>
    </ul>
 </div>
 
</asp:Content>
