using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_privileges_tree
    /// </summary>
    public partial class v_privileges_treeDAL
    {
        public v_privileges_treeDAL()
        { }

        #region BasicMethod

        /// <summary>
        /// 获得某角色拥有的菜单权限、页面元素的权限列表
        /// </summary>
        public DataSet GetMenuAndElementPrivilegeListOfRole(int roleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,item_name,item_code,item_id,parent_id,app_id,role_id ");
            strSql.Append(" FROM v_privileges_tree ");
            strSql.Append(" where role_id=@role_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@role_id", SqlDbType.Int,4)
			};
            parameters[0].Value = roleID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得某系统菜单权限、页面元素的权限列表
        /// </summary>
        public DataSet GetMenuAndElementPrivilegeListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DISTINCT p_id, p_name, pt_id, item_name, item_code, item_id, parent_id, app_id ");
            strSql.Append(" FROM v_privileges_tree ");
            strSql.Append(" where app_id=@app_id ");
            strSql.Append(" GROUP BY p_id, p_name, pt_id, item_name, item_code, item_id, parent_id, app_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion
    }
}