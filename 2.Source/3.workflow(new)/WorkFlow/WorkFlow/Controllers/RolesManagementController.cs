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
    public class RolesManagementController : Controller
    {
        //
        // GET: /RolesManagement/

        public ActionResult AppRoles()
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
      
        public ActionResult Role_Privileges()
        {
            string msg = string.Empty;
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                int roleID = Convert.ToInt32(Request.Params[0].ToString());

                try{
                    m_rolesModel = m_rolesBllService.GetModel(roleID,out msg);
                }catch(Exception ex)
                {
                }

                ViewData["r_ID"] = m_rolesModel.id;
                ViewData["r_Name"] = m_rolesModel.name;
                return View();
            }
        }       

        //角色添加    
        public ActionResult AddRoles(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();



                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;


                string m_rolesName = collection["rolesName"].Trim();
                if (m_rolesName.Length == 0)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'角色名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["rolesName"])==false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'角色名称含有非法字符串,只能包含字母、汉字、数字、下划线!'}");
                }
                m_rolesModel.name = collection["rolesName"].Trim();
                //获得deleted=false的rolesName列表
                DataSet ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id, out msg);
                var total = ds.Tables[0].Rows.Count;
                ArrayList rolesList = new ArrayList();
                for (int i = 0; i < total; i++)
                {
                    rolesList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                foreach (string rolesname in rolesList)
                {
                    if (rolesname.Equals(collection["rolesName"].Trim()))
                    {
                       
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的角色名称!'}");
                    }
                }
                m_rolesModel.name = collection["rolesName"].Trim();
                //m_rolesModel.invalid = Convert.ToBoolean(collection["rolesInvalid"].Trim());//String转化为Boolean
                //m_rolesModel.deleted = Convert.ToBoolean(collection["rolesDeleted"].Trim());//String转化为Boolean
                m_rolesModel.app_id = Convert.ToInt32(collection["rolesApp_id"].Trim());
                m_rolesModel.created_at = Convert.ToDateTime(collection["rolesCreated_at"].Trim());
                m_rolesModel.created_by = Convert.ToInt32(collection["rolesCreated_by"].Trim());
                m_rolesModel.created_ip = collection["rolesCreated_ip"].Trim();
                m_rolesModel.remark = collection["rolesRemark"].Trim();


                try
                {
                    if (m_rolesBllService.Add(m_rolesModel, out msg) != 0)
                    {
                       
                        return Json("{success:true,css:'alert alert-success',message:'角色添加成功!'}");
                    }
                    else
                    {
                      
                        return Json("{success:false,css:'alert alert-error',message:'角色添加失败!'}");
                    }
                }
                catch (Exception ex)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
           
        }
   
        //删除角色信息
        public ActionResult DeleteRole()
        {
           
                string msg = string.Empty;
                int roleID = Convert.ToInt32(Request.Form["roleID"]);
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                try
                {
                    if (m_rolesBllService.Delete(roleID, out msg))
                    {
                      
                        return Json("{success:true,css:'alert alert-success',message:'成功删除记录!'}");
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
        //获取是否有效的列表
        public ActionResult GetInvalidList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;

                int m_roleID = Convert.ToInt32(Request.Params["rolesID"]);//用户ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";

                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.RolesWebService.rolesModel m_roleModel = m_rolesBllService.GetModel(m_roleID, out msg);

                string m_selected = string.Empty;
                int total = 1;
                int m_rolesID = m_roleID;
                string m_InvalidName;
                m_InvalidName = "是";
                //判断角色中是否已经存在该权限
                if (m_roleModel.invalid == false)
                {
                    m_selected = "true";
                }
                else
                {
                    m_selected = "false";
                }
                strJson += "{id:'" + m_rolesID + "',";
                strJson += "name:'" + m_InvalidName + "',";
                strJson += "selected:'" + m_selected + "'}";


                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
            
        }
        /// <summary>
        /// 获取ID的数据表详情
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>
        public ActionResult EditPage(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();

                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;


                WorkFlow.RolesWebService.rolesModel m_rolesModel = m_rolesBllService.GetModel(id, out msg);

                ViewData["rolesId"] = m_rolesModel.id;
                ViewData["rolesName"] = m_rolesModel.name;
                ViewData["rolesRemark"] = m_rolesModel.remark;
                ViewData["rolesInvalid"] = m_rolesModel.invalid;
                ViewData["rolesDeleted"] = m_rolesModel.deleted;
                ViewData["rolesCreated_at"] = m_rolesModel.created_at;
                ViewData["rolesCreated_by"] = m_rolesModel.created_by;
                ViewData["rolesCreated_ip"] = m_rolesModel.created_ip;
                ViewData["rolesUpdated_at"] = m_rolesModel.updated_at;
                ViewData["rolesUpdated_by"] = m_rolesModel.updated_by;
                ViewData["rolesUpdated_ip"] = m_rolesModel.updated_ip;
                ViewData["rolesApp_id"] = m_rolesModel.app_id;
                return View();
            }   
           
        }

        public ActionResult GetInvalidName()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else {
                string[] invalidI = new string[2];
                invalidI[0] = "false";
                invalidI[1] = "true";

                string[] invalidN = new string[2];
                invalidN[0] = "是";
                invalidN[1] = "否";

                List<Saron.WorkFlow.Models.InvalidHelper> m_invalidlist = new List<Saron.WorkFlow.Models.InvalidHelper>();
                for (int i = 0; i < 2; i++)
                {
                    m_invalidlist.Add(new Saron.WorkFlow.Models.InvalidHelper { InvalidID = invalidI[i].ToString(), InvalidName = invalidN[i].ToString() });
                }
                var dataJson = new
                {
                    Rows = m_invalidlist,
                    Total = 2
                };
                return Json(dataJson, JsonRequestBehavior.AllowGet);
            }
           
        }

        /// <summary>
        /// 修改数据库中的信息
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>
        /// <summary>
        public ActionResult EditRoles(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_ri_total = Convert.ToInt32(Request.Form["rv_Total"]);//角色"是否有效"的数量
                string msg = string.Empty;
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();


                WorkFlow.RolesWebService.rolesModel m_roleModel = new WorkFlow.RolesWebService.rolesModel();

                WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                int id = Convert.ToInt32(collection["rolesId"].Trim());
                m_rolesModel = m_rolesBllService.GetModel(id, out msg);

                string name = collection["rolesName"].Trim();

                if (name.Length == 0)
                {
                    
                    return Json("{success:false,css:'alert alert-error',message:'角色名称不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsValidString(collection["rolesName"])==false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'角色名称中含有非法字符串,只能包含字母、汉字、数字、下划线!'}");
                }
                //获得deleted=false且应用系统ID为appid的rolesName列表
                DataSet ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id, out msg);
                var total = ds.Tables[0].Rows.Count;
                ArrayList rolesList = new ArrayList();
                for (int i = 0; i < total; i++)
                {
                    rolesList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                //如果是自己本身，角色名称修改后的名称和修改前的名称一样。
                for (int i = 0; i < total; i++)
                {
                    if (m_rolesModel.name.ToString().Equals(collection["rolesName"].Trim().ToString()))
                    {
                        rolesList.Remove(m_rolesModel.name);
                    }
                }
                string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);
                m_rolesModel.name = collection["rolesName"].Trim();
                if (m_ri_total == 1)
                {
                    m_rolesModel.invalid = false;
                }
                if (m_ri_total == 0)
                {
                    m_rolesModel.invalid = true;
                }
                //m_rolesModel.invalid = Convert.ToBoolean(collection["InvalidParent"].Trim());
                m_rolesModel.deleted = Convert.ToBoolean(collection["rolesDeleted"].Trim());
                m_rolesModel.remark = collection["rolesRemark"].Trim();
                m_rolesModel.app_id = Convert.ToInt32(collection["rolesApp_id"].Trim());
                m_rolesModel.updated_at = t;
                m_rolesModel.updated_by = Convert.ToInt32(m_usersModel.id);
                m_rolesModel.updated_ip = collection["rolesCreated_ip"].Trim();
                foreach (string rolesname in rolesList)
                {
                    if (rolesname.Equals(collection["rolesName"].Trim()))
                    {
                      
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的角色名称!'}");
                    }
                }
                try
                {
                    if (m_rolesBllService.Update(m_rolesModel, out msg))
                    {
                      
                        return Json("{success:true,css:'alert alert-success',message:'修改角色成功!'}");
                    }
                    else
                    {
                       
                        return Json("{success:false,css:'alert alert-error',message:'修改角色失败!'}");
                    }
                }
                catch (Exception ex)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
           
          
        }
        
        //角色的详细信息    
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                m_rolesModel = m_rolesBllService.GetModel(id, out msg);
                ViewData["rolesName"] = m_rolesModel.name;
                ViewData["rolesRemark"] = m_rolesModel.remark;
                if (m_rolesModel.invalid == true)
                {
                    ViewData["rolesInvalid"] ="否";
                }
                if (m_rolesModel.invalid == false)
                {
                    ViewData["rolesInvalid"] = "是";
                }
               
      
                ViewData["rolesCreated_at"] = m_rolesModel.created_at;
                ViewData["rolesCreated_by"] = m_usersModel.login;
                ViewData["rolesCreated_ip"] = m_rolesModel.created_ip;
                ViewData["rolesUpdated_at"] = m_rolesModel.updated_at;
                ViewData["rolesUpdated_by"] =m_usersModel.login;
                ViewData["rolesUpdated_ip"] = m_rolesModel.updated_ip;
                ViewData["rolesApp_id"] = m_rolesModel.app_id;
                return View();
            }
           
        }

        public ActionResult GetRoles_Apply()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                int appID = Convert.ToInt32(m_usersModel.app_id);
                DataSet ds = m_rolesBllService.GetAllRolesListOfApp(appID, out msg);
                string data = "{Rows:[";
                if (ds == null)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "无权访问WebService！" });
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = ds.Tables[0].Rows[i][1].ToString();
                            string id = ds.Tables[0].Rows[i][0].ToString();
                            string remark = ds.Tables[0].Rows[i][2].ToString();
                            if (i == ds.Tables[0].Rows.Count - 1)
                            {
                                data += "{name:'" + name + "',";
                                data += "id:'" + id + "',";
                                data += "remark:'" + remark + "'}";
                            }
                            else
                            {
                                data += "{name:'" + name + "',";
                                data += "id:'" + id + "',";
                                data += "remark:'" + remark + "'},";
                            }
                        }
                    }
                    catch (Exception ex) { }
                    data += "]}";
                    return Json(data);
                }
            }
        }

        
        // 后台分页方式获得角色的信息列表      
        public ActionResult GetRolesList()
        {
            if(Session["user"]==null)
            {
              return RedirectToAction("Home","Login");
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
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue = m_SecurityContext;

                int appID = Convert.ToInt32(m_usersModel.app_id);
                DataSet ds = m_rolesBllService.GetAllRolesListOfApp(appID, out msg);
                if (ds == null)
                {
                    //return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "无权访问WebService！" });
                    return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                }
                IList<WorkFlow.RolesWebService.rolesModel> m_list = new List<WorkFlow.RolesWebService.rolesModel>();

                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.RolesWebService.rolesModel m_rolesModel = (WorkFlow.RolesWebService.rolesModel)Activator.CreateInstance(typeof(WorkFlow.RolesWebService.rolesModel));
                    PropertyInfo[] m_propertys = m_rolesModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            {
                                // 数据库NULL值单独处理 
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(m_rolesModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(m_rolesModel, null, null);
                                break;
                            }
                        }
                    }
                    m_list.Add(m_rolesModel);
                }

                IList<WorkFlow.RolesWebService.rolesModel> m_targetList = new List<WorkFlow.RolesWebService.rolesModel>();
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

       // 获取操作类型的权限列表
        public ActionResult GetOperationsPrivilegeList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_roleID = Convert.ToInt32(Request.Params["roleID"]);//角色ID

                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";
                WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();
                WorkFlow.Privileges_RoleWebService.SecurityContext m_PR_securityContext = new Privileges_RoleWebService.SecurityContext();

                string msg = string.Empty;

                #region webservice方法授权
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                m_PR_securityContext.UserName = m_usersModel.login;
                m_PR_securityContext.PassWord = m_usersModel.password;
                m_PR_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilege_roleBllService.SecurityContextValue = m_PR_securityContext;//实例化 [SoapHeader("m_securityContext")]
                #endregion

                DataSet ds = new DataSet();
                try
                {
                    ds = m_privilegesBllService.GetListByPrivilegeType(3, (int)m_usersModel.app_id, out msg);
                }
                catch (Exception ex)
                {
                }
                int total = ds.Tables[0].Rows.Count;
                for (int i = 0; i < total; i++)
                {
                    int m_privilegeID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    string m_privilegeName = ds.Tables[0].Rows[i][1].ToString();
                    string m_selected = string.Empty;

                    //判断角色中是否已经存在该权限
                    if (m_privilege_roleBllService.Exists(m_roleID, m_privilegeID,out msg))
                    {
                        m_selected = "true";
                    }
                    else
                    {
                        m_selected = "false";
                    }

                    if (i < total - 1)
                    {
                        strJson += "{id:'" + m_privilegeID + "',";
                        strJson += "name:'" + m_privilegeName + "',";
                        strJson += "selected:'" + m_selected + "'},";
                    }
                    else
                    {
                        strJson += "{id:'" + m_privilegeID + "',";
                        strJson += "name:'" + m_privilegeName + "',";
                        strJson += "selected:'" + m_selected + "'}";
                    }
                }
                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
          
        }

        //获取菜单类型的权限列表
        public ActionResult GetMunusPrivilegeList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_roleID = Convert.ToInt32(Request.Params["roleID"]);//角色ID

                string strJson = "{List:[";
                WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();
                WorkFlow.Privileges_RoleWebService.SecurityContext m_PR_securityContext = new Privileges_RoleWebService.SecurityContext();

                string msg = string.Empty;

                #region webservice方法授权
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                m_PR_securityContext.UserName = m_usersModel.login;
                m_PR_securityContext.PassWord = m_usersModel.password;
                m_PR_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilege_roleBllService.SecurityContextValue = m_PR_securityContext;//实例化 [SoapHeader("m_securityContext")]
                #endregion

                DataSet ds = new DataSet();
                try
                {
                    ds = m_privilegesBllService.GetListByPrivilegeType(1, (int)m_usersModel.app_id, out msg);
                }
                catch (Exception ex)
                {
                }

                int total = ds.Tables[0].Rows.Count;//菜单类型的权限数量

                for (int i = 0; i < total; i++)
                {
                    int m_privilegeID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    string m_privilegeName = ds.Tables[0].Rows[i][1].ToString();
                    string m_selected = string.Empty;

                    //判断角色中是否已经存在该权限
                    if (m_privilege_roleBllService.Exists(m_roleID, m_privilegeID,out msg))
                    {
                        m_selected = "true";
                    }
                    else
                    {
                        m_selected = "false";
                    }

                    if (i < total - 1)
                    {
                        strJson += "{id:'" + m_privilegeID + "',";
                        strJson += "name:'" + m_privilegeName + "',";
                        strJson += "selected:'" + m_selected + "'},";
                    }
                    else
                    {
                        strJson += "{id:'" + m_privilegeID + "',";
                        strJson += "name:'" + m_privilegeName + "',";
                        strJson += "selected:'" + m_selected + "'}";
                    }
                }
                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
           
        }

        //编辑角色权限
        public ActionResult AddRolePrivileges()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_rmp_total = Convert.ToInt32(Request.Params["mpTotal"]);//角色菜单、页面元素权限数量
                int m_rop_total = Convert.ToInt32(Request.Params["opTotal"]);//角色操作权限数量
                int m_roleID = Convert.ToInt32(Request.Params["r_ID"]);//角色ID

                WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
                WorkFlow.Privileges_RoleWebService.privilege_roleModel m_privilege_roleModel = new Privileges_RoleWebService.privilege_roleModel();

                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllservice = new PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.Privileges_RoleWebService.SecurityContext m_PR_securityContext = new Privileges_RoleWebService.SecurityContext();
                WorkFlow.PrivilegesWebService.SecurityContext m_p_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                #region webservice方法授权验证
                
                //SecurityContext实体对象赋值
                m_PR_securityContext.UserName = m_usersModel.login;
                m_PR_securityContext.PassWord = m_usersModel.password;
                m_PR_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilege_roleBllService.SecurityContextValue = m_PR_securityContext;//实例化 [SoapHeader("m_securityContext")]

                m_p_securityContext.UserName = m_usersModel.login;
                m_p_securityContext.PassWord = m_usersModel.password;
                m_p_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllservice.SecurityContextValue = m_p_securityContext;//实例化 [SoapHeader("m_securityContext")]
                
                #endregion

                try
                {
                    if (m_privilege_roleBllService.DeleteByRoleID(m_roleID, out msg))//删除角色下的权限
                    {
                        m_privilege_roleModel.role_id = m_roleID;
                        //角色-菜单、页面元素权限
                        for (int i = 0; i < m_rmp_total; i++)
                        {
                            int m_mprivilegeID = Convert.ToInt32(Request.Params[("rmprivilegeID" + i)]);

                            int m_parentMenuID = Convert.ToInt32(Request.Params[("parentID" + i)]);
                            
                            //如果菜单权限对应的菜单有父ID（m_parentMenuID不等于0），先判断角色权限列表中角色是否拥有该父菜单的权限
                            //若角色没有父菜单的权限，首先要添加父菜单的角色权限
                            if (m_parentMenuID != 0)
                            {
                                int m_menuprivilegeID = m_privilegesBllservice.GetMenuPrivilegeIDByMenuID(m_parentMenuID,out msg);
                                if (!m_privilege_roleBllService.Exists(m_roleID, m_menuprivilegeID, out msg))
                                {
                                    m_privilege_roleModel.privilege_id = m_menuprivilegeID;//父菜单权限ID
                                    m_privilege_roleBllService.Add(m_privilege_roleModel,out msg);//先给角色添加父菜单权限
                                }
                            }

                            m_privilege_roleModel.privilege_id = m_mprivilegeID;//菜单权限ID
                            if (!m_privilege_roleBllService.Add(m_privilege_roleModel,out msg))
                            {
                              //  return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "修改失败！" });
                                return Json("{success:false,css:'alert alert-error',message:'修改失败!'}");
                            }
                        }

                        //角色-操作权限
                        for (int i = 0; i < m_rop_total; i++)
                        {
                            int m_oprivilegeID = Convert.ToInt32(Request.Params[("roprivilegeID" + i)]);
                            m_privilege_roleModel.role_id = m_roleID;
                            m_privilege_roleModel.privilege_id = m_oprivilegeID;
                            if (!m_privilege_roleBllService.Add(m_privilege_roleModel,out msg))
                            {
                              //  return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "修改失败！" });
                                return Json("{success:false,css:'alert alert-error',message:'修改失败！'}");
                            }
                        }
                    }
                    else
                    {
                      //  return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "修改失败！" });
                        return Json("{success:false,css:'alert alert-error',message:'修改失败!'}");
                    }
                }
                catch (Exception ex)
                {
                  //  return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错！" });
                    return Json("{success:false,css:'alert alert-error',message:'程序出错!'}");
                }

               // return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功！", toUrl = "/RolesManagement/Role_Privileges" });
                return Json("{success:true,css:'alert alert-success',message:'修改成功!'}");
            }
    
       
        }

        //获得菜单、页面元素权限--Tree控件menusList
        public ActionResult GetMenuPrivilegeTree()
        {
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
            WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象
            
            WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();
            WorkFlow.Privileges_RoleWebService.SecurityContext m_PR_securityContext = new Privileges_RoleWebService.SecurityContext();

            string msg = string.Empty;

            #region webservice方法授权验证
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            m_PR_securityContext.UserName = m_usersModel.login;
            m_PR_securityContext.PassWord = m_usersModel.password;
            m_PR_securityContext.AppID = (int)m_usersModel.app_id;
            m_privilege_roleBllService.SecurityContextValue = m_PR_securityContext;//实例化 [SoapHeader("m_securityContext")]
            #endregion

            int roleID = Convert.ToInt32(Request.Params["roleID"]);//角色ID

            DataSet ds = new DataSet();
            DataSet rp_ds = new DataSet();
            try
            {
                ds = m_privilegesBllService.GetMenuAndElementPrivilegeListOfApp((int)m_usersModel.app_id, out msg);
                rp_ds = m_privilegesBllService.GetMenuAndElementPrivilegeListOfRole(roleID, out msg);
            }
            catch (Exception ex)
            {
                return Json("{success:false,message:'" + msg + "'}");
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                return Json("[]");
            }

            string datajson = "[";
            int total = ds.Tables[0].Rows.Count;
            int rp_total = rp_ds.Tables[0].Rows.Count;
            for (int i = 0; i <total; i++)
            {
                string m_privilegeID = ds.Tables[0].Rows[i][0].ToString();//权限ID
                string m_privilegeName = ds.Tables[0].Rows[i][1].ToString();//权限名称
                string m_privilegeTypeID = ds.Tables[0].Rows[i][2].ToString();//权限类型ID
                string m_itemID =ds.Tables[0].Rows[i][5].ToString();//菜单ID
                string m_parentID = ds.Tables[0].Rows[i][6].ToString();//父菜单ID
                string m_realparentID = ds.Tables[0].Rows[i][8].ToString();//父菜单ID

                if (i < total - 1)
                {
                    datajson += "{id:'" + m_privilegeID + "',";
                    datajson += "name:'" + m_privilegeName + "',";
                    datajson += "pt_id:'" + m_privilegeTypeID + "',";
                    datajson += "real_parentID:'" + m_realparentID + "',";
                    datajson += "menuID:'" + m_itemID + "',";
                    datajson += "parentID:'" + m_parentID + "',";

                    if(rp_total==0)
                    {
                        datajson += "ischecked:false},";
                    }

                    for (int j = 0; j < rp_total; j++)
                    {
                        if (rp_ds.Tables[0].Rows[j][0].ToString() == m_privilegeID)
                        {
                            datajson += "ischecked:true},";
                            break;
                        }

                        if (j == rp_total - 1)
                        {
                            datajson += "ischecked:false},";
                        }
                    }
                }
                else
                {
                    datajson += "{id:'" + m_privilegeID + "',";
                    datajson += "name:'" + m_privilegeName + "',";
                    datajson += "pt_id:'" + m_privilegeTypeID + "',";
                    datajson += "real_parentID:'" + m_realparentID + "',";
                    datajson += "menuID:'" + m_itemID + "',";
                    datajson += "parentID:'" + m_parentID + "',";

                    if (rp_total == 0)
                    {
                        datajson += "ischecked:false}]";
                    }

                    for (int j = 0; j < rp_total; j++)
                    {
                        if (rp_ds.Tables[0].Rows[j][0].ToString() == m_privilegeID)
                        {
                            datajson += "ischecked:true}]";
                            break;
                        }

                        if (j == rp_total - 1)
                        {
                            datajson += "ischecked:false}]";
                        }
                    }
                }
            }

            return Json(datajson);
        }

        //角色名称的模糊查询
        public ActionResult GetListByRoleName(string roleName)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
                WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_rolesBllService.SecurityContextValue=m_SecurityContext;

                //如果搜索的字段为空，则显示全部角色列表
                if (roleName.Length == 0)
                {
                    DataSet ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id,out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {
                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService!'}");
                    }
                    else
                    {
                        try
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string remark = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'},";
                                }
                            }
                        }
                        catch (Exception ex) { }
                        data += "]}";
                        return Json(data);
                    }
                }
                //如果搜索的字段不为空，则根据搜索字段显示部分角色列表
                else 
                {
                    DataSet ds = m_rolesBllService.GetListByRoleName(roleName,(int)m_usersModel.app_id,out msg);
                    String data = "{Rows:[";
                   
                        try
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string remark = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "remark:'" + remark + "'},";
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
