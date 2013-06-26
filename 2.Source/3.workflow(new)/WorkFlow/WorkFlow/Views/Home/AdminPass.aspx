<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
AdminPass
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
   <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    
    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            var form = $("#modifyAdminPass");
            form.submit(function () {
                $.post(form.attr("action"),
               form.serialize(),
               function (result, status) {
                   //debugger
                   $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                   $("#promptDIV").addClass(result.css);
                   $("#promptDIV").html(result.message);
                   if (result.success) {
                       location.href = result.toUrl;
                   }
               },
               "JSON");
               return false;
             
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
 <div class="container"><h2 class="span5">应用系统管理员密码修改</h2></div>

 <div class="container">
  <%--操作提示DIV--%>
  <div id="promptDIV" class="row"></div>
 </div>

 <%--修改应用管理员密码--%>
 <div class="container" style="margin-top:16px;">
   <form id="modifyAdminPass" method="post" action="/Home/ModifyAdminPass" class="form-horizontal">
    <div class="control-group">
      <label class="control-label" for="oldpassword">原密码:</label>
      <div class="controls">
        <input name="oldpassword" id="oldpassword" type="password" class="span3"/>
      </div>
    </div>
    <div class="control-group">
      <label class="control-label" for="newpassword">新密码:</label>
      <div class="controls">
         <input name="newpassword" id="newpassword" type="password" class="span3"/>
      </div>
    </div>
    <div class="control-group">
      <label class="control-label" for="newpassword2">确认密码:</label>
      <div class="controls">
       <input name="newpassword2" id="newpassword2" type="password" class="span3"/>
      </div>
    </div>
    <div class="control-group">
      <div class="controls">
        <input type="submit" class="btn btn-primary span3" value="修改"/>
      </div>
    </div>
   </form>
 </div>
</asp:Content>
