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
    /// elementsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class elementsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.elementsDAL m_elementsDal = new Saron.WorkFlowService.DAL.elementsDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public int Add(Saron.WorkFlowService.Model.elementsModel model, out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_elementsDal.Add(model);

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
        public bool Update(Saron.WorkFlowService.Model.elementsModel model, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_elementsDal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除id为id的记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool Delete(int id, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_elementsDal.Delete(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.elementsModel GetModel(int id, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_elementsDal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据系统ID获得数据列表:appid(where条件)，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetElementsListOfApp(int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_elementsDal.GetElementsListOfApp(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某一菜单下的页面元素，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetElementsListOfMenus(int menusID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_elementsDal.GetElementsLisOfMenus(menusID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "菜单下是否存在页面元素，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsElementsOfMenus(int menusID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_elementsDal.ExistsElementsOfMenus(menusID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某菜单下的页面元素的Code列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetCodeListOfMenuApp(int app_id, int menu_id, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_elementsDal.GetCodeListOfMenuApp(app_id,menu_id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得所有数据列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetAllElementsListOfMenuApp(int appID, int menuID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_elementsDal.GetAllElementsListOfMenuApp(appID, menuID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据appID和ID获得系统中元素名称，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetElementsNameOfAppID(int appID,int ID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_elementsDal.GetElementsNameOfAppID(appID,ID);
        }
        #endregion  Method
    }
}
