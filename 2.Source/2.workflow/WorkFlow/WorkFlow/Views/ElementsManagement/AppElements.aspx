<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppElements
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <%--<script src="../../Scripts/jquery.form.js" type="text/javascript"></script>--%>
<script type="text/javascript">
//    function AddElements() {
//        var xmlhttp;
//        if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
//            xmlhttp = new XMLHttpRequest();
//        }
//        else {// code for IE6, IE5
//            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
//        }
//        xmlhttp.open("POST", "/ElementsManagement/AddElements", true);
//        xmlhttp.send();
    //    }

    $(document).ready(function () {
        var form = $("#addElements");
        form.submit(function () {
            $.post(form.attr("action"),
                form.serialize(),
                function (result, status) {
                    //debugger;
                    alert(status);
                },
                "JSON");
            return false;
        });
    });
</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>元素管理</h2></div>
    <div class="container">
        <div></div>
    </div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllElements" data-toggle="tab"><i class="icon-check"></i>全部<%=10 %></a></li>
            <li><a href="#AddElements" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
        <div class="tab-pane active" id="AllElements">所有元素</div>
        <div class="tab-pane" id="AddElements">
            <form id="addElements" class="form-horizontal" method="post" action="/ElementsManagement/AddElements">
                <div class="control-group span6 offset2">
                    <label class="control-label">页面元素名称</label>
                    <div class="controls">
                        <input name="ElementsName" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">页面元素编码</label>
                    <div class="controls">
                        <input name="ElementsCode" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">初始化状态</label>
                    <div class="controls">
                        <select class="span4">
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">所在页面</label>
                    <div class="controls">
                        <select class="span4">
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">排序码</label>
                    <div class="controls">
                        <input name="ElementsCode" type="text" class="span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea name="ElementsRemark" rows="4" cols="5" class="span4"></textarea>
                    </div>
                </div>
                <div class="control-group span6 offset3">
                    <input type="submit" class="btn btn-primary btn-large span4" onclick="" value="添加元素" />
                </div>
            </form>
        </div>
    </div>

</asp:Content>


