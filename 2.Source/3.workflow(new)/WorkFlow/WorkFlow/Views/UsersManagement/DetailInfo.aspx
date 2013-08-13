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
        <td>登录名称：</td><td><%=ViewData["usersLogin"]%></td>
        </tr>
        <tr>
        <td>用户姓名：</td><td><%=ViewData["usersName"]%></td>
        </tr>
        <tr>
        <td>工号：</td>
        <td><%=ViewData["usersEmployee_no"]%></td>
        </tr>
        <tr>
        <td>手机号码：</td>
        <td><%= ViewData["usersMobile_phone"]%></td>
        </tr>
        <tr>
        <td>邮件地址：</td>
        <td><%=ViewData["usersMail"]%></td>
        </tr>
        <tr>
        <td>备注：</td>
        <td><%=ViewData["usersRemark"]%></td>
        </tr>
        <tr>
        <td>是否管理员：</td>
        <td><%= ViewData["usersAdmin"]%></td>
        </tr>
        <tr>
        <td>是否有效：</td>
        <td><%=ViewData["usersInvalid"]%></td>
        </tr>
        <tr>
        <td>记录创建时间：</td>
        <td><%= ViewData["usersCreated_at"]%></td>
        </tr>
        <tr>
        <td>记录创建用户：</td>
        <td><%=ViewData["usersCreated_by"]%></td>
        </tr>
        <tr>
        <td>记录创建IP：</td>
        <td><%=ViewData["usersCreated_ip"]%></td>
        </tr>
        <tr>
        <td>记录更新时间：</td>
        <td><%=ViewData["usersUpdated_at"]%></td>
        </tr>
        <tr>
        <td>记录更新用户：</td>
        <td><%=ViewData["usersUpdated_by"]%></td>
        </tr>
        <tr>
        <td>记录更新IP：</td>
        <td><%=ViewData["usersUpdated_ip"]%></td>
        </tr>
        <tr>
        <td>系统名称：</td>
        <td><%=ViewData["usersApp_id"]%></td>
        </tr>
        </table>
     

</asp:Content>
