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
    /// flowsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class flowsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.flowsDAL m_flowsDal = new DAL.flowsDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "某系统中流程信息是否已经存在，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsFlowName(string name, int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问(密码为密文)
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_flowsDal.ExistsFlowName(name, appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "添加一条流程信息记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public int AddFlow(Saron.WorkFlowService.Model.flowsModel model, out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问(密码为密文)
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            if (m_flowsDal.ExistsFlowName(model.name, (int)model.app_id))
            {
                msg = "系统中流程信息已经存在！";
                return result;
            }

            result = m_flowsDal.Add(model);

            if (result == 0)
            {
                msg = "流程信息添加失败";
            }
            else
            {
                msg = "";
            }

            return result;
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得系统中流程信息列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetListOfFlows(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问(密码为密文)
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_flowsDal.GetListOfFlows(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据系统名称获得系统中流程信息列表，<h4>（需要授权验证，系统管理员）</h4>")]
        public DataSet GetListOfFlowsByName(string flowName, int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问(密码为密文)
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_flowsDal.GetListOfFlowsByName(flowName,appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据id删除系统中的流程信息，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool DeleteFlow(int id, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问(密码为密文)
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_flowsDal.Delete(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新流程信息，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool UpdateFlow(flowsModel flowModel, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问(密码为密文)
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_flowsDal.Update(flowModel);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个流程信息的实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.flowsModel GetFlowModel(int id, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_flowsDal.GetModel(id);
        }

        #endregion
    }
}
