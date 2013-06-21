<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/appsadmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
AppUsers
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">

    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--LigerUI Dialog文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css"/>
   <%-- <link href="../../LigerUI/lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css"/>--%>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
  
    
    <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>

    <%--在Grid中显示user信息--%>
    <script type="text/javascript">
            var managerListGrid;
            $(document).ready(function () {
                //定义ligerGrid;
                $("#usersgrid").ligerGrid({
                    width: '90%',
                    height: 400
                });
                managerListGrid = $("#usersgrid").ligerGetGridManager();
                GetUsersList(); //获取用户数据列表
                $("#infoTab").click(function () {
                    GetUsersList(); //获取用户数据列表
                });
            });
            function GetUsersList() {
                $.ajax({
                    url: "/UsersManagement/GetUsers_Apply",
                    type: "POST",
                    dataType: "json",
                    data: {},
                    success: function (responseText, statusText) {
                       // alert(responseText);
                        
                        var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                       
                        //更新mygrid数据
                        managerListGrid.setOptions({
                         columns:[
                        { display: '登录名称',name:'login', width: 80, align: 'center' },
                        { display: '用户姓名',name:'name', width: 80, align: 'center' },
                        { display: '工号',name:'employee_no', width: 80, align: 'center' },
                        { display: '', width: 180,
                            render: function (row) {
                                var html = '<i class="icon-lock"></i><a href="/UsersManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-edit"></i><a href="/UsersManagement/EditPage?id=' + row.id + '">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 80,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteUser('+row.id+')">删除</a>';
                                return html;
                            }
                        },
                       { display: '', width: 200,
                           render: function (row) {
                               var html = '<i class="icon-edit"></i><a href="/UsersManagement/UserRoles?id=' + row.id + '">角色设置</a>';
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

    <script type="text/javascript">
        function DeleteUser(id) {
            //alert(id);
            var userid = id;           
            $.ligerDialog.confirm('确定要删除吗?', function (yes) {
                //return true;
                $.ajax({
                    url: "/UsersManagement/DeleteUser",
                    type: "POST",
                    dataType: "json",
                    data: { userID: userid },
                    success: function (responseText, statusText) {
                        GetUsersList();
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(responseText.css);
                        $("#promptDIV").html(responseText.message);
                    }
                });
            })
        }
    </script>
  
    <script type="text/javascript">
        $(document).ready(function () {
            var form = $("#add_Users");
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>用户管理</h2></div>
<div class="container">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllUsers" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddUsers" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
 </div>
  <div class="container">
  <%--操作提示DIV--%>
  <div id="promptDIV" class="row"></div>
  </div>
  <div class="tab-content"> 
       <%--查看所有用户--%>
       <div class="tab-pane active" id="AllUsers">
        <div id="usersgrid"></div>
       </div>

       <%--添加用户--%>
       <div class="tab-pane" id="AddUsers">
          <form id="add_Users" class="form-horizontal" method="post" action="/UsersManagement/AddUsers">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersLogin">登录名称：</label>
                        <div class="controls">
                            <input type="text" name="usersLogin" id="usersLogin" class="input-prepend span4"/>                            
                        </div>
                    </div>
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersPassword">登录密码：</label>
                        <div class="controls">
                            <input type="password" name="usersPassword" id="usersPassword" class="input-prepend span4"/>                            
                        </div>
                    </div> 
                     <div class="control-group span6 offset2">
                            <label class="control-label" for="passwordcon">确认密码：</label>
                            <div class="controls">
                                <input name="passwordcon" id="passwordcon" type="password" class="input-prepend span4" />
                            </div>
                        </div>
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="usersName">用户姓名：</label>
                        <div class="controls">
                            <input type="text" name="usersName" id="usersName" class="input-prepend span4"/>                            
                        </div>
                    </div>  
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="usersEmployee_no">工号：</label>
                        <div class="controls">
                            <input type="text" name="usersEmployee_no" id="usersEmployee_no" class="input-prepend span4"/>                            
                        </div>
                    </div> 
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersMobile_phone">手机号：</label>
                        <div class="controls">
                            <input type="text" name="usersMobile_phone" id="usersMobile_phone" class="input-prepend span4"/>                            
                        </div>
                    </div> 
                     <div class="control-group span6 offset2">
                        <label class="control-label" for="usersMail">邮件：</label>
                        <div class="controls">
                            <input type="text" name="usersMail" id="usersMail" class="input-prepend span4"/>                            
                        </div>
                    </div>      
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="usersRemark">备注：</label>
                        <div class="controls">
                            <textarea name="usersRemark" id="usersRemark" rows="4" cols="5" class="span4"></textarea>
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
                            <input type="submit" value="添加" class="btn btn-primary  span1" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="reset" value="重置"  class="btn btn-primary  span1" />
                        </div>
                    </div>
              </form>
       </div>
   </div>

</asp:Content>
