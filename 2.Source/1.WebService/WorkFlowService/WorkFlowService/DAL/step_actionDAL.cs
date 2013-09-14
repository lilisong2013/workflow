using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:step_action
    /// </summary>
    public partial class step_actionDAL
    {
        public step_actionDAL()
        { }
        #region  BasicMethod
        
        /// <summary>
        /// 获得流程处理结果代码数据列表
        /// </summary>
        public DataSet GetStep_ActionList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,remark,order_no ");
			strSql.Append(" FROM step_action ");

            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}