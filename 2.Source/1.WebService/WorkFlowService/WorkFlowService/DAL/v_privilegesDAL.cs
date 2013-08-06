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
        //获取视图下权限下菜单列表
        public DataSet GetMPrivilegesListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,app_id ");
            strSql.Append(" FROM v_privileges ");
            strSql.Append(" where app_id=@app_id and pt_id=1 order by p_id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        //获取视图下权限下元素列表
        public DataSet GetEPrivilegesListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,app_id ");
            strSql.Append(" FROM v_privileges ");
            strSql.Append(" where app_id=@app_id and pt_id=2 order by p_id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        //获取视图下权限下操作列表
        public DataSet GetOPrivilegesListOfApp(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,app_id ");
            strSql.Append(" FROM v_privileges ");
            strSql.Append(" where app_id=@app_id and pt_id=3 order by p_id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.v_privilegesModel GetModel(int p_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 p_id,p_name,pt_id,pt_name,pt_code,item_name,item_code,item_id,app_id from v_privileges ");
            strSql.Append(" where p_id=@p_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@p_id", SqlDbType.Int,4),
					new SqlParameter("@p_name", SqlDbType.NVarChar,80),
					new SqlParameter("@pt_id", SqlDbType.Int,4),
					new SqlParameter("@pt_name", SqlDbType.NVarChar,80),
					new SqlParameter("@pt_code", SqlDbType.NVarChar,40),
					new SqlParameter("@item_name", SqlDbType.NVarChar,80),
					new SqlParameter("@item_code", SqlDbType.NVarChar,80),
					new SqlParameter("@item_id", SqlDbType.Int,4),
					new SqlParameter("@app_id", SqlDbType.Int,4)			};
            parameters[0].Value = p_id;

            Saron.WorkFlowService.Model.v_privilegesModel model = new Model.v_privilegesModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.v_privilegesModel DataRowToModel(DataRow row)
        {
            Saron.WorkFlowService.Model.v_privilegesModel model = new Model.v_privilegesModel();
            if (row != null)
            {
                if (row["p_id"] != null && row["p_id"].ToString() != "")
                {
                    model.p_id = int.Parse(row["p_id"].ToString());
                }
                if (row["p_name"] != null)
                {
                    model.p_name = row["p_name"].ToString();
                }
                if (row["pt_id"] != null && row["pt_id"].ToString() != "")
                {
                    model.pt_id = int.Parse(row["pt_id"].ToString());
                }
                if (row["pt_name"] != null)
                {
                    model.pt_name = row["pt_name"].ToString();
                }
                if (row["pt_code"] != null)
                {
                    model.pt_code = row["pt_code"].ToString();
                }
                if (row["item_name"] != null)
                {
                    model.item_name = row["item_name"].ToString();
                }
                if (row["item_code"] != null)
                {
                    model.item_code = row["item_code"].ToString();
                }
                if (row["item_id"] != null && row["item_id"].ToString() != "")
                {
                    model.item_id = int.Parse(row["item_id"].ToString());
                }
                if (row["app_id"] != null && row["app_id"].ToString() != "")
                {
                    model.app_id = int.Parse(row["app_id"].ToString());
                }
            }
            return model;
        }
		#endregion  Method
    }
}