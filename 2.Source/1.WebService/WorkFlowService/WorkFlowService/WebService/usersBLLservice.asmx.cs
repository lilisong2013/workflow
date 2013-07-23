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
        private readonly Saron.WorkFlowService.DAL.user_roleDAL m_user_roledal = new DAL.user_roleDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        [WebMethod(Description = "（系统管理员登录）是否存在系统管理员login且密码password的系统管理员,<h4>（无需授权验证）</h4>")]
        public bool SysAdminLoginValidator(string login,string password,out string msg)
        {
            if (!m_securityContext.AdminIsValidCK(login, password, out msg))
            {
                return false;
            }
            

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
        
        //[WebMethod(Description = "（普通用户）是否存在系统管理员login且密码password的系统管理员,<h4>（无需授权验证）</h4>")]
        //public bool OLoginValidator(string login, string password,int appID, out string msg)
        //{
        //    if (!m_securityContext.OrdinaryIsValidCK(login, password,appID, out msg))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "是否存在登录名为login的系统管理员记录,<h4>（需授权验证,自定义用户）</h4>")]
        public bool ExistsLogin(string login,out string msg)
        {
            //是否有权限访问
            if (!m_securityContext.AnyOneIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }

            return m_usersdal.ExistsLogin(login);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条系统管理员记录,<h4>（需授权验证,自定义用户）</h4>")]
        public int AddSysAdmin(Saron.WorkFlowService.Model.usersModel model, out string msg)
        {
            int result = 0;
            //是否有权限访问
            if (!m_securityContext.AnyOneIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return result;
            }
            //添加的实体对象的系统管理员login在表中已经存在
            //或者添加的实体对象不是系统管理员，都无法添加实体对象到数据库的users表中
            if (!model.admin)
            {
                return -1;
            }

            if (!m_usersdal.ExistsLogin(model.login))
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
        [WebMethod(Description = "通过系统主键删除系统管理员信息,<h4>（需授权验证，超级管理员用户）</h4>")]
        public bool DeleteAdminByAppID(int appID, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.DeleteAdminByAppID(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "是否存在login为login且app_id为appID的普通用户记录，<h4>（需要授权验证，系统管理员）</h4>")]
        public bool ExistsLoginAndAppID(string login, int? appId,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.ExistsLogin(login, appId);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条普通用户记录记录，<h4>（需要授权验证，系统管理员）")]
        public int AddSysUser(Saron.WorkFlowService.Model.usersModel model, out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
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
        [WebMethod(Description = "系统管理员更新一条记录(密码为密文，且普通用户修改密码不为空，需要修改信息)，<h4>（需要授权验证，系统管理员）")]
        public bool AdminUpdate(Saron.WorkFlowService.Model.usersModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.Update(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "系统管理员更新一条记录(密码为明文，且普通用户修改密码为空，保存原密码不变，其他信息修改)，<h4>（需要授权验证，系统管理员）")]
        public bool AdminUpdatePass(Saron.WorkFlowService.Model.usersModel model, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.UpdateUserPass(model);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "逻辑上删除一条id为id的记录")]
        public bool LogicDelete(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            int user_roleCount = m_user_roledal.User_RoleCountByUserID(id);
            if (user_roleCount > 0)
            {
                if (m_user_roledal.DeleteByUserID(id) == user_roleCount)
                {
                    return m_usersdal.LogicDelete(id); ;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return m_usersdal.LogicDelete(id); ;
            }
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByID(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据应用系统主键appid获得系统管理员实体对象，<h4>（需要授权验证,超级管理员用户）")]
        public Saron.WorkFlowService.Model.usersModel GetAdminModelByAppID(int appid,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.SuperAdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }
            return m_usersdal.GetAdminModelByAppID(appid);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据系统管理员登录名login得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.usersModel GetUserModelByLoginCK(string login,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValidCK(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetModelByLogin(login);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据系统管理员登录名login得到一个实体对象，<h4>（需要授权验证，系统管理员）</h4>")]
        public Saron.WorkFlowService.Model.usersModel GetUserModelByLogin(string login, out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
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
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetModelByAppID(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllUsersListOfApp(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_usersdal.GetAllUsersListOfApp(appID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "修改密码")]
        public bool ModifyPassword(string login, string password,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_usersdal.ModifyPassword(login,password);
        }

        #endregion  Method
    }
}
