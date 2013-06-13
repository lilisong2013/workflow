<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
BU_AppsPassModify
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             var form = $("#modifyAppsPass");
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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
       
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>                 
        <div class="container"><h2 class="span3">管理员密码管理</h2></div>  
         <div class="container">
        <div class="btn-group pull-right">        
            <div class="btn btn-large btn-info">欢迎：<%=m_baseuserModel.login %></div>
            <div class="btn btn-large btn-info dropdown-toggle" data-toggle="dropdown">
                <span class="caret"></span>
            </div>
            <ul class="dropdown-menu">
                <li><a href="/AppsManagement/QuitSys">退出</a></li>
                <li><a href="/AppsManagement/LoginAgain">重新登录</a></li>
                <li><a href="/AppsManagement/BaseUserApps">返回</a></li>
            </ul>
        </div>
    </div>     
        <div class="container">
         <%--操作提示DIV--%>
         <div id="promptDIV" class="row"></div>
         </div>     
       
    <%--修改超级管理员密码--%>
            <div class="tab-pane">                
                    <form  id="modifyAppsPass" method="post" action="/AppsManagement/ModifyAdminPassword" class="form-horizontal">
                        <div class="control-group">
                            <label class="control-label" for="oldpassword">原密码：</label>
                            <div class="controls">
                                <input name="oldpassword" id="oldpassword" type="password" class="span3" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="newpassword">新密码：</label>
                            <div class="controls">
                                <input name="newpassword" id="newpassword" type="password" class="span3" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="newpassword2">确认密码：</label>
                            <div class="controls">
                                <input name="newpassword2" id="newpassword2" type="password" class="span3" />
                            </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <input  type="submit" class="btn btn-primary span3" value="修改" />
                            </div>
                        </div>
                    </form>
               
            </div>
</asp:Content>
