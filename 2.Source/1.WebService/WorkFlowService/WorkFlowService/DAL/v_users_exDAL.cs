using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_users_ex
    /// </summary>
    public partial class v_users_exDAL
    {
        public v_users_exDAL()
		{}
		#region  Method
        
        /// <summary>
        /// 获得用户权限列表
        /// </summary>
        /// <param name="appId">系统ID</param>
        /// <returns></returns>
        public DataSet GetPrivilegesListOfUser(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select user_id,user_name,user_login,user_psw,app_id,role_id,role_name,p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id ");
            strSql.Append(" FROM v_users_ex ");
            strSql.Append(" where user_id=@user_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        
        #endregion
    }
}