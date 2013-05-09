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
            if (Request.IsAjaxRequest())
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


                return Json(new AddResultDTO { Success = true, Message = "warningDIV", ReturnUrl = "SomeURL" });
            }
            else
            {
                return RedirectToAction("AppMenus");
            }
        }

        public class AddResultDTO
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string ReturnUrl { get; set; }
        } 
    }
}
