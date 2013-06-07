using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// v_users:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class v_usersModel
    {
        public v_usersModel()
        { }
        #region Model
        private int _user_id;
        private string _user_name;
        private string _user_login;
        private string _user_psw;
        private int _app_id;
        private int _role_id;
        private string _role_name;
        private int _p_id;
        private string _p_name;
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string user_login
        {
            set { _user_login = value; }
            get { return _user_login; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string user_psw
        {
            set { _user_psw = value; }
            get { return _user_psw; }
        }
        /// <summary>
        /// 系统id
        /// </summary>
        public int app_id
        {
            set { _app_id = value; }
            get { return _app_id; }
        }
        /// <summary>
        /// 角色id
        /// </summary>
        public int role_id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string role_name
        {
            set { _role_name = value; }
            get { return _role_name; }
        }
        /// <summary>
        /// 权限id
        /// </summary>
        public int p_id
        {
            set { _p_id = value; }
            get { return _p_id; }
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string p_name
        {
            set { _p_name = value; }
            get { return _p_name; }
        }
        #endregion Model


    }
}