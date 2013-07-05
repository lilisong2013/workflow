using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Collections;
namespace WorkFlow.Controllers
{
    public class UsersManagementController : Controller
    {
        public ActionResult AppUsers()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
           
        }

        //删除一条用户信息
        public ActionResult DeleteUser()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                int userID = Convert.ToInt32(Request.Form["userID"]);
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_userModel = new WorkFlow.UsersWebService.usersModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;

                WorkFlow.UsersWebService.SecurityContext m_securityContext = new UsersWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_usersBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                //m_userModel = m_usersBllService.GetModelByID(userID,out msg);

                try
                {
                    if (m_usersBllService.LogicDelete(userID, out msg))
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "成功删除记录", toUrl = "/UsersManagement/AppUsers" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "删除失败!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常!" });
                }
            }
          
        }

        //获取用户列表(在grid中显示)
        public ActionResult GetUsers_Apply()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                string msg = string.Empty;
                WorkFlow.UsersWebService.usersBLLservice m_usersService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();


                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersService.SecurityContextValue = m_SecurityContext;

                int appID = Convert.ToInt32(m_userModel.app_id);
                string data = "{Rows:[";
                try
                {
                    DataSet ds = m_usersService.GetAllUsersListOfApp(appID, out msg);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string login = ds.Tables[0].Rows[i][1].ToString();
                        string id = ds.Tables[0].Rows[i][0].ToString();
                        string name = ds.Tables[0].Rows[i][3].ToString();
                        string employee_no = ds.Tables[0].Rows[i][4].ToString();
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            data += "{login:'" + login + "',";
                            data += "id:'" + id + "',";
                            data += "name:'" + name + "',";
                            data += "employee_no:'" + employee_no + "'}";
                        }
                        else
                        {
                            data += "{login:'" + login + "',";
                            data += "id:'" + id + "',";
                            data += "name:'" + name + "',";
                            data += "employee_no:'" + employee_no + "'},";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                data += "]}";
                return Json(data);
          
            }
            
        }
       
        //获取是否有效的列表
        public ActionResult GetInvalidList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_userID = Convert.ToInt32(Request.Params["userID"]);//用户ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";

                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                WorkFlow.UsersWebService.usersModel userModel = new WorkFlow.UsersWebService.usersModel();

                #region 系统管理员授权
                string msg = string.Empty;
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_usersBllService.SecurityContextValue = m_SecurityContext;
                #endregion

                userModel = m_usersBllService.GetModelByID(m_userID, out msg);

                string m_selected = string.Empty;
                int total = 1;
                int m_usersID = Convert.ToInt32(userModel.id);
                string m_InvalidName;
                m_InvalidName = "是";
                //判断角色中是否已经存在该权限
                if (userModel.invalid == false)
                {
                    m_selected = "true";
                }
                else
                {
                    m_selected = "false";
                }
                strJson += "{id:'" + m_usersID + "',";
                strJson += "name:'" + m_InvalidName + "',";
                strJson += "selected:'" + m_selected + "'}";


                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
           
        }
        
        ///<summary>
        ///显示数据库中用户表的详细信息
        ///</summary>
        ///<param name="id">系统的ID</param>
        ///<return>返回所有信息</return>
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersBllService.SecurityContextValue = m_SecurityContext;

                m_usersModel = m_usersBllService.GetModelByID(id, out msg);
                ViewData["usersLogin"] = m_usersModel.login;
                ViewData["usersName"] = m_usersModel.name;
                ViewData["usersEmployee_no"] = m_usersModel.employee_no;
                ViewData["usersMobile_phone"] = m_usersModel.mobile_phone;
                ViewData["usersMail"] = m_usersModel.mail;
                ViewData["usersRemark"] = m_usersModel.remark;
                ViewData["usersAdmin"] = m_usersModel.admin;
                ViewData["usersInvalid"] = m_usersModel.invalid;
                ViewData["usersDeleted"] = m_usersModel.deleted;
                ViewData["usersCreated_at"] = m_usersModel.created_at;
                ViewData["usersCreated_by"] = m_usersModel.created_by;
                ViewData["usersCreated_ip"] = m_usersModel.created_ip;
                ViewData["usersUpdated_at"] = m_usersModel.updated_at;
                ViewData["usersUpdated_by"] = m_usersModel.updated_by;
                ViewData["usersUpdated_ip"] = m_usersModel.updated_ip;
                ViewData["usersApp_id"] = m_usersModel.app_id;
                return View();
            }
        
        }
     
        ///<summary>
        ///添加数据到数据库表中
        ///</summary>
        public ActionResult AddUsers(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

                #region 系统管理员授权
                string msg = string.Empty;
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();
                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersBllService.SecurityContextValue = m_SecurityContext;
                #endregion

                int appID = Convert.ToInt32(m_userModel.app_id);
                string login = collection["usersLogin"].Trim();
                string password = collection["usersPassword"].Trim();
                string passwordCon = collection["passwordcon"].Trim();
                string name = collection["usersName"].Trim();
                string employee_no = collection["usersEmployee_no"].Trim();
                string mobile_phone = collection["usersMobile_phone"].Trim();
                string mail = collection["usersMail"].Trim();
                if (login.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "登录名称不能为空!" });
                }
                if (password.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "登录密码不能为空!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(password) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "登录密码以字母开头,字母和数字的组成,至少6位!" });
                }
                if (passwordCon.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "确认密码不能为空!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(passwordCon) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "确认密码以字母开头,字母和数字的组合,至少6位!" });
                }
                if (password.Equals(passwordCon) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "登录密码和确认密码不一致!" });
                }
                if (name.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户姓名不能为空!" });
                }
                if (employee_no.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "工号不能为空!" });
                }
                if (mobile_phone.Length == 0 || mobile_phone.Length != 11)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "手机号不能为空或手机号码不足11位!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsMobilePhone(mobile_phone) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "手机号码格式不正确!" });
                }
                if (mail.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "邮件不能为空!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsEmail(mail) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "邮件格式不正确!" });
                }
                DataSet ds = m_usersBllService.GetAllUsersListOfApp(appID, out msg);
                ArrayList usersList = new ArrayList();
                var total = ds.Tables[0].Rows.Count;
                for (int i = 0; i < total; i++)
                {
                    usersList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                foreach (string userList in usersList)
                {
                    if (userList.Equals(collection["usersLogin"].Trim().ToString()))
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的登录名称！" });
                    }
                }

                WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)Session["apps"];
                m_usersModel.login = collection["usersLogin"].Trim();
                m_usersModel.password = collection["usersPassword"].Trim();
                m_usersModel.name = collection["usersName"].Trim();
                m_usersModel.employee_no = collection["usersEmployee_no"].Trim();
                m_usersModel.mobile_phone = collection["usersMobile_phone"].Trim();
                m_usersModel.mail = collection["usersMail"].Trim();
                m_usersModel.remark = collection["usersRemark"].Trim();
                m_usersModel.admin = Convert.ToBoolean(collection["usersAdmin"].Trim());
                m_usersModel.created_at = Convert.ToDateTime(collection["usersCreated_at"].Trim());
                m_usersModel.created_by = Convert.ToInt32(collection["usersCreated_by"].Trim());
                m_usersModel.created_ip = collection["usersCreated_ip"].Trim();
                m_usersModel.app_id = appID;
                try
                {
                    if (m_usersBllService.AddSysUser(m_usersModel, out msg) != 0)
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加用户成功!", toUrl = "/UsersManagement/AppUsers" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "添加用户失败!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错!" });
                }
           
            }
              
        }
       
        ///<summary>
        ///获取数据表中ID的信息
        ///</summary>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult EditPage(int id)
        {  if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }  
            else
            {
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

                #region 系统管理员授权
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();
                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;

                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersBllService.SecurityContextValue = m_SecurityContext;
                #endregion

                int appID = Convert.ToInt32(m_userModel.app_id);
                string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);
                string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress();

                m_usersModel = m_usersBllService.GetModelByID(id, out msg);
                ViewData["usersId"] = id;
                ViewData["usersLogin"] = m_usersModel.login;
                ViewData["usersName"] = m_usersModel.name;
                ViewData["usersPassword"] = m_usersModel.password;
                ViewData["usersEmployee_no"] = m_usersModel.employee_no;
                ViewData["usersMobile_phone"] = m_usersModel.mobile_phone;
                ViewData["usersMail"] = m_usersModel.mail;
                ViewData["usersRemark"] = m_usersModel.remark;
                ViewData["usersAdmin"] = m_usersModel.admin;
                ViewData["usersInvalid"] = m_usersModel.invalid;
                ViewData["usersDeleted"] = m_usersModel.deleted;
                ViewData["usersCreated_at"] = m_usersModel.created_at;
                ViewData["usersCreated_by"] = m_usersModel.created_by;
                ViewData["usersCreated_ip"] = m_usersModel.created_ip;
                ViewData["usersUpdated_at"] = m_usersModel.updated_at;
                ViewData["usersUpdated_by"] = m_usersModel.updated_by;
                ViewData["usersUpdated_ip"] = m_usersModel.updated_ip;
                ViewData["usersApp_id"] = appID;
                
                return View();
            }
            
        }
       
        ///<summary>
        ///编辑数据表中的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUsers(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_ue_total = Convert.ToInt32(Request.Params["in_Total"]);//用户"是否有效"的数量
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

                #region 系统管理员授权
                string msg = string.Empty;
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();
                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersBllService.SecurityContextValue = m_SecurityContext;
                #endregion

                int appID = Convert.ToInt32(m_userModel.app_id);
                string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);
                string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress();

                int id = Convert.ToInt32(collection["usersId"]);
                m_usersModel = m_usersBllService.GetModelByID(id, out msg);
                string login = collection["usersLogin"].Trim();
                string opass = Request.Form["oldPassword"];
                string npass = Request.Form["newPassword"];
                string passcon = Request.Form["PasswordCon"];
                string name = collection["usersName"].Trim();
                string employeeno = collection["usersEmployee_no"].Trim();
                string phone = collection["usersMobile_phone"].Trim();
                string mail = collection["usersMail"].Trim();

                string invalid = Request.Params["invalidValue"];

                if (login.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "登录名称不能为空!" });
                }
                if (opass.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "原密码不能为空!" });
                }
                if (m_usersBllService.OLoginValidator(m_usersModel.login, opass, (int)m_usersModel.app_id, out msg)==false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "原密码不正确!" });
                }
                if (npass.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "新密码不能为空!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(npass) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="新密码必须以字母开头，且字母数字组合，至少为6位!"});
                }
                if (passcon.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="确认密码不能为空!"});
                }

                if (npass != passcon)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="新密码和确认密码不一致!"});
                }
                if (name.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "用户姓名不能为空!" });
                }
                if (employeeno.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "工号不能为空!" });
                }
                if (phone.Length == 0 || phone.Length != 11)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "手机号不能为空或手机号位数必须为11位!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsMobilePhone(phone) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "手机号码的格式不正确!" });
                }
                if (mail.Length == 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "邮件不能为空!" });
                }
                if (Saron.Common.PubFun.ConditionFilter.IsEmail(mail) == false)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "邮件格式不正确!" });
                }

                DataSet ds = m_usersBllService.GetAllUsersListOfApp(appID, out msg);
                ArrayList userList = new ArrayList();
                var total = ds.Tables[0].Rows.Count;
                //将数据库中登录名称放到ArrayList中。
                for (int i = 0; i < total; i++)
                {
                    userList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                //如果修改后的登录名称和修改前的登录名称一样。将数据库中修改前的登录名称删除
                for (int i = 0; i < total; i++)
                {
                    if (m_usersModel.login.ToString().Equals(collection["usersLogin"].Trim().ToString()))
                    {
                        userList.Remove(m_usersModel.login);
                    }
                }
                //遍历数据表中登录名称与编辑修改后的登录名称是否相同，如果相同给出提示。
                foreach (string userlist in userList)
                {
                    if (userlist.Equals(collection["usersLogin"].Trim().ToString()))
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的登录名称" });
                    }
                }
                m_usersModel.login = collection["usersLogin"].Trim();
                m_usersModel.password = collection["usersPassword"].Trim();
                m_usersModel.name = collection["usersName"].Trim();
                m_usersModel.employee_no = collection["usersEmployee_no"].Trim();
                m_usersModel.mobile_phone = collection["usersMobile_phone"].Trim();
                m_usersModel.mail = collection["usersMail"].Trim();
                m_usersModel.remark = collection["usersRemark"].Trim();
                m_usersModel.admin = false;
                if (m_ue_total == 1)
                {
                    m_usersModel.invalid = false;
                }
                if (m_ue_total == 0)
                {
                    m_usersModel.invalid = true;
                }
                //m_usersModel.invalid = Convert.ToBoolean(Request.Params["invalidValue"]);

                m_usersModel.deleted = Convert.ToBoolean(collection["usersDeleted"].Trim());
                m_usersModel.updated_at = t;
                m_usersModel.updated_by = m_userModel.id;
                m_usersModel.updated_ip = ipAddress;
                // m_usersModel.app_id = Convert.ToInt32(collection["usersApp_id"].Trim());
                try
                {
                    if (m_usersBllService.AdminUpdate(m_usersModel, out msg))
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改用户成功!", toUrl = "/UsersManagement/AppUsers" });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "修改用户失败!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常!" });
                }
          
            }
           
          
        }
       
        ///<summary>
        ///给用户赋角色
        ///</summary>
        ///<returns></returns>
        public ActionResult UserRoles(int id)
        {    if (Session["user"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }           
           else
           {
            string msg = string.Empty;
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();
            WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();

            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            m_SecurityContext.UserName = m_userModel.login;
            m_SecurityContext.PassWord = m_userModel.password;
            m_SecurityContext.AppID = (int)m_userModel.app_id;
            m_usersBllService.SecurityContextValue = m_SecurityContext;

            int usersID = Convert.ToInt32(Request.Params[0].ToString());
            try
            {
                m_usersModel = m_usersBllService.GetModelByID(usersID, out msg);
            }
            catch (Exception ex) { }
            ViewData["u_ID"] = m_usersModel.id;
            ViewData["u_login"] = m_usersModel.login;
            return View();
          }
               
                      
        }
        
        ///<summary>
        ///获取角色类型的权限列表
        /// </summary>
        ///<returns></returns>
        public ActionResult GetRolesList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int m_usersID = Convert.ToInt32(Request.Params["usersID"]);//用户ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";
                WorkFlow.User_RoleBLLservice.user_roleBLLservice m_user_roleBllService = new User_RoleBLLservice.user_roleBLLservice();
                WorkFlow.User_RoleBLLservice.user_roleModel m_user_roleModel = new User_RoleBLLservice.user_roleModel();

                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = new DataSet();
                try
                {   //？？
                    ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id, out msg);
                }
                catch (Exception ex)
                { }
                int total = ds.Tables[0].Rows.Count;//某系统角色类型的数量
                for (int i = 0; i < total; i++)
                {
                    int m_rolesID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    string m_rolesName = ds.Tables[0].Rows[i][1].ToString();
                    string m_selected = string.Empty;
                    //判断用户中是否存在此角色
                    if (m_user_roleBllService.Exists(m_usersID, m_rolesID))
                    {
                        m_selected = "true";
                    }
                    else
                    {
                        m_selected = "false";
                    }
                    if (i < total - 1)
                    {
                        strJson += "{id:'" + m_rolesID + "',";
                        strJson += "name:'" + m_rolesName + "',";
                        strJson += "selected:'" + m_selected + "'},";
                    }
                    else
                    {
                        strJson += "{id:'" + m_rolesID + "',";
                        strJson += "name:'" + m_rolesName + "',";
                        strJson += "selected:'" + m_selected + "'}";
                    }
                }
                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
            
        }
        
        ///<summary>
        ///添加用户角色
        ///</summary>
        ///<returns></returns>
        public ActionResult AddUserRoles()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_usersID = Convert.ToInt32(Request.Params["u_ID"]);//用户ID
                int m_ur_total = Convert.ToInt32(Request.Params["ur_total"]);//用户角色数量
                WorkFlow.User_RoleBLLservice.user_roleBLLservice m_user_roleBllService = new User_RoleBLLservice.user_roleBLLservice();
                WorkFlow.User_RoleBLLservice.user_roleModel m_user_roleModel = new User_RoleBLLservice.user_roleModel();

                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();
                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                try
                {
                    if (m_user_roleBllService.DeleteByRoleID(m_usersID))
                    //删除用户下的角色
                    {
                        for (int i = 0; i < m_ur_total; i++)
                        {
                            int m_rolesID = Convert.ToInt32(Request.Params[("rprivilegeID" + i)]);
                            m_user_roleModel.user_id = m_usersID;
                            m_user_roleModel.role_id = m_rolesID;
                            if ((m_user_roleBllService.Add(m_user_roleModel) == 0))
                            {
                                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "修改失败!" });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错！" });
                }
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "角色添加成功！", toUrl = "/UsersManagement/AppUsers" });
            }

        }

    }
}