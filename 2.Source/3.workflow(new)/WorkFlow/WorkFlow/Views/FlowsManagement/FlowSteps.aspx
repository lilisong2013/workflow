<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
 
  <link href="../../LigerUI/lib/ligerUI/skins/ligerui-icons.css" rel="Stylesheet" type="text/css"/>
   <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
   <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
   <%--LigerUI ToolBar文件--%>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerFilter.js" type="text/javascript"></script>

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

   <%--顺序隐藏提示信息--%>
    <script type="text/javascript">
       //隐藏提示信息
       $(document).click(function () {
           $("#promptDIV1").removeClass("alert alert-error alert-success");
           $("#promptDIV1").html("");
       });
   </script>
  
   <%--并序隐藏提示信息--%>
    <script type="text/javascript">
       //隐藏提示信息
       $(document).click(function () {
           $("#promptDIV2").removeClass("alert alert-error alert-success");
           $("#promptDIV2").html("");
       });
  </script>

   <%--编辑隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV3").removeClass("alert alert-error alert-success");
            $("#promptDIV3").html("");
        });
    </script>

   <%--获得初始值--%>
    <script type="text/javascript">
        var flowsID;
        $(document).ready(function () {
            flowsID = $("#flowsID").val(); //流程ID

        });
        var stepTotal = 0; //步骤数量
   </script>

   <%--流程列表(后台分页)--%>
    <script type="text/javascript">
        var StepTypeID;
        var FlowTypeID;
        $(document).ready(function () {
            $("#infoTab").click(function () {
                GetStepsList();

            })

            GetStepsList(); //获取步骤列表

            //获取步骤列表
            function GetStepsList() {

                window['s'] = $("#flowstepgrid").ligerGrid({
                    columns: [
                        { display: '步骤ID', name: 's_id', width: 80, align: 'center' },
                        { display: '步骤名称', name: 's_name', width: 150, align: 'center' },
                        { display: '流程名称', name: 'f_name', width: 150, align: 'center' },
                        { display: '步骤类型', name: 'step_type_name', width: 150, align: 'center' },
                        { display: '排序编码', name: 'order_no', width: 150, align: 'center' },
                        { display: '步骤用户', name: 'user_name', width: 150, align: 'center' },
                       ],
                    dataAction: 'server',
                    width: '99%',
                    pageSizeOptions: [5, 10, 15, 20, 25, 50],
                    pageSize: 10,
                    height: '400',
                    rownumbers: true,
                    usePager: true,
                    url: "/StepsManagement/GetFlowStepsList?flowid=" + flowsID
                });
                s.loadData();
            }
        });

      
   </script>

   <%--流程步骤初始化--%>
    <script type="text/javascript">
        var Orderno;
        var RepeatCount;
        var Name;
        var Remark;
        $(document).ready(function () {

            //alert("flowsID:" + flowsID);
            FlowStepInitial(); //流程步骤初始化

            //流程步骤初始化
            function FlowStepInitial() {
                var OrderNo;
                var RepeatCount;
                $.ajax({
                    url: "/FlowsManagement/GetFlowStepsList",
                    type: "POST",
                    dataType: "json",
                    async: false,
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
                                                //alert("step_id:" + dataJson.StepRows[j].step_id);
                                                stepHtmlStr += "<tr><td><div class='myicon-arrow-right'></div></td><td><table width='160px'><tr><td><div class='myicon-man'></div></td></tr><tr><td><a href='#' data-toggle='modal' data-target='#Edit_StepModal' data-backdrop='false' onclick=\"GetIDValue('" + dataJson.StepRows[j].step_id + "')\">" + dataJson.StepRows[j].step_name + "</a></td></tr></table></td></tr>";
                                            }
                                        }

                                        if (dataJson.Order_NoRows[i].stepType_Name == "并行") {
                                            stepHtmlStr += "<tr><td colspan='2'><a  href='#' class='btn btn-primary' data-toggle='modal' data-target='#AddB_StepModal'data-backdrop='false' onclick=\"GetORValue('" + dataJson.Order_NoRows[i].order_no + "','" + dataJson.Order_NoRows[i].repeat_count + "')\">增加并行步骤</a></td></tr>";
                                        }
                                        stepHtmlStr += "</table></td>";
                                        //alert(stepHtmlStr);
                                        $("#AllStepShow").append(stepHtmlStr);
                                    } else {
                                        stepHtmlStr += "<td><table width='160px'>";
                                        // alert(stepHtmlStr);
                                        for (var j = 0; j < dataJson.AllstepCount; j++) {
                                            if (dataJson.Order_NoRows[i].order_no == dataJson.StepRows[j].order_no) {
                                                //alert(datajson.StepRows[j].step_name);
                                                stepHtmlStr += "<tr><td><div class='myicon-man'></div></td></tr><tr><td><a href='#' data-toggle='modal' data-target='#Edit_StepModal' data-backdrop='false' onclick=\"GetIDValue('" + dataJson.StepRows[j].step_id + "')\">" + dataJson.StepRows[j].step_name + "</a></td></tr>";
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


            //添加顺序步骤
            $("#addSave").click(function () {
                if (false) {
                    return false;
                } else {
                    AddStep(); //添加顺序步骤信息
                }
            });

            //添加顺序步骤信息
            function AddStep() {
                var options = {
                    beforeSubmit: step_showRequest, //form提交前的响应的回调函数
                    success: step_showResponse, //form提交响应后执行的回调函数
                    url: "/StepsManagement/AddStep",
                    type: "POST",
                    dataType: "json"
                };
                $("#add_Steps").ajaxForm(options);
            }

            //form提交前的响应的回调函数
            function step_showRequest() {
                var stepName = $("#stepsName").val();
                if (stepName == "") {
                    $("#promptDIV1").removeClass("alert alert-error alert-success");
                    $("#promptDIV1").addClass("alert alert-error");
                    $("#promptDIV1").html("步骤名称不能为空!");
                    return false;
                }

            }

            //form提交响应成功后执行的回调函数
            function step_showResponse(responseText, statusText) {
                //alert(responseText);
                var dataJson = eval("(" + responseText + ")");
                if (dataJson.success) {
                    $("#AllStepShow").html("");

                    FlowStepInitial();
                    show_promptDIV(dataJson); //提示信息
                }
                show_promptDIV(dataJson); //提示信息
            }

            //提示信息
            function show_promptDIV(data) {
                $("#promptDIV1").removeClass("alert alert-error alert-success");
                $("#promptDIV1").addClass(data.css);
                $("#promptDIV1").html(data.message);
            }

            //添加并序步骤
            $("#BaddSave").click(function () {
                if (false) {
                    return false;
                } else {
                    AddBstep(); //添加并序信息
                }
            });
            var Ocount;
            var Rcount;
            //添加并序信息
            function AddBstep() {
                Ocount = Orderno;
                Rcount = RepeatCount;
                var options = {
                    beforeSubmit: Bstep_showRequest, //form提交前的响应的回调函数
                    success: Bstep_showResponse, //form提交响应后执行的回调函数
                    url: "/StepsManagement/AddBStepNodes",
                    type: "POST",
                    dataType: "json",
                    data: { orderno: Ocount, repeatcount: Rcount, flowid: flowsID }
                };
                $("#add_BSteps").ajaxForm(options);
            }

            //form提交前的响应的回调函数
            function Bstep_showRequest() {
                var stepName = $("#BstepsName").val();
                if (stepName == "") {
                    $("#promptDIV2").removeClass("alert alert-error alert-success");
                    $("#promptDIV2").addClass("alert alert-error");
                    $("#promptDIV2").html("步骤名称不能为空!");
                    return false;
                }
            }

            //form提交响应成功后执行的回调函数
            function Bstep_showResponse(responseText, statusText) {
                var dataJson = eval("(" + responseText + ")");
                if (dataJson.success) {
                    $("#AllStepShow").html("");
                    FlowStepInitial();
                    show_promptDIV2(dataJson); //提示信息
                }
                show_promptDIV2(dataJson); //提示信息
            }

            //提示信息
            function show_promptDIV2(data) {
                $("#promptDIV2").removeClass("alert alert-error alert-success");
                $("#promptDIV2").addClass(data.css);
                $("#promptDIV2").html(data.message);
            }


            //编辑步骤信息
            $("#Editsave").click(function () {
                if (false) {
                    return false;
                } else {
                    EditStep(); //编辑步骤
                }
            });

            //编辑步骤
            function EditStep() {
                var options = {
                    beforeSubmit: Estep_showRequest, //form提交前的响应的回调函数
                    success: Estep_showResponse, //form提交响应后执行的回调函数
                    url: "/StepsManagement/EditStep",
                    type: "POST",
                    dataType: "json",
                    data: { flowid: flowsID }
                };
                $("#edit_steps").ajaxForm(options);
            }
            //form提交前的响应的回调函数
            function Estep_showRequest() {
                var stepName = $("#EstepsName").val();
                if (stepName == "") {
                    $("#promptDIV3").removeClass("alert alert-error alert-success");
                    $("#promptDIV3").addClass("alert alert-error");
                    $("#promptDIV3").html("步骤名称不能为空!");
                    return false;
                }
            }
            //form提交响应成功后执行的回调函数
            function Estep_showResponse(responseText, statusText) {
                var dataJson = eval("(" + responseText + ")");
                if (dataJson.success) {
                    $("#AllStepShow").html("");
                    FlowStepInitial();
                    show_promptDIV3(dataJson); //提示信息

                }
                show_promptDIV3(dataJson); //提示信息
              
            }

            //提示信息
            function show_promptDIV3(data) {
                $("#promptDIV3").removeClass("alert alert-error alert-success");
                $("#promptDIV3").addClass(data.css);
                $("#promptDIV3").html(data.message);
            }
        });
   </script>

   <%--删除流程步骤--%>
    <script type="text/javascript">
     //删除流程
     function DeleteFlows() {
         $.ligerDialog.confirm("确定要删除该流程下所有的步骤吗?", function (yes) {
             if (yes) {
                 $.ajax({
                     url: "/StepsManagement/DeleteFlowSteps",
                     type: "POST",
                     dataType: "json",
                     data: { FlowID: flowsID },
                     success: function (responseText, statusText) {

                         var dataJson = eval("(" + responseText + ")");
                        // alert(dataJson.success);
                         if (dataJson.success) {
                             $("#AllStepShow").html("");
                             FlowStepInitial();
                             show_promptDIV(dataJson);
                         }
                         show_DIV(dataJson);
                     }
                 });

                 //删除提示信息
                 function show_DIV(data) {
                     $("#promptDIV").removeClass("alert alert-error alert-success");
                     $("#promptDIV").addClass(data.css);
                     $("#promptDIV").html(data.message);
                 }
             }
         });
     }
 </script>

   <%--流程步骤类型初始化操作--%>
    <script type="text/javascript">
        $(document).ready(function () {
            GetStepTypeName();
            $("#stepsTypeInfo").html("请选择");
        });

        function GetStepTypeName() {
            $.ajax({
                type: "Post",
                contentType: "application/json",
                url: "/StepsManagement/GetStepTypeName",
                data: {},
                dataType: 'JSON',
                success: function (result, status) {
                    try {
                        if (status == "success") {
                            for (var i = 0; i < result.Total; i++) {
                                $("#stepsType").append("<option value='" + result.Rows[i].Flowsteptypeid + "'>" + result.Rows[i].Flowsteptypename + "</option>");
                            }
                        }
                    } catch (e) { }
                }
            });

        }
  </script>

   <%--顺序步骤用户信息初始化--%>
    <script type="text/javascript">
       $(document).ready(function () {

           GetStepUserName();
           $("#stepsUserInfo").html("请选择");
       });
       function GetStepUserName() {
           $.ajax({
               type: "Post",
               contentType: "application/json",
               url: "/StepsManagement/GetStepUserName",
               data: {},
               dataType: 'JSON',
               success: function (result, status) {
                   try {
                       if (status == "success") {
                           for (var i = 0; i < result.Total; i++) {
                               $("#stepsUser").append("<option value='" + result.Rows[i].StepuserID + "'>" + result.Rows[i].StepuserName + "</option>");
                           }
                       }
                   } catch (e) { }
               }
           });

       }
  </script>
   
   <%--并序步骤用户信息初始化--%>
    <script type="text/javascript">
       $(document).ready(function () {

           GetBStepUserName();
           $("#BstepsUserInfo").html("请选择");
       });
       function GetBStepUserName() {
           $.ajax({
               type: "Post",
               contentType: "application/json",
               url: "/StepsManagement/GetStepUserName",
               data: {},
               dataType: 'JSON',
               success: function (result, status) {
                   try {
                       if (status == "success") {
                           for (var i = 0; i < result.Total; i++) {
                               $("#BstepsUser").append("<option value='" + result.Rows[i].StepuserID + "'>" + result.Rows[i].StepuserName + "</option>");
                           }
                       }
                   } catch (e) { }
               }
           });

       }
  </script>
    
   <%--编辑步骤用户信息初始化--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#EstepsUserInfo").html("请选择");
            GetEStepUserName();
 
        });
        function GetEStepUserName() {
            $.ajax({
                type: "Post",
                contentType: "application/json",
                url: "/StepsManagement/GetStepUserName",
                data: {},
                dataType: 'JSON',
                success: function (result, status) {
                    try {
                        if (status == "success") {
                            for (var i = 0; i < result.Total; i++) {
                                $("#EstepsUser").append("<option value='" + result.Rows[i].StepuserID + "'>" + result.Rows[i].StepuserName + "</option>");
                            }
                        }
                    } catch (e) { }
                }
            });

        }
  </script>

  <%--添加并序节点时获取orderno、repeatcount值--%>
   <script type="text/javascript">
        function GetORValue(orderno, repeatcount) {
            Orderno = orderno;
            RepeatCount = repeatcount;

     }
 </script>

   <%--通过step_id获得实体将实体的值赋给编辑框--%>
   <script type="text/javascript">
       function GetIDValue(stepid) {
           $.ajax({
               url: "/StepsManagement/GetStepsModel",
               type: "POST",
               dataType: "json",
               async: false,
               data: { StepID: stepid },
               success: function (responseText, statusText) {
                   // alert("responseText:" + responseText);
                   var dataJson = eval("(" + responseText + ")");
                   Name = dataJson.Name;
                   Remark = dataJson.Remark;
                   $("#EstepsName").val(dataJson.Name);
                   $("#EstepsTypeInfo").val(dataJson.Type);
                   $("#E_stepsRemark").val(dataJson.Remark);
                   $("#EstepID").val(stepid);
                   $("#EstepsUser").val("请选择");
           
               }
           });

       }
   </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 
 <div class="container"><h2>流程步骤管理</h2></div>
 
   <div class="container">
  <%--操作提示DIV--%>
  <div id="promptDIV" class="row"></div>
  </div>
  
   <div class="container" style="margin-top:16px;">
       <ul class="nav nav-tabs">
         <li class="active" id="modifyTab"><a href="#ModifyFlowSteps" data-toggle="tab"><i class="icon-check"></i>维护</a></li>
         <li id="infoTab"><a href="#AllFlowSteps" data-toggle="tab"><i class="icon-plus"></i>全部</a></li>
       </ul>   
    </div>

   <div class="tab-content">
    
    <%--维护操作--%>
    <div class="tab-pane active" id="ModifyFlowSteps">
     <form id="flow_steps" method="post" action="" class="form-horizontal">
 
         <div class="control-group page-header">
           <label class="control-label">流程名称:</label>
            <div class="controls">
               <span class="input-xlarge uneditable-input"><%=ViewData["flowsName"]%></span>
               <input id="flowsID" name="flowsID" type="hidden" value="<%=ViewData["flowsID"]%>"/>
               <a id="addstep" href="#" class="btn btn-primary" data-toggle="modal" data-target="#AddC_StepModal" data-backdrop="false">增加顺序步骤</a>
               <a href="#" class="btn btn-primary" onclick="DeleteFlows()">删除流程步骤</a>
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

    <%--增加顺序步骤的模态对话框--%>
    <form id="add_Steps" method="post" action="" class="form-horizontal">

        <div id="AddC_StepModal" class="modal hide fade">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h3>添加顺序步骤</h3>
            </div>
            <div class="modal-body modal-form">
              <%-- modal内提示信息条--%>
               <div class="container">
               <%--操作提示DIV--%>
               <div id="promptDIV1" class="row" style="width:300px;height:10px;position:absolute;left:140px;top:1px;"></div>
               </div>
               <br />
                <div class="control-group">
                    <div class="input-prepend input-append">
                    <label class="control-label">流程名称:</label>
                        <div class="controls">
                          <input name="flowsName" id="flowsName" type="text" class="span2 uneditable-input" value="<%=ViewData["flowsName"]%>" disabled="disabled"/>
                          <input id="flowsID1" name="flowsID1" type="hidden" value="<%=ViewData["flowsID"]%>"/>
                        </div>
                    </div>
                </div>
                
                <div class="control-group">
                    <div class="input-prepend input-append">
                    <label class="control-label">步骤名称:</label>
                    <div class="controls">
                    <input name="stepsName" id="stepsName" type="text" class="span2"/>
                    </div>                      
                    </div>
                </div>
                
                <div class="control-group">
                    <div class="input-prepend input-append">
                        <label class="control-label">步骤类型:</label>
                        <div class="controls">
                           <select class="span2" id="stepsType" name="stepsType">
                             <option id="stepsTypeInfo"></option>
                           </select>
                        </div>
                    </div>
                </div>
               
                <div class="control-group">
                    <div class="input-prepend input-append">
                       <label class="control-label">步骤用户:</label>
                       <div class="controls">
                       <select class="span2" id="stepsUser" name="stepsUser">
                       <option id="stepsUserInfo"></option>
                       </select>
                       </div>
                    </div>
                </div>
                
                <div class="control-group">
                    <div class="input-prepend input-append">
                        <label class="control-label">备注信息:</label>
                        <div class="controls">
                        <textarea id="stepsRemark" name="stepsRemark" rows="3" cols="5" class="span2"></textarea>
                        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                        <%string dt = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                        <%DateTime t = Convert.ToDateTime(dt);%>
                        <input  type="hidden" id="stepsCreated_at" name="stepsCreated_at" value="<%=t%>"/>
                        </div>                       
                    </div>
                </div>
            
            </div>
           
            <div class="modal-footer">
                <div class="input-append input-prepend">
                   <div class="controls">
                     <input id="addSave" type="submit" class="btn btn-primary" value="确定"/>&nbsp;&nbsp;&nbsp;
                     <input type="reset" class="btn btn-primary" value="重置"/>
                   </div>
                  
                </div>
            </div>

        </div>
    </form>

    <%--增加并行步骤的模态对话框--%>
    <form id="add_BSteps" method="post" action="" class="form-horizontal">
      
      <div id="AddB_StepModal" class="modal hide fade">        
         
            <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3>添加并行步骤</h3>
            </div>

            <div class="modal-body modal-form">
              <div class="container">
               <%--操作提示DIV--%>
               <div id="promptDIV2" class="row" style="width:300px;height:10px;position:absolute;left:140px;top:1px;"></div>
              </div>
              <br />

              <div class="control-group">
                <div class="input-prepend input-append">
                  <label class="control-label">流程名称:</label>
                  <div class="controls">
                   <input name="BflowsName" id="BflowsName" type="text" class="span2 uneditable-input" value="<%=ViewData["flowsName"]%>" disabled="disabled"/>
                   <input id="BflowsID1" name="BflowsID1" type="hidden" value="<%=ViewData["flowsID"]%>"/>
                  </div>
                </div>
              </div>

              <div class="control-group">
                <div class="input-prepend input-append">
                  <label class="control-label">步骤名称:</label>
                  <div class="controls">
                    <input name="BstepsName" id="BstepsName" type="text" class="span2"/>
                  </div>
                </div>
              </div>

              <div class="control-group">
                <div class="input-prepend input-append">
                  <label class="control-label">步骤类型:</label>
                  <div class="controls">
                     <input id="BstepsTypeInfo" name="BstepsTypeInfo" type="text" class="uneditable-input span2" value="并行" disabled="disabled"/>
                  </div>
                </div>
              </div>

              <div class="control-group">
                    <div class="input-prepend input-append">
                       <label class="control-label">步骤用户:</label>
                       <div class="controls">
                       <select class="span2" id="BstepsUser" name="BstepsUser">
                       <option id="BstepsUserInfo"></option>
                       </select>
                       </div>
                    </div>
                </div>

              <div class="control-group">
                    <div class="input-prepend input-append">
                        <label class="control-label">备注信息:</label>
                        <div class="controls">
                        <textarea id="BstepsRemark" name="BstepsRemark" rows="3" cols="5" class="span2"></textarea>
                        
                        <input  type="hidden" id="BstepsCreated_at" name="BstepsCreated_at" value="<%=t%>"/>
                        </div>                       
                    </div>
                </div>
            </div>
           
            <div class="modal-footer">
               <div class="input-append input-prepend">
                  <div class="controls">
                     <input id="BaddSave" type="submit" class="btn btn-primary" value="确定"/>&nbsp;&nbsp;&nbsp;
                     <input type="reset" class="btn btn-primary" value="重置"/>
                  </div>
               </div>
            </div>
         
      </div>
    </form>

    <%--编辑模态对话框--%>
    <form id="edit_steps" class="form-horizontal" method="post" action="">
        <div id="Edit_StepModal" class="modal hide fade">
           
           <div class="modal-header">
             <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
             <h3>编辑步骤信息</h3>
           </div>
           <div class="modal-body modal-form">
              
              <div class="container">
                 <%--操作提示DIV--%>
                 <div id="promptDIV3" class="row" style="width:300px;height:10px;position:absolute;left:140px;top:1px;"></div>
              </div>
              <br />

              <div class="control-group">
                <div class="input-prepend input-append">
                  <label class="control-label">流程名称:</label>
                  <div class="controls">
                    <input name="EflowsName" id="EflowsName" type="text" class="span2 uneditable-input" value="<%=ViewData["flowsName"]%>" disabled="disabled"/>
                    <input id="EstepID" name="EstepID" type="hidden"/>
                  </div>
                </div>
              </div>

              <div class="control-group">
                <div class="input-prepend input-append">
                  <label class="control-label">步骤名称:</label>
                  <div class="controls">
                    <input name="EstepsName" id="EstepsName" type="text" class="span2" />
                  </div>
                </div>
              </div>

              <div class="control-group">
                <div class="input-prepend input-append">
                  <label class="control-label">步骤类型:</label>
                  <div class="controls">
                    <input id="EstepsTypeInfo" name="EstepsTypeInfo" type="text" class="uneditable-input span2" disabled="disabled"/>
                  </div>
                </div>
              </div>

               <div class="control-group">
                    <div class="input-prepend input-append">
                       <label class="control-label">步骤用户:</label>
                       <div class="controls">
                       <select class="span2" id="EstepsUser" name="EstepsUser">
                       <option id="EstepsUserInfo" value="请选择"></option>
                       </select>
                       </div>
                    </div>
                </div>

            <div class="control-group">
              <div class="input-prepend input-append">
                <label class="control-label">备注信息:</label>
                <div class="controls">
                  <textarea id="E_stepsRemark" name="E_stepsRemark" rows="3" cols="5" class="span2"></textarea>
                  <input type="hidden" id="E_stepsCreated_at" name="E_stepsCreated_at" value="<%=t%>"/>
                </div>
              </div>
            </div>

           </div>
           <div class="modal-footer">
            <div class="input-append input-prepend">
                  <div class="controls">
                     <input id="Editsave" type="submit" class="btn btn-primary" value="确定"/>&nbsp;&nbsp;&nbsp;
                     <input type="reset" class="btn btn-primary" value="重置"/>
                  </div>
            </div>
           </div>

        </div>
    </form>

    <%--显示操作--%>
    <div class="tab-pane" id="AllFlowSteps">  
     <div id="flowstepgrid"></div>
    </div>

    </div>

</asp:Content>
