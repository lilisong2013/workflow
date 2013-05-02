using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:privilege_role
    /// </summary>
    public class privilege_roleDAL
    {
        public privilege_roleDAL()
		{}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int role_id, int privilege_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from privilege_role");
            strSql.Append(" where role_id=@role_id and privilege_id=@privilege_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@privilege_id", SqlDbType.Int,4)			};
            parameters[0].Value = role_id;
            parameters[1].Value = privilege_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Saron.WorkFlowService.Model.privilege_roleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into privilege_role(");
            strSql.Append("role_id,privilege_id)");
            strSql.Append(" values (");
            strSql.Append("@role_id,@privilege_id)");
            SqlParameter[] parameters = {
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@privilege_id", SqlDbType.Int,4)};
            parameters[0].Value = model.role_id;
            parameters[1].Value = model.privilege_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int role_id, int privilege_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from privilege_role ");
            strSql.Append(" where role_id=@role_id and privilege_id=@privilege_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@privilege_id", SqlDbType.Int,4)			};
            parameters[0].Value = role_id;
            parameters[1].Value = privilege_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
