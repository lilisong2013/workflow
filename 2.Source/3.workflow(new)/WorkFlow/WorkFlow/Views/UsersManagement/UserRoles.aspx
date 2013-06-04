<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
UseRolesInfo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
   <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
  
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../../bootstrap/js/jquery-1.9.1.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
 <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/UsersManagement/AppUsers">返回</a></li></ul>
 </div> 
</div>
<div class="container"><h4>用户角色管理&nbsp;
<%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];%>
用户>><%=m_usersModel.login%></h4></div>
<%=m_usersModel.app_id%>
<%=m_usersModel.id %>
 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div>
  <div class="tab-pane">
   <form  method="post" action="/UsersManagement/UserRoles" class="form-horizontal">   
       <div class="control-group span6 offset2">
       <label class="control-label">项目名称：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls"><input id="usersLogin" name="usersLogin" type="text" value="<%= ViewData["usersLogin"] %>" /></div>
      </div>
       <div class="control-group span6 offset2">
        <label class="control-label">角色：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        
        </div>
       </div>          
       <div class="control-group span6 offset3">
       <input type="submit" value="添加" class="btn btn-primary span3" style="background-position:center"/>  
       </div>
   </form>
</div>
</asp:Content>
