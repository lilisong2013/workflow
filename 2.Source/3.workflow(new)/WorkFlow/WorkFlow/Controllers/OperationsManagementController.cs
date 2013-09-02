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

        //AppOperations页面
        public ActionResult AppOperations()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        
        //后台分页获取操作数据列表
        public ActionResult GetOperations_List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                //排序的字段名
                string sortname = Request.Params["sortname"];
                //排序的方向
                string sortorder = Request.Params["sortorder"];
                //当前页
                int page = Convert.ToInt32(Request.Params["page"]);
                //每页显示的记录数
                int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

                string msg = string.Empty;
                WorkFlow.OperationsWebService.operationsBLLservice m_operationsService = new OperationsWebService.operationsBLLservice();
                WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_operationsService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                DataSet ds = m_operationsService.GetOperationsListOfApp((int)m_usersModel.app_id,out msg);
                
                IList<WorkFlow.OperationsWebService.operationsModel> m_list=new List<WorkFlow.OperationsWebService.operationsModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.OperationsWebService.operationsModel m_operationsModel = (WorkFlow.OperationsWebService.operationsModel)Activator.CreateInstance(typeof(WorkFlow.OperationsWebService.operationsModel));
                    PropertyInfo[] m_propertys = m_operationsModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            // 属性与字段名称一致的进行赋值 
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
        
        }
        
        //删除一条记录
        public ActionResult DeleteOperation()
        {
            Boolean flag = false;//判断删除的标志
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                
                string msg = string.Empty;
                int operationID = Convert.ToInt32(Request.Form["operationID"]);
                WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
                WorkFlow.OperationsWebService.SecurityContext m_SecurityContext = new OperationsWebService.SecurityContext();

                WorkFlow.OperationsWebService.operationsModel m_operationModel = new OperationsWebService.operationsModel();
                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_operationsBllService.SecurityContextValue = m_SecurityContext;


                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext mo_SecurityContext = new PrivilegesWebService.SecurityContext();

                mo_SecurityContext.UserName = m_usersModel.login;
                mo_SecurityContext.PassWord = m_usersModel.password;
                mo_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = mo_SecurityContext;

                //获得操作权限项目在权限中的权限项目号列表
                DataSet ods = m_privilegesBllService.GetItemIDOfPrivileges((int)m_usersModel.app_id,3,out msg);
                var total = ods.Tables[0].Rows.Count;
                ArrayList oidlist = new ArrayList();

                //权限项目列表中操作项目的ID列表
                if(total!=0)
                {
                    for (int i = 0; i < total; i++)
                    {
                        oidlist.Add(ods.Tables[0].Rows[i][3]);
                    }
                }

                //判断一下当前的操作ID(operationID)是否和权限项目列表中操作项目的ID列表相等
                foreach (int idlist in oidlist)
                {
                    if (idlist.Equals(operationID))
                    {
                        flag = true;//存在
                    }
                    else
                    {
                        flag = false;//不存在
                    }
                    
                }
                try
                {
                    if (flag == false)
                    {
                        if (m_operationsBllService.DeleteOperations(operationID, out msg))
                        {

                            return Json("{success:true,css:'alert alert-success',message:'删除成功!'}");
                        }
                        else
                        {

                            return Json("{success:false,css:'alert alert-error',message:'删除失败!'}");
                        }

                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'权限项目中已经设置了操作功能，不能删除!'}");
                    }
                   
                }
                catch (Exception ex)
                {
                    
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
         
        }

        //获取是否有效的列表
        public ActionResult GetInvalidList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int m_operationID = Convert.ToInt32(Request.Params["operationID"]);//用户ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";

                WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();

                WorkFlow.OperationsWebService.SecurityContext m_SecurityContext = new OperationsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_operationsBllService.SecurityContextValue = m_SecurityContext;
                WorkFlow.OperationsWebService.operationsModel operationsModel = m_operationsBllService.GetModel(m_operationID, out msg);
                string m_selected = string.Empty;
                int total = 1;
                int m_operationsID = m_operationID;
                string m_InvalidName;
                m_InvalidName = "是";
             

                strJson += "{id:'" + m_operationsID + "',";
                strJson += "name:'" + m_InvalidName + "',";
                strJson += "selected:'" + m_selected + "'}";


                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
          
          
        }
      
        // 显示操作的详情  
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
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

                m_operationsModel = m_operationsBllService.GetModel(id, out msg);
                ViewData["operationsName"] = m_operationsModel.name;
                ViewData["operationsCode"] = m_operationsModel.code;
                ViewData["operationsDescription"] = m_operationsModel.description;
                ViewData["operationsRemark"] = m_operationsModel.remark;

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext mp_SecurityContext = new AppsWebService.SecurityContext();
                mp_SecurityContext.UserName = m_usersModel.login;
                mp_SecurityContext.PassWord = m_usersModel.password;
                mp_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_appsBllService.SecurityContextValue = mp_SecurityContext;
                ViewData["operationsApp_id"] = m_appsBllService.GetAppNameByID((int)m_usersModel.app_id,out msg);
      
              
           
                ViewData["operationsCreated_at"] = m_operationsModel.created_at;
                ViewData["operationsCreated_by"] = m_usersModel.login;
                ViewData["operationsCreated_ip"] = m_operationsModel.created_ip;
                ViewData["operationsUpdated_at"] = m_operationsModel.updated_at;
                ViewData["operationsUpdated_by"] = m_usersModel.login;
                ViewData["operationsUpdated_ip"] = m_operationsModel.updated_ip;
                return View();
            }
          
        }
        
        //获取一条为id的操作信息
        public ActionResult EditPage(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
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

                m_operationsModel = m_operationsBllService.GetModel(id, out msg);
                ViewData["operationsId"] = m_operationsModel.id;
                ViewData["operationsName"] = m_operationsModel.name;
                ViewData["operationsCode"] = m_operationsModel.code;
                ViewData["operationsDescription"] = m_operationsModel.description;
                ViewData["operationsDeleted"] = m_operationsModel.deleted;
                ViewData["operationsRemark"] = m_operationsModel.remark;
                ViewData["operationsApp_id"] = m_operationsModel.app_id;
              
                return View();
            }
         
        }
        
        //编辑数据库中指定记录的操作
        public ActionResult EditOperations(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                int m_oi_total = Convert.ToInt32(Request.Form["oi_Total"]);//功能"是否有效"的数量
                string msg = string.Empty;
                WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
                WorkFlow.OperationsWebService.operationsModel m_operationsModel = new OperationsWebService.operationsModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.OperationsWebService.SecurityContext m_securityContext = new OperationsWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_operationsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                int appID = Convert.ToInt32(m_usersModel.app_id);
                int m_operationsId = Convert.ToInt32(collection["operationsId"].Trim());
                m_operationsModel = m_operationsBllService.GetModel(m_operationsId, out msg);
                string name = collection["operationsName"].Trim().ToString();
                string code = collection["operationsCode"].Trim().ToString();
                string remark = collection["operationsRemark"].Trim().ToString();
                //string invalid = collection["operationsInvalid"].Trim();
                if (name.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'操作名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["operationsName"]) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'操作名称含有非法字符,只能包含字母、汉字、数字、下划线!'}");
                }
                if (code.Length == 0)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'操作编码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsCode(code) == false)
                {

                    return Json("{success:false,css:'alert alert-error',message:'操作编码以字母开头,不能超过40个字符!'}");
                }
                if (Convert.ToInt32(remark.ToString().Length) > 150)
                {
                    return Json("{success:false,css:'alert alert-error',message:'备注的长度不能超过150个字符!'}");
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
                DataSet codeds = m_operationsBllService.GetCodeListOfApp(appID, out msg);
                ArrayList codeList = new ArrayList();
                var codetotal = codeds.Tables[0].Rows.Count;
                for (int i = 0; i < codetotal; i++)
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
           
                // m_operationsModel.invalid = Convert.ToBoolean(collection["operationsInvalid"].Trim());
                m_operationsModel.updated_at = t;
                m_operationsModel.updated_by = m_usersModel.id;
                m_operationsModel.updated_ip = collection["operationsCreated_ip"].Trim();
                foreach (string operationListname in operationsList)
                {
                    if (operationListname.Equals(m_operationsModel.name.ToString()))
                    {
     
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的操作名称!'}");
                    }
                }
                foreach (string codename in codeList)
                {
                    if (codename.Equals(m_operationsModel.code.ToString()))
                    {

                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的操作编码!'}");
                    }
                }
                try
                {
                    //修改后的操作名称与数据库表中的操作名称不相同并且操作名称不是本身自己            
                    if (m_operationsBllService.Update(m_operationsModel, out msg))
                    {

                        return Json("{success:true,css:'alert alert-success',message:'操作修改成功!'}");
                    }
                    else
                    {

                        return Json("{success:false,css:'alert alert-error',message:'修改失败!'}");
                    }
                }
                catch (Exception ex)
                {
                    
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
         
          
        }
             
        //添加操作信息   
        public ActionResult AddOperations(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
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
                string remark = collection["operationsRemark"].Trim();
                if (m_operationsName.Length == 0)
                {

                    return Json("{success:false,css:'alert alert-error',message:'操作名称不能为空!'}");

                }
                if(Saron.Common.PubFun.ConditionFilter.IsValidString(collection["operationsName"])==false)
                {
                   return Json("{success:fasle,css:'alert alert-error',message:'操作名称含有非法字符，只能包含字母、汉字、数字、下划线!'}");
                }
                if (m_operationsCode.Length == 0)
                {
         
                    return Json("{success:false,css:'alert alert-error',message:'操作编码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsCode(m_operationsCode) == false)
                {
            
                    return Json("{success:false,css:'alert alert-error',message:'操作编码以字母开头,不能超过40个字符!'}");
                }
                if (Convert.ToInt32(remark.ToString().Length) > 150)
                {
                    return Json("{success:false,css:'alert alert-error',message:'备注的长度不能超过150个字符!'}");
                }
                string m_operationsDescription = collection["operationsDescription"].Trim();
                string m_operationsRemark = collection["operationsRemark"].Trim();

                //获取operations表中所有name的值       
                DataSet ds = m_operationsBllService.GetOperationsListOfApp((int)m_usersModel.app_id,out msg);
                var total = ds.Tables[0].Rows.Count;
                ArrayList operationsList = new ArrayList();
                for (int i = 0; i < total; i++)
                {
                    operationsList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                foreach (string operationsname in operationsList)
                {
                    if (operationsname.Equals(collection["operationsName"].Trim()))
                    {
                    
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的操作名称!'}");
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
               
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的操作编码!'}");
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
                    if (m_operationsBllService.Add(m_operationsModel, out msg) != 0)
                    {
               
                        return Json("{success:true,css:'alert alert-success',message:'操作添加成功!'}");
                    }
                    else
                    {
   
                        return Json("{success:false,css:'alert alert-error',message:'操作添加失败!'}");
                    }
                }
                catch (Exception ex)
                {

                    return Json("{success:false,css:'alert alert-error',message:'程序出错!'}");
                }
            }
        
        }

        //操作功能中的模糊查询
        public ActionResult GetListByOperationName(string operationName)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
                WorkFlow.OperationsWebService.SecurityContext m_SecurityContext = new OperationsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_operationsBllService.SecurityContextValue = m_SecurityContext;

                 //如果搜索字段为空，默认为显示全部数据
                if (operationName.Length == 0)
                {
                    DataSet ds = m_operationsBllService.GetOperationsListOfApp((int)m_usersModel.app_id,out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else
                    {
                        try {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string remark = Convert.ToString(ds.Tables[0].Rows[i][4]);
                                string code = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                string description = Convert.ToString(ds.Tables[0].Rows[i][3]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "',";
                                    data += "description:'" + description + "',";
                                    data += "remark:'" + remark + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "',";
                                    data += "description:'" + description + "',";
                                    data += "remark:'" + remark + "'},";
                                }
                            }
                        }
                        catch (Exception ex) { }
                        data += "]}";
                        return Json(data);
                    }
                }
                //如果搜索字段不为空，按照搜索字段显示部分数据列表
                else
                {
                    DataSet ds = m_operationsBllService.GetListByOperationName(operationName,(int)m_usersModel.app_id,out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else
                    {
                        try {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string remark = Convert.ToString(ds.Tables[0].Rows[i][4]);
                                string code = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                string description = Convert.ToString(ds.Tables[0].Rows[i][3]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "',";
                                    data += "description:'" + description + "',";
                                    data += "remark:'" + remark + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "',";
                                    data += "description:'" + description + "',";
                                    data += "remark:'" + remark + "'},";
                                }
                            }
                        }
                        catch (Exception ex) { }

                        data += "]}";
                        return Json(data);
                    }
                }
            }
        }


    }
}
