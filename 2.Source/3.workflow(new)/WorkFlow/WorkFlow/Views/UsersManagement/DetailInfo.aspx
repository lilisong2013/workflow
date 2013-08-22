<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage" %>
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

        <table class="table table-bordered  table-condensed table-hover" width="600">
        <tr><th colspan="2"><center>用户详情</center></th></tr>
        <tr>
        <td width="300">登录名称：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersLogin"]%></td>
        </tr>
        <tr>
        <td width="300">用户姓名：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersName"]%></td>
        </tr>
        <tr>
        <td width="300">员工编号：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersEmployee_no"]%></td>
        </tr>
        <tr>
        <td width="300">手机号码：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%= ViewData["usersMobile_phone"]%></td>
        </tr>
        <tr>
        <td width="300">邮件地址：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersMail"]%></td>
        </tr>
        <tr>
        <td width="300">备注信息：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersRemark"]%></td>
        </tr>
        <tr>
        <td width="300">是否管理员：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%= ViewData["usersAdmin"]%></td>
        </tr>
        <tr>
        <td width="300">是否有效：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersInvalid"]%></td>
        </tr>
        <tr>
        <td width="300">创建时间：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%= ViewData["usersCreated_at"]%></td>
        </tr>
        <tr>
        <td width="300">创建用户：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersCreated_by"]%></td>
        </tr>
        <tr>
        <td width="300">创建IP：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersCreated_ip"]%></td>
        </tr>
        <tr>
        <td width="300">更新时间：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersUpdated_at"]%></td>
        </tr>
        <tr>
        <td width="300">更新用户：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersUpdated_by"]%></td>
        </tr>
        <tr>
        <td width="300">更新IP：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersUpdated_ip"]%></td>
        </tr>
        <tr>
        <td width="300">系统名称：</td>
        <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["usersApp_id"]%></td>
        </tr>
        </table>
     

</asp:Content>
