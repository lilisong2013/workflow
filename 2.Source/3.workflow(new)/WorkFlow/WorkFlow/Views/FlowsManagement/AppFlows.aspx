﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<%--本页用到的CSS/JS--%>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
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
       var PageName = "流程管理";
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
  
   <%--在Grid中分页显示flows信息--%>
   <script type="text/javascript">
        $(document).ready(function () {

            $("#infoTab").click(function () {//切换Tab标签时获取流程列表
               
                GetFlowList();
            })

            GetFlowList(); //获取流程列表

            function GetFlowList() {
                window['t'] = $("#AllFlows").ligerGrid({
                    columns: [
                            { display: '流程ID', name: 'id', align: 'center', width: 80 },
                            { display: '流程名称', name: 'name', align: 'center' },
                            { display: '备注信息', name: 'remark', align: 'center' },
                            { display: '是否有效', name: 'invalid', align: 'center',
                                render: function (item) {
                                    if (item.invalid == true) {
                                        return '<span>否</span>';
                                    } else if (item.invalid == false) {
                                        return '<span>是</span>';
                                    }
                                }
                             },
                            { display: '', width: 80,
                                render: function (row) {
                                    var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 80,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 80,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteFlow(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 120,
                                render: function (row) {
                                    var html = '<i class="icon-user"></i><a href="#" onclick="FlowStep(' + row.id + ')">流程步骤维护</a>';
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
                    alternatingRow: false,
                    url: "/FlowsManagement/GetFlow_List",
                    rowAttrRender: function (row, rowid) {
                        if (row.invalid == true) {
                            return "style='background:#F1D3F7;'";
                        }
                        return "";
                    }

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
                    title: '更新流程信息',
                    width: 900,
                    height: 600,
                    isDrag: false,
                    url: '/FlowsManagement/EditPage?id=' + id,
                    buttons:
                    [
                    {text:'返回',onclick:function(item,dialog){t.loadData();dialog.close();}}                                   
                   
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
                    width: 700,
                    height: 600,
                    isDrag: false,
                    url: '/FlowsManagement/DetailInfo?id=' + id
                });
            }
        
        }
 
    </script>

   <%--流程步骤弹出框函数--%>
   <script type="text/javascript">
       function FlowStep(id) {

           if (id) {
               $.ligerDialog.open({
                   title: '流程步骤维护',
                   width: 1300,
                   height: 800,
                   isDrag: false,
                   url: '/FlowsManagement/FlowSteps?id=' + id,
                   buttons:
                   [
                    { text: '返回', onclick: function (item, dialog) { t.loadData(); dialog.close(); } }

                   ]
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
            });
        }
    </script>

   <%--添加流程信息--%>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#addSave").click(function () {
               if (false) {
                   return false;
               }
               else {
                   AddFlows(); //添加流程信息
               }
           });

           //添加流程信息
           function AddFlows() {
               var options = {
                   beforeSubmit: flow_showRequest, //form提交前的响应的回调函数
                   success: flow_showResponse, //form提交相应成功后执行的回调函数
                   url: "/FlowsManagement/AddFlows",
                   type: "POST",
                   dataType: "json"
               };
               $("#add_Flows").ajaxForm(options);
           }

           //form提交前的响应的回调函数
           function flow_showRequest() {
               var flowName = $("#flowsName").val();
               if (flowName == "") {
                   $("#promptDIV").removeClass("alert alert-error alert-success");
                   $("#promptDIV").addClass("alert alert-error");
                   $("#promptDIV").html("流程名称不能为空!");
                   return false;
               }
           }

           //form提交相应成功后执行的回调函数
           function flow_showResponse(responseText, statusText) {
               var dataJson = eval("(" + responseText + ")");

               show_promptDIV(dataJson); //提示信息
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
       var pageCount;
       var sizeCount;
       function search() {
           
           key = $("#txtKey").val();

           GetSFlowList(); //根据搜索关键字，显示列表

           function GetSFlowList() {
               window['s'] = $("#AllFlows").ligerGrid({
                   columns: [
                            { display: '流程ID', name: 'id', align: 'center', width: 80 },
                            { display: '流程名称', name: 'name', align: 'center' },
                            { display: '备注信息', name: 'remark', align: 'center' },
                            { display: '是否有效', name: 'invalid', align: 'center',
                                render: function (item) {
                                    if (item.invalid == true) {
                                        return '<span>否</span>';
                                    } else if (item.invalid == false) {
                                        return '<span>是</span>';
                                    }
                                }
                            },
                            { display: '', width: 80,
                                render: function (row) {
                                    var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="DetailDialog(' + row.id + ')">详情</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 80,
                                render: function (row) {
                                    var html = '<i class="icon-edit"></i><a href="javascript:void(0);" onclick="EditDialog(' + row.id + ')">编辑</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 80,
                                render: function (row) {
                                    var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteFlow(' + row.id + ')">删除</a>';
                                    return html;
                                }
                            },
                            { display: '', width: 120,
                                render: function (row) {
                                    var html = '<i class="icon-user"></i><a href="#" onclick="FlowStep(' + row.id + ')">流程步骤维护</a>';
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
                   newPage: 1,
                   alternatingRow: false,
                   url: "/FlowsManagement/GetFlowName_List?flowname=" + key,
                   rowAttrRender: function (row, rowid) {
                       if (row.invalid == true) {
                           return "style='background:#F1D3F7;'";
                       }
                       return "";
                   }
               });
               s.loadData();
           }

       }
   </script>

   <%--查询后删除确认函数--%>
   <script type="text/javascript">
       function SDeleteFlow(id) {
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
                           var dataJson = eval("(" + responseText + ")");
                           //删除提示信息
                           alert(dataJson.success)
                           show_DIV(dataJson);
                           $("#AllFlows").ligerGetGridManager().loadData();
                           st.loadData();
                         

                       }
                   });
                   //删除提示信息
                   function show_DIV(data) {
                       $("#promptDIV").removeClass("alert alert-error alert-success");
                       $("#promptDIV").addClass(data.css);
                       $("#promptDIV").html(data.message);
                   }
               }
           });
       }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container"><h2>流程管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
    </div>
      <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)(Session["user"]);%>
    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"> <a href="#AllFlows1" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li><a href="#AddFlows" data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
    
    <div class="tab-content">
  
 
      <div class="tab-pane active" id="AllFlows1">

      <%--查询按钮--%> 
      <b>流程名称:</b><input id="txtKey" type="text" class="input-medium search-query span3"/>
      <input id="btnOK" type="button" value="查询" onclick="search()" class="btn btn-primary"/> 
      <hr />  
     
      <%--显示全部流程--%>
      <div id="AllFlows"></div>

      </div>
     <%--添加流程--%>
     <div class="tab-pane " id="AddFlows">
                <form id="add_Flows" class="form-horizontal" method="post" action="">
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="flowsName">流程名称：</label>
                        <div class="controls">
                            <input type="text" name="flowsName" id="flowsName" class="input-prepend span4" placeholder="流程名称" />                                                     
                        </div>
                    </div>
                          
                    <div class="control-group span6 offset2">
                        <label class="control-label" for="flowsRemark">备注信息：</label>
                        <div class="controls">
                        <textarea name="flowsRemark" id="flowsRemark" rows="4" cols="5" class="span4" placeholder="备注信息" maxlength="200"></textarea>
                                          
                            <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                            <%string dt = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
                            <%DateTime t = Convert.ToDateTime(dt);%>
                        <input  type="hidden" id="flowsCreated_at" name="flowsCreated_at" value="<%=t%>"/>
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
