﻿using System;
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
            strSql.Append("id,step_id,user_id,remark)");
            strSql.Append(" values (");
            strSql.Append("@id,@step_id,@user_id,@remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@step_id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.step_id;
            parameters[2].Value = model.user_id;
            parameters[3].Value = model.remark;

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
            parameters[1].Value = stepID;

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
        #endregion
    }
}