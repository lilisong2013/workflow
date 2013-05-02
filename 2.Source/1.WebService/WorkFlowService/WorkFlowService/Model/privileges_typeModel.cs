using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 权限类型表
    /// </summary>
    [Serializable]
    public class privileges_typeModel
    {
        public privileges_typeModel()
		{}
		
        #region Model
		private int _id;
		private string _name;
		private string _code;
		private string _remark;
		/// <summary>
		/// 记录ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 权限类型名称
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 权限类型编码
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

    }
}
