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
    /// privilegesBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class privilegesBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.privilegesDAL m_privilegesDal = new Saron.WorkFlowService.DAL.privilegesDAL();
        private readonly Saron.WorkFlowService.DAL.privilege_roleDAL m_privilege_roledal = new DAL.privilege_roleDAL();

        private readonly Saron.WorkFlowService.DAL.v_menu_privilegesDAL m_v_menu_privilegesdal = new DAL.v_menu_privilegesDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "某种权限类型下某权限项目的权限是否已经存在，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsItemOfPrivilegesType(int privilegesTypeID, int privilegesItemID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_privilegesDal.ExistsItemOfPrivilegesType(privilegesTypeID, privilegesItemID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "系统ID为appID的系统中是否存在privilegeName的权限名称，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsPrivilegeName(string privilegeName, int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_privilegesDal.ExistsPrivilegesName(privilegeName, appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public int Add(Saron.WorkFlowService.Model.privilegesModel model,out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_privilegesDal.Add(model);

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
        public bool Update(Saron.WorkFlowService.Model.privilegesModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_privilegesDal.Update(model);
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

            int privilege_roleCount=m_privilege_roledal.Privilege_RoleCountByPrivilegeID(id);
            if (privilege_roleCount > 0)
            {
                if (m_privilege_roledal.DeleteByPrivilegeID(id) == privilege_roleCount)
                {
                    return m_privilegesDal.Delete(id);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return m_privilegesDal.Delete(id);
            }
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.privilegesModel GetModel(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_privilegesDal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统的所有权限列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetAllListByAppID(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_privilegesDal.GetAllListByAppID(appID);
        }
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统的所有菜单列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetMListByAppTypeID(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_privilegesDal.GetMListByAppTypeID(appID);
        }

       [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统的所有元素列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetElListByAppID(int appID,out string msg)
        {  //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }
            return m_privilegesDal.GetElListByAppID(appID);
        }
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统的所有操作列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetOpListByAppID(int appID,out string msg)
        { //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }
            return m_privilegesDal.GetOpListByAppID(appID);
        }
        
      
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统某种权限类型下的权限列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetListByPrivilegeType(int privilegeTypeID, int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_privilegesDal.GetListByPrivilegeType(privilegeTypeID, appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统菜单权限的顶级菜单权限列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetTopMenuPrivilegeListOfApp(int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_v_menu_privilegesdal.GetTopMenuPrivilegeListOfApp(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统菜单权限的权限列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetMenuPrivilegeListOfApp(int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_v_menu_privilegesdal.GetMenuPrivilegeListOfApp(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "菜单权限对应菜单的父菜单ID，<h4>（需要授权验证，系统管理员）</h4>")]
        public int ParentMenuIDOfMenuPrivilege(int menuprivilegeID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return -1;
            }

            return m_v_menu_privilegesdal.GetMenuPrivilegeParentID(menuprivilegeID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据菜单ID获取菜单权限ID，<h4>（需要授权验证，系统管理员）</h4>")]
        public int GetMenuPrivilegeIDByMenuID(int menuID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return -1;
            }

            return m_privilegesDal.GetMenuPrivilegeIDByMenuID(menuID);
        }
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据权限项目类型获得相应的项目ID列表,<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetItemIDOfPrivileges(int appID, int flag,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }
            return m_privilegesDal.GetItemIDOfPrivileges(appID,flag);
        }
        
        #endregion  Method
    }
}
