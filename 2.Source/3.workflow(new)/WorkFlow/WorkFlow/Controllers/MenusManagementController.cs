using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;

namespace WorkFlow.Controllers
{
    public class MenusManagementController : Controller
    {
        //
        // GET: /MenusManagement/

        public ActionResult AppMenus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        public ActionResult AddMenus()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            WorkFlow.ElementsWebService.SecurityContext m_esecurityContext = new ElementsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_esecurityContext.UserName = m_usersModel.login;
            m_esecurityContext.PassWord = m_usersModel.password;
            m_esecurityContext.AppID = (int)m_usersModel.app_id;
            m_elementsBllService.SecurityContextValue = m_esecurityContext;//实例化 [SoapHeader("m_securityContext")]
            
            if (Request.Form["MenusName"] == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单名不能为空" });
            }

            if (Request.Form["MenusCode"] == "")
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单编码不能为空" });
            }

            m_menusModel.name = Request.Form["MenusName"];
            m_menusModel.code = Request.Form["MenusCode"];
            m_menusModel.url = Request.Form["MenusUrl"];
            m_menusModel.app_id = m_usersModel.app_id;//系统ID

            if (Request.Form["MenusParent"] != "-1")
            {
                m_menusModel.parent_id = Convert.ToInt32(Request.Form["MenusParent"]);
            }

            m_menusModel.remark = Request.Form["MenusRemark"];
            m_menusModel.created_at = DateTime.Now;
            m_menusModel.created_by = m_usersModel.id;
            m_menusModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();

