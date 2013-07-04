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
        
        //删除ID为id的应用系统管理员
        public ActionResult DeleteApp()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else { 
            int appID = Convert.ToInt32(Request.Params["appID"]);
            #region 注释
            WorkFlow.Base_UserWebService.base_userModel m_base_usersModel = (WorkFlow.Base_UserWebService.base_userModel)Session["baseuser"];
            
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
            
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

            #region 超级管理员授权
            string msg = string.Empty;
            
            WorkFlow.AppsWebService.SecurityContext m_securityContext = new AppsWebService.SecurityContext();
            m_securityContext.UserName = m_base_usersModel.login;
            m_securityContext.PassWord = m_base_usersModel.password;
            m_appsBllService.SecurityContextValue = m_securityContext;

            WorkFlow.UsersWebService.SecurityContext mu_securityContext = new UsersWebService.SecurityContext();
            mu_securityContext.UserName = m_base_usersModel.login;
            mu_securityContext.PassWord = m_base_usersModel.password;
            m_usersBllService.SecurityContextValue = mu_securityContext;
            #endregion

            try
            {
                m_appsModel = m_appsBllService.GetModel(appID, out msg);
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "发生异常！", toUrl = "" }, JsonRequestBehavior.AllowGet);
            }

            if (m_appsModel == null)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false });
            }

            try
            {
                m_usersModel = m_usersBllService.GetAdminModelByAppID(m_appsModel.id, out msg);
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "发生异常！", toUrl = "" }, JsonRequestBehavior.AllowGet);
            }

            if (m_usersModel == null)
            {
                try
                {
                    if (msg == "")
                    {
                        if (m_appsBllService.DeleteApp(m_appsModel.id, out msg))
                        {
                            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "删除成功！", toUrl = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "删除失败！", toUrl = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "发生异常！", toUrl = "" }, JsonRequestBehavior.AllowGet);
                }
            }

            bool flag = false;

            try
            {
                flag = m_usersBllService.DeleteAdminByAppID(m_appsModel.id, out msg);
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "发生异常！", toUrl = "" }, JsonRequestBehavior.AllowGet);
            }

            if (!flag)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "删除失败！", toUrl = "" }, JsonRequestBehavior.AllowGet);
            }

            flag = false;

            try
            {
                flag = m_appsBllService.DeleteApp(m_appsModel.id, out msg);

                if (flag)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "删除成功！", toUrl = "" }, JsonRequestBehavior.AllowGet);
                }
                else 
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "删除失败！", toUrl = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "发生异常！", toUrl = "" }, JsonRequestBehavior.AllowGet);
            }

            #endregion
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
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户的新密码不能为空!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(m_newpassword) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "新密码以字母开头，且为数字和字母的组合，至少六位" });
                }
                if (m_oldpassword.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户的原密码不能为空!" });
                }
                if (m_newpassword2.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户的确认密码不能为空!" });
                }
                if (m_newpassword != m_newpassword2)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "两次密码不一致！" });
                }

                try
                {
                    //密码验证
                    if (!m_baseuserBllService.LoginValidator(m_baseuserModel.login, m_oldpassword, out msg))
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "原密码不正确！" });
                    }

                    if (m_baseuserBllService.ModifyPassword(m_baseuserModel.login, m_newpassword, out msg))
                    {
                        m_baseuserModel = m_baseuserBllService.GetModelByLogin(m_baseuserModel.login, out msg);
                        Session["baseuser"] = m_baseuserModel;
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "密码修改成功！", toUrl = "/AppsManagement/BU_AppsPassModifyCon" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "密码修改失败！" });
                    }
                }
                catch (WebException ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "服务器连接失败！" });
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常！" });
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
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统出错！" });
                }

                m_appsModel.invalid = false;

                if (m_appsBllService.SuperAdminUpdateApp(m_appsModel, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "系统：" + m_appsModel.name + "，已经可以使用！", toUrl = "/AppsManagement/BaseUserApps" });

                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统：" + m_appsModel.name + "，审批失败！" });
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
    }
}
