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
            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
            int appid = Convert.ToInt32(m_usersModel.app_id);
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsService = new OperationsWebService.operationsBLLservice();

            string msg = string.Empty;

            WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_operationsService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

           // DataSet ds = m_operationsService.GetAllOperationsList();
            DataSet ds = m_operationsService.GetOperationsListOfApp(appid,out msg);
            IList<WorkFlow.OperationsWebService.operationsModel> m_list = new List<WorkFlow.OperationsWebService.operationsModel>();

            var total = ds.Tables[0].Rows.Count;
            for (var i = 0; i < total; i++)
            {
                WorkFlow.OperationsWebService.operationsModel m_operationsModel = (WorkFlow.OperationsWebService.operationsModel)Activator.CreateInstance(typeof(WorkFlow.OperationsWebService.operationsModel));
                PropertyInfo[] m_propertys = m_operationsModel.GetType().GetProperties();
                foreach (PropertyInfo pi in m_propertys)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        //属性与字段名称一致的进行赋值
                        if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                        {
                            //数据库NULL值单独处理
                            if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                pi.SetValue(m_operationsModel, ds.Tables[0].Rows[i][j], null);
                            else
                                pi.SetValue(m_operationsModel, null, null);
                            break;
                        }
                    }
                }
                m_list.Add(m_operationsModel);
            }
            //模拟排序操作
            if (sortorder == "desc")
                m_list = m_list.OrderByDescending(c => c.id).ToList();
            else
                m_list = m_list.OrderBy(c => c.id).ToList();
            IList<WorkFlow.OperationsWebService.operationsModel> m_targetList = new List<WorkFlow.OperationsWebService.operationsModel>();
            //模拟分页操作
            for (var i = 0; i < total; i++)
            {
                if (i >= (page - 1) * pagesize && i < page * pagesize)
                {
                    m_targetList.Add(m_list[i]);
                }
            }
            var gridData = new
            {
                Rows = m_targetList,
                Total = total
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

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_operationsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            m_operationsModel = m_operationsBllService.GetModel(id,out msg);
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

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_operationsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            m_operationsModel = m_operationsBllService.GetModel(id,out msg);
            ViewData["operationsId"] = m_operationsModel.id;
            ViewData["operationsName"] = m_operationsModel.name;
            ViewData["operationsCode"] = m_operationsModel.code;
            ViewData["operationsDescription"] = m_operationsModel.description;
            ViewData["operationsDeleted"] = m_operationsModel.deleted;
            ViewData["operationsRemark"] = m_operationsModel.remark;
            ViewData["operationsApp_id"] = m_operationsModel.app_id;
            ViewData["operationsInvalid"] = m_operationsModel.invalid;
            return View();
        }
        
        ///<summay>
        ///编辑数据库中指定记录的操作
        ///</summay>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult EditOperations(FormCollection collection)
        {
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_operationsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            int appID = Convert.ToInt32(m_usersModel.app_id);
            int m_operationsId = Convert.ToInt32(collection["operationsId"].Trim());
            m_operationsModel = m_operationsBllService.GetModel(m_operationsId,out msg);
            string name = collection["operationsName"].Trim().ToString();
            string invalid = collection["operationsInvalid"].Trim();
            if (name.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "操作名称不能为空!" });
            }
            if (invalid.Length == 0 || invalid.Equals("请选择"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "是否有效不能为空!" });
            }
            DataSet ds = m_operationsBllService.GetOperationsNameList(out msg);
            var total = ds.Tables[0].Rows.Count;
            ArrayList operationsList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                operationsList.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            for (int i = 0; i < total; i++)
            {  //修改后的操作名称和本身相同
                if (m_operationsModel.name.ToString().Equals(collection["operationsName"]))
                {
                    operationsList.Remove(m_operationsModel.name);
                }
            }
            DataSet codeds = m_operationsBllService.GetCodeListOfApp(appID,out msg);
            ArrayList codeList = new ArrayList();
            var codetotal = codeds.Tables[0].Rows.Count;
            for(int i = 0; i < codetotal; i++)
            {
                codeList.Add(codeds.Tables[0].Rows[i][0].ToString());
            }
            for (int i = 0; i < total; i++)
            { //修改后的操作名称和本身相同
                if (m_operationsModel.code.ToString().Equals(collection["operationsCode"]))
                {
                    codeList.Remove(m_operationsModel.code);
                }
            }

            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);

            m_operationsModel.name = collection["operationsName"].Trim();
            m_operationsModel.code = collection["operationsCode"].Trim();
            m_operationsModel.description = collection["operationsDescription"].Trim();
            m_operationsModel.deleted = Convert.ToBoolean(collection["operationsDeleted"]);
            m_operationsModel.remark = collection["operationsRemark"].Trim();
            m_operationsModel.app_id = Convert.ToInt32(collection["operationsApp_id"].Trim());
            m_operationsModel.invalid = Convert.ToBoolean(collection["operationsInvalid"].Trim());
            m_operationsModel.updated_at = t;
            m_operationsModel.updated_by = m_usersModel.id;
            m_operationsModel.updated_ip = collection["operationsCreated_ip"].Trim();
            foreach (string operationListname in operationsList)
            {
                if (operationListname.Equals(m_operationsModel.name.ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的操作名称!" });
                }
            }
            foreach (string codename in codeList)
            {
                if (codename.Equals(m_operationsModel.code.ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的代码名称!"});
                }
            }
            try
            {
                //修改后的操作名称与数据库表中的操作名称不相同并且操作名称不是本身自己            
                if (m_operationsBllService.Update(m_operationsModel,out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功！", toUrl = "/OperationsManagement/AppOperations" });
                    // return RedirectToAction("AppOperations");
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="修改失败!"});
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序异常!"});
            }
          
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

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_operationsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            if (m_operationsBllService.DeleteOperations(id,out msg))
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
        public ActionResult AddOperations(FormCollection collection)
        {
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_operationsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
         
            string m_operationsName = collection["operationsName"].Trim();
            string m_operationsCode = collection["operationsCode"].Trim();
            if (m_operationsName.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "操作名称不能为空!" });
            }
            if (m_operationsCode.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="操作编码不能为空!"});
            }
            string m_operationsDescription = collection["operationsDescription"].Trim();
            string m_operationsRemark = collection["operationsRemark"].Trim();
            //获取operations表中所有name的值       
            DataSet ds = m_operationsBllService.GetOperationsNameList(out msg);
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
            //获取operations表中所有code的值
            DataSet dscode = m_operationsBllService.GetCodeListOfApp(Convert.ToInt32(m_usersModel.app_id), out msg);
            ArrayList codelist = new ArrayList();
            var totalcode = dscode.Tables[0].Rows.Count;
            for (int i = 0; i < totalcode; i++)
            {
                codelist.Add(dscode.Tables[0].Rows[i][0].ToString());
            }
            foreach (string codename in codelist)
            {
                if (codename.Equals(collection["operationsCode"].Trim()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的操作编码!"});
                }
            }
            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);

            m_operationsModel.name = collection["operationsName"].Trim();
            m_operationsModel.code = collection["operationsCode"].Trim();
            m_operationsModel.description = collection["operationsDescription"].Trim();
            m_operationsModel.remark = collection["operationsRemark"].Trim();
            m_operationsModel.app_id = Convert.ToInt32(collection["operationsApp_id"].Trim());
            m_operationsModel.created_at = t;
            m_operationsModel.created_by = m_usersModel.id;
            m_operationsModel.created_ip = Convert.ToString(collection["createdIP"].Trim());
            try
            {
                if (m_operationsBllService.Add(m_operationsModel,out msg) != 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "功能添加成功!", toUrl = "/OperationsManagement/AppOperations" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="添加功能失败!"});
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序出错!"});
            }

            //return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功！", toUrl = "/OperationsManagement/AppOperations" });
        }
    }
}
