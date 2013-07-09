using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 流程处理明细
    /// </summary>
    [Serializable]
    public partial class flow_processingModel
    {
        public flow_processingModel()
        { }
        #region Model
        private int _id;
        private int? _user_id;
        private int? _step_id;
        private int? _step_action_id;
        private string _process_remark;
        private DateTime _process_time = DateTime.Now;
        private bool _deleted = false;
        private DateTime _created_at = DateTime.Now;
        private int _created_by;
        private string _created_ip;
        private DateTime _updated_at = DateTime.Now;
        private int _updated_by;
        private string _updated_ip;
        /// <summary>
        /// 处理明细ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 步骤ID
        /// </summary>
        public int? step_id
        {
            set { _step_id = value; }
            get { return _step_id; }
        }
        /// <summary>
        /// 处理结果ID
        /// </summary>
        public int? step_action_id
        {
            set { _step_action_id = value; }
            get { return _step_action_id; }
        }
        /// <summary>
        /// 处理说明
        /// </summary>
        public string process_remark
        {
            set { _process_remark = value; }
            get { return _process_remark; }
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime process_time
        {
            set { _process_time = value; }
            get { return _process_time; }
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