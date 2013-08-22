using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_steps
    /// </summary>
    public partial class v_stepsDAL
    {
        public v_stepsDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.v_stepsModel GetV_StepsModel(int stepID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 s_id,s_name,f_name,step_type_name,order_no,app_id,f_id from v_steps ");
            strSql.Append(" where s_id=@s_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@s_id", SqlDbType.Int,4)
     		};
            parameters[0].Value = stepID;

            Saron.WorkFlowService.Model.v_stepsModel model = new Model.v_stepsModel();
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
        public Saron.WorkFlowService.Model.v_stepsModel DataRowToModel(DataRow row)
        {
            Saron.WorkFlowService.Model.v_stepsModel model = new Model.v_stepsModel();
            if (row != null)
            {
                if (row["s_id"] != null && row["s_id"].ToString() != "")
                {
                    model.s_id = int.Parse(row["s_id"].ToString());
                }
                if (row["s_name"] != null)
                {
                    model.s_name = row["s_name"].ToString();
                }
                if (row["f_name"] != null)
                {
                    model.f_name = row["f_name"].ToString();
                }
                if (row["step_type_name"] != null)
                {
                    model.step_type_name = row["step_type_name"].ToString();
                }
                if (row["order_no"] != null && row["order_no"].ToString() != "")
                {
                    model.order_no = int.Parse(row["order_no"].ToString());
                }
                if (row["app_id"] != null && row["app_id"].ToString() != "")
                {
                    model.app_id = int.Parse(row["app_id"].ToString());
                }
                if (row["f_id"] != null && row["f_id"].ToString() != "")
                {
                    model.f_id = int.Parse(row["f_id"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFlowStepListByFlowID(int flowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select s_id,s_name,f_name,step_type_name,order_no,app_id,f_id ");
            strSql.Append(" FROM v_steps ");
            strSql.Append(" where f_id=@f_id order by order_no asc  ");

            SqlParameter[] parameters = {
					new SqlParameter("@f_id", SqlDbType.Int,4)
     		};
            parameters[0].Value = flowID;

            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        //根据AppID获得步骤列表
        public DataSet GetFlowStepListByAppID(int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select s_id,s_name,f_name,step_type_name,order_no,app_id,f_id ");
            strSql.Append(" FROM v_steps ");
            strSql.Append(" where app_id=@app_id order by s_id desc ");

            SqlParameter[] parameters ={
                     new SqlParameter("@app_id",SqlDbType.Int,4)                 
                                      };
            parameters[0].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        #endregion
    }
}