<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    
     <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet" type="text/css" />        
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>

    <%--LigerUI Dialog文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
  
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

  <%--表单添加提交数据--%>
  <script type="text/javascript">
      $(document).ready(function () {
          $("#saveSubmit").click(function () {
              if (false) {
                  return false;
              } else {
                  AddNode(); //添加并行节点
              }
          });

          //添加并行节点
          function AddNode() {
              var options = {
                  beforeSubmit: node_showRequest, //form提交前的响应的回调函数
                  success: node_showResponse, //form提交响应成功后执行的回调函数
                  url: "/StepsManagement/AddStepNodes",
                  type: "POST",
                  dataType: "json"
              };

              $("#edit_Node").ajaxForm(options);
          }

          //form提交前的响应的回调函数
          function node_showRequest() {
              var nodeName = $("#s_name").val();
              if (nodeName == "") {
                  $("#promptDIV").removeClass("alert alert-error alert-success");
                  $("#promptDIV").addClass("alert alert-error");
                  $("#promptDIV").html("步骤名称不能为空!");
                  return false;
              }
          }
          //form提交响应成功后执行的回调函数
          function node_showResponse(responseText, statusText) {
              var dataJson = eval("(" + responseText + ")");
              show_promptDIV(dataJson);
              
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
<div class="container"><h4>步骤节点添加</h4></div>
 
 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div> 

 <div class="tab-pane">
    <form id="edit_Node" method="post" action="" class="form-horizontal">
        
        <div class="m-newline offset2">
         <label class="control-label">步骤名称:</label>
         <div class="controls">
            <input id="s_name" name="s_name" type="text" placeholder="步骤名称" />
            <input id="s_id" name="s_id" type="hidden" value="<%=ViewData["s_id"]%>"/>
         </div>
        </div>

        <div class="m-newline offset2">
         <label class="control-label">流程名称:</label>
         <div class="controls">
            <input id="f_name" name="f_name" class="uneditable-input" value="<%=ViewData["f_name"]%>"/>
           
         </div>
        </div>

        <div class="m-newline offset2">
         <label class="control-label">步骤类型:</label>
         <div class="controls">
            <input id="step_type_name" name="step_type_name" class="uneditable-input" value="<%=ViewData["step_type_name"]%>"/>       
         </div>
        </div>

         <div class="m-newline offset2">
         <label class="control-label">排序码:</label>
         <div class="controls">
            <input id="order_no" name="order_no" class="uneditable-input" value="<%=ViewData["order_no"]%>"/>
         </div>
        </div>

        <div class="control-group span7 offset3" >
        <input id="saveSubmit" type="submit" value="添加" class="btn btn-primary span7 offset3" />  
        </div>  
          
    </form>
</div>

</asp:Content>
