using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Reflection;

namespace WorkFlow.Controllers
{
    public class RolesManagementController : Controller
    {
        //
        // GET: /RolesManagement/

        public ActionResult AppRoles()
        {
            if (Session["loginName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return View();
            }
            
        }
        /// <summary>
        /// 删除一条内容为系统好为id的信息
        /// </summary>
        /// <param name="id">系统id号</param>
        /// <returns></returns>
        public ActionResult ChangePage(int id)
        {
          //  return RedirectToAction("ChangePage");
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
            return View();
        }

        public ActionResult GetRoles_Apply()
        {
            Boolean flag;
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
