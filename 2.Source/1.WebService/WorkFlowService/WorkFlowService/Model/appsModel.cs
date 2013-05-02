using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
        /// <summary>
        /// 应用信息表
        /// </summary>
        [Serializable]
        public partial class appsModel
        {
            public appsModel()
            { }
            #region Model
            private int _id;
            private string _name;
            private string _code;
            private string _url;
            private string _remark;
            private bool _invalid = false;
            private DateTime _created_at = DateTime.Now;
            private string _created_ip;
            private DateTime? _updated_at;
            private int? _updated_by;
            private string _updated_ip;
            private DateTime? _apply_at;
            private DateTime? _approval_at;
            /// <summary>
            /// 记录ID
            /// </summary>
            public int id
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 系统名称
            /// </summary>
            public string name
            {
                set { _name = value; }
                get { return _name; }
            }
            /// <summary>
            /// 系统编码
            /// </summary>
            public string code
            {
                set { _code = value; }
                get { return _code; }
            }
            /// <summary>
            /// 访问链接
            /// </summary>
            public string url
            {
                set { _url = value; }
                get { return _url; }
            }
            /// <summary>
            /// 备注
            /// </summary>
            public string remark
            {
                set { _remark = value; }
                get { return _remark; }
            }
            /// <summary>
            /// 是否有效
            /// </summary>
            public bool invalid
            {
                set { _invalid = value; }
                get { return _invalid; }
            }

            /// <summary>
            /// 记录创建时间
            /// </summary>
            public DateTime created_at
            {
                set { _created_at = value; }
                get { return _created_at; }
            }

            /// <summary>
            /// 记录创建IP
            /// </summary>
            public string created_ip
            {
                set { _created_ip = value; }
                get { return _created_ip; }
            }
            /// <summary>
            /// 记录更新时间
            /// </summary>
            public DateTime? updated_at
            {
                set { _updated_at = value; }
                get { return _updated_at; }
            }
            /// <summary>
            /// 记录更新用户
            /// </summary>
            public int? updated_by
            {
                set { _updated_by = value; }
                get { return _updated_by; }
            }
            /// <summary>
            /// 记录更新IP
            /// </summary>
            public string updated_ip
            {
                set { _updated_ip = value; }
                get { return _updated_ip; }
            }

            /// <summary>
            /// 申请时间
            /// </summary>
            public DateTime? apply_at
            {
                set { _apply_at = value; }
                get { return _apply_at; }
            }
            /// <summary>
            /// 审批时间
            /// </summary>
            public DateTime? approval_at
            {
                set { _approval_at = value; }
                get { return _approval_at; }
            }
            #endregion Model

        }
}
