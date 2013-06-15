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
        public ActionResult EditPageCon()
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

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="collection">表单数据</param>
        /// <returns>成功,返回主页面</returns>
        public ActionResult AddRoles(FormCollection collection)
        {
            string msg = string.Empty;
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
            
            
            string m_rolesName = collection["rolesName"].Trim();
            if (m_rolesName.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "角色名称不能为空??！" });
            }
            m_rolesModel.name = collection["rolesName"].Trim();
            //获得deleted=false的rolesName列表
            DataSet ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id,out msg);
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
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的角色名称!" });
                }
            }
            m_rolesModel.name = collection["rolesName"].Trim();
            m_rolesModel.invalid = Convert.ToBoolean(collection["rolesInvalid"].Trim());//String转化为Boolean
            m_rolesModel.deleted = Convert.ToBoolean(collection["rolesDeleted"].Trim());//String转化为Boolean
            m_rolesModel.app_id = Convert.ToInt32(collection["rolesApp_id"].Trim());
            m_rolesModel.created_at = Convert.ToDateTime(collection["rolesCreated_at"].Trim());
            m_rolesModel.created_by = Convert.ToInt32(collection["rolesCreated_by"].Trim());
            m_rolesModel.created_ip = collection["rolesCreated_ip"].Trim();
            m_rolesModel.remark = collection["rolesRemark"].Trim();

        
            try
            {
                if (m_rolesBllService.Add(m_rolesModel, out msg) != 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "角色添加成功!" + msg, toUrl = "/RolesManagement/AppRoles" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="角色添加失败!"});
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序异常!"});
            }
           
            //return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功！"+msg,  });
        }
        
        /// <summary>
        /// 删除一条内容为系统好为id的信息
        /// </summary>
        /// <param name="id">系统id号</param>
        /// <returns></returns>
        public ActionResult ChangePage(int id)
        {
            string msg = string.Empty;
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

            if (m_rolesBllService.Delete(id, out msg))
            {
               
                return RedirectToAction("AppRoles");
            }
            else
            {
                return View();
            }
        }
        
        /// <summary>
        /// 获取ID的数据表详情
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>
        public ActionResult EditPage(int id)
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

        public ActionResult GetInvalidName()
        {
            string[] invalidI=new string[2];
            invalidI[0] = "false";
            invalidI[1] = "true";

            string[] invalidN = new string[2];
            invalidN[0] = "是";
            invalidN[1] = "否";

            List<Saron.WorkFlow.Models.InvalidHelper> m_invalidlist=new List<Saron.WorkFlow.Models.InvalidHelper>();
            for (int i = 0; i < 2; i++)
            {
                m_invalidlist.Add(new Saron.WorkFlow.Models.InvalidHelper { InvalidID = invalidI[i].ToString(), InvalidName = invalidN[i].ToString() });
            }
            var dataJson = new
            {
                Rows = m_invalidlist,
                Total = 2
            };
            return Json(dataJson,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改数据库中的信息
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>
        /// <summary>
        public ActionResult EditRoles(FormCollection collection)
        {
            string msg = string.Empty;
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
            WorkFlow.RolesWebService.SecurityContext m_SecurityContext = new RolesWebService.SecurityContext();


            WorkFlow.RolesWebService.rolesModel m_roleModel=new WorkFlow.RolesWebService.rolesModel();

            WorkFlow.UsersWebService.usersBLLservice m_usersBllService = new UsersWebService.usersBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

            m_SecurityContext.UserName = m_usersModel.login;
            m_SecurityContext.PassWord = m_usersModel.password;
            m_SecurityContext.AppID = (int)m_usersModel.app_id;
            m_rolesBllService.SecurityContextValue = m_SecurityContext;

            int id = Convert.ToInt32(collection["rolesId"].Trim());
            m_rolesModel = m_rolesBllService.GetModel(id,out msg);

            string name = collection["rolesName"].Trim();
            string invalid = collection["InvalidParent"].Trim();
            if (name.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "角色名称不能为空！" });
            }
            if (invalid.Length == 0 || invalid.Equals("请选择"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="是否有效不能为空!"});
            }
            //获得deleted=false且应用系统ID为appid的rolesName列表
            DataSet ds = m_rolesBllService.GetAllRolesListOfApp((int)m_usersModel.app_id,out msg);
            var total = ds.Tables[0].Rows.Count;
            ArrayList rolesList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                rolesList.Add(ds.Tables[0].Rows[i][1].ToString());
            }
            //如果是自己本身，角色名称修改后的名称和修改前的名称一样。
            for (int i = 0; i < total; i++)
            { 
               if(m_rolesModel.name.ToString().Equals(collection["rolesName"].Trim().ToString()))
               {
                   rolesList.Remove(m_rolesModel.name);
               }
            }
            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);
            //WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            m_rolesModel.name = collection["rolesName"].Trim();
            m_rolesModel.invalid = Convert.ToBoolean(collection["InvalidParent"].Trim());
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
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的角色名称!" });
                }
            }
            try
            {
                if (m_rolesBllService.Update(m_rolesModel,out msg))
                {
                    m_roleModel = m_rolesBllService.GetModel(id,out msg);
                    Session["role"] = m_roleModel.name;
                    //m_usersModel = m_usersBllService.GetModelByID(Convert.ToInt32(collection["rolesId"].Trim()));
                    //Session["user"] = m_usersModel.login;
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改角色成功！", toUrl = "/RolesManagement/EditPageCon" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="修改角色失败!"});                  
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序异常!"});
            }
          
        }
        
        /// <summary>
        /// 显示所选系统的详情
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>     
        public ActionResult DetailInfo(int id)
        {
            string msg = string.Empty;
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

            m_rolesModel = m_rolesBllService.GetModel(id,out msg);
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
        
        /// <summary>
        /// 显示系统的详细的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoles_Apply()
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
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);
            DataSet ds = m_rolesBllService.GetAllRolesListOfApp(appID,out msg);
            if (ds == null)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="无权访问WebService！"});
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

            //模拟排序操作
            if (sortorder == "desc")
                m_list = m_list.OrderByDescending(c => c.id).ToList();
            else
                m_list = m_list.OrderBy(c => c.id).ToList();

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


        //获取操作类型的权限列表
        public ActionResult GetOperationsPrivilegeList()
        {
            int m_roleID = Convert.ToInt32(Request.Params["roleID"]);//角色ID

            string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";
            WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            DataSet ds = new DataSet();
            try
            {
                ds=m_privilegesBllService.GetListByPrivilegeType(3, (int)m_userModel.app_id);
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
                if (m_privilege_roleBllService.Exists(m_roleID, m_privilegeID))
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
            strJson += "],total:'"+total+"'}";
            return Json(strJson);
        }

        //获取菜单类型的权限列表
        public ActionResult GetMunusPrivilegeList()
        {
            int m_roleID = Convert.ToInt32(Request.Params["roleID"]);//角色ID
            
            string strJson = "{List:[";
            WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            DataSet ds = new DataSet();
            try
            {
                ds = m_privilegesBllService.GetListByPrivilegeType(1, (int)m_userModel.app_id);
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
                if (m_privilege_roleBllService.Exists(m_roleID, m_privilegeID))
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

        //获取页面元素类型的权限列表
        public ActionResult GetElementsPrivilegeList()
        {
            int m_roleID = Convert.ToInt32(Request.Params["roleID"]);//角色ID

            string strJson = "{List:[";
            WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            DataSet ds = new DataSet();
            try
            {
                ds = m_privilegesBllService.GetListByPrivilegeType(2, (int)m_userModel.app_id);
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
                if (m_privilege_roleBllService.Exists(m_roleID, m_privilegeID))
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

        //修改角色权限
        public ActionResult AddRolePrivileges()
        {
            int m_rp_total = Convert.ToInt32(Request.Params["rp_total"]);//角色权限数量
            int m_roleID = Convert.ToInt32(Request.Params["r_ID"]);//角色ID
            WorkFlow.Privileges_RoleWebService.privilege_roleBLLservice m_privilege_roleBllService = new Privileges_RoleWebService.privilege_roleBLLservice();
            WorkFlow.Privileges_RoleWebService.privilege_roleModel m_privilege_roleModel = new Privileges_RoleWebService.privilege_roleModel();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            try
            {
                if (m_privilege_roleBllService.DeleteByRoleID(m_roleID))//删除角色下的权限
                {
                    for (int i = 0; i < m_rp_total; i++)
                    {
                        int m_privilegeID = Convert.ToInt32(Request.Params[("rprivilegeID" + i)]);
                        m_privilege_roleModel.role_id = m_roleID;
                        m_privilege_roleModel.privilege_id = m_privilegeID;
                        if (!m_privilege_roleBllService.Add(m_privilege_roleModel))
                        {
                            return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "修改失败！" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错！" });
            }

            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功！", toUrl = "/RolesManagement/Role_Privileges" });
        }
    }
}
