<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

   <%--页面标题--%>
   <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "用户角色";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
   
  <%--隐藏提示信息--%>
  <script type="text/javascript">
       //隐藏提示信息
       $(document).click(function () {
           $("#promptDIV").removeClass("alert alert-error alert-success");
           $("#promptDIV").html("");
       });
    </script>
 
  <script type="text/javascript">
        var usersID;
        $(document).ready(function () {
            usersID = $("#usersID").val(); //用户ID
        });
        var epTotal = 0; //角色数量
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
                  //alert(responseText);
                  var dataJson = eval("(" + responseText + ")");
                  // alert(dataJson);
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
                 user_rolesStr = "{"; //JSON数据字符串
                 var urTotal = 0; //用户角色的数量
                 //alert(urTotal);

                 //用户角色被选中的项
                 for (var i = 0; i < epTotal; i++) {
                     var checkBoxID = $("#rolesprivilege" + i.toString()); //复选框ID
                     // alert(checkBoxID);
                     if (checkBoxID.is(":checked")) {
                         // alert(checkBoxID.val() + "选中");
                         user_rolesStr += "rprivilegeID" + urTotal.toString() + ":'" + checkBoxID.val() + "',";
                         urTotal++;
                     } else {
                         // alert(checkBoxID.val() + "未选中");
                     }
                 }
                 user_rolesStr += "ur_total:'" + urTotal + "',u_ID:'" + $("#usersID").val() + "'}";
                 // alert(user_rolesStr);
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
             var dataJson = eval("(" + responseText + ")");

             show_DIV(dataJson);//提示信息
         }

         //提示信息
         function show_DIV(data) {
             $("#promptDIV").removeClass("alert alert-error alert-success");
             $("#promptDIV").addClass(data.css);
             $("#promptDIV").html(data.message);
         }
     });
 </script>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>用户角色管理</h2></div>

  <div class="container">
  <%--操作提示DIV--%>
  <div id="promptDIV" class="row"></div>
  </div>

  <div class="tab-pane">
   <form  id="user_roles" method="post" action="" class="form-horizontal">   
      <div class="control-group page-header">
      <label class="control-label">用户名称：</label>
       <div class="controls">
       <%WorkFlow.UsersWebService.usersModel m_userModel=(WorkFlow.UsersWebService.usersModel)Session["user"]; %>
       <span class="input-xlarge uneditable-input"><%=ViewData["u_login"]%></span>
       <input id="usersID" name="usersID" type="hidden" value="<%=ViewData["u_ID"]%>" />
       </div>
      </div>
      <div class="control-group">
       <div class="page-header">
       <h4>角色</h4>
       </div>
       <div id="rolesList">
                   
       </div>
      </div>          
     <div class="control-group span6 offset1">
      <input id="saveSubmit" type="submit" class="btn btn-primary span10 offset2"  value="保存" />
     </div>
   </form>
</div>
</asp:Content>
