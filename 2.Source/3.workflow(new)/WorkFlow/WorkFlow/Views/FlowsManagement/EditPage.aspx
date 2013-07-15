<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "流程编辑";
    </script>

    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

     <script type="text/javascript">
         var flowsID;
         $(document).ready(function () {
             flowsID = $("#flowsID").val(); //角色ID
             alert(flowsID);
         });
         var fiTotal = 0; //是否有效数量
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>流程管理</h2></div>
<div class="container">
 <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/FlowsManagement/AppFlows">返回</a></li></ul>
 </div> 
</div>
<div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div> 
<div class="tab-pane">
    <form  id="Edit_Flows" method="post" action="" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">流程名称：</label>
       <div class="controls">
       <input id="flowsName" name="flowsName" type="text" value="<%=ViewData["flowsName"]%>" />
       <input id="flowsID" name="flowsID" type="hidden" value="<%=ViewData["flowsID"]%>"/>
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
       <textarea id="flowsRemark" name="flowsRemark" cols="5" rows="4"><%=ViewData["flowsRemark"]%></textarea>      

        <input type="hidden" name="flowsDeleted" id="flowsDeleted" value="<%=ViewData["flowsDeleted"]%>" />
        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  
        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        <input type="hidden" name="flowsApp_id" id="flowsApp_id" value="<%=m_usersModel.app_id %>"/>
        <input type="hidden" name="flowsCreated_at" id="flowsCreated_at" value="<%=t%>" />
        <input type="hidden" name="flowsCreated_by" id="flowsCreated_by" value="<%=m_usersModel.id%>" />       
        <input type="hidden" name="flowsCreated_ip" id="flowsCreated_ip" value="<%= ipAddress %>" /> 
        </div> 
       </div>
       <div class="control-group span6 offset3">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span3" />  
       </div>
   </form>
</div>
</asp:Content>
