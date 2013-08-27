<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
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
 
   <script src="../../Scripts/ligerGrid.showFilter.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "流程步骤管理";
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

   <%--在Grid中分页显示steps信息--%>
   <script type="text/javascript">
       $(document).ready(function () {

           $("#infoTab").click(function () {
               GetStepsList();

           })

           GetStepsList(); //获取步骤列表

           //获取步骤列表
           function GetStepsList() {

               window['s'] = $("#stepsgrid").ligerGrid({
                   columns: [
                        { display: '步骤ID', name: 's_id', width: 80, align: 'center' },
                        { display: '步骤名称', name: 's_name', align: 'center' },
                        { display: '流程名称', name: 'f_name', align: 'center' },
                        { display: '步骤类型', name: 'step_type_name', align: 'center' },
                        { display: '排序码', name: 'order_no', align: 'center' },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.s_id + ')">详情</a>';
                                return html;
                            }
                        },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteStep(' + row.s_id + ')">删除</a>';
                                return html;
                            }
                        }
                       ],
                   dataAction: 'server',
                   width: '99%',
                   pageSizeOptions: [5, 10, 15, 20, 25, 50],
                   pageSize: 10,
                   height: '400',
                   rownumbers: true,
                   usePager: true,
                   url: "/StepsManagement/GetStepsList"
               });
               s.loadData();
           }
       });

      
   </script>

  <%--步骤详情弹出窗--%>
  <script type="text/javascript">
      function DetailDialog(id) {

          if (id) {
              $.ligerDialog.open({
                  title:'详情信息',
                  width:700,
                  height:600,
                  isDrag:true,
                  isResize:true,
                  url: '/StepsManagement/DetailInfo?id='+id
              });        
          }
      }
  </script>

  <%--删除流程步骤--%>
  <script type="text/javascript">
      function DeleteStep(id) {
          var stepid = id;
          $.ligerDialog.confirm("确定要删除步骤吗?", function (yes) {
              if (yes) {
                  $.ajax({
                      url: "/StepsManagement/DeleteFlowStep",
                      type: "POST",
                      dataType: "json",
                      data: {stepID:stepid},
                      success: function (responseText, statusText) {
                          var dataJson = eval("(" + responseText + ")");
                          show_DIV(dataJson);
                          s.loadData();
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

  <%--流程步骤名称初始化操作--%>
  <script type="text/javascript">
      $(document).ready(function () {
          BindFlowName();
          $("#flowsNameInfo").html("请选择");
      });

      function BindFlowName() {
          $.ajax({
              type: "Post",
              contentType: "application/json",
              url: "/StepsManagement/GetFlowName",
              data: {},
              dataType: 'JSON',
              success: function (result, status) {
                  try {
                      if (status == "success") {
                          for (var i = 0; i < result.Total; i++) {
                              $("#flowsName").append("<option value='" + result.Rows[i].FlowsID + "'>" + result.Rows[i].FlowsName + "</option>");
                          }
                      }
                  } catch (e) { }
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

  <%--步骤用户信息初始化--%>
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

  <%--添加步骤信息--%>
  <script type="text/javascript">
      $(document).ready(function () {

          $("#addSave").click(function () {
              if (false) {
                  return false;
              } else {
                  AddStep(); //添加步骤信息
              }
          });

          //添加步骤信息
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
                  $("#promptDIV").removeClass("alert alert-error alert-success");
                  $("#promptDIV").addClass("alert alert-error");
                  $("#promptDIV").html("步骤名称不能为空!");
                  return false;
              }
          }

          //form提交响应成功后执行的回调函数
          function step_showResponse(responseText, statusText) {
              var dataJson = eval("(" + responseText + ")");
              show_promptDIV(dataJson);//提示信息
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
 
 <div class="container"><h2>流程步骤管理</h2></div>
 
  <div class="container">
    <%--操作提示DIV--%>
    <div id="promptDIV" class="row"></div>
  </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"> <a href="#AllSteps" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li id="AddTab"><a href="#AddSteps" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
    
    <div class="tab-content">
    
      <%--显示全部流程步骤--%>
      <div class="tab-pane active" id="AllSteps">
        
        <%--查看所有步骤--%>
        <div id="stepsgrid"></div>
      </div>

     <%--添加步骤--%>
     <div class="tab-pane " id="AddSteps">
        <form id="add_Steps" class="form-horizontal" method="post" action="">
            <div class="control-group span6 offset2">
                <label class="control-label" for="stepsName">步骤名称：</label>
                <div class="controls">
                    <input type="text" name="stepsName" id="stepsName" class="input-prepend span4" placeholder="步骤名称" />                                                     
                </div>
            </div>

            <div class="control-group span6 offset2">
               <label class="control-label">流程名称:</label>
               <div class="controls">
                  <select class="span4" id="flowsName" name="flowsName" >
                     <option id="flowsNameInfo"></option>
                  </select>
               </div>
            </div>

            <div class="control-group span6 offset2">
              <label class="control-label">步骤类型:</label>
              <div class="controls">
                 <select class="span4" id="stepsType" name="stepsType">
                    <option id="stepsTypeInfo"></option>
                 </select>
              </div>
            </div>

             <div class="control-group span6 offset2">
              <label class="control-label">操作用户:</label>
              <div class="controls">
                 <select class="span4" id="stepsUser" name="stepsUser">
                    <option id="stepsUserInfo"></option>
                 </select>
              </div>
            </div>

            <div class="control-group span6 offset2">
                <label class="control-label" for="repeatCount">重复次数：</label>
                <div class="controls">
                    <input type="text" name="repeatCount" id="repeatCount" class="input-prepend span4" placeholder="重复次数" />                                                     
                </div>
            </div> 
            <div class="control-group span6 offset2">
                <label class="control-label" for="orderNo">排序编码：</label>
                <div class="controls">
                    <input type="text" name="orderNo" id="orderNo" class="input-prepend span4" placeholder="排序编码" />                                                     
                </div>
            </div>         
            <div class="control-group span6 offset2">
                <label class="control-label" for="stepsRemark">备注信息：</label>
                <div class="controls">
                <textarea name="stepsRemark" id="stepsRemark" rows="4" cols="5" class="span4" placeholder="备注信息" maxlength="200"></textarea>
                                          
                    <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                    <%string dt = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                    <%DateTime t = Convert.ToDateTime(dt);%>
                <input  type="hidden" id="stepsCreated_at" name="stepsCreated_at" value="<%=t%>"/>
                </div>
            </div>

            <div class="control-group span6 offset3">
                <div class="controls">
                    <input id="addSave" type="submit" value="添加" class="btn btn-primary  span1" /> 
                    &nbsp;&nbsp;&nbsp;
                    <input type="reset" value="重置"  class="btn btn-primary  span1" />
                </div>
            </div>

        </form>
     </div>
   
    </div>
</asp:Content>
