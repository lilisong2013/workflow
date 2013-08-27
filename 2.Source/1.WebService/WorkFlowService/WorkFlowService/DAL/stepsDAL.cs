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
        /// 获得一条步骤列表
        /// </summary>
        public DataSet GetStepListByID(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,remark,flow_id,step_type_id,repeat_count,invalid,order_no,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from steps ");
            strSql.Append(" where id=@id and deleted=0 ");
            
            SqlParameter[] parameters = { 
                         new SqlParameter("@id",SqlDbType.Int,4)                            
            };
            parameters[0].Value = id;
            return DbHelperSQL.Query(strSql.ToString(),parameters);

        }

        public DataSet GetStepListOfFlowID(int flowid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,remark,flow_id,step_type_id,repeat_count,invalid,order_no,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from steps ");
            strSql.Append(" where flow_id=@flow_id and deleted=0 ");

            SqlParameter[] parameters = { 
                         new SqlParameter("@flow_id",SqlDbType.Int,4)                            
            };
            parameters[0].Value = flowid;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
        /// 是否存在并行的个数
        /// </summary>
        public int ExistStpeType(int flowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(distinct(order_no)) from steps");
            strSql.Append(" where flow_id=@flow_id and deleted=0 and step_type_id=2");
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
        /// 获得flow中最大步骤排序码(顺序、并序状态下)
        /// </summary>
        public int GetFlowMaxOrderNum(int flowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(order_no) from steps ");

            strSql.Append(" where flow_id=@flow_id and deleted=0");
            
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
        /// 获得flow中最大步骤排序码(并序状态下)
        /// </summary>
        public int GetFlowMaxNumOfUnorder(int flowID) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(repeat_count) from steps ");

            strSql.Append(" where flow_id=@flow_id and deleted=0 and step_type_id=2 ");

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
        /// 获得flow中最大步骤排序码(并序状态下)
        /// </summary>
        public DataSet GetFlowMaxOrderNumOfUnorder(int flowID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(repeat_count),max(order_no) from steps ");

            strSql.Append(" where flow_id=@flow_id and deleted=0 and step_type_id=2 ");

            SqlParameter[] parameters = {
					new SqlParameter("@flow_id", SqlDbType.Int,4)
            };
            parameters[0].Value = flowID;
           
            return DbHelperSQL.Query(strSql.ToString(), parameters);
          
        }

        /// <summary>
        /// 设置步骤的重复次数
        /// </summary>
        public int GetRepeatCount(Saron.WorkFlowService.Model.stepsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select repeat_count from steps ");

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

        /// <summary>
        /// 得到一个steps对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.stepsModel GetModel(int id) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,name,remark,flow_id,step_type_id,repeat_count,invalid,order_no,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from steps ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            Saron.WorkFlowService.Model.stepsModel model = new Model.stepsModel();

            DataSet ds = DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["flow_id"] != null && ds.Tables[0].Rows[0]["flow_id"].ToString() != "")
                {
                    model.flow_id = int.Parse(ds.Tables[0].Rows[0]["flow_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["step_type_id"] != null && ds.Tables[0].Rows[0]["step_type_id"].ToString() != "")
                {
                    model.step_type_id = int.Parse(ds.Tables[0].Rows[0]["step_type_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["repeat_count"] != null && ds.Tables[0].Rows[0]["repeat_count"].ToString() != "")
                {
                    model.repeat_count = int.Parse(ds.Tables[0].Rows[0]["repeat_count"].ToString());
                }
                           
                if (ds.Tables[0].Rows[0]["invalid"] != null && ds.Tables[0].Rows[0]["invalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["invalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["invalid"].ToString().ToLower() == "true"))
                    {
                        model.invalid = true;
                    }
                    else
                    {
                        model.invalid = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["order_no"] != null && ds.Tables[0].Rows[0]["order_no"].ToString() != "")
                {
                    model.order_no = int.Parse(ds.Tables[0].Rows[0]["order_no"].ToString());
                }
                if (ds.Tables[0].Rows[0]["deleted"] != null && ds.Tables[0].Rows[0]["deleted"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["deleted"].ToString() == "1") || (ds.Tables[0].Rows[0]["deleted"].ToString().ToLower() == "true"))
                    {
                        model.deleted = true;
                    }
                    else
                    {
                        model.deleted = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["created_at"] != null && ds.Tables[0].Rows[0]["created_at"].ToString() != "")
                {
                    model.created_at = DateTime.Parse(ds.Tables[0].Rows[0]["created_at"].ToString());
                }
                if (ds.Tables[0].Rows[0]["created_by"] != null && ds.Tables[0].Rows[0]["created_by"].ToString() != "")
                {
                    model.created_by = int.Parse(ds.Tables[0].Rows[0]["created_by"].ToString());
                }
                if (ds.Tables[0].Rows[0]["created_ip"] != null && ds.Tables[0].Rows[0]["created_ip"].ToString() != "")
                {
                    model.created_ip = ds.Tables[0].Rows[0]["created_ip"].ToString();
                }
                if (ds.Tables[0].Rows[0]["updated_at"] != null && ds.Tables[0].Rows[0]["updated_at"].ToString() != "")
                {
                    model.updated_at = DateTime.Parse(ds.Tables[0].Rows[0]["updated_at"].ToString());
                }
                if (ds.Tables[0].Rows[0]["updated_by"] != null && ds.Tables[0].Rows[0]["updated_by"].ToString() != "")
                {
                    model.updated_by = int.Parse(ds.Tables[0].Rows[0]["updated_by"].ToString());
                }
                if (ds.Tables[0].Rows[0]["updated_ip"] != null && ds.Tables[0].Rows[0]["updated_ip"].ToString() != "")
                {
                    model.updated_ip = ds.Tables[0].Rows[0]["updated_ip"].ToString();
                }
             
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Saron.WorkFlowService.Model.stepsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update steps set ");
            strSql.Append("name=@name,");
            strSql.Append("remark=@remark,");
            strSql.Append("flow_id=@flow_id,");
            strSql.Append("step_type_id=@step_type_id,");
            strSql.Append("repeat_count=@repeat_count,");
            strSql.Append("invalid=@invalid,");
            strSql.Append("order_no=@order_no,");
            strSql.Append("deleted=@deleted,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("created_by=@created_by,");
            strSql.Append("created_ip=@created_ip,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("updated_by=@updated_by,");
            strSql.Append("updated_ip=@updated_ip");
            strSql.Append(" where id=@id");

            SqlParameter[] parameters = { 
                    new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,100),
					new SqlParameter("@flow_id", SqlDbType.Int,4),
					new SqlParameter("@step_type_id",SqlDbType.Int,4),
					new SqlParameter("@repeat_count",SqlDbType.Int,4),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
				    new SqlParameter("@order_no",SqlDbType.Int,4),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@id", SqlDbType.Int,4)
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
            parameters[11].Value = model.updated_at;
            parameters[12].Value = model.updated_by;
            parameters[13].Value = model.updated_ip;
            parameters[14].Value = model.id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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