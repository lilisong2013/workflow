<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/secondsite.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    
</asp:Content>

<%--子页面的css、js--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageJS" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
  <%--  <link href="../../Css/promptDivCss.css" rel="stylesheet" type="text/css" />--%>


    <%--页面标题--%>
    <script type="text/javascript">
        var titleUrl = "/Home/GetPageTitle";
        var PageName = "应用系统注册";
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

    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                //beforeSubmit: showRequest,  // from提交前的响应的回调函数
                success: showResponse,  // form提交响应成功后执行的回调函数
                url: "/Home/RegistUser",
                type: "POST",
                dataType: "json"
            };

            $("#submit").click(function () {
                if (false) {

                    return false;
                } else {
                    $("#registerUser").ajaxForm(options);
                }
            });
        });

        function showResponse(responseText, statusText) {
            //alert(responseText.toUrl);
            var dataJson = eval("(" + responseText + ")");
            if (!dataJson.success) {
                show_DIV(dataJson);
            } else {
                location.href = dataJson.toUrl;
            }
        }

        //提示信息
        function show_DIV(data) {
            $("#promptDIV").removeClass("alert alert-success alert-error");
            $("#promptDIV").addClass(data.css);
            $("#promptDIV").html(data.message);
        }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div class="container">
        <div class="row">
            <ul class="breadcrumb">
                <li class="active">应用系统注册<span class="divider">/</span></li>
                <li><a href="/Home/Login">登录</a></li>
            </ul>
        </div>
        
        <div class="row">
            <%--操作提示DIV--%>
            <div id="promptDIV"></div>
        </div>
       
    </div>     
 
    <div class="container">

        <form id="registerUser" class="form-horizontal" method="post" action="">
            <div class="control-group">
                <div class="row span2 pull-right">
                    <input type="reset" class="btn btn-primary span2" value="重置信息" />
                </div>
                <div class="row span2 pull-right">
                    <input id="submit"  type="submit" class="btn btn-primary span2" value="提交申请" /> 
                </div>
            </div>
            
            <div class="row text-center">
                <h3>填写系统信息</h3>
            </div>
            <div class="row well">
                

                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">系统名称：</label>
                                <div class="controls">
                                    <input name="appsName" type="text" class="span3" maxlength="30"/>
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">系统编码：</label>
                                <div class="controls">
                                    <input name="appsCode" type="text" class="span3" maxlength="30"/>
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
                                    <input name="appsUrl" type="text" class="span3" maxlength="30"/>
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
                                    <textarea name="appsRemark" class="m-textarea span8" rows="3" cols="10" maxlength="100"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row text-center">
                <h3>填写管理员信息</h3>
            </div>
            <div class="row well">
                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">登录名称：</label>
                                <div class="controls">
                                    <input name="userLogin" type="text" class="span3" maxlength="30"/>
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">真实姓名：</label>
                                <div class="controls">
                                    <input name="userName" type="text" class="span3" maxlength="30"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="span12">
                    <div class="row">
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">登录密码：</label>
                                <div class="controls">
                                    <input name="userPassword" type="password" class="span3" maxlength="16"/>
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">确认密码：</label>
                                <div class="controls">
                                    <input name="userPassword2" type="password" class="span3" />
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
                                    <input name="userEmployeeNo" type="text" class="span3" maxlength="40"/>
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="control-group">
                                <label class="control-label">手机号码：</label>
                                <div class="controls">
                                    <input name="userMobilePhone" type="text" class="span3" />
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
                                    <input name="userMail" type="text" class="span3" />
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
                                    <textarea name="userRemark" class="m-textarea span8" rows="3" cols="10" maxlength="80"></textarea>
                                    <% string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress(); %>
                                    <input type="hidden" name="createdIP" id="Hidden3" value="<%= ipAddress %>" />
                                    <% string apply_at = DateTime.Now.ToString(); %>
                                    <input type="hidden" name="apply_at" id="Hidden4" value="<%= apply_at %>" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>

</asp:Content>


