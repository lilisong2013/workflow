<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AppElements
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>

    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <%--在Grid中显示Element信息--%>
     <script type="text/javascript">
         $(document).ready(function () {
             $("#AllElements").ligerGrid({
                 columns: [
                { display: '元素名称', name: 'name', width: 100 },
                { display: '元素编码', name: 'code', width: 100 },               
                { display: '', width: 260,
                    render: function (row) {
                        var html = '<i class="icon-lock"></i><a href="/ElementsManagement/DetailInfo?id=' + row.id + '">详情</a><i class="icon-trash"></i><a href="/ElementsManagement/ChangePage?id=' + row.id + '">删除</a><i class="icon-edit"></i><a href="/ElementsManagement/EditPage?id=' + row.id + '">编辑</a>';
                        return html;
                    }
                }
                ],
                 dataAction: 'server',
                 width: '90%',
                 pageSizeOptions: [5, 10, 15, 20, 25, 50],
                 pageSize: 15,
                 height: '400',
                 url: '/ElementsManagement/GetElements_Apply',
                 rownumbers: true,
                 usePager: true
             });

         });
 </script>

 <script type="text/javascript">
     $(document).ready(function () {
         BindStatus();
         $("#StatusInfo").html("请选择");
     });
     function BindStatus() {
         $.ajax({
             type: "Post",
             contentType: "application/json",
             url: "/ElementsManagement/GetStatusName",
             data: {}, //即使参数为空，也需要设置
             dataType: 'JSON', //返回的类型为XML
             success: function (result, status) {
                 //成功后执行的方法
                 try {
                     if (status == "success") {
                         for (var i = 0; i < result.Total; i++) {
                             $("#StatusParent").append("<option value='" + result.Rows[i].InitStatusID + "'>" + result.Rows[i].InitStatusName + "</option>");
                         }
                     }
                 } catch (e)
               { }
             }
         });
     }
 </script>

 <script type="text/javascript">
     $(document).ready(function () {
         BindMenuName();
         $("#MenuInfo").html("请选择");
     });
     function BindMenuName() {
         $.ajax({
             type: "Post",
             contentType: "application/json",
             url: "/ElementsManagement/GetMenusName",
             data: {}, //即使参数为空，也需要设置
             dataType: 'JSON', //返回的类型为XML
             success: function (result, status) {
                 //成功后执行的方法
                 try {
                     if (status == "success") {
                         for (var i = 0; i < result.Total; i++) {
                             $("#MenuParent").append("<option value='" + result.Rows[i].menusID + "'>" + result.Rows[i].menusName + "</option>");
                         }
                     }
                 } catch (e)
               { }
             }
         });
     }
 </script>

     <%--添加菜单选项卡：菜单树的显示与隐藏--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#selectMenus").hide();

            $("#MenusParent").click(function () {
                if ($("#selectMenus").is(":hidden")) {
                    $("#selectMenus").show();
                }
                else {
                    $("#selectMenus").hide();
                }
            });
        });
    </script>

    <%--菜单树的数据显示--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var datas;
            var dataJson;

            $.ajax({
                url: "/MenusManagement/GetMenus",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    datas = responseText; //获取到的菜单json格式字符串
                    dataJson = eval(datas); //将json字符串转化为json数据
                    //显示菜单树
                    $("#tree1").ligerTree({ data: dataJson, checkbox: false, textFieldName: 'name', onSelect: onSelect });
                }
            });
            //选择节点将父菜单信息赋给menusInfo
            function onSelect(note) {
                $("#menusInfo").val(note.data.id);
                $("#menusInfo").html(note.data.name);
            }
        });        
    </script>

    <%--添加菜单--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/MenusManagement/AddMenus",
                type: "POST",
                dataType: "json"
            };

            $("#submit").click(function () {
                if (false) {

                    return false;
                } else {
                    $("#add_Menus").ajaxForm(options);
                }
            });

            function showResponse(responseText, statusText) {
                //成功后执行的方法
                //alert(responseText.Id + responseText.Name);
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
            }
        });
    </script>

           <script type="text/javascript">
               $(document).ready(function () {
                   var form = $("#add_Menus");
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

    <div class="container"><h2>元素管理</h2></div>
    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllElements" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddElements" data-toggle="tab"><i class="icon-adjust"></i>添加</a></li>
        </ul>
    </div>
      <% string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
      <% string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
      <% DateTime t = Convert.ToDateTime(s); %>
     <div class="container">
     <%--操作提示DIV--%>
     <div id="promptDIV" class="row"></div>
     </div>
   <div class="tab-content">
     <div class="tab-pane active" id="AllElements"> </div>  
     <div class="tab-pane" id="AddElements">
            <form id="add_Menus" class="form-horizontal" method="post" action="/ElementsManagement/AddElements">
                <div class="control-group span6 offset2">
                    <label class="control-label">页面元素名称</label>
                    <div class="controls">
                        <input id="elementsName" name="elementsName" type="text" class="input-prepend span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">页面元素编码</label>
                    <div class="controls">
                        <input id="elementsCode" name="elementsCode" type="text" class="input-prepend span4" />
                    </div>
                </div>             
                <div class="control-group span6 offset2">
                    <label class="control-label">初始化状态</label>
                    <div class="controls">
                        <select class="span4" id="StatusParent" name="StatusParent">
                         <option id="StatusInfo"></option>
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">父菜单</label>
                    <div class="controls">
                        <select id="MenusParent" name="MenusParent" class="span4">
                            <option id="menusInfo"></option>
                        </select>
                    </div>
                </div>
                <div id="selectMenus" class="control-group span6 offset2">
                    <div class="controls">
                        <ul id="tree1"></ul>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">排序码</label>
                    <div class="controls">
                        <input id="elementsSeqno" name="elementsSeqno" type="text" class="span4" />
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <label class="control-label">备注信息</label>
                    <div class="controls">
                        <textarea id="elementsRemark" name="elementsRemark" rows="4" cols="5" class="span4"></textarea>
                        <%WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>                   
                        <input type="hidden" id="elementsApp_id" name="elementsApp_id" value="<%=m_userModel.app_id%>"/>                     
                        <input type="hidden" id="Created_at" name="Created_at" value="<%=t %>"/>               
                        <input type="hidden" id="Created_by" name="Created_by" value="<%=m_userModel.id%>"/>
                        <input type="hidden" id="Created_ip" name="Created_ip" value="<%=ipAddress%>"/>           
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



