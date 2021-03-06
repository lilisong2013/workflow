﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Reflection;

namespace WorkFlow.Controllers
{
    public class AppsManagementController : Controller
    {
        //
        // GET: /AppsManagement/

        public ActionResult BaseUserApps()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ReturnBaseUserApps()
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("BaseUserApps");
            }
        }

        //退出超级管理员界面
        public ActionResult QuitSys()
        {
            Session["baseuser"] = null;
            return RedirectToAction("Login", "Home");
        }

        //超级管理员重新登录
        public ActionResult LoginAgain()
        {
            Session["baseuser"] = null;
            return RedirectToAction("AdminLogin", "Home");
        }

        //系统审批页面
        public ActionResult BU_ApprovalApps(int id)
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.UsersWebService.usersBLLservice m_userBllService=new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_userModel=new UsersWebService.usersModel();

                m_appsModel = m_appsBllService.GetModel(id);
                m_userModel = m_userBllService.GetModelByAppID(m_appsModel.id);
                ViewData["appInfo"] = m_appsModel;//系统信息
                ViewData["userInfo"] = m_userModel;//用户信息

                return View();
            }
        }

        //系统信息页面
        public ActionResult BU_AppsInfo(int id)
        {
            if (Session["baseuser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
                WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
                WorkFlow.UsersWebService.usersBLLservice m_userBllService = new UsersWebService.usersBLLservice();
                WorkFlow.UsersWebService.usersModel m_userModel = new UsersWebService.usersModel();

                m_appsModel = m_appsBllService.GetModel(id);
                m_userModel = m_userBllService.GetModelByAppID(m_appsModel.id);
                ViewData["appInfo"] = m_appsModel;//系统信息
                ViewData["userInfo"] = m_userModel;//用户信息

                return View();
            }
        }

        //public ActionResult  ApprovalAppsJSON(int id)
        //{
        //    workflow.AppsWebService.appsModel m_appsModel=new AppsWebService.appsModel();
        //    workflow.AppsWebService.appsBLLservice m_appsWebService = new AppsWebService.appsBLLservice();
        //    m_appsModel = m_appsWebService.GetModel(id);
        //    return RedirectToAction("BU_ApprovalApps",Json(m_appsModel));
        //}

        //修改超级管理员密码
        public ActionResult ModifyAdminPassword()
        {
            string m_oldpassword = Request.Form["oldpassword"];
            string m_newpassword = Request.Form["newpassword"];
            string m_newpassword2 = Request.Form["newpassword2"];

            WorkFlow.Base_UserWebService.base_userBLLservice m_baseuserBllService = new Base_UserWebService.base_userBLLservice();
            WorkFlow.Base_UserWebService.base_userModel m_baseuserModel=(Base_UserWebService.base_userModel)Session["baseuser"];

            if (m_newpassword != m_newpassword2)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-warningDIV", message = "两次密码不一致！" });
            }

            try
            {
                if (!m_baseuserBllService.LoginValidator(m_baseuserModel.login, m_oldpassword))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "原密码不正确！" });
                }

                if (m_baseuserBllService.ModifyPassword(m_baseuserModel.login, m_newpassword))
                {
                    m_baseuserModel = m_baseuserBllService.GetModelByLogin(m_baseuserModel.login);
                    Session["baseuser"] = m_baseuserModel;
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-successDIV", message = "密码修改成功！" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "密码修改失败！" });
                }
            }
            catch(WebException ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "服务器连接失败！" });
            }
            catch(Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "程序异常！" });
            }

            
        }

        //批准系统申请
        public ActionResult ApprovalAppsApply(FormCollection collection)
        {
            string appid = collection["id"].ToString();

            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();

            try
            {
                m_appsModel = m_appsBllService.GetModel(Convert.ToInt32(appid));
            }
            catch(Exception ex) 
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统出错！" });
            }

            m_appsModel.invalid = false;

            if (m_appsBllService.Update(m_appsModel))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "系统：" + m_appsModel.name + "，已经可以使用！" });
            }
            else
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "系统：" + m_appsModel.name + "，审批失败！" });
            }
        }


        public DataSet ValidAppsDataset()
        {
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            DataSet ds = new DataSet();
            try
            {
                ds = m_appsBllService.GetValidAppsList();
            }
            catch(Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public DataSet InvalidAppsDataset()
        {
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            DataSet ds = new DataSet();
            try
            {
                ds = m_appsBllService.GetInvalidAppsList();
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }

        public JsonResult AppsValidDSToJSON()
        {
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

            DataSet ds = ValidAppsDataset();

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

        public JsonResult AppsInvalidDSToJSON()
        {
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

            DataSet ds = InvalidAppsDataset();

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
