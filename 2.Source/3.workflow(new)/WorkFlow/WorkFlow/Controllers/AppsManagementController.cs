using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Reflection;

namespace WorkFlow.Controllers
{
    public class AppsManagementController : Controller
    {
        //
        // GET: /AppsManagement/

        public ActionResult BaseUserApps()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else {
                return View();    
            }
                      
        }
        public ActionResult BU_AppsPassModifyCon()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
           
        }
        
        public ActionResult ReturnBaseUserApps()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("BaseUserApps");
            }
        }
        
        
        //删除应用系统
        public ActionResult Delete()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                bool flag;
                bool result;
                int appID = Convert.ToInt32(Request.Params["appID"]);
                string msg = string.Empty;
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext m_SecurityContext = new AppsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.SecurityContext mu_SecurityContext = new UsersWebService.SecurityContext();

                WorkFlow.Base_UserWebService.base_userModel m_base_userModel=(WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];

                m_SecurityContext.UserName = m_base_userModel.login;
                m_SecurityContext.PassWord = m_base_userModel.password;
                m_appsBllService.SecurityContextValue = m_SecurityContext;

                mu_SecurityContext.UserName = m_base_userModel.login;
                mu_SecurityContext.PassWord = m_base_userModel.password;
                m_usersBllService.SecurityContextValue = mu_SecurityContext;

                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

                //根据应用系统ID得到对应下面的用户实体
                m_usersModel = m_usersBllService.GetModelByAppID(appID,out msg);
                int userAppID = Convert.ToInt32(m_usersModel.app_id);
                try
                {  //删除应用系统下对应的用户
                    flag = m_usersBllService.DeleteAdminByAppID(userAppID,out msg);
                    if (flag)
                    {
                        result = m_appsBllService.DeleteApp(appID, out msg);
                        if (result)
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
                        return Json("{success:false,css:'alert alert-error',message:'删除失败!'}");
                    }
                }
                catch (Exception ex)
                {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }

            }
        }

        //退出超级管理员界面
        public ActionResult QuitSys()
        {
            Session["baseuser"] = null;
            return RedirectToAction("Login", "Home");
        }

        //超级管理员重新登录
        public ActionResult LoginAgain()
        {
            Session["baseuser"] = null;
            return RedirectToAction("AdminLogin", "Home");
        }
       
        //超级管理员密码修改
        public ActionResult BU_AppsPassModify()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
            
        }
       
        //系统审批页面
        public ActionResult BU_ApprovalApps(int id)
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                
                WorkFlow.UsersWebService.usersBLLservice m_userBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();
                
                WorkFlow.Base_UserWebService.base_userModel m_base_usersModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];
                
                #region 超级管理员用户授权
                WorkFlow.AppsWebService.SecurityContext ma_SecurityContext = new AppsWebService.SecurityContext();

                string msg = string.Empty;

               
                ma_SecurityContext.UserName = m_base_usersModel.login;
                ma_SecurityContext.PassWord = m_base_usersModel.password;
                m_appsBllService.SecurityContextValue = ma_SecurityContext;

                WorkFlow.UsersWebService.SecurityContext mu_SecurityContext = new UsersWebService.SecurityContext();

                mu_SecurityContext.UserName = m_base_usersModel.login;
                mu_SecurityContext.PassWord = m_base_usersModel.password;
                m_userBllService.SecurityContextValue = mu_SecurityContext;
                #endregion

                m_appsModel = m_appsBllService.GetModel(id,out msg);//webservice方法需要超级管理员权限
                m_userModel = m_userBllService.GetAdminModelByAppID(m_appsModel.id, out msg);//webservice方法需要超级管理员权限
                
                ViewData["appInfo"] = m_appsModel;//系统信息
                ViewData["userInfo"] = m_userModel;//用户信息

                return View();
            }
        }

        //系统已审批页面
        public ActionResult BU_ApprovalInfo(int id)
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();

                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

                WorkFlow.Base_UserWebService.base_userModel m_base_usersModel=(WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];

                #region 超级管理员用户授权
                string msg = string.Empty;
                WorkFlow.AppsWebService.SecurityContext ma_SecurityContext = new AppsWebService.SecurityContext();

                ma_SecurityContext.UserName = m_base_usersModel.login;
                ma_SecurityContext.PassWord = m_base_usersModel.password;
                m_appsBllService.SecurityContextValue = ma_SecurityContext;

                WorkFlow.UsersWebService.SecurityContext mu_SecurityContext = new UsersWebService.SecurityContext();

                mu_SecurityContext.UserName = m_base_usersModel.login;
                mu_SecurityContext.PassWord = m_base_usersModel.password;
                m_usersBllService.SecurityContextValue = mu_SecurityContext;
                #endregion

                m_appsModel = m_appsBllService.GetModel(id,out msg);

                m_usersModel = m_usersBllService.GetAdminModelByAppID(m_appsModel.id,out msg);


                ViewData["appInfo"] = m_appsModel;//系统信息
                ViewData["userInfo"] = m_usersModel;//用户信息

                return View();
            }
        }
       
        //系统信息页面
        public ActionResult BU_AppsInfo(int id)
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                
                WorkFlow.UsersWebService.usersBLLservice m_userBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();

                WorkFlow.Base_UserWebService.base_userModel m_base_usersModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];

                #region 超级管理员用户授权
                WorkFlow.AppsWebService.SecurityContext ma_SecurityContext = new AppsWebService.SecurityContext();

                string msg = string.Empty;


                ma_SecurityContext.UserName = m_base_usersModel.login;
                ma_SecurityContext.PassWord = m_base_usersModel.password;
                m_appsBllService.SecurityContextValue = ma_SecurityContext;

                WorkFlow.UsersWebService.SecurityContext mu_SecurityContext = new UsersWebService.SecurityContext();

                mu_SecurityContext.UserName = m_base_usersModel.login;
                mu_SecurityContext.PassWord = m_base_usersModel.password;
                m_userBllService.SecurityContextValue = mu_SecurityContext;
                #endregion

                m_appsModel = m_appsBllService.GetModel(id, out msg);//webservice方法需要超级管理员权限
                m_userModel = m_userBllService.GetAdminModelByAppID(m_appsModel.id, out msg);//webservice方法需要超级管理员权限
                
                ViewData["appInfo"] = m_appsModel;//系统信息
                ViewData["userInfo"] = m_userModel;//用户信息

                return View();
            }
        }

     

        //修改超级管理员密码
        public ActionResult ModifyAdminPassword()
        {
          
           
                string m_oldpassword = Request.Form["oldpassword"];
                string m_newpassword = Request.Form["newpassword"];
                string m_newpassword2 = Request.Form["newpassword2"];

                #region 超级管理员用户授权
                WorkFlow.Base_UserWebService.base_userBLLservice m_baseuserBllService = new Base_UserWebService.base_userBLLservice();
                WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (Base_UserWebService.base_userModel)Session["baseuser"];

                string msg = string.Empty;

                WorkFlow.Base_UserWebService.SecurityContext m_securityContext = new Base_UserWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_baseuserModel.login;
                m_securityContext.PassWord = m_baseuserModel.password;
                m_baseuserBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
                #endregion

                if (m_newpassword.Length == 0)
                {
                 
                    return Json("{success:false,css:'alert alert-error',message:'用户的新密码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(m_newpassword) == false)
                {
                 
                    return Json("{success:false,css:'alert alert-error',message:'新密码以字母开头，且为数字和字母的组合，至少六位!'}");
                }
                if (m_oldpassword.Length == 0)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'用户的原密码不能为空!'}");
                }
                if (m_newpassword2.Length == 0)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'用户的确认密码不能为空!'}");
                }
                if (m_newpassword != m_newpassword2)
                {
                
                    return Json("{success:false,css:'alert alert-error',message:'两次密码不一致！'}");
                }

                try
                {
                    //密码验证
                    if (!m_baseuserBllService.LoginValidator(m_baseuserModel.login, m_oldpassword, out msg))
                    {
                     
                        return Json("{success:false,css:'alert alert-error',message:'原密码不正确！'}");
                    }

                    if (m_baseuserBllService.ModifyPassword(m_baseuserModel.login, m_newpassword, out msg))
                    {
                       
                        return Json("{success:true,css:'alert alert-success',message:'密码修改成功！'}");
                    }
                    else
                    {
                     
                        return Json("{success:false,css:'alert alert-error',message:'密码修改失败！'}");
                    }
                }
                catch (WebException ex)
                {
                
                    return Json("{success:false,css:'alert alert-error',message:'服务器连接失败！'}");
                }
                catch (Exception ex)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'程序异常！'}");
                }
          
  
        }

        //批准系统申请
        public ActionResult ApprovalAppsApply(FormCollection collection)
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string appid = collection["id"].ToString();

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();

                #region 超级管理员用户授权
                WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (Base_UserWebService.base_userModel)Session["baseuser"];

                string msg = string.Empty;

                WorkFlow.AppsWebService.SecurityContext m_securityContext = new AppsWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_baseuserModel.login;
                m_securityContext.PassWord = m_baseuserModel.password;
                m_appsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
                #endregion

                try
                {
                    m_appsModel = m_appsBllService.GetModel(Convert.ToInt32(appid), out msg);
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "系统出错！" });
                }

                m_appsModel.invalid = false;

                if (m_appsBllService.SuperAdminUpdateApp(m_appsModel, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "alert alert-success", message = "系统：" + m_appsModel.name + "，已经可以使用！", toUrl = "/AppsManagement/BaseUserApps" });

                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "系统：" + m_appsModel.name + "，审批失败！" });
                }
            }
         

        }

        //审批系统
        public ActionResult ApprovalApps()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int ID = Convert.ToInt32(Request.Params["appID"]);
                string Name = Request.Params["appName"];
                string msg = string.Empty;
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext m_SecurityContext = new AppsWebService.SecurityContext();
                
                WorkFlow.Base_UserWebService.base_userModel m_base_userModel=(WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];

                m_SecurityContext.UserName = m_base_userModel.login;
                m_SecurityContext.PassWord = m_base_userModel.password;
                m_appsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.AppsWebService.appsModel m_appsModel = m_appsBllService.GetModel(ID,out msg);
         
                try 
                {
                    m_appsModel.invalid = false;
                    if (m_appsBllService.SuperAdminUpdateApp(m_appsModel, out msg) == true)
                    {
                        return Json("{success:true,css:'alert alert-success',message:'应用系统名称为:'+Name+'审批成功!'}");
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'审批失败!'}");
                    }
                }
                catch (Exception ex) 
                {
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
              
            }
        }

        //获得已审批系统数据列表的json字符串格式
        public JsonResult AppsValidDSToJSON()
        {
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();

            #region 超级管理员用户授权
            WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (Base_UserWebService.base_userModel)Session["baseuser"];

            string msg = string.Empty;

            WorkFlow.AppsWebService.SecurityContext m_securityContext = new AppsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_baseuserModel.login;
            m_securityContext.PassWord = m_baseuserModel.password;
            m_appsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
            #endregion

            string data = "{Rows:[";
            try
            {
                DataSet ds = m_appsBllService.GetValidAppsList(out msg);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string id = ds.Tables[0].Rows[i][0].ToString();
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        string code = ds.Tables[0].Rows[i][2].ToString();
                        string url = ds.Tables[0].Rows[i][3].ToString();
                        string remark = ds.Tables[0].Rows[i][4].ToString();
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "url:'" + url + "',";
                            data += "remark:'" + remark + "'}";
                        }
                        else
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "url:'" + url + "',";
                            data += "remark:'" + remark + "'},";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}";
            return Json(data);
        }

        //获得未审批系统数据列表的json字符串格式
        public JsonResult AppsInvalidDSToJSON()
        {
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();

            #region 超级管理员用户授权
            WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (Base_UserWebService.base_userModel)Session["baseuser"];

            string msg = string.Empty;

            WorkFlow.AppsWebService.SecurityContext m_securityContext = new AppsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_baseuserModel.login;
            m_securityContext.PassWord = m_baseuserModel.password;
            m_appsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
            #endregion

            string data = "{Rows:[";
            try
            {
                DataSet ds = m_appsBllService.GetInvalidAppsList(out msg);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string id = ds.Tables[0].Rows[i][0].ToString();
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        string code = ds.Tables[0].Rows[i][2].ToString();
                        string url = ds.Tables[0].Rows[i][3].ToString();
                        string remark = ds.Tables[0].Rows[i][4].ToString();
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "url:'" + url + "',";
                            data += "remark:'" + remark + "'}";
                        }
                        else
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "url:'" + url + "',";
                            data += "remark:'" + remark + "'},";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}";
            return Json(data);
        }
        
        //获得已经审批系统数据列表(后台分页)
        public ActionResult AppsValidList()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Home", "AdminLogin");
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
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext m_SecurityContext = new AppsWebService.SecurityContext();

                WorkFlow.Base_UserWebService.base_userModel m_base_userModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];

                m_SecurityContext.UserName = m_base_userModel.login;
                m_SecurityContext.PassWord = m_base_userModel.password;
                m_appsBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_appsBllService.GetValidAppsList(out msg);

                IList<WorkFlow.AppsWebService.appsModel> m_list = new List<WorkFlow.AppsWebService.appsModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)Activator.CreateInstance(typeof(WorkFlow.AppsWebService.appsModel));
                    PropertyInfo[] m_propertys = m_appsModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            { //数据库Null值单独处理
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(m_appsModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(m_appsModel, null, null);
                                break;
                            }
                        }
                    }
                    m_list.Add(m_appsModel);
                }
                IList<WorkFlow.AppsWebService.appsModel> m_targetList = new List<WorkFlow.AppsWebService.appsModel>();
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

        //获得待审批系统数据列表(后台分页)
        public ActionResult AppsInvalidList()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Home", "AdminLogin");
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
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext m_SecurityContext = new AppsWebService.SecurityContext();

                WorkFlow.Base_UserWebService.base_userModel m_base_userModel=(WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];
                
                m_SecurityContext.UserName = m_base_userModel.login;
                m_SecurityContext.PassWord = m_base_userModel.password;
                m_appsBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_appsBllService.GetInvalidAppsList(out msg);

                IList<WorkFlow.AppsWebService.appsModel> m_list=new List<WorkFlow.AppsWebService.appsModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)Activator.CreateInstance(typeof(WorkFlow.AppsWebService.appsModel));
                    PropertyInfo[] m_propertys = m_appsModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            { //数据库Null值单独处理
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(m_appsModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(m_appsModel,null,null);
                                break;
                            }
                        }
                    }
                    m_list.Add(m_appsModel);          
                }
                IList<WorkFlow.AppsWebService.appsModel> m_targetList=new List<WorkFlow.AppsWebService.appsModel>();
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

        ///<summary>
        ///统计已审批、待审批的系统数量
        ///</summary>
        public ActionResult InvalidAppsCount()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                int invalidCount = 0;//待审批系统数量
                int validCount = 0;//已审批系统数量
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = (Base_UserWebService.base_userModel)Session["baseuser"];

                #region 超级管理员用户授权
                string msg = string.Empty;

                WorkFlow.AppsWebService.SecurityContext m_securityContext = new AppsWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_baseuserModel.login;
                m_securityContext.PassWord = m_baseuserModel.password;
                m_appsBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
                #endregion

                try
                {
                    invalidCount = m_appsBllService.GetInValidAppCount(out msg);
                    validCount = m_appsBllService.GetValidAppCount(out msg);
                }
                catch (Exception ex)
                {

                }

                return Json("{invalidCount:'" + invalidCount + "',validCount:'" + validCount + "'}");
            }
         
        }

        public ActionResult AppName()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                int ID = Convert.ToInt32(Request.Params["appID"]);
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext m_SecurityContext = new AppsWebService.SecurityContext();

                WorkFlow.Base_UserWebService.base_userModel m_base_userModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];
                string msg = string.Empty;
                m_SecurityContext.UserName = m_base_userModel.login;
                m_SecurityContext.PassWord = m_base_userModel.password;
                m_appsBllService.SecurityContextValue = m_SecurityContext;

                string appName = m_appsBllService.GetAppNameByAdminID(ID,out msg);
                return Json("{appName:'" +appName+ "'}");
            }
        }
    }
}
