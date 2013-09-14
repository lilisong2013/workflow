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
    /// step_actionBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class step_actionBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.step_actionDAL m_step_actionDal = new DAL.step_actionDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region Method

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = " 获得流程处理结果代码数据列表数据列表，<h4>（需要授权验证，管理员用户）</h4>")]
        public DataSet GetStep_ActionList(out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }

            return m_step_actionDal.GetStep_ActionList();
        }

        #endregion
    }
}
