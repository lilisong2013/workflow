<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
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
 
   <script src="../../Scripts/ligerGrid.showFilter.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "用户管理";
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

    <%--在Grid中分页显示user信息--%>
    <script type="text/javascript">
        $(document).ready(function () {

            GetUserList();
            function GetUserList() {
                window['t'] = $("#usersgrid").ligerGrid({
                    columns: [
                        { display: '用户ID', name: 'id', width: 80, align: 'center' },
                        { display: '登录名称', name: 'login', align: 'center' },
                        { display: '用户姓名', name: 'name', align: 'center' },
                        { display: '工号', name: 'employee_no', align: 'center' },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')" >编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteUser(' + row.id + ')">删除</a>';
                                return html;
                            }
                        },
                     
                       { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-user"></i><a href="javascript:void(0);" onclick="UserRoleDialog('+row.id+')">角色设置</a>';
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
                    url: "/UsersManagement/GetUsers_List"

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
                        title: '更新用户信息',
                        width: 900,
                        height: 600,
                        isDrag: true,                    
                        isResize: true,  
                        url: '/UsersManagement/EditPage?id=' + id,
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
                    title: '详情(' + id + ')信息',
                    isDrag: true,
                    isResize: true, 
                    width: 700,
                    height: 600,
                    url: '/UsersManagement/DetailInfo?id=' + id
                });
            }
        }
    </script>

   <%--用户角色弹出框函数--%>
   <script type="text/javascript">
       function UserRoleDialog(id) {
           if (id) {
               $.ligerDialog.open({
                   title: '用户角色信息',
                   width: 900,
                   height:600,
                   isResize: true,
                   isDrag:true,
                   url: '/UsersManagement/UserRoles?id=' + id,
                   buttons:
                    [
                    { text: '返回', onclick: function (item, dialog) { t.loadData(); dialog.close(); } }

                    ]
               });
           }
       }
   </script>

    <%--用户删除--%>
    <script type="text/javascript">
        function DeleteUser(id) {
            //alert(id);
            var userid = id;
            $.ligerDialog.confirm('确定要删除吗?', function (yes) {
                //return true;
                if (yes) {
                    $.ajax({
                        url: "/UsersManagement/DeleteUser",
                        type: "POST",
                        dataType: "json",
                        data: { userID: userid },
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
  
    <%--用户添加--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#addSave").click(function () {
                if (false) {
                    return false;
                } else {
                    AddUsers(); //添加用户信息
                }
            });

            //添加用户信息
            function AddUsers() {
                var options = {
                    beforeSubmit: user_showRequest, //form提交前的响应的回调函数
                    success: user_showResponse, //form提交相应成功后执行的回调函数
                    url: "/UsersManagement/AddUsers",
                    type: "POST",
                    dataType: "json"
                };
                $("#add_Users").ajaxForm(options);
            }

            //form提交前的响应的回调函数
            function user_showRequest() {

                var userName = $("#usersLogin").val();
                if (userName == "") {
                    $("#promptDIV").removeClass("alert alert-error alert-success");
                    $("#promptDIV").addClass("alert alert-error");
                    $("#promptDIV").html("用户名称不能为空!");
                    return false;
                }
            }

            //form提交相应成功后执行的回调函数
            function user_showResponse(responseText, statusText) {
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
                url: "/UsersManagement/GetUserListByLogin?userlogin=" + key,
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    var dataSearchJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    $("#usersgrid").ligerGrid({
                        columns: [
                        { display: '用户ID', name: 'id', width: 80, align: 'center' },
                        { display: '登录名称', name: 'login', align: 'center' },
                        { display: '用户姓名', name: 'name', align: 'center' },
                        { display: '工号', name: 'employee_no', align: 'center' },
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
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteUser(' + row.id + ')">删除</a>';
                                return html;
                            }
                        },
                       { display: '', width: 100,
                           render: function (row) {
                               var html = '<i class="icon-user"></i><a href="/UsersManagement/UserRoles?id=' + row.id + '">角色设置</a>';
                               return html;
                           }
                       }
                       ],
                        data: dataSearchJson,
                        newPage: 1
                    });
                    $("#usersgrid").ligerGetGridManager().loadData();
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>用户管理</h2></div>

    <div class="container">
      <%--操作提示DIV--%>
      <div id="promptDIV" class="row"></div>
    </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllUsers" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddUsers" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
  
  <div class="tab-content"> 
      
       <div class="tab-pane active" id="AllUsers">
       <%--查询按钮--%> 
       <b>登录名称:</b><input id="txtKey" type="text" class="input-medium search-query span3"/>
       <input id="btnOK" type="button" value="查询" onclick="search()"/> 
       <hr />  
       <%--查看所有用户--%>
        <div id="usersgrid"></div>
       </div>

       <%--添加用户--%>
       <div class="tab-pane" id="AddUsers">
          <form id="add_Users" class="form-horizontal" method="post" action="">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersLogin">登录名称：</label>
                        <div class="controls">
                            <input type="text" name="usersLogin" id="usersLogin" class="input-prepend span4" placeholder="登录名称"/>                            
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersPassword">登录密码：</label>
                        <div class="controls">
                            <input type="password" name="usersPassword" id="usersPassword" class="input-prepend span4" placeholder="登录密码"/>                            
                        </div>
                    </div> 
                     <div class="control-group span6 offset2">
                            <label class="control-label" for="passwordcon">确认密码：</label>
                            <div class="controls">
                                <input name="passwordcon" id="passwordcon" type="password" class="input-prepend span4" placeholder="确认密码"/>
                            </div>
                        </div>
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="usersName">用户姓名：</label>
                        <div class="controls">
                            <input type="text" name="usersName" id="usersName" class="input-prepend span4" placeholder="用户姓名"/>                            
                        </div>
                    </div>  
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="usersEmployee_no">工号：</label>
                        <div class="controls">
                            <input type="text" name="usersEmployee_no" id="usersEmployee_no" class="input-prepend span4" placeholder="工号"/>                            
                        </div>
                    </div> 
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersMobile_phone">手机号：</label>
                        <div class="controls">
                            <input type="text" name="usersMobile_phone" id="usersMobile_phone" class="input-prepend span4" placeholder="手机号"/>                            
                        </div>
                    </div> 
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="usersMail">邮件：</label>
                        <div class="controls">
                            <input type="text" name="usersMail" id="usersMail" class="input-prepend span4" placeholder="邮件"/>                            
                        </div>
                    </div>      
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersRemark">备注：</label>
                        <div class="controls">
                            <textarea name="usersRemark" id="usersRemark" rows="4" cols="5" class="span4" placeholder="备注"></textarea>
                            <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
                            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                            <%string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                            <%DateTime t = Convert.ToDateTime(s);%>
                            <input type="hidden" name="usersApp_id" id="usersApp_id" value=""/>  
                            <input type="hidden" name="usersAdmin" id="usersAdmin" value="false"/>
                            <input type="hidden" name="usersCreated_by" id="usersCreated_by" value="<%=m_usersModel.id%>" />
                            <input type="hidden" name="usersCreated_ip" id="usersCreated_ip" value="<%=ipAddress%>" />
                            <input type="hidden" name="usersCreated_at" id="usersCreated_at" value="<%=t%>"/>
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
