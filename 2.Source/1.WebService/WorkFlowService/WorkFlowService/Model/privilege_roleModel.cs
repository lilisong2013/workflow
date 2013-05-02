using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlowService.Model
{
    /// <summary>
    /// 权限角色表
    /// </summary>
    [Serializable]
    public class privilege_roleModel
    {
        public privilege_roleModel()
        { }
        
        #region Model
        private int _role_id;
        private int _privilege_id;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int privilege_id
        {
            set { _privilege_id = value; }
            get { return _privilege_id; }
        }
        #endregion Model
    }
}
