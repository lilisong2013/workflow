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
        /// <returns></returns>
        public ActionResult AddMenus()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            
            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

            string msg = string.Empty;

            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
            
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

            if (m_elementsBllService.ExistsElementsOfMenus((int)m_menusModel.parent_id, out msg))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "父菜单下存在页面元素，不允许添加子菜单！" });
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
       
        ///<summary>
        ///删除菜单
        /// </summary>
        //public ActionResult ChangePage(int id)
        //{
        //    WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
        //    WorkFlow.MenusWebService.menusModel m_menusModel = m_menusBllService.GetModel(id);
        //    //如果有孩子节点
        //    if (m_menusBllService.ExistsChildrenMenus(id) == true)
        //    {
        //        //return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "本节点不是叶子节点，不能删除!" });
        //        return RedirectToAction("AppMenus");
        //    }
        //    else
        //    {
        //        // return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "删除成功!", toUrl = "/MenusManagement/AppMenus" });
        //        if (m_menusBllService.DeleteID(id))
        //        {
        //            return RedirectToAction("AppMenus");
        //        }
        //        else
        //        {
        //            return RedirectToAction("AppMenus");
        //        }
        //    }
           
        //}
       
        //删除菜单
        public ActionResult DeleteMenus()
        {
            int menusID = Convert.ToInt32(Request.Params["menuID"]);
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService=new ElementsWebService.elementsBLLservice();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();

            string msg = string.Empty;

            //SecurityContext实体对象赋值
            m_securityContext.UserName = m_usersModel.login;
            m_securityContext.PassWord = m_usersModel.password;
            m_securityContext.AppID = (int)m_usersModel.app_id;
            m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]
            try
            {
                if (m_menusBllService.ExistsChildrenMenus(menusID, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单存在子菜单，无法删除！" }, JsonRequestBehavior.AllowGet);
                }

                if(m_elementsBllService.ExistsElementsOfMenus(menusID,out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单下存在页面元素，无法删除！" }, JsonRequestBehavior.AllowGet);
                }

                if (m_menusBllService.DeleteMenus(menusID, out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "菜单删除成功！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "菜单删除失败！" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "删除失败！" }, JsonRequestBehavior.AllowGet);
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
        //public ActionResult DetailInfo(int id)
        //{
        //    WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
        //    WorkFlow.MenusWebService.menusModel m_menusModel = m_menusBllService.GetModel(id);
        //    ViewData["name"] = m_menusModel.name;
        //    ViewData["code"] = m_menusModel.code;
        //    ViewData["url"] = m_menusModel.url;
        //    ViewData["app_id"] = m_menusModel.app_id;
        //    ViewData["parent_id"] = m_menusModel.parent_id;
        //    ViewData["remark"] = m_menusModel.remark;
        //    ViewData["invalid"] = m_menusModel.invalid;
           
        //    return View();
        //}
    }
}
