using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
<<<<<<< HEAD
using System.Web.Services.Protocols;
=======
>>>>>>> df28b0b202da255851e8d8093c0ecee4aeaf9f95
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

<<<<<<< HEAD
        public SecurityContext m_securityContext = new SecurityContext();

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "判断用户是否有访问项目的权限（userID：用户ID，item_Code：项目编码，pt_Name：权限类型名称，appID：系统ID）")]
        public bool UserIsItemPrivilege(int userID, string item_Code, string pt_Name, int appID,out string msg)
        {
            //对webservice进行授权验证
            if (!m_securityContext.UserIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID,out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            //项目是否创建相应权限
            int privilegeID = m_v_privilegesDal.GetPrivilegeID(item_Code, pt_Name, appID);
            if (privilegeID == 0)
            {
                //若项目不存在相应权限，用户可以访问该项目，返回true
                return true;
            }

            //判断用户是否有访问项目的权限
            if (m_v_userDal.ExistsUser_Privilege(userID, privilegeID))
            {
                //用户有访问权限
                return true;
            }
            else
            {
                //用户无访问权限
                msg = "无权限访问此项目";
                return false;
            }
=======
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
>>>>>>> df28b0b202da255851e8d8093c0ecee4aeaf9f95
        }
    }
}
