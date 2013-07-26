using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:flows
    /// </summary>
    public partial class flowsDAL
    {
        public flowsDAL()
		{}
		#region  BasicMethod

        /// <summary>
        /// 某系统中流程信息是否已经存在
        /// </summary>
        public bool ExistsFlowName(string name, int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from flows");
            strSql.Append(" where name=@name and deleted=0 and app_id=@app_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
                    new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = name;
            parameters[1].Value = appID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条流程信息记录
        /// </summary>
        public int Add(Saron.WorkFlowService.Model.flowsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into flows(");
            strSql.Append("name,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip,app_id)");
            strSql.Append(" values (");
            strSql.Append("@name,@remark,@invalid,@deleted,@created_at,@created_by,@created_ip,@updated_at,@updated_by,@updated_ip,@app_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@app_id", SqlDbType.Int,4)
            };
            parameters[0].Value = model.name;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.invalid;
            parameters[3].Value = model.deleted;
            parameters[4].Value = model.created_at;
            parameters[5].Value = model.created_by;
            parameters[6].Value = model.created_ip;
            parameters[7].Value = model.updated_at;
            parameters[8].Value = model.updated_by;
            parameters[9].Value = model.updated_ip;
            parameters[10].Value = model.app_id;

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
        /// 更新一条流程信息记录
        /// </summary>
        public bool Update(Saron.WorkFlowService.Model.flowsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flows set ");
            strSql.Append("name=@name,");
            strSql.Append("remark=@remark,");
            strSql.Append("invalid=@invalid,");
            strSql.Append("deleted=@deleted,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("created_by=@created_by,");
            strSql.Append("created_ip=@created_ip,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("updated_by=@updated_by,");
            strSql.Append("updated_ip=@updated_ip,");
            strSql.Append("app_id=@app_id");
            strSql.Append(" where id=@id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@app_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = model.name;
            parameters[1].Value = model.remark;
            parameters[2].Value = model.invalid;
            parameters[3].Value = model.deleted;
            parameters[4].Value = model.created_at;
            parameters[5].Value = model.created_by;
            parameters[6].Value = model.created_ip;
            parameters[7].Value = model.updated_at;
            parameters[8].Value = model.updated_by;
            parameters[9].Value = model.updated_ip;
            parameters[10].Value = model.app_id;
            parameters[11].Value = model.id;
            

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
        /// 删除一条流程信息记录（不做物理删除）
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flows set deleted=1 where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)		
            };
            parameters[0].Value = id;

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
        /// 获得系统流程信息列表
        /// </summary>
        public DataSet GetListOfFlows(int appId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip,app_id  ");
            strSql.Append(" FROM flows ");
            strSql.Append(" where app_id=@app_id and deleted=0 order by id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据流程名称获得系统流程信息列表
        /// </summary>
        public DataSet GetListOfFlowsByName(string flowName,int appId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip,app_id  ");
            strSql.Append(" FROM flows ");
            strSql.Append(" where app_id=@app_id and name=@name and deleted=0 order by id desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4),
                    new SqlParameter("@name", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = appId;
            parameters[1].Value = flowName;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个流程信息的对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.flowsModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip,app_id from flows ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            Saron.WorkFlowService.Model.flowsModel model = new Model.flowsModel();
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
        /// 将DataRow类型数据转化为flowsmodel类型
        /// </summary>
        public Saron.WorkFlowService.Model.flowsModel DataRowToModel(DataRow row)
        {
            Saron.WorkFlowService.Model.flowsModel model = new Saron.WorkFlowService.Model.flowsModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["invalid"] != null && row["invalid"].ToString() != "")
                {
                    if ((row["invalid"].ToString() == "1") || (row["invalid"].ToString().ToLower() == "true"))
                    {
                        model.invalid = true;
                    }
                    else
                    {
                        model.invalid = false;
                    }
                }
                if (row["deleted"] != null && row["deleted"].ToString() != "")
                {
                    if ((row["deleted"].ToString() == "1") || (row["deleted"].ToString().ToLower() == "true"))
                    {
                        model.deleted = true;
                    }
                    else
                    {
                        model.deleted = false;
                    }
                }
                if (row["created_at"] != null && row["created_at"].ToString() != "")
                {
                    model.created_at = DateTime.Parse(row["created_at"].ToString());
                }
                if (row["created_by"] != null && row["created_by"].ToString() != "")
                {
                    model.created_by = int.Parse(row["created_by"].ToString());
                }
                if (row["created_ip"] != null)
                {
                    model.created_ip = row["created_ip"].ToString();
                }
                if (row["updated_at"] != null && row["updated_at"].ToString() != "")
                {
                    model.updated_at = DateTime.Parse(row["updated_at"].ToString());
                }
                if (row["updated_by"] != null && row["updated_by"].ToString() != "")
                {
                    model.updated_by = int.Parse(row["updated_by"].ToString());
                }
                if (row["updated_ip"] != null)
                {
                    model.updated_ip = row["updated_ip"].ToString();
                }
                if (row["app_id"] != null && row["app_id"].ToString() != "")
                {
                    model.app_id = int.Parse(row["app_id"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}