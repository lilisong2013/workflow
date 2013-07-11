using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 流程步骤类型代码(0.顺序 1.并行)
    /// </summary>
    [Serializable]
    public partial class flowstep_typeModel
    {
        public flowstep_typeModel()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _remark;
        private int? _order_no;
        /// <summary>
        /// 步骤类型ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 步骤类型名称
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