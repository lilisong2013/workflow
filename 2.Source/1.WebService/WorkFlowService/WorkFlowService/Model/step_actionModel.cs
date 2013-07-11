using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 流程处理结果代码
    /// </summary>
    [Serializable]
    public partial class step_actionModel
    {
        public step_actionModel()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _remark;
        private int? _order_no;
        /// <summary>
        /// 处理结果ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 处理结果名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 排序码
        /// </summary>
        public int? order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        #endregion Model

    }
}