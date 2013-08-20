using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:steps
    /// </summary>
    public partial class stepsDAL
    {
        public stepsDAL()
        { }

        #region BasicMethod
        /// <summary>
        /// 判断流程步骤名称是否存在
        /// </summary>
        public bool ExistStepName(string stepName,int flowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from steps");
            strSql.Append(" where name=@name and flow_id=@flow_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@flow_id", SqlDbType.Int,4)			};
            parameters[0].Value = stepName;
            parameters[1].Value = flowID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddStep(Saron.WorkFlowService.Model.stepsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into steps(");
            strSql.Append("name,remark,flow_id,step_type_id,repeat_count,invalid,order_no,deleted,created_at,created_by,created_ip)");
            strSql.Append(" values (");
            strSql.Append("@name,@remark,@flow_id,@step_type_id,@repeat_count,@invalid,@order_no,@deleted,@created_at,@created_by,@created_ip)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,100),
					new SqlParameter("@flow_id", SqlDbType.Int,4),
					new SqlParameter("@step_type_id", SqlDbType.Int,4),
					new SqlParameter("@repeat_count", SqlDbType.Int,4),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@order_no", SqlDbType.Int,4),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40)
            };

            parameters[0].Value = model.name;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.flow_id;
            parameters[3].Value = model.step_type_id;
            parameters[4].Value = model.repeat_count;
            parameters[5].Value = model.invalid;
            parameters[6].Value = model.order_no;
            parameters[7].Value = model.deleted;
            parameters[8].Value = model.created_at;
            parameters[9].Value = model.created_by;
            parameters[10].Value = model.created_ip;

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
        /// 删除一条数据
        /// </summary>
        public bool DeleteStep(int stepID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update steps set ");
            strSql.Append("deleted=@deleted ");
            strSql.Append(" where id=@id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@id", SqlDbType.Int,4)		
            };
            parameters[0].Value = 1;
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

        /// <summary>
        /// 根据步骤ID获得步骤名称
        /// </summary>
        public string GetStepName(int stepID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select name from steps ");

            strSql.Append(" where id=@id and deleted=0 ");

            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = stepID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return null;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 获得flow中最大步骤排序码
        /// </summary>
        public int GetFlowMaxOrderNum(int flowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(order_no) from steps ");
            
            strSql.Append(" where flow_id=@flow_id and deleted=0 ");
            
            SqlParameter[] parameters = {
					new SqlParameter("@flow_id", SqlDbType.Int,4)
            };
            parameters[0].Value = flowID;

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
        /// 设置步骤的重复次数
        /// </summary>
        public int GetRepeatCount(Saron.WorkFlowService.Model.stepsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top repeat_count from steps ");

            strSql.Append(" where flow_id=@flow_id and order_no=@order_no and deleted=0 ");

            SqlParameter[] parameters = {
					new SqlParameter("@flow_id", SqlDbType.Int,4),
                    new SqlParameter("@order_no", SqlDbType.Int,4)
            };
            parameters[0].Value = model.flow_id;
            parameters[1].Value = model.order_no;

            if (model.step_type_id == 1)
            {
                //顺序类型，repeat_count=0
                return 0;
            }
            else if (model.step_type_id == 2)
            {
                //并行类型
                object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 1;
                }
                else
                {
                    int max_repeatCount = Convert.ToInt32(obj);
                    return max_repeatCount + 1;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}