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
    /// base_userBLLservice系统维护用户
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class base_userBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.base_userDAL m_base_userdal = new Saron.WorkFlowService.DAL.base_userDAL();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id的记录")]
        public bool Exists(int id)
        {
            return m_base_userdal.Exists(id);
        }

        /// <summary>
        /// （超级管理员登录）是否存在用户或密码
        /// </summary>
        [WebMethod(Description = "是否存在用户名login且密码password的超级管理员")]
        public bool LoginValidator(string login, string password)
        {
            return m_base_userdal.Exists(login, password);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(base_userModel model)
        {
            return m_base_userdal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(base_userModel model)
        {
            return m_base_userdal.Update(model);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        [WebMethod(Description = "修改密码")]
        public bool ModifyPassword(string login, string password)
        {
            return m_base_userdal.ModifyPassword(login, password);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_base_userdal.Delete(id);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_base_userdal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public base_userModel GetModel(int id)
        {
            return m_base_userdal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据登录名login得到一个实体对象")]
        public base_userModel GetModelByLogin(string login)
        {
            return m_base_userdal.GetModel(login);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetBase_UserList(string strWhere)
        {
            return m_base_userdal.GetList(strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        [WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        public DataSet GetBase_UserTopList(int Top, string strWhere, string filedOrder)
        {
            return m_base_userdal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllBase_UserList()
        {
            return GetBase_UserList("");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        [WebMethod(Description = "获得记录总条数")]
        public int GetRecordCount(string strWhere)
        {
            return m_base_userdal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        [WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return m_base_userdal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
