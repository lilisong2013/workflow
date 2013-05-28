<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/adminSite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
EditPage
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
   <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script> 
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../../bootstrap/js/jquery-1.9.1.js" type="text/javascript"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              var form = $("#Edit_Elements");
              form.submit(function () {
                  $.post(form.attr("action"),
                    form.serialize(),
                    function (result, status) {
                        //debugger
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(result.css);
                        $("#promptDIV").html(result.message);

                        if (result.success) {
                            location.href = result.toUrl;
                        }
                    },
                    "JSON");
                  return false;
              });
          });
    </script>

<script type="text/javascript">
    $(document).ready(function () {
        BindInvalidName();
        $("#elementsInfo").html("请选择");
    });
    function BindInvalidName() {
        $.ajax({
            type: "Post",
            contentType: "application/json",
            url: "/RolesManagement/GetInvalidName",
            data: {}, //即使参数为空，也需要设置
            dataType: 'JSON', //返回的类型为XML
            success: function (result, status) {
                //成功后执行的方法
                try {
                    if (status == "success") {
                        for (var i = 0; i < result.Total; i++) {
                            $("#elementsInvalid").append("<option value='" + result.Rows[i].InvalidID + "'>" + result.Rows[i].InvalidName + "</option>");
                        }
                    }
                } catch (e) { }
            }
        });
    }
  </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container"><h2>元素管理</h2></div>
    <div class="container">
       <div class="row-fluid">
        <ul class="pager"><li class="next"><a href="/ElementsManagement/AppElements">返回</a></li></ul>
       </div>         
    </div>
    <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
    <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
    <%DateTime t = Convert.ToDateTime(s); %>   
    <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
   <div class="container">
    <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
   </div>  
     <div class="tab-pane">
      <form  id="Edit_Elements" method="post" action="/ElementsManagement/EditElements" class="form-horizontal">           
       <div class="control-group span6 offset2">       
       <label class="control-label">元素名称：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsName" name="elementsName" type="text" value="<%=ViewData["elementsName"]%>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">元素编码：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsCode" name="elementsCode" type="text" value="<%=ViewData["elementsCode"]%>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">初始状态ID：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsInitstatus_id" name="elementsInitstatus_id" type="text" value="<%=ViewData["elementsInitstatus_id"]%>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;排序码：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsSeqno" name="elementsSeqno" type="text" value="<%=ViewData["elementsSeqno"]%>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">页面ID：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsMenu_id" name="elementsMenu_id" type="text" value="<%=ViewData["elementsMenu_id"]%>"/>
       </div> 
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">是否有效：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls"> 
       <select id="elementsInvalid" name="elementsInvalid" width="140px">
        <option id="elementsInfo"></option>
       </select>
       </div>   
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label"> 备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>    
       <div class="controls">
       <textarea id="elementsRemark" name="elementsRemark" cols="5" rows="4"><%=ViewData["elementsRemark"]%></textarea>
       </div>   
        <input type="hidden" name="elementsId" id="elementsId" value="<%=ViewData["elementsId"]%>"/>
        <input type="hidden" name="elementsDeleted" id="elementsDeleted" value="<%=ViewData["elementsDeleted"]%>" />
        <input type="hidden" name="elementsCreated_at" id="elementsCreated_at" value="<%=t%>" />
        <input type="hidden" name="elementsCreated_by" id="elementsCreated_by" value="<%=m_usersModel.id%>" />  
        <input type="hidden" name="elementsApp_id" id="elementsApp_id" value="<%=m_usersModel.app_id%>"/>     
        <input type="hidden" name="elementsCreated_ip" id="elementsCreated_ip" value="<%= ipAddress %>" />
        <input type="hidden" name="elementsUpdated_at" id="elementsUpdated_at"/>
        <input type="hidden" name="elementsUpdated_by" id="elementsUpdated_by"/>
        <input type="hidden" name="elementsUpdated_ip" id="elementsUpdated_ip"/>
       </div>
       <div class="control-group span6 offset3" style="background-position:center">
       <input type="submit" value="修改" class="btn btn-primary  span3" />  
       </div>    
    </form>
    </div> 
</asp:Content>
