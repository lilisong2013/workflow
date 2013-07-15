
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">

   <link href="../../CSS/promptDivCss.css" rel="stylesheet" type="text/css" />
   <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <%-- ligerUI核心文件--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet" type="text/css" />    
    <script src="../../LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>   
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <%--LigerUI Dialog文件--%>
   <%-- <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css"/>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "页面代码管理";
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
    <%--在Grid中测试信息--%>
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            alert("-----");
            var user =
            {
                "username": "andy",
                "age": 20,
                "info": { "tel": "123456", "cellphone": "98765" },
                "address":
                [
                 { "city": "beijing", "postcode": "222333" },
                 { "city": "newyork", "postcode": "555666" }
                ]
            }
            alert(user.username);
            alert(user.age);
            alert(user.info.cellphone);
            alert(user.address[0].city);
            alert(user.address[0].postcode);
            var test =
            { Rows: [
              { name: '顺序', id: '1', reamrk: '', order_no: '1' },
              { name: '并序', id: '2', remark: '', order_no: '2' }
              ]
            }
            var JsonTest = eval(test);
            alert("length:" + test.Rows.length)
            for (var i = 0; i < test.Rows.length; i++) {
                alert("i:" + i);
                alert("name:"+test.Rows[i].name+"id:"+test.Rows[i].id+"order_no:"+test.Rows[i].order_no);
            }
            alert(JsonTest);
