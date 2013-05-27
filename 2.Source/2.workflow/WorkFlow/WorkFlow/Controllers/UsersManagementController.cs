using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WorkFlow.Controllers
{
    public class UsersManagementController:Controller
    {
        public ActionResult AppUsers()
        {
            return View();
        }
    }
}