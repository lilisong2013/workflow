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

        public ActionResult App_Apply()
        {
            if (Session["loginName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                WorkFlow.AppsWebService.appsBLLservice m_apps = new AppsWebService.appsBLLservice();
                int m_appCount = m_apps.GetRecordCount("invalid=1");
                DataSet ds=m_apps.GetAppsTopList(1,"invalid=1","apply_at Asc");
                if (m_appCount > 0)
                {
                    ViewData["appCount"] = m_appCount;
                    ViewData["appName"] = ds.Tables[0].Rows[0][1];
                }
                else
                {
                    ViewData["appCount"] = m_appCount;
                }
                return View();
            }
        }

        public ActionResult ChangePage(int id)
        {
            return RedirectToAction("App_Apply");
        }

        public ActionResult GetApps_Apply()
        {
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

            WorkFlow.AppsWebService.appsBLLservice m_appsService = new AppsWebService.appsBLLservice();
            DataSet ds = m_appsService.GetAllAppsList();

            IList<WorkFlow.AppsWebService.appsModel> m_list = new List<WorkFlow.AppsWebService.appsModel>();

            var total = ds.Tables[0].Rows.Count;
            for (var i = 0; i < total; i++)
            {
                WorkFlow.AppsWebService.appsModel m_appsModel = (WorkFlow.AppsWebService.appsModel)Activator.CreateInstance(typeof(WorkFlow.AppsWebService.appsModel));
                PropertyInfo[] m_propertys = m_appsModel.GetType().GetProperties();
                foreach (PropertyInfo pi in m_propertys)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                        {
                            // 数据库NULL值单独处理 
                            if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                pi.SetValue(m_appsModel, ds.Tables[0].Rows[i][j], null);
                            else
                                pi.SetValue(m_appsModel, null, null);
                            break;
                        }
                    }
                }
                m_list.Add(m_appsModel);
            }

            //模拟排序操作
            if (sortorder == "desc")
                m_list = m_list.OrderByDescending(c => c.id).ToList();
            else
                m_list = m_list.OrderBy(c => c.id).ToList();

            IList<WorkFlow.AppsWebService.appsModel> m_targetList = new List<WorkFlow.AppsWebService.appsModel>();
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
