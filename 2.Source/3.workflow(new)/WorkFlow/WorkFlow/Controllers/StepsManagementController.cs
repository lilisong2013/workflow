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
    public class StepsManagementController : Controller
    {
        //
        // GET: /StepsManagement/

        //AppSteps页面
        public ActionResult AppSteps()
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

        //添加步骤
        public ActionResult AddStep(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                
                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                int userID = m_usersModel.id;
                string stepName = collection["stepsName"];
                if (stepName.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称不能为空!'}");
                }

                m_stepsModel.name = collection["stepsName"];
                m_stepsModel.remark = collection["stepsRemark"];
                m_stepsModel.flow_id = Convert.ToInt32(collection["flowsName"]);
                m_stepsModel.step_type_id = Convert.ToInt32(collection["stepsType"]);
                m_stepsModel.repeat_count = Convert.ToInt32(collection["repeatCount"]);
                m_stepsModel.order_no = Convert.ToInt32(collection["orderNo"]);
                m_stepsModel.created_by = (int)m_usersModel.id;
                m_stepsModel.created_at = Convert.ToDateTime(collection["stepsCreated_at"].Trim());
                m_stepsModel.created_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();
       
                
                try
                {
                    if (m_stepsBllService.AddStep(m_stepsModel, userID, out msg))
                    {
                        return Json("{success:true,css:'alert alert-success'}");
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error'}");
                    }
                }
                catch (Exception ex)
                {
                    return Json("{success:false,css:'alert alert-error'}");
                }
            }
        }

    }
}
