using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlow.Controllers
{
    public class ElementsManagementController : Controller
    {
        //
        // GET: /ElementsManagement/

        public ActionResult AppElements()
        {
            return View();
        }

        public ActionResult AddElements()
        {
            if (Request.IsAjaxRequest())
            {
                string str1 = Request.Form["ElementsName"];
                string str2 = Request.Form["ElementsCode"];

                return Json(new LoginResultDTO { Success = true, Message = "添加成功", ReturnUrl = "SomeURL" });
            }
            else
            {
                return RedirectToAction("AppElements");
            }
        }


        public class LoginResultDTO
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string ReturnUrl { get; set; }
        } 

    }
}
