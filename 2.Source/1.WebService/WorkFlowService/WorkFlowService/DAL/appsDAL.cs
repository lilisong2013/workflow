using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作
namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:apps
    /// </summary>
    public partial class appsDAL
    {
        public appsDAL()
        { }
        #region  Method
        /// <summary>
        /// 是否存在id号为appId的该记录
        /// </summary>
        public bool Exists(int appId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from apps");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = appId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在系统名称为appName该记录
        /// </summary>
        public bool ExistsName(string appName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from apps");
            strSql.Append(" where name=@name ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80)
			};
            parameters[0].Value = appName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">apps实体</param>
        /// <returns>成功，返回记录ID；失败，返回0</returns>
        public int Add(Saron.WorkFlowService.Model.appsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into apps(");
            strSql.Append("name,code,url,remark,invalid,created_at,created_ip,updated_at,updated_by,updated_ip,apply_at,approval_at)");
            strSql.Append(" values (");
            strSql.Append("@name,@code,@url,@remark,@invalid,@created_at,@created_ip,@updated_at,@updated_by,@updated_ip,@apply_at,@approval_at)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
					new SqlParameter("@code", SqlDbType.NVarChar,40),
					new SqlParameter("@url", SqlDbType.NVarChar,80),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
                    new SqlParameter("@apply_at", SqlDbType.DateTime),
					new SqlParameter("@approval_at", SqlDbType.DateTime)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.code;
            parameters[2].Value = model.url;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.invalid;
            parameters[5].Value = model.created_at;
            parameters[6].Value = model.created_ip;
            parameters[7].Value = model.updated_at;
            parameters[8].Value = model.updated_by;
            parameters[9].Value = model.updated_ip;
            parameters[10].Value = model.apply_at;
            parameters[11].Value = model.approval_at;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Saron.WorkFlowService.Model.appsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update apps set ");
            strSql.Append("name=@name,");
            strSql.Append("code=@code,");
            strSql.Append("url=@url,");
            strSql.Append("remark=@remark,");
            strSql.Append("invalid=@invalid,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("created_ip=@created_ip,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("updated_by=@updated_by,");
            strSql.Append("updated_ip=@updated_ip");
            strSql.Append("apply_at=@apply_at,");
            strSql.Append("approval_at=@approval_at");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
					new SqlParameter("@code", SqlDbType.NVarChar,40),
					new SqlParameter("@url", SqlDbType.NVarChar,80),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
                    new SqlParameter("@apply_at", SqlDbType.DateTime),
					new SqlParameter("@approval_at", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.code;
            parameters[2].Value = model.url;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.invalid;
            parameters[5].Value = model.created_at;
            parameters[6].Value = model.created_ip;
            parameters[7].Value = model.updated_at;
            parameters[8].Value = model.updated_by;
            parameters[9].Value = model.updated_ip;
            parameters[10].Value = model.apply_at;
            parameters[11].Value = model.approval_at;
            parameters[12].Value = model.id;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from apps ");
            strSql.Append(" where id=@id");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from apps ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.appsModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,code,url,remark,invalid,created_at,created_ip,updated_at,updated_by,updated_ip,apply_at,approval_at from apps ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Saron.WorkFlowService.Model.appsModel model = new Saron.WorkFlowService.Model.appsModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
                if (ds.Tables[0].Rows[0]["code"] != null && ds.Tables[0].Rows[0]["code"].ToString() != "")
                {
                    model.code = ds.Tables[0].Rows[0]["code"].ToString();
                }
                if (ds.Tables[0].Rows[0]["url"] != null && ds.Tables[0].Rows[0]["url"].ToString() != "")
                {
                    model.url = ds.Tables[0].Rows[0]["url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
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
                if (ds.Tables[0].Rows[0]["created_at"] != null && ds.Tables[0].Rows[0]["created_at"].ToString() != "")
                {
                    model.created_at = DateTime.Parse(ds.Tables[0].Rows[0]["created_at"].ToString());
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
                if (ds.Tables[0].Rows[0]["apply_at"] != null && ds.Tables[0].Rows[0]["apply_at"].ToString() != "")
                {
                    model.apply_at = DateTime.Parse(ds.Tables[0].Rows[0]["apply_at"].ToString());
                }
                if (ds.Tables[0].Rows[0]["approval_at"] != null && ds.Tables[0].Rows[0]["approval_at"].ToString() != "")
                {
                    model.approval_at = DateTime.Parse(ds.Tables[0].Rows[0]["approval_at"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,code,url,remark,invalid,created_at,created_ip,updated_at,updated_by,updated_ip,apply_at,approval_at ");
            strSql.Append(" FROM apps ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,name,code,url,remark,invalid,created_at,created_ip,updated_at,updated_by,updated_ip,apply_at,approval_at ");
            strSql.Append(" FROM apps ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM apps ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from apps T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  Method
    }
}

