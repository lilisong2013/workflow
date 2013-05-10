using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public ActionResult AddMenus()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            
            m_menusModel.name = Request.Form["MenusName"];
            m_menusModel.code = Request.Form["MenusCode"];
            m_menusModel.url = Request.Form["MenusUrl"];
            m_menusModel.parent_id = -1;
            m_menusModel.remark = Request.Form["MenusRemark"];
            m_menusModel.created_by = 5;
            m_menusModel.created_ip = Saron.Common.PubFun.IPHelper.GetClientIP();


            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "<i class='icon-check'></i>添加成功" });

        }

        public ActionResult GetMenusName()
        {
            return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "<i class='icon-check'></i>添加成功" });
        }

    }
}
