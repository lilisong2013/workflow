<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
  
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
        var PageName = "元素信息编辑";
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

   <%--获得元素的ID--%>
   <script type="text/javascript">
       var elementsID;
        $(document).ready(function () {
            elementsID = $("#elementsID").val(); //角色ID
          // alert(elementsID);
        });
        var eiTotal = 0; //是否有效数量
    </script>

   <%--是否有效初始化--%>
   <script type="text/javascript">
       $(document).ready(function () {
           
           $.ajax({
               url: "/ElementsManagement/GetInvalidList",
               type: "POST",
               dataType: "json",
               data: { elementsId: elementsID },
               success: function (responseText, statusText) {
                   //alert(responseText);
                   var dataJson = eval("(" + responseText + ")");
                   //alert(dataJson);
                   eiTotal = parseInt(dataJson.total); //元素有效数量
                   for (var i = 0; i < dataJson.total; i++) {
                       $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                   }
                   for (var i = 0; i < dataJson.total; i++) {
                       if (dataJson.List[i].selected == 'true') {
                           $("#invalidValue" + i.toString()).prop("checked", true);
                           
                       }
                       else {
                           $("#invalidValue" + i.toString()).prop("checked", false);
                       }
                   }
               }
           });
       });
   </script>

    <%--添加元素的菜单树(数据)--%>
    <script type="text/javascript">
        var eManagerTree;
        //var eManagerGrid;
        $(document).ready(function () {
            //alert("EditPage---??");
            $("#eMyTree").hide(); //初始化隐藏eMyTree树
            //初始化ligerTree
            $("#eMyTree").ligerTree({
                checkbox: false,
                textFieldName: 'name',
                onSelect: OnSelectMenusOfElements
            });
            eManagerTree = $("#eMyTree").ligerGetTreeManager();
            BindMenusListOfElements(); //mMyTree绑定数据
       
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
                data: {elementsID: note.data.id },
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
                data: {elementsID: pageID },
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

   <%--表单提交数据--%>
   <script type="text/javascript">
       $(document).ready(function () {
           var elementsData;
           var elementsStr;
           $("#saveSubmit").click(function () {
               if (false) {
                   return false;
               } else {
                   elementsStr = "{"; //JSON数据字符串
                   var evTotal = 0; //元素有效的数量

                   //元素"是否有效"中被选中的项
                   for (var i = 0; i < 1; i++) {
                       var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                       if (checkBoxID.is(":checked")) {
                           elementsStr += "eInvalidID" + evTotal.toString() + ":'" + checkBoxID.val() + "',";
                           evTotal++;

                       } else {

                       }
                   }
                   elementsStr += "ev_Total:'" + evTotal + "',u_ID:'" + $("#elementsID").val() + "'}";
                   //alert(elementsStr);
                   elementsData = eval("(" + elementsStr + ")");
                   ModifyElement(); //修改元素信息
                 
               }
           });

           //修改元素信息
           function ModifyElement() {
               var options = {
                   beforeSubmit: element_showRequest, //form提交前的响应回调函数
                   success: element_showResponse, //form提交响应成功后执行的回调函数
                   url: "/ElementsManagement/EditElements",
                   type: "POST",
                   dataType: "json"
               };
               $("#Edit_Elements").ajaxForm(options);
           }

           //form提交前的响应回调函数
           function element_showRequest() {
               var elementName = $("#elementsName").val();
               if (elementName == "") {
                   $("#promptDIV").removeClass("alert alert-error alert-success");
                   $("#promptDIV").addClass("alert alert-error");
                   $("#promptDIV").html("元素名称不能为空!");
                   return false;
               }
           }

           //form提交响应成功后执行的回调函数
           function element_showResponse(responseText, statusText) {
               var dataJson = eval("(" + responseText + ")");
               show_promptDIV(dataJson);//提示信息
           }

           //提示信息
           function show_promptDIV(data) {
               $("#promptDIV").removeClass("alert alert-error alert-success");
               $("#promptDIV").addClass(data.css);
               $("#promptDIV").html(data.message);
           }

       });
    
   </script>

   <%--表单提交数据--%>
  <%-- <script type="text/javascript">
       $(document).ready(function () {
           var elementsData;
           var elementsStr;
           $("#saveSubmit").click(function () {
               
               if (false) {
                   return false;
               } else {
                   elementsStr = "{"; //JSON数据字符串
                   var evTotal = 0; //元素有效的数量
                  
                   //元素"是否有效"中被选中的项
                   for (var i = 0; i < 1; i++) {
                       var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                       
                       if (checkBoxID.is(":checked")) {
                           //alert(checkBoxID.val() + "选中");
                           elementsStr += "eInvalidID" + evTotal.toString() + ":'" + checkBoxID.val() + "',";
                           evTotal++;
                           // checkBoxID.prop("checked", true);
                       } else {
                           //alert(checkBoxID.is(":checked"));
                           //alert(checkBoxID.val() + "未选中");
                           // checkBoxID.prop("checked", false);
                       }
                   }
                   elementsStr += "ev_Total:'" + evTotal + "',u_ID:'" + $("#elementsID").val() + "'}";
                   //alert(elementsStr);
                   elementsData = eval("(" + elementsStr + ")");
                  // alert(elementsData);
                   $("#Edit_Elements").ajaxForm({
                       success: ri_showResponse, //form提交相应成功后执行的回调函数
                       url: "/ElementsManagement/EditElements",
                       type: "POST",
                       dataType: "json",
                       data: elementsData
                   });
               }
           });
           //提交element表单后执行的函数
           function ri_showResponse(responseText, statusText) {
               //alert("ok?????");
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
 <div class="container"><h2>元素管理</h2></div>
   
    <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
    <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
    <%DateTime t = Convert.ToDateTime(s); %>   
    <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
   <div class="container">
    <%--操作提示DIV--%>
   <div id="promptDIV" class="row"></div>
   </div>  
     <div class="tab-pane">
      <form  id="Edit_Elements" method="post" action="" class="form-horizontal">           
       <div class="control-group span5 offset2">       
       <label class="control-label">元素名称：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsName" name="elementsName" type="text" value="<%=ViewData["elementsName"]%>" class="input-prepend span5" placeholder="元素名称"/>
       <input id="elementsID" name="elementsID" type="hidden" value="<%=ViewData["elementsId"]%>"/>
       </div>
       </div>
       <div class="control-group span5 offset2">
       <label class="control-label">元素编码：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsCode" name="elementsCode" type="text" value="<%=ViewData["elementsCode"]%>" class="input-prepend span5" placeholder="元素编码"/>
       </div>
       </div>
       <div class="control-group span5 offset2">
       <label class="control-label">初始状态：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <label class="uneditable-input span5" ><%=ViewData["elementsInitstatus_id"]%></label>
       <input type="hidden" id="elementsInitstatus_id" name="elementsInitstatus_id" value="<%=ViewData["elementsInitstatus_id"]%>"/>
       </div>
       </div>
       <div class="control-group span5 offset2">
       <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;排序码：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div class="controls">
       <input id="elementsSeqno" name="elementsSeqno" type="text" value="<%=ViewData["elementsSeqno"]%>" class="input-prepend span5" placeholder="排序码"/>
       </div>
       </div>
       <div class="control-group span5 offset2">
       <label class="control-label">所在页面：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>   
       <div class="controls">
       <label class="uneditable-input span5"><%=ViewData["elementsMenu_IDName"]%></label>
       <input id="elementsMenu_id" name="elementsMenu_id" type="hidden" value="<%=ViewData["elementsMenu_id"]%>"/>      
       </div> 
       </div>

       <div class="control-group span5 offset2">
       <label class="control-label">是否有效：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
       <div id="invalidList">
       </div>
       </div>
       <div class="control-group span5 offset2">
       <label class="control-label"> 备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>    
       <div class="controls">
       <textarea id="elementsRemark" name="elementsRemark" cols="5" rows="4" class="span5" placeholder="备注"><%=ViewData["elementsRemark"]%></textarea>
       </div>   
        
        <input type="hidden" name="elementsDeleted" id="elementsDeleted" value="<%=ViewData["elementsDeleted"]%>" />
        <input type="hidden" name="elementsCreated_at" id="elementsCreated_at" value="<%=t%>" />
        <input type="hidden" name="elementsCreated_by" id="elementsCreated_by" value="<%=m_usersModel.id%>" />  
        <input type="hidden" name="elementsApp_id" id="elementsApp_id" value="<%=m_usersModel.app_id%>"/>     
        <input type="hidden" name="elementsCreated_ip" id="elementsCreated_ip" value="<%= ipAddress %>" />
        <input type="hidden" name="elementsUpdated_at" id="elementsUpdated_at"/>
        <input type="hidden" name="elementsUpdated_by" id="elementsUpdated_by"/>
        <input type="hidden" name="elementsUpdated_ip" id="elementsUpdated_ip"/>
       </div>
       <div class="control-group span5 offset3" >
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary  span6" />  
       </div>    
    </form>
    </div> 
</asp:Content>
