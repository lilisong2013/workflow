<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/supersite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

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
        $(document).ready(function () {
            ShowAppsCount();//显示待审批、已审批系统的数量
        });

    function ShowAppsCount() {
        var options = {
            //beforeSubmit: showRequest,  // from提交前的响应的回调函数
            success: showResponse,  // form提交响应成功后执行的回调函数
            url: "/AppsManagement/InvalidAppsCount",
            type: "POST",
            dataType: "json"
        };

        $.ajax(options);
    }

    function showResponse(responseText, statusText) {
        //成功后执行的方法
        //alert(responseText);
        var dataJson = eval("(" + responseText + ")");

        $("#invalidTab").html("待审批系统（" + dataJson.invalidCount + "）");  //待审批
        $("#validTab").html("已审批系统（" + dataJson.validCount + "）"); //已审批

        return false;
    } 
    </script>

    <script type="text/javascript">
        var managerValidGrid;
        var managerInvalidGrid;
        $(document).ready(function () {
            //定义已审批系统ligerGrid
            $("#validgrid").ligerGrid({
                width: '99%',
                height: 400
            });
            managerValidGrid = $("#validgrid").ligerGetGridManager();

            //定义待审批系统ligerGrid
            $("#invalidGrid").ligerGrid({
                width: '99%',
                height: 400
            });
            managerInvalidGrid = $("#invalidGrid").ligerGetGridManager();

            GetValidAppsList(); //初始化页面，获取数据列表

            $("#validTab").click(function () {
                GetValidAppsList(); //切换选项卡，获取数据列表
                ShowAppsCount(); //显示待审批、已审批系统的数量
            });

            $("#invalidTab").click(function () {
                GetInvalidAppsList(); //切换选项卡，获取数据列表
                ShowAppsCount(); //显示待审批、已审批系统的数量
            });
        });

        function GetValidAppsList() {
            $.ajax({
                url: "/AppsManagement/AppsValidDSToJSON",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    
                    //更新mygrid数据
                    managerValidGrid.setOptions({
                        columns: [{ display: '系统名称', name: 'name', align: 'center' },
                                  { display: '系统编码', name: 'code',  align: 'center' },
                                  { display: '访问连接', name: 'url',  align: 'center' },
                                  { display: '备注信息', name: 'remark', type: 'int', align: 'center' },
                                  { display: '', width: 100,
                                      render: function (row) {
                                          var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="VDetailDialog('+row.id+')">详情</a>';
                                          return html;
                                      }
                                  }
                                 ],
                        data: dataJson
                    });
                    managerValidGrid.loadData();
                }
            });
        }

        function GetInvalidAppsList() {
            $.ajax({
                url: "/AppsManagement/AppsInvalidDSToJSON",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //alert(responseText);
                    var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据

                    //更新mygrid数据
                    managerInvalidGrid.setOptions({
                        columns: [{ display: '系统名称', name: 'name', width: 150, align: 'center' },
                                  { display: '系统编码', name: 'code', width: 150, align: 'center' },
                                  { display: '访问连接', name: 'url', width: 150, align: 'center' },
                                  { display: '备注信息', name: 'remark', width: 180, type: 'int', align: 'center' },
                                  { display: '', width: 100,
                                      render: function (row) {
                                          var html = '<i class="icon-list"></i><a href="javascript:void(0);" onclick="IDetailDialog('+row.id+')">详情</a>';
                                          return html;
                                      }
                                  },
                                  { display: '', width: 100,
                                      render: function (row) {
                                          var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteApp(' + row.id + ')">删除</a>';
                                          return html;
                                      }
                                  }
                                 ],
                        data: dataJson
                    });
                    managerInvalidGrid.loadData();
                }
            });
        }

    
        //删除弹出框
        function DeleteApp(appid) {
            //alert(appid);
            var app_id = appid;
            $.ligerDialog.confirm('确认要删除吗?', function (yes) {
                if (yes) {
                    $.ajax({
                        url: "/AppsManagement/DeleteApp",
                        type: "POST",
                        dataType: "json",
                        data: { appID: appid },
                        success: function (responseText, statusText) {
                            GetInvalidAppsList(); //重载待审批系统数据列表
                            ShowAppsCount(); //显示待审批、已审批系统的数量
                            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                            $("#promptDIV").addClass(responseText.css);
                            $("#promptDIV").html(responseText.message);
                        }
                    });
                }
            });
          
        }
    </script>

  <%--待审批详情弹出框函数--%>
  <script type="text/javascript">
      function IDetailDialog(id) {

          if (id) {
              $.ligerDialog.open({
                title: '详情(' + id + ')信息',
                isDrag: true,
                width:700,
                height:600,
                url: '/AppsManagement/BU_ApprovalApps?id='+id
              });
          }
      }
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <% WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"]; %>
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


