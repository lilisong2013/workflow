using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

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

        public ActionResult TreeTest()
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

        //添加权限
        public ActionResult AddPrivileges()
        {
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
            WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = new PrivilegesWebService.privilegesModel();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

            m_privilegesModel.name = Request.Form["PrivilegesName"].ToString();

            m_privilegesModel.privilegetype_id = Convert.ToInt32(Request.Form["PrivilegesType"]);
            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功！" });
        }

        //获取权限类型的下拉列表
        public ActionResult GetPrivilegesType()
        {
            WorkFlow.Privileges_TypeWebService.privileges_typeBLLservice m_privilegesTypeBllService = new Privileges_TypeWebService.privileges_typeBLLservice();

            DataSet ds = new DataSet();
            try
            {
                ds = m_privilegesTypeBllService.GetAllPrivileges_typeList();
            }
            catch (Exception ex)
            {
            }

            List<WorkFlow.Privileges_TypeWebService.privileges_typeModel> m_privilegesTypeModelList = new List<Privileges_TypeWebService.privileges_typeModel>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                WorkFlow.Privileges_TypeWebService.privileges_typeModel m_privilegesTypeModel = new Privileges_TypeWebService.privileges_typeModel();
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

        //权限项目列表：菜单
        public ActionResult GetMenusOfItem()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_userModel = (UsersWebService.usersModel)Session["user"];
            string data = "[{name:'顶级菜单',id:'-1',children:[";
            try
            {
                DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_userModel.app_id);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string code = ds.Tables[0].Rows[i][2].ToString();
                    string url = ds.Tables[0].Rows[i][3].ToString();
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    string invalid = ds.Tables[0].Rows[i][7].ToString();
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                        data += "code:'" + code + "',";
                        data += "url:'" + url + "',";
                        data += "invalid:'" + invalid + "',";
                        data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "}";
                    }
                    else
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                        data += "code:'" + code + "',";
                        data += "url:'" + url + "',";
                        data += "invalid:'" + invalid + "',";
                        data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}]";
            return Json(data);
        }
        //权限项目列表：操作
        public ActionResult GetOperationOfItem()
        {
            string str = "";
            return Json(str);
        }
        //权限项目列表：元素
        public ActionResult GetElementOfItem(int menusID)
        {
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (UsersWebService.usersModel)Session["user"];


            string str = "";
            return Json(str);
        }

        //菜单Grid
        public string GetChildrenMenusList(int parentId)
        {
            string dataStr = ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            if (m_menusBllService.ExistsChildrenMenus(parentId))
            {
                DataSet ds = m_menusBllService.GetChildrenMenus(parentId);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string code = ds.Tables[0].Rows[i][2].ToString();
                    string url = ds.Tables[0].Rows[i][3].ToString();
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    string invalid = ds.Tables[0].Rows[i][7].ToString();
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0]))
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
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
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "invalid:'" + invalid + "',";
                            dataStr += "remark:'" + remark + "'},";
                        }
                    }
                }
            }
            dataStr += "]";
            return dataStr;
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
            WorkFlow.UsersWebService.usersModel m_userModel = (UsersWebService.usersModel)Session["user"];
            string data = "[{name:'顶级菜单',id:'-1',children:[";
            try
            {
                DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_userModel.app_id);
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
            string dataStr = ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            if (m_menusBllService.ExistsChildrenMenus(parentId))
            {
                DataSet ds = m_menusBllService.GetChildrenMenus(parentId);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0]))
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

    }
}
