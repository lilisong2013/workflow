<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
 <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
 <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
  <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-tree.css" rel="stylesheet"
        type="text/css" />
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

 <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "菜单编辑";
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
        var MenuManagerTree;
        $(document).ready(function () {
            //初始化ligerTree
            $("#tree1").ligerTree({
                checkbox: false,
                textFieldName: 'name',
                onSelect: OnSelect
            });
            eManagerTree = $("#tree1").ligerGetTreeManager();

            GetMenusTreeList(); //绑定菜单树数据并展示
        });

        //选择节点将父菜单信息赋给menusInfo
        function OnSelect(note) {
            //alert(note.data.id);
            $("#menusInfo").val(note.data.id);
            $("#menusInfo").html(note.data.name);
        }

        //获得菜单列表的树形菜单json数据格式，并展示
        function GetMenusTreeList() {
            $.ajax({
                url: "/MenusManagement/GetMenus",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                    //datas = responseText; //获取到的菜单json格式字符串
                    var dataJson = eval(responseText); //将json字符串转化为json数据

                    eManagerTree.clear();
                    eManagerTree.setData(dataJson);
                    eManagerTree.loadData();
                }
            });
        }      
    </script>


 <script type="text/javascript">
     var menusID;
     $(document).ready(function () {
         menusID = $("#menusID").val(); //菜单ID
         //alert(menusID);
     });
     var miTotal = 0; //是否有效数量
 </script>
 <%--是否有效初始化--%>
<%-- <script type="text/javascript">
     $(document).ready(function () {

         $.ajax({
             url: "/MenusManagement/GetInvalidList",
             type: "POST",
             dataType: "json",
             data: { menusId: menusID },
             success: function (responseText, statusText) {
                 //alert(responseText);
                 var dataJson = eval("(" + responseText + ")");
                 //alert(dataJson);
                 miTotal = parseInt(dataJson.total); //用户有效数量
                 for (var i = 0; i < dataJson.total; i++) {
                     $("#invalidList").append("<label class='checkbox span2'><input id='invalidValue" + i + "' type='checkbox' value='" + dataJson.List[i].id + "' />" + dataJson.List[i].name + "</label>");
                 }
                 for (var i = 0; i < dataJson.total; i++) {
                     if (dataJson.List[i].selected == 'true') {
                         $("#invalidValue" + i.toString()).prop("checked", true);
                         //alert("ok??");
                     } else {
                         $("#invalidValue" + i.toString()).prop("checked", false);
                     }
                 }
             }

         });
     });
 </script>--%>

 <%--表单提交数据--%>
 <script type="text/javascript">

     $(document).ready(function () {
         var menusData;
         var menusStr;
         $("#saveSubmit").click(function () {
             if (false) {
                 return false;
             } else {
                 menusStr = "{"; //JSON数据
                 var mvTotal = 0; //菜单有效的数量
                 // alert(mvTotal);
                 //菜单"是否有效"中被选中的项
                 for (var i = 0; i < 1; i++) {
                     var checkBoxID = $("#invalidValue" + i.toString()); //复选框ID
                     //alert(checkBoxID);
                     if (checkBoxID.is(":checked")) {
                         //alert(checkBoxID.val() + "选中");
                         menusStr += "mInvalidID" + mvTotal.toString() + ":'" + checkBoxID.val() + "',";
                         mvTotal++;
                         // checkBoxID.prop("checked", true);
                     } else {
                         //alert(checkBoxID.is(":checked"));
                         // alert(checkBoxID.val() + "未选中");
                         // checkBoxID.prop("checked", false);
                     }
                 }
                 menusStr += "mv_Total:'" + mvTotal + "',u_ID:'" + $("#menusID").val() + "'}";
                 //alert(menusStr);
                 menusData = eval("(" + menusStr + ")");
                 //alert(menusData);
                 $("#Edit_Menus").ajaxForm({
                     success: ri_showResponse, //form提交相应成功后执行的回调函数
                     url: "/MenusManagement/EditMenus",
                     type: "POST",
                     dataType: "json",
                     data: menusData
                 });
             }
         });
         //提交menu表单后执行的函数
         function ri_showResponse(responseText, statusText) {
             //alert("ok?????");
             $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
             $("#promptDIV").addClass(responseText.css);
             $("#promptDIV").html(responseText.message);
         }
     });
 </script>

  <%--隐藏提示信息--%>
    <script type="text/javascript">
        //隐藏提示信息
        $(document).click(function () {
            $("#promptDIV").removeClass("p-warningDIV p-successDIV p-errorDIV");
            $("#promptDIV").html("");
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h4>菜单管理</h4></div>

 <div class="container">
  <%--操作提示DIV--%>
 <div id="promptDIV" class="row"></div>
 </div> 
   
   <div class="tab-pane">
   <form  id="Edit_Menus" method="post" action="" class="form-horizontal"> 
       <div class="m-newline offset2">
       <label class="control-label">菜单名称：</label>
       <div class="controls">
       <input id="menusName" name="menusName" type="text" value="<%=ViewData["menusName"] %>" placeholder="菜单名称" maxlength="16"/>
       <input id="menusID" name="menusID" type="hidden" value="<%=ViewData["menusId"]%>"/>
       </div>
       </div>

       <div class="m-newline offset2">
       <label class="control-label">菜单编码：</label>
       <div class="controls">
       <input id="menuCode" name="menuCode" type="text" value="<%=ViewData["menuCode"] %>" placeholder="菜单编码" maxlength="40"/>
       </div>
       </div>

       <div class="m-newline offset2">
       <label class="control-label">菜单URL：</label>
       <div class="controls">
       <input id="menuUrl" name="menuUrl" type="text" value="<%=ViewData["menuUrl"] %>" placeholder="菜单URL"/>
       </div>
       </div>

       <div class="m-newline offset2">
       <label class="control-label">父菜单：</label>


       <div class="controls">
       <label class="uneditable-input"><%=ViewData["menuParrent_id1"]%></label>   
       <input type="hidden" id="menuParent_id" name="menuParent_id" value="<%=ViewData["menuParent_id"]%>" placeholder="父菜单"/>
       </div>
      </div>

       <div class="m-newline offset2">
       <label class="control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备注信息：</label>
       <div class="controls">  
       <textarea id="menuRemark" name="menuRemark" cols="5" rows="4" maxlength="200"><%=ViewData["menuRemark"]%></textarea>      
     
        <%string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>             
        <%string  s = DateTime.Now.ToString() +"."+ System.DateTime.Now.Millisecond.ToString(); %>
        <%DateTime t = Convert.ToDateTime(s); %>  
        <%WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)(Session["user"]); %>
        
        <input type="hidden" name="menuApp_id" id="menuApp_id" value="<%=m_usersModel.app_id %>"/>
        <input type="hidden" name="menuCreated_at" id="menuCreated_at" value="<%=t%>" />
        <input type="hidden" name="menuCreated_by" id="menuCreated_by" value="<%=m_usersModel.id%>" />       
        <input type="hidden" name="menuCreated_ip" id="menuCreated_ip" value="<%= ipAddress %>" /> 

        </div> 
       </div>
       <br />
       <div class="control-group span10 offset2">
       <input id="saveSubmit" type="submit" value="修改" class="btn btn-primary span10 offset2" />  
       </div>
   </form>
</div>
</asp:Content>
