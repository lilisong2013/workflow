<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "流程步骤编辑";
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

   <%--表单提交信息--%>
   <script type="text/javascript">
       $(document).ready(function () {

           $("#saveSubmit").click(function () {
               if (false) {
                   return false;
               } else {

                   EditSteps();
               }
           });

           function EditSteps() {
               var options = {
                   beforeSubmit: step_showRequest, //form提交前的响应回调函数
                   success: step_showResponse, //form提交成功后执行的回调函数
                   url: "/StepsManagement/EditStep",
                   type: "POST",
                   dataType: "json"
               };
               $("#Edit_Steps").ajaxForm(options);
           }

           //form提交前的响应回调函数
           function step_showRequest() {
               var stepsName = $("#stepsName").val();
               if (stepsName == "") {
                   $("#promptDIV").removeClass("alert alert-error alert-success");
                   $("#promptDIV").addClass("alert alert-error");
                   $("#promptDIV").html("步骤名称不能为空!");
                   return false;
               }
           }
           //form提交成功后执行的回调函数
           function step_showResponse(responseText, statusText) {
               
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

 <div class="container"><h4>流程步骤管理</h4></div>
  <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
  </div> 

  <div class="tab-content">
    <form id="Edit_Steps" method="post" action="" class="form-horizontal">
       
       <div class="m-newline offset1">
         <label class="control-label ">步骤名称:</label>
         <div class="controls">
           <input id="stepsName" name="stepsName" type="text" value="<%=ViewData["s_name"]%>" class="span4" placeholder="步骤名称"/>
           <input id="stepsID" name="stepsID" type="hidden" value="<%=ViewData["s_id"]%>"/>
         </div>
       </div>

       <div class="m-newline offset1">
         <label class="control-label">流程名称:</label>
         <div class="controls">
           <label class="uneditable-input span4"><%=ViewData["s_flow_name"]%></label>
           <input id="s_flow_name" name="s_flow_name" type="hidden" value="<%=ViewData["s_flow_name"]%>"/>
           <input id="s_flow_id" name="s_flow_id" type="hidden" value="<%=ViewData["s_flow_id"]%>"/>
         </div>
       </div>

       <div class="m-newline offset1">
         <label class="control-label">步骤类型:</label>
         <div class="controls">
           <label class="uneditable-input span4"><%=ViewData["s_step_type_name"]%></label>
           <input id="s_step_type_name" name="s_step_type_name" type="hidden" value="<%=ViewData["s_step_type_name"]%>"/>
           <input id="s_step_type_id" name="s_step_type_id" type="hidden" value="<%=ViewData["s_step_type_id"]%>"/>
         </div>
       </div>
       
       <div class="m-newline offset1">
         <label class="control-label ">排序编码:</label>
         <div class="controls">
           <label class="uneditable-input span4"><%=ViewData["s_order_no"]%></label>
           <input id="s_order_no" name="s_order_no" type="hidden" value="<%=ViewData["s_order_no"]%>" />
         </div>
       </div>

       <div class="m-newline offset1">
            <label class="control-label ">重复次数:</label>
            <div class="controls">
            <label class="uneditable-input span4"><%=ViewData["s_repeat_count"]%></label>
            <input id="s_repeat_count" name="s_repeat_count" type="hidden" value="<%=ViewData["s_repeat_count"]%>" class="uneditable-input"/>
            </div>
            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
            <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
            <%DateTime t = Convert.ToDateTime(s); %>  
            <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
           
            <input type="hidden" name="stepsUpdate_at" id="stepsUpdate_at" value="<%=t%>" />
            <input type="hidden" name="stepsUpdate_by" id="stepsUpdate_by" value="<%=m_usersModel.id%>" />       
            <input type="hidden" name="stepsUpdate_ip" id="stepsUpdate_ip" value="<%= ipAddress %>" /> 
        </div>

       <div class="control-group span4 offset1">
       <label class="control-label" for="stepsRemark">备注信息:</label>
       <div class="controls">  
       <textarea id="stepsRemark" name="stepsRemark" cols="2" rows="4" placeholder="备注信息" maxlength="80" class="span4"> <%=ViewData["s_remark"]%></textarea>      
        </div> 
       </div>

       <div class="control-group span8 offset2">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span8 offset2" style="background-position:center"/>  
       </div>

    </form>
  </div>
</asp:Content>
