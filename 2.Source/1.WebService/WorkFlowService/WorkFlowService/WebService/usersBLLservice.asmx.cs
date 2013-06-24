using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Saron.WorkFlowService.Model;

namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// usersBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class usersBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.usersDAL m_usersdal = new Saron.WorkFlowService.DAL.usersDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [WebMethod(Description = "（系统管理员登录）是否存在系统管理员login且密码password的系统管理员,<h4>（无需授权验证）</h4>")]
        public bool SysAdminLoginValidator(string login,string password)
        {
            string msg="";
            if (m_securityContext.AdminIsValidCK(login, password, out msg))
            {
                return false;
            }

            bool flag = m_usersdal.ExistsSysAdmin(login, password);
            
            if (flag)
            {
                Saron.WorkFlowService.Model.usersModel m_userModel = new usersModel();
                Saron.WorkFlowService.Model.appsModel m_appModel = new appsModel();
                Saron.WorkFlowService.DAL.appsDAL m_appDal=new DAL.appsDAL();
                
                m_userModel = m_usersdal.GetModelByLogin(login);//根据系统管理员登录名login得到一个实体对象
                m_appModel = m_appDal.GetModel((int)m_userModel.app_id);//根据系统ID得到一个系统实体对象

                if (m_appModel.invalid)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "是否存在login为login且app_id为appID的普通用户记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsLoginAndAppID(string login, int? appId,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.ExistsLogin(login, appId);
        }

        [WebMethod(Description = "是否存在登录名为login的系统管理员记录,<h4>（无需授权验证）</h4>")]
        public bool ExistsLogin(string login)
        {
            return m_usersdal.ExistsLogin(login);
        }

        [WebMethod(Description = "增加一条系统管理员记录,<h4>（无需授权验证）</h4>")]
        public int AddSysAdmin(Saron.WorkFlowService.Model.usersModel model)
        {
            //添加的实体对象的系统管理员login在表中已经存在
            //或者添加的实体对象不是系统管理员，都无法添加实体对象到数据库的users表中
            if (!model.admin)
            {
                return -1;
            }

            if (!ExistsLogin(model.login))
            {
                return m_usersdal.Add(model);
            }
            else
            {
                return -1;
            }
        }

        [WebMethod(Description = "删除id为id，登录名为login的记录,<h4>（无需授权验证）</h4>")]
        public bool DeleteIdAndLogin(int id, string login)
        {
            return m_usersdal.Delete(id, login);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条普通用户记录记录，<h4>（需要授权验证，系统管理员）")]
        public int AddSysUser(Saron.WorkFlowService.Model.usersModel model, out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_usersdal.Add(model);

            if (result == 0)
            {
                msg = "添加失败";
            }
            else
            {
                msg = "";
            }

            return result;
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "系统管理员更新一条记录")]
        public bool AdminUpdate(Saron.WorkFlowService.Model.usersModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "逻辑上删除一条id为id的记录")]
        public bool LogicDelete(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.LogicDelete(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByID(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetModel(id);
        }
        
        ///<summary>
        ///根据appid获得一个用户对象实体
        /// </summary>
       [WebMethod(Description = "根据应用系统appid、admin、invalid信息得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByAppAdmin(int appid)
        {
            return m_usersdal.GetModelByAppAdmin(appid);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据系统管理员登录名login得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.usersModel GetModelByLogin(string login,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetModelByLogin(login);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据系统ID得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByAppID(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetModelByAppID(appID);
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //[WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        //public DataSet GetUsersList(string strWhere)
        //{
        //    return m_usersdal.GetUsersList(strWhere);
        //}
        
        

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //[WebMethod(Description = "获得所有数据列表")]
        //public DataSet GetAllUsersList()
        //{
        //    return GetUsersList("");
        //}

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllUsersListOfApp(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetAllUsersListOfApp(appID);
        }

        ///// <summary>
        ///// 获得记录总数
        ///// </summary>
        //[WebMethod(Description = "获得记录总条数")]
        //public int GetRecordCount(string strWhere)
        //{
        //    return m_usersdal.GetRecordCount(strWhere);
        //}

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "修改密码")]
        public bool ModifyPassword(string login, string password,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.ModifyPassword(login,password);
        }

        #endregion  Method

        //外部系统使用接口
        #region InterfaceMethod
        
        ///// <summary>
        ///// （普通用户登录）是否存在用户名或密码
        ///// </summary>
        //[WebMethod(Description = "是否存在用户名login且密码password的系统用户")]
        //public bool SysUserLoginValidator(string login, string password, int appID)
        //{
        //    bool flag = m_usersdal.ExistsSysUser(login, password, appID);
        //    if (flag)
        //    {
        //        Saron.WorkFlowService.Model.usersModel m_userModel = new usersModel();
        //        Saron.WorkFlowService.Model.appsModel m_appModel = new appsModel();
        //        Saron.WorkFlowService.DAL.appsDAL m_appDal = new DAL.appsDAL();
        //        m_userModel = GetUserModelByLogin(login,appID);
        //        m_appModel = m_appDal.GetModel((int)m_userModel.app_id);
        //        if (m_appModel.invalid)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        
        /////<summary>
        /////(应用系统apps表删除记录时判断user表中)是否存在系统应用ID为app_id的记录
        ///// </summary>
        //[WebMethod(Description = "apps表删除记录时，首先判断user表中是否还有与此关联的记录")]
        //public bool ExistsAppofUser(int app_id)
        //{
        //    return m_usersdal.ExistsAppofUser(app_id);
        //}
        
        ///// <summary>
        ///// 得到一个对象实体(普通用户)
        ///// </summary>
        //[WebMethod(Description = "根据登录名login和系统ID得到一个实体对象（普通用户）")]
        //public Saron.WorkFlowService.Model.usersModel GetUserModelByLogin(string login,int appID)
        //{
        //    return m_usersdal.GetModel(login,appID);
        //}
        #endregion
    }
}
