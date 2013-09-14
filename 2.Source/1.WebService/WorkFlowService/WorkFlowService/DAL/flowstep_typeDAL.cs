using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:flowstep_type
    /// </summary>
    public partial class flowstep_typeDAL
    {
        public flowstep_typeDAL()
        { }
        #region  BasicMethod
        
        /// <summary>
        /// 获得流程步骤类型代码数据列表
        /// </summary>
        public DataSet GetFlowStep_TypeList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,remark,order_no ");
            strSql.Append(" FROM flowstep_type ");

            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}