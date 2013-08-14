<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "系统申请提交成功";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="alert alert-success">您的系统申请已成功提交！系统信息如下：</div>
    </div>
    <div class="container">
        <table class="table table-bordered">
            <caption>系统信息浏览</caption>
            <thead>
                <tr>
                    <th>系统名称：</th>
                    <th>系统编码：</th>
                    <th>访问链接：</th>
                </tr>
            </thead>
            <tbody> 
                <tr class="info">
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
            <thead>
                <tr>
                    <th colspan="3">系统备注：</th>
                </tr>
            </thead>
            <tbody> 
                <tr class="info">
                    <td colspan="3"></td>
                </tr>
            </tbody>
        </table>
        
        <table class="table table-bordered">
            <caption>管理员信息浏览</caption>
            <thead>
                <tr>
                    <th>登录名称：</th>
                    <th>真实姓名：</th>
                    <th>工号：</th>
                </tr>
            </thead>
            <tbody> 
                <tr class="info">
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
            
            <thead>
                <tr>
                    <th>手机号：</th>
                    <th colspan="2">电子邮箱：</th>
                </tr>
            </thead>
            <tbody> 
                <tr class="info">
                    <td></td>
                    <td colspan="2"></td>
                </tr>
            </tbody>
            
            <thead>
                <tr>
                    <th colspan="3">备注信息：</th>
                </tr>
            </thead>
            <tbody> 
                <tr class="info">
                    <td colspan="3"></td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="container">
        <ul class="pager">
            <li class="next">
                <a href="/Home/Login">返回登录</a>
            </li>
        </ul>
    </div>
</asp:Content>
