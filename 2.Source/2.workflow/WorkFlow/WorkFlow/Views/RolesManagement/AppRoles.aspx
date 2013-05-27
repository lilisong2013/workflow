<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppRoles
</asp:Content>

<%--本页用到的CSS/JS--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
  
    <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var tem = "";
        function checkAll(e, thisvalue) {
            tem = document.getElementById("rolesInvalid").value;
            var bb = document.getElementById('rolesInvalid');
            if (e.checked == true) {
                tem = thisvalue;
            }
            else {
                tem = tem.replace(thisvalue, "False");
            }
            bb.value = tem;
        }
    </script>
    <script type="text/javascript">
        function onclick() {
            if (document.getElementById("rolesInvalid").checked) {
                return false;
            }
            else
                return true;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var form = $("#add_Roles");
            form.submit(function () {
                if ($.trim($("#rolesName").val()).length == 0) {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-warningDIV");
                    $("#promptDIV").html("角色名称不能为空--???！");

                    return false;
                }
                else {
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
                }
            });
        });
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#AllRoles").ligerGrid({
                columns: [
                { display: '角色名称', name: 'name', width: 80 },
                { display: '角色备注', name: 'remark', width: 200 },
                { display: '是否有效', name: 'invalid', width: 80,
                    render: function (record, rowindex, value, column) {
                        if (!value) {
                            return "<img src='../../images/grid-checkbox.gif' />";
                        }
                        else {
                            return "<img src='../../images/grid-checkbox-checked.gif' />";
                        }
                    }
                },
                { display: '', width: 200,
                    render: function (row) {
                        var html = '<i class="icon-lock"></i><a href="/RolesManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/RolesManagement/ChangePage?id=' + row.id + '">删除</a><i class="icon-edit"></i><a href="/RolesManagement/EditPage?id=' + row.id + '">编辑</a>';
                        return html;
                    }
                }
                ],
                dataAction: 'server',
                width: '100%',
                pageSizeOptions: [5, 10, 15, 20, 25, 50],
                pageSize: 15,
                height: '400',
                url: '/RolesManagement/GetRoles_Apply',
                rownumbers: true,
                usePager: true
            });

        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            BindAppId();
            $("#AppIdInfo").html("请选择");
        });
        function BindAppId() {
            $.ajax({
                type: "Post",
                contentType: "application/json",
                url: "/ElementsManagement/GetAppId",
                data: {}, //即使参数为空，也需要设置
                dataType: 'JSON', //返回的类型为XML
                success: function (result, status) {
                    //成功后执行的方法
                    try {
                        if (status == "success") {
                            for (var i = 0; i < result.Total; i++) {
                                $("#AppIdParent").append("<option value='" + result.Rows[i].AppID + "'>" + result.Rows[i].AppID + "</option>");
                            }
                        }
                    } catch (e)
            { }
                }
            });
        }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>角色管理</h2></div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllRoles" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddRoles" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
    <div class="tab-content">
       <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
       </div>

        <div class="tab-pane active" id="AllRoles"></div>
        <div class="tab-pane" id="AddRoles">
          <form id="add_Roles" class="form-horizontal" method="post" action="/RolesManagement/AddRoles">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesName">角色名称：</label>
                        <div class="controls">
                            <input type="text" name="rolesName" id="rolesName" class="input-prepend span4"/>                            
                        </div>
                    </div>
                          
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesRemark">备注：</label>
                        <div class="controls">
                            <textarea name="rolesRemark" id="rolesRemark" rows="4" cols="5" class="span4"></textarea>
                            <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
                            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                            <%string dt = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                            <%DateTime t = Convert.ToDateTime(dt);%>
                          
                            <input type="hidden" name="rolesApp_id" id="rolesApp_id" value="<%=m_usersModel.app_id%>"/>
                            <input type="hidden" name="rolesInvalid" id="rolesInvalid" value="true"/>
                            <input type="hidden" name="rolesDeleted" id="rolesDeleted" value="false" />
                            <input type="hidden" name="rolesCreated_by" id="rolesCreated_by" value="<%=m_usersModel.id%>" />
                            <input type="hidden" name="rolesCreated_ip" id="rolesCreated_ip" value="<%= ipAddress %>" />
                            <input type="hidden" name="rolesCreated_at" id="rolesCreated_at" value="<%=t %>"/>
                        </div>
                    </div>
                    <div class="control-group span6 offset3">
                        <div class="controls">
                            <input type="submit" value="添加" class="btn btn-primary  span1" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="reset" value="重置"  class="btn btn-primary  span1" />
                        </div>
                    </div>
                </form>
        </div>
    </div>

</asp:Content>
