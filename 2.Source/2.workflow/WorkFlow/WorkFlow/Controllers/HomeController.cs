using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlow.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

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

        public ActionResult RegistPage()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult OrganizationStruct()
        {
            if (Session["user"]==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 系统用户登录验证
        /// </summary>
        /// <param name="collection">表单数组</param>
        /// <returns>通过，进入系统首页</returns>
        public ActionResult LoginValidation()
        {
            string m_loginName = Request.Form["loginName"];
            string m_loginPassword = Request.Form["loginPassword"];

            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

            try
            {
                if (m_usersBllService.LoginValidator(m_loginName, m_loginPassword))
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
            catch(Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "数据访问出错："+ex.ToString() });
            }

        }

        /// <summary>
        /// 超级管理员登录验证
        /// </summary>
        /// <param name="collection">表单数组</param>
        /// <returns>通过，进入超级管理员页面</returns>
        public ActionResult AdminLoginValidation(FormCollection collection)
        {
            if (collection["loginName"].Trim() == "")
            {
                return Content("<script>alert('登录名不能为空！');window.history.back();</script>");
            }



            Session["loginName"] = collection["loginName"].Trim();
            int m_count = collection.Count;
            return RedirectToAction("App_Apply","BaseUserManagement");
        }
        
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="collection">表单数据</param>
        /// <returns>成功，返回登录页面</returns>
        public ActionResult RegistUser(FormCollection collection)
        {
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
            WorkFlow.AppsWebService.appsBLLservice m_appsBllservice = new AppsWebService.appsBLLservice();

            WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();
            WorkFlow.UsersWebService.usersBLLservice m_usersBllservice = new UsersWebService.usersBLLservice();
            if (collection["appsName"].Trim() == "")
            {
                return Content("<script>alert('系统名称不能为空！');window.history.back();</script>");
            }
            if (collection["appsCode"].Trim() == "")
            {
                return Content("<script>alert('系统编码不能为空！');window.history.back();</script>");
            }
            if (collection["userLogin"].Trim() == "")
            {
                return Content("<script>alert('系统管理员登录名不能为空！');window.history.back();</script>");
            }
            if (collection["userPassword"].Trim() == "")
            {
                return Content("<script>alert('系统管理员登录密码不能为空！');window.history.back();</script>");
            }
            if (collection["userName"].Trim() == "")
            {
                return Content("<script>alert('系统管理员真实姓名不能为空！');window.history.back();</script>");
            }

            m_appsModel.name = collection["appsName"].Trim();
            m_appsModel.code = collection["appsCode"].Trim();
            m_appsModel.url = collection["appsUrl"].Trim();
            m_appsModel.remark = collection["appsRemark"].Trim();
            m_appsModel.invalid = true;
            m_appsModel.created_at = DateTime.Now;
            m_appsModel.created_ip = collection["createdIP"].Trim();
            string datetime = collection["apply_at"].Trim();
            try
            {
                m_appsModel.apply_at = Convert.ToDateTime(collection["apply_at"].Trim());
            }
            catch (Exception ex)
            {
            }

            int appsID = 0;
            try
            {
                appsID = m_appsBllservice.Add(m_appsModel);
            }
            catch(Exception ex)
            {
            }

            if (appsID != 0)
            {
                m_userModel.login = collection["userLogin"].Trim();
                m_userModel.password = collection["userPassword"].Trim();
                m_userModel.name = collection["userName"].Trim();
                m_userModel.employee_no = collection["userEmployeeNo"].Trim();
                m_userModel.mobile_phone = collection["userMobilePhone"].Trim();
                m_userModel.mail = collection["userMail"].Trim();
                m_userModel.remark = collection["userRemark"].Trim();
                //m_userModel.admin = true;
                m_userModel.invalid = true;
                m_userModel.deleted = false;
                m_userModel.created_at = DateTime.Now;
                m_userModel.created_by = -1;
                m_userModel.created_ip = collection["createdIP"].Trim();
                m_userModel.app_id = appsID;

                try
                {
                    if (m_usersBllservice.Add(m_userModel) == 0)
                    {
                        m_appsBllservice.Delete(appsID);//删除申请的系统记录
                        return Content("<script>alert('系统管理员添加失败！');window.history.back();</script>");
                    }
                }
                catch(Exception ex)
                {
                    m_appsBllservice.Delete(appsID);//删除申请的系统记录
                    return Content("<script>alert('系统管理员添加失败！');window.history.back();</script>");
                }
            }
            else
            {
                return Content("<script>alert('系统信息添加失败！');window.history.back();</script>");
            }

            return RedirectToAction("Login");
        }
    }
}
