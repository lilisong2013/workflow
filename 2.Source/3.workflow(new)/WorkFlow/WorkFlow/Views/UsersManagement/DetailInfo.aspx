<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "用户信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

        <table border="1" align="center" width="600">
        <tr><th colspan="2">用户详情</th></tr>
        <tr>
        <td width="300">登录名称：</td>
        <td width="300"><%=ViewData["usersLogin"]%></td>
        </tr>
        <tr>
        <td width="300">用户姓名：</td>
        <td width="300"><%=ViewData["usersName"]%></td>
        </tr>
        <tr>
        <td width="300">工号：</td>
        <td width="300"><%=ViewData["usersEmployee_no"]%></td>
        </tr>
        <tr>
        <td width="300">手机号码：</td>
        <td width="300"><%= ViewData["usersMobile_phone"]%></td>
        </tr>
        <tr>
        <td width="300">邮件地址：</td>
        <td width="300"><%=ViewData["usersMail"]%></td>
        </tr>
        <tr>
        <td width="300">备注：</td>
        <td width="300"><%=ViewData["usersRemark"]%></td>
        </tr>
        <tr>
        <td width="300">是否管理员：</td>
        <td width="300"><%= ViewData["usersAdmin"]%></td>
        </tr>
        <tr>
        <td width="300">是否有效：</td>
        <td width="300"><%=ViewData["usersInvalid"]%></td>
        </tr>
        <tr>
        <td width="300">记录创建时间：</td>
        <td width="300"><%= ViewData["usersCreated_at"]%></td>
        </tr>
        <tr>
        <td width="300">记录创建用户：</td>
        <td width="300"><%=ViewData["usersCreated_by"]%></td>
        </tr>
        <tr>
        <td width="300">记录创建IP：</td>
        <td width="300"><%=ViewData["usersCreated_ip"]%></td>
        </tr>
        <tr>
        <td width="300">记录更新时间：</td>
        <td width="300"><%=ViewData["usersUpdated_at"]%></td>
        </tr>
        <tr>
        <td width="300">记录更新用户：</td>
        <td width="300"><%=ViewData["usersUpdated_by"]%></td>
        </tr>
        <tr>
        <td width="300">记录更新IP：</td>
        <td width="300"><%=ViewData["usersUpdated_ip"]%></td>
        </tr>
        <tr>
        <td width="300">系统名称：</td>
        <td width="300"><%=ViewData["usersApp_id"]%></td>
        </tr>
        </table>
     

</asp:Content>
