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

        //获取步骤列表(后台分页)

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
                string flowName = collection["flowsName"];
                string stepsType = collection["stepsType"];
                //string repeatCount = collection["repeatCount"];
                string orderNo = collection["orderNo"];
                if (stepName.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(stepName) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称含有非法字符，只能包含字母、汉字、数字、下划线!'}");
                }
                if (flowName.Equals("请选择"))
                {
                    return Json("{success:false,css:'alert alert-error',message:'请选择流程名称!'}");
                }
                if (stepsType.Equals("请选择"))
                {
                    return Json("{success:false,css:'alert alert-error',message:'请选择步骤类型!'}");
                }
                //if (repeatCount.Length == 0)
                //{
                //    return Json("{success:false,css:'alert alert-error',message:'重复次数不能为空!'}");
                //}
                //if (Saron.Common.PubFun.ConditionFilter.IsNumber(repeatCount) == false)
                //{
                //    return Json("{success:false,css:'alert alert-error',message:'重复次数只能是数字!'}");
                //}
                //if (orderNo.Length == 0)
                //{
                //    return Json("{success:false,css:'alert alert-error',message:'排序编码不能为空!'}");
                //}
                //if (Saron.Common.PubFun.ConditionFilter.IsNumber(orderNo) == false)
                //{
                //    return Json("{success:false,css:'alert alert-error',message:'排序编码只能是数字!'}");
                //}
                m_stepsModel.name = collection["stepsName"];
                m_stepsModel.remark = collection["stepsRemark"];
                m_stepsModel.flow_id = Convert.ToInt32(collection["flowsName"]);
                m_stepsModel.step_type_id = Convert.ToInt32(collection["stepsType"]);
                //m_stepsModel.repeat_count = Convert.ToInt32(collection["repeatCount"]);
                
                m_stepsModel.order_no = m_stepsBllService.GetFlowMaxOrderNum((int)m_stepsModel.flow_id, out msg) + 1;
                
                m_stepsModel.created_by = (int)m_usersModel.id;
                m_stepsModel.created_at = Convert.ToDateTime(collection["stepsCreated_at"].Trim());
                m_stepsModel.created_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();
       
                
                try
                {
                    if (m_stepsBllService.AddStep(m_stepsModel, userID, out msg))
                    {
                        return Json("{success:true,css:'alert alert-success',message:'步骤添加成功!'}");
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'" + msg + "!'}");
                    }
                }
                catch (Exception ex)
                {
                    return Json("{success:false,css:'alert alert-error',message:'" + msg + "!'}");
                }
            }
        }

        //根据流程ID获得流程名称
        public ActionResult GetFlowName()
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
                m_usersModel.app_id = (int)m_usersModel.app_id;
                m_flowsBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_flowsBllService.GetListOfFlows((int)m_usersModel.app_id, out msg);
                List<Saron.WorkFlow.Models.FlowsNameHelper> m_flowsNameList = new List<Saron.WorkFlow.Models.FlowsNameHelper>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    m_flowsNameList.Add(new Saron.WorkFlow.Models.FlowsNameHelper { FlowsID = Convert.ToInt32(ds.Tables[0].Rows[i][0]), FlowsName=Convert.ToString(ds.Tables[0].Rows[i][1])});
                }
                var dataJson = new
                {
                   Rows=m_flowsNameList,
                   Total=ds.Tables[0].Rows.Count

                };
                return Json(dataJson,JsonRequestBehavior.AllowGet);
            }
        }

       //获取步骤类型名称
        public ActionResult GetStepTypeName()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
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

                DataSet ds = m_flowstep_typeBllService.GetFlowStep_TypeList(out msg);
                List<Saron.WorkFlow.Models.FlowStepTypeHelper> m_flowstypelist = new List<Saron.WorkFlow.Models.FlowStepTypeHelper>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    m_flowstypelist.Add(new Saron.WorkFlow.Models.FlowStepTypeHelper { Flowsteptypeid = Convert.ToInt32(ds.Tables[0].Rows[i][0]), Flowsteptypename=Convert.ToString(ds.Tables[0].Rows[i][1]) });
                }
                var dataJson = new { 
                  Rows=m_flowstypelist,
                  Total=ds.Tables[0].Rows.Count
                };
                return Json(dataJson,JsonRequestBehavior.AllowGet);
            }
        }

    }
}
