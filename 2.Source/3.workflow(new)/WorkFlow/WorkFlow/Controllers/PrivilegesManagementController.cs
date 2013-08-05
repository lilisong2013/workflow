using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;
using System.Reflection;

namespace WorkFlow.Controllers
{
    public class PrivilegesManagementController : Controller
    {
        //
        // GET: /PrivilegesManagement/

        public ActionResult AppPrivileges()
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


        //添加操作权限
        public ActionResult AddPrivilegesOfOperations()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = new WorkFlow.PrivilegesWebService.privilegesModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]


                string m_privilegeName = Request.Form["oPrivilegesName"].ToString();

                //权限名称是否存在
                if (m_privilegesBllService.ExistsPrivilegeName(m_privilegeName, (int)m_usersModel.app_id,3,out msg))
                {
                    // return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统中权限名称已经存在！" });
                   return Json("{success:false,css:'alert alert-error',message:'系统中权限名称已经存在！'}");
                }

                m_privilegesModel.name = m_privilegeName;//权限名称
                m_privilegesModel.privilegetype_id = 3;//权限类型ID
                m_privilegesModel.privilegeitem_id = Convert.ToInt32(Request.Form["oPrivilegesItem"].ToString());
                m_privilegesModel.remark = Request.Form["oPrivilegesRemark"].ToString();
                m_privilegesModel.app_id = (int)m_usersModel.app_id;//系统ID
                m_privilegesModel.created_at = DateTime.Now;
                m_privilegesModel.created_by = m_usersModel.id;
                m_privilegesModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();

