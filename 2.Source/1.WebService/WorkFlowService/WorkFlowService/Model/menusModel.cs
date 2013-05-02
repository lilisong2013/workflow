using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Serializable]
    public class menusModel
    {
        public menusModel()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _code;
		private string _url;
		private int? _app_id;
		private int? _parent_id;
		private string _remark;
		private bool _invalid= false;
		private bool _deleted= false;
		private DateTime _created_at= DateTime.Now;
		private int _created_by;
		private string _created_ip;
		private DateTime? _updated_at;
		private int? _updated_by;
		private string _updated_ip;
		/// <summary>
		/// 记录ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 菜单编码
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 菜单URL
		/// </summary>
		public string url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 应用系统id
		/// </summary>
		public int? app_id
		{
			set{ _app_id=value;}
			get{return _app_id;}
		}
		/// <summary>
		/// 父菜单ID
		/// </summary>
		public int? parent_id
		{
			set{ _parent_id=value;}
			get{return _parent_id;}
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
		#endregion Model

    }
}
