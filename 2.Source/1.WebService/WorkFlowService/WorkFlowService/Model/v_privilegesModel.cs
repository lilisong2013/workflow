﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// v_privileges:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class v_privilegesModel
    {
        public v_privilegesModel()
		{}
		#region Model
		private int _p_id;
		private string _p_name;
		private int _pt_id;
		private string _pt_name;
		private string _pt_code;
		private string _item_name;
        private string _item_code;
		private int _item_id;
		private int _app_id;
		/// <summary>
		/// 权限id
		/// </summary>
		public int p_id
		{
			set{ _p_id=value;}
			get{return _p_id;}
		}
		/// <summary>
		/// 权限名称
		/// </summary>
		public string p_name
		{
			set{ _p_name=value;}
			get{return _p_name;}
		}
		/// <summary>
		/// 权限类型id
		/// </summary>
		public int pt_id
		{
			set{ _pt_id=value;}
			get{return _pt_id;}
		}
		/// <summary>
		/// 权限类型名称
		/// </summary>
		public string pt_name
		{
			set{ _pt_name=value;}
			get{return _pt_name;}
		}
		/// <summary>
		/// 权限类型编码
		/// </summary>
		public string pt_code
		{
			set{ _pt_code=value;}
			get{return _pt_code;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string item_name
		{
			set{ _item_name=value;}
			get{return _item_name;}
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
			set{ _item_id=value;}
			get{return _item_id;}
		}
		/// <summary>
		/// 系统id
		/// </summary>
		public int app_id
		{
			set{ _app_id=value;}
			get{return _app_id;}
		}
		#endregion Model


    }
}