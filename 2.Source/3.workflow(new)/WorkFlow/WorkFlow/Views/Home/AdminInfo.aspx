<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mainsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />

    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "系统管理员信息修改";
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

    <div class="container"><h2>信息管理</h2></div>

    <div class="container">
        <%--操作提示DIV--%>
        <div id="promptDIV" class="row"></div>
    </div>

    <div class="container">
        <div class="tabbable">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#AdminInfo" data-toggle="tab">系统信息</a></li>
                <li><a href="#AppInfo" data-toggle="tab">管理员信息</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="AdminInfo">
                    <div class="container">
                        <form class="form-horizontal" action="">
                            <div class="control-group">
                                <label class="control-label">登录名：</label>
                                <div class="controls">
                                    <input type="text" id="adminLogin" placeholder="登录名">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">姓名：</label>
                                <div class="controls">
                                    <input type="text" id="Text1" placeholder="姓名">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">工号：</label>
                                <div class="controls">
                                    <input type="text" id="Text2" placeholder="工号">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">电话：</label>
                                <div class="controls">
                                    <input type="text" id="Text3" placeholder="电话">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">电子邮件：</label>
                                <div class="controls">
                                    <input type="text" id="Text4" placeholder="电子邮件">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">备注信息：</label>
                                <div class="controls">
                                    <input type="text" id="Text5" placeholder="备注信息">
                                </div>
                            </div>
                        </form>
                    </div>
                </div>

                <div class="tab-pane" id="AppInfo">
                    <div class="container">
                        <form class="form-horizontal" action="">
                            <div class="control-group">
                                <label class="control-label">登录名：</label>
                                <div class="controls">
                                    <input type="text" id="Text6" placeholder="登录名">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">姓名：</label>
                                <div class="controls">
                                    <input type="text" id="Text7" placeholder="姓名">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">工号：</label>
                                <div class="controls">
                                    <input type="text" id="Text8" placeholder="工号">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">电话：</label>
                                <div class="controls">
                                    <input type="text" id="Text9" placeholder="电话">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">电子邮件：</label>
                                <div class="controls">
                                    <input type="text" id="Text10" placeholder="电子邮件">
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">备注信息：</label>
                                <div class="controls">
                                    <input type="text" id="Text11" placeholder="备注信息">
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


