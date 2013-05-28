<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
EditPage
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
    <script type="text/javascript">
        $(document).ready(function () {
            var i = $("#rolesInvalid").attr("value");
            $("[name = rb]:checkbox").attr("checked", true);
        });
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             var form = $("#Edit_Roles");
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

  <script type="text/javascript">
      $(document).ready(function () {
          BindInvalidName();
          $("#InvalidInfo").html("请选择");
      });
      function BindInvalidName() {
          $.ajax({
              type: "Post",
              contentType: "application/json",
              url: "/RolesManagement/GetInvalidName",
              data: {}, //即使参数为空，也需要设置
              dataType: 'JSON', //返回的类型为XML
              success: function (result, status) {
                  //成功后执行的方法
                  try {
                      if (status == "success") {
                          for (var i = 0; i < result.Total; i++) {
                              $("#InvalidParent").append("<option value='" + result.Rows[i].InvalidID + "'>" + result.Rows[i].InvalidName + "</option>");
                          }
                      }
                  } catch (e) { }
              }
          });
      }
  </script>

   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>角色管理</h2></div>
<div class="container">
 <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/RolesManagement/AppRoles">返回</a></li></ul>
 </div> 
</div>
 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
  </div>  
   <div class="tab-pane">
   <form  id="Edit_Roles" method="post" action="/RolesManagement/EditRoles" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">角色名称：</label>
       <div class="controls">
       <input id="rolesName" name="rolesName" type="text" value="<%=ViewData["rolesName"] %>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">是否有效：</label>
       <div class="controls">
       <select id="InvalidParent" name="InvalidParent" width="140px">
        <option id="InvalidInfo"></option>
       </select>
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</label>
       <div class="controls">  
       <textarea id="rolesRemark" name="rolesRemark" cols="5" rows="4"><%=ViewData["rolesRemark"]%></textarea>      
        <input type="hidden" name="rolesId" id="rolesId" value="<%=ViewData["rolesId"]%>"/>
        <input type="hidden" name="rolesDeleted" id="rolesDeleted" value="<%=ViewData["rolesDeleted"]%>" />
        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  
        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        <input type="hidden" name="rolesApp_id" id="rolesApp_id" value="<%=m_usersModel.app_id %>"/>
        <input type="hidden" name="rolesCreated_at" id="rolesCreated_at" value="<%=t%>" />
        <input type="hidden" name="rolesCreated_by" id="rolesCreated_by" value="<%=m_usersModel.id%>" />       
        <input type="hidden" name="rolesCreated_ip" id="rolesCreated_ip" value="<%= ipAddress %>" />
        <input type="hidden" name="rolesUpdated_at" id="rolesUpdated_at"/>
        <input type="hidden" name="rolesUpdated_by" id="rolesUpdated_by" />
        <input type="hidden" name="rolesUpdated_ip" id="rolesUpdated_ip" />
        </div> 
       </div>
       <div class="control-group span6 offset3">
       <input type="submit" value="修改" class="btn btn-primary span3" />  
       </div>
   </form>
</div>
</asp:Content>
