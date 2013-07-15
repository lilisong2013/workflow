<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WorkFlowService.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <table>
            <tr>
                <td>服务器名称：</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>登录名：</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>密码：</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>数据库：</td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="修改" onclick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <a href="WebService/base_userBLLservice.asmx">基础用户WebService/base_userBLLservice.asmx</a>
    <br />
    <a href="WebService/appsBLLservice.asmx">系统WebService/appsBLLservice.asmx</a>
    <br />
    <a href="WebService/usersBLLservice.asmx">用户WebService/usersBLLservice.asmx</a>
    <br />
    <a href="WebService/rolesBLLservice.asmx">角色WebService/rolesBLLservice.asmx</a>
    <br />
    <a href="WebService/user_roleBLLservice.asmx">用户角色WebService/user_roleBLLservice.asmx</a>
    <br />
    <a href="WebService/privilegesBLLservice.asmx">权限WebService/privilegesBLLservice.asmx</a>
    <br />
    <a href="WebService/privileges_typeBLLservice.asmx">权限类型WebService/privileges_typeBLLservice.asmx</a>
    <br />
    <a href="WebService/privilege_roleBLLservice.asmx">权限角色WebService/privilege_roleBLLservice.asmx</a>
    <br />
    <a href="WebService/operationsBLLservice.asmx">功能操作WebService/operationsBLLservice.asmx</a>
    <br />
    <a href="WebService/menusBLLservice.asmx">菜单WebService/menusBLLservice.asmx</a>
    <br />
    <a href="WebService/init_statusBLLservice.asmx">页面元素初始化状态代码WebService/init_statusBLLservice.asmx</a>
    <br />
    <a href="WebService/elementsBLLservice.asmx">页面元素WebService/elementsBLLservice.asmx</a>
    <br />
    <a href="WebService/v_privilegesBLLservice.asmx">权限项目视图WebService/v_privilegesBLLservice.asmx</a>
    <br />
    <a href="WebService/flowstep_typeBLLservice.asmx">流程步骤类型代码WebService/flowstep_typeBLLservice.asmx</a>
    <br />
    <a href="WebService/step_actionBLLservice.asmx">流程处理结果代码WebService/step_actionBLLservice.asmx</a>
    <br />
    <a href="WebService/flowsBLLservice.asmx">流程信息WebService/flowsBLLservice.asmx</a>
    </form>
</body>
</html>
