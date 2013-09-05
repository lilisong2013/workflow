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

    

                try
                {
                    if (m_usersBllService.LogicDelete(userID, out msg))
                    {

                        return Json("{success:true,css:'alert alert-success',message:'成功删除用户信息!'}");
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
    
        //后台分页，获取用户列表
        public ActionResult GetUsers_List()
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

                string msg = string.Empty;
                WorkFlow.UsersWebService.usersBLLservice m_usersService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_usersService.GetAllUsersListOfApp((int)m_userModel.app_id,out msg);

                IList<WorkFlow.UsersWebService.usersModel> m_list = new List<WorkFlow.UsersWebService.usersModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.UsersWebService.usersModel m_usersModel= (WorkFlow.UsersWebService.usersModel)Activator.CreateInstance(typeof(WorkFlow.UsersWebService.usersModel));
                    PropertyInfo[] m_propertys = m_usersModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            {
                                //数据库NULL值单独处理
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
        
        //显示用户的详细信息   
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
                if (m_usersModel.admin == true)
                {
                    ViewData["usersAdmin"] ="是";
                }
                if (m_usersModel.admin == false)
                {
                    ViewData["usersAdmin"] = "否";
                }
                if (m_usersModel.invalid == true)
                {
                   ViewData["usersInvalid"] ="否";
                }
                if (m_usersModel.invalid == false)
                {
                    ViewData["usersInvalid"] = "是";
                }
              
                ViewData["usersCreated_at"] = m_usersModel.created_at;
                ViewData["usersCreated_by"] = m_userModel.login;
                ViewData["usersCreated_ip"] = m_usersModel.created_ip;
                ViewData["usersUpdated_at"] = m_usersModel.updated_at;
                ViewData["usersUpdated_by"] = m_userModel.login;
                ViewData["usersUpdated_ip"] = m_usersModel.updated_ip;
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.SecurityContext ma_SecurityContext = new AppsWebService.SecurityContext();

                ma_SecurityContext.UserName = m_userModel.login;
                ma_SecurityContext.PassWord = m_userModel.password;
                ma_SecurityContext.AppID = (int)m_userModel.app_id;
                m_appsBllService.SecurityContextValue = ma_SecurityContext;

                ViewData["usersApp_id"] = m_appsBllService.GetAppNameByID((int)m_userModel.app_id, out msg);

                return View();
            }
        
        }
     
        //添加用户信息
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
                string remark = collection["usersRemark"].Trim();
                if (login.Length == 0)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'登录名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["usersLogin"]) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'登录名称含有非法字符,只能包含字母、汉字、数字、下划线!'}");
                }
             
                if (password.Length == 0)
                {

                    return Json("{success:false,css:'alert alert-error',message:'登录密码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(password) == false)
                {
            
                    return Json("{success:false,css:'alert alert-error',message:'登录密码以字母开头,字母和数字的组成,至少6位!'}");
                }
                if (passwordCon.Length == 0)
                {

                    return Json("{success:false,css:'alert alert-error',message:'确认密码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsPassWord(passwordCon) == false)
                {

                    return Json("{success:false,css:'alert alert-error',message:'确认密码以字母开头,字母和数字的组合,至少6位!'}");
                }
                if (password.Equals(passwordCon) == false)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'登录密码和确认密码不一致!'}");
                }
                if (name.Length == 0)
                {
           
                    return Json("{success:false,css:'alert alert-error',message:'用户姓名不能为空!'}");
                }
                if (employee_no.Length == 0)
                {
           
                    return Json("{success:false,css:'alert alert-error',message:'员工编号不能为空!'}");
                }

                if (Convert.ToInt32(employee_no.Length.ToString()) > 40)
                {
                    return Json("{success:false,css:'alert alert-error',message:'员工编号不能超过40个字符!'}");
                }

                if (mobile_phone.Length == 0 || mobile_phone.Length != 11)
                {

                    return Json("{success:false,css:'alert alert-error',message:'手机号码不能为空或手机号码不足11位!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsMobilePhone(mobile_phone) == false)
                {
    
                    return Json("{success:false,css:'alert alert-error',message:'手机号码格式不正确!'}");
                }
                if (mail.Length == 0)
                {
              
                    return Json("{success:false,css:'alert alert-error',message:'邮件地址不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsEmail(mail) == false)
                {

                    return Json("{success:false,css:'alert alert-error',message:'邮件地址格式不正确!'}");
                }

                if (Convert.ToInt32(remark.ToString().Length) > 150)
                {
                    return Json("{success:false,css:'alert alert-error',message:'备注信息的字符长度不能超过150个字符!'}");
                }
                //获取某应用系统中数据表中用户的登录名和工号列表
                DataSet ds = m_usersBllService.GetAllUsersListOfApp(appID, out msg);
                ArrayList usersList = new ArrayList();
                ArrayList empNoList = new ArrayList();
                var total = ds.Tables[0].Rows.Count;
                for (int i = 0; i < total; i++)
                {
                    usersList.Add(ds.Tables[0].Rows[i][1].ToString());
                    empNoList.Add(ds.Tables[0].Rows[i][4].ToString());
                }

               //判断数据表中和添加的登录名称是否相同
                foreach (string userList in usersList)
                {
                    if (userList.Equals(collection["usersLogin"].Trim().ToString()))
                    {
 
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的登录名称！'}");
                    }
                }

                //判断数据表中和添加的工号是否相同
                foreach (string empnoList in empNoList)
                {
                    if (empnoList.Equals(collection["usersEmployee_no"].Trim().ToString()))
                    {
       
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的员工编号!'}");
                    }
                }

                //赋值
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
           
                        return Json("{success:true,css:'alert alert-success',message:'添加用户成功!'}");
                    }
                    else
                    {

                        return Json("{success:false,css:'alert alert-error',message:'添加用户失败!'}");
                    }
                }
                catch (Exception ex)
                {

                    return Json("{success:false,css:'alert alert-error',message:'程序出错!'}");
                }
           
            }
              
        }
       
        //获取用户表中ID的一条信息
        public ActionResult EditPage(int id)
        { 
            if (Session["user"] == null)
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
         
        //编辑用户信息    
        public ActionResult EditUsers(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_ue_total = Convert.ToInt32(Request.Form["in_Total"]);//用户"是否有效"的数量
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
  
                string npass = Request.Form["newPassword"];
                string passcon = Request.Form["PasswordCon"];
                string passvalue=m_usersModel.password;
                string name = collection["usersName"].Trim();
                string employeeno = collection["usersEmployee_no"].Trim();
                string phone = collection["usersMobile_phone"].Trim();
                string mail = collection["usersMail"].Trim();
                string remark = collection["usersRemark"].Trim();
                string invalid = Request.Params["invalidValue"];

                if (login.Length == 0)
                {
 
                    return Json("{success:false,css:'alert alert-error',message:'登录名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["usersLogin"]) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'登录名称含有非法字符,只能包含字母、汉字、数字、下划线!'}");
                }
             
                if (npass != passcon)
                {
   
                    return Json("{success:false,css:'alert alert-error',message:'新密码和确认密码不一致!'}");
                }

                if (name.Length == 0)
                {
             
                    return Json("{success:false,css:'alert alert-error',message:'用户姓名不能为空!'}");
                }
                if (employeeno.Length == 0)
                {
       
                    return Json("{success:false,css:'alert alert-error',message:'工号不能为空!'}");
                }
                if (phone.Length == 0 || phone.Length != 11)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'手机号不能为空或手机号位数必须为11位!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsMobilePhone(phone) == false)
                {
                
                    return Json("{success:false,css:'alert alert-error',message:'手机号码的格式不正确!'}");
                }
                if (mail.Length == 0)
                {
                 
                    return Json("{success:false,css:'alert alert-error',message:'邮件不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsEmail(mail) == false)
                {
     
                    return Json("{success:false,css:'alert alert-error',message:'邮件格式不正确!'}");
                }

                if (Convert.ToInt32(remark.ToString().Length) > 150)
                {
                    return Json("{success:false,css:'alert alert-error',message:'备注的长度不能超过150个字符!'}");
                }
                DataSet ds = m_usersBllService.GetAllUsersListOfApp(appID, out msg);
                ArrayList userList = new ArrayList();
                ArrayList empNoList = new ArrayList();
                var total = ds.Tables[0].Rows.Count;

                //将数据库中登录名称放到ArrayList中。
                for (int i = 0; i < total; i++)
                {
                    userList.Add(ds.Tables[0].Rows[i][1].ToString());
                    empNoList.Add(ds.Tables[0].Rows[i][4].ToString());
                }
                //如果修改后的登录名称和修改前的登录名称一样。将数据库中修改前的登录名称删除
                for (int i = 0; i < total; i++)
                {
                    if (m_usersModel.login.ToString().Equals(collection["usersLogin"].Trim().ToString()))
                    {
                        userList.Remove(m_usersModel.login);
                    }
                }
                //如果修改后的员工号和修改前的员工号一样，将数据库中修改前的员工号删除
                for (int i = 0; i < total; i++)
                {
                    if (m_usersModel.employee_no.ToString().Equals(collection["usersEmployee_no"].Trim().ToString()))
                    {
                        empNoList.Remove(m_usersModel.employee_no);
                    }
                }
                   //遍历数据表中登录名称与编辑修改后的登录名称是否相同，如果相同给出提示。
                    foreach (string userlist in userList)
                    {
                        if (userlist.Equals(collection["usersLogin"].Trim().ToString()))
                        {
                         
                            return Json("{success:false,css:'alert alert-error',message:'已经存在相同的登录名称!'}");
                        }
                    }
                  //遍历数据表中的员工编号与编辑修改后的员工编号是否相同，如果相同给出提示
                    foreach (string empnolist in empNoList)
                    {
                        if (empnolist.Equals(collection["usersEmployee_no"].Trim().ToString()))
                        {
                    
                            return Json("{success:false,css:'alert alert-error',message:'已经存在相同的员工编号!'}");
                        }
                    }
                m_usersModel.login = collection["usersLogin"].Trim();
        
                if (npass.Length != 0)
                {
                    if (Saron.Common.PubFun.ConditionFilter.IsPassWord(npass) == false)
                    {  
           
                        return Json("{success:false,css:'alert alert-error',message:'新密码以字母开头，字母和数字的组合，至少6位!'}");
                    }
                    if (Saron.Common.PubFun.ConditionFilter.IsPassWord(passcon) == false)
                    {
                   
                        return Json("{success:false,css:'alert alert-error',message:'确认密码以字母开头，字母和数字的组合，至少6位!'}");
                    }
                    m_usersModel.password = Request.Form["newPassword"];
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
                  
                    m_usersModel.deleted = Convert.ToBoolean(collection["usersDeleted"].Trim());
                    m_usersModel.updated_at = t;
                    m_usersModel.updated_by = m_userModel.id;
                    m_usersModel.updated_ip = ipAddress;
                    try
                    {
                    if (m_usersBllService.AdminUpdate(m_usersModel, out msg))
                    {
                   
                        return Json("{success:true,css:'alert alert-success',message:'修改用户成功!'}");
                    }
                    else
                    {
                   
                        return Json("{success:false,css:'alert alert-error',message:'修改用户失败!'}");
                    }
                  }
                 catch (Exception ex)
                    {
         
                        return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                 }
               
                }
              //新密码为空，不修改，保持原来密码。
                else 
                {
                    m_usersModel.password = passvalue;
                   // m_usersModel.password = Request.Form["newPassword"];
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

                    m_usersModel.deleted = Convert.ToBoolean(collection["usersDeleted"].Trim());
                    m_usersModel.updated_at = t;
                    m_usersModel.updated_by = m_userModel.id;
                    m_usersModel.updated_ip = ipAddress;
                    try
                    {
                        if (m_usersBllService.AdminUpdatePass(m_usersModel, out msg))
                        {
              
                            return Json("{success:true,css:'alert alert-success',message:'修改用户成功!'}");
                        }
                        else
                        {
                    
                            return Json("{success:false,css:'alert alert-error',message:'修改用户失败!'}");
                        }
                    }
                    catch (Exception ex)
                    {

                        return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                    }
                }
          

            }
        }
       
        //给用户赋角色
        public ActionResult UserRoles(int id)
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
        
        //获取角色类型的权限列表
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
                {   
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
        
        //添加用户角色
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

                                return Json("{success:false,css:'alert alert-error',message:'修改失败!'}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                
                    return Json("{success:false,css:'alert alert-error',message:'程序出错！'}");
                }
               
                return Json("{success:true,css:'alert alert-success',message:'角色添加成功！'}");
            }

        }

        //根据用户登录名称实现模糊查询
        public ActionResult GetUserListByLogin(string userlogin)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.SecurityContext m_SecurityContext = new UsersWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_userModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_usersBllService.SecurityContextValue = m_SecurityContext;

                //搜索关键字为空的情况，显示全部数据
                if (userlogin.Length == 0)
                {
                    DataSet ds = m_usersBllService.GetAllUsersListOfApp((int)m_userModel.app_id, out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else
                    {
                        try
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string login = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string name = Convert.ToString(ds.Tables[0].Rows[i][3]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string employeeno = Convert.ToString(ds.Tables[0].Rows[i][4]);
                                string invalid;
                                if (Convert.ToBoolean(ds.Tables[0].Rows[i][9]) == false)
                                {
                                    invalid = "是";
                                }
                                else
                                {
                                    invalid = "否"; 
                                }
                             
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{login:'" + login + "',";
                                    data += "id:'" + id + "',";
                                    data += "name:'" + name + "',";
                                    data+="invalid:'"+invalid+"',";
                                    data += "employee_no:'" + employeeno + "'}";
                                }
                                else
                                {
                                    data += "{login:'" + login + "',";
                                    data += "id:'" + id + "',";
                                    data += "name:'" + name + "',";
                                    data += "invalid:'" + invalid + "',";
                                    data += "employee_no:'" + employeeno + "'},";
                                }
                            }
                        }
                        catch (Exception ex) { }
                        data += "]}";
                        return Json(data);
                    }

                }
                //搜索关键字不为空的情况，显示部分搜索结果数据
                else {
                    DataSet ds = m_usersBllService.GetUserListByLogin(userlogin,(int)m_userModel.app_id,out msg);
                    string data = "{Rows:[";
                        try {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) 
                            {
                                string login = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string name = Convert.ToString(ds.Tables[0].Rows[i][3]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string employeeno = Convert.ToString(ds.Tables[0].Rows[i][4]);
                                string invalid;
                                if (Convert.ToBoolean(ds.Tables[0].Rows[i][9]) == false)
                                {
                                    invalid = "是";
                                }
                                else
                                {
                                    invalid = "否";
                                }
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{login:'" + login + "',";
                                    data += "id:'" + id + "',";
                                    data += "name:'" + name + "',";
                                    data += "invalid:'" + invalid + "',";
                                    data += "employee_no:'" + employeeno + "'}";
                                }
                                else
                                {
                                    data += "{login:'" + login + "',";
                                    data += "id:'" + id + "',";
                                    data += "name:'" + name + "',";
                                    data += "invalid:'" + invalid + "',";
                                    data += "employee_no:'" + employeeno + "'},";
                                }
                            }
                        }
                        catch (Exception ex) { }
                        data += "]}";
                        return Json(data);                
                }

            }
        }
    }
}