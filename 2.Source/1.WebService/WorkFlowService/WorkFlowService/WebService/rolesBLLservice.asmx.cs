using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Saron.WorkFlowService.Model;

namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// rolesBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class rolesBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.rolesDAL m_rolesDal = new Saron.WorkFlowService.DAL.rolesDAL();
        private readonly Saron.WorkFlowService.DAL.privilege_roleDAL m_privilege_roledal = new DAL.privilege_roleDAL();
        private readonly Saron.WorkFlowService.DAL.user_roleDAL m_user_roledal = new DAL.user_roleDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public int Add(Saron.WorkFlowService.Model.rolesModel model,out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_rolesDal.Add(model);

            if (result == 0)
            {
                msg = "添加失败";
            }
            else
            {
                msg = "";
            }

            return result;
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新一条记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool Update(Saron.WorkFlowService.Model.rolesModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_rolesDal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除id为id的记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool Delete(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            int privilege_roleCount = m_privilege_roledal.Privilege_RoleCountByRoleID(id);
            int user_roleCount = m_user_roledal.User_RoleCountByRoleID(id);
            if (privilege_roleCount > 0 || user_roleCount > 0)
            {
                msg = "角色已经分配权限或者被用户使用，无法删除";
                return false;
            }
            else
            {
                return m_rolesDal.Delete(id);
            }

            
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.rolesModel GetModel(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_rolesDal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某应用系统的角色数据列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetAllRolesListOfApp(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_rolesDal.GetAllRolesListOfApp(appID);
        }

        #endregion  Method
    }
}
