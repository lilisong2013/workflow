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
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// 获取数据库中Elements表中所有有效的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetElements_Apply()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsService.SecurityContextValue = m_SecurityContext;

                int AppID = Convert.ToInt32(m_usersModel.app_id);
                ////排序的字段名
                //string sortname = Request.Params["sortname"];
                ////排序的方向
                //string sortorder = Request.Params["sortorder"];
                ////当前页
                //int page = Convert.ToInt32(Request.Params["page"]);
                ////每页显示的记录数
                //int pagesize = Convert.ToInt32(Request.Params["pagesize"]);


                DataSet ds = m_elementsService.GetElementsListOfApp(AppID, out msg);
                string data = "{Rows:[";
                if (ds == null)
                {
                    return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "无权访问WebService！" });
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = ds.Tables[0].Rows[i][1].ToString();
                            string id = ds.Tables[0].Rows[i][0].ToString();
                            string code = ds.Tables[0].Rows[i][2].ToString();
                            if (i == ds.Tables[0].Rows.Count - 1)
                            {
                                data += "{name:'" + name + "',";
                                data += "id:'" + id + "',";
                                data += "code :'" + code + "'}";
                            }
                            else
                            {
                                data += "{name:'" + name + "',";
                                data += "id:'" + id + "',";
                                data += "code:'" + code + "'},";
                            }
                        }
                    }
                    catch (Exception ex) { }
                    data += "]}";
                    return Json(data);
            }
         
          }
          
        }

        //后台分页显示元素列表
        public ActionResult GetElements_List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                //排序的字段名
                string sortname = Request.Params["sortname"];
                //排序的方向
                string sortorder = Request.Params["sortorder"];
                //当前页
                int page = Convert.ToInt32(Request.Params["page"]);
                //每页显示的记录数
                int pagesize = Convert.ToInt32(Request.Params["pagesize"]);

                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsService.SecurityContextValue = m_SecurityContext;
                DataSet ds = m_elementsService.GetElementsListOfApp((int)m_usersModel.app_id,out msg);

                IList<WorkFlow.ElementsWebService.elementsModel> m_list = new List<WorkFlow.ElementsWebService.elementsModel>();
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
        }
        /// <summary>
        /// 显示元素操作的详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailInfo(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                m_elementsModel = m_elementsBllService.GetModel(id, out msg);
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
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                //WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                if (m_elementsBllService.Delete(id, out msg))
                {
                    return RedirectToAction("AppElements");
                }
                else
                {
                    return View();
                }          
            }
          
        }

        //删除一条记录
        public ActionResult DeleteElement()
        {
            Boolean flag=false;
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
               
                string msg = string.Empty;
                int elementID = Convert.ToInt32(Request.Form["elementsID"]);
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;


                WorkFlow.PrivilegesWebService.privilegesBLLservice m_privilegesBllService = new PrivilegesWebService.privilegesBLLservice();
                WorkFlow.PrivilegesWebService.SecurityContext me_SecurityContext = new PrivilegesWebService.SecurityContext();

                me_SecurityContext.UserName = m_usersModel.login;
                me_SecurityContext.PassWord = m_usersModel.password;
                me_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_privilegesBllService.SecurityContextValue = me_SecurityContext;

                DataSet eds = m_privilegesBllService.GetItemIDOfPrivileges((int)m_usersModel.app_id,2,out msg);
                var total = eds.Tables[0].Rows.Count;
                ArrayList eidlist = new ArrayList();
               
               //权限项目列表中元素项目加入到元素列表中
               if(total!=0)
                {
                    for (int i = 0; i < total; i++)
                    {
                        eidlist.Add(eds.Tables[0].Rows[i][3]);
                    }
                }

                //判断一下当前元素ID是否存在权限项目中元素ID列表中
                foreach (int idlist in eidlist)
                {
                    if (idlist.Equals(elementID))
                    {
                        flag = true;//存在
                    }
                    else
                    {
                        flag = false;//不存在
                    }
                }


                try
                {
                    //不存在权限项目中可以删除
                    if (flag==false)
                    {

                        if (m_elementsBllService.Delete(elementID, out msg))
                        {

                            return Json("{success:true,css:'alert alert-success',message:'成功删除元素!'}");
                        }
                        else
                        {

                            return Json("{success:false,css:'alert alert-error',message:'元素删除失败!'}");
                        }
                    }
                   //存在权限项目中，不能删除
                    else
                    {
                        return Json("{success:false,css:'alert alert-error',message:'元素已经在权限项目中设置，不能删除!'}");
                    }
                    
                }
                catch (Exception ex)
                {

                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }
            }
           

        }
        
        //获取是否有效的列表
        public ActionResult GetInvalidList()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int m_elementID = Convert.ToInt32(Request.Params["elementsID"]);//元素ID
                string strJson = "{List:[";//"{List:[{name:'删除',id:'1',selected:'true'},{name:'删除',id:'1',selected:'true'}],total:'2'}";
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                WorkFlow.ElementsWebService.elementsModel m_elementModel = m_elementsBllService.GetModel(m_elementID, out msg);
                string m_selected = string.Empty;
                int total = 1;
                string m_InvalidName = "是";
                int m_elementsID = m_elementID;
                //判断"元素"中是否有效
                if (m_elementModel.invalid == false)
                {
                    m_selected = "true";
                }
                else
                {
                    m_selected = "false";
                }
                strJson += "{id:'" + m_elementsID + "',";
                strJson += "name:'" + m_InvalidName + "',";
                strJson += "selected:'" + m_selected + "'}";

                strJson += "],total:'" + total + "'}";
                return Json(strJson);
            }
           
        }
        ///<summary>
        ///编辑元素的详细信息
        ///</summary>
        ///<returns></returns>
        public ActionResult EditPage(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
                WorkFlow.MenusWebService.SecurityContext m_MSecurityContext = new MenusWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                //元素授权验证
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;
                //菜单授权验证
                m_MSecurityContext.UserName = m_usersModel.login;
                m_MSecurityContext.PassWord = m_usersModel.password;
                m_MSecurityContext.AppID = (int)m_usersModel.app_id;
                m_menusBllService.SecurityContextValue = m_MSecurityContext;
                //根据ID得到元素实体
                WorkFlow.ElementsWebService.elementsModel m_elementsModel = m_elementsBllService.GetModel(id, out msg);
                string s = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);
               
                m_elementsModel.updated_at = t;
                m_elementsModel.updated_by = m_elementsModel.created_by;
                m_elementsModel.updated_ip = m_elementsModel.created_ip;
                ViewData["elementsId"] = m_elementsModel.id;
                ViewData["elementsName"] = m_elementsModel.name;
                ViewData["elementsCode"] = m_elementsModel.code;
                ViewData["elementsRemark"] = m_elementsModel.remark;
                if (m_elementsModel.initstatus_id == 1)
                {
                    ViewData["elementsInitstatus_id"] = "可见";
                }
                if (m_elementsModel.initstatus_id == 2)
                {
                    ViewData["elementsInitstatus_id"] = "不可见";
                }
                if (m_elementsModel.initstatus_id == 3)
                {
                    ViewData["elementsInitstatus_id"] = "无效";
                }
                ViewData["elementsSeqno"] = m_elementsModel.seqno;
                ViewData["elementsMenu_id"] = m_elementsModel.menu_id;

                int menuID = Convert.ToInt32(m_elementsModel.menu_id);
                DataSet mds = m_menusBllService.GetMenuNameOfAppID((int)m_usersModel.app_id,menuID,out msg);
                ViewData["elementsMenu_IDName"]=mds.Tables[0].Rows[0][0];
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
           
        }
        /// <summary>
        /// 添加元素操作
        /// </summary>
        /// <returns></returns>
        public ActionResult AddElements(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
                WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];
                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                string name = collection["elementsName"].Trim();
                string code = collection["elementsCode"].Trim();
                string initstatusid = (collection["StatusParent"].Trim());
                string menuid = Request.Form["eElementPage"];
                string seqno = (collection["elementsSeqno"].Trim());
                if (name.Length == 0)
                {
          
                    return Json("{success:false,css:'alert alert-error',message:'元素名称不能为空!'}");
                }
                if (code.Length == 0)
                {

                    return Json("{success:false,css:'alert alert-error',message:'元素编码不能空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsCode(code) == false)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'元素编码以字母开头!'}");
                }
                if (initstatusid.Length == 0 || initstatusid.Equals("请选择"))
                {
                 
                    return Json("{success:false,css:'alert alert-error',message:'初始化状态不能为空!'}");
                }
                if (menuid.Length == 0 || menuid.Equals("-1"))
                {

                    return Json("{success:false,css:'alert alert-error',message:'所在页面不能为空'}");
                }
                if (seqno.Length == 0)
                {

                    return Json("{success:false,css:'alert alert-error',message:'排序码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsNumber(seqno) == false)
                {
                
                    return Json("{success:false,css:'alert alert-error',message:'排序码只能是数字!'}");
                }
                int appID = Convert.ToInt32(m_usersModel.app_id);
                int menuID = Convert.ToInt32(collection["eElementPage"]);
                DataSet ds = m_elementsBllService.GetAllElementsListOfMenuApp(appID, menuID, out msg);
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
                      
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的元素名称!'}");
                    }
                }

                DataSet codeds = m_elementsBllService.GetCodeListOfMenuApp(appID, menuID, out msg);

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
                      
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的编码名称!'}");
                    }
                }
                m_elementsModel.name = collection["elementsName"].Trim();
                m_elementsModel.code = collection["elementsCode"].Trim();
                m_elementsModel.remark = collection["elementsRemark"].Trim();
                m_elementsModel.initstatus_id = Convert.ToInt32(collection["StatusParent"].Trim());
                m_elementsModel.seqno = Convert.ToInt32(collection["elementsSeqno"].Trim());
                m_elementsModel.menu_id = Convert.ToInt32(collection["eElementPage"].Trim());
                m_elementsModel.app_id = Convert.ToInt32(collection["elementsApp_id"].Trim());
                m_elementsModel.created_at = Convert.ToDateTime(collection["Created_at"].Trim());
                m_elementsModel.created_by = Convert.ToInt32(collection["Created_by"].Trim());
                m_elementsModel.created_ip = collection["Created_ip"].Trim();
                try
                {
                    if (m_elementsBllService.Add(m_elementsModel, out msg) != 0)
                    {
                    
                        return Json("{success:true,css:'alert alert-success',message:'元素添加成功!'}");
                    }
                    else
                    {
                      
                        return Json("{success:false,css:'alert alert-error',message:'添加失败!'}");
                    }
                }
                catch (Exception ex)
                {
                   
                    return Json("{success:false,css:'alert alert-error',message:'程序出错!'}");
                }     
            }
            
        }

        ///<summary>
        ///获得初始化状态的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetStatusName()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.Init_StatusWebService.init_statusBLLservice m_init_statusBllService = new Init_StatusWebService.init_statusBLLservice();
                WorkFlow.Init_StatusWebService.init_statusModel m_init_statusModel = new Init_StatusWebService.init_statusModel();
                DataSet ds = m_init_statusBllService.GetAllInit_StatusList();
                List<Saron.WorkFlow.Models.InitStatusIDHelper> m_elementlist = new List<Saron.WorkFlow.Models.InitStatusIDHelper>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    m_elementlist.Add(new Saron.WorkFlow.Models.InitStatusIDHelper { InitStatusID = Convert.ToInt32(ds.Tables[0].Rows[i][0]), InitStatusName = Convert.ToString(ds.Tables[0].Rows[i][1]) });
                }
                var dataJson = new
                {
                    Rows = m_elementlist,
                    Total = ds.Tables[0].Rows.Count
                };
                return Json(dataJson, JsonRequestBehavior.AllowGet);
            }
          
        }

        /// <summary>
        /// 获得菜单的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetMenusName()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
                WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                string msg = string.Empty;

                WorkFlow.MenusWebService.SecurityContext m_securityContext = new MenusWebService.SecurityContext();
                //SecurityContext实体对象赋值
                m_securityContext.UserName = m_usersModel.login;
                m_securityContext.PassWord = m_usersModel.password;
                m_securityContext.AppID = (int)m_usersModel.app_id;
                m_menusBllService.SecurityContextValue = m_securityContext;//实例化 [SoapHeader("m_securityContext")]


                //获取Menu表中所有的id列表
                DataSet ds = m_menusBllService.GetAllMenusListofApp((int)m_usersModel.app_id, out msg);


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
          
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult EditElements(FormCollection collection)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string msg = string.Empty;
                int m_ev_Total = Convert.ToInt32(Request.Form["ev_Total"]);
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.elementsModel m_elementsModel = new ElementsWebService.elementsModel();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel codeModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = codeModel.login;
                m_SecurityContext.PassWord = codeModel.password;
                m_SecurityContext.AppID = (int)codeModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                m_elementsModel = m_elementsBllService.GetModel(Convert.ToInt32(collection["elementsId"].Trim()), out msg);
                int appID = Convert.ToInt32(codeModel.app_id);
                int menuID = Convert.ToInt32(collection["elementsMenu_id"]);
                //int menuID = Convert.ToInt32(Request.Form["eElementPage"]);
                string name = collection["elementsName"].Trim();
                string code = collection["elementsCode"].Trim();
                string Initstatus_id = collection["elementsInitstatus_id"].Trim();
                //string Menu_id = Request.Form["eElementPage"];
                string Menu_id = collection["elementsMenu_id"].Trim();
                string seqno = collection["elementsSeqno"].Trim();
                if (name.Length == 0)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'元素名称不能为空!'}");
                }
                if (code.Length == 0)
                {
                 
                    return Json("{success:false,css:'alert alert-error',message:'元素编码不能空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsCode(code) == false)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'元素编码以字母开头!'}");
                }
                if (Initstatus_id.Length == 0)
                {
                  
                    return Json("{success:false,css:'alert alert-error',message:'初始化状态码不能为空!'}");
                }
                if (seqno.Length == 0)
                {
                    return Json("{success:false,css:'alert alert-error',message:'排序码不能为空!'}");
                }
                if (Saron.Common.PubFun.ConditionFilter.IsNumber(seqno) == false)
                {
                    return Json("{success:false,css:'alert alert-error',message:'排序码只能为数字!'}");
                }

                //判断元素名称重名处理操作
                DataSet ds = m_elementsBllService.GetElementsListOfApp(appID, out msg);
                var total = ds.Tables[0].Rows.Count;
                ArrayList elementsList = new ArrayList();
                for (int i = 0; i < total; i++)
                {
                    elementsList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                for (int i = 0; i < total; i++)
                {  //修改后的名称和原名称相同          
                    if (m_elementsModel.name.ToString().Equals(collection["elementsName"].Trim().ToString()))
                    {
                        elementsList.Remove(m_elementsModel.name);
                    }
                }
                //判断元素编码重名处理操作
                DataSet codeds = m_elementsBllService.GetCodeListOfMenuApp(appID, menuID, out msg);
                ArrayList codeList = new ArrayList();
                var codetotal = codeds.Tables[0].Rows.Count;
                for (int i = 0; i < codetotal; i++)
                {
                    codeList.Add(codeds.Tables[0].Rows[i][0].ToString());
                }
                for (int i = 0; i < codetotal; i++)
                { //修改后的编码和原编码相同
                    if (m_elementsModel.code.ToString().Equals(collection["elementsCode"].Trim().ToString()))
                    {
                        codeList.Remove(m_elementsModel.code);
                    }
                }

                String s = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();
                DateTime t = Convert.ToDateTime(s);
                WorkFlow.UsersWebService.usersModel m_usersModel = (WorkFlow.UsersWebService.usersModel)Session["user"];

                m_elementsModel.name = collection["elementsName"].Trim();
                m_elementsModel.code = collection["elementsCode"].Trim();
                m_elementsModel.remark = collection["elementsRemark"].Trim();
                if (collection["elementsInitstatus_id"].Trim().ToString() == "可见")
                {
                    m_elementsModel.initstatus_id = 1;
                }
                if (collection["elementsInitstatus_id"].Trim().ToString() == "不可见")
                {
                    m_elementsModel.initstatus_id = 2;
                }
                if (collection["elementsInitstatus_id"].Trim().ToString() == "无效")
                {
                    m_elementsModel.initstatus_id = 3;
                }
                m_elementsModel.seqno = Convert.ToInt32(collection["elementsSeqno"].Trim().ToString());
                //m_elementsModel.menu_id = Convert.ToInt32(Request.Form["eElementPage"]);
                m_elementsModel.menu_id = Convert.ToInt32(collection["elementsMenu_id"].Trim().ToString());
                m_elementsModel.app_id = Convert.ToInt32(collection["elementsApp_id"].Trim().ToString());
                if (m_ev_Total == 1)
                {
                    m_elementsModel.invalid = false;
                }
                if (m_ev_Total == 0)
                {
                    m_elementsModel.invalid = true;
                }

                m_elementsModel.deleted = Convert.ToBoolean(collection["elementsDeleted"].Trim().ToString());
                m_elementsModel.updated_at = t;
                m_elementsModel.updated_by = Convert.ToInt32(m_usersModel.id);
                m_elementsModel.updated_ip = collection["elementsCreated_ip"].Trim();

                foreach (string elementsName in elementsList)
                {//如果修改后的名称与数据表中的名称相同
                    if (elementsName.Equals(m_elementsModel.name.ToString()))
                    {
                 
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的元素名称!'}");
                    }
                }
                foreach (string codeName in codeList)
                {//如果修改后的编码与数据库中的编码相同 
                    if (codeName.Equals(m_elementsModel.code.ToString()))
                    {
       
                        return Json("{success:false,css:'alert alert-error',message:'已经存在相同的编码!'}");
                    }
                }
                try
                {
                    if (m_elementsBllService.Update(m_elementsModel, out msg))
                    {
                       
                        return Json("{success:true,css:'alert alert-success',message:'元素修改成功!'}");
                    }
                    else
                    {
              
                        return Json("{success:false,css:'alert alert-error',message:'元素修改失败!'}");
                    }
                }
                catch (Exception ex)
                {

                    return Json("{success:false,css:'alert alert-error',message:'程序异常!'}");
                }          
            
            }
       
        }

        //根据元素名称实现模糊查询
        public ActionResult GetListByElementName(string elementName)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Home", "Login");
            }
            else
            {
                string msg = string.Empty;
                WorkFlow.ElementsWebService.elementsBLLservice m_elementsBllService = new ElementsWebService.elementsBLLservice();
                WorkFlow.ElementsWebService.SecurityContext m_SecurityContext = new ElementsWebService.SecurityContext();

                WorkFlow.UsersWebService.usersModel m_usersModel =(WorkFlow.UsersWebService.usersModel)Session["user"];

                m_SecurityContext.UserName = m_usersModel.login;
                m_SecurityContext.PassWord = m_usersModel.password;
                m_SecurityContext.AppID = (int)m_usersModel.app_id;
                m_elementsBllService.SecurityContextValue = m_SecurityContext;

                //如果搜索元素名称为空，显示全部元素列表
                if (elementName.Length == 0)
                {
                    DataSet ds = m_elementsBllService.GetElementsListOfApp((int)m_usersModel.app_id,out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {

                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else 
                    {
                        try {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string code = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "'},";
                                }
                            }

                        }
                        catch (Exception ex) { }
                        data += "]}";
                        return Json(data);
                    }
                }
                //如果搜索元素名称不为空，显示相应的元素列表
                else
                {
                    DataSet ds = m_elementsBllService.GetListByOperationName(elementName,(int)m_usersModel.app_id, out msg);
                    string data = "{Rows:[";
                    if (ds == null)
                    {

                        return Json("{success:false,css:'alert alert-error',message:'无权访问WebService！'}");
                    }
                    else
                    {
                        try {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string name = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                string id = Convert.ToString(ds.Tables[0].Rows[i][0]);
                                string code = Convert.ToString(ds.Tables[0].Rows[i][2]);
                                if (i == ds.Tables[0].Rows.Count - 1)
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "'}";
                                }
                                else
                                {
                                    data += "{name:'" + name + "',";
                                    data += "id:'" + id + "',";
                                    data += "code:'" + code + "'},";
                                }
                            }
                        }
                        catch (Exception ex) { }
                        data += "]}";
                        return Json(data);
                    }
                }
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
