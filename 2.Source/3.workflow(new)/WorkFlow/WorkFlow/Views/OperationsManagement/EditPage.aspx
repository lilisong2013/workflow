<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
  <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
  <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

  <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "操作编辑";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

  <script type="text/javascript">
      var operationID;
      $(document).ready(function () {
          operationID = $("#operationID").val(); //用户ID
          //alert(operationID);
      });
      var opTotal = 0;//是否有效数量
  </script>
  <%--是否有效初始化--%>
  <script type="text/javascript">
      $(document).ready(function () {
          $.ajax({
              url: "/OperationsManagement/GetInvalidList",
              type: "POST",
              dataType: "json",
              data: { operationid: operationID },
              success: function (responseText, statusText) {
                  //alert(responseText);
                  var dataJson = eval("(" + responseText + ")");
                   //alert(dataJson);
                  opTotal = parseInt(dataJson.total); //操作权限数量
                  for (var i = 0; i < dataJson.total; i++) {
                      $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                  }
                  for (var i = 0; i < dataJson.total; i++) {
                      if (dataJson.List[i].selected == "true") {
                          $("#invalidValue" + i.toString()).prop("checked", true);
                         // alert("ok?");
                      } else {
                          $("#invalidValue" + i.toString()).prop("checked", false);
                         //alert("???");
                      }
                  }
              }
          });
      }); 
  </script>
  <%--表单提交数据--%>
  <script type="text/javascript">
     $(document).ready(function(){
      var operationsData;
      var operationsStr;
      $("#saveSubmit").click(function () {
          if (false) {
              return false;
          } else {
              operationsStr = "{"; //JSON数据字符串
              var oiTotal = 0; //操作有效的数量              
              //操作"是否有效"中被选中的项 
              for (var i = 0; i < 1; i++) {
                  var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                  if (checkBoxID.is(":checked")) {
                      //alert(checkBoxID.val() + "选中");
                      //alert(checkBoxID.is(":checked"));
                      operationsStr += "oInvalidID" + oiTotal.toString() + ":'" + checkBoxID.val() + "',";
                      oiTotal++;
                      //checkBoxID.prop("checked", true);
                  } else {
                      //alert(checkBoxID.is(":checked"));
                      //alert(checkBoxID.val() + "未选中");
                      //checkBoxID.prop("checked", false);
                  }
              }
              operationsStr += "oi_Total:'" + oiTotal + "',u_ID:'" + $("#operationID").val() + "'}";
              operationsData = eval("(" + operationsStr + ")");
              $("#Edit_Operations").ajaxForm({
                  success: ue_showResponse,  // form提交响应成功后执行的回调函数
                  url: "/OperationsManagement/EditOperations",
                  type: "POST",
                  dataType: "json",
                  data: operationsData

              });
          }
        });
          function ue_showResponse(responseText, statusText) {
              //alert("ok????");
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
 <div class="container"><h2>操作管理</h2></div>
   <div class="container">
     <div class="row-fluid">
       <ul class="pager"><li class="next"><a href="/OperationsManagement/AppOperations">返回</a></li></ul>
     </div>
    </div> 
       <% string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
       <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
       <%DateTime t = Convert.ToDateTime(s); %>  
       <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
    <div class="container">
     <%--操作提示DIV--%>
     <div id="promptDIV" class="row"></div>
    </div>   
    <div class="tab-pane">
      <form  id="Edit_Operations" method="post" action="" class="form-horizontal">           
       <div class="control-group span6 offset2">
       <label class="control-label">操作名称：</label>
       <div class="controls">
       <input id="operationsName" name="operationsName" type="text" value="<%=ViewData["operationsName"]%>" />
       <input id="operationID" name="operationID" type="hidden" value="<%=ViewData["operationsId"]%>"/>
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">操作编码：</label>
       <div class="controls">
       <input id="operationsCode" name="operationsCode" type="text" value="<%=ViewData["operationsCode"]%>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">是否有效：</label>     
        <div id="invalidList">
        
        </div>   
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">操作描述：</label>
       <div class="controls">
       <textarea id="operationsDescription" name="operationsDescription" cols="5" rows="4" ><%=ViewData["operationsDescription"]%></textarea>
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</label>
       <div class="controls">
       <textarea id="operationsRemark" name="operationsRemark" cols="5" rows="4"><%=ViewData["operationsRemark"]%></textarea>
        <input type="hidden" name="operationsApp_id" id="operationsApp_id" value="<%=m_usersModel.app_id%>"/>
        <input type="hidden" name="operationsId" id="operationsId" value="<%=ViewData["operationsId"]%>"/>
        <input type="hidden" name="operationsDeleted" id="operationsDeleted" value="<%=ViewData["operationsDeleted"]%>" />
        <input type="hidden" name="operationsCreated_at" id="operationsCreated_at" value="<%=t%>" />
        <input type="hidden" name="operationsCreated_by" id="operationsCreated_by" value="<%=m_usersModel.id%>" />       
        <input type="hidden" name="operationsCreated_ip" id="operationsCreated_ip" value="<%=ipAddress%>" />
        <input type="hidden" name="operationsUpdated_at" id="operationsUpdated_at" value=""/>
        <input type="hidden" name="operationsUpdated_by" id="operationsUpdated_by" value=""/>
        <input type="hidden" name="operationsUpdated_ip" id="operationsUpdated_ip" value=""/>
       </div>
       </div>
       <div class="control-group span6 offset3">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span3" />  
       </div>    
    </form>
    </div>
</asp:Content>

