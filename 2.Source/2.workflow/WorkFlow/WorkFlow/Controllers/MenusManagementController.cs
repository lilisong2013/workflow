using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WorkFlow.Controllers
{
    public class MenusManagementController : Controller
    {
        //
        // GET: /MenusManagement/

        public ActionResult AppMenus()
        {
            return View();
        }


        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMenus()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            string str = Request.Form["MenusParent"];
            

            try
            {
                if (Request.Form["MenusName"]=="")
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "<i class='icon-check'></i>菜单名不能为空" });
                }
                if (Request.Form["MenusCode"] == "")
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "<i class='icon-check'></i>菜单编码不能为空" });
                }
                m_menusModel.name = Request.Form["MenusName"];
                m_menusModel.code = Request.Form["MenusCode"];
                m_menusModel.url = Request.Form["MenusUrl"];

                if (Request.Form["MenusParent"] != "-1")
                {
                    m_menusModel.parent_id = Convert.ToInt32(Request.Form["MenusParent"]);
                }
                m_menusModel.remark = Request.Form["MenusRemark"];
                m_menusModel.created_at = DateTime.Now;
                m_menusModel.created_by = m_usersModel.id;
                m_menusModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();

                if (m_menusBllService.Add(m_menusModel) != 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "<i class='icon-check'></i>添加成功" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "<i class='icon-check'></i>添加失败" });
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "<i class='icon-check'></i>添加失败" });
            }

        }

        /// <summary>
        /// 获得菜单的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetMenusName()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            DataSet ds = m_menusBllService.GetAllMenusList();
            List<Saron.WorkFlow.Models.menusHelper> m_menuslist = new List<Saron.WorkFlow.Models.menusHelper>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][5] == DBNull.Value)
                {
                    m_menuslist.Add(new Saron.WorkFlow.Models.menusHelper { menusID = Convert.ToInt32(ds.Tables[0].Rows[i][0]), menusName = ds.Tables[0].Rows[i][1].ToString(), menusLevel = 1, parentID = -1 });
                }
                else
                {
                    m_menuslist.Add(new Saron.WorkFlow.Models.menusHelper { menusID = Convert.ToInt32(ds.Tables[0].Rows[i][0]), menusName = ds.Tables[0].Rows[i][1].ToString(), menusLevel = 1, parentID = Convert.ToInt32(ds.Tables[0].Rows[i][5]) });
                }
                
            }

            var dataJson = new { 
                Rows=m_menuslist,
                Total = ds.Tables[0].Rows.Count
            };
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }




    }
}
