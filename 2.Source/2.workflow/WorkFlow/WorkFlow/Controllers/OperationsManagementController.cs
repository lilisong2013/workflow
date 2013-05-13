using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Reflection;
using System.Collections;

namespace WorkFlow.Controllers
{
    public class OperationsManagementController : Controller
    {
        //
        // GET: /OperationsManagement/

        public ActionResult AppOperations()
        {
            return View();
        }
        /// <summary>
        /// 显示数据库中功能操作表的信息
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>
        public ActionResult GetOperations_Apply()
        {
            //排序的字段名
            string sortname=Request.Params["sortname"];
            //排序的方向
            string sortorder=Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsService = new OperationsWebService.operationsBLLservice();
            DataSet ds = m_operationsService.GetAllOperationsList();
            IList<WorkFlow.OperationsWebService.operationsModel> m_list = new List<WorkFlow.OperationsWebService.operationsModel>();
            
            var total = ds.Tables[0].Rows.Count;
            for(var i=0;i<total;i++)
            {
               WorkFlow.OperationsWebService.operationsModel m_operationsModel=(WorkFlow.OperationsWebService.operationsModel)Activator.CreateInstance(typeof(WorkFlow.OperationsWebService.operationsModel));
               PropertyInfo[] m_propertys=m_operationsModel.GetType().GetProperties();
               foreach(PropertyInfo pi in m_propertys)
               {
                for(int j=0;j<ds.Tables[0].Columns.Count;j++)
                {
                  //属性与字段名称一致的进行赋值
                  if(pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                  {
                   //数据库NULL值单独处理
                      if(ds.Tables[0].Rows[i][j]!=DBNull.Value)
                          pi.SetValue(m_operationsModel,ds.Tables[0].Rows[i][j],null);
                      else
                          pi.SetValue(m_operationsModel,null,null);
                      break;
                  }
                }
               }
              m_list.Add(m_operationsModel);
            }
            //模拟排序操作
            if(sortorder=="desc")
                m_list=m_list.OrderByDescending(c=>c.id).ToList();
            else
                m_list=m_list.OrderBy(c=>c.id).ToList();
            IList<WorkFlow.OperationsWebService.operationsModel> m_targetList=new List<WorkFlow.OperationsWebService.operationsModel>();
            //模拟分页操作
            for(var i=0;i<total;i++)
            {
              if(i>=(page-1)*pagesize&&i<page*pagesize)
              {
                m_targetList.Add(m_list[i]);
              }
            }
            var gridData=new
            {
             Rows=m_targetList,
             Total=total
            };
            return Json(gridData);
        }
    }
}
