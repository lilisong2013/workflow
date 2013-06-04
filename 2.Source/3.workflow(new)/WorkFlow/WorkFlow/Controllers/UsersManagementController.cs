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
            return View();
        }
        /// <summary>
        /// 显示数据库用户表的所有信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsers_Apply()
        {
            WorkFlow.UsersWebService.usersModel m_userModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
            int appID = Convert.ToInt32(m_userModel.app_id);
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);
            WorkFlow.UsersWebService.usersBLLservice m_usersService = new UsersWebService.usersBLLservice();
            DataSet ds = m_usersService.GetAllUsersListOfApp(appID);
            IList<WorkFlow.UsersWebService.usersModel> m_list = new List<WorkFlow.UsersWebService.usersModel>();
            var total = ds.Tables[0].Rows.Count;
            for (var i = 0; i < total; i++)
            {
                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Activator.CreateInstance(typeof(WorkFlow.UsersWebService.usersModel));
                PropertyInfo[] m_propertys = m_usersModel.GetType().GetProperties();
                foreach (PropertyInfo pi in m_propertys)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                        {
                            // 数据库NULL值单独处理 
                            if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                pi.SetValue(m_usersModel, ds.Tables[0].Rows[i][j], null);
                            else
                                pi.SetValue(m_usersModel, null, null);
                            break;
                        }
                    }
                }
                m_list.Add(m_usersModel);
            }

            //模拟排序操作
            if (sortorder == "desc")
                m_list = m_list.OrderByDescending(c => c.id).ToList();
            else
                m_list = m_list.OrderBy(c => c.id).ToList();

            IList<WorkFlow.UsersWebService.usersModel> m_targetList = new List<WorkFlow.UsersWebService.usersModel>();
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

        ///<summary>
        ///显示数据库中用户表的详细信息
        ///</summary>
        ///<param name="id">系统的ID</param>
        ///<return>返回所有信息</return>
        public ActionResult DetailInfo(int id)
        {
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();
            m_usersModel = m_usersBllService.GetModelByID(id);
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
        ///<summary>
        ///删除ID号的记录
        ///</summary>
        public ActionResult ChangePage(int id)
        {
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();
            m_usersModel = m_usersBllService.GetModelByID(id);
            if (m_usersBllService.Delete(id))
            {
                return RedirectToAction("AppUsers");
            }
            else
                return View();
        }
        ///<summary>
        ///添加数据到数据库表中
        ///</summary>
        public ActionResult AddUsers(FormCollection collection)
        {
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            int appID = Convert.ToInt32(m_userModel.app_id);
            string login = collection["usersLogin"].Trim();
            string password = collection["usersPassword"].Trim();
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
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "密码不能为空!" });
            }
            if (Saron.Common.PubFun.ConditionFilter.IsPassWord(password)==false)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "密码必须为字母和数字的组合且至少为8位!" });
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
            DataSet ds = m_usersBllService.GetAllUsersListOfApp(appID);
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
            m_usersModel.invalid = Convert.ToBoolean(collection["usersInvalid"].Trim());
            m_usersModel.deleted = Convert.ToBoolean(collection["usersDeleted"].Trim());
            m_usersModel.created_at = Convert.ToDateTime(collection["usersCreated_at"].Trim());
            m_usersModel.created_by = Convert.ToInt32(collection["usersCreated_by"].Trim());
            m_usersModel.created_ip = collection["usersCreated_ip"].Trim();
            m_usersModel.app_id = appID;
            m_usersBllService.Add(m_usersModel);
            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功!", toUrl = "/UsersManagement/AppUsers" });
        }
        ///<summary>
        ///获取数据表中ID的信息
        ///</summary>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult EditPage(int id)
        {
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

            WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();
            int appID =Convert.ToInt32(m_userModel.app_id);
            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);
            string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress();
 
            m_usersModel = m_usersBllService.GetModelByID(id);
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
            //ViewData["usersApp_id"] = m_appsModel.id;
            return View();
        }
        ///<summary>
        ///编辑数据表中的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUsers(FormCollection collection)
        {
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();

            WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();
            int appID = Convert.ToInt32(m_userModel.app_id);

            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);
            string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress();

            int id = Convert.ToInt32(collection["usersId"]);
            m_usersModel = m_usersBllService.GetModelByID(id);
            string login = collection["usersLogin"].Trim();
            //string pass=Request.Form["usersPassword"];
            string pass = collection["usersPassword"].Trim();
            string name = collection["usersName"].Trim();
            string employeeno = collection["usersEmployee_no"].Trim();
            string phone = collection["usersMobile_phone"].Trim();
            string mail = collection["usersMail"].Trim();
            // string appid = (collection["usersApp_id"].Trim());
            string admin = (collection["usersAdmin"].Trim());
            string invalid = (collection["usersInvalid"].Trim());
            if (login.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "登录名称不能为空!" });
            }
            if (pass.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "密码不能为空!" });
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
            /*if (appid.Length == 0)
             {
                 return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="系统ID不能为空!" });
             }*/
            if (admin.Length == 0 || admin.Equals("请选择"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "是否为管理员不能为空!" });
            }
            if (invalid.Length == 0 || invalid.Equals("请选择"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "是否有效不能为空!" });
            }
            DataSet ds = m_usersBllService.GetAllUsersListOfApp(appID);
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
            m_usersModel.admin = Convert.ToBoolean(collection["usersAdmin"].Trim());
            m_usersModel.invalid = Convert.ToBoolean(collection["usersInvalid"].Trim());
            m_usersModel.deleted = Convert.ToBoolean(collection["usersDeleted"].Trim());
            m_usersModel.updated_at = t;
            m_usersModel.updated_by = m_userModel.id;
            m_usersModel.updated_ip = ipAddress;
            // m_usersModel.app_id = Convert.ToInt32(collection["usersApp_id"].Trim());
            if (m_usersBllService.Update(m_usersModel))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功!", toUrl = "/UsersManagement/AppUsers" });
            }
            else
            {
                return RedirectToAction("AppUsers");
            }
        }
        ///<summary>
        ///给用户赋角色
        ///</summary>
        ///<returns></returns>
        public ActionResult UserRoles()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_usersModel = new UsersWebService.usersModel();
                int usersID = Convert.ToInt32(Request.Params[0].ToString());
                try
                {
                    m_usersModel = m_usersBllService.GetModelByID(usersID);
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
            int m_usersID = Convert.ToInt32(Request.Params["usersID"]);//用户ID
            string strJson="{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";
            WorkFlow.User_RoleBLLservice.user_roleBLLservice m_user_roleBllService = new User_RoleBLLservice.user_roleBLLservice();
            WorkFlow.User_RoleBLLservice.user_roleModel m_user_roleModel = new User_RoleBLLservice.user_roleModel();
          
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
           
            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
            DataSet ds = new DataSet();
            try
            {
                ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id);
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
                    strJson += "name:'" + m_rolesName + "'},";
                }
                else 
                {
                    strJson += "{id:'"+m_rolesID+"',";
                    strJson += "name:'" + m_rolesName + "'}";
                }
            }
            strJson += "],total:'"+total+"'}";
            return Json(strJson);
        }
        ///<summary>
        ///添加用户角色
        ///</summary>
        ///<returns></returns>
        public ActionResult AddUserRoles()
        {
            int m_usersID = Convert.ToInt32(Request.Params["u_ID"]);//用户ID
            int m_ur_total = Convert.ToInt32(Request.Params["ur_total"]);//用户角色数量
            WorkFlow.User_RoleBLLservice.user_roleBLLservice m_user_roleBllService = new User_RoleBLLservice.user_roleBLLservice();
            WorkFlow.User_RoleBLLservice.user_roleModel m_user_roleModel = new User_RoleBLLservice.user_roleModel();
            
            WorkFlow.UsersWebService.usersBLLservice m_usersBllService=new UsersWebService.usersBLLservice();
            WorkFlow.UsersWebService.usersModel m_usersModel=new UsersWebService.usersModel();
            WorkFlow.UsersWebService.usersModel m_userModel =(WorkFlow.UsersWebService.usersModel)Session["user"];
            try
            {
                if (m_user_roleBllService.DeleteByRoleID(m_usersID))
                //删除用户下的角色
                {
                    for (int i = 0;i < m_ur_total;i++)
                    {
                        int m_rolesID = Convert.ToInt32(Request.Params[("rprivilegeID" + i)]);
                        m_user_roleModel.user_id = m_usersID;
                        m_user_roleModel.role_id = m_rolesID;
                        if ((m_user_roleBllService.Add(m_user_roleModel)==0))
                        {
                            return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="修改失败!"});
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错！" });
            }
            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功！", toUrl = "/UsersManagement/UserRoles" });
        }
    }
}