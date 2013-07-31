<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">

    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "用户编辑";
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

     <script type="text/javascript">
         var userID;
         $(document).ready(function () {
             userID = $("#userID").val(); //用户ID
         });
         var inTotal = 0; //是否有效数量
     </script>
 
    <%--是否有效初始化--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                url: "/UsersManagement/GetInvalidList",
                type: "POST",
                dataType: "json",
                data: { userid: userID },
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")");
                    //alert(dataJson);
                    inTotal = parseInt(dataJson.total); //操作权限数量
                    for (var i = 0; i < dataJson.total; i++) {
                        $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                    }
                    for (var i = 0; i < dataJson.total; i++) {
                        if (dataJson.List[i].selected == "true") {
                            $("#invalidValue" + i.toString()).prop("checked", true);
                            
                        } else {
                            $("#invalidValue" + i.toString()).prop("checked", false);
                           
                        }
                    }
                }
            });
        });
    </script>

   <%--表单提交数据--%>
	<script type="text/javascript">
	    $(document).ready(function () {
	        var usersData;
	        var usersStr;
	        var uiTotal = 0; //用户有效的数量
	        $("#saveSubmit").click(function () {
	            if (false) {
	                return false;
	            } else {
	                usersStr = "{"; //JSON数据字符串

	                //alert(uiTotal);
	                //用户"是否有效"中被选中的项 
	                for (var i = 0; i < 1; i++) {
	                    var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID                 
	                    if (checkBoxID.is(":checked")) {
	                        usersStr += "uInvalidID" + uiTotal.toString() + ":'" + checkBoxID.val() + "',";
	                        uiTotal++;
	                        checkBoxID.prop("checked", true);
	                    } else {

	                        checkBoxID.prop("checked", false);
	                    }
	                }

	                usersStr += "in_Total:'" + uiTotal + "',u_ID:'" + $("#userID").val() + "'}";

	                usersData = eval("(" + usersStr + ")");
	                ModifyUser(); //修改用户信息

	            }
	        });

	        //修改用户信息
	        function ModifyUser() {
	            var options = {
	                beforeSubmit: user_showRequest, //form提交前的响应回调函数
	                success: user_showResponse, //form提交响应成功后执行的回调函数
	                url: "/UsersManagement/EditUsers",
	                type: "POST",
	                dataType: "json",
	                data: {in_Total:uiTotal}
	            };
	            $("#Edit_Users").ajaxForm(options);
	        }

	        //form提交前的响应回调函数
	        function user_showRequest() {
	            var userLogin = $("#usersLogin").val();
	            if (userLogin == "") {
	                $("#promptDIV").removeClass("alert alert-error alert-success");
	                $("#promptDIV").addClass("alert alert-error");
	                $("#promptDIV").html("用户登录名称不能为空!");
	                return false;
	            }
	        }

	        //form提交响应成功后执行的回调函数
	        function user_showResponse(responseText, statusText) {
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
<div class="container"><h2>用户管理</h2></div>

 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div> 
  
  <div class="tab-pane">
   <form  id="Edit_Users" method="post" action="" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">登录名称：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls"><input id="usersLogin" name="usersLogin" type="text" value="<%= ViewData["usersLogin"] %>" placeholder="登录名称"/></div>
       <input id="userID" name="userID" type="hidden" value="<%=ViewData["usersId"] %>"/>
      </div>    
       <div class="control-group span6 offset2">
        <label class="control-label">新密码：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        <input id="newPassword" name="newPassword" type="password" placeholder="新密码"/> 
        </div>
       </div> 

        <div class="control-group span6 offset2">
        <label class="control-label">确认密码：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        <input id="PasswordCon" name="PasswordCon" type="password" placeholder="确认密码"/> 
        </div>
       </div>
          
       <div class="control-group span6 offset2">
       <label class="control-label">用户姓名：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersName" name="usersName" type="text" value="<%= ViewData["usersName"] %>" placeholder="用户姓名"/>
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">工号：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersEmployee_no" name="usersEmployee_no" type="text" value="<%= ViewData["usersEmployee_no"]%>" placeholder="工号"/> 
       </div>
       </div>     
       <div class="control-group span6 offset2">
       <label class="control-label">手机号码：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">     
       <input id="usersMobile_phone" name="usersMobile_phone" type="text" value="<%= ViewData["usersMobile_phone"]%>" placeholder="手机号码"/>
       </div> 
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">邮件地址：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersMail" name="usersMail" type="text" value="<%=ViewData["usersMail"]%>" placeholder="邮件地址"/>
       </div>
       </div>                             

       <div class="control-group span6 offset2">      
       <label class="control-label">是否有效：&nbsp;&nbsp;&nbsp;</label>
       <div id="invalidList">
      
       </div>      
       </div> 
                                 
       <div class="control-group span6 offset2">
       <label class="control-label">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：&nbsp;&nbsp;&nbsp;</label> 
       <div class="controls">   
       <textarea id="usersRemark" name="usersRemark" cols="5" rows="4" placeholder="备注"><%=ViewData["usersRemark"]%></textarea>
        <input type="hidden" name="usersId" id="usersId" value="<%=ViewData["usersId"]%>"/>
        <input type="hidden" name="usersDeleted" id="usersDeleted" value="False" />
        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  
        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        <input type="hidden" name="usersCreated_at" id="usersCreated_at"/>
        <input type="hidden" name="usersCreated_by" id="usersCreated_by"/>       
        <input type="hidden" name="usersCreated_ip" id="usersCreated_ip"/>
        <input type="hidden" name="usersUpdated_at" id="usersUpdated_at" value="<%=t%>"/>
        <input type="hidden" name="usersUpdated_by" id="usersUpdated_by" value="<%=-1%>"/>
        <input type="hidden" name="usersUpdated_ip" id="usersUpdated_ip" value="<%=ipAddress%>"/>
       </div>
       </div>   
       <div class="control-group span6 offset3">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span3" style="background-position:center"/>  
       </div>
   </form>
</div>
</asp:Content>
