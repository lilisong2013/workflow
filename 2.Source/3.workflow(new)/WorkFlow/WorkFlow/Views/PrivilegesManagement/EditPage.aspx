﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    权限管理系统/权限编辑
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
 <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
 <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
 <script type="text/javascript">
     var privilegeID;
     $(document).ready(function () {
         privilegeID = $("#privilegeId").val(); //权限ID
         //alert(privilegeID);
     });
    var piTotal = 0;//是否有效数量
 </script>
 <%--是否有效初始化--%>
 <script type="text/javascript">
     $(document).ready(function () {

         $.ajax({
             url: "/PrivilegesManagement/GetInvalidList",
             type: "POST",
             dataType: "json",
             data: { privilegeId: privilegeID },
             success: function (responseText, statusText) {
                 //alert(responseText);
                 var dataJson = eval("(" + responseText + ")");
                 piTotal = parseInt(dataJson.total); //权限有效数量
                 for (var i = 0; i < dataJson.total; i++) {
                     $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                 }
                 for (var i = 0; i < dataJson.total; i++) {
                     if (dataJson.List[i].selected == 'true') {
                         $("#invalidValue" + i.toString()).prop("checked", true);
                        // alert("ok??");
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
        var privilegesStr;
        var privilegeData;
        $("#saveSubmit").click(function () {
         if (false) {
             return false;
         } else {
             privilegesStr = "{"; //JSON数据字符串
             var pvTotal = 0; //权限有效的数量
             //alert(pvTotal);
             //权限"是否有效"中被选中的项
             for (var i = 0; i < 1; i++) {
                 var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                // alert(checkBoxID);
                 if (checkBoxID.is(":checked")) {
                     //alert(checkBoxID.val() + "选中");
                     privilegesStr += "privilegesID" + pvTotal.toString() + ":'" + checkBoxID.val() + "',";
                     pvTotal++;
                 }
                 else { }
             }
             privilegesStr += "pv_Total:'" + pvTotal + "',u_ID:'" + $("#privilegeID").val() + "'}";
             //alert(privilegesStr);
             privilegeData = eval("(" + privilegesStr + ")");
            // alert(privilegeData);
             $("#Edit_Privileges").ajaxForm({
                 success: ri_showResponse, //form提交相应成功后执行的回调函数
                 url: "/PrivilegesManagement/EditPrivileges",
                 type: "POST",
                 dataType: "json",
                 data: privilegeData
             });
         }
     });
     //提交privilege表单后执行的函数
          function ri_showResponse(responseText, statusText) {
               //alert("ok?????");
               $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
               $("#promptDIV").addClass(responseText.css);
               $("#promptDIV").html(responseText.message);
               if (responseText.success) {
                   location.href = responseText.toUrl;
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
<div class="container"><h2>权限管理</h2></div>

<div class="container">
 <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/PrivilegesManagement/AppPrivileges">返回</a></li></ul>
 </div> 
</div>

 <div class="container">
 <%--操作提示DIV--%>
 <div id="promptDIV" class="row"></div>
 </div> 
 <div class="tab-pane">
   <form  id="Edit_Privileges" method="post" action="" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">权限名称：</label>
       <div class="controls">
       <input id="privilegeName" name="privilegeName" type="text" value="<%=ViewData["privilegeName"] %>" />
       <input id="privilegeId" name="privilegeId" type="hidden" value="<%=ViewData["privilegeId"]%>"/>
       </div>
       </div>

       <div class="control-group span6 offset2">
       <label class="control-label">权限类型：</label>
       <div class="controls">
       <label class="uneditable-input" width="200px"><%=ViewData["privilegeType_id"]%></label>       
       </div>
       </div>

       <div class="control-group span6 offset2">
       <label class="control-label">权限项目：</label>
       <div class="controls">
       <input id="privilegeItem_id" name="privilegeItem_id" type="text" value="<%=ViewData["privilegeItem_id"] %>" />   
       <input type="hidden" id="privilegeItem_id1" name="privilegeItem_id1" value="<%=ViewData["privilegeItem_id1"]%>"/>
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
       <textarea id="privilegeRemark" name="privilegeRemark" cols="5" rows="4"><%=ViewData["privilegeRemark"]%></textarea>      

       <input type="hidden" name="privilegeDeleted" id="privilegeDeleted" value="<%=ViewData["privilegeDeleted"]%>" />

        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  

        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        
        <input type="hidden" name="privilegeApp_id" id="privilegeApp_id" value="<%=m_usersModel.app_id %>"/>
        <input type="hidden" name="privilegeUpdated_at" id="privilegeUpdated_at" value="<%=t%>" />
        <input type="hidden" name="privilegeUpdated_by" id="privilegeUpdated_by" value="<%=m_usersModel.id%>" />       
        <input type="hidden" name="privilegeUpdated_ip" id="privilegeUpdated_ip" value="<%=ipAddress%>" /> 
        
        </div> 
       </div>

       <div class="control-group span6 offset3">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span3" />  
       </div>
   </form>
</div>
</asp:Content>