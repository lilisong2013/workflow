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
    public class ElementsManagementController : Controller
    {
        //
        // GET: /ElementsManagement/

        public ActionResult AppElements()
        {
            return View();
        }
        /// <summary>
        /// 获取数据库中Elements表中所有有效的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetElements_Apply()
        {
            string msg = string.Empty;
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsService = new ElementsWebService.elementsBLLservice();
            WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
            WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();

            m_SecurityContext.UserName = m_usersModel.name;
            m_SecurityContext.PassWord = m_usersModel.password;
            m_SecurityContext.AppID =(int)m_usersModel.app_id;
            m_elementsService.SecurityContextValue = m_SecurityContext;

            int AppID = Convert.ToInt32(m_usersModel.app_id);
            //排序的字段名
            string sortname = Request.Params["sortname"];
            //排序的方向
            string sortorder = Request.Params["sortorder"];
            //当前页
            int page = Convert.ToInt32(Request.Params["page"]);
            //每页显示的记录数
            int pagesize = Convert.ToInt32(Request.Params["pagesize"]);


            DataSet ds = m_elementsService.GetElementsListOfApp(AppID, out msg);

            IList<WorkFlow.ElementsWebService.elementsModel> m_list=new List<WorkFlow.ElementsWebService.elementsModel>();
            var total = ds.Tables[0].Rows.Count;
            for (var i = 0; i < total; i++)
            {
                WorkFlow.ElementsWebService.elementsModel m_elementModel = (WorkFlow.ElementsWebService.elementsModel)Activator.CreateInstance(typeof(WorkFlow.ElementsWebService.elementsModel));
                PropertyInfo[] m_propertys = m_elementModel.GetType().GetProperties();
                foreach (PropertyInfo pi in m_propertys)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.Equals(ds.Tables[0].Columns[j].ColumnName))
                        {
                            // 数据库NULL值单独处理 
                            if (ds.Tables[0].Rows[i][j] != DBNull.Value)
                                pi.SetValue(m_elementModel, ds.Tables[0].Rows[i][j], null);
                            else
                                pi.SetValue(m_elementModel, null, null);
                            break;
                        }
                    }
                }
                m_list.Add(m_elementModel);
            }

            //模拟排序操作
            if (sortorder == "desc")
                m_list = m_list.OrderByDescending(c => c.id).ToList();
            else
                m_list = m_list.OrderBy(c => c.id).ToList();
            IList<WorkFlow.ElementsWebService.elementsModel> m_targetList = new List<WorkFlow.ElementsWebService.elementsModel>();
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
        /// <summary>
        /// 显示元素操作的详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailInfo(int id)
        {
            String msg = String.Empty;
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
            WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
            m_elementsModel = m_elementsBllService.GetModel(id,out msg);
            ViewData["elementsName"] = m_elementsModel.name;
            ViewData["elementsCode"] = m_elementsModel.code;
            ViewData["elementsRemark"] = m_elementsModel.remark;
            //ViewData["elementsInitstatus_id"] = m_elementsModel.initstatus_id;
            ViewData["elementsInitstatus_id"] = GetStatusValue(Convert.ToInt32(m_elementsModel.initstatus_id));
            ViewData["elementsSeqno"] = m_elementsModel.seqno;
            ViewData["elementsMenu_id"] = m_elementsModel.menu_id;
            ViewData["elementsApp_id"] = m_elementsModel.app_id;
            ViewData["elementsInvalid"] = m_elementsModel.invalid;
            ViewData["elementsDeleted"] = m_elementsModel.deleted;
            ViewData["elementsCreated_at"] = m_elementsModel.created_at;
            ViewData["elementsCreated_by"] = m_elementsModel.created_by;
            ViewData["elementsCreated_ip"] = m_elementsModel.created_ip;
            ViewData["elementsUpdated_at"] = m_elementsModel.updated_at;
            ViewData["elementsUpdated_by"] = m_elementsModel.updated_by;
            ViewData["elementsUpdated_ip"] = m_elementsModel.updated_ip;
            return View();
        }
        /// <summary>
        /// 根据状态ID返回状态值
        /// </summary>
        /// <param name="id">状态ID</param>
        /// <returns>状态值</returns>
        public string  GetStatusValue(int id)
        {
            if (id == 1)
            {
                return "可见";
            }
            if (id == 2)
            {
                return "不可见";
            }
            return "无效";
            
        }
        ///<summary>
        ///删除指定ID的操作
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePage(int id)
        {
            String msg = String.Empty;
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
            WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
            if (m_elementsBllService.Delete(id,out msg))
            {
                return RedirectToAction("AppElements");
            }
            else 
            {
                return View();
            }          
        }
        ///<summary>
        ///编辑元素的详细信息
        ///</summary>
        ///<returns></returns>
        public ActionResult EditPage(int id)
        {
            String msg = String.Empty;
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
            WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
            m_elementsModel = m_elementsBllService.GetModel(id,out msg);
            string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);
            m_elementsModel.updated_at = t;
            m_elementsModel.updated_by = m_elementsModel.created_by;
            m_elementsModel.updated_ip = m_elementsModel.created_ip;
            ViewData["elementsId"] = m_elementsModel.id;
            ViewData["elementsName"] = m_elementsModel.name;
            ViewData["elementsCode"] = m_elementsModel.code;
            ViewData["elementsRemark"] = m_elementsModel.remark;
            ViewData["elementsInitstatus_id"] = m_elementsModel.initstatus_id;
            ViewData["elementsSeqno"] = m_elementsModel.seqno;
            ViewData["elementsMenu_id"] = m_elementsModel.menu_id;
            ViewData["elementsApp_id"] = m_elementsModel.app_id;
            ViewData["elementsInvalid"] = m_elementsModel.invalid;
            ViewData["elementsDeleted"] = m_elementsModel.deleted;
            ViewData["elementsCreated_at"] = m_elementsModel.created_at;
            ViewData["elementsCreated_by"] = m_elementsModel.created_by;
            ViewData["elementsCreated_ip"] = m_elementsModel.created_ip;
            ViewData["elementsUpdated_at"] = m_elementsModel.updated_at;
            ViewData["elementsUpdated_by"] = m_elementsModel.updated_by;
            ViewData["elementsUpdated_ip"] = m_elementsModel.updated_ip;
            return View();
        }
        /// <summary>
        /// 添加元素操作
        /// </summary>
        /// <returns></returns>
        public ActionResult AddElements(FormCollection collection)
        {
            string msg = string.Empty;
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
            WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();

            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
            int appID = Convert.ToInt32(m_usersModel.app_id);
            
            string str=Request.Form["MenusParent"];
            string name = collection["elementsName"].Trim();
            string code = collection["elementsCode"].Trim();
            string  initstatusid = (collection["StatusParent"].Trim());
            //string  menuid = (collection["MenuParent"].Trim());
            string menuid = Request.Form["MenusParent"];
            string  seqno =(collection["elementsSeqno"].Trim());
            if (name.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "元素名称不能为空!" });
            }
            if (code.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "元素编码不能空!" });
            }
            if (initstatusid.Length == 0 || initstatusid.Equals("请选择"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "初始化状态不能为空!" });
            }
            if (menuid.Length == 0||menuid.Equals("顶级菜单"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "父菜单不能为空!" });
            }
            if (seqno.Length== 0) 
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="排序码不能为空!" });
            }
            int menuID = Convert.ToInt32(collection["MenusParent"]);
            DataSet ds = m_elementsBllService.GetAllElementsListOfMenuApp(appID,menuID,out msg);
            ArrayList elementsList = new ArrayList();
            var total = ds.Tables[0].Rows.Count;
            for (int i = 0; i < total; i++)
            {
                elementsList.Add(ds.Tables[0].Rows[i][1].ToString());
            }
            foreach (string elementslist in elementsList)
            {
                if (elementslist.Equals(collection["elementsName"].Trim().ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的元素名称!" });
                }
            }

            DataSet codeds = m_elementsBllService.GetCodeListOfMenuApp(appID,menuID,out msg);
            ArrayList codeList = new ArrayList();
            var codetotal = codeds.Tables[0].Rows.Count;
            for (int i = 0; i < codetotal; i++)
            {
                codeList.Add(codeds.Tables[0].Rows[i][0]);
            }
            foreach (string codename in codeList)
            {
                if (codename.Equals(collection["elementsCode"].Trim().ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的编码名称!"});
                }
            }
            m_elementsModel.name = collection["elementsName"].Trim();
            m_elementsModel.code = collection["elementsCode"].Trim();
            m_elementsModel.remark = collection["elementsRemark"].Trim();
            m_elementsModel.initstatus_id = Convert.ToInt32(collection["StatusParent"].Trim());
            m_elementsModel.seqno = Convert.ToInt32(collection["elementsSeqno"].Trim());
            m_elementsModel.menu_id = Convert.ToInt32(collection["MenusParent"].Trim());
            m_elementsModel.app_id = Convert.ToInt32(collection["elementsApp_id"].Trim());
            m_elementsModel.invalid = Convert.ToBoolean(collection["elementsInvalid"].Trim());
            m_elementsModel.deleted = Convert.ToBoolean(collection["elementsDeleted"].Trim());
            m_elementsModel.created_at=Convert.ToDateTime(collection["Created_at"].Trim());
            m_elementsModel.created_by = Convert.ToInt32(collection["Created_by"].Trim());
            m_elementsModel.created_ip=collection["Created_ip"].Trim();
            try
            {
                if (m_elementsBllService.Add(m_elementsModel,out msg) != 0)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "添加成功", toUrl = "/ElementsManagement/AppElements" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="添加失败" });
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序出错!"});
            }
            
          
        }
        ///<summary>
        ///获得系统ID的下拉列表框
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetAppId()
        {
            WorkFlow.AppsWebService.appsBLLservice m_appsBllService = new AppsWebService.appsBLLservice();
            WorkFlow.AppsWebService.appsModel m_appsModel = new AppsWebService.appsModel();
            DataSet ds = m_appsBllService.GetAllAppsList();
            List<Saron.WorkFlow.Models.AppIDHelper> m_appidlist = new List<Saron.WorkFlow.Models.AppIDHelper>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                m_appidlist.Add(new Saron.WorkFlow.Models.AppIDHelper { AppID=Convert.ToInt32(ds.Tables[0].Rows[i][0]) });
            }
            var dataJson=new{
            Rows=m_appidlist,
            Total=ds.Tables[0].Rows.Count
            };
            return Json(dataJson,JsonRequestBehavior.AllowGet);
        }
        ///<summary>
        ///获得初始化状态的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetStatusName()
        {
            WorkFlow.Init_StatusWebService.init_statusBLLservice m_init_statusBllService = new Init_StatusWebService.init_statusBLLservice();
            WorkFlow.Init_StatusWebService.init_statusModel m_init_statusModel = new Init_StatusWebService.init_statusModel();
            DataSet ds = m_init_statusBllService.GetAllInit_StatusList();
            List<Saron.WorkFlow.Models.InitStatusIDHelper> m_elementlist = new List<Saron.WorkFlow.Models.InitStatusIDHelper>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {               
                    m_elementlist.Add(new Saron.WorkFlow.Models.InitStatusIDHelper { InitStatusID = Convert.ToInt32(ds.Tables[0].Rows[i][0]), InitStatusName = Convert.ToString(ds.Tables[0].Rows[i][1])});               
            }
               var dataJson = new { 
                Rows=m_elementlist,
                Total = ds.Tables[0].Rows.Count
            };
            return Json(dataJson, JsonRequestBehavior.AllowGet);
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

            var dataJson = new
            {
                Rows = m_menuslist,
                Total = ds.Tables[0].Rows.Count
            };
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult EditElements(FormCollection collection)
        {
           
            String msg = String.Empty;
            WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
            WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();

            WorkFlow.UsersWebService.usersModel codeModel=(WorkFlow.UsersWebService.usersModel)Session["user"];
           
            m_elementsModel = m_elementsBllService.GetModel(Convert.ToInt32(collection["elementsId"].Trim()),out msg);
            int appID = Convert.ToInt32(codeModel.app_id);
            int menuID = Convert.ToInt32(collection["elementsMenu_id"]);

            string name = collection["elementsName"].Trim();
            string code = collection["elementsCode"].Trim();
            string Initstatus_id = collection["elementsInitstatus_id"].Trim();
            string Menu_id = collection["elementsMenu_id"].Trim();
            string invalid = collection["elementsInvalid"].Trim();
            if (name.Length == 0)
            {      
               return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "元素名称不能为空!" });
            }
            if (code.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success=false,css="p-errorDIV",message="元素编码不能空!"});
            }
            if (Initstatus_id.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success=false,css="p-errorDIV",message="初始化状态码不能为空!"});
            }
            if (Menu_id.Length == 0)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel { success=false,css="p-errorDIV",message="页面ID不能为空!"});
            }
            if (invalid.Length == 0 || invalid.Equals("请选择"))
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="是否有效不能为空!"});
            }
            DataSet ds = m_elementsBllService.GetElementsListOfApp(appID,out msg);
            var total = ds.Tables[0].Rows.Count;
            ArrayList elementsList = new ArrayList();
            for (int i = 0; i < total; i++)
            {
                elementsList.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            for (int i = 0; i < total; i++)
            {  //修改后的名称和原名称相同          
                if (m_elementsModel.name.ToString().Equals(collection["elementsName"].Trim().ToString()))
                {
                    elementsList.Remove(m_elementsModel.name);
                }
            }
            DataSet codeds = m_elementsBllService.GetCodeListOfMenuApp(appID,menuID,out msg);
            ArrayList codeList = new ArrayList();
            var codetotal = codeds.Tables[0].Rows.Count;
            for(int i = 0; i < codetotal; i++)
            {
                codeList.Add(codeds.Tables[0].Rows[i][0].ToString());
            }
            for (int i = 0; i < codetotal; i++)
            { //修改后的名称和原名称相同
                if (m_elementsModel.code.ToString().Equals(collection["elementsCode"].Trim().ToString()))
                {
                    codeList.Remove(m_elementsModel.code);
                }
            }
            String s = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();
            DateTime t = Convert.ToDateTime(s);
            WorkFlow.UsersWebService.usersModel m_usersModel=(WorkFlow.UsersWebService.usersModel)Session["user"];

            m_elementsModel.name = collection["elementsName"].Trim();
            m_elementsModel.code = collection["elementsCode"].Trim();
            m_elementsModel.remark = collection["elementsRemark"].Trim();
            m_elementsModel.initstatus_id = Convert.ToInt32(collection["elementsInitstatus_id"].Trim().ToString());
            m_elementsModel.seqno = Convert.ToInt32(collection["elementsSeqno"].Trim().ToString());
            m_elementsModel.menu_id = Convert.ToInt32(collection["elementsMenu_id"].Trim().ToString());
            m_elementsModel.app_id = Convert.ToInt32(collection["elementsApp_id"].Trim().ToString());
            m_elementsModel.invalid = Convert.ToBoolean(collection["elementsInvalid"].Trim().ToString());
            m_elementsModel.deleted = Convert.ToBoolean(collection["elementsDeleted"].Trim().ToString());
            m_elementsModel.updated_at = t;
            m_elementsModel.updated_by = Convert.ToInt32(m_usersModel.id);
            m_elementsModel.updated_ip = collection["elementsCreated_ip"].Trim();
            foreach (string elementsName in elementsList)
            {//如果修改后的名称与数据表中的名称相同
                if (elementsName.Equals(m_elementsModel.name.ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的元素名称!" });
                }
            }
            foreach (string codeName in codeList)
            {//如果修改后的编码与数据库中的编码相同 
                if (codeName.Equals(m_elementsModel.code.ToString()))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的编码!"});
                }
            }
            try
            {
                if (m_elementsBllService.Update(m_elementsModel,out msg))
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "修改成功!", toUrl = "/ElementsManagement/AppElements" });
                }
                else
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="修改失败!"});
                }
            }
            catch (Exception ex)
            {
                return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="程序异常!"});
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
