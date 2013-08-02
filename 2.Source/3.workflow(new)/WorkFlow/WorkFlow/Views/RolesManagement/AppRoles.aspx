<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
</asp:Content>

<%--本页用到的CSS/JS--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">

   <link href="../../LigerUI/lib/ligerUI/skins/ligerui-icons.css" rel="Stylesheet" type="text/css"/>
   <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
   <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
   <%--LigerUI ToolBar文件--%>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
   <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerFilter.js" type="text/javascript"></script>

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
            $("#promptDIV").removeClass("alert alert-error alert-success");
            $("#promptDIV").html("");
        });
    </script>

    <%--在Grid中后台分页显示role信息--%>
    <script type="text/javascript">
        $("document").ready(function () {

            GetRoleList();
            function GetRoleList() {
                window['t'] = $("#rolesgrid").ligerGrid({
                    columns: [
                   { display: '角色ID', name: 'id', width: 80, align: 'center' },
                        { display: '角色名称', name: 'name', align: 'center' },
                        { display: '角色备注', name: 'remark', align: 'center' },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
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
                        },
                         { display: '', width: 100,
                             render: function (row) {
                                 var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="RolePrivilegeDialog(' + row.id + ')">权限设置</a>';
                                 return html;
                             }
                         }
                   ],
                    dataAction: 'server',
                    width: '99%',
                    pageSizeOptions: [5, 10, 15, 20, 25, 50],
                    pageSize: 10,
                    height: '400',
                    rownumbers: true,
                    usePager: true,
                    url: "/RolesManagement/GetRolesList"
                });
                t.loadData();
            }
        });
    </script>
  
  <%--编辑弹出框函数--%>
    <script type="text/javascript">
        function EditDialog(id) {

            if (id) {
                var m = $.ligerDialog.open({
                    title: '更新角色信息',
                    width: 800,
                    height: 500,
                    showMax: true,
                    showMin: true,
                    url: '/RolesManagement/EditPage?id=' + id,
                    buttons:
                    [
                    { text: '返回', onclick: function (item, dialog) { t.loadData(); dialog.close(); } }

                    ]
                });

            }
        }
    </script>
   <%--角色权限弹出框函数--%>
   <script type="text/javascript">
       function RolePrivilegeDialog(id) {
           if (id) {
               var m = $.ligerDialog.open({
                   title: '更新角色信息',
                   width: 800,
                   height: 500,
                   showMax: true,
                   showMin: true,
                   url: '/RolesManagement/Role_Privileges?id=' + id,
                   buttons:
                    [
                    { text: '返回', onclick: function (item, dialog) { t.loadData(); dialog.close(); } }

                    ]
               });
           }
       }
   </script>
   <%--详情弹出框函数--%>
   <script type="text/javascript">
       function DetailDialog(id) {
           if (id) {
               $.ligerDialog.open({
                 title:'详情('+id+')信息',
                 width:700,
                 height:600,
                 url: '/RolesManagement/DetailInfo?id=' + id
               });
           }
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
                             var dataJson = eval("(" + responseText + ")");
                             //删除提示信息
                             show_DIV(dataJson);
                             t.loadData();

                         }
                     });

                     //删除提示信息
                     function show_DIV(data) {
                         $("#promptDIV").removeClass("alert alert-error alert-success");
                         $("#promptDIV").addClass(data.css);
                         $("#promptDIV").html(data.message);
                     }
                 }
             })
         }
    </script>

   <%--添加角色--%>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#addSave").click(function () {
               if (false) {
                   return false;
               } else {
                   AddRoles(); //添加角色信息
               }
           });

           //添加角色信息
           function AddRoles() {
               var options = {
                   beforeSubmit: role_showRequest, //form提交前的响应的回调函数
                   success: role_showResponse, //form提交响应成功后执行的回调函数
                   url: "/RolesManagement/AddRoles",
                   type: "POST",
                   dataType: "json"
               };
               $("#add_Roles").ajaxForm(options);
           }

           //form提交前的响应的回调函数
           function role_showRequest() {
               var roleName = $("#rolesName").val();
               if (roleName == "") {
                   $("#promptDIV").removeClass("alert alert-error alert-success");
                   $("#promptDIV").addClass("alert alert-error");
                   $("#promptDIV").html("角色名称不能为空!");
                   return false;
               }
           }

           //form提交响应成功后执行的回调函数
           function role_showResponse(responseText, statusText) {
               var dataJson = eval("(" + responseText + ")");
               show_promptDIV(dataJson);
               t.loadData();
           }

           //提示信息
           function show_promptDIV(data) {
               $("#promptDIV").removeClass("alert alert-error alert-success");
               $("#promptDIV").addClass(data.css);
               $("#promptDIV").html(data.message);
           }

       });
   </script>
  
  <%--查询信息--%>
  <script type="text/javascript">
      var key;
      function search() {
          key = $("#txtKey").val();

          $.ajax({
              url: "/RolesManagement/GetListByRoleName?roleName="+key,
              type: "POST",
              dataType: "json",
              data: {},
              success: function (responseText, statusText) {
                  var dataSearchJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                  $("#rolesgrid").ligerGrid({
                      columns: [
                        { display: '角色ID', name: 'id', width: 80, align: 'center' },
                        { display: '角色名称', name: 'name', align: 'center' },
                        { display: '角色备注', name: 'remark', align: 'center' },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
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
                      data: dataSearchJson,
                      newPage: 1
                  });
                  $("#rolesgrid").ligerGetGridManager().loadData();
              }
          });
      }
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

        
        <div class="tab-pane active" id="AllRoles">
        <%--查询按钮--%> 
        <b>角色名称:</b><input id="txtKey" type="text" class="input-medium search-query span3"/>
        <input id="btnOK" type="button" value="查询" onclick="search()"/> 
        <hr /> 
        <%--查看所有角色--%>
        <div id="rolesgrid"></div>
        </div>

        <%--添加角色--%>
        <div class="tab-pane" id="AddRoles">
          <form id="add_Roles" class="form-horizontal" method="post" action="">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesName">角色名称：</label>
                        <div class="controls">
                            <input type="text" name="rolesName" id="rolesName" class="input-prepend span4" placeholder="角色名称"/>                                                     
                        </div>
                    </div>
                          
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="rolesRemark">备注：</label>
                        <div class="controls">
                            <textarea name="rolesRemark" id="rolesRemark" rows="4" cols="5" class="span4" placeholder="备注"></textarea>

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
                            <input id="addSave" type="submit" value="添加" class="btn btn-primary  span1" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="reset" value="重置"  class="btn btn-primary  span1" />
                        </div>
                    </div>

              </form>
        </div>

    </div>

</asp:Content>
