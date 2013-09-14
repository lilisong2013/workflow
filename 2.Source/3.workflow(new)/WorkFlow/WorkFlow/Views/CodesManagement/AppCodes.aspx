
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="container"><h2>代码表信息管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
       <div id="promptDIV" class="row"></div>
    </div>

     <div class="container" style="margin-top:16px;">

    </div>
    <div class="tab-content">
  
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