                try
                {
                    if (m_privilegesBllService.Add(m_privilegesModel, out msg) != 0)
                    {
                      return Json("{success:true,css:'alert alert-success',message:'添加权限成功！'}");
                       // return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加权限成功！" });
                    }
                    else
                    {
                       return Json("{success:false,css:'alert alert-error',message:'添加权限失败！'}");
                        //  return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "添加权限失败！" });
                    }
                }
                catch (Exception ex)
                {
                    // return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错！" });
                  return Json("{success:false,css:'alert alert-error',message:'程序出错！'}");
                  
                }
            }
           

        }

        //添加菜单权限
        public ActionResult AddPrivilegesOfMenus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = new WorkFlow.PrivilegesWebService.privilegesModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                string m_privilegeName = Request.Form["mPrivilegesName"].ToString();

                //权限名称是否存在
                if (m_privilegesBllService.ExistsPrivilegeName(m_privilegeName, (int)m_usersModel.app_id,1,out msg))
                {
              
                    return Json("{success:false,css:'alert alert-error',message:'系统中权限名称已经存在！'}");
                }

                m_privilegesModel.name = m_privilegeName;//权限名称
                m_privilegesModel.privilegetype_id = 1;//权限类型ID
                m_privilegesModel.privilegeitem_id = Convert.ToInt32(Request.Form["mPrivilegesItem"].ToString());
                m_privilegesModel.remark = Request.Form["mPrivilegesRemark"].ToString();
                m_privilegesModel.app_id = (int)m_usersModel.app_id;//系统ID
                m_privilegesModel.created_at = DateTime.Now;
                m_privilegesModel.created_by = m_usersModel.id;
                m_privilegesModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();

                try
                {
                    if (m_privilegesBllService.Add(m_privilegesModel, out msg) != 0)
                    {
                      
                        return Json("{success:true,css:'alert alert-success',message:'添加权限成功！'}");
                    }
                    else
                    {
                     
                        return Json("{success:false,css:'alert alert-error',message:'添加权限失败！'}");
                    }
                }
                catch (Exception ex)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'程序出错！'}");
                }

            }
          
        }

        //添加页面元素权限
        public ActionResult AddPrivilegesOfElements()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = new WorkFlow.PrivilegesWebService.privilegesModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                string m_privilegeName = Request.Form["ePrivilegesName"].ToString();

                //权限名称是否存在
                if (m_privilegesBllService.ExistsPrivilegeName(m_privilegeName, (int)m_usersModel.app_id,2,out msg))
                {
                
                    return Json("{success:false,css:'alert alert-error',message:'系统中权限名称已经存在！'}");
                }

                m_privilegesModel.name = m_privilegeName;//权限名称
                m_privilegesModel.privilegetype_id = 2;//权限类型ID
                m_privilegesModel.privilegeitem_id = Convert.ToInt32(Request.Form["ePrivilegesItem"].ToString());
                m_privilegesModel.remark = Request.Form["ePrivilegesRemark"].ToString();
                m_privilegesModel.app_id = (int)m_usersModel.app_id;//系统ID
                m_privilegesModel.created_at = DateTime.Now;
                m_privilegesModel.created_by = m_usersModel.id;
                m_privilegesModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();

                try
                {
                    if (m_privilegesBllService.Add(m_privilegesModel, out msg) != 0)
                    {
                   
                        return Json("{success:true,css:'alert alert-success',message:'添加权限成功！'}");
                    }
                    else
                    {
         
                        return Json("{success:false,css:'alert alert-error',message:'添加权限失败！'}");
                    }
                }
                catch (Exception ex)
                {
    
                    return Json("{success:false,css:'alert alert-error',message:'程序出错！'}");
                }

            }
           
        }

        //获取权限类型的下拉列表
        public ActionResult GetPrivilegesType()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                WorkFlow.Privileges_TypeWebService.privileges_typeBLLservice m_privilegesTypeBllService = new WorkFlow.Privileges_TypeWebService.privileges_typeBLLservice();

                DataSet ds = new DataSet();
                try
                {
                    ds = m_privilegesTypeBllService.GetAllPrivileges_typeList();
                }
                catch (Exception ex)
                {
                }

                List<WorkFlow.Privileges_TypeWebService.privileges_typeModel> m_privilegesTypeModelList = new List<WorkFlow.Privileges_TypeWebService.privileges_typeModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    WorkFlow.Privileges_TypeWebService.privileges_typeModel m_privilegesTypeModel = new WorkFlow.Privileges_TypeWebService.privileges_typeModel();
                    m_privilegesTypeModel.id = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    m_privilegesTypeModel.name = ds.Tables[0].Rows[i][1].ToString();
                    m_privilegesTypeModel.code = ds.Tables[0].Rows[i][2].ToString();
                    m_privilegesTypeModelList.Add(m_privilegesTypeModel);
                }

                var dataJson = new
                {
                    Rows = m_privilegesTypeModelList,
                    Total = ds.Tables[0].Rows.Count
                };
                return Json(dataJson);
            }
          

        }

        //权限项目列表：菜单
        public ActionResult GetMenusOfItem()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new WorkFlow.MenusWebService.menusBLLservice();
                WorkFlow.MenusWebService.menusModel m_menusModel = new WorkFlow.MenusWebService.menusModel();
                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                string data = "[";
                try
                {
                    DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_usersModel.app_id, out msg);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        string id = ds.Tables[0].Rows[i][0].ToString();
                        string code = ds.Tables[0].Rows[i][2].ToString();
                        string url = ds.Tables[0].Rows[i][3].ToString();
                        string parent_id = ds.Tables[0].Rows[i][5].ToString();
                        string remark = ds.Tables[0].Rows[i][6].ToString();
                        string invalid = ds.Tables[0].Rows[i][7].ToString();
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                            data += "code:'" + code + "',";
                            data += "url:'" + url + "',";
                            data += "parent_id:'" + parent_id + "',";
                            data += "invalid:'" + invalid + "',";
                            data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "}";
                        }
                        else
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                            data += "code:'" + code + "',";
                            data += "url:'" + url + "',";
                            data += "parent_id:'" + parent_id + "',";
                            data += "invalid:'" + invalid + "',";
                            data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                data += "]";
                return Json(data);
            }
          
        }
        //菜单
        public string GetChildrenMenusList(int parentId)
        {
            string dataStr = ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new WorkFlow.MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new WorkFlow.MenusWebService.menusModel();

            WorkFlow.UsersWebService.usersModel m_usersModel = (UsersWebService.usersModel)Session["user"];

            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

            string msg = string.Empty;

            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            if (m_menusBllService.ExistsChildrenMenus(parentId,out msg))
            {
                DataSet ds = m_menusBllService.GetChildrenMenus(parentId,out msg);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string code = ds.Tables[0].Rows[i][2].ToString();
                    string url = ds.Tables[0].Rows[i][3].ToString();
                    string parent_id = ds.Tables[0].Rows[i][5].ToString();
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    string invalid = ds.Tables[0].Rows[i][7].ToString();
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0],out msg))
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "parent_id:'" + parent_id + "',";
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "parent_id:'" + parent_id + "',";
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                    }
                    else
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "parent_id:'" + parent_id + "',";
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "parent_id:'" + parent_id + "',";
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'},";
                        }
                    }
                }
            }
            dataStr += "]";
            return dataStr;
        }
        
        //权限项目列表：操作
        public ActionResult GetOperationOfItem()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
                WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;
                WorkFlow.OperationsWebService.SecurityContext m_SecurityContext = new OperationsWebService.SecurityContext();

                m_SecurityContext.UserName = m_userModel.login;
                m_SecurityContext.PassWord = m_userModel.password;
                m_SecurityContext.AppID = (int)m_userModel.app_id;
                m_operationsBllService.SecurityContextValue = m_SecurityContext;

                string data = "{Rows:[";
                try
                {
                    DataSet ds = m_operationsBllService.GetOperationsListOfApp((int)m_userModel.app_id, out msg);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string id = ds.Tables[0].Rows[i][0].ToString();
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        string code = ds.Tables[0].Rows[i][2].ToString();
                        string description = ds.Tables[0].Rows[i][3].ToString();
                        string remark = ds.Tables[0].Rows[i][4].ToString();
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "description:'" + description + "',";
                            data += "remark:''}";
                        }
                        else
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "description:'" + description + "',";
                            data += "remark:''},";
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
        
        //权限项目列表：元素
        public ActionResult GetElementOfItem()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int menusID = Convert.ToInt32(Request.Params["menusID"]);//菜单ID
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new WorkFlow.ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                string data = "{Rows:[";
                try
                {
                    DataSet ds = m_elementsBllService.GetElementsListOfMenus(menusID, out msg);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string id = ds.Tables[0].Rows[i][0].ToString();
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        string code = ds.Tables[0].Rows[i][2].ToString();
                        string remark = ds.Tables[0].Rows[i][3].ToString();
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "remark:''}";
                        }
                        else
                        {
                            data += "{name:'" + name + "',";
                            data += "id:'" + id + "',";
                            data += "code:'" + code + "',";
                            data += "remark:''},";
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

        //判断项目(操作)是否已经创建权限
        public ActionResult ExistPrivilegeItemOfOperations()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int privilegeTypeID = 3;//权限类型ID
                int privilegeItemID = Convert.ToInt32(Request.Params["operationsID"]);//权限项目ID

                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                if (m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, privilegeItemID, out msg))
                {
                   // return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "该项目已设置权限！" });
                    return Json("{success:false,css:'alert alert-error',message:'该项目已设置权限！'}");
                }
                else
                {
                 // return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
                    return Json("{success:true}");         
                }
            }
        
        }
        
        //判断项目(菜单)是否已经创建权限
        public ActionResult ExistPrivilegeItemOfMenus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int privilegeTypeID = 1;//权限类型ID
                int privilegeItemID = Convert.ToInt32(Request.Params["menusID"]);//权限项目ID
                int menuparentID = 0;//父菜单ID
                if (Request.Params["parentID"] != "")
                {
                    menuparentID = Convert.ToInt32(Request.Params["parentID"]);//父菜单ID
                }

                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                #region webservice授权
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
                #endregion

                //菜单存在父菜单时，判断父菜单是否创建权限
                if(menuparentID!=0)
                {
                    if (!m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, menuparentID, out msg))
                    {
                        return Json("{success:false,css:'alert alert-error',message:'该菜单的父菜单尚未设置权限！'}");
                    }
                }

                if (m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, privilegeItemID, out msg))
                {
                  //  return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "该项目已设置权限！" });
                    return Json("{success:false,css:'alert alert-error',message:'该项目已设置权限！'}");
                }
                else
                {
                   // return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
                    return Json("{success:true}");
                }
            }
            
        }
       
        //判断项目(页面元素)是否已经创建权限
        public ActionResult ExistPrivilegeItemOfElements()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int privilegeTypeID = 2;//权限类型ID
                int privilegeItemID = Convert.ToInt32(Request.Params["elementID"]);//权限项目ID

                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                if (m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, privilegeItemID, out msg))
                {
                   // return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "该项目已设置权限！" });
                    return Json("{success:false,css:'alert alert-error',message:'该项目已设置权限！'}");
                }
                else
                {
                   // return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
                    return Json("{success:true}");
                }
            }
           
        }

        //判断菜单是否有子菜单
        public ActionResult ExistChildreMenus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int menusID = Convert.ToInt32(Request.Params["menusID"]);//菜单ID
                WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (UsersWebService.usersModel)Session["user"];
                WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                if (menusID == -1)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false });
                }

                try
                {
                    if (!m_menusBllService.ExistsChildrenMenus(menusID, out msg))
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = false });
                    }
                    else
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false });
                }
            }
           
            
        }

       //菜单列表(后台分页)
        public ActionResult GetMPrivilegesList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
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
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_SecurityContext;
    
                DataSet ds = m_privilegesBllService.GetMListByAppTypeID((int)m_usersModel.app_id,out msg);
                IList<WorkFlow.PrivilegesWebService.privilegesModel> m_list=new List<WorkFlow.PrivilegesWebService.privilegesModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = (WorkFlow.PrivilegesWebService.privilegesModel)Activator.CreateInstance(typeof(WorkFlow.PrivilegesWebService.privilegesModel));
                    PropertyInfo[] m_propertys = m_privilegesModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {

                                // 属性与字段名称一致的进行赋值 
                                if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                                {
                                
                                    //数据库NULL值单独处理
                                    if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                        pi.SetValue(m_privilegesModel, ds.Tables[0].Rows[i][j], null);
                                    else
                                        pi.SetValue(m_privilegesModel, null, null);
                                    break;
                                }
                           
                          

                        }
                    }
                    m_list.Add(m_privilegesModel);
                }
                IList<WorkFlow.PrivilegesWebService.privilegesModel> m_targetList = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
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
                    Total = total
                };
                return Json(gridData);
            }
        }
       //测试列表(后台分页)
        //public ActionResult GetTPrivilegesList()
        //{
        //    if (Session["user"] == null)
        //    {
        //        return RedirectToAction("Home", "Login");
        //    }
        //    else
        //    {
        //        //排序的字段名
        //        string sortname = Request.Params["sortname"];
        //        //排序的方向
        //        string sortorder = Request.Params["sortorder"];
        //        //当前页
        //        int page = Convert.ToInt32(Request.Params["page"]);
        //        //每页显示的记录数
        //        int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

        //        string msg = string.Empty;
            
        //        WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];



        //        WorkFlow.V_PrivilegesWebService.v_privilegesBLLservice mv_privilegesBllService = new V_PrivilegesWebService.v_privilegesBLLservice();
        //        WorkFlow.V_PrivilegesWebService.SecurityContext mv_SecurityContext = new V_PrivilegesWebService.SecurityContext();

               
        //        mv_SecurityContext.UserName = m_usersModel.login;
        //        mv_SecurityContext.PassWord = m_usersModel.password;
        //        mv_SecurityContext.AppID = (int)m_usersModel.app_id;
        //        mv_privilegesBllService.SecurityContextValue = mv_SecurityContext;

        //        DataSet ds =mv_privilegesBllService.GetMPrivilegesListOfApp((int)m_usersModel.app_id,out msg);

        //        IList<WorkFlow.V_PrivilegesWebService.v> m_list = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
        //        var total = ds.Tables[0].Rows.Count;
        //        for (var i = 0; i < total; i++)
        //        {
        //            WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = (WorkFlow.PrivilegesWebService.privilegesModel)Activator.CreateInstance(typeof(WorkFlow.PrivilegesWebService.privilegesModel));
        //            PropertyInfo[] m_propertys = m_privilegesModel.GetType().GetProperties();
        //            foreach (PropertyInfo pi in m_propertys)
        //            {
        //                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //                {

        //                    // 属性与字段名称一致的进行赋值 
        //                    if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
        //                    {

        //                        //数据库NULL值单独处理
        //                        if (ds.Tables[0].Rows[i][j] != DBNull.Value)
        //                            pi.SetValue(m_privilegesModel, ds.Tables[0].Rows[i][j], null);
        //                        else
        //                            pi.SetValue(m_privilegesModel, null, null);
        //                        break;
        //                    }



        //                }
        //            }
        //            m_list.Add(m_privilegesModel);
        //        }
        //        IList<WorkFlow.PrivilegesWebService.privilegesModel> m_targetList = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
        //        //模拟分页操作
        //        for (var i = 0; i < total; i++)
        //        {
        //            if (i >= (page - 1) * pagesize && i < page * pagesize)
        //            {
        //                m_targetList.Add(m_list[i]);
        //            }
        //        }
        //        var gridData = new
        //        {
        //            Rows = m_targetList,
        //            Total = total
        //        };
        //        return Json(gridData);
        //    }
        //}
       //元素列表(后台分页)
        public ActionResult GetEPrivilegesList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
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
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_privilegesBllService.GetElListByAppID((int)m_usersModel.app_id, out msg);

                IList<WorkFlow.PrivilegesWebService.privilegesModel> m_list = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = (WorkFlow.PrivilegesWebService.privilegesModel)Activator.CreateInstance(typeof(WorkFlow.PrivilegesWebService.privilegesModel));
                    PropertyInfo[] m_propertys = m_privilegesModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {

                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            {
                                //数据库NULL值单独处理
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(m_privilegesModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(m_privilegesModel, null, null);
                                break;
                            }



                        }
                    }
                    m_list.Add(m_privilegesModel);
                }
                IList<WorkFlow.PrivilegesWebService.privilegesModel> m_targetList = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
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

       //操作列表(后台分页)
        public ActionResult GetOPrivilegesList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
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
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_SecurityContext;

                DataSet ds = m_privilegesBllService.GetOpListByAppID((int)m_usersModel.app_id,out msg);
                IList<WorkFlow.PrivilegesWebService.privilegesModel> m_list = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
                var total = ds.Tables[0].Rows.Count;
                for (var i = 0; i < total; i++)
                {
                    WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = (WorkFlow.PrivilegesWebService.privilegesModel)Activator.CreateInstance(typeof(WorkFlow.PrivilegesWebService.privilegesModel));
                    PropertyInfo[] m_propertys = m_privilegesModel.GetType().GetProperties();
                    foreach (PropertyInfo pi in m_propertys)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {

                            // 属性与字段名称一致的进行赋值 
                            if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                            {
                                //数据库NULL值单独处理
                                if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                    pi.SetValue(m_privilegesModel, ds.Tables[0].Rows[i][j], null);
                                else
                                    pi.SetValue(m_privilegesModel, null, null);
                                break;
                            }



                        }
                    }
                    m_list.Add(m_privilegesModel);
                }
                IList<WorkFlow.PrivilegesWebService.privilegesModel> m_targetList = new List<WorkFlow.PrivilegesWebService.privilegesModel>();
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

        //详情
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

                WorkFlow.PrivilegesWebService.SecurityContext m_securityContext = new PrivilegesWebService.SecurityContext();

                string msg = string.Empty;

                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

                WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = m_privilegesBllService.GetModel(id, out msg);
                ViewData["name"] = m_privilegesModel.name;

                if (m_privilegesModel.privilegetype_id == 1)
                {
                    WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
                    WorkFlow.MenusWebService.SecurityContext m_MSecurity = new MenusWebService.SecurityContext();

                    m_MSecurity.UserName = m_usersModel.login;
                    m_MSecurity.PassWord = m_usersModel.password;
                    m_MSecurity.AppID = (int)m_usersModel.app_id;
                    m_menusBllService.SecurityContextValue = m_MSecurity;

                    DataSet dsM = m_menusBllService.GetMenuNameOfAppID((int)m_usersModel.app_id, m_privilegesModel.privilegeitem_id, out msg);
                    ViewData["privilegetype_id"] = "菜单项目";
                    ViewData["privilegeitem_id"] = dsM.Tables[0].Rows[0][0].ToString();

                }
                if (m_privilegesModel.privilegetype_id == 2)
                {
                    WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                    WorkFlow.ElementsWebService.SecurityContext m_ESecurity = new ElementsWebService.SecurityContext();

                    m_ESecurity.UserName = m_usersModel.login;
                    m_ESecurity.PassWord = m_usersModel.password;
                    m_ESecurity.AppID = (int)m_usersModel.app_id;
                    m_elementsBllService.SecurityContextValue = m_ESecurity;

                    DataSet dsE = m_elementsBllService.GetElementsNameOfAppID((int)m_usersModel.app_id, m_privilegesModel.privilegeitem_id, out msg);

                    ViewData["privilegeitem_id"] = dsE.Tables[0].Rows[0][0].ToString();

                    ViewData["privilegetype_id"] = "元素项目";
                }
                if (m_privilegesModel.privilegetype_id == 3)
                {
                    WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
                    WorkFlow.OperationsWebService.SecurityContext m_OSecurity = new OperationsWebService.SecurityContext();

                    m_OSecurity.UserName = m_usersModel.login;
                    m_OSecurity.PassWord = m_usersModel.password;
                    m_OSecurity.AppID = (int)m_usersModel.app_id;
                    m_operationsBllService.SecurityContextValue = m_OSecurity;

                    DataSet dsO = m_operationsBllService.GetOperationsNameOfAppID((int)m_usersModel.app_id, m_privilegesModel.privilegeitem_id, out msg);
                    ViewData["privilegeitem_id"] = dsO.Tables[0].Rows[0][0].ToString();
                    ViewData["privilegetype_id"] = "操作项目";
                }

                if (m_privilegesModel.invalid == false)
                {
                    ViewData["invalid"] = "是"; 
                }
                if (m_privilegesModel.invalid == true)
                {
                    ViewData["invalid"] = "否";
                }
            
                ViewData["app_id"] = m_privilegesModel.app_id;
                ViewData["remark"] = m_privilegesModel.remark;
            
                return View();
            }

        }

        //删除权限
        public ActionResult DeletePrivileges()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int privilegeID = Convert.ToInt32(Request.Form["privilegeID"]);

                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegeBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegeBllService.SecurityContextValue = m_SecurityContext;

                try
                {
                    if (m_privilegeBllService.Delete(privilegeID, out msg) == true)
                    {
       
                        return Json("{success:true,css:'alert alert-success',message:'成功删除!'}");
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

       //权限编辑的基本信息
        public ActionResult EditPage(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();
                WorkFlow.PrivilegesWebService.privilegesModel m_privilegeModel = new PrivilegesWebService.privilegesModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_SecurityContext;

                m_privilegeModel = m_privilegesBllService.GetModel(id, out msg);
                if (m_privilegeModel.privilegetype_id == 1)
                {
                    ViewData["privilegeType_id"] = "菜单";
                    WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
                    WorkFlow.MenusWebService.SecurityContext m_MSecurity = new MenusWebService.SecurityContext();

                    m_MSecurity.UserName = m_usersModel.login;
                    m_MSecurity.PassWord = m_usersModel.password;
                    m_MSecurity.AppID = (int)m_usersModel.app_id;
                    m_menusBllService.SecurityContextValue = m_MSecurity;

                    DataSet dsM = m_menusBllService.GetMenuNameOfAppID((int)m_usersModel.app_id, Convert.ToInt32(m_privilegeModel.privilegeitem_id), out msg);
                    ViewData["privilegeItem_id"] = (dsM.Tables[0].Rows[0][0].ToString());

                }
                if (m_privilegeModel.privilegetype_id == 2)
                {
                    ViewData["privilegeType_id"] = "页面元素";
                    WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                    WorkFlow.ElementsWebService.SecurityContext m_ESecurity = new ElementsWebService.SecurityContext();

                    m_ESecurity.UserName = m_usersModel.login;
                    m_ESecurity.PassWord = m_usersModel.password;
                    m_ESecurity.AppID = (int)m_usersModel.app_id;
                    m_elementsBllService.SecurityContextValue = m_ESecurity;

                    DataSet dsE = m_elementsBllService.GetElementsNameOfAppID((int)m_usersModel.app_id, Convert.ToInt32(m_privilegeModel.privilegeitem_id), out msg);
                    ViewData["privilegeItem_id"] = (dsE.Tables[0].Rows[0][0].ToString());
                }
                if (m_privilegeModel.privilegetype_id == 3)
                {
                    ViewData["privilegeType_id"] = "操作";
                    WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
                    WorkFlow.OperationsWebService.SecurityContext m_OSecurity = new OperationsWebService.SecurityContext();

                    m_OSecurity.UserName = m_usersModel.login;
                    m_OSecurity.PassWord = m_usersModel.password;
                    m_OSecurity.AppID = (int)m_usersModel.app_id;
                    m_operationsBllService.SecurityContextValue = m_OSecurity;

                    DataSet dsO = m_operationsBllService.GetOperationsNameOfAppID((int)m_usersModel.app_id, Convert.ToInt32(m_privilegeModel.privilegeitem_id), out msg);
                    ViewData["privilegeItem_id"] = (dsO.Tables[0].Rows[0][0].ToString());
                }
                ViewData["privilegeId"] = m_privilegeModel.id;
                ViewData["privilegeName"] = m_privilegeModel.name;
                //ViewData["privilegeType_id"] = m_privilegeModel.privilegetype_id;
                ViewData["privilegeItem_id1"] = m_privilegeModel.privilegeitem_id;
                ViewData["privilegeRemark"] = m_privilegeModel.remark;
                ViewData["privilegeApp_id"] = m_privilegeModel.app_id;
                ViewData["privilegeInvalid"] = m_privilegeModel.invalid;
                return View();
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
                int m_privilegeID = Convert.ToInt32(Request.Params["privilegeID"]);//权限ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";

                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();

                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.PrivilegesWebService.privilegesModel privilegesModel = m_privilegesBllService.GetModel(m_privilegeID, out msg);

                string m_selected = string.Empty;
                int total = 1;
                int m_privilegesID = m_privilegeID;
                string m_InvalidName;
                m_InvalidName = "是";
                //判断权限中是否有效
                if (privilegesModel.invalid == false)
                {
                    m_selected = "true";
                }
                else
                {
                    m_selected = "false";
                }
                strJson += "{id:'" + m_privilegesID + "',";
                strJson += "name:'" + m_InvalidName + "',";
                strJson += "selected:'" + m_selected + "'}";


                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
         
        }
       
        //权限编辑信息
        public ActionResult EditPrivileges(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                int m_pi_total = Convert.ToInt32(Request.Form["pv_Total"]);//权限"是否有效"的数量
                string msg = string.Empty;
                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = new PrivilegesWebService.privilegesModel();
                WorkFlow.PrivilegesWebService.SecurityContext m_SecurityContext = new PrivilegesWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = m_SecurityContext;

                int appID = Convert.ToInt32(m_usersModel.app_id);

                int m_privilegesId = Convert.ToInt32(collection["privilegeId"].Trim());
                m_privilegesModel = m_privilegesBllService.GetModel(m_privilegesId, out msg);
                string name = collection["privilegeName"].Trim().ToString();
                if (name.Length == 0)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'权限名称不能为空!'}");
                }
                DataSet ds = m_privilegesBllService.GetAllListByAppID(appID,out msg);
                var total = ds.Tables[0].Rows.Count;
                ArrayList privilgeList = new ArrayList();
                for (int i = 0; i < total; i++)
                {
                    privilgeList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                for (int i = 0; i < total; i++)
                { //修改后的操作名称和本身相同
                    if (m_privilegesModel.name.ToString().Equals(collection["privilegeName"]))
                    {
                        privilgeList.Remove(m_privilegesModel.name);
                    }
                }
                string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);

                m_privilegesModel.name = collection["privilegeName"];
                m_privilegesModel.privilegetype_id = Convert.ToInt32(collection["privilegeType_id"]);

                m_privilegesModel.privilegeitem_id = Convert.ToInt32(collection["privilegeItem_id1"]);
                m_privilegesModel.remark = collection["privilegeRemark"];
                m_privilegesModel.app_id = Convert.ToInt32(collection["privilegeApp_id"]);
                if (m_pi_total == 1)
                {
                    m_privilegesModel.invalid = false;
                }
                if (m_pi_total == 0)
                {
                    m_privilegesModel.invalid = true;
                }
                m_privilegesModel.updated_at = t;
                m_privilegesModel.updated_by = m_usersModel.id;
                m_privilegesModel.updated_ip = collection["privilegeUpdated_ip"].Trim();
                foreach (string privilegename in privilgeList)
                {
                    if (privilegename.Equals(m_privilegesModel.name.ToString()))
                    {
                      
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的权限名称!'}");
                    }
                }
                try
                {
                    if (m_privilegesBllService.Update(m_privilegesModel, out msg))
                    {
                   
                        return Json("{success:true,css:'alert alert-success',message:'修改成功！'}");
                    }
                    else
                    {
                     
                        return Json("{success:false,css:'alert alert-error',message:'修改失败!'}");
                    }
                }
                catch (Exception ex)
                {
             
                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
                      
        }
    }
}
