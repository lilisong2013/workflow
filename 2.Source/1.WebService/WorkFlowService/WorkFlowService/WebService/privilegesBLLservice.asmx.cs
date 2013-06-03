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
    /// privilegesBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class privilegesBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.privilegesDAL m_privilegesDal = new Saron.WorkFlowService.DAL.privilegesDAL();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id的记录")]
        public bool Exists(int id)
        {
            return m_privilegesDal.Exists(id);
        }

        /// <summary>
        /// 某种权限类型下某种权限项目的权限是否已经存在
        /// </summary>
        [WebMethod(Description = "某种权限类型下某种权限项目的权限是否已经存在")]
        public bool ExistsItemOfPrivilegesType(int privilegesTypeID, int privilegesItemID)
        {
            return m_privilegesDal.ExistsItemOfPrivilegesType(privilegesTypeID, privilegesItemID);
        }

        /// <summary>
        /// 某系统中是否存在权限名称
        /// </summary>
        [WebMethod(Description = "系统ID为appID的系统中是否存在privilegeName的权限名称")]
        public bool ExistsPrivilegeName(string privilegeName, int appID)
        {
            return m_privilegesDal.ExistsPrivilegesName(privilegeName, appID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.privilegesModel model)
        {
            return m_privilegesDal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.privilegesModel model)
        {
            return m_privilegesDal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_privilegesDal.Delete(id);
        }
        
        /// <summary>
        /// 批量删除数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_privilegesDal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.privilegesModel GetModel(int id)
        {
            return m_privilegesDal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetPrivilegesList(string strWhere)
        {
            return m_privilegesDal.GetList(strWhere);
        }
       
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        [WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        public DataSet GetPrivilegesTopList(int Top, string strWhere, string filedOrder)
        {
            return m_privilegesDal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllPrivilegesList()
        {
            return GetPrivilegesList("");
        }

        /// <summary>
        /// 获得某系统的所有权限列表
        /// </summary>
        [WebMethod(Description = "获得某系统的所有权限列表")]
        public DataSet GetAllListByAppID(int appID)
        {
            return m_privilegesDal.GetAllListByAppID(appID);
        }

        /// <summary>
        /// 获得某系统某种权限类型下的权限列表
        /// </summary>
        [WebMethod(Description = "获得某系统某种权限类型下的权限列表")]
        public DataSet GetListByPrivilegeType(int privilegeTypeID, int appID)
        {
            return m_privilegesDal.GetListByPrivilegeType(privilegeTypeID, appID);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        [WebMethod(Description = "获得记录总条数")]
        public int GetRecordCount(string strWhere)
        {
            return m_privilegesDal.GetRecordCount(strWhere);
        }
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        [WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return m_privilegesDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  Method
    }
}
