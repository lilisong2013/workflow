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
    /// operationsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class operationsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.operationsDAL m_operationsDal = new Saron.WorkFlowService.DAL.operationsDAL();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id的记录")]
        public bool Exists(int id)
        {
            return m_operationsDal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.operationsModel model)
        {
            return m_operationsDal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.operationsModel model)
        {
            return m_operationsDal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_operationsDal.Delete(id);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_operationsDal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.operationsModel GetModel(int id)
        {
            return m_operationsDal.GetModel(id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetOperationsList(string strWhere)
        {
            return m_operationsDal.GetList(strWhere);
        }
        ///<summary>
        /// 获得operations表中所有的name列
        /// </summary>
        [WebMethod(Description = "获取operations表中所有的name字段的值")]
        public DataSet GetOperationsNameList()
        {
            return m_operationsDal.GetNameList();
        }

        ///<summary>
        /// 获得某系统中操作的数据列表
        /// </summary>
        [WebMethod(Description = "获得某系统中操作的数据列表")]
        public DataSet GetOperationsListOfApp(int appID)
        {
            return m_operationsDal.GetOperationsListOfApp(appID);
        }


        [WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        public DataSet GetOperationsTopList(int Top, string strWhere, string filedOrder)
        {
            return m_operationsDal.GetList(Top, strWhere, filedOrder);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllOperationsList()
        {
            return GetOperationsList("");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        [WebMethod(Description = "获得记录总条数")]
        public int GetRecordCount(string strWhere)
        {
            return m_operationsDal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        [WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return m_operationsDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }


        #endregion  Method
    }
}
