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
    /// menusBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class menusBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.menusDAL m_menusDal = new Saron.WorkFlowService.DAL.menusDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条菜单记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public int Add(Saron.WorkFlowService.Model.menusModel model,out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_menusDal.Add(model);

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
        [WebMethod(Description = "更新一条菜单记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool Update(Saron.WorkFlowService.Model.menusModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_menusDal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除菜单主键为id的菜单记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool DeleteMenus(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_menusDal.DeleteMenus(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.menusModel GetModel(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_menusDal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某菜单的子菜单列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetChildrenMenus(int parentID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_menusDal.GetChildrenMenusListOfApp(parentID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统下所有菜单数据列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetAllMenusListofApp(int app_id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_menusDal.GetListOfApp(app_id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统的顶级菜单，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetTopMenusListOfApp(int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_menusDal.GetTopMenusListOfApp(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据appID和ID获得某系统的菜单名称，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetMenuNameOfAppID(int appID,int ID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_menusDal.GetMenuNameOfAppID(appID,ID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "菜单主键为parentId的菜单是否存在子菜单，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsChildrenMenus(int parentId, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }
            return m_menusDal.ExistsChildrenMenus(parentId);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "系统中菜单编码是否已经存在，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsMenusCode(string code, int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_menusDal.ExistsMenusCode(code, appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "系统中同一父菜单下的子菜单名称是否已经存在，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsMenusName(string name, int? parentID,int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            bool flag = false;

            if (parentID != null)
            {
                flag = m_menusDal.ExistsMenusName(name, parentID);
            }
            else
            {
                flag = m_menusDal.ExistsTopMenusName(name,appID);
            }

            return flag;
        }    

        #endregion  Method
    }
}
