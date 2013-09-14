using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_menu_privileges
    /// </summary>
    public partial class v_menu_privilegesDAL
    {
        public v_menu_privilegesDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 获得某系统菜单权限的顶级菜单权限列表
        /// </summary>
        public DataSet GetTopMenuPrivilegeListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,parent_id,app_id ");
            strSql.Append(" FROM v_menu_privileges ");
            strSql.Append(" where app_id=@app_id and parent_id is null ");
            SqlParameter[] parameters = {
                    new SqlParameter("@app_id", SqlDbType.Int,4)
            };
            parameters[0].Value = appID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得某系统菜单权限的权限列表
        /// </summary>
        public DataSet GetMenuPrivilegeListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,parent_id,app_id ");
            strSql.Append(" FROM v_menu_privileges ");
            strSql.Append(" where app_id=@app_id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@app_id", SqlDbType.Int,4)
            };
            parameters[0].Value = appID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得菜单权限的父菜单ID
        /// </summary>
        public int GetMenuPrivilegeParentID(int menuprivilegeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select parent_id ");
            strSql.Append(" FROM v_menu_privileges ");
            strSql.Append(" where p_id=@p_id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@p_id", SqlDbType.Int,4)
            };
            parameters[0].Value = menuprivilegeID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        #endregion

    }
}