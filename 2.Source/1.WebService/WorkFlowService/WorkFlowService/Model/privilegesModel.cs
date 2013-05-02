using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Serializable]
    public class privilegesModel
    {
        public privilegesModel()
		{}
		#region Model
		private int _id;
		private string _name;
		private int _privilegetype_id;
		private int _privilegeitem_id;
		private string _remark;
		private int _app_id;
		private bool _invalid= false;
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
		/// 权限名称
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 权限类型ID
		/// </summary>
		public int privilegetype_id
		{
			set{ _privilegetype_id=value;}
			get{return _privilegetype_id;}
		}
		/// <summary>
		/// 权限项目ID
		/// </summary>
		public int privilegeitem_id
		{
			set{ _privilegeitem_id=value;}
			get{return _privilegeitem_id;}
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
		/// 应用系统ID
		/// </summary>
		public int app_id
		{
			set{ _app_id=value;}
			get{return _app_id;}
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
