using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// v_users_ex:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class v_users_exModel
    {
        public v_users_exModel()
        { }
        #region Model
        private int _user_id;
        private string _user_name;
        private string _user_login;
        private string _user_psw;
        private int? _app_id;
        private int _role_id;
        private string _role_name;
        private int _p_id;
        private string _p_name;
        private int _pt_id;
        private string _pt_name;
        private string _pt_code;
        private string _item_name;
        private string _item_code;
        private int _item_id;
        
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
        /// 登录密码
        /// </summary>
        public string user_psw
        {
            set { _user_psw = value; }
            get { return _user_psw; }
        }
        /// <summary>
        /// 系统id
        /// </summary>
        public int? app_id
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
        /// <summary>
        /// 权限类型id
        /// </summary>
        public int pt_id
        {
            set { _pt_id = value; }
            get { return _pt_id; }
        }
        /// <summary>
        /// 权限类型名称
        /// </summary>
        public string pt_name
        {
            set { _pt_name = value; }
            get { return _pt_name; }
        }
        /// <summary>
        /// 权限类型编码
        /// </summary>
        public string pt_code
        {
            set { _pt_code = value; }
            get { return _pt_code; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string item_name
        {
            set { _item_name = value; }
            get { return _item_name; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string item_code
        {
            set { _item_code = value; }
            get { return _item_code; }
        }
        /// <summary>
        /// 项目id
        /// </summary>
        public int item_id
        {
            set { _item_id = value; }
            get { return _item_id; }
        }
        #endregion Model

    }
}