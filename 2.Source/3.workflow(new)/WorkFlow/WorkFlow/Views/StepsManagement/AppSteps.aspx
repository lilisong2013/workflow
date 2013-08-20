<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
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
            <li><a href="#AddSteps" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
    
    <div class="tab-content">
    
      <%--显示全部流程步骤--%>
      <div class="tab-pane active" id="AllSteps">
      
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
                <label class="control-label" for="flowsName">流程名称：</label>
                <div class="controls">
                    <input type="text" name="flowsName" id="flowsName" class="input-prepend span4" placeholder="流程名称" />                                                     
                </div>
            </div> 
            <div class="control-group span6 offset2">
                <label class="control-label" for="stepsType">步骤类型：</label>
                <div class="controls">
                    <input type="text" name="stepsType" id="stepsType" class="input-prepend span4" placeholder="步骤类型" />                                                     
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
