<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">

    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
     <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
   <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
    <%--<link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css"/>--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
     <%--LigerUI ToolBar文件--%>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerFilter.js" type="text/javascript"></script>
   <script src="../../Scripts/ligerGrid.showFilter.js" type="text/javascript"></script>

   <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "流程步骤维护";
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
  
   <%--获得初始值--%>
   <script type="text/javascript">
       var flowsID;
       $(document).ready(function () {
           flowsID = $("#flowsID").val(); //流程ID
           // alert(flowsID);
       });
       var stepTotal = 0; //步骤数量
   </script>
   
  

   <%--流程步骤初始化--%>
   <script type="text/javascript">
       $(document).ready(function () {

           //alert("flowsID:" + flowsID);
           FlowStepInitial(); //流程步骤初始化

           //流程步骤初始化
           function FlowStepInitial() {
               $.ajax({
                   url: "/FlowsManagement/GetFlowStepsList",
                   type: "POST",
                   dataType: "json",
                   data: { flowID: flowsID },
                   success: function (responseText, statusText) {
                       //alert(responseText);
                       var dataJson = eval("(" + responseText + ")");

                       if (dataJson.sessionIsNull) {
                           location.href = "/Home/Login";
                       }
                       if (dataJson.success) {
                           $("#AllStepShow").html("");
                           if (dataJson.stepCount > 0) {
                               //alert(dataJson.AllstepCount);
                               for (var i = 0; i < dataJson.stepCount; i++) {
                                   var stepHtmlStr = "";
                                   //alert(dataJson.StepRows[i].order_no);
                                   if (dataJson.StepRows[i].order_no > 1) {
                                       stepHtmlStr += "<td><table>";
                                       //alert(stepHtmlStr);
                                       for (var j = 0; j < dataJson.AllstepCount; j++) {
                                           if (dataJson.Order_NoRows[i].order_no == dataJson.StepRows[j].order_no) {
                                               //alert(dataJson.StepRows[j].step_name);
                                               stepHtmlStr += "<tr><td><div class='myicon-arrow-right'></div></td><td><table><tr><td class='myicon-man'></td></tr><tr><td>" + dataJson.StepRows[j].step_name + "</td></tr></table></td></tr>";
                                           }
                                       }
                                       stepHtmlStr += "</table></td>";
                                       //alert(stepHtmlStr);
                                       $("#AllStepShow").append(stepHtmlStr);
                                   } else {
                                       stepHtmlStr += "<td><table>";
                                       // alert(stepHtmlStr);
                                       for (var j = 0; j < dataJson.AllstepCount; j++) {
                                           if (dataJson.Order_NoRows[i].order_no == dataJson.StepRows[j].order_no) {
                                               //alert(datajson.StepRows[j].step_name);
                                               stepHtmlStr += "<tr><td class='myicon-man'></td></tr><tr><td>" + dataJson.StepRows[j].step_name + "</td></tr>";
                                           }
                                       }
                                       stepHtmlStr += "</table></td>";
                                       //alert(stepHtmlStr);
                                       $("#AllStepShow").append(stepHtmlStr);
                                   }
                               }
                           }
                       } else {
                           ShowPromptInfoDIV(dataJson);
                       }
                   }
               });
           }

           //显示提示信息
           function ShowPromptInfoDIV(dataJson) {
               $("#PromptInfoDIV").removeClass("alert alert-success alert-error");
               $("#PromptInfoDIV").addClass(dataJson.css);
               $("#PromptInfoDIV").html(dataJson.message);
           }

       });
   </script>

   <%--表单提交操作--%>
   <script type="text/javascript">
       $(document).ready(function () {
           var flow_stepsData;
           var flow_stepsStr = "{";
           $("#saveSubmit").click(function () {
               if (false) {
                   return false;
               } else {
                   flow_stepsStr = "{"; //JSON数据字符串
                   var fsTotal = 0; //流程步骤的数量
                   //alert(fsTotal);
                   //alert("stepTotal:" + stepTotal);
                   //流程步骤被选中的项
                   for (var i = 0; i < stepTotal; i++) {

                       var checkBoxID = $("#flowstep" + i.toString()); //复选框
                       if (checkBoxID.is(":checked")) {
                           //alert(checkBoxID.val() + "选中");
                           // flow_stepsStr += "fstepID" + fsTotal.toString() + ":'" + checkBoxID.val() + "',";
                           flow_stepsStr += "fstepID" + fsTotal.toString() + ":'" + checkBoxID.val() + "',";
                           flow_stepsStr += "valid" + fsTotal.toString() + ":'" + checkBoxID.is(":checked") + "',";
                           fsTotal++;
                       } else {
                           //alert(checkBoxID.val() + "未选中");
                           //flow_stepsStr += "fstepID" + fsTotal.toString() + ":'" + checkBoxID.val() + "',";
                           flow_stepsStr += "fstepID" + fsTotal.toString() + ":'" + checkBoxID.val() + "',";
                           flow_stepsStr += "valid" + fsTotal.toString() + ":'" + checkBoxID.is(":checked") + "',";
                           fsTotal++;
                       }
                   }
                   flow_stepsStr += "fs_total:'" + fsTotal + "',f_ID:'" + $("#flowsID").val() + "'}";
                   //alert("flow_stepsStr:" + flow_stepsStr);
                   flow_stepsData = eval("(" + flow_stepsStr + ")");
                   // alert("flow_stepsData:" + flow_stepsData);

                   $("#flow_steps").ajaxForm({
                       success: fs_showResponse, //form提交响应成功执行的回调函数
                       url: "/FlowsManagement/EditFlowSteps",
                       type: "POST",
                       dataType: "json",
                       data: flow_stepsData
                   });

               }
           });

           //提交flow_steps表单后执行的函数
           function fs_showResponse(responseText, statusText) {

               var dataJson = eval("(" + responseText + ")");
               show_DIV(dataJson);
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
<div class="container"><h2>流程步骤管理</h2></div>
  
  <div class="container">
  <%--操作提示DIV--%>
  <div id="promptDIV" class="row"></div>
  </div>

   
  
    
    <div class="tab-pane active">

     <form id="flow_steps" method="post" action="" class="form-horizontal">
 
         <div class="control-group page-header">
           <label class="control-label">流程名称:</label>
            <div class="controls">
               <span class="input-xlarge uneditable-input"><%=ViewData["flowsName"]%></span>
               <input id="flowsID" name="flowsID" type="hidden" value="<%=ViewData["flowsID"]%>"/>
            </div>
         </div>
   
         <%--步骤显示区--%>
         <div class="container" style="overflow:auto;">
           <table>
             <tr id="AllStepShow"></tr>
           </table>
         </div>
 
     </form>
 
    </div>


 

</asp:Content>
