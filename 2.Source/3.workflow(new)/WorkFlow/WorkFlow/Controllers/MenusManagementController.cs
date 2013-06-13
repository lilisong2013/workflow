using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;

namespace WorkFlow.Controllers
{
    public class MenusManagementController : Controller
    {
        //
        // GET: /MenusManagement/

        public ActionResult AppMenus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                return View();
            }
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
            int id = m_usersModel.id;
            int appid =Convert.ToInt32(m_usersModel.app_id);
            //parent_id
            int parentid = Convert.ToInt32(Request.Form["MenusParent"]);
            string str = Request.Form["MenusParent"];
            //获得app_id系统所有的id,parent_id列表
            DataSet ds1= m_menusBllService.GetAllParentIdOfApp(appid);
            var total1 = ds1.Tables[0].Rows.Count;
            ArrayList IdList = new ArrayList();
            ArrayList parentList = new ArrayList();
            for (int i = 0; i < total1; i++)
             {
                 IdList.Add(ds1.Tables[0].Rows[i][0]);
                 parentList.Add(ds1.Tables[0].Rows[i][1]);
             }
                try
                {
                    if (Request.Form["MenusName"] == "")
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "<i class='icon-check'></i>菜单名不能为空" });
                    }
                    if (Request.Form["MenusCode"] == "")
                    {
                        return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-errorDIV", message = "<i class='icon-check'></i>菜单编码不能为空" });
                    }
                    //获得系统为appid的顶级菜单
                    DataSet pds = m_menusBllService.GetTopMenusListOfApp(appid);
                    int ptotal = pds.Tables[0].Rows.Count;
                    ArrayList pidList = new ArrayList();
                    ArrayList pnameList = new ArrayList();
                    ArrayList pcodeList = new ArrayList();
                    for (int i = 0; i < ptotal; i++)
                    {
                        pidList.Add(pds.Tables[0].Rows[i][0]);
                        pnameList.Add(pds.Tables[0].Rows[i][1]);
                        pcodeList.Add(pds.Tables[0].Rows[i][2]);
                    }
                   
                 
                         foreach (string pnamelist in pnameList)
                         {
                             if (pnamelist.Equals(Request.Form["MenusName"]))
                             {
                                 return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的菜单名称!" });
                             }
                         }
                         foreach (string pcodelist in pcodeList)
                         {
                             if (pcodelist.Equals(Request.Form["MenusCode"]))
                             {
                                 return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "已经存在相同的菜单编码" });
                             }
                         }
                  
                     //获得顶级菜单的ID
                          int[] pcount=new int[ptotal];                    
                          for (int i = 0; i < ptotal; i++)
                          {
                              pcount[i] = Convert.ToInt32(pds.Tables[0].Rows[i][0].ToString());
                          }
                          int count=0;
                          while (count < ptotal)
                          {    //如果下面有子菜单
                              ArrayList cIdList = new ArrayList();
                              ArrayList cNameList = new ArrayList();
                              ArrayList cCodeList = new ArrayList();
                           
                              while(m_menusBllService.ExistsChildrenMenus(pcount[count]) == true)
                              { //获取子菜单
                                  DataSet clist = m_menusBllService.GetChildrenMenus(pcount[count]);
                                  int ID=0;
                                  for (int j = 0; j < 1; j++)
                                  {
                                      pcount[count] = Convert.ToInt32(clist.Tables[0].Rows[j][0]);
                                      ID= Convert.ToInt32(clist.Tables[0].Rows[j][0]);
                                      string cname = Convert.ToString(clist.Tables[0].Rows[j][1]);
                                      string ccode = Convert.ToString(clist.Tables[0].Rows[j][2]);
                                      if (cname.Equals(Request.Form["MenusName"]))
                                      {
                                          return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的菜单名称"});
                                      }
                                      if (ccode.Equals(Request.Form["MenusCode"]))
                                      {
                                          return Json(new Saron.WorkFlow.Models.InformationModel {success=false,css="p-errorDIV",message="已经存在相同的菜单编码" });
                                      }
                                      cIdList.Add(clist.Tables[0].Rows[j][0]);
                                      cNameList.Add(clist.Tables[0].Rows[j][1]);
                                      cCodeList.Add(clist.Tables[0].Rows[j][2]);
                                      
                                      //ID = Convert.ToInt32(clist.Tables[0].Rows[j][0]);
                                  }
                             
                                 
                              }
                          

                              cIdList.Clear();
                              cCodeList.Clear();
                              cNameList.Clear();
                              count++;
                          } 
                    
                    m_menusModel.name = Request.Form["MenusName"];
                    m_menusModel.code = Request.Form["MenusCode"];
                    m_menusModel.url = Request.Form["MenusUrl"];
                    m_menusModel.app_id = m_usersModel.app_id;//系统ID

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
       ///<summary>
       ///删除菜单
       /// </summary>
        public ActionResult ChangePage(int id)
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel =m_menusBllService.GetModel(id);
            //如果有孩子节点
            if (m_menusBllService.ExistsChildrenMenus(id) == true)
            {
                //return Json(new Saron.WorkFlow.Models.InformationModel { success = false, css = "p-errorDIV", message = "本节点不是叶子节点，不能删除!" });
                return RedirectToAction("AppMenus");
            }
            else
            {
               // return Json(new Saron.WorkFlow.Models.InformationModel { success = true, css = "p-successDIV", message = "删除成功!", toUrl = "/MenusManagement/AppMenus" });
                if (m_menusBllService.DeleteID(id))
                {
                    return RedirectToAction("AppMenus");
                }
                else
                {
                    return RedirectToAction("AppMenus");
                }
            }
           
        }
        //删除菜单
        public ActionResult DeleteMenus(int id)
        {
            string a = id.ToString();
            return RedirectToAction("AppMenus");
        }

        //获取菜单列表（在grid中显示）
        public ActionResult GetMenusList()
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_userModel = (UsersWebService.usersModel)Session["user"];
            string data = "{Rows:[";
            try
            {
                DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_userModel.app_id);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string code=ds.Tables[0].Rows[i][2].ToString();
                    string url = ds.Tables[0].Rows[i][3].ToString();
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                        data += "code:'" + code + "',";
                        data += "url:'" + url + "',";
                        data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "}";
                    }
                    else
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                        data += "code:'" + code + "',";
                        data += "url:'" + url + "',";
                        data += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}";
            return Json(data);
        }

        /// <summary>
        /// 获得菜单的下拉列表
        /// </summary>
        /// <returns>json数据</returns>
        public ActionResult GetMenus()
        {
            //string data = "[{name:'顶级菜单',id:'-1',children:[{ name: '部门1',id:'1',children:[{ name: '角色1.1',id:'2'},{ name: '角色1.2',id:'3'}] },{ name: '部门2',id:'4'},{ name: '部门3',id:'5'}]}]";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();
            WorkFlow.UsersWebService.usersModel m_userModel = (UsersWebService.usersModel)Session["user"];
            string data="[{name:'顶级菜单',id:'-1',children:[";
            try
            {
                DataSet ds = m_menusBllService.GetTopMenusListOfApp((int)m_userModel.app_id);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "}";
                    }
                    else
                    {
                        data += "{name:'" + name + "',";
                        data += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "},";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            data += "]}]";
            return Json(data);
        }

        //菜单树
        public string GetChildrenMenus(int parentId)
        {
            string dataStr= ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            if (m_menusBllService.ExistsChildrenMenus(parentId))
            {
                DataSet ds = m_menusBllService.GetChildrenMenus(parentId);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0]))
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'" + GetChildrenMenus(Convert.ToInt32(id)) + "},";
                        }
                    }
                    else
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "'},";
                        }
                    }
                }
            }
            dataStr += "]";
            return dataStr;
        }

        //菜单Grid
        public string GetChildrenMenusList(int parentId)
        {
            string dataStr = ",children:[";
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = new MenusWebService.menusModel();

            if (m_menusBllService.ExistsChildrenMenus(parentId))
            {
                DataSet ds = m_menusBllService.GetChildrenMenus(parentId);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string name = ds.Tables[0].Rows[i][1].ToString();
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string code = ds.Tables[0].Rows[i][2].ToString();
                    string url = ds.Tables[0].Rows[i][3].ToString();
                    string remark = ds.Tables[0].Rows[i][6].ToString();
                    if (m_menusBllService.ExistsChildrenMenus((int)ds.Tables[0].Rows[i][0]))
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "}";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',"; //+ GetChildrenMenus(Convert.ToInt32(id)) + "},";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "remark:'" + remark + "'" + GetChildrenMenusList(Convert.ToInt32(id)) + "},";
                        }
                    }
                    else
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "remark:'" + remark + "'}";
                        }
                        else
                        {
                            dataStr += "{name:'" + name + "',";
                            dataStr += "id:'" + id + "',";
                            dataStr += "code:'" + code + "',";
                            dataStr += "url:'" + url + "',";
                            dataStr += "remark:'" + remark + "'},";
                        }
                    }
                }
            }
            dataStr += "]";
            return dataStr;
        }
        //菜单详情
        public ActionResult DetailInfo(int id)
        {
            WorkFlow.MenusWebService.menusBLLservice m_menusBllService = new MenusWebService.menusBLLservice();
            WorkFlow.MenusWebService.menusModel m_menusModel = m_menusBllService.GetModel(id);
            ViewData["name"] = m_menusModel.name;
            ViewData["code"] = m_menusModel.code;
            ViewData["url"] = m_menusModel.url;
            ViewData["app_id"] = m_menusModel.app_id;
            ViewData["parent_id"] = m_menusModel.parent_id;
            ViewData["remark"] = m_menusModel.remark;
            ViewData["invalid"] = m_menusModel.invalid;
           
            return View();
        }
    }
}
