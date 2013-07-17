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

        //流程基本信息页面
        public ActionResult AppFlows()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AppFlowsPage()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                //ViewData["flowsPageCount"] = id;
                return View();
            }
        }
        //获取流程数据列表
        public ActionResult GetFlowList()
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

                DataSet ds = m_flowsBllService.GetListOfFlows((int)m_usersModel.app_id,out msg);
                string data = "{Rows:[";
                if (ds == null)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "无权访问WebService！" });
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

                string flowsName = collection["flowsName"];
                if (flowsName.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success=false,css="p-errorDIV",message="流程名称不能为空!"});
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
                        return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="流程名称已经存在!"});
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
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "添加失败!" });                      
                    }
                    else 
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "流程添加成功!", toUrl = "/FlowsManagement/AppFlows" }); 
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常!" });
                }
            }
        }
        //流程的详细信息
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                //var id = Convert.ToInt32(Request.Form["flowID"]);
                var count = Convert.ToInt32(Request.Form["pageCount"]);
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id,out msg);
                ViewData["flowsPageCount"] = count;
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
        public ActionResult DetailConfirm()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Home","Login");
            }
            else
            {
                var id = Convert.ToInt32(Request.Form["flowID"]);
                var count = Convert.ToInt32(Request.Form["pageCount"]);
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id, out msg);
                ViewData["flowsPageCount"] = count;
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
                return RedirectToAction("FlowsManagement", "DetailInfo");
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
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "删除成功!", toUrl = "/FlowsManagement/AppFlows" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="删除失败!"});
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常!" });
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

        //编辑流程信息
    
        //public ActionResult EditPage1()
        //{
        //    if (Session["user"] == null)
        //    {
        //        return RedirectToAction("Home", "Login");
        //    }
        //    else
        //    {
               
        //        int id = Convert.ToInt32(Request.Form["flowID"]);
        //        WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();

        //        WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

        //        WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

        //        string msg = string.Empty;
        //        m_SecurityContext.UserName = m_usersModel.login;
        //        m_SecurityContext.PassWord = m_usersModel.password;
        //        m_SecurityContext.AppID = (int)m_usersModel.app_id;
        //        m_flowsBllService.SecurityContextValue = m_SecurityContext;

        //        WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id, out msg);
        //        ViewData["flowsID"] = m_flowsModel.id;
        //        ViewData["flowsName"] = m_flowsModel.name;
        //        ViewData["flowsRemark"] = m_flowsModel.remark;
        //        ViewData["flowsInvalid"] = m_flowsModel.invalid;
        //        ViewData["flowsDeleted"] = m_flowsModel.deleted;
        //        ViewData["flowsCreated_at"] = m_flowsModel.created_at;
        //        ViewData["flowsCreated_by"] = m_flowsModel.created_by;
        //        ViewData["flowsCreated_ip"] = m_flowsModel.created_ip;
        //        ViewData["flowsUpdated_at"] = m_flowsModel.updated_at;
        //        ViewData["flowsUpdated_by"] = m_flowsModel.updated_by;
        //        ViewData["flowsUpdated_ip"] = m_flowsModel.updated_ip;
        //        ViewData["flowsApp_id"] = m_flowsModel.app_id;
        //        return View();
        //    }
        //}
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
                int m_fi_total = Convert.ToInt32(Request.Params["fv_Total"]);//流程"是否有效"的数量

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
                if (name.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="流程名称不能为空!"});
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
                        return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的流程名称!"});
                    }
                }
                try 
                {
                    if (m_flowsBllService.UpdateFlow(m_flowsModel, out msg))
                    {
                        m_flowsModel = m_flowsBllService.GetFlowModel(id, out msg);
                        Session["flow"] = m_flowsModel.name;
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改流程成功!", toUrl = "/FlowsManagement/AppFlows" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="修改流程失败!"});
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序异常!"});
                }
            }
        }
    }
}
