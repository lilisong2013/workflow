<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/supersite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "密码修改";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

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
       
<div class="container">
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
    <div class="row">
        <h2 class="span4">修改超级管理员密码</h2>
       <ul class="pager">
       <li class="next">
        <a href="/AppsManagement/BaseUserApps">&lt;&lt;&nbsp;返回上页</a>
       </li>
       </ul>
    </div>
    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
</div>
    <%--修改超级管理员密码--%>
<div class="container" style="margin-top:16px;">                
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
