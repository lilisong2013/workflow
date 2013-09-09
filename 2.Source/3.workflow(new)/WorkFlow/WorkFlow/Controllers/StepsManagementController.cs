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
                return RedirectToAction("Login", "Home");
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

        //获取流程步骤列表(后台分页)
        public ActionResult GetFlowStepsList(int flowid)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
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

                DataSet ds = m_stepsBllService.GetFlowStepListByFlowID(flowid, out msg);
                IList<WorkFlow.StepsWebService.v_stepsModel> m_list = new List<WorkFlow.StepsWebService.v_stepsModel>();

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
                                    pi.SetValue(mv_stepsModel, null, null);
                                break;
                            }
                        }
                    }
                    m_list.Add(mv_stepsModel);
                }

                IList<WorkFlow.StepsWebService.v_stepsModel> m_targetList = new List<WorkFlow.StepsWebService.v_stepsModel>();
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
                string flowName = collection["flowsName1"];
                int flowID = Convert.ToInt32(collection["flowsID1"]);
                string stepsType = collection["stepsType"];
                bool flag=false;
                if (stepName.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称不能为空!'}");
                }
                if (stepName.Length < 5)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称长度至少是四个字符以上!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(stepName) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称含有非法字符，只能包含字母、汉字、数字、下划线!'}");
                }
           
                if (stepsType.Equals("请选择"))
                {
                    return Json("{success:false,css:'alert alert-error',message:'请选择步骤类型!'}");
                }
                if (userName.Equals("请选择"))
                {
                    flag = false;//步骤用户暂时不维护
                    //return Json("{success:false,css:'alert alert-error',message:'你确定暂时先不选择步骤用户吗?'}");
                }
                else
                {
                    flag = true;//步骤用户已添加上
                }
                if (m_stepsBllService.ExistStepName(stepName,flowID,out msg))
                {
                    return Json("{success:false,css:'alert alert-error',message:'已经存在相同的步骤名称!'}");
                }
                if (flag == true)
                {
                    int userID = Convert.ToInt32(collection["stepsUser"]);
                    m_stepsModel.name = collection["stepsName"];
                    m_stepsModel.remark = collection["stepsRemark"];
                    m_stepsModel.flow_id = Convert.ToInt32(collection["flowsID1"]);
                    m_stepsModel.step_type_id = Convert.ToInt32(collection["stepsType"]);

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
                else 
                {

                    //int userID = Convert.ToInt32(collection["stepsUser"]);
                    m_stepsModel.name = collection["stepsName"];
                    m_stepsModel.remark = collection["stepsRemark"];
                    m_stepsModel.flow_id = Convert.ToInt32(collection["flowsID1"]);
                    m_stepsModel.step_type_id = Convert.ToInt32(collection["stepsType"]);

                    m_stepsModel.created_by = (int)m_usersModel.id;
                    m_stepsModel.created_at = Convert.ToDateTime(collection["stepsCreated_at"].Trim());
                    m_stepsModel.created_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();


                    try
                    {
                        if (m_stepsBllService.AddNoUserStep(m_stepsModel,out msg))
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
               
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.SecurityContext mr_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.User_RoleBLLservice.user_roleBLLservice m_uroleBllService = new User_RoleBLLservice.user_roleBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();

                mu_SecurityContext.UserName = m_usersModel.login;
                mu_SecurityContext.PassWord = m_usersModel.password;
                mu_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_usersBllService.SecurityContextValue = mu_SecurityContext;

                mr_SecurityContext.UserName = m_usersModel.login;
                mr_SecurityContext.PassWord = m_usersModel.password;
                mr_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = mr_SecurityContext;

                //根据流程角色名称获得角色ID
                DataSet rds = m_rolesBllService.GetRoleIDByName((int)m_usersModel.app_id,out msg);
                int froleID = Convert.ToInt32(rds.Tables[0].Rows[0][0]); 
               
                //根据角色ID获得用户ID列表
                DataSet uIDds = m_uroleBllService.GetUserListByRoleID(froleID);

                int total = uIDds.Tables[0].Rows.Count;
                
                ArrayList UList = new ArrayList();
                int[] uid=new int[total];
               
                ArrayList LNlist = new ArrayList();            
                string[] uname=new string[total];

                for (int i = 0; i < total; i++)
                {
                    UList.Add(uIDds.Tables[0].Rows[i][0]);
                    uid[i] = Convert.ToInt32(uIDds.Tables[0].Rows[i][0]);
                }

                int j=0;
                foreach(int ulist in UList)
                {
                    m_userModel = m_usersBllService.GetModelByID(ulist,out msg);
                    LNlist.Add(m_userModel.login + "-" + m_userModel.name);
                    uname[j++] = Convert.ToString(m_userModel.login + "-" + m_userModel.name);
                }

                //DataSet ds = m_usersBllService.GetUserListByAppID((int)m_usersModel.app_id,out msg);

                List<Saron.WorkFlow.Models.FlowStepUsersHelper> m_stepuserlist = new List<Saron.WorkFlow.Models.FlowStepUsersHelper>();

                for (int i = 0; i <total; i++)
                {
                    m_stepuserlist.Add(new Saron.WorkFlow.Models.FlowStepUsersHelper { StepuserID = Convert.ToInt32(uid[i]), StepuserName = Convert.ToString(uname[i]) });
                }
                var dataJson = new { 
                  Rows=m_stepuserlist,
                  Total=total
                };
                return Json(dataJson,JsonRequestBehavior.AllowGet);
            }

        }

       //删除一条流程步骤
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

                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();
                
                m_stepsModel = m_stepsBllService.GetModelByID(stepID,out msg);

                int flow_ID = Convert.ToInt32(m_stepsModel.flow_id);

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

       //批量删除流程信息
        public ActionResult DeleteFlowSteps()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int flowID = Convert.ToInt32(Request.Params["FlowID"]);
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                string msg = string.Empty;
                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
               
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                ArrayList IDList = new ArrayList();
                ArrayList fuList = new ArrayList();

                DataSet ds = m_stepsBllService.GetFlowStepListByFlowID(flowID,out msg);
                var total = ds.Tables[0].Rows.Count;
                for (int i = 0; i < total; i++)
                {
                    IDList.Add(ds.Tables[0].Rows[i][0]);
                    if (m_stepsBllService.ExistsFlowUser(Convert.ToInt32(ds.Tables[0].Rows[i][0]), out msg))
                    {
                        fuList.Add(ds.Tables[0].Rows[i][0]);
                    }
                }
                bool flag=false;
                bool fuag = true;
                try
                {
                    foreach (int fulist in fuList)
                    {
                        if (m_stepsBllService.DeleteFlow_User(fulist, out msg))
                        {
                            fuag = true;
                        }
                        else
                        {
                            fuag = false;
                        }
                    }

                    if (fuag == true)
                    {
                        foreach (int idlist in IDList)
                        {
                            if (m_stepsBllService.DeleteStep(idlist, out msg))
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'删除失败!'}");
                    }
                  
                    if (flag == true)
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

        //添加并行节点的操作
        public ActionResult AddBStepNodes(FormCollection collection)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                bool uag=true;
                int Flowno=Convert.ToInt32(Request.Params["flowid"]);
                int Orderno = Convert.ToInt32(Request.Params["orderno"]);
                int Repeatno = Convert.ToInt32(Request.Params["repeatcount"]);
                string msg = string.Empty;
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;
                
                WorkFlow.StepsWebService.stepsModel m_stepsModel=new StepsWebService.stepsModel();

                string stepsName=collection["BstepsName"];
                string userName = collection["BstepsUser"];
                if (stepsName.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称不能为空!'}");
                }
                if (stepsName.Length < 5)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称长度至少四个字符以上!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(stepsName) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称含有非法字符，只能包含字母、汉字、数字、下划线!'}");
                }

                if (userName.Equals("请选择"))
                {
                    uag = false;//暂时不添加步骤用户

                    //int userID = Convert.ToInt32(collection["BstepsUser"]);
                    m_stepsModel.name = collection["BstepsName"];
                    m_stepsModel.remark = collection["BstepsRemark"];
                    m_stepsModel.flow_id = Flowno;
                    m_stepsModel.step_type_id = 2;
                    m_stepsModel.repeat_count = Convert.ToInt32(Repeatno) + 1;

                    m_stepsModel.order_no = Orderno;
                    m_stepsModel.deleted = false;
                    m_stepsModel.created_by = Convert.ToInt32(m_usersModel.id);
                    m_stepsModel.created_at = Convert.ToDateTime(collection["BstepsCreated_at"]);
                    m_stepsModel.created_ip = Convert.ToString(Saron.Common.PubFun.IPHelper.GetClientIP());

                    try
                    {
                        bool flag = false;
                        if (m_stepsBllService.ExistStepName(m_stepsModel.name, (int)m_stepsModel.flow_id, out msg) == true)
                        {//存在相同的名称
                            flag = false;
                        }
                        else
                        {//与添加的与系统的名称不相同
                            flag = true;
                        }
                        if (flag == false)
                        {
                            return Json("{success:false,css:'alert alert-error',message:'该流程下存在相同的步骤名称!'}");
                        }
                        else
                        {
                            if (m_stepsBllService.AddNoUserNode(m_stepsModel,out msg))//添加成功
                            {
                                //统计下流程flow_id下排序码为order_no下的
                                int repeat_count = m_stepsBllService.GetOrderNoCount((int)m_stepsModel.flow_id, (int)m_stepsModel.order_no, out msg);
                                if (m_stepsBllService.UpdateNode((int)m_stepsModel.flow_id, (int)m_stepsModel.order_no, repeat_count, out msg))
                                {
                                    return Json("{success:true,css:'alert alert-success',message:'添加同步成功!'}");
                                }
                                else
                                {
                                    return Json("{success:false,css:'alert alert-error',message:'添加同步失败!'}");
                                }

                            }
                            else
                            {
                                return Json("{success:false,css:'alert alert-error',message:'添加失败!'}");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                    }

                }
                else
                {
                    uag = true;//步骤用户已经添加上

                    int userID = Convert.ToInt32(collection["BstepsUser"]);
                    m_stepsModel.name = collection["BstepsName"];
                    m_stepsModel.remark = collection["BstepsRemark"];
                    m_stepsModel.flow_id = Flowno;
                    m_stepsModel.step_type_id = 2;
                    m_stepsModel.repeat_count = Convert.ToInt32(Repeatno) + 1;

                    m_stepsModel.order_no = Orderno;
                    m_stepsModel.deleted = false;
                    m_stepsModel.created_by = Convert.ToInt32(m_usersModel.id);
                    m_stepsModel.created_at = Convert.ToDateTime(collection["BstepsCreated_at"]);
                    m_stepsModel.created_ip = Convert.ToString(Saron.Common.PubFun.IPHelper.GetClientIP());

                    try
                    {
                        bool flag = false;
                        if (m_stepsBllService.ExistStepName(m_stepsModel.name, (int)m_stepsModel.flow_id, out msg) == true)
                        {//存在相同的名称
                            flag = false;
                        }
                        else
                        {//与添加的与系统的名称不相同
                            flag = true;
                        }
                        if (flag == false)
                        {
                            return Json("{success:false,css:'alert alert-error',message:'该流程下存在相同的步骤名称!'}");
                        }
                        else
                        {
                            if (m_stepsBllService.AddNode(m_stepsModel, userID, out msg))//添加成功
                            {
                                //统计下流程flow_id下排序码为order_no下的
                                int repeat_count = m_stepsBllService.GetOrderNoCount((int)m_stepsModel.flow_id, (int)m_stepsModel.order_no, out msg);
                                if (m_stepsBllService.UpdateNode((int)m_stepsModel.flow_id, (int)m_stepsModel.order_no, repeat_count, out msg))
                                {
                                    return Json("{success:true,css:'alert alert-success',message:'添加同步成功!'}");
                                }
                                else
                                {
                                    return Json("{success:false,css:'alert alert-error',message:'添加同步失败!'}");
                                }

                            }
                            else
                            {
                                return Json("{success:false,css:'alert alert-error',message:'添加失败!'}");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                    }

                }
            
            }
        }

        //添加并行节点的操作
        public ActionResult AddStepNodes(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                int ID = Convert.ToInt32(collection["s_id"]);

                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();
                m_stepsModel = m_stepsBllService.GetModelByID(ID, out msg);

                int userID = Convert.ToInt32(collection["stepsUser"]);

                m_stepsModel.name = collection["s_name"];
                m_stepsModel.remark = collection["nodesRemark"];
                m_stepsModel.flow_id = m_stepsModel.flow_id;
                m_stepsModel.step_type_id = m_stepsModel.step_type_id;
                m_stepsModel.repeat_count = Convert.ToInt32(m_stepsModel.repeat_count) + 1;

                m_stepsModel.order_no = m_stepsModel.order_no;
                m_stepsModel.deleted = false;
                m_stepsModel.created_by = Convert.ToInt32(collection["nodesCreated_by"]);
                m_stepsModel.created_at = Convert.ToDateTime(collection["nodesCreated_at"]);
                m_stepsModel.created_ip = Convert.ToString(collection["nodesCreated_ip"]);

                try {
                    bool flag=false;
                    if (m_stepsBllService.ExistStepName(m_stepsModel.name, (int)m_stepsModel.flow_id, out msg) == true)
                    {//存在相同的名称
                        flag = false;
                    }
                    else
                    {//与添加的与系统的名称不相同
                        flag = true;
                    }
                    if (flag == false)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'该流程下存在相同的步骤名称!'}");
                    }
                    else
                    {
                        if (m_stepsBllService.AddNode(m_stepsModel, userID, out msg))//添加成功
                        {
                            //统计下流程flow_id下排序码为order_no下的
                            int repeat_count = m_stepsBllService.GetOrderNoCount((int)m_stepsModel.flow_id, (int)m_stepsModel.order_no, out msg);
                            if (m_stepsBllService.UpdateNode((int)m_stepsModel.flow_id, (int)m_stepsModel.order_no, repeat_count, out msg))
                            {
                                return Json("{success:true,css:'alert alert-success',message:'添加同步成功!'}");
                            }
                            else
                            {
                                return Json("{success:false,css:'alert alert-error',message:'添加同步失败!'}");
                            }

                        }
                        else
                        {
                            return Json("{success:false,css:'alert alert-error',message:'添加失败!'}");
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
              
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
  
        //根据step_id获得对应的flow_id
        public ActionResult GetFlowID()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int stepID = Convert.ToInt32(Request.Params["stepID"]);
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();

                try
                {
                    m_stepsModel = m_stepsBllService.GetModelByID(stepID, out msg);
                    int flowID = Convert.ToInt32(m_stepsModel.flow_id);
                    return Json("{flowID:'"+flowID+"'}");
                }
                catch (Exception ex) {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
               


            }
        }

        //编辑详情
        public ActionResult EditPage(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                string msg = string.Empty;
                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();
                WorkFlow.StepsWebService.v_stepsModel mv_stepsModel = new StepsWebService.v_stepsModel();

                m_stepsModel = m_stepsBllService.GetModelByID(id,out msg);
                mv_stepsModel = m_stepsBllService.GetV_StepsModel(id,out msg);

                ViewData["s_id"] = m_stepsModel.id;
                ViewData["s_name"] = m_stepsModel.name;
                ViewData["s_remark"] = m_stepsModel.remark;

                ViewData["s_flow_id"] = m_stepsModel.flow_id;
                ViewData["s_flow_name"] = mv_stepsModel.f_name;

                ViewData["s_step_type_id"] = m_stepsModel.step_type_id;
                ViewData["s_step_type_name"] = mv_stepsModel.step_type_name;

                ViewData["s_repeat_count"] = m_stepsModel.repeat_count;
             
                ViewData["s_order_no"] = m_stepsModel.order_no;
                ViewData["s_deleted"] = m_stepsModel.deleted;
                ViewData["s_created_at"] = m_stepsModel.created_at;
                ViewData["s_created_by"] = m_stepsModel.created_by;
                ViewData["s_created_ip"] = m_stepsModel.created_ip;
                ViewData["s_updated_at"] = m_stepsModel.updated_at;
                ViewData["s_updated_by"] = m_stepsModel.updated_by;
                ViewData["s_updated_ip"] = m_stepsModel.updated_ip;
                return View();
            }

        }
        
       //编辑信息
        public ActionResult EditStep(FormCollection collection)
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

                int stepID = Convert.ToInt32(collection["EstepID"].Trim());
                int flowID = Convert.ToInt32(Request.Params["flowid"]);
               
                string stepName = collection["EstepsName"].Trim();
                string stepUser = collection["EstepsUser"].Trim();

                if (stepName.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(stepName)==false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'步骤名称中含有非法字符,只能包含字母、汉字、数字、下划线!'}");
                }
                if (stepUser.Equals("请选择"))
                {
                    return Json("{success:false,css:'alert alert-error',message:'请选择操作步骤用户!'}");
                }

                int userID = Convert.ToInt32(collection["EstepsUser"]);

                try
                {
                    bool flag = false;
                    WorkFlow.StepsWebService.stepsModel m_stepModel = new StepsWebService.stepsModel();
                    m_stepModel = m_stepsBllService.GetModelByID(stepID,out msg);
                    if (m_stepModel.name.Equals(collection["EstepsName"]))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag == true)
                    {
                        if (m_stepsBllService.ExistStepName(stepName, flowID, out msg) == true)
                        {
                            return Json("{success:false,css:'alert alert-error',message:'该流程下存在相同的步骤名称!'}");
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
               
                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();
                m_stepsModel = m_stepsBllService.GetModelByID(stepID,out msg);
                m_stepsModel.name = collection["EstepsName"].Trim();
                m_stepsModel.remark = collection["E_stepsRemark"].Trim();
                m_stepsModel.updated_at = Convert.ToDateTime(collection["E_stepsCreated_at"]);
                m_stepsModel.updated_by = Convert.ToInt32(collection["EstepsUser"]);
                m_stepsModel.updated_ip = Convert.ToString(Saron.Common.PubFun.IPHelper.GetClientIP());
                try {
                    bool Tag = true;
                    bool Eag = true;
                    bool Aag = true;
                    bool Dag = true;
                    WorkFlow.StepsWebService.flow_usersModel m_flowuserModel=new StepsWebService.flow_usersModel();                 
                    m_flowuserModel.user_id=userID;
                    m_flowuserModel.step_id=stepID;

                    if (m_stepsBllService.ExistsFlowUser(stepID,out msg))//存在此记录
                    {
                        Eag = true;
                        if (m_stepsBllService.DeleteFlow_User(stepID, out msg))//删除此记录
                        {
                            Dag = true;
                        }
                        else
                        {
                            Dag = false;
                        }

                        if (m_stepsBllService.AddFlow_User(m_flowuserModel, out msg))//添加
                        {
                            Aag = true;
                        }
                        else
                        {
                            Aag = false;
                        }

                    }
                    else
                    { //不存在此记录
                        Eag = false;
                        if (m_stepsBllService.AddFlow_User(m_flowuserModel, out msg))//添加
                        {
                            Aag = true;
                            Eag = true;
                        }
                        else
                        {
                            Aag = false;
                        }
                    }
                    if (m_stepsBllService.UpdateFlowUser(stepID, userID, out msg))
                    {
                        Tag = true;//更新flow_user表成功
                    }
                    else
                    {
                        Tag = false;//更新flow_user表失败
                    }
                    if (Tag == true&&Aag==true&&Eag==true&&Dag==true)
                    {
                        if (m_stepsBllService.Update(m_stepsModel, out msg))
                        {
                            return Json("{success:true,css:'alert alert-success',message:'更新成功!'}");
                        }
                        else
                        {
                            return Json("{success:false,css:'alert alert-error',message:'更新失败!'}");
                        }
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'更新失败!'}");
                    }
                   
                }
                catch (Exception ex) {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
        }

        //根据step_id获得step实体
        public ActionResult GetStepsModel()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int step_id = Convert.ToInt32(Request.Params["StepID"]);

                WorkFlow.StepsWebService.stepsBLLservice m_stepsBllService = new StepsWebService.stepsBLLservice();
                WorkFlow.StepsWebService.SecurityContext m_SecurityContext = new StepsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                string msg = string.Empty;

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_stepsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.StepsWebService.stepsModel m_stepsModel = new StepsWebService.stepsModel();
                try {
                   m_stepsModel = m_stepsBllService.GetModelByID(step_id,out msg);
                   string stepname = m_stepsModel.name;
                   string remark = m_stepsModel.remark;
                   string steptype;
                   if (Convert.ToInt32(m_stepsModel.step_type_id) == 1)
                   {
                        steptype = "顺序";
                   }
                   else
                   {
                        steptype = "并行";
                   }
                   return Json("{Name:'"+stepname+"',Type:'"+steptype+"',Remark:'"+remark+"'}");
                }
                catch (Exception ex) {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
              
               
            }
        }

    }
}
