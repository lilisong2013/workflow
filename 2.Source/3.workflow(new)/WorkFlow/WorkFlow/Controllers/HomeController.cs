using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Web.Services.Protocols;

namespace WorkFlow.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

      
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AdminLogin()
        {
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

            try
            {
                if (m_usersBllService.SysAdminLoginValidator(m_loginName, m_loginPassword))
                {
                    m_usersModel = m_usersBllService.GetModelByLogin(m_loginName);
                    Session["user"] = m_usersModel;
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "", message = "", toUrl = "/Home/Index" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户名或密码不正确！" });
                }
            }
            catch (WebException  ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "数据库连接失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "数据访问出错：" + ex.ToString() });
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

            try
            {
                if (m_baseuserBllService.LoginValidator(m_loginName, m_loginPassword))
                {
                    m_baseuserModel = m_baseuserBllService.GetModelByLogin(m_loginName);
                    Session["baseuser"] = m_baseuserModel;
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "", message = "", toUrl = "/AppsManagement/BaseUserApps" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户名或密码不正确！" });
                }
            }
            catch (WebException ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "数据库连接失败！" });
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "数据访问出错：" + ex.ToString() });
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
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统添加失败1" + ex.ToString() });
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
            m_userModel.invalid = true;
            m_userModel.deleted = false;
            m_userModel.created_at = DateTime.Now;
            m_userModel.created_by = -1;
            m_userModel.created_ip = Request.Form["createdIP"].Trim();
            
            
            //判断系统name、code是否为空
            if (m_appsModel.name == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统名称不能为空！" });
            }
            if (m_appsModel.code == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="系统编码不能为空!"});
            }
            //判断用户login、name、password是否为空
            if (m_userModel.login == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户登录名称不能为空！" });
            }      
            if (m_userModel.password == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="密码不能为空!"});
            }
            if (Request.Form["userPassword2"].Trim() == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="确认密码不能为空!"});
            }
            if (m_userModel.password != Request.Form["userPassword2"].Trim())
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "两次输入的密码不一致！" });
            }
            if (m_userModel.name == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "真实姓名不能为空!" });
            }
            if (m_userModel.mobile_phone == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="手机号码不能为空!" });
            }
            if (Saron.Common.PubFun.ConditionFilter.IsMobilePhone(m_userModel.mobile_phone) == false)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="手机号码输入不正确!"});
            }
            if (m_userModel.mail == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="邮箱不能为空!"});
            }
            if (Saron.Common.PubFun.ConditionFilter.IsEmail(m_userModel.mail) == false)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="邮箱格式输入不正确!"});
            }
            //系统是否存在
            if (m_appsBllservice.ExistsName(m_appsModel.name))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统名称已经存在！" });
            }

            int appsID = 0;//系统的ID
            try
            {
                appsID = m_appsBllservice.Add(m_appsModel);//系统添加成功后的ID
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "系统添加失败2"+ex.ToString() });
            }

            if (appsID == -1)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "系统名称已经存在！" });
            }

            if (appsID != 0)
            {
                m_userModel.app_id = appsID;//所属系统ID

                try
                {
                    int flag = m_usersBllservice.Add(m_userModel);
                    if (flag == 0)
                    {
                        m_appsBllservice.Delete(appsID);//删除申请的系统记录
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "用户添加失败！" });
                    }
                    else if (flag == -1) 
                    {
                        m_appsBllservice.Delete(appsID);//删除申请的系统记录
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "用户登录名称已经存在" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "注册成功！系统名称为：" + m_appsModel.name + "，管理员名称：" + m_userModel.name });
                    }
                }
                catch (Exception ex)
                {
                    m_appsBllservice.Delete(appsID);//删除申请的系统记录
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "系统添加失败4" + ex.ToString() });
                }
            }
            else
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "系统添加失败5" });
            }
        }
    }
}
