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
    <div class="container form-horizontal">
        <div class="row text-center">
            <h3>系统信息浏览</h3>
        </div>
        <div class="row well">
            <div class="span12">
                <div class="row">
                    <div class="span5">
                        <div class="control-group">
                            <label class="control-label">系统名称：</label>
                            <div class="controls">
                                <label class="span3"><%=ViewData["appName"]%></label>
                            </div>
                        </div>
                    </div>
                    <div class="span5">
                        <div class="control-group">
                            <label class="control-label">系统编码：</label>
                            <div class="controls">
                                <label class="span3"><%=ViewData["appCode"]%></label> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                
            <div class="span12">
                <div class="row">
                    <div class="span5">
                        <div class="control-group">
                            <label class="control-label">访问链接：</label>
                            <div class="controls">
                                <label class="span3"><%=ViewData["appUrl"]%></label> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="span12">
                <div class="row">
                    <div class="span5">
                        <div class="control-group">
                            <label class="control-label">系统备注：</label>
                            <div class="controls">
                                <p class="span8" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["appMark"]%></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

            <div class="row text-center">
                <h3>管理员信息浏览</h3>
            </div>
            <div class="row well">
                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">登录名称：</label>
                                <div class="controls">
                                    <label class="span3"><%=ViewData["adminLogin"]%></label> 
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">真实姓名：</label>
                                <div class="controls">
                                    <label class="span3"><%=ViewData["adminName"]%></label> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">员工编号：</label>
                                <div class="controls">
                                    <label class="span3"><%=ViewData["adminEmployee_no"]%></label> 
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">手机号码：</label>
                                <div class="controls">
                                    <label class="span3"><%=ViewData["adminPhone"]%></label> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">电子邮箱：</label>
                                <div class="controls">
                                    <label class="span3"><%=ViewData["adminMail"]%></label> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">备注信息：</label>
                                <div class="controls">
                                    <p class="span8" style="word-break:break-all; word-wrap:break-word;"><%=ViewData["adminMark"]%></p> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <div class="container">
        <ul class="pager">
            <li class="next">
                <a href="/Home/Login">返回登录</a>
            </li>
        </ul>
    </div>
</asp:Content>
