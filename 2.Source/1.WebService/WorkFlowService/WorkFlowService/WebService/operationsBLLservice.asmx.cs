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
    /// operationsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class operationsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.operationsDAL m_operationsDal = new Saron.WorkFlowService.DAL.operationsDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条操作记录,<h4>（需要授权验证）</h4>")]
        public int Add(Saron.WorkFlowService.Model.operationsModel model,out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_operationsDal.Add(model);

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
        [WebMethod(Description = "更新一条操作记录,<h4>（需要授权验证）</h4>")]
        public bool Update(Saron.WorkFlowService.Model.operationsModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_operationsDal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除操作主键为id的记录,<h4>（需要授权验证）</h4>")]
        public bool DeleteOperations(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_operationsDal.DeleteOperations(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象,<h4>（需要授权验证）</h4>")]
        public Saron.WorkFlowService.Model.operationsModel GetModel(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_operationsDal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获取operations表中所有的name字段的值")]
        public DataSet GetOperationsNameList(out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_operationsDal.GetNameList();
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统中操作的数据列表,<h4>（需要授权验证）</h4>")]
        public DataSet GetOperationsListOfApp(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_operationsDal.GetOperationsListOfApp(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统中操作的Code数据列表")]
        public DataSet GetCodeListOfApp(int app_id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_operationsDal.GetCodeListOfApp(app_id);
        }

        #endregion  Method
    }
}
