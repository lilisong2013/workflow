<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
UseRolesInfo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../../bootstrap/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var usersID;
        $(document).ready(function () {
            usersID =$("#usersID").val(); //用户ID
        });
        var epTotal = 0;//角色数量
    </script>
  <%--角色列表初始化--%>
  <script type="text/javascript">
      $(document).ready(function () {
          $.ajax({
              url: "/UsersManagement/GetRolesList",
              type: "POST",
              dataType: "json",
              data: { usersid: usersID },
              success: function (responseText, statusText) {
                 // alert(responseText);
                  var dataJson = eval("(" + responseText + ")");
                  epTotal = parseInt(dataJson.total); //角色数量
                  for (var i = 0; i < dataJson.total; i++) {
                      $("#rolesList").append("<label class='checkbox span2'><input id='rolesprivilege" + i + "' type='checkbox' value='" + dataJson.List[i].id + "'/>" + dataJson.List[i].name + "</label>");
                  }
                 // alert($("#rolesprivilege").val());
                  for (var i = 0; i < dataJson.total; i++) {
                      if (dataJson.List[i].selected == "true") {
                          $("#rolesprivilege" + i.toString()).prop("checked", true);
                      } else {
                          $("#rolesprivilege" + i.toString()).prop("checked", false);
                      }
                  }
              }
          });
      });   
  </script>
 <%--表单提交操作--%>
 <script type="text/javascript">
     $(document).ready(function () {
         var user_rolesData;
         var user_rolesStr = "{";
         $("#saveSubmit").click(function () {
             if (false) {
                 return false;
             } else {
                 for (var i = 0; i < epTotal; i++) {
                     if ($("#rolesprivilege" + i.toString()).attr("checked") == "true") {
                         alert($("#rolesprivilege" + i.toString()).val());
                     } else {
                         alert($("#rolesprivilege" + i.toString()).val());
                     }
                 }
                 user_rolesStr += "ur_total:'3'}";
                 user_rolesData = eval("(" + user_rolesStr + ")");
                 $("#user_roles").ajaxForm({
                     success: ue_showResponse, //form提交响应成功后执行的回调函数
                     url: "/UsersManagement/AddUserRoles",
                     type: "POST",
                     dataType: "json",
                     data: user_rolesData
                 });
             }
         });
         //提交user_roles表单后执行的函数
         function ue_showResponse(responseText, statusText) {
             $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
             $("#promptDIV").addClass(responseText.css);
             $("#promptDIV").html(responseText.message);
         }
     });
 </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>用户角色管理</h2></div>
  <div class="tab-pane">
   <form  id="user_roles" method="post" action="" class="form-horizontal">   
      <div class="control-group page-header">
      <label class="control-label">用户名称：</label>
       <div class="controls">
       <%WorkFlow.UsersWebService.usersModel m_userModel=(WorkFlow.UsersWebService.usersModel)Session["user"]; %>
       <span class="input-xlarge uneditable-input"><%=m_userModel.login%></span>
       <input id="usersID" name="usersID" type="hidden" value="<%=m_userModel.id%>" />
       </div>
      </div>
      <div class="control-group">
       <div class="page-header">
       <h4>角色</h4>
       </div>
       <div id="rolesList">
                   
       </div>
      </div>          
     <div class="control-group">
      <input id="saveSubmit" type="submit" class="btn btn-primary btn-large span3"  value="保存" />
     </div>
   </form>
</div>
</asp:Content>
