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
    /// privilege_roleBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class privilege_roleBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.privilege_roleDAL m_privilege_roleDal = new Saron.WorkFlowService.DAL.privilege_roleDAL();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在角色id为role_id，权限id为privilege_id的记录")]
        public bool Exists(int role_id, int privilege_id)
        {
            return m_privilege_roleDal.Exists(role_id, privilege_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public bool Add(Saron.WorkFlowService.Model.privilege_roleModel model)
        {
            return m_privilege_roleDal.Add(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除角色id为role_id，权限id为privilege_id的记录")]
        public bool Delete(int role_id, int privilege_id)
        {
            return m_privilege_roleDal.Delete(role_id, privilege_id);
        }
    }
}
