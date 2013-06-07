using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Saron.WorkFlowService.Model;

namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// v_privilegesBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class v_privilegesBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.v_privilegesDAL m_v_privilegesDal = new Saron.WorkFlowService.DAL.v_privilegesDAL();
        private readonly Saron.WorkFlowService.DAL.v_usersDAL m_v_userDal = new Saron.WorkFlowService.DAL.v_usersDAL();

        /// <summary>
        /// 判断用户是否有访问项目的权限
        /// </summary>
        [WebMethod(Description = "判断用户是否有访问项目的权限（userID：用户ID，item_Code：项目编码，pt_Name：权限类型名称，appID：系统ID）")]
        public bool UserIsItemPrivilege(int userID, string item_Code, string pt_Name, int appID)
        {
            int privilegeID = m_v_privilegesDal.GetPrivilegeID(item_Code, pt_Name, appID);
            if (privilegeID == 0)
            {
                return true;
            }

            return m_v_userDal.ExistsUser_Privilege(userID,privilegeID);
        }
    }
}
