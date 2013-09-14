using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// v_steps:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class v_stepsModel
    {
        public v_stepsModel()
        { }
        #region Model
        private int _s_id;
        private string _s_name;
        private string _f_name;
        private string _step_type_name;
        private int _order_no;
        private int? _app_id;
        private int _f_id;
        private int _user_id;
        private string _user_name;
      
        /// <summary>
        /// 流程步骤ID
        /// </summary>
        public int s_id
        {
            set { _s_id = value; }
            get { return _s_id; }
        }
        /// <summary>
        /// 流程步骤名称
        /// </summary>
        public string s_name
        {
            set { _s_name = value; }
            get { return _s_name; }
        }
        /// <summary>
        /// 步骤所属流程名称
        /// </summary>
        public string f_name
        {
            set { _f_name = value; }
            get { return _f_name; }
        }
        /// <summary>
        /// 步骤类型名称：顺序、并行
        /// </summary>
        public string step_type_name
        {
            set { _step_type_name = value; }
            get { return _step_type_name; }
        }
        /// <summary>
        /// 步骤排序码
        /// </summary>
        public int order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public int? app_id
        {
            set { _app_id = value; }
            get { return _app_id; }
        }
        /// <summary>
        /// 所属流程ID
        /// </summary>
        public int f_id
        {
            set { _f_id = value; }
            get { return _f_id; }
        }

        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        #endregion Model

    }
}