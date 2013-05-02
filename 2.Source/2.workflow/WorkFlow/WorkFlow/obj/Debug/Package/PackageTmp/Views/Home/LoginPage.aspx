<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>LoginPage</title>
    <link href="../../bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../bootstrap/css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />

    <script src="../../bootstrap/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../../bootstrap/js/bootstrap.js" type="text/javascript"></script>

    <style type="text/css">
      body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
      }

      .form-signin {
        max-width: 300px;
        padding: 19px 29px 29px;
        margin: 0 auto 20px;
        background-color: #fff;
        border: 1px solid #e5e5e5;
        -webkit-border-radius: 5px;
           -moz-border-radius: 5px;
                border-radius: 5px;
        -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
           -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
                box-shadow: 0 1px 2px rgba(0,0,0,.05);
      }
    </style>
</head>
<body>
    <div class="container">
        <form class="form-signin" method="post" action="/Home/LoginValidation">
            <h2 class="">帐号登录</h2>

            

            <div class="controls">
                <div class="input-prepend">
                    <span class="add-on span1">@<i class="icon-eye-open"></i></span>
                    <select class="span2" name="loginRole">
                        <option>超级管理员</option>
                        <option>系统管理员</option>
                        <option>用户</option>
                    </select>
                </div>
            </div>
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

            <button class="btn btn-small  btn-primary offset1" type="submit">确定</button>

        </form>
    </div>
</body>
</html>
