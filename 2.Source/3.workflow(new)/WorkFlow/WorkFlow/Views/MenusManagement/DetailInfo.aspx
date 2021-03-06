﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/superapproval.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "菜单信息";
    </script>
    <script src="../../Scripts/jquery.title.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table class="table table-bordered  table-condensed table-hover" width="600">
   <tr><th colspan="2"><center>菜单详情</center></th></tr>
   <tr>
   <td width="300">菜单名称：</td>
   <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["name"]%></td>
   </tr>
   <tr>
   <td width="300">菜单编码：</td>
   <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["code"]%></td>
   </tr>
   <tr>
   <td width="300">菜单URL：</td>
   <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["url"]%></td>
   </tr>
   <tr>
   <td width="300">系统名称：</td>
   <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["app_id"]%></td>
   </tr>
   <tr>
   <td width="300">父菜单名称：</td>
   <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["parent_id1"]%></td>
   </tr>
   <tr>
   <td width="300">备注信息：</td>
   <td width="300" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["remark"]%></td>
   </tr>

</table>
</asp:Content>
