using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
namespace WorkFlow.Controllers
{
    public class BaseUserManagementController : Controller
    {
        //
        // GET: /BaseUserManagement/

        public ActionResult BaseUserPage()
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


        public ActionResult GetAllBase_UserList()
        {
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

            WorkFlow.Base_UserWebService.base_userBLLservice m_base_userBllService = new Base_UserWebService.base_userBLLservice();
            DataSet ds = m_base_userBllService.GetAllBase_UserList();

            IList<WorkFlow.Base_UserWebService.base_userModel> m_list = new List<WorkFlow.Base_UserWebService.base_userModel>();

            var total = ds.Tables[0].Rows.Count;
            for (var i = 0; i < total; i++)
            {
                WorkFlow.Base_UserWebService.base_userModel m_base_userModel = (WorkFlow.Base_UserWebService.base_userModel)Activator.CreateInstance(typeof(WorkFlow.Base_UserWebService.base_userModel));
                PropertyInfo[] m_propertys = m_base_userModel.GetType().GetProperties();
                foreach (PropertyInfo pi in m_propertys)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                        {
                            // 数据库NULL值单独处理 
                            if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                pi.SetValue(m_base_userModel, ds.Tables[0].Rows[i][j], null);
                            else
                                pi.SetValue(m_base_userModel, null, null);
                            break;
                        }
                    }
                }
                m_list.Add(m_base_userModel);
            }

            //模拟排序操作
            if (sortorder == "desc")
                m_list = m_list.OrderByDescending(c => c.id).ToList();
            else
                m_list = m_list.OrderBy(c => c.id).ToList();

            IList<WorkFlow.Base_UserWebService.base_userModel> m_targetList = new List<WorkFlow.Base_UserWebService.base_userModel>();
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
