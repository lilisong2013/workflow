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

                //m_flowsModel.id = 8;
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
                WorkFlow.FlowsWebService.flowsBLLservice m_flowsBllService = new FlowsWebService.flowsBLLservice();
                WorkFlow.FlowsWebService.SecurityContext m_SecurityContext = new FlowsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.FlowsWebService.flowsModel m_flowsModel = m_flowsBllService.GetFlowModel(id,out msg);
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
    }
}
