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
    /// menusBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class menusBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.menusDAL m_menusDal = new Saron.WorkFlowService.DAL.menusDAL();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id的记录")]
        public bool Exists(int id)
        {
            return m_menusDal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.menusModel model)
        {
            return m_menusDal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.menusModel model)
        {
            return m_menusDal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_menusDal.Delete(id);
        }
        ///<summar>
        ///删除一条记录，令deleted=1
        /// </summar>
        [WebMethod(Description = "令deleted=1的记录")]
        public bool DeleteID(int id)
        {
            return m_menusDal.DeleteID(id);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_menusDal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.menusModel GetModel(int id)
        {
            return m_menusDal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetMenusList(string strWhere)
        {
            return m_menusDal.GetList(strWhere);
        }
        
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        [WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        public DataSet GetMenusTopList(int Top, string strWhere, string filedOrder)
        {
            return m_menusDal.GetList(Top, strWhere, filedOrder);
        }
        

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllMenusList()
        {
            return GetMenusList("");
        }
        ///<summary>
        ///获得某系统的Code、parent_id编码
        /// </summary>
        [WebMethod(Description = "获得某系统的code、parent_id")]
        public DataSet GetCodeParentOfApp(int appId, int Id)
        {
            return m_menusDal.GetCodeParentOfApp(appId,Id);
        }
        /// <summary>
        /// 获得某系统的id,parent_id编码
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [WebMethod(Description="获得系统appid的id、parent_id")]
        public DataSet GetAllParentIdOfApp(int appId)
        {
            return m_menusDal.GetAllParentIdOfApp(appId);
        }
        /// <summary>
        /// 获得某系统某菜单的子菜单
        /// </summary>
        [WebMethod(Description = "获得某系统某菜单的子菜单")]
        public DataSet GetChildrenMenus(int parentID)
        {
            return m_menusDal.GetChildrenMenusListOfApp(parentID);
        }

        /// <summary>
        /// 获得某系统的顶级菜单
        /// </summary>
        [WebMethod(Description = "获得某系统的顶级菜单")]
        public DataSet GetTopMenusListOfApp(int appID)
        {
            return m_menusDal.GetTopMenusListOfApp(appID);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "id为id的菜单是否存在子菜单记录")]
        public bool ExistsChildrenMenus(int parentId)
        {
            return m_menusDal.ExistsChildrenMenus(parentId);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        [WebMethod(Description = "获得记录总条数")]
        public int GetRecordCount(string strWhere)
        {
            return m_menusDal.GetRecordCount(strWhere);
        }
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        [WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return m_menusDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
     
        #endregion  Method
    }
}
