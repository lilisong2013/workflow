<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
  <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
  <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "步骤节点信息";
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h4>步骤节点添加</h4></div>
 
 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div> 
 <%--<div class="tab-pane">
    <form id="Edit_Node" method="post" action="" class="form-horizontal">
        
        <div class="m-newline offset2">
         <label class="control-label">步骤名称:</label>
         <div class="controls">
            <input id="s_name" name="s_name" type="text" value="<%=ViewData["s_name"]%>"/>
            <input id="s_id" name="s_id" type="hidden" value="<%=ViewData["s_id"]%>"/>
         </div>
        </div>

        <div class="m-newline offset2">
         <label class="control-label">流程名称:</label>
         <div class="controls">
            <input id="f_name" name="f_name" type="text" value="<%=ViewData["f_name"]%>"/>
         </div>
        </div>

        <div class="m-newline offset2">
         <label class="control-label">步骤类型:</label>
         <div class="controls">
            <input id="step_type_name" name="step_type_name" type="text" value="<%=ViewData["step_type_name"]%>"/>
         </div>
        </div>

         <div class="m-newline offset2">
         <label class="control-label">排序码:</label>
         <div class="controls">
            <input id="order_no" name="order_no" type="text" value="<%=ViewData["order_no"]%>"/>
         </div>
        </div>
    </form>
</div>--%>
</asp:Content>
