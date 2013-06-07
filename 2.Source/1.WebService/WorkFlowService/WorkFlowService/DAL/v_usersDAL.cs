using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_users
    /// </summary>
    public partial class v_usersDAL
    {
        public v_usersDAL()
		{ }
        #region Method
        /// <summary>
        /// （用户登录）是否存在用户登录名或密码
        /// </summary>
        public bool ExistsUserLogin(string login, string password,int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from v_users");
            strSql.Append(" where user_login=@user_login and user_psw=dbo.f_tobase64(HASHBYTES('md5', CONVERT(nvarchar,@user_psw))) and app_id=@app_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@login", SqlDbType.NVarChar,40),
					new SqlParameter("@password", SqlDbType.NVarChar,255),
                    new SqlParameter("@app_id", SqlDbType.Int,4)
           };
            parameters[0].Value = login;
            parameters[1].Value = password;
            parameters[2].Value = appID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 用户是否有访问项目的权限
        /// </summary>
        public bool ExistsUser_Privilege(int userID,int privilegeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from v_users");
            strSql.Append(" where user_id=@user_id and p_id=@p_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
                    new SqlParameter("@p_id", SqlDbType.Int,4)
           };
            parameters[0].Value = userID;
            parameters[1].Value = privilegeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion
    }
}