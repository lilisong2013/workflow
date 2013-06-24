using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Saron.WorkFlowService.Model;
using System.Web.Services.Protocols;
namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// base_userBLLservice系统维护用户
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class base_userBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.base_userDAL m_base_userdal = new Saron.WorkFlowService.DAL.base_userDAL();

        public SecurityContext m_securityContext = new SecurityContext();


        [WebMethod(Description = "（超级管理员登录验证）是否存在用户名login且密码password的超级管理员,<h4>（无需授权验证）</h4>")]
        public bool LoginValidator(string login, string password)
        {
            return m_base_userdal.ExistsSuperAdmin(login, password);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新一条超级管理员记录,<h4>（需要授权验证）</h4>")]
        public bool Update(base_userModel model,out string msg)
        {
            //对webservice进行授权验证,超级管理员才可访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_base_userdal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "修改超级管理员密码,<h4>（需要授权验证）</h4>")]
        public bool ModifyPassword(string login, string password,out string msg)
        {
            //对webservice进行授权验证,超级管理员才可访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_base_userdal.ModifyPassword(login, password);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象,<h4>（需要授权验证）</h4>")]
        public base_userModel GetModel(int id,out string msg)
        {
            //对webservice进行授权验证,超级管理员才可访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_base_userdal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据登录名login得到一个实体对象,<h4>（需要授权验证）</h4>")]
        public base_userModel GetModelByLogin(string login,out string msg)
        {
            //对webservice进行授权验证,超级管理员才可访问
            if (!m_securityContext.SuperAdminIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_base_userdal.GetModel(login);
        }
    }
}
