
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<%--本页用到的CSS/JS--%>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
   <%--<link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="Stylesheet" type="text/css"/>--%>
   <link href="../../LigerUI/lib/ligerUI/skins/ligerui-icons.css" rel="Stylesheet" type="text/css"/>
   <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
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
       var PageName = "流程管理";
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
  
   <%--在Grid中显示flows信息--%>
    <script type="text/javascript">
        var managerListGrid;
        var dataJson;
        $(document).ready(function () {

            //去掉  大于小于包括,并改变顺序
            $.ligerDefaults.Filter.operators['string'] =
            $.ligerDefaults.Filter.operators['text'] =
            ["like", "equal", "notequal", "startwith", "endwith"];

            GetFlowList(); //获取流程数据列表

            $("#infoTab").click(function () {
                GetFlowList(); //获取流程数据列表
            });

        });
        //流程数据列表
        function GetFlowList() {
            $.ajax({
                url: "/FlowsManagement/GetFlowList",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {

                    dataJson = eval("(" + responseText + ")");

                    window['g'] = $("#flowsgrid").ligerGrid({
                        width: '99%',
                        height: 400,
                        checkbox: false,
                        columns: [
                            { display: '流程ID', name: 'id', align: 'left' },
                            { display: '流程名称', name: 'name', align: 'left' },
                            { display: '备注', name: 'remark', align: 'left' },
                            { display: '', width: 100,
                                render: function (row) {
                                    var html = '<i class="icon-list"></i><a href="/FlowsManagement/DetailInfo?id=' + row.id + '" onclick="DetailConfirmCon(' + row.id + ');">详情</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 100,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="/FlowsManagement/EditPage?id=' + row.id + '" onclick="EditPageCon(' + row.id + ');">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 100,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteFlow(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            }
                           ],
                        data: dataJson,
                        toolbar: { items: [{ text: '高级自定义查询:', click: itemclick, icon: 'search2'}] }
                    });

                    g.loadData();
                    $("#flowsgrid").ligerGetGridManager().loadData();

                    function itemclick() {
                        g.options.data = dataJson;
                        g.showFilter();
                    }

                }

            });
        }

       
   </script>

   <%--在Grid中分页显示flows信息--%>
    <script type="text/javascript">
        $(document).ready(function () {       
           window['t']=$("#AllFlows").ligerGrid({
                columns: [
                            { display: '流程ID', name: 'id', align: 'left' },
                            { display: '流程名称', name: 'name', align: 'left' },
                            { display: '备注', name: 'remark', align: 'left' },
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
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteFlow(' + row.id + ')">删除</a>';
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
                            url: "/FlowsManagement/GetFlow_List"
                            
                            
            });
          
        });

    </script>

    <%--编辑确认函数--%>
    <script type="text/javascript">
        function EditPageCon(id) {
            var flowid = id;
            var Count = t.get('page');
            var Size = t.get('pageSize');

            $.ajax({
                url: "/FlowsManagement/EditPageCon",
                type: "POST",
                dataType: "json",
                data: { flowID: flowid, pageCount: Count, SizeCount: Size }
                
            });
        }
    </script>
    <%--编辑弹出框函数--%>
    <script type="text/javascript">
        function EditDialog(id) {
            if (id) {
                $.ligerDialog.open({
                    title: '更新流程信息',
                    width: 800,
                    height: 700,
                    url: '/FlowsManagement/EditPage?id='+id,
                   
                });
            }
        }
    </script>
    <%--详情确认函数--%>
    <script type="text/javascript">
        function DetailConfirmCon(id) {
            var flowid = id;
            var Count = t.get('page');
            var Size = t.get('pageSize');
            alert("id:" + flowid);
            alert("Count:" + Count);
            alert("Size:"+Size);
            $.ajax({
                url: "/FlowsManagement/DetailConfirmCon",
                type: "POST",
                dataType: "json",
                data: { flowID: flowid, pageCount: Count, SizeCount: Size }
            });
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
                   url:'/FlowsManagement/DetailInfo?id='+id,
                 });
            }
        
        }
 
    </script>
    <%--删除确认函数--%>
    <script type="text/javascript">
        function DeleteFlow(id) {
            //alert(id);
            //alert(g.get('total'));
            var flowid = id;
            $.ligerDialog.confirm("确定要删除信息?", function (yes) {
                if (yes) {
                    $.ajax({
                        url: "/FlowsManagement/DeleteFlows",
                        type: "POST",
                        dataType: "json",
                        data: { flowID: flowid },
                        success: function (responseText, statusText) {
                            GetFlowList();
                            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                            $("#promptDIV").addClass(responseText.css);
                            $("#promptDIV").html(responseText.message);
                            if (responseText.success) {
                                location.href = responseText.toUrl;
                            }
                        }
                    });
                }
            });
        }
    </script>
    <%--添加流程确认信息--%>
   <%-- <script type="text/javascript">
        $(document).ready(function () {

            var form = $("#add_Flows");

            form.submit(function () {
                if ($.trim($("#flowsName").val()).length == 0) {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-errorDIV");
                    $("#promptDIV").html("流程名称不能为空！");

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
                        alert(result.success);
                        if (result.success) {
                            location.href = result.toUrl;
                        }
                    },
                    "JSON");
                    return false;
                }
            });
        });
    </script>--%>

       <script type="text/javascript">
           $(document).ready(function () {
               var form = $("#add_Flows");
               form.submit(function () {
                   $.post(form.attr("action"),
                    form.serialize(),
                    function (result, status) {
                        //debugger
                        $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                        $("#promptDIV").addClass(result.css);
                        $("#promptDIV").html(result.message);
                        //alert(result.success);
                        if (result.success) {

                            location.href = result.toUrl;
                        }
                    },
                    "JSON");
                   return false;

               });
           });
    </script>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/FlowsManagement/AddFlows",
                type: "POST",
                dataType: "json"
            };
            $("#submit").click(function () {
                if (false) {
                    return false;
                } else {
                    $("add_Flows").ajaxForm(options);
                }
            });

            function showResponse(responseText, statusText) {

                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
                if (responseText.success) {
                    location.href = responseText.toUrl;
                }

            }
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container"><h2>流程管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
    </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#AllFlows" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddFlows" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
    
    <div class="tab-content">
    
    <%--查看流程列表--%>
    <%-- <div class="l-loading" style="display:block" id="pageloading"></div>--%>
     <div class="tab-pane active" id="AllFlows">
     </div>

     <%--添加流程--%>
     <div class="tab-pane" id="AddFlows">
                <form id="add_Flows" class="form-horizontal" method="post" action="/FlowsManagement/AddFlows">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="flowsName">流程名称：</label>
                        <div class="controls">
                            <input type="text" name="flowsName" id="flowsName" class="input-prepend span4"/>                                                     
                        </div>
                    </div>
                          
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="flowsRemark">备注：</label>
                        <div class="controls">
                        <textarea name="flowsRemark" id="flowsRemark" rows="4" cols="5" class="span4"></textarea>
                        <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)(Session["user"]);%>
                            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                            <%string dt = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                            <%DateTime t = Convert.ToDateTime(dt);%>
                        <input  type="hidden" id="flowsCreated_at" name="flowsCreated_at" value="<%=t%>"/>
                       </div>
                    </div>

                    <div class="control-group span6 offset3">
                        <div class="controls">
                            <input  type="submit" value="添加" class="btn btn-primary  span1" /> 
                            &nbsp;&nbsp;&nbsp;
                            <input type="reset" value="重置"  class="btn btn-primary  span1" />
                        </div>
                    </div>

              </form>
     </div>
    </div>
</asp:Content>
