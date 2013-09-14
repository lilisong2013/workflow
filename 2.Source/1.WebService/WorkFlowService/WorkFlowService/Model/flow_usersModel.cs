using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 流程用户定义
    /// </summary>
    [Serializable]
    public partial class flow_usersModel
    {
        public flow_usersModel()
        { }
        #region Model
        private int _id;
        private int _step_id;
        private int _user_id;
        private string _remark;
        /// <summary>
        /// 记录ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 步骤ID
        /// </summary>
        public int step_id
        {
            set { _step_id = value; }
            get { return _step_id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}