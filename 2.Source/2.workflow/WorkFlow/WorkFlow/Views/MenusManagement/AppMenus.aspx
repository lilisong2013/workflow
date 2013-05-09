<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppMenus
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">

        $(document).ready(function () {
            var form = $("#add_Menus");
            form.submit(function () {
                $.post(form.attr("action"),
                    form.serialize(),
                    function (result, status) {
                        //debugger;
                        $("#promptDIV").addClass(result.css);
                        $("#promptDIV").html(result.message);
                    },
                    "JSON");
                return false;
            });
        });
    </script>

    <style type="text/css">

    </style>

    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>菜单管理</h2></div>
    <div class="container">
        <div></div>
    </div>
    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllMenus" data-toggle="tab"><i class="icon-check"></i>全部<%=10 %></a></li>
            <li><a href="#AddMenus" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
       
        <%--查看所有菜单--%>
        <div class="tab-pane active" id="AllMenus">所有菜单</div>
        
        <%--添加菜单--%>
        <div class="tab-pane" id="AddMenus">
            <form id="add_Menus" class="form-horizontal" method="post" action="/MenusManagement/AddMenus">
                <div class="control-group span6 offset2">
                    <label class="control-label">菜单名称</label>
                    <div class="controls">
                        <input name="MenusName" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">菜单编码</label>
                    <div class="controls">
                        <input name="MenusCode" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">菜单URL</label>
                    <div class="controls">
                        <input name="MenusUrl" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">父菜单</label>
                    <div class="controls">
                        <select name="MenusParent" class="span4">
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="MenusRemark" rows="4" cols="5" class="span4"></textarea>
                    </div>
                </div>
                <div class="control-group span6 offset3">
                    <input type="submit" class="btn btn-primary btn-large span4" onclick="" value="添加菜单" />
                </div>
            </form>
        </div>
    </div>

</asp:Content>


