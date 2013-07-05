<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">

    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "用户编辑";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

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
                            //alert("ok?");
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
	    $(document).ready(function () {
	        var usersData;
	        var usersStr;
	        $("#saveSubmit").click(function () {
	            if (false) {
	                return false;
	            } else {
	                usersStr = "{"; //JSON数据字符串
	                var uiTotal = 0; //用户有效的数量
	                //alert(uiTotal);
	                //用户"是否有效"中被选中的项 
	                for (var i = 0; i < 1; i++) {
	                    var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
	                    // alert(checkBoxID);
	                    if (checkBoxID.is(":checked")) {
	                        // alert(checkBoxID.val() + "选中");
	                        //alert(checkBoxID.is(":checked"));
	                        usersStr += "uInvalidID" + uiTotal.toString() + ":'" + checkBoxID.val() + "',";
	                        uiTotal++;
	                        checkBoxID.prop("checked", true);
	                    } else {
	                        //alert(checkBoxID.is(":checked"));
	                        // alert(checkBoxID.val() + "未选中");
	                        checkBoxID.prop("checked", false);
	                    }
	                }

	                usersStr += "in_Total:'" + uiTotal + "',u_ID:'" + $("#userID").val() + "'}";
	                // alert(usersStr);

	                usersData = eval("(" + usersStr + ")");
	                // alert(usersData);
	                $("#Edit_Users").ajaxForm({
	                    success: ue_showResponse,  // form提交响应成功后执行的回调函数
	                    url: "/UsersManagement/EditUsers",
	                    type: "POST",
	                    dataType: "json",
	                    data: usersData
	                });
	            }
	        });


	        //提交role_privileges表单后执行的函数
	        function ue_showResponse(responseText, statusText) {
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
<div class="container"><h2>用户管理</h2></div>
<div class="container">
 <div class="row-fluid">
    <ul class="pager"><li class="next"><a href="/UsersManagement/AppUsers">返回</a></li></ul>  
 </div> 
</div>
 <div class="container">
   <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
 </div> 
  
  <div class="tab-pane">
   <form  id="Edit_Users" method="post" action="" class="form-horizontal"> 
       <div class="control-group span6 offset2">
       <label class="control-label">登录名称：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls"><input id="usersLogin" name="usersLogin" type="text" value="<%= ViewData["usersLogin"] %>" /></div>
       <input id="userID" name="userID" type="hidden" value="<%=ViewData["usersId"] %>"/>
      </div>
       <div class="control-group span6 offset2">
        <label class="control-label">原始密码：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        <input id="oldPassword" name="oldPassword" type="password" />     
        </div>
       </div> 
       <div class="control-group span6 offset2">
        <label class="control-label">新密码：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        <input id="newPassword" name="newPassword" type="password" /> 
        </div>
       </div> 

        <div class="control-group span6 offset2">
        <label class="control-label">确认密码：&nbsp;&nbsp;&nbsp;</label>
        <div class="controls">
        <input id="PasswordCon" name="PasswordCon" type="password"/> 
        </div>
       </div>
          
       <div class="control-group span6 offset2">
       <label class="control-label">用户姓名：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersName" name="usersName" type="text" value="<%= ViewData["usersName"] %>" />
       </div>
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">工号：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersEmployee_no" name="usersEmployee_no" type="text" value="<%= ViewData["usersEmployee_no"]%>"/> 
       </div>
       </div>     
       <div class="control-group span6 offset2">
       <label class="control-label">手机号码：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">     
       <input id="usersMobile_phone" name="usersMobile_phone" type="text" value="<%= ViewData["usersMobile_phone"]%>" />
       </div> 
       </div>
       <div class="control-group span6 offset2">
       <label class="control-label">邮件地址：&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="usersMail" name="usersMail" type="text" value="<%=ViewData["usersMail"]%>"/>
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
       <textarea id="usersRemark" name="usersRemark" cols="5" rows="4"><%=ViewData["usersRemark"]%></textarea>
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
