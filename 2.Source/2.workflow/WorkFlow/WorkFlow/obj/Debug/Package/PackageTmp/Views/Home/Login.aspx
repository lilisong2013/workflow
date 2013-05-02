<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Login</title>
    <link href="../../bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../bootstrap/css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <script src="../../bootstrap/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../../bootstrap/js/bootstrap.js" type="text/javascript"></script>
    
    <style type="text/css">
    .navbar .navbar-inner {
        background: #3993ba;
        background: -moz-linear-gradient(top, #3993ba 0%, #067ead 100%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#3993ba), color-stop(100%,#067ead));
        background: -webkit-linear-gradient(top, #3993ba 0%,#067ead 100%);
        background: -o-linear-gradient(top, #3993ba 0%,#067ead 100%);
        background: -ms-linear-gradient(top, #3993ba 0%,#067ead 100%);
        background: linear-gradient(top, #3993ba 0%,#067ead 100%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#3993ba', endColorstr='#067ead',GradientType=0 );
    }

    .navbar .nav > li > a {
        color: #c1dce7;
    }
    .navbar .nav > li:hover > a {
        color:#fff;
    }
    .navbar .nav .active > a, .navbar .nav .active > a:hover {
        background: #206484 !important;
    }
    .navbar .divider-vertical {
        border-left-color:#2078A1;
        border-right-color:#3497C2;
    }

    #footer{
     height: 60px;
     background-color: #C8CFA9;
    }

    .loginform{
        max-width: 420px;
        padding: 19px 29px 29px;
        margin: 0 auto 20px;
        background-color: #fff;
        -webkit-border-radius: 8px;
           -moz-border-radius: 8px;
                border-radius: 8px;
                   box-shadow: 0 0 15px #222;  
    }
    .loginform{
         margin-top:60px;
    }

    .loginform h2{height:45px;font-size:20px;font-weight:normal; margin-left:10px;}

    .loginleft{
        width:260px;
         height:230px;
         position:relative;
         float:left;
       border-right:1px solid #ccc;
    }

    .loginright{
        width:158px;
         height:230px;
         position:relative;
         float: right;
    }

    .loginform .controls{
         margin-top:18px;
    }

    .loginform .controls label{
         margin-left:40px;
         width:90px;
         position:relative;
         float:left;
    }
    .loginform .controls button{
         position:relative;
         float: left;
    }
    </style>
</head>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <a href="#" class="brand">权限管理系统</a>
        </div>
    </div>
    <div class="container">
        <div class="loginform">
            <div class="row">
                <div class="loginleft">
                    <form class="form-signin" method="post" action="/Home/LoginValidation">
                        <h2>应用系统管理员登录</h2>

                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on span1">@<i class="icon-user"></i></span>
                                <input type="text" name="loginName" class="span2" />
                            </div>
                        </div>

                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on span1">@<i class="icon-lock"></i></span>
                                <input type="password" name="loginPassword" class="span2" />
                            </div>
                        </div>

                        <div class="controls">
                            <label class="checkbox">
                                <input type="checkbox" /> 下次自动登录
                            </label>
                            <button type="submit" class="btn btn-primary">登录</button>
                        </div>
                    </form>
                </div>
                <div class="loginright">
                    <h2>没有帐号？</h2>
                    <p>应用系统如果想要集成通用权限管理功能，请点击下面的注册按钮！</p>
                    <p><a href="/Home/RegistPage" class="btn">注册</a></p>
                </div>
            </div>
        </div>
    </div>
    <div id="footer" class="navbar-fixed-bottom">
        <div class="container">
            <p class="navbar-text">@山东三龙智能技术有限公司</p>
        </div>
    </div>
</body>
</html>

