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


        //添加权限
        public ActionResult AddPrivileges()
        {
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();
            WorkFlow.PrivilegesWebService.privilegesModel m_privilegesModel = new WorkFlow.PrivilegesWebService.privilegesModel();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];//获取session存储的系统管理员对象

            m_privilegesModel.name = Request.Form["PrivilegesName"].ToString();
            m_privilegesModel.privilegetype_id = Convert.ToInt32(Request.Form["PrivilegesType"].ToString());
            m_privilegesModel.privilegeitem_id = Convert.ToInt32(Request.Form["PrivilegesItem"].ToString());
            m_privilegesModel.remark = Request.Form["PrivilegesRemark"].ToString();
            m_privilegesModel.app_id = (int)m_usersModel.app_id;//系统ID
            m_privilegesModel.created_at = DateTime.Now;
            m_privilegesModel.created_by = m_usersModel.id;
            m_privilegesModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();

            try
            {
                if (m_privilegesBllService.Add(m_privilegesModel) != 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加权限成功！" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "添加权限成功！" });
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序出错！" });
            }

        }

        //获取权限类型的下拉列表
        public ActionResult GetPrivilegesType()
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

        //权限项目列表：菜单
        public ActionResult GetMenusOfItem()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new WorkFlow.MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new WorkFlow.MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
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
            WorkFlow.OperationsWebService.operationsBLLservice m_operationsBllService = new OperationsWebService.operationsBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            string data = "{Rows:[";
            try
            {
                DataSet ds = m_operationsBllService.GetOperationsListOfApp((int)m_userModel.app_id);
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
        //权限项目列表：元素
        public ActionResult GetElementOfItem()
        {
            int menusID = Convert.ToInt32(Request.Params["menusID"]);//菜单ID
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new WorkFlow.ElementsWebService.elementsBLLservice();
            string data = "{Rows:[";
            try
            {
                DataSet ds = m_elementsBllService.GetElementsListOfMenus(menusID);
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

        //菜单
        public string GetChildrenMenusList(int parentId)
        {
            string dataStr = ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new WorkFlow.MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new WorkFlow.MenusWebService.menusModel();

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

        //判断项目(操作)是否已经创建权限
        public ActionResult ExistPrivilegeItemOfOperations()
        {
            int privilegeTypeID = 3;//权限类型ID
            int privilegeItemID = Convert.ToInt32(Request.Params["operationsID"]);//权限项目ID

            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();

            if (m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, privilegeItemID))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "该项目已设置权限！" });
            }
            else
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
            }
        }
        //判断项目(菜单)是否已经创建权限
        public ActionResult ExistPrivilegeItemOfMenus()
        {
            int privilegeTypeID = 1;//权限类型ID
            int privilegeItemID = Convert.ToInt32(Request.Params["menusID"]);//权限项目ID

            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();

            if (m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, privilegeItemID))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "该项目已设置权限！" });
            }
            else
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
            }
        }
        //判断项目(页面元素)是否已经创建权限
        public ActionResult ExistPrivilegeItemOfElements()
        {
            int privilegeTypeID = 2;//权限类型ID
            int privilegeItemID = Convert.ToInt32(Request.Params["privilegeItemID"]);//权限项目ID

            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new WorkFlow.PrivilegesWebService.privilegesBLLservice();

            if (m_privilegesBllService.ExistsItemOfPrivilegesType(privilegeTypeID, privilegeItemID))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "该项目已设置权限！" });
            }
            else
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true });
            }
        }

        //判断菜单是否有子菜单
        public ActionResult ExistChildreMenus()
        {
            int menusID = Convert.ToInt32(Request.Params["menusID"]);//菜单ID
            WorkFlow.MenusWebService.menusBLLservice m_menusWebService = new MenusWebService.menusBLLservice();

            if (menusID == -1)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false });
            }

            try
            {
                if (m_menusWebService.ExistsChildrenMenus(menusID))
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

        //权限列表
        public ActionResult GetAllPrivilegesList()
        {
            WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            string data = "{Rows:[";
            try
            {
                DataSet ds = m_privilegesBllService.GetAllListByAppID((int)m_userModel.app_id);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string privilegetype_id = ds.Tables[0].Rows[i][2].ToString();
                    string privilegeitem_id = ds.Tables[0].Rows[i][3].ToString();
                    string remark = ds.Tables[0].Rows[i][4].ToString();
                    string invalid = ds.Tables[0].Rows[i][5].ToString();
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',";
                        data += "privilegetype_id:'" + privilegetype_id + "',";
                        data += "privilegeitem_id:'" + privilegeitem_id + "',";
                        data += "invalid:'" + invalid + "',";
                        data += "remark:''}";
                    }
                    else
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',";
                        data += "privilegetype_id:'" + privilegetype_id + "',";
                        data += "privilegeitem_id:'" + privilegeitem_id + "',";
                        data += "invalid:'" + invalid + "',";
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











        /// <summary>
        /// 获得菜单的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetMenus()
        {
            //string data = "[{name:'顶级菜单',id:'-1',children:[{ name: '部门1',id:'1',children:[{ name: '角色1.1',id:'2'},{ name: '角色1.2',id:'3'}] },{ name: '部门2',id:'4'},{ name: '部门3',id:'5'}]}]";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new WorkFlow.MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new WorkFlow.MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_userModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
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
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new WorkFlow.MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new WorkFlow.MenusWebService.menusModel();

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
