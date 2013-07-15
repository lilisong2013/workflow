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
    public class CodesManagementController : Controller
    {
        //
        // GET: /CodesManagement/

        public ActionResult AppCodes()
        {
            if (Session["user"] == null)
            {
               return  RedirectToAction("Home","Login");
            }
            else
            { 
                return View(); 
            }         
        }
        public ActionResult GetFlowstep_Type_List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                string msg = string.Empty;
                WorkFlow.Flowstep_TypeWebService.flowstep_typeBLLservice m_flowstep_typeBllService = new Flowstep_TypeWebService.flowstep_typeBLLservice();
                WorkFlow.Flowstep_TypeWebService.SecurityContext m_SecurityContext = new Flowstep_TypeWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_flowstep_typeBllService.SecurityContextValue = m_SecurityContext;

               DataSet ds=m_flowstep_typeBllService.GetFlowStep_TypeList(out msg);
               ArrayList tablename=new ArrayList();
               ArrayList tableremark= new ArrayList();
               ArrayList tableorder_no = new ArrayList();
               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               {
                   tablename.Add(ds.Tables[0].Rows[i][1]);
                   tableremark.Add(ds.Tables[0].Rows[i][2]);
                   tableorder_no.Add(ds.Tables[0].Rows[i][3]);
               }
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
                           string name = ds.Tables[0].Rows[i][1].ToString();
                           string id = ds.Tables[0].Rows[i][0].ToString();
                           string remark = ds.Tables[0].Rows[i][2].ToString();
                           string order_no = ds.Tables[0].Rows[i][3].ToString();
                           if (i == ds.Tables[0].Rows.Count - 1)
                           {
                               data += "{name:'" + name + "',";
                               data += "id:'" + id + "',";
                               data += "remark:'" + remark + "',";
                               data += "order_no:'" + order_no + "'}";
                           }
                           else
                           { 
                               data += "{name:'" + name + "',";
                               data += "id:'" + id + "',";
                               data += "remark:'" + remark + "',";
                               data += "order_no:'" + order_no + "'},";
                           }
                        }
                       
                   } catch (Exception ex) { }
                   
                   data += "]}";
                   return Json(data);
                   }
                                   
               }
            }
        public ActionResult GetStep_Action_List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                

                    WorkFlow.Step_ActionWebService.step_actionBLLservice m_step_actionBllService = new Step_ActionWebService.step_actionBLLservice();
                    WorkFlow.Step_ActionWebService.SecurityContext m_SecurityContext = new Step_ActionWebService.SecurityContext();

                    string msg = string.Empty;
                    WorkFlow.UsersWebService.usersModel m_usersModel =(WorkFlow.UsersWebService.usersModel)Session["user"];

                    m_SecurityContext.UserName = m_usersModel.login;
                    m_SecurityContext.PassWord = m_usersModel.password;
                    m_SecurityContext.AppID = (int)m_usersModel.app_id;
                    m_step_actionBllService.SecurityContextValue = m_SecurityContext;

                    DataSet ds = m_step_actionBllService.GetStep_ActionList(out msg);
                    
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
                                string name = ds.Tables[0].Rows[i][1].ToString();
                                string id = ds.Tables[0].Rows[i][0].ToString();
                                string remark = ds.Tables[0].Rows[i][2].ToString();
                                string order_no = ds.Tables[0].Rows[i][3].ToString();
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "',";
                                    data += "order_no:'" + order_no + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "',";
                                    data += "order_no:'" + order_no + "'},";
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