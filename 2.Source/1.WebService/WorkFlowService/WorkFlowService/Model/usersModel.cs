using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Serializable]
    public class usersModel
    {
        public usersModel()
		{}
		#region Model
		private int _id;
		private string _login;
		private string _password;
		private string _name;
		private string _employee_no;
		private string _mobile_phone;
		private string _mail;
		private string _remark;
        private bool _admin = false;
        private bool _invalid = false;
        private bool _deleted = false;
		private DateTime _created_at= DateTime.Now;
		private int _created_by;
		private string _created_ip;
		private DateTime? _updated_at;
		private int? _updated_by;
		private string _updated_ip;
		private int? _app_id;
		/// <summary>
		/// 用户ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 登录名称
		/// </summary>
		public string login
		{
			set{ _login=value;}
			get{return _login;}
		}
		/// <summary>
		/// 登录密码
		/// </summary>
		public string password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 工号
		/// </summary>
		public string employee_no
		{
			set{ _employee_no=value;}
			get{return _employee_no;}
		}
		/// <summary>
		/// 手机号码
		/// </summary>
		public string mobile_phone
		{
			set{ _mobile_phone=value;}
			get{return _mobile_phone;}
		}
		/// <summary>
		/// 邮件地址
		/// </summary>
		public string mail
		{
			set{ _mail=value;}
			get{return _mail;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 是否管理员
		/// </summary>
		public bool admin
		{
			set{ _admin=value;}
			get{return _admin;}
		}
		/// <summary>
		/// 是否有效
		/// </summary>
        public bool invalid
		{
			set{ _invalid=value;}
			get{return _invalid;}
		}
		/// <summary>
		/// 记录删除标志
		/// </summary>
        public bool deleted
		{
			set{ _deleted=value;}
			get{return _deleted;}
		}
		/// <summary>
		/// 记录创建时间
		/// </summary>
		public DateTime created_at
		{
			set{ _created_at=value;}
			get{return _created_at;}
		}
		/// <summary>
		/// 记录创建用户
		/// </summary>
		public int created_by
		{
			set{ _created_by=value;}
			get{return _created_by;}
		}
		/// <summary>
		/// 记录创建IP
		/// </summary>
		public string created_ip
		{
			set{ _created_ip=value;}
			get{return _created_ip;}
		}
		/// <summary>
		/// 记录更新时间
		/// </summary>
		public DateTime? updated_at
		{
			set{ _updated_at=value;}
			get{return _updated_at;}
		}
		/// <summary>
		/// 记录更新用户
		/// </summary>
		public int? updated_by
		{
			set{ _updated_by=value;}
			get{return _updated_by;}
		}
		/// <summary>
		/// 记录更新IP
		/// </summary>
		public string updated_ip
		{
			set{ _updated_ip=value;}
			get{return _updated_ip;}
		}
		/// <summary>
		/// 应用系统ID
		/// </summary>
		public int? app_id
		{
			set{ _app_id=value;}
			get{return _app_id;}
		}
		#endregion Model
    }
}
