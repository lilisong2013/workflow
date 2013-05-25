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
               
                return View();         
        }

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="collection">表单数据</param>
        /// <returns>成功,返回主页面</returns>
        public ActionResult AddRoles(FormCollection collection)
        {
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();

            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
            string m_rolesName = collection["rolesName"].Trim();
            if (m_rolesName.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "角色名称不能为空??！" });
            }
            m_rolesModel.name = collection["rolesName"].Trim();
            //获得deleted=false的rolesName列表
            DataSet ds = m_rolesBllService.GetDeletedRoles();
            var total = ds.Tables[0].Rows.Count;
            ArrayList rolesList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                rolesList.Add(ds.Tables[0].Rows[i][0].ToString());
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
            m_rolesBllService.Add(m_rolesModel);
            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功！", toUrl = "/RolesManagement/AppRoles" });
        }
        /// <summary>
        /// 删除一条内容为系统好为id的信息
        /// </summary>
        /// <param name="id">系统id号</param>
        /// <returns></returns>
        public ActionResult ChangePage(int id)
        {
       
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBLLService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
            if (m_rolesBLLService.Delete(id))
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
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);
            string ipAddress = Saron.Common.PubFun.IPHelper.GetIpAddress();
            m_rolesModel = m_rolesBllService.GetModel(id);
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

            string[] invalid = new string[2];
            invalid[0]="true";
            invalid[1]="false";
            List<Saron.WorkFlow.Models.InvalidHelper> m_invalidlist=new List<Saron.WorkFlow.Models.InvalidHelper>();
            for (int i = 0; i < 2; i++)
            {
                m_invalidlist.Add(new Saron.WorkFlow.Models.InvalidHelper { InvalidID=invalid[i].ToString(),InvalidName=invalid[i].ToString()});
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
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
            int id = Convert.ToInt32(collection["rolesId"].Trim());
            m_rolesModel = m_rolesBllService.GetModel(id);
            string name = collection["rolesName"].Trim();
            if (name.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "角色名称不能为空！" });
            }
            //获得deleted=false的rolesName列表
            DataSet ds = m_rolesBllService.GetDeletedRoles();
            var total = ds.Tables[0].Rows.Count;
            ArrayList rolesList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                rolesList.Add(ds.Tables[0].Rows[i][0].ToString());
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
            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
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
            if (m_rolesBllService.Update(m_rolesModel))
            {             
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功！", toUrl = "/RolesManagement/AppRoles" });           
            }

            else
            {
                return RedirectToAction("AppRoles");
            }
        }
        /// <summary>
        /// 显示所选系统的详情
        /// </summary>
        /// <param name="id">系统的ID</param>
        /// <returns></returns>     
        public ActionResult DetailInfo(int id)
        {
            WorkFlow.RolesWebService.rolesBLLservice m_rolesBllService = new RolesWebService.rolesBLLservice();
            WorkFlow.RolesWebService.rolesModel m_rolesModel = new RolesWebService.rolesModel();
            m_rolesModel = m_rolesBllService.GetModel(id);
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
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

            WorkFlow.RolesWebService.rolesBLLservice m_rolesService = new RolesWebService.rolesBLLservice();
            DataSet ds = m_rolesService.GetAllRolesList();
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
    }
}
