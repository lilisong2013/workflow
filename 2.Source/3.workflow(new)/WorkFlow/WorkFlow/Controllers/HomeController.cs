﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Web.Services.Protocols;
using System.Data;
using System.Collections;

namespace WorkFlow.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        //检查Session

        public ActionResult CheckSession()
        {
            if (Session["user"] == null)
            {
                return Json("{sessionNull:true,toUrl:'/Home/Login'}");
            }
            else
            {
                return Json("{sessionNull:false}");
            }
        }

        public ActionResult CheckSession2()
        {
            if (Session["baseuser"] == null)
            {
                return Json("{sessionNull:true,toUrl:'/Home/Login'}");
            }
            else
            {
                return Json("{sessionNull:false}");
            }
        }

        public ActionResult Login()
        {
            Session["user"] = null;
            Session["baseuser"] = null;
            return View();
        }

        /// <summary>
        /// 修改系统管理员密码页面
        /// </summary>
        public ActionResult AdminPass()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 修改系统管理员信息页面
        /// </summary>
        public ActionResult AdminInfo()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AdminLogin()
        {
            Session["baseuser"] = null;    
            return View();
     
        }

        public ActionResult RegistPage()
        {
            return View();
        }
        
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        
    
        
        public ActionResult RegistPageCon()
        {
            int m_userID = Convert.ToInt32(Request.Params["adminID"]);

            WorkFlow.UsersWebService.usersBLLservice m_userBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.v_app_adminModel m_v_app_adminModel = new UsersWebService.v_app_adminModel();

            WorkFlow.UsersWebService.SecurityContext m_securityContext = new UsersWebService.SecurityContext();

            #region webservice授权验证
            string msg = string.Empty;
            m_securityContext.UserName = "saron";
            m_securityContext.PassWord = "123";
            m_userBllService.SecurityContextValue = m_securityContext;
            #endregion

            m_v_app_adminModel = m_userBllService.GetV_AppAdminModelByAdminID(m_userID, out msg);

            ViewData["appName"] = m_v_app_adminModel.App_Name;
            ViewData["appCode"] = m_v_app_adminModel.App_Code;
            ViewData["appUrl"] = m_v_app_adminModel.URL;
            ViewData["appMark"] = m_v_app_adminModel.App_Mark;

            ViewData["adminLogin"] = m_v_app_adminModel.User_Login;
            ViewData["adminName"] = m_v_app_adminModel.User_Name;
            ViewData["adminEmployee_no"] = m_v_app_adminModel.employee_no;
            ViewData["adminPhone"] = m_v_app_adminModel.mobile_phone;
            ViewData["adminMail"] = m_v_app_adminModel.mail;
            ViewData["adminMark"] = m_v_app_adminModel.User_Remark;
            return View();
        }
        
        /// <summary>
        /// 系统管理员登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginValidation()
        {
            string m_loginName = Request.Form["loginName"];
            string m_loginPassword = Request.Form["loginPassword"];

            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

            string msg = string.Empty;

            WorkFlow.UsersWebService.SecurityContext m_securityContext = new UsersWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_loginName;
            m_securityContext.PassWord = m_loginPassword;
            m_usersBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            try
            {
                if (m_usersBllService.SysAdminLoginValidator(m_loginName, m_loginPassword, out msg))
                {
                    m_usersModel = m_usersBllService.GetUserModelByLoginCK(m_loginName, out msg);
                    Session["user"] = m_usersModel;
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "", message = "", toUrl = "/Home/Index" });
                }
                else
                {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "用户名或密码不正确！" });
         
                }
            }
            catch (WebException ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "数据库连接失败！" });
              
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "数据访问出错：" + ex.ToString() });
              
            }
           
     
        }


        /// <summary>
        /// 超级管理员登录验证
        /// </summary>
        /// <param name="collection">表单数组</param>
        /// <returns>通过，进入超级管理员页面</returns>
        public ActionResult AdminLoginValidation()
        {
           
                string m_loginName = Request.Form["loginName"];
                string m_loginPassword = Request.Form["loginPassword"];

                WorkFlow.Base_UserWebService.base_userBLLservice m_baseuserBllService = new Base_UserWebService.base_userBLLservice();
                WorkFlow.Base_UserWebService.base_userModel m_baseuserModel = new Base_UserWebService.base_userModel();

                string msg = string.Empty;

                WorkFlow.Base_UserWebService.SecurityContext m_securityContext = new Base_UserWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_loginName;
                m_securityContext.PassWord = m_loginPassword;
                m_baseuserBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                try
                {
                    if (m_baseuserBllService.LoginValidator(m_loginName, m_loginPassword, out msg))
                    {
                        m_baseuserModel = m_baseuserBllService.GetModelByLoginCK(m_loginName, out msg);
                        Session["baseuser"] = m_baseuserModel;
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "", message = "", toUrl = "/AppsManagement/BaseUserApps" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "用户名或密码不正确！" });
                    }
                }
                catch (WebException ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "数据库连接失败！" });
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "alert alert-error", message = "数据访问出错：" + ex.ToString() });
                }
            
           
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="collection">表单数据</param>
        /// <returns>成功，返回登录页面</returns>
        public ActionResult RegistUser()
        {
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
            WorkFlow.AppsWebService.appsBLLservice m_appsBllservice = new AppsWebService.appsBLLservice();

            
            WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();
            WorkFlow.UsersWebService.usersBLLservice m_usersBllservice = new UsersWebService.usersBLLservice();

            string msg = string.Empty;

            WorkFlow.AppsWebService.SecurityContext m_a_securityContext = new AppsWebService.SecurityContext();
            m_a_securityContext.UserName = "saron";
            m_a_securityContext.PassWord = "123";
            m_appsBllservice.SecurityContextValue = m_a_securityContext;

            WorkFlow.UsersWebService.SecurityContext m_u_securityContext = new UsersWebService.SecurityContext();
            m_u_securityContext.UserName = "saron";
            m_u_securityContext.PassWord = "123";
            m_usersBllservice.SecurityContextValue = m_u_securityContext;

            //对象m_appsModel赋值
            m_appsModel.name = Request.Form["appsName"].Trim();
            m_appsModel.code = Request.Form["appsCode"].Trim();
            m_appsModel.url = Request.Form["appsUrl"].Trim();
            m_appsModel.remark = Request.Form["appsRemark"].Trim();
            m_appsModel.invalid = true;
            m_appsModel.created_at = DateTime.Now;
            m_appsModel.created_ip = Request.Form["createdIP"].Trim();
            string datetime = Request.Form["apply_at"].Trim();
            
            try
            {
                m_appsModel.apply_at = Convert.ToDateTime(datetime);
            }
            catch (Exception ex)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统添加失败1'}");
            }

            //对象m_userModel赋值
            m_userModel.login = Request.Form["userLogin"].Trim();
            m_userModel.password = Request.Form["userPassword"].Trim();
            m_userModel.name = Request.Form["userName"].Trim();
            m_userModel.employee_no = Request.Form["userEmployeeNo"].Trim();
            m_userModel.mobile_phone = Request.Form["userMobilePhone"].Trim();
            m_userModel.mail = Request.Form["userMail"].Trim();
            m_userModel.remark = Request.Form["userRemark"].Trim();
            m_userModel.admin = true;
            m_userModel.invalid = false;
            m_userModel.deleted = false;
            m_userModel.created_at = DateTime.Now;
            m_userModel.created_by = -1;
            m_userModel.created_ip = Request.Form["createdIP"].Trim();


            //判断系统name、code是否为空
            if (m_appsModel.name == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'系统名称不能为空！'}");
            }
            if (Convert.ToInt32(m_appsModel.name.Length) > 30)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统名称不能超过30个字符!'}");
            }
            if (m_appsModel.code == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'系统编码不能为空!'}");
            }
            if (Saron.Common.PubFun.ConditionFilter.IsCode(m_appsModel.code) == false)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统编码以字母开头，最多为40个字符!'}");
            }
            if (Convert.ToInt32(m_appsModel.code.Length) > 40)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统编码不能超过40个字符!'}");
            }
            if (Convert.ToInt32(m_appsModel.url.Length) != 0)
            {
                if (Convert.ToInt32(m_appsModel.url.Length) > 30)
                {
                    return Json("{success:false,css:'alert alert-error',message:'访问链接不能超过30个字符!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidURL(m_appsModel.url) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'不是有效的访问链接!'}");
                }
            }
          

            if (Convert.ToInt32(m_appsModel.remark.Length) > 150)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统备注不能超过150个字符!'}");
            }

            //判断用户login、name、password是否为空
            if (m_userModel.login == "")
            {
                   return Json("{success:false,css:'alert alert-error',message:'登录名称不能为空！'}");
            }
            if (Convert.ToInt32(m_userModel.login.Length)>30)
            {
                return Json("{success:false,css:'alert alert-error',message:'登录名称长度不能超过30个字符!'}");
            }
            if (m_userModel.name == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'真实姓名不能为空!'}");
            }
            if (Convert.ToInt32(m_userModel.name.Length) > 30)
            {
                return Json("{success:false,css:'alert alert-error',message:'真实姓名长度不能超过30个字符!'}");
            }
            if (m_userModel.password == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'登录密码不能为空!'}");
            }
            if (Saron.Common.PubFun.ConditionFilter.IsPassWord(m_userModel.password) == false)
            {
                return Json("{success:false,css:'alert alert-error',message:'登录密码以字母开头，字母和数字组合，至少6位!'}");
            }
            if (Convert.ToInt32(m_userModel.password.Length) > 15)
            {
                return Json("{success:false,css:'alert alert-error',message:'登录密码长度不能超过15位!'}");
            }
            if (Request.Form["userPassword2"].Trim() == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'确认密码不能为空!'}");
            }
            if (Saron.Common.PubFun.ConditionFilter.IsPassWord(Request.Form["userPassword2"]) == false)
            {
                return Json("{success:false,css:'alert alert-error',message:'确认密码应字母开头，字母和数字组合，至少6位!'}");
            }
            if (m_userModel.password != Request.Form["userPassword2"].Trim())
            {
                return Json("{success:false,css:'alert alert-error',message:'两次输入的密码不一致！'}");
            }
         
            if (m_userModel.employee_no == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'员工编号不能为空!'}");
            }
            if (Convert.ToInt32(m_userModel.employee_no.Length) > 40)
            {
                return Json("{success:false,css:'alert alert-error',message:'员工编号长度不能超过40个字符!}");
            }
            if (m_userModel.mobile_phone == "")
            {

                return Json("{success:false,css:'alert alert-error',message:'手机号码不能为空!'}");
            }
            if (Saron.Common.PubFun.ConditionFilter.IsMobilePhone(m_userModel.mobile_phone) == false)
            {
                return Json("{success:false,css:'alert alert-error',message:'手机号码输入不正确!'}");
            }
            if (m_userModel.mail == "")
            {
                return Json("{success:false,css:'alert alert-error',message:'邮箱不能为空!'}");
            }
            if (Saron.Common.PubFun.ConditionFilter.IsEmail(m_userModel.mail) == false)
            {
                return Json("{success:false,css:'alert alert-error',message:'邮箱格式输入不正确!'}");
            }
            if (Convert.ToInt32(m_userModel.remark.Length) > 150)
            {
                return Json("{success:false,css:'alert alert-error',message:'备注信息不能超过150个字符!'}");
            }
            //系统是否存在
            if (m_appsBllservice.ExistsAppName(m_appsModel.name,out msg))
            {
                return Json("{success:false,css:'alert alert-error',message:'系统名称已经存在！'}");
            }

            int appsID = 0;//系统的ID
            try
            {
                appsID = m_appsBllservice.Add(m_appsModel,out msg);//系统添加成功后的ID
            }
            catch (Exception ex)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统添加失败！'}");
            }

            if (appsID == -1)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统名称已经存在！'}");
            }

            if (appsID != 0)
            {
                m_userModel.app_id = appsID;//所属系统ID

                try
                {
                    int flag = m_usersBllservice.AddSysAdmin(m_userModel,out msg);
                    if (flag == 0)
                    {
                        m_appsBllservice.Delete(appsID,out msg);//删除申请的系统记录
                        return Json("{success:false,css:'alert alert-error',message:'用户添加失败！'}");
                    }
                    else if (flag == -1)
                    {
                        m_appsBllservice.Delete(appsID,out msg);//删除申请的系统记录
                        return Json("{success:false,css:'alert alert-error',message:'用户登录名称已经存在!'}");
                    }
                    else
                    {
                        return Json("{success:true,css:'alert alert-success',message:'申请已成功提交!',toUrl:'/Home/RegistPageCon?adminID=" + flag + "'}");
                    }
                }
                catch (Exception ex)
                {
                    m_appsBllservice.Delete(appsID,out msg);//删除申请的系统记录
                    return Json("{success:false,css:'alert alert-error',message:'系统添加失败4!'}");
                }
            }
            else
            {
                return Json("{success:false,css:'alert alert-error',message:'系统添加失败5!'}");
            }
        }
  
        //应用管理员修改密码     
        public ActionResult ModifyAdminPass(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return Json("{sessionInfo:true,toUrl:'/Home/Login'}");
            }

            string m_oldpassword = Request.Form["oldpassword"];
            string m_newpassword = Request.Form["newpassword"];
            string m_newpassword2 = Request.Form["newpassword2"];
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;
            WorkFlow.UsersWebService.SecurityContext m_u_securityContext = new UsersWebService.SecurityContext();
           
            m_u_securityContext.UserName = m_usersModel.login;
            m_u_securityContext.PassWord = m_usersModel.password;
            m_u_securityContext.AppID = (int)m_usersModel.app_id;
            m_usersBllService.SecurityContextValue = m_u_securityContext;

            if (m_oldpassword.Length == 0)
            {
     
                return Json("{success:false,css:'alert alert-error',message:'原密码不能为空!'}");
            }
            if (m_usersBllService.SysAdminLoginValidator(m_usersModel.login, m_oldpassword, out msg) == false)
            {
          
                return Json("{success:false,css:'alert alert-error',message:'原始密码不正确!'}");
            }
            if (m_newpassword.Length == 0)
            {
        
                return Json("{success:false,css:'alert alert-error',message:'新密码不能为空!'}");
            }
            if (Saron.Common.PubFun.ConditionFilter.IsPassWord(m_newpassword) == false)
            {
          
                return Json("{success:false,css:'alert alert-error',message:'新密码以字母开头，字母和数字的组合，至少为6位!'}");
            }
            if (m_newpassword2.Length == 0)
            {
          
                return Json("{success:false,css:'alert alert-error',message:'确认密码不能为空!'}");
            }
            if (m_newpassword != m_newpassword2)
            {
           
                return Json("{success:false,css:'alert alert-error',message:'两次密码不一致!'}");
            }
            try
            {
               
                if (m_usersBllService.ModifyPassword(m_usersModel.login, m_newpassword,out msg) == true)
                {
                    WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();
                    m_u_securityContext.PassWord = m_newpassword;

                    m_userModel = m_usersBllService.GetUserModelByLoginCK(m_usersModel.login, out msg);
                    Session["user"] = m_userModel;
                    return Json("{success:true,css:'alert alert-success',message:'密码修改成功!'}");
                }
                else
                {
  
                    return Json("{success:false,css:'alert alert-error',message:'密码修改失败!'}");
                }
            }
            catch (WebException ex)
            {
   
                return Json("{success:false,css:'alert alert-error',message:'服务器连接失败!'}");
            }
            catch (Exception ex)
            {

                return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
            }
        }

        /// <summary>
        /// 获取页面的标题
        /// </summary>
        public ActionResult GetPageTitle()
        {
            string pageName = "权限管理系统 | ";
            pageName += Request.Params["pageName"];
            return Json("{pageName:'" + pageName + "'}");
        }

        /// <summary>
        /// 获取系统标题
        /// </summary>
        public ActionResult GetSysTitle()
        {
            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();

            #region 系统管理员授权
            string msg = string.Empty;
            WorkFlow.AppsWebService.SecurityContext m_securityContext = new AppsWebService.SecurityContext();
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_appsBllService.SecurityContextValue = m_securityContext;
            #endregion

            try
            {
                m_appsModel = m_appsBllService.AdminGetModel((int)m_usersModel.app_id, out msg);
            }
            catch (Exception ex)
            {

            }

            string appName = m_appsModel.name;
            return Json("{appName:'" + appName + "'}");
        }

        /// <summary>
        /// 获得系统信息
        /// </summary>
        public ActionResult GetAppInfo()
        {
            string dataStr = "{";
            WorkFlow.AppsWebService.appsBLLservice m_appBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appModel = new AppsWebService.appsModel();

            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            WorkFlow.AppsWebService.SecurityContext securityContext = new AppsWebService.SecurityContext();

            string msg = "";
            #region Webservice访问授权
            securityContext.UserName = m_userModel.login;
            securityContext.PassWord = m_userModel.password;
            securityContext.AppID = (int)m_userModel.app_id;
            m_appBllService.SecurityContextValue = securityContext;
            #endregion

            try
            {
                m_appModel = m_appBllService.AdminGetModel((int)m_userModel.app_id, out msg);
            }
            catch (Exception ex)
            {

            }

            dataStr += "appName:'" + m_appModel.name + "',";
            dataStr += "appCode:'" + m_appModel.code + "',";
            dataStr += "appUrl:'" + m_appModel.url + "',";
            dataStr += "appRemark:'" + m_appModel.remark + "'}";

            return Json(dataStr);
        }

        /// <summary>
        /// 获得管理员信息
        /// </summary>
        public ActionResult GetAdminInfo()
        {
            string dataStr = "{";
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            dataStr += "adminLogin:'" + m_userModel.login + "',";
            dataStr += "adminName:'" + m_userModel.name + "',";
            dataStr += "adminEmployeNum:'" + m_userModel.employee_no + "',";
            dataStr += "adminPhone:'" + m_userModel.mobile_phone + "',";
            dataStr += "adminEmail:'" + m_userModel.mail + "',";
            dataStr += "adminRemark:'" + m_userModel.remark + "'}";
            return Json(dataStr);
        }

        /// <summary>
        /// 修改系统信息
        /// </summary>
        public ActionResult ModifyAppInfo()
        {
            if (Session["user"] == null)
            {
                return Json("{sessionInfo:true,toUrl:'/Home/Login'}");
            }

            WorkFlow.AppsWebService.appsBLLservice m_appBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appModel = new AppsWebService.appsModel();

            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            WorkFlow.AppsWebService.SecurityContext securityContext = new AppsWebService.SecurityContext();

            string msg = "";
            #region Webservice访问授权
            securityContext.UserName = m_userModel.login;
            securityContext.PassWord = m_userModel.password;
            securityContext.AppID = (int)m_userModel.app_id;
            m_appBllService.SecurityContextValue = securityContext;
            #endregion

            try
            {
                m_appModel = m_appBllService.AdminGetModel((int)m_userModel.app_id, out msg);
            }
            catch (Exception ex)
            {

            }
            if (Convert.ToInt32(Request.Params["appCode"].ToString().Length) == 0)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统编码不能为空!'}"); 
            }
            if (Saron.Common.PubFun.ConditionFilter.IsCode(Request.Params["appCode"]) == false)
            {
                return Json("{success:false,css:'alert alert-error',message:'系统编码以字母开头，最多为40个字符!'}");
            }
            m_appModel.name = Request.Params["appName"];
            m_appModel.code = Request.Params["appCode"];
            m_appModel.url = Request.Params["appUrl"];
            m_appModel.remark = Request.Params["appRemark"];
            m_appModel.updated_at = DateTime.Now;//修改时间
            m_appModel.updated_by = m_userModel.id;//修改用户ID
            m_appModel.updated_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();//修改IP
           
           

            try
            {
                //判断系统名称是否已经存在
                if (m_appBllService.ExistsAppNameOutAppModel(m_appModel, out msg))
                {
                    //名称存在返回提示信息
                    return Json("{success:false,css:'alert alert-error',message:'系统名称已经存在！'}");
                }
                else
                {
                    if (m_appBllService.AdminUpdateApp(m_appModel, out msg))
                    {
                        return Json("{success:true,css:'alert alert-success',message:'修改系统信息成功！'}");
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'系统信息修改失败！'}");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("{success:false,css:'alert alert-error',message:'程序异常！'}");
            }
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        public ActionResult ModifyAdminInfo()
        {
            if (Session["user"] == null)
            {
                return Json("{sessionInfo:true,toUrl:'/Home/Login'}");
            }

            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            WorkFlow.UsersWebService.SecurityContext securityContext = new UsersWebService.SecurityContext();

            string msg = "";
            #region Webservice访问授权
            securityContext.UserName = m_userModel.login;
            securityContext.PassWord = m_userModel.password;
            securityContext.AppID = (int)m_userModel.app_id;
            m_usersBllService.SecurityContextValue = securityContext;
            #endregion

            if (Convert.ToInt32(Request.Params["adminEmployeNum"].ToString().Length) == 0)
            {
                return Json("{success:false,css:'alert alert-error',message:'员工编码不能为空!'}");
            }
            if (Convert.ToInt32(Request.Params["adminEmployeNum"].ToString().Length) > 40)
            {
                return Json("{success:false,css:'alert alert-error',message:'员工编码最多为40个字符!'}");
            }
            m_userModel.login = Request.Params["adminLogin"];
            m_userModel.name = Request.Params["adminName"];
            m_userModel.employee_no = Request.Params["adminEmployeNum"];
            m_userModel.mobile_phone = Request.Params["adminPhone"];
            m_userModel.mail = Request.Params["adminEmail"];
            m_userModel.remark = Request.Params["adminRemark"];
            m_userModel.updated_at = DateTime.Now;//修改时间
            m_userModel.updated_by = m_userModel.id;//修改用户ID
            m_userModel.updated_ip = Saron.Common.PubFun.IPHelper.GetIpAddress();//修改IP

            try
            {
                //判断系统名称是否已经存在
                if (m_usersBllService.ExistsAdminLogin(m_userModel, out msg))
                {
                    //名称存在返回提示信息
                    return Json("{success:false,css:'alert alert-error',message:'系统管理员登录名已经存在！'}");
                }
                else
                {
                    if (m_usersBllService.AdminInfoUpdate(m_userModel, out msg))
                    {
                        Session["user"] = m_userModel;
                        return Json("{success:true,css:'alert alert-success',message:'系统管理员信息修改成功！'}");
                    }
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'系统管理员信息修改失败！'}");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("{success:false,css:'alert alert-error',message:'程序异常！'}");
            }
        }
    }
}