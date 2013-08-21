using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Web.Mvc.Ajax;

namespace WorkFlow.Controllers
{
    public class FlowsManagementController : Controller
    {
        //
        // GET: /FlowsManagement/

        //AppFlows页面
        public ActionResult AppFlows()
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
       
        //FlowSteps页面
        public ActionResult FlowSteps(int id)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;
                
                WorkFlow.FlowsWebService.flowsModel m_flowsModel = new FlowsWebService.flowsModel();
                m_flowsModel = m_flowsBllService.GetFlowModel(id,out msg);
                ViewData["flowsName"] = m_flowsModel.name;
                ViewData["flowsID"] = m_flowsModel.id;
                return View();
            }
        }

        //后台分页获取流程数据列表
        public ActionResult GetFlow_List()
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
               
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_flowsBllService.GetListOfFlows((int)m_usersModel.app_id, out msg);

                IList<WorkFlow.FlowsWebService.flowsModel> m_list=new List<WorkFlow.FlowsWebService.flowsModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.FlowsWebService.flowsModel m_flowsModel = (WorkFlow.FlowsWebService.flowsModel)Activator.CreateInstance(typeof(WorkFlow.FlowsWebService.flowsModel));
                    PropertyInfo[] m_propertys = m_flowsModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            { 
                              //数据库NULL值单独处理
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(m_flowsModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(m_flowsModel,null,null);
                                break;
                            }
                        }
                    }
                    m_list.Add(m_flowsModel);
                }
                //模拟排序操作
                //if (sortorder == "desc")
                //    m_list = m_list.OrderByDescending(c => c.id).ToList();
                //else
                //    m_list = m_list.OrderBy(c => c.id).ToList();
                IList<WorkFlow.FlowsWebService.flowsModel> m_targetList=new List<WorkFlow.FlowsWebService.flowsModel>();
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

        //根据流程名称搜索数据列表
        public ActionResult GetFlowName_List(string flowname)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {              
                string msg = string.Empty;
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                //搜索关键字为空的情况，显示全部数据
                if (flowname.Length == 0)
                {
                    DataSet ds = m_flowsBllService.GetListOfFlows((int)m_usersModel.app_id,out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {
                       // return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "无权访问WebService！" });
                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else
                    {
                        try
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string remark = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'},";
                                }
                            }

                        }
                        catch (Exception ex) { }

                        data += "]}";
                        return Json(data);
                    }

                }
                //搜索关键字不为空的情况，显示部分搜索结果数据
                else
                {
                    DataSet ds = m_flowsBllService.GetListOfFlowsByName(flowname, (int)m_usersModel.app_id, out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {
                        //return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "无权访问WebService！" });
                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else
                    {
                        try
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string remark = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
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
       
        //添加流程数据
        public ActionResult AddFlows(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.flowsModel m_flowsModel = new FlowsWebService.flowsModel();
                
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                string flowsName = collection["flowsName"].Trim();
                string remark = collection["flowsRemark"].Trim();
                if (flowsName.Length == 0)
                {
                  
                   return Json("{success:false,css:'alert alert-error',message:'流程名称不能为空！'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["flowsName"]) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'流程名称含有非法字符,只能包含字母、汉字、数字、下划线!'}");
                }
                if (Convert.ToInt32(remark.ToString().Length) > 150)
                {
                    return Json("{success:false,css:'alert alert-error',message:'备注不能超过150个字符!'}");
                }
                //m_flowsModel.name=collection["flowsName"];
                //获得deleted=false且应用系统ID为app_id的flowsName列表
                DataSet ds = m_flowsBllService.GetListOfFlows((int)m_usersModel.app_id,out msg);
                ArrayList flowNamelist = new ArrayList();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    flowNamelist.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                foreach (string flownamelist in flowNamelist)
                {
                    if (flownamelist.Equals(collection["flowsName"].Trim()))
                    {
                      
                        return Json("{success:false,css:'alert alert-error',message:'流程名称已经存在!'}");
                    }
                }

                m_flowsModel.name=collection["flowsName"];
                m_flowsModel.remark = collection["flowsRemark"];
                m_flowsModel.created_by =(int)m_usersModel.id;
                m_flowsModel.created_at = Convert.ToDateTime(collection["flowsCreated_at"].Trim());
                m_flowsModel.created_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();
                m_flowsModel.updated_at = Convert.ToDateTime(collection["flowsCreated_at"].Trim());
                m_flowsModel.updated_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();
                m_flowsModel.updated_by = (int)m_usersModel.id;
                m_flowsModel.app_id =(int)m_usersModel.app_id;
                try
                {
                    if (m_flowsBllService.AddFlow(m_flowsModel,out msg)==0)
                    {
                        
                        return Json("{success:false,css:'alert alert-error',message:'添加失败!'}");
                    }
                    else 
                    {
                       
                        return Json("{success:true,css:'alert alert-success',message:'流程添加成功!'}");
                    }
                }
                catch (Exception ex)
                {
                    
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
        }
   
        //流程的详情
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {

                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id,out msg);

                ViewData["flowsName"] = m_flowsModel.name;
                ViewData["flowsRemark"] = m_flowsModel.remark;
                if (m_flowsModel.invalid == true)
                {
                    ViewData["flowsInvalid"] ="否";
                }
                if (m_flowsModel.invalid == false)
                {
                    ViewData["flowsInvalid"] = "是";
                }
                ViewData["flowsCreated_at"] = m_flowsModel.created_at;
                ViewData["flowsCreated_by"] = m_usersModel.login;
                ViewData["flowsCreated_ip"] = m_flowsModel.created_ip;
                ViewData["flowsUpdated_at"] = m_flowsModel.updated_at;
                ViewData["flowsUpdated_by"] = m_usersModel.login;
                ViewData["flowsUpdated_ip"] = m_flowsModel.updated_ip;

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext ma_SecurityContext = new AppsWebService.SecurityContext();
                
                ma_SecurityContext.UserName = m_usersModel.login;
                ma_SecurityContext.PassWord = m_usersModel.password;
                ma_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_appsBllService.SecurityContextValue = ma_SecurityContext;
                ViewData["flowsApp_id"] = m_appsBllService.GetAppNameByID((int)m_usersModel.app_id,out msg);
                return View();
            }
        }
       
        //删除一条流程信息
        public ActionResult DeleteFlows()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int flowsid = Convert.ToInt32(Request.Form["flowID"]);
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();

                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                try
                {
                    if (m_flowsBllService.DeleteFlow(flowsid, out msg))
                    {
                      
                        return Json("{success:true,css:'alert alert-success',message:'删除成功!'}");
                    }
                    else
                    {
                        
                        return Json("{success:false,css:'alert alert-error',message:'删除失败!'}");
                    }
                }
                catch (Exception ex)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
        }
       
        //编辑一条流程信息
        public ActionResult EditPage(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();

                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel  m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id,out msg);

                ViewData["flowsEditCount"] = Convert.ToInt32(Session["EditPageCount"]);
                ViewData["flowsEditSize"] = Convert.ToInt32(Session["EditSizeCount"]);
                ViewData["flowsID"] = m_flowsModel.id;
                ViewData["flowsName"] = m_flowsModel.name;
                ViewData["flowsRemark"] = m_flowsModel.remark;
                ViewData["flowsInvalid"] = m_flowsModel.invalid;
                ViewData["flowsDeleted"] = m_flowsModel.deleted;
                ViewData["flowsCreated_at"] = m_flowsModel.created_at;
                ViewData["flowsCreated_by"] = m_flowsModel.created_by;
                ViewData["flowsCreated_ip"] = m_flowsModel.created_ip;
                ViewData["flowsUpdated_at"] = m_flowsModel.updated_at;
                ViewData["flowsUpdated_by"] = m_flowsModel.updated_by;
                ViewData["flowsUpdated_ip"] = m_flowsModel.updated_ip;
                ViewData["flowsApp_id"] = m_flowsModel.app_id;
                return View();
            }
        }
   
        //获取是否有效列表
        public ActionResult GetInvalidList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                int m_flowID = Convert.ToInt32(Request.Params["flowsID"]);//流程ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";

                WorkFlow.FlowsWebService.flowsModel m_flowModel = m_flowsBllService.GetFlowModel(m_flowID,out msg);

                string m_selected = string.Empty;
                int total = 1;
                int m_flowsID = m_flowID;
                string m_InvalidName;
                m_InvalidName="是";
                //判断角色中是否已经存在该权限
                if (m_flowModel.invalid == false)
                {
                    m_selected = "true";
                }
                else
                {
                    m_selected = "false";
                }
                strJson += "{id:'"+m_flowsID+"',";
                strJson += "name:'"+m_InvalidName+"',";
                strJson += "selected:'"+m_selected+"'}";

                strJson += "],total:'"+total+"'}";
                return Json(strJson);
            }
        }

        //编辑流程信息
        public ActionResult EditFlow(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int m_fi_total = Convert.ToInt32(Request.Form["fv_Total"]);//流程"是否有效"的数量
                int EditPageCount = Convert.ToInt32(Session["EditPageCount"]);
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                int id = Convert.ToInt32(collection["flowsId"].Trim());

                WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id,out msg);

                string name=collection["flowsName"].Trim();
                string remark = collection["flowsRemark"].Trim();
                if (name.Length == 0)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'流程名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["flowsName"]) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'流程名称含有非法字符,只能包含字母、数字、汉字、下划线!'}");
                }
                if (Convert.ToInt32(remark.ToString().Length) > 150)
                {
                    return Json("{success:false,css:'alert alert-error',message:'备注长度不能超过150个字符!'}");
                }
               //获得deleted=false且应用系统ID为appid的flowsName列表
                DataSet ds = m_flowsBllService.GetListOfFlows((int)m_usersModel.app_id,out msg);
                var total = ds.Tables[0].Rows.Count;
                ArrayList flowsList = new ArrayList();
                for (int i = 0; i < total; i++)
                {
                    flowsList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                //如果是自己本身，流程名称修改前和修改后的名称一样
                for (int i = 0; i < total; i++)
                {
                    if (m_flowsModel.name.ToString().Equals(collection["flowsName"].Trim().ToString()))
                    {
                        flowsList.Remove(m_flowsModel.name);
                    }
                }
                string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);
                m_flowsModel.name=collection["flowsName"].Trim();
                if(m_fi_total == 1)
                {
                    m_flowsModel.invalid = false;
                }
                if (m_fi_total == 0)
                {
                    m_flowsModel.invalid = true;
                }
                m_flowsModel.deleted = Convert.ToBoolean(collection["flowsDeleted"].Trim());
                m_flowsModel.remark=collection["flowsRemark"].Trim();
                m_flowsModel.app_id = Convert.ToInt32(collection["flowsApp_id"].Trim());
                m_flowsModel.updated_at = t;
                m_flowsModel.updated_by = Convert.ToInt32(m_usersModel.id);
                m_flowsModel.updated_ip = collection["flowsCreated_ip"].Trim();
                foreach (string flowsname in flowsList)
                {
                    if (flowsname.Equals(collection["flowsName"].Trim()))
                    {
                       
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的流程名称!'}");
                    }
                }
                try 
                {
                    if (m_flowsBllService.UpdateFlow(m_flowsModel, out msg))
                    {                 

                        m_flowsModel = m_flowsBllService.GetFlowModel(id, out msg);
                        Session["flow"] = m_flowsModel.name;

                        
                        return Json("{success:true,css:'alert alert-success',message:'修改成功!'}");
                    }
                    else
                    {
                       
                        return Json("{success:false,css:'alert alert-error',message:'修改流程失败!'}");
                    }
                }
                catch (Exception ex)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
        }

        //获取流程步骤的列表
        public ActionResult GetStepsList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int m_flowsID = Convert.ToInt32(Request.Params["flowsID"]);//流程ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";
                WorkFlow.Flowstep_TypeWebService.flowstep_typeBLLservice m_flowstep_typeBllService = new Flowstep_TypeWebService.flowstep_typeBLLservice();
                
                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;
                DataSet ds=new DataSet();
                try {
                   ds = m_stepsBllService.GetFlowStepListByFlowID(m_flowsID,out msg);
                }
                catch (Exception ex) { }
                int total = ds.Tables[0].Rows.Count;//某系统某流程下步骤的数量
                for (int i = 0; i < total; i++)
                {
                    int m_stepsID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    string m_stepsName = ds.Tables[0].Rows[i][1].ToString();
                    if (i < total - 1)
                    {
                        strJson += "{id:'" + m_stepsID + "',";
                        strJson += "name:'" + m_stepsName + "'},";
                    }
                    else
                    {
                        strJson += "{id:'" + m_stepsID + "',";
                        strJson += "name:'" + m_stepsName + "'}";
                    }
                }
                strJson += "],total:'"+total+"'";
                return Json(strJson);
            }
        }
    }
}
