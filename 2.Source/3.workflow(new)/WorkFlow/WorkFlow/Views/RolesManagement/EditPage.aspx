<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">  

    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "角色编辑";
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

   <%--获得角色的ID--%>
   <script type="text/javascript">
        var rolesID;
        $(document).ready(function () {
            rolesID = $("#rolesID").val(); //角色ID
            //alert(rolesID);
        });
        var riTotal = 0; //是否有效数量
    </script>

   <%--是否有效初始化--%>
   <script type="text/javascript">
       $(document).ready(function () {
           $.ajax({
               url: "/RolesManagement/GetInvalidList",
               type: "POST",
               dataType: "json",
               data: { rolesId: rolesID },
               success: function (responseText, statusText) {
                   //alert(responseText);
                   var dataJson = eval("(" + responseText + ")");
                   // alert(dataJson);
                   riTotal = parseInt(dataJson.total); //用户有效数量
                   for (var i = 0; i < dataJson.total; i++) {
                       $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                   }
                   for (var i = 0; i < dataJson.total; i++) {
                       if (dataJson.List[i].selected == 'true') {
                           $("#invalidValue" + i.toString()).prop("checked", true);
                         
                       } else {
                           $("#invalidValue" + i.toString()).prop("checked", false);
                       }
                   }
               }

           });
       });
   </script>

   <%--表单提交数据--%>
   <script type="text/javascript">
       $(document).ready(function () {
           var rolesData;
           var rolesStr;
           var rvTotal = 0;
           $("#saveSubmit").click(function () {
               if (false) {
                   return false;
               } else {
                   rolesStr = "{"; //JSON数据字符串
                   //角色有效的数量                
                   //角色"是否有效"中被选中的项
                   for (var i = 0; i < 1; i++) {
                       var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                       //alert(checkBoxID);
                       if (checkBoxID.is(":checked")) {
                         
                           rolesStr += "rInvalidID" + rvTotal.toString() + ":'" + checkBoxID.val() + "',";
                           rvTotal++;

                       } else {
                           
                       }
                   }
                   rolesStr += "rv_Total:'" + rvTotal + "',u_ID:'" + $("#rolesID").val() + "'}";
                   rolesData = eval("(" + rolesStr + ")");
                 
                   ModifyRole();
               }
           });

           function ModifyRole() {
               var options = {
                   beforeSubmit: role_showRequest, //form提交前的响应回调函数
                   success: role_showResponse, //form提交成功后执行的回调函数
                   url: "/RolesManagement/EditRoles",
                   type: "POST",
                   dataType: "json",
                   data: {rv_Total:rvTotal}
               };
               $("#Edit_Roles").ajaxForm(options);
           }

           //form提交前的响应回调函数
           function role_showRequest() {
               var roleName = $("#rolesName").val();
               if (roleName == "") {
                   $("#promptDIV").removeClass("alert alert-error alert-success");
                   $("#promptDIV").addClass("alert alert-error");
                   $("#promptDIV").html("角色名称不能为空!");
                   return false;
               }
           }

           //form提交成功后执行的回调函数
           function role_showResponse(responseText, statusText) {

               var dataJson = eval("(" + responseText + ")");
               show_promptDIV(dataJson); //提示信息
           }

           //提示信息
           function show_promptDIV(data) {
               $("#promptDIV").removeClass("alert alert-error alert-success");
               $("#promptDIV").addClass(data.css);
               $("#promptDIV").html(data.message);
           }

       });
   </script>
 

   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h4>角色管理</h4></div>

 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
  </div>  
   <div class="tab-pane">
   <form  id="Edit_Roles" method="post" action="" class="form-horizontal"> 
       <div class="m-newline offset2">
       <label class="control-label">角色名称：</label>
       <div class="controls">
       <input id="rolesName" name="rolesName" type="text" value="<%=ViewData["rolesName"] %>" placeholder="角色名称" />
       <input id="rolesID" name="rolesID" type="hidden" value="<%=ViewData["rolesId"]%>"/>
       </div>
       </div>
    
       <div class="m-newline offset2">
       <label class="control-label">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</label>
       <div class="controls">  
       <textarea id="rolesRemark" name="rolesRemark" cols="2" rows="4" placeholder="备注" maxlength="80"><%=ViewData["rolesRemark"]%></textarea>      

        <input type="hidden" name="rolesDeleted" id="rolesDeleted" value="<%=ViewData["rolesDeleted"]%>" />
        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  
        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        <input type="hidden" name="rolesApp_id" id="rolesApp_id" value="<%=m_usersModel.app_id %>"/>
        <input type="hidden" name="rolesCreated_at" id="rolesCreated_at" value="<%=t%>" />
        <input type="hidden" name="rolesCreated_by" id="rolesCreated_by" value="<%=m_usersModel.id%>" />       
        <input type="hidden" name="rolesCreated_ip" id="rolesCreated_ip" value="<%= ipAddress %>" /> 
        </div> 
       </div>

       <div class="m-newline offset2">
       <label class="control-label">是否有效：</label>
       <div id="invalidList">
       </div>

       </div>
       <div class="control-group span10 offset2">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span10 offset2" style="background-position:center"/>  
       </div>
   </form>
</div>
</asp:Content>
