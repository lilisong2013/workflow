<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
  
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>

    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet" type="text/css" />        
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>

    <%--LigerUI Dialog文件--%>

    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "元素管理";
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
    <%--在Grid中显示Element信息--%>
    <script type="text/javascript">
        var managerListGrid;
        $(document).ready(function () {
            //定义ligerGrid;
            $("#elementgrid").ligerGrid({
                width: '90%',
                height: 400
            });
            managerListGrid = $("#elementgrid").ligerGetGridManager();
            GetElementsList(); //获取元素数据列表
        });
        function GetElementsList() {
        $.ajax({
            url: "/ElementsManagement/GetElements_Apply",
            type: "POST",
            dataType: "json",
            data: {},
            success: function (responseText, statusText) {
                // alert(responseText);
                var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                //更新mygrid数据
                managerListGrid.setOptions({
                    columns: [
                        { display: '元素名称', name: 'name', align: 'center' },
                        { display: '元素编码', name: 'code', align: 'center' },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-list"></i><a href="/ElementsManagement/DetailInfo?id=' + row.id + '">详情</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-edit"></i><a href="/ElementsManagement/EditPage?id=' + row.id + '">编辑</a>';
                                return html;
                            }
                        },
                        { display: '', width: 100,
                            render: function (row) {
                                var html = '<i class="icon-trash"></i><a href="#" onclick="DeleteElement(' + row.id + ')">删除</a>';
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
   <%--删除信息确认函数--%>
    <script type="text/javascript">
        function DeleteElement(id) {
         //alert(id);
         var elementsId = id;
         $.ligerDialog.confirm('确定要删除吗?', function (yes) {
             //return true;
             if (yes) {
             $.ajax({
                 url: "/ElementsManagement/DeleteElement",
                 type: "POST",
                 dataType: "json",
                 data: { elementsID: elementsId },
                 success: function (responseText, statusText) {
                     GetElementsList();
                     $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                     $("#promptDIV").addClass(responseText.css);
                     $("#promptDIV").html(responseText.message);
                 }
             });
         }

       })
     }
 </script>
     
    <%--初始化状态选择操作--%>
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

 
    <%--添加元素的菜单树(数据)--%>
    <script type="text/javascript">
        var eManagerTree;
        //var eManagerGrid;
        $(document).ready(function () {
            $("#eMyTree").hide(); //初始化隐藏eMyTree树
            //初始化ligerTree
            $("#eMyTree").ligerTree({
                
                checkbox: false,
                textFieldName: 'name',
                onSelect: OnSelectMenusOfElements
            });
            eManagerTree = $("#eMyTree").ligerGetTreeManager();

            //切换Tab页面时重载mMyTree数据
            $("#elementsTab").click(function () {
                BindMenusListOfElements(); //mMyTree绑定数据
            });

            //下拉列表控件的点击事件
            $("#eElementPage").click(function () {
                if ($("#eMyTree").is(":hidden")) {
                    $("#eMyTree").show();
                } else {
                    $("#eMyTree").hide();
                }
            });

        });
      //选择元素所在页面后重载eMyGrid数据
        function OnSelectMenusOfElements(note) {
            //alert(note.data.id);
            $.ajax({
                url: "/PrivilegesManagement/ExistChildreMenus",
                type: "POST",
                dataType: "json",
                data: { menusID: note.data.id },
                success: function (responseText, statusText) {
                    //alert(responseText.success);
                    if (responseText.success) {
                        $("#eElementPageInfo").val("-1");
                        $("#eElementPageInfo").html("选择页面");
                    } else {
                        $("#eElementPageInfo").val(note.data.id);
                        $("#eElementPageInfo").html(note.data.name);
                        BindElementsList(note.data.id);
                    }
                }
            });
        }
   //加载eMyTree树的数据
   function BindMenusListOfElements() {
       //alert("BindMenusListOfElements???--");
       $.ajax({
           url: "/PrivilegesManagement/GetMenusOfItem",
           type: "POST",
           dataType: "json",
           data: {},
           success: function (responseText, statusText) {
               //alert(responseText);
               var dataMenusJson = eval(responseText); //将json字符串转化为json数据
               eManagerTree.clear();
               eManagerTree.setData(dataMenusJson);
               eManagerTree.loadData();
           }
       });
   }
   //加载表格eMyGrid的数据
   function BindElementsList(pageID) {
       //alert(pageID);
      // alert("BindElementsList???ok---");
       $.ajax({
           url: "/PrivilegesManagement/GetElementOfItem",
           type: "POST",
           dataType: "json",
           data: { menusID: pageID },
           success: function (responseText, statusText) {
               //alert(responseText);
               var dataElementsJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
               //更新eMyGrid数据
               eManagerGrid.setOptions({
                   columns: [
                            { display: '页面元素名称', name: 'name', width: 120 },
                            { display: '页面元素编码', name: 'code', width: 120 },
                            { display: '备注信息', name: 'remark', width: 180 }
                            ],
                   data: dataElementsJson,
                   onSelectRow: OnSelectElements//进一步确认是否优化？
               });
               //重载oMyGrid数据
               eManagerGrid.loadData();
           }
       });
   }
    </script>
 

    <%--添加页面元素权限(按钮点击)--%>
    <script type="text/javascript">
        $(document).ready(function () {           
            var e_options = {
                beforeSubmit: e_showRequest,  // from提交前的响应的回调函数
                success: e_showResponse,  // form提交响应成功后执行的回调函数
                url: "/ElementsManagement/AddElements",
                type: "POST",
                dataType: "json"
            };

            $("#eSubmit").click(function () {
                if (false) {
                    return false;
                } else {
                    $("#add_Elements").ajaxForm(e_options);
                }
            });

            //提交add_Elements表单前执行的函数
            function e_showRequest() {
               // alert("e_showRequest");
                var elementsName = $.trim($("#elementsName").val()); //权限名称
                //var elementID = $("#ePrivilegesItem").val(); //元素ID
                //alert(oPrivilegesName);
                if (elementsName == "") {
                    $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                    $("#promptDIV").addClass("p-errorDIV");
                    $("#promptDIV").html("元素名称不能为空");

                    return false;
                }

            }

            //提交add_Elements表单后执行的函数
            function e_showResponse(responseText, statusText) {
                //alert("e_showResponse");
                $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
                $("#promptDIV").addClass(responseText.css);
                $("#promptDIV").html(responseText.message);
                if (responseText.success) {
                    location.href = responseText.toUrl;
                }
            }
        });
       
    </script>

 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container"><h2>元素管理</h2></div>

    <div class="container">
     <%--操作提示DIV--%>
     <div id="promptDIV" class="row"></div>
     </div>

    <div class="container" style="margin-top:16px;">
        <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllElements" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
            <li id="elementsTab"><a href="#AddElements"  data-toggle="tab"><i class="icon-plus"></i>添加</a></li>
        </ul>
    </div>
      <% string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
      <% string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString(); %>
      <% DateTime t = Convert.ToDateTime(s); %>
     
   <div class="tab-content">
     <%--查看所有元素--%>
     <div class="tab-pane active" id="AllElements">
     <div id="elementgrid"></div>
     </div>  
     <%--添加元素--%>
     <div class="tab-pane" id="AddElements">
            <form id="add_Elements" class="form-horizontal" method="post" action="">
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
                    <label class="control-label">所在页面</label>
                    <div class="controls">
                        <select id="eElementPage" name="eElementPage" class="span4">
                            <option id="eElementPageInfo" value="-1">选择页面</option>
                        </select>
                    </div>
                </div>
                <div class="control-group span6 offset2">
                    <div class="controls span9">
                        <div id="eMyTree"></div>
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
                     <input  id="eSubmit" type="submit" value="添加" class="btn btn-primary  span1" /> 
                     &nbsp;&nbsp;&nbsp;
                     <input type="reset" value="重置"  class="btn btn-primary  span1" />
                  </div>
              </div>
            </form>
        </div>
    </div>

</asp:Content>