//            alert(test.Rows[0].name);
//            alert(test.Rows[0].id);
//            alert(test.Rows[0].order_no);
//            alert(test.Rows[1].name);
//            alert(test.Rows[1].id);
//            alert(test.Rows[1].order_no);
        });
    </script>--%>
    <%--在Grid中显示flowstep_type信息--%>
   <%-- <script type="text/javascript">
        var FmanagerListGrid;
        $(document).ready(function () {

         GetFlowstep_Type_List(); 

        });
     function GetFlowstep_Type_List() {
         $.ajax({
             url: "/CodesManagement/GetFlowstep_Type_List",
             type: "POST",
             dataType: "json",
             data: {},
             success: function (responseText, statusText) {
                 alert(responseText);
                 var test = eval("(" + responseText + ")");
                 for (var i = 0; i < test.Rows.length; i++) {
                     alert("name:" + test.Rows[i].name + "order_no:" + test.Rows[i].order_no);
                 }
                 var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
             }
         });
      }
    </script>--%>
    <%--在Grid中显示step_action信息--%>
   <%--  <script type="text/javascript">
        var SmangerListGrid;
        $(document).ready(function () {
            //定义ligerGrid
            $("#step_actiongrid").ligerGrid({
                width: '99%',
                height: 300
            });
            SmangerListGrid = $("#step_actiongrid").ligerGetGridManager();
            GetStep_Action_List(); //获取处理结果列表
//            $("#infoTab1").click(function () {
//                GetStep_Action_List();//获取处理结果列表
//            });
        });
        function GetStep_Action_List() {
            $.ajax({
                url: "/CodesManagement/GetStep_Action_List",
                type: "POST",
                dataType: "json",
                data: {},
                success: function (responseText, statusText) {
                   // alert(responseText);
                    var dataJson = eval("(" + responseText + ")"); //将json字符串转化为json数据
                    //更新mygrid数据
                    SmangerListGrid.setOptions({
                        columns: [
                      { display: '处理结果名称', name: 'name',width:80, align: 'center' },
                      { display: '排序码', name: 'order_no', width:80,align: 'center' },
                      { display: '备注', name: 'reamrk', width:80,align: 'center' }
                      
                      ],
                        data: dataJson
                    });
                    SmangerListGrid.loadData();
                }
            });
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>代码表信息管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
    </div>

     <div class="container" style="margin-top:16px;">
       <%-- <ul class="nav nav-tabs">
            <li class="active" id="#infoTab"><a href="#AllFlow" data-toggle="tab"><i class="icon-check"></i>全部</a></li>
        </ul>--%>
    </div>
    <div class="tab-content">
   
    <%--  <script type="text/javascript">
          $(document).ready(function () {
              alert("-----");
                  var user =
              {
                  "username": "andy",
                  "age": 20,
                  "info": { "tel": "123456", "cellphone": "98765" },
                  "address":
                [
                 { "city": "beijing", "postcode": "222333" },
                 { "city": "newyork", "postcode": "555666" }
                ]
              }
                  alert(user.username);
                  alert(user.age);
                  alert(user.info.cellphone);
                  alert(user.address[0].city);
                  alert(user.address[0].postcode);
                  var test =
            { Rows: [
              { name: '顺序', id: '1', reamrk: '', order_no: '1' },
              { name: '并序', id: '2', remark: '', order_no: '2' }
              ]
            }
                  var JsonTest = eval(test);
                  alert("length:" + test.Rows.length)
                  for (var i = 0; i < test.Rows.length; i++) {
                      alert("i:" + i);
                      alert("name:" + test.Rows[i].name + "id:" + test.Rows[i].id + "order_no:" + test.Rows[i].order_no);
                  }
                  alert(JsonTest);               
          });
     
      </script>--%>
  
      <%string msg = string.Empty; %>
      <%WorkFlow.Flowstep_TypeWebService.flowstep_typeBLLservice m_flowstepBllService = new WorkFlow.Flowstep_TypeWebService.flowstep_typeBLLservice(); %>
      <%WorkFlow.Flowstep_TypeWebService.SecurityContext m_SecurityContext = new WorkFlow.Flowstep_TypeWebService.SecurityContext();%>
      <%WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"]; %>
      <%m_SecurityContext.UserName = m_usersModel.login; %>
      <%m_SecurityContext.PassWord = m_usersModel.password;%>
      <%m_SecurityContext.AppID = (int)m_usersModel.app_id;%>
      <%m_flowstepBllService.SecurityContextValue = m_SecurityContext; %>
      <%var fsDs=m_flowstepBllService.GetFlowStep_TypeList(out msg);%>
    
      <%WorkFlow.Step_ActionWebService.step_actionBLLservice m_step_actionBllService = new WorkFlow.Step_ActionWebService.step_actionBLLservice();%>
      <%WorkFlow.Step_ActionWebService.SecurityContext ms_SecurityContext = new WorkFlow.Step_ActionWebService.SecurityContext();%>
      <%ms_SecurityContext.UserName = m_usersModel.login;%>
      <%ms_SecurityContext.PassWord = m_usersModel.password;%>
      <%ms_SecurityContext.AppID = (int)m_usersModel.app_id;%>
      <%m_step_actionBllService.SecurityContextValue = ms_SecurityContext;%>
      <%var saDs = m_step_actionBllService.GetStep_ActionList(out msg);%>        
       
      <table class="table table-striped table-bordered">
      <tr class="success"><td colspan="4"><center><b><h8>步骤类型一览表</h8></b></center></td></tr>
      <tr>
      <td><center><b>步骤类型ID</b></center></td>
      <td><center><b>名称</b></center></td>
      <td><center><b>排序码</b></center></td>
      <td><center><b>备注</b></center></td>
      </tr>
      <tr>
      <td><center><%=fsDs.Tables[0].Rows[0][0]%></center></td>
      <td><center><%=fsDs.Tables[0].Rows[0][1]%></center></td>
      <td><center><%=fsDs.Tables[0].Rows[0][3]%></center></td>
      <td><center><%=fsDs.Tables[0].Rows[0][2]%></center></td>
      </tr>
      <tr>
      <td><center><%=fsDs.Tables[0].Rows[1][0]%></center></td>
      <td><center><%=fsDs.Tables[0].Rows[1][1]%></center></td>
      <td><center><%=fsDs.Tables[0].Rows[1][3]%></center></td>
      <td><center><%=fsDs.Tables[0].Rows[1][2]%></center></td>
      </tr>
      </table>

      <table class="table table-striped table-bordered">
       <tr class="success"><td colspan="4"><center><b><h8>处理结果名称一览表</h8></b></center></td></tr>
       <tr>
          <td><center><b>步骤类型ID</b></center></td>
          <td><center><b>名称</b></center></td>
          <td><center><b>排序码</b></center></td>
          <td><center><b>备注</b></center></td>
       </tr>
       <tr>
       <td><center><%=saDs.Tables[0].Rows[0][0]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[0][1]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[0][3]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[0][2]%></center></td>
       </tr>
       <tr>
       <td><center><%=saDs.Tables[0].Rows[1][0]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[1][1]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[1][3]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[1][2]%></center></td>
       </tr>
       <tr>
       <td><center><%=saDs.Tables[0].Rows[2][0]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[2][1]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[2][3]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[2][2]%></center></td>
       </tr>
       <tr>
       <td><center><%=saDs.Tables[0].Rows[3][0]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[3][1]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[3][3]%></center></td>
       <td><center><%=saDs.Tables[0].Rows[3][2]%></center></td>
       </tr>
      </table>
    </div>

    
</asp:Content>
