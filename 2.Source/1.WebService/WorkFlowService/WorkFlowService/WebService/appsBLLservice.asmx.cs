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
    /// appsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class appsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.appsDAL m_appsdal=new Saron.WorkFlowService.DAL.appsDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "是否存在系统名称为appName该记录，<h4>（需要授权验证，自定义用户）</h4>")]
        public bool ExistsAppName(string appName,out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AnyOneIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }

            return m_appsdal.ExistsName(appName);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "通过系统ID获得系统名称，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public string GetAppNameByID(int id, out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return "";
            }

            return m_appsdal.GetAppNameByID(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "通过系统ID获得系统名称，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public string GetAppNameByAdminID(int id, out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return "";
            }

            return m_appsdal.GetAppNameByID(id);
        }
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "除原系统外是否存在系统名称为appName该记录，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public bool ExistsAppNameOutAppModel(Saron.WorkFlowService.Model.appsModel appModel, out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }

            return m_appsdal.ExistsNameOutAppModel(appModel);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条系统记录（返回0添加失败，返回-1系统已经存在，不等于0或-1为记录ID），<h4>（需要授权验证，自定义用户）</h4>")]
        public int Add(Saron.WorkFlowService.Model.appsModel model,out string msg)
        {
            int result = 0;
            //是否有权限访问
            if (!m_securityContext.AnyOneIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return result;
            }

            if (!m_appsdal.ExistsName(model.name))
            {
                return m_appsdal.Add(model);
            }
            else
            {
                return -1;//系统名称已经存在
            }
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新一条系统记录，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public bool SuperAdminUpdateApp(Saron.WorkFlowService.Model.appsModel model,out string msg)
        {
            //对webservice进行授权验证,超级管理员才可访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_appsdal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新一条系统记录，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public bool AdminUpdateApp(Saron.WorkFlowService.Model.appsModel model, out string msg)
        {
            //对webservice进行授权验证,超级管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_appsdal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除id为id的记录，<h4>（需要授权验证，自定义用户）</h4>")]
        public bool Delete(int id,out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AnyOneIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }

            return m_appsdal.Delete(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除id为id的记录，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public bool DeleteApp(int id, out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }

            return m_appsdal.Delete(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public Saron.WorkFlowService.Model.appsModel GetModel(int id,out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            return m_appsdal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public Saron.WorkFlowService.Model.appsModel AdminGetModel(int id, out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            return m_appsdal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得有效系统数据列表，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public DataSet GetInvalidAppsList(out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }

            return m_appsdal.GetInvalidAppsList();
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得无效系统数据列表，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public DataSet GetValidAppsList(out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            return m_appsdal.GetValidAppsList();
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "统计下已审批系统的数量，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public int GetValidAppCount(out string msg)
        {
            int result = -1;
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return result;
            }

            return m_appsdal.GetValidAppCount();
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "统计下已审批系统的数量，<h4>（需要授权验证，超级管理员用户）</h4>")]
        public int GetInValidAppCount(out string msg)
        {
            int result = -1;
            //是否有权限访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return result;
            }

            return m_appsdal.GetInValidAppCount();
        }

        #endregion
    }
}
