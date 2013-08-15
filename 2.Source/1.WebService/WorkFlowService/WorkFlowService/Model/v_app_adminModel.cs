using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  Saron.WorkFlowService.Model
{
    /// <summary>
    /// v_app_admin:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class v_app_adminModel
    {
        public v_app_adminModel()
        { }
        #region Model
        private int _app_id;
        private string _app_name;
        private string _app_code;
        private string _url;
        private string _app_mark;
        private int _user_id;
        private string _user_login;
        private string _user_password;
        private string _user_name;
        private string _employee_no;
        private string _mobile_phone;
        private string _mail;
        private string _user_remark;
        /// <summary>
        /// 
        /// </summary>
        public int App_id
        {
            set { _app_id = value; }
            get { return _app_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string App_Name
        {
            set { _app_name = value; }
            get { return _app_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string App_Code
        {
            set { _app_code = value; }
            get { return _app_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string URL
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string App_Mark
        {
            set { _app_mark = value; }
            get { return _app_mark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int User_ID
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Login
        {
            set { _user_login = value; }
            get { return _user_login; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Password
        {
            set { _user_password = value; }
            get { return _user_password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string employee_no
        {
            set { _employee_no = value; }
            get { return _employee_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mobile_phone
        {
            set { _mobile_phone = value; }
            get { return _mobile_phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mail
        {
            set { _mail = value; }
            get { return _mail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_Remark
        {
            set { _user_remark = value; }
            get { return _user_remark; }
        }
        #endregion Model

    }
}