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
        public ActionResult GetStepsList()
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

                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_stepsBllService.GetFlowStepListByAppID((int)m_usersModel.app_id,out msg);
                IList<WorkFlow.StepsWebService.v_stepsModel> m_list=new List<WorkFlow.StepsWebService.v_stepsModel>();

                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.StepsWebService.v_stepsModel mv_stepsModel = (WorkFlow.StepsWebService.v_stepsModel)Activator.CreateInstance(typeof(WorkFlow.StepsWebService.v_stepsModel));
                    PropertyInfo[] m_propertys = mv_stepsModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        { 
                           //属性与字段名称一致的进行赋值
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            { 
                               //数据库NULL单独处理
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(mv_stepsModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(mv_stepsModel,null,null);
                                break;
                            }
                        }
                    }
                    m_list.Add(mv_stepsModel);
                }

                IList<WorkFlow.StepsWebService.v_stepsModel> m_targetList=new List<WorkFlow.StepsWebService.v_stepsModel>();
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
                    Rows=m_targetList,
                    Total=total
                };
                return Json(gridData);
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
               
                string userName = collection["stepsUser"];
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
                if (userName.Equals("请选择"))
                {
                    return Json("{success:false,css:'alert alert-error',message:'请选择操作的用户!'}");
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

                int userID = Convert.ToInt32(collection["stepsUser"]);
                m_stepsModel.name = collection["stepsName"];
                m_stepsModel.remark = collection["stepsRemark"];
                m_stepsModel.flow_id = Convert.ToInt32(collection["flowsName"]);
                m_stepsModel.step_type_id = Convert.ToInt32(collection["stepsType"]);
                //m_stepsModel.repeat_count = Convert.ToInt32(collection["repeatCount"]);
                
                //m_stepsModel.order_no = m_stepsBllService.GetFlowMaxOrderNum((int)m_stepsModel.flow_id, out msg) + 1;
                
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
       
       //获取步骤用户列表
        public ActionResult GetStepUserName()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext ms_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.SecurityContext mu_SecurityContext = new UsersWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                mu_SecurityContext.UserName = m_usersModel.login;
                mu_SecurityContext.PassWord = m_usersModel.password;
                mu_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_usersBllService.SecurityContextValue = mu_SecurityContext;

                DataSet ds = m_usersBllService.GetUserListByAppID((int)m_usersModel.app_id,out msg);
                List<Saron.WorkFlow.Models.FlowStepUsersHelper> m_stepuserlist = new List<Saron.WorkFlow.Models.FlowStepUsersHelper>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    m_stepuserlist.Add(new Saron.WorkFlow.Models.FlowStepUsersHelper { StepuserID = Convert.ToInt32(ds.Tables[0].Rows[i][0]), StepuserName=Convert.ToString(ds.Tables[0].Rows[i][1]) });
                }
                var dataJson = new { 
                  Rows=m_stepuserlist,
                  Total=ds.Tables[0].Rows.Count
                };
                return Json(dataJson,JsonRequestBehavior.AllowGet);
            }

        }

       //删除流程步骤
        public ActionResult DeleteFlowStep()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int stepID = Convert.ToInt32(Request.Form["stepID"]);
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext ms_SecurityContext = new StepsWebService.SecurityContext();

                
                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;
                ms_SecurityContext.UserName = m_usersModel.login;
                ms_SecurityContext.PassWord = m_usersModel.password;
                ms_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = ms_SecurityContext;

                try 
                {
                    if (m_stepsBllService.DeleteStep(stepID, out msg))
                    {
                        return Json("{success:true,css:'alert alert-success',message:'成功删除步骤!'}");
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'"+msg+"'}");
                    }
                }
                catch (Exception ex) {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
        }

       //流程步骤详情
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.StepsWebService.v_stepsModel mv_stepsModel= m_stepsBllService.GetV_StepsModel(id,out msg);

                ViewData["s_name"] = mv_stepsModel.s_name;
                ViewData["f_name"] = mv_stepsModel.f_name;
                ViewData["step_type_name"] = mv_stepsModel.step_type_name;
                ViewData["order_no"] = mv_stepsModel.order_no;

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext ma_SecurityContext = new AppsWebService.SecurityContext();

                ma_SecurityContext.UserName = m_usersModel.login;
                ma_SecurityContext.PassWord = m_usersModel.password;
                ma_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_appsBllService.SecurityContextValue = ma_SecurityContext;

                ViewData["appsName"] = m_appsBllService.GetAppNameByID((int)m_usersModel.app_id,out msg);
                
                return View();
            }
        }

        //添加并行节点详情
        public ActionResult AddNode(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();
                WorkFlow.StepsWebService.v_stepsModel mv_stepsModel = new StepsWebService.v_stepsModel();

                string msg = string.Empty;
                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                mv_stepsModel = m_stepsBllService.GetV_StepsModel(id, out msg);

                ViewData["s_id"] = mv_stepsModel.s_id;
                ViewData["s_name"] = mv_stepsModel.s_name;
                ViewData["f_name"] = mv_stepsModel.f_name;
                ViewData["step_type_name"] = mv_stepsModel.step_type_name;
                ViewData["order_no"] = mv_stepsModel.order_no;
                ViewData["app_id"] = mv_stepsModel.app_id;
                ViewData["f_id"] = mv_stepsModel.f_id;

                return View();
            }
        }

        //判断一下步骤是顺序还是并序
        public ActionResult GetStepType()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int id = Convert.ToInt32(Request.Params["StepID"]);

                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                
                string msg = string.Empty;
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.StepsWebService.stepsModel m_stepsModel =new StepsWebService.stepsModel();
                try
                {
                    m_stepsModel = m_stepsBllService.GetModelByID(id, out msg);
                    int steptypeID = Convert.ToInt32(m_stepsModel.step_type_id);
                    return Json("{steptypeID:'" + steptypeID + "'}");
                }
                catch (Exception ex)
                {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
               
                
                
            }
        }
    }
}
