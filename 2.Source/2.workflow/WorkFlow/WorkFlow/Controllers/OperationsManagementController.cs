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
        /// <summary>
        /// 显示所选系统的详情
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>
        public ActionResult DetailInfo(int id)
        {
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();
            m_operationsModel = m_operationsBllService.GetModel(id);
            ViewData["operationsName"] = m_operationsModel.name;
            ViewData["operationsCode"] = m_operationsModel.code;
            ViewData["operationsDescription"] = m_operationsModel.description;
            ViewData["operationsRemark"] = m_operationsModel.remark;
            ViewData["operationsApp_id"] = m_operationsModel.app_id;
            ViewData["operationsInvalid"] = m_operationsModel.invalid;
            ViewData["operationsDeleted"] = m_operationsModel.deleted;
            ViewData["operationsCreated_at"] = m_operationsModel.created_at;
            ViewData["operationsCreated_by"] = m_operationsModel.created_by;
            ViewData["operationsCreated_ip"] = m_operationsModel.created_ip;
            ViewData["operationsUpdated_at"] = m_operationsModel.updated_at;
            ViewData["operationsUpdated_by"] = m_operationsModel.updated_by;
            ViewData["operationsUpdated_ip"] = m_operationsModel.updated_ip;
            return View();
        }
        ///<summay>
        ///编辑数据库中指定记录的操作
        ///</summay>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult EditPage(int id)
        {
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();
            m_operationsModel = m_operationsBllService.GetModel(id);
            ViewData["operationsName"] = m_operationsModel.name;
            ViewData["operationsCode"] = m_operationsModel.code;
            ViewData["operationsDescription"] = m_operationsModel.description;
            ViewData["operationsRemark"] = m_operationsModel.remark;
            ViewData["operationsApp_id"] = m_operationsModel.app_id;
            ViewData["operationsInvalid"] = m_operationsModel.invalid;
            return View();
        }
        ///<summary>
        ///删除数据库中指定记录的操作
        ///</summary>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult ChangePage(int id)
        {
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();
            if (m_operationsBllService.Delete(id))
            {
                return RedirectToAction("AppOperations");
            }
            else 
            {
                return View();
            }      
        }
        ///<summary>
        ///向数据库中添加记录的操作
        ///</summary>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult RegisterOperations(FormCollection collection)
        {
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();
            string m_operationsName=collection["operationsName"].Trim();
            string m_operationsCode = collection["operationsCode"].Trim();
            string m_operationsDescription = collection["operationsDescription"].Trim();
            string m_operationsRemark = collection["operationsRemark"].Trim();
            string m_operationsApp_id = collection["operationsApp_id"].Trim();
            string m_operationsInvalid = collection["operationsInvalid"].Trim();
            //string m_operationsDeleted = collection["operationsDeleted"].Trim();
           // string m_operationsCreated_at = collection["operationsCreated_at"].Trim();
           // string m_operationsCreated_by = collection["operationsCreated_by"].Trim();
           // string m_operationsCreated_ip = collection["operationsCreated_ip"].Trim();
           // string m_operationsUpdated_at = collection["operationsUpdated_at"].Trim();
           // string m_operationsUpdated_by = collection["operationsUpdated_by"].Trim();
           // string m_operationsUpdated_ip = collection["operationsUpdated_ip"].Trim();
            if (m_operationsName.Length== 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success=false,css="p-errorDIV",message="操作名称不能为空!"});
            }
            //获取operations表中所有name的值       
            DataSet ds = m_operationsBllService.GetOperationsNameList();
            var total = ds.Tables[0].Rows.Count;
            ArrayList operationsList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                operationsList.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            foreach (string operationsname in operationsList)
            {
                if (operationsname.Equals(collection["operationsName"].Trim()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的操作名称!" });
                }
            }
            m_operationsModel.name = collection["operationsName"].Trim();
            m_operationsModel.code = collection["operationsCode"].Trim();
            m_operationsModel.description = collection["operationsDescription"].Trim();
            m_operationsModel.remark = collection["operationsRemark"].Trim();
            m_operationsModel.app_id = Convert.ToInt32(collection["operationsApp_id"].Trim());
            m_operationsModel.invalid = Convert.ToBoolean(collection["operationsInvalid"].Trim());
            m_operationsModel.deleted = false;
            m_operationsModel.created_at = System.DateTime.Now;
            m_operationsModel.created_by = 11;
            m_operationsModel.created_ip=Convert.ToString(Session["createdIP"]);
            m_operationsBllService.Add(m_operationsModel);
            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功！", toUrl = "/OperationsManagement/AppOperations" });
        }
    }
}
