<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
EditPage
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">  
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

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
                 alert(responseText);
                   var dataJson = eval("(" + responseText + ")");
                  alert(dataJson);
                   riTotal = parseInt(dataJson.total); //用户有效数量
                   for (var i = 0; i < dataJson.total; i++) {
                       $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                   }
                   for (var i = 0; i < dataJson.total; i++) {
                       if (dataJson.List[i].selected == 'true') {
                           $("#invalidValue" + i.toString()).prop("checked", true);
                           //alert("ok??");
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
           $("#saveSubmit").click(function () {
               if (false) {
                   return false;
               } else {
                   rolesStr = "{"; //JSON数据字符串
                   var rvTotal = 0; //角色有效的数量
                  // alert(rvTotal);
                   //角色"是否有效"中被选中的项
                   for (var i = 0; i < 1; i++) {
                       var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                       //alert(checkBoxID);
                       if (checkBoxID.is(":checked")) {
                           //alert(checkBoxID.val() + "选中");
                           rolesStr += "rInvalidID" + rvTotal.toString() + ":'" + checkBoxID.val() + "',";
                           rvTotal++;
                          // checkBoxID.prop("checked", true);
                       } else {
                           //alert(checkBoxID.is(":checked"));
                          // alert(checkBoxID.val() + "未选中");
                          // checkBoxID.prop("checked", false);
                       }
                   }
                   rolesStr += "rv_Total:'" + rvTotal + "',u_ID:'" + $("#rolesID").val() + "'}";
                   //alert(rolesStr);
                   rolesData = eval("(" + rolesStr + ")");
                   //alert(rolesData);
                   $("#Edit_Roles").ajaxForm({
                       success: ri_showResponse, //form提交相应成功后执行的回调函数
                       url: "/RolesManagement/EditRoles",
                       type: "POST",
                       dataType: "json",
                       data: rolesData
                   });
               }
           });
           //提交role表单后执行的函数
           function ri_showResponse(responseText, statusText) {
               //alert("ok?????");
               $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
               $("#promptDIV").addClass(responseText.css);
               $("#promptDIV").html(responseText.message);
               if (result.success) {
                   location.href = result.toUrl;
               }
           }
       });
   </script>
    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
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
   <form  id="Edit_Roles" method="post" action="" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">角色名称：</label>
       <div class="controls">
       <input id="rolesName" name="rolesName" type="text" value="<%=ViewData["rolesName"] %>" />
       <input id="rolesID" name="rolesID" type="hidden" value="<%=ViewData["rolesId"]%>"/>
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">是否有效：</label>
       <div id="invalidList">
       </div>

       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</label>
       <div class="controls">  
       <textarea id="rolesRemark" name="rolesRemark" cols="5" rows="4"><%=ViewData["rolesRemark"]%></textarea>      
<%--        <input type="hidden" name="rolesId" id="rolesId" value="<%=ViewData["rolesId"]%>"/>--%>
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
       <div class="control-group span6 offset3">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span3" />  
       </div>
   </form>
</div>
</asp:Content>
