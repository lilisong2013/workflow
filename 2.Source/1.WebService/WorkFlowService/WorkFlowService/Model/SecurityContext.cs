using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// SoapHeader派生类
    /// </summary>
    public class SecurityContext:SoapHeader
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private int _appid;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SecurityContext()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="passWord"></param>
        public SecurityContext(string userName,string passWord,int appID)
        {
            UserName = userName;
            PassWord = passWord;
            AppID=appID;
        }

        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// 系统id
        /// </summary>
        public int AppID
        {
            set { _appid = value; }
            get { return _appid; }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 管理员webservice授权判断
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="appID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AdminIsValid(string userName, string passWord, int appID, out string msg)
        {
            msg = "";
            Saron.WorkFlowService.DAL.usersDAL m_userDal = new DAL.usersDAL();

            try
            {
                if (m_userDal.ExistsSysAdminSecurity(userName, passWord, appID))
                {
                    return true;
                }
                else
                {
                    msg = "无权访问WebService!";
                    return false;
                }
            }catch(Exception ex)
            {
                msg = "数据库访问出错！";
                return false;
            }
        }

        /// <summary>
        /// 普通用户webservice授权判断
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="appID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UserIsValid(string userName, string passWord, int appID, out string msg)
        {
            msg = "";
            Saron.WorkFlowService.DAL.usersDAL m_userDal = new DAL.usersDAL();

            try
            {
                if (m_userDal.ExistsSysUserSecurity(userName, passWord, appID))
                {
                    return true;
                }
                else
                {
                    msg = "无权访问WebService!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "数据库访问出错！";
                return false;
            }
        }

        /// <summary>
        /// 超级管理员webservice授权判断
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="appID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SuperAdminIsValid(string userName, string passWord, out string msg)
        {
            msg = "";
            Saron.WorkFlowService.DAL.base_userDAL m_base_userDal = new DAL.base_userDAL();

            try
            {
                if (m_base_userDal.ExistsSuperAdminSecurity(userName, passWord))
                {
                    return true;
                }
                else
                {
                    msg = "无权访问WebService!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "数据库访问出错！";
                return false;
            }
        }

        #endregion
    }
}