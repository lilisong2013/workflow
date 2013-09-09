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
    /// user_roleBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class user_roleBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.user_roleDAL m_userRoleDal = new Saron.WorkFlowService.DAL.user_roleDAL();
        ///<summary>
        ///是否存在用户id为user_id,角色id为role_id
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "判断是否存在用户id为user_id,角色id为role_id的记录")]
        public bool Exists(int user_id, int role_id)
        {
            return m_userRoleDal.Exists(user_id,role_id);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.user_roleModel model)
        {
            return m_userRoleDal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.user_roleModel model)
        {
            return m_userRoleDal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_userRoleDal.Delete(id);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_userRoleDal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.user_roleModel GetModel(int id)
        {
            return m_userRoleDal.GetModel(id);
        }

        /// <summary>
        /// 某用户存在多少个角色
        /// </summary>
        [WebMethod(Description = "角色user_id的权限个数")]
        public int User_RoleCountByUserID(int user_id)
        {
            return m_userRoleDal.User_RoleCountByUserID(user_id);
        }
        /// <summary>
        /// 按照user_id批量删除
        /// </summary>
        [WebMethod(Description = "删除用户id为user_id的所有权限")]
        public bool DeleteByRoleID(int user_id)
        {
            int rpCount=User_RoleCountByUserID(user_id);
            int deleteRp=m_userRoleDal.DeleteByUserID(user_id);
            if (deleteRp < rpCount)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        [WebMethod(Description = "根据流程角色ID获得用户ID列表")]
        public DataSet GetUserListByRoleID(int roleID)
        {
            return m_userRoleDal.GetUserListByRoleID(roleID);
        }
    }
}
