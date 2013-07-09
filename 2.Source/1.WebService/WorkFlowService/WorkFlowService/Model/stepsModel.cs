using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 流程步骤
    /// </summary>
    [Serializable]
    public partial class stepsModel
    {
        public stepsModel()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _remark;
        private int? _flow_id;
        private int? _step_type_id;
        private int? _repeat_count;
        private bool _invalid = false;
        private int _order_no;
        private bool _deleted = false;
        private DateTime _created_at = DateTime.Now;
        private int _created_by;
        private string _created_ip;
        private DateTime _updated_at = DateTime.Now;
        private int _updated_by;
        private string _updated_ip;
        /// <summary>
        /// 步骤ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 流程步骤名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 流程步骤说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public int? flow_id
        {
            set { _flow_id = value; }
            get { return _flow_id; }
        }
        /// <summary>
        /// 步骤类型ID
        /// </summary>
        public int? step_type_id
        {
            set { _step_type_id = value; }
            get { return _step_type_id; }
        }
        /// <summary>
        /// 步骤重复次数
        /// </summary>
        public int? repeat_count
        {
            set { _repeat_count = value; }
            get { return _repeat_count; }
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
        /// 排序码
        /// </summary>
        public int order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 记录删除标志
        /// </summary>
        public bool deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
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
        /// 记录创建用户
        /// </summary>
        public int created_by
        {
            set { _created_by = value; }
            get { return _created_by; }
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
        public DateTime updated_at
        {
            set { _updated_at = value; }
            get { return _updated_at; }
        }
        /// <summary>
        /// 记录更新用户
        /// </summary>
        public int updated_by
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
        #endregion Model

    }
}