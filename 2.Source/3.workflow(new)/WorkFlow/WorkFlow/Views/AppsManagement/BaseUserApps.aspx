<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/supersite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">

     <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
     <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
   <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
    <%--<link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css"/>--%>
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
        var PageName = "系统总览";
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
    
    <%--统计待审批、已审批系统的数量--%>
    <script type="text/javascript">

        var Name;//应用系统的名称

        $(document).ready(function () {
            ShowAppsCount();//显示待审批、已审批系统的数量
        });

        function ShowAppsCount() {
            var options = {
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/AppsManagement/InvalidAppsCount",
                type: "POST",
                dataType: "json"
            };
            $.ajax(options);
        }

        function showResponse(responseText, statusText) {
            //alert(responseText);
            var dataJson = eval("(" + responseText + ")");
            $("#invalidTab").html("待审批系统（" + dataJson.invalidCount + "）");  //待审批
            $("#validTab").html("已审批系统（" + dataJson.validCount + "）"); //已审批
      
            return false;
        }
    </script>

   
    <%--显示审批系统列表--%>
    <script type="text/javascript">
        $(document).ready(function () {

            GetValidAppsList(); //已审批系统

            //已审批系统
            function GetValidAppsList() {
                window['v'] = $("#validgrid").ligerGrid({
                    columns: [
                    { display: '系统ID', name: 'id', width: 150, align: 'center' },
                    { display: '系统名称', name: 'name', width: 150, align: 'center' },
                    { display: '系统编码', name: 'code', width: 150, align: 'center' },
                    { display: '访问连接', name: 'url', width: 150, align: 'center' },
                    { display: '备注信息', name: 'remark', width: 180, type: 'int', align: 'center' },
                    { display: '', width: 100,
                        render: function (row) {
                            var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="VDetailDialog(' + row.id + ')">详情</a>';
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
                    url: "/AppsManagement/AppsValidList"
                });
                v.loadData();
            }

            GetInvalidAppsList(); //待审批系统

            //待审批系统
            function GetInvalidAppsList() {
                window['i'] = $("#invalidGrid").ligerGrid({
                    columns: [
                    { display: '系统ID', name: 'id', width: 150, align: 'center' },
                    { display: '系统名称', name: 'name', width: 150, align: 'center' },
                    { display: '系统编码', name: 'code', width: 150, align: 'center' },
                    { display: '访问连接', name: 'url', width: 150, align: 'center' },
                    { display: '备注信息', name: 'remark', width: 180, type: 'int', align: 'center' },
                    { display: '', width: 100,
                        render: function (row) {
                            var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="IDetailDialog(' + row.id + ')">详情</a>';
                            return html;
                        }
                    },

                     { display: '', width: 100,
                         render: function (row) {
                             var html = '<i class="icon-list"></i><a href="#" onclick="ApprovalDialog(' + row.id + ')">审批</a>';
                             
                             return html;
                         }
                     },
                    { display: '', width: 100,
                        render: function (row) {
                            var html = '<i class="icon-trash"></i><a href="#" onclick="Delete(' + row.id + ')">删除</a>';              
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
                    url: "/AppsManagement/AppsInvalidList"
                });
                i.loadData();
            }

            //已审批Tab
            $("#validTab").click(function () {
                GetValidAppsList();
                ShowAppsCount();
            });

            //待审批Tab
            $("#invalidTab").click(function () {
                GetInvalidAppsList();
                ShowAppsCount();
            });
        });
    </script>

    <%--已审批详情弹出框函数--%>
    <script type="text/javascript">
      function VDetailDialog(id) {

          if (id) {
              $.ligerDialog.open({
                  title: '详情(' + id + ')信息',
                  isDrag: true,
                  width: 700,
                  height: 600,
                  url: '/AppsManagement/BU_ApprovalInfo?id=' + id
              });
            
          }
      }
  </script>

    <%--待审批详情弹出框函数--%>
    <script type="text/javascript">
           function IDetailDialog(id) {

               if (id) {
                   $.ligerDialog.open({
                       title: '详情(' + id + ')信息',
                       isDrag: true,
                       width: 700,
                       height: 600,
                       url: '/AppsManagement/BU_ApprovalApps?id=' + id
                   });
                 
               }
           }
  </script>

    <%--待审批列表的删除提示框--%>
    <script type="text/javascript">
        function Delete(id) {
              var ID=id;
              $.ligerDialog.confirm('确认要删除吗?', function (yes) {
                  if (yes) {
                      $.ajax({
                          url: "/AppsManagement/Delete",
                          type: "POST",
                          dataType: "json",
                          data: { appID: ID },
                          success: function (responseText, statusText) {
                              var dataJson = eval("(" + responseText + ")");
                              show_DIV(dataJson);

                              i.loadData();
                              ShowAppsCount();
                          }
                      });

                      //删除提示
                      function show_DIV(data) {
                          $("#promptDIV").removeClass("alert alert-error alert-success");
                          $("#promptDIV").addClass(data.css);
                          $("#promptDIV").html(data.message);
                      }
                  }
              });

          }
  </script>

    <%--审批流程--%>
    <script type="text/javascript">

        function ApprovalDialog(id) {      
            var ID = id;
               
                $.ligerDialog.confirm('确认要审批系统ID号为:' +id+ '吗?', function (yes) {
                    if (yes) {
                        $.ajax({
                            url: "/AppsManagement/ApprovalApps",
                            type: "POST",
                            dataType: "json",
                            data: { appID: ID},
                            success: function (responseText, statusText) {
                                var dataJson = eval("(" + responseText + ")");
                                show_DIV(dataJson);

                                i.loadData();
                                ShowAppsCount();
                            }
                        });

                        //删除提示
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
<div class="container">
    <div class="row">
        <h2 class="span3">系统管理</h2>      
    </div>
    <div class="container">
         <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>
</div>

<div class="container" style="margin-top:16px;">
   
    <ul class="nav nav-tabs">
        <li class="active"><a id="validTab" href="#Apps_valid" data-toggle="tab">已审批系统</a></li>
        <li><a id="invalidTab" href="#Apps_Invalid" data-toggle="tab">待审批系统</a></li>             
     </ul>
  
    <div class="tab-content">
        <%--系统中运行中的系统--%>
        <div class="tab-pane active" id="Apps_valid">
            <div id="validgrid"></div>
        </div>

        <%--审批中的系统申请--%>
        <div class="tab-pane" id="Apps_Invalid">
            <div id="invalidGrid"></div>
        </div>         
    </div>

</div>

</asp:Content>
