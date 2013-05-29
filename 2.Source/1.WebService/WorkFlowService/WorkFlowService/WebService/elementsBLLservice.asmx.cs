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
    /// elementsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class elementsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.elementsDAL m_elementsDal = new Saron.WorkFlowService.DAL.elementsDAL();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id的记录")]
        public bool Exists(int id)
        {
            return m_elementsDal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.elementsModel model)
        {
            return m_elementsDal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.elementsModel model)
        {
            return m_elementsDal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_elementsDal.Delete(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_elementsDal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.elementsModel GetModel(int id)
        {
            return m_elementsDal.GetModel(id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetElementsList(string strWhere)
        {
            return m_elementsDal.GetList(strWhere);
        }

        /// <summary>
        /// 获得某一菜单下的页面元素
        /// </summary>
        [WebMethod(Description = "获得某一菜单下的页面元素")]
        public DataSet GetElementsListOfMenus(int menusID)
        {
            return m_elementsDal.GetElementsLisOfMenus(menusID);
        }

        /// <summary>
        /// 获得Name数据列表
        /// </summary>
        [WebMethod(Description = "获得所有元素表中所有的Name的数据列表")]
        public DataSet GetNameList()
        {
            return m_elementsDal.GetNameList();
        }
        
        
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        [WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        public DataSet GetElementsTopList(int Top, string strWhere, string filedOrder)
        {
            return m_elementsDal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllElementsList()
        {
            return GetElementsList("");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        [WebMethod(Description = "获得记录总条数")]
        public int GetRecordCount(string strWhere)
        {
            return m_elementsDal.GetRecordCount(strWhere);
        }
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        [WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return m_elementsDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }


        #endregion  Method
    }
}
