using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:flow_users
    /// </summary>
    public partial class flow_usersDAL
    {
        public flow_usersDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 添加一条流程步骤、用户对应记录
        /// </summary>
        public bool AddFlow_User(Saron.WorkFlowService.Model.flow_usersModel model)
        {
            #region 自动添加备注信息
            Saron.WorkFlowService.DAL.stepsDAL m_stepsDal = new stepsDAL();
            Saron.WorkFlowService.DAL.usersDAL m_userDal = new usersDAL();
            string m_stepName = m_stepsDal.GetStepName(model.step_id);
            string m_userName = m_userDal.GetUserName(model.user_id);
            model.remark = "流程步骤名称：" + m_stepName + ",步骤处理人：" + m_userName;
            #endregion
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into flow_users(");
            strSql.Append("step_id,user_id,remark)");
            strSql.Append(" values (");
            strSql.Append("@step_id,@user_id,@remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@step_id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,100)
            };

            parameters[0].Value = model.step_id;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.remark;

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
        /// 删除一条流程步骤、用户对应记录
        /// </summary>
        public bool DeleteFlow_User(int stepID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from flow_users ");
            strSql.Append(" where  step_id=@step_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@step_id", SqlDbType.Int,4)
            };
            parameters[0].Value = stepID;

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
        /// 是否存在用户id为user_id,步骤id为step_id的记录
        /// </summary>
        public bool ExistsFlowUser(int step_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from flow_users");
            strSql.Append(" where step_id=@step_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@step_id",SqlDbType.Int,4)
			};
            parameters[0].Value = step_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        //更新用户id为user_id,步骤id为step_id的记录
        public bool UpdateFlowUser(int step_id, int user_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flow_users set user_id=@user_id");
            strSql.Append(" where step_id=@step_id ");
            SqlParameter[] parameters = 
            { 
               new SqlParameter("@user_id",SqlDbType.Int,4),
               new SqlParameter("@step_id",SqlDbType.Int,4)
            };
            parameters[0].Value = user_id;
            parameters[1].Value = step_id;

            int row = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);

            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        #endregion
    }
}