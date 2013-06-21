using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_privileges
    /// </summary>
    public partial class v_privilegesDAL
    {
        public v_privilegesDAL()
		{}
		#region  Method
        /// <summary>
        /// 通过项目编码item_code、权限类型名称pt_name、系统ID获得权限ID
        /// </summary>
        public int GetPrivilegeID(string item_Code,string pt_Name,int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id FROM v_privileges ");
            strSql.Append(" where item_code=@item_code and pt_name=@pt_name and app_id=@app_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@item_code", SqlDbType.NVarChar,80),
					new SqlParameter("@pt_name", SqlDbType.NVarChar,80),
                    new SqlParameter("@app_id",SqlDbType.Int,4)
            };
            parameters[0].Value = item_Code;
            parameters[1].Value = pt_Name;
            parameters[2].Value = appID;
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
        
        /// <summary>
        /// 通过项目编码(item_code)、权限类型ID(pt_id)、系统ID(app_id)获得权限ID
        /// </summary>
        public int GetPrivilegeID(string item_Code, int pt_ID, int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id FROM v_privileges ");
            strSql.Append(" where item_code=@item_code and pt_id=@pt_id and app_id=@app_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@item_code", SqlDbType.NVarChar,80),
					new SqlParameter("@pt_id", SqlDbType.Int,4),
                    new SqlParameter("@app_id",SqlDbType.Int,4)
            };
            parameters[0].Value = item_Code;
            parameters[1].Value = pt_ID;
            parameters[2].Value = appID;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 获取系统权限列表
        /// </summary>
        public DataSet GetPrivilegeListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,app_id ");
            strSql.Append(" FROM v_privileges ");
            strSql.Append(" where app_id=@app_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
		
		#endregion  Method
    }
}