            if (m_menusBllService.ExistsMenusName(m_menusModel.name, m_menusModel.parent_id, (int)m_menusModel.app_id, out msg))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单名称在父菜单下已经存在！" });
            }

            if (m_menusBllService.ExistsMenusCode(m_menusModel.code, (int)m_menusModel.app_id, out msg))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统中菜单编码已经存在！" });
            }

            if (m_menusModel.parent_id != null)
            {
                if (m_elementsBllService.ExistsElementsOfMenus((int)m_menusModel.parent_id, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单下存在页面元素，不允许添加子菜单！" });
                }
            }

            try
            {
                int addFlag = m_menusBllService.Add(m_menusModel,out msg);

                if (addFlag == -1)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = msg });
                }

                if ( addFlag!= 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "添加失败" });
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "添加失败" });
            }

        }
       
        //删除菜单
        public ActionResult DeleteMenus()
        {
            int menusID = Convert.ToInt32(Request.Params["menuID"]);
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService=new ElementsWebService.elementsBLLservice();
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.MenusWebService.SecurityContext m_msecurityContext = new MenusWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_msecurityContext.UserName = m_usersModel.login;
            m_msecurityContext.PassWord = m_usersModel.password;
            m_msecurityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_msecurityContext;//实例化 [SoapHeader("m_securityContext")]

            WorkFlow.PrivilegesWebService.SecurityContext m_psecurityContext = new PrivilegesWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_psecurityContext.UserName = m_usersModel.login;
            m_psecurityContext.PassWord = m_usersModel.password;
            m_psecurityContext.AppID = (int)m_usersModel.app_id;
            m_privilegesBllService.SecurityContextValue = m_psecurityContext;//实例化 [SoapHeader("m_securityContext")]

            WorkFlow.ElementsWebService.SecurityContext m_esecurityContext = new ElementsWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_esecurityContext.UserName = m_usersModel.login;
            m_esecurityContext.PassWord = m_usersModel.password;
            m_esecurityContext.AppID = (int)m_usersModel.app_id;
            m_elementsBllService.SecurityContextValue = m_esecurityContext;//实例化 [SoapHeader("m_securityContext")]
            try
            {
                if (m_menusBllService.ExistsChildrenMenus(menusID, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单存在子菜单，无法删除！" });
                }

                if(m_elementsBllService.ExistsElementsOfMenus(menusID,out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单下存在页面元素，无法删除！" });
                }

                if (m_privilegesBllService.ExistsItemOfPrivilegesType(1, menusID,out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单已创建权限，无法删除！" });
                }

                if (m_menusBllService.DeleteMenus(menusID, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "菜单删除成功！" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单删除失败！" });
                }
            }
            catch(Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "删除失败！" });
            }
        }

        //获取菜单列表（在grid中显示）
        public ActionResult GetMenusList()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_usersModel = (UsersWebService.usersModel)Session["user"];

            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

            string msg = string.Empty;

            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
            
            string data = "{Rows:[";
            try
            {
                DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_usersModel.app_id,out msg);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string code=ds.Tables[0].Rows[i][2].ToString();
                    string url = ds.Tables[0].Rows[i][3].ToString();
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                        data += "code:'" + code + "',";
                        data += "url:'" + url + "',";
                        data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "}";
                    }
                    else
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                        data += "code:'" + code + "',";
                        data += "url:'" + url + "',";
                        data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}";
            return Json(data);
        }

        /// <summary>
        /// 获得菜单的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetMenus()
        {
            //string data = "[{name:'顶级菜单',id:'-1',children:[{ name: '部门1',id:'1',children:[{ name: '角色1.1',id:'2'},{ name: '角色1.2',id:'3'}] },{ name: '部门2',id:'4'},{ name: '部门3',id:'5'}]}]";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_usersModel = (UsersWebService.usersModel)Session["user"];

            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

            string msg = string.Empty;

            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            string data="[{name:'顶级菜单',id:'-1',children:[";
            try
            {
                DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_usersModel.app_id,out msg);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "}";
                    }
                    else
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "},";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}]";
            return Json(data);
        }

        //菜单树
        public string GetChildrenMenus(int parentId)
        {
            string dataStr= ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

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
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0],out msg))
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "},";
                        }
                    }
                    else
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'},";
                        }
                    }
                }
            }
            dataStr += "]";
            return dataStr;
        }

        //菜单Grid
        public string GetChildrenMenusList(int parentId)
        {
            string dataStr = ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

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
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0],out msg))
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
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
                            dataStr += "remark:'" + remark + "'}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "remark:'" + remark + "'},";
                        }
                    }
                }
            }
            dataStr += "]";
            return dataStr;
        }
        
        //菜单详情
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string msg = string.Empty;

            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();
            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]

            WorkFlow.MenusWebService.menusModel m_menusModel = m_menusBllService.GetModel(id,out msg);

            ViewData["name"] = m_menusModel.name;
            ViewData["code"] = m_menusModel.code;
            ViewData["url"] = m_menusModel.url;
            ViewData["app_id"] = m_menusModel.app_id;
            ViewData["parent_id"] = m_menusModel.parent_id;
            ViewData["remark"] = m_menusModel.remark;
            ViewData["invalid"] = m_menusModel.invalid;
           
            return View();
        }
        //菜单为id的基本信息
        public ActionResult EditPage(int id)
        {
            string msg = string.Empty;
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.SecurityContext m_SecurityContext = new MenusWebService.SecurityContext();

            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
            m_SecurityContext.UserName = m_usersModel.login;
            m_SecurityContext.PassWord = m_usersModel.password;
            m_SecurityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_SecurityContext;

            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            m_menusModel = m_menusBllService.GetModel(id,out msg);
         
            ViewData["menusId"] = m_menusModel.id;
            ViewData["menusName"] = m_menusModel.name;
            ViewData["menuCode"] = m_menusModel.code;
            ViewData["menuUrl"] = m_menusModel.url;
            ViewData["menuApp_id"] = m_menusModel.app_id;
            if (m_menusModel.parent_id.ToString().Length == 0)
            {
                ViewData["menuParent_id"] = "顶级菜单";
            }
            else 
            {
               ViewData["menuParent_id"] = m_menusModel.parent_id;
            }           
            ViewData["menuRemark"] = m_menusModel.remark;
            ViewData["menuInvalid"] = m_menusModel.invalid;
            ViewData["menuDeleted"] = m_menusModel.deleted;
            return View();
        }
        //获取是否有效的列表
        public ActionResult GetInvalidList()
        {
            string msg = string.Empty;
            int m_menuID = Convert.ToInt32(Request.Params["menusID"]);//菜单ID
            string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";

            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();

            WorkFlow.MenusWebService.SecurityContext m_SecurityContext = new MenusWebService.SecurityContext();

            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

            m_SecurityContext.UserName = m_usersModel.login;
            m_SecurityContext.PassWord = m_usersModel.password;
            m_SecurityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_SecurityContext;

            WorkFlow.MenusWebService.menusModel menusModel = m_menusBllService.GetModel(m_menuID,out msg);
            string m_selected = string.Empty;
            int total = 1;
            int m_menusID = m_menuID;
            string m_InvalidName;
            m_InvalidName ="是";
            //判断菜单中是否有效
            if (menusModel.invalid == false)
            {
                m_selected = "true";
            }
            else
            {
                m_selected = "false";
            }

            strJson += "{id:'" + m_menusID + "',";
            strJson += "name:'" + m_InvalidName + "',";
            strJson += "selected:'" + m_selected + "'}";


            strJson += "],total:'" + total + "'}";
            return Json(strJson);
        }
        ///<summay>
        ///编辑数据库中指定记录的操作
        ///</summay>
        ///<param name="id">系统的ID</param>
        ///<returns></returns>
        public ActionResult EditMenus(FormCollection collection)
        {
            int m_mi_total = Convert.ToInt32(Request.Params["mv_Total"]);
            string msg = string.Empty;
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.MenusWebService.SecurityContext m_SecurityContext = new MenusWebService.SecurityContext();

            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

            m_SecurityContext.UserName = m_usersModel.login;
            m_SecurityContext.PassWord = m_usersModel.password;
            m_SecurityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_SecurityContext;

            int appID = Convert.ToInt32(m_usersModel.app_id);
            int m_menusId = Convert.ToInt32(collection["menusId"].Trim());
            m_menusModel = m_menusBllService.GetModel(m_menusId,out msg);
            string name = collection["menusName"].Trim().ToString();

            if (name.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单名称不能为空!" });
            }
            DataSet ds = m_menusBllService.GetAllMenusListofApp(appID,out msg);
            var total = ds.Tables[0].Rows.Count;
            ArrayList menusList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                menusList.Add(ds.Tables[0].Rows[i][1].ToString());
            }
            for (int i = 0; i < total; i++)
            { 
            //修改后的菜单名称和本身相同
                if (m_menusModel.name.ToString().Equals(collection["menusName"]))
                {
                    menusList.Remove(m_menusModel.name);
                }
            }

            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);

            m_menusModel.name = collection["menusName"];
            m_menusModel.code=collection["menuCode"];
            m_menusModel.url = collection["menuUrl"];
            m_menusModel.app_id = Convert.ToInt32(collection["menuApp_id"]);
            //m_menusModel.parent_id = collection["menuParent_id"];
            m_menusModel.remark = collection["menuRemark"];
            if (m_mi_total == 1)
            {
                m_menusModel.invalid = false;
            }
            if (m_mi_total == 0)
            {
                m_menusModel.invalid = true;
            }
            //m_menusModel.invalid = Convert.ToBoolean(collection["menuInvalid"]);
            m_menusModel.updated_at = t;
            m_menusModel.updated_by=m_usersModel.id;
            m_menusModel.updated_ip = collection["menuCreated_ip"].Trim();
            foreach (string menuListname in menusList)
            {
                if (menuListname.Equals(m_menusModel.name.ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的菜单名称!" });
                }
            }
            try {

                if (m_menusBllService.Update(m_menusModel, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功！", toUrl = "/MenusManagement/AppMenus" });
                }
                else 
                {
                 return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="修改失败!"});
                }
               
            }
            catch (Exception ex) 
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常!" });
            }
        }
    }
}
