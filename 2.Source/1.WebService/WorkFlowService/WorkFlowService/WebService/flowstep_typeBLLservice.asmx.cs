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
    /// flowstep_typeBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class flowstep_typeBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.flowstep_typeDAL m_flowstep_typeDal = new Saron.WorkFlowService.DAL.flowstep_typeDAL();
        
        public SecurityContext m_securityContext = new SecurityContext();

        #region Method
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = " 获得流程步骤类型代码数据列表，<h4>（需要授权验证，管理员用户）</h4>")]
        public DataSet GetFlowStep_TypeList(out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }

            return m_flowstep_typeDal.GetFlowStep_TypeList();
        }

        #endregion
    }
}
