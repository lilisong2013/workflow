<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
</asp:Content>

<%--本页用到的CSS/JS--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">

   <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
   <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
   <%-- <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "角色管理";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>

     <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>
    <%--在Grid中显示role信息--%>
    <script type="text/javascript">
        var managerListGrid;
        $(document).ready(function () {
            //定义ligerGrid;
            $("#rolesgrid").ligerGrid({
                width: '90%',
                height: 400
            });
            managerListGrid = $("#rolesgrid").ligerGetGridManager();
            GetRolesList(); //获取用户数据列表
            $("#infoTab").click(function () {
                GetRolesList(); //获取用户数据列表
            });
        });
        function GetRolesList() {

            $.ajax({
                url: "/RolesManagement/GetRoles_Apply",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    
                    var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                   // alert(dataJson);
                    //更新mygrid数据
                    managerListGrid.setOptions({
                        columns: [
                        { display: '角色ID', name: 'id',width:80,align: 'center' },
                        { display: '角色名称', name: 'name', align: 'center' },
                        { display: '角色备注', name: 'remark', align: 'center' },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="/RolesManagement/DetailInfo?id=' + row.id + '">详情</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="/RolesManagement/EditPage?id=' + row.id + '">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteRole(' + row.id + ')">删除</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-user"></i><a href="/RolesManagement/Role_Privileges?id=' + row.id + '">权限设置</a>';
                                return html;
                            }
                        }
                       ],
                        data: dataJson
                    });
                    managerListGrid.loadData();
                }
            });
        }
    </script>

  <%--删除确认函数--%>
     <script type="text/javascript">
         function DeleteRole(id) {
             //alert(id);
             var roleid = id;
             $.ligerDialog.confirm('确定要删除吗?', function (yes) {
                 //return true;
                 if (yes) { 
                 $.ajax({
                     url: "/RolesManagement/DeleteRole",
                     type: "POST",
                     dataType: "json",
                     data: { roleID: roleid },
                     success: function (responseText, statusText) {
                         GetRolesList();
                         $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                         $("#promptDIV").addClass(responseText.css);
                         $("#promptDIV").html(responseText.message);
                     }
                 });
             }
           })
         }
    </script>
  <%--添加角色确认信息--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var form = $("#add_Roles");
            form.submit(function () {
                if ($.trim($("#rolesName").val()).length == 0) {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-errorDIV");
                    $("#promptDIV").html("角色名称不能为空！");

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


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>角色管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
    </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllRoles" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddRoles" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
       
    <div class="tab-content">

        <%--查看所有角色--%>
        <div class="tab-pane active" id="AllRoles">
        <div id="rolesgrid"></div>
        </div>

        <%--添加角色--%>
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

                            <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)(Session["user"]);%>
                            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                            <%string dt = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                            <%DateTime t = Convert.ToDateTime(dt);%>
                          
                            <input type="hidden" name="rolesApp_id" id="rolesApp_id" value="<%=m_usersModel.app_id%>"/>
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
