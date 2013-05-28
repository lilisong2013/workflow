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
             var form = $("#Edit_Users");
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
            BindAdminName();
            $("#usersInfo").html("请选择");
        });
        function BindAdminName() {
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
                                $("#usersAdmin").append("<option value='" + result.Rows[i].InvalidID + "'>" + result.Rows[i].InvalidName + "</option>");
                            }
                        }
                    } catch (e) { }
                }
            });
        }
  </script>

   <script type="text/javascript">
       $(document).ready(function () {
           BindInvalidName();
           $("#invalidInfo").html("请选择");
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
                               $("#usersInvalid").append("<option value='" + result.Rows[i].InvalidID + "'>" + result.Rows[i].InvalidName + "</option>");
                           }
                       }
                   } catch (e) { }
               }
           });
       }
  </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>用户管理</h2></div>
<div class="container">
 <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/UsersManagement/AppUsers">返回</a></li></ul>
 </div> 
</div>
 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div>  
  <div class="tab-pane">
   <form  id="Edit_Users" method="post" action="/UsersManagement/EditUsers" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">登录名称：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls"><input id="usersLogin" name="usersLogin" type="text" value="<%= ViewData["usersLogin"] %>" /></div>
       </div>
       <div class="control-group span6 offset2">
        <label class="control-label">登录密码：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        <input id="usersPassword" name="usersPassword" type="password" value="<%= ViewData["usersPassword"]%>"/> 
        </div>
       </div>     
       <div class="control-group span6 offset2">
       <label class="control-label">用户姓名：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersName" name="usersName" type="text" value="<%= ViewData["usersName"] %>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">工号：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersEmployee_no" name="usersEmployee_no" type="text" value="<%= ViewData["usersEmployee_no"]%>"/> 
       </div>
       </div>     
       <div class="control-group span6 offset2">
       <label class="control-label">手机号码：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">     
       <input id="usersMobile_phone" name="usersMobile_phone" type="text" value="<%= ViewData["usersMobile_phone"]%>" />
       </div> 
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">邮件地址：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersMail" name="usersMail" type="text" value="<%=ViewData["usersMail"]%>"/>
       </div>
       </div>     
       <div class="control-group span6 offset2">
       <label class="control-label">是否管理员：</label>
       <div class="controls">
       <select id="usersAdmin" name="usersAdmin" width="140px">
         <option id="usersInfo"></option>
       </select>      
       </div>
       </div>                             
       <div class="control-group span6 offset2">
       <label class="control-label">是否有效：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <select id="usersInvalid" name="usersInvalid" width="140px">
        <option id="invalidInfo"></option>
       </select>
       </div>                             
       </div>                           
       <div class="control-group span6 offset2">
       <label class="control-label">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：&nbsp;&nbsp;&nbsp;</label> 
       <div class="controls">   
       <textarea id="usersRemark" name="usersRemark" cols="5" rows="4"><%=ViewData["usersRemark"]%></textarea>
        <input type="hidden" name="usersId" id="usersId" value="<%=ViewData["usersId"]%>"/>
        <input type="hidden" name="usersDeleted" id="usersDeleted" value="False" />
        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  
        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        <input type="hidden" name="usersCreated_at" id="usersCreated_at"/>
        <input type="hidden" name="usersCreated_by" id="usersCreated_by"/>       
        <input type="hidden" name="usersCreated_ip" id="usersCreated_ip"/>
        <input type="hidden" name="usersUpdated_at" id="usersUpdated_at" value="<%=t%>"/>
        <input type="hidden" name="usersUpdated_by" id="usersUpdated_by" value="<%=-1%>"/>
        <input type="hidden" name="usersUpdated_ip" id="usersUpdated_ip" value="<%=ipAddress%>"/>
       </div>
       </div>   
       <div class="control-group span6 offset3">
       <input type="submit" value="修改" class="btn btn-primary span3" style="background-position:center"/>  
       </div>
   </form>
</div>
</asp:Content>
