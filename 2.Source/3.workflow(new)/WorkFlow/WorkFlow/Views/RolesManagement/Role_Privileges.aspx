<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Role_Privileges
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        
    });
</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>角色权限管理</h2></div>
    <div class="container">
        <form method="post" action="" class="form-horizontal">
            <div class="control-group page-header">
                <label class="control-label">角色名称：</label>
                <div class="controls">
                    <input id="roleName" name="roleName" type="text" class="input-prepend span6" />
                </div>
            </div>

            <div class="control-group">
                <div class="page-header">
                    <h3>菜单</h3>
                </div>
                <div id="menusList">
                   
                </div>
            </div>

            <div class="control-group">
                <div class="page-header">
                    <h3>操作</h3>
                </div>
                <div id="operationsList">
                
                </div>
            </div>

            <div class="control-group">
                <div class="page-header">
                    <h3>页面元素</h3>
                </div>
                <div id="elementsList">
                
                </div>
            </div>
        </form>
    </div>

</asp:Content>


