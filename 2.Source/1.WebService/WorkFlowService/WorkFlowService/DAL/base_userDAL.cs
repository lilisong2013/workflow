using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作
namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:base_user
    /// </summary>
    public partial class base_userDAL
    {
        public base_userDAL()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_user");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Decimal)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// （超级管理员登录）是否存在用户或密码
        /// </summary>
        public bool Exists(string login, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_user");
            strSql.Append(" where login=@login and password=dbo.f_tobase64(HASHBYTES('md5', CONVERT(nvarchar,@password))) ");
            SqlParameter[] parameters = {
					new SqlParameter("@login", SqlDbType.NVarChar,40),
					new SqlParameter("@password", SqlDbType.NVarChar,255)};
            parameters[0].Value = login;
            parameters[1].Value = password;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Saron.WorkFlowService.Model.base_userModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into base_user(");
            strSql.Append("login,password,name,mobile_phone,mail,remark,admin,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip)");
            strSql.Append(" values (");
            strSql.Append("@login,dbo.f_tobase64(HASHBYTES('md5', CONVERT(nvarchar,@password))),@name,@mobile_phone,@mail,@remark,@admin,@invalid,@deleted,@created_at,@created_by,@created_ip,@updated_at,@updated_by,@updated_ip)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@login", SqlDbType.NVarChar,40),
					new SqlParameter("@password", SqlDbType.NVarChar,255),
					new SqlParameter("@name", SqlDbType.NVarChar,40),
					new SqlParameter("@mobile_phone", SqlDbType.NVarChar,40),
					new SqlParameter("@mail", SqlDbType.NVarChar,40),
					new SqlParameter("@remark", SqlDbType.NVarChar,80),
					new SqlParameter("@admin", SqlDbType.Bit,1),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40)};
            parameters[0].Value = model.login;
            parameters[1].Value = model.password;
            parameters[2].Value = model.name;
            parameters[3].Value = model.mobile_phone;
            parameters[4].Value = model.mail;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.admin;
            parameters[7].Value = model.invalid;
            parameters[8].Value = model.deleted;
            parameters[9].Value = model.created_at;
            parameters[10].Value = model.created_by;
            parameters[11].Value = model.created_ip;
            parameters[12].Value = model.updated_at;
            parameters[13].Value = model.updated_by;
            parameters[14].Value = model.updated_ip;

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
        public bool Update(Saron.WorkFlowService.Model.base_userModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update base_user set ");
            strSql.Append("login=@login,");
            strSql.Append("password=@password,");
            strSql.Append("name=@name,");
            strSql.Append("mobile_phone=@mobile_phone,");
            strSql.Append("mail=@mail,");
            strSql.Append("remark=@remark,");
            strSql.Append("admin=@admin,");
            strSql.Append("invalid=@invalid,");
            strSql.Append("deleted=@deleted,");
            strSql.Append("created_at=@created_at,");
            strSql.Append("created_by=@created_by,");
            strSql.Append("created_ip=@created_ip,");
            strSql.Append("updated_at=@updated_at,");
            strSql.Append("updated_by=@updated_by,");
            strSql.Append("updated_ip=@updated_ip");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@login", SqlDbType.NVarChar,40),
					new SqlParameter("@password", SqlDbType.NVarChar,255),
					new SqlParameter("@name", SqlDbType.NVarChar,40),
					new SqlParameter("@mobile_phone", SqlDbType.NVarChar,40),
					new SqlParameter("@mail", SqlDbType.NVarChar,40),
					new SqlParameter("@remark", SqlDbType.NVarChar,80),
					new SqlParameter("@admin", SqlDbType.Bit,1),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.login;
            parameters[1].Value = model.password;
            parameters[2].Value = model.name;
            parameters[3].Value = model.mobile_phone;
            parameters[4].Value = model.mail;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.admin;
            parameters[7].Value = model.invalid;
            parameters[8].Value = model.deleted;
            parameters[9].Value = model.created_at;
            parameters[10].Value = model.created_by;
            parameters[11].Value = model.created_ip;
            parameters[12].Value = model.updated_at;
            parameters[13].Value = model.updated_by;
            parameters[14].Value = model.updated_ip;
            parameters[15].Value = model.id;

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
            strSql.Append("delete from base_user ");
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
            strSql.Append("delete from base_user ");
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
        /// 得到一个base_user对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.base_userModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,login,password,name,mobile_phone,mail,remark,admin,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from base_user ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Decimal)
			};
            parameters[0].Value = id;

            Saron.WorkFlowService.Model.base_userModel model = new Saron.WorkFlowService.Model.base_userModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["login"] != null && ds.Tables[0].Rows[0]["login"].ToString() != "")
                {
                    model.login = ds.Tables[0].Rows[0]["login"].ToString();
                }
                if (ds.Tables[0].Rows[0]["password"] != null && ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    model.password = ds.Tables[0].Rows[0]["password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile_phone"] != null && ds.Tables[0].Rows[0]["mobile_phone"].ToString() != "")
                {
                    model.mobile_phone = ds.Tables[0].Rows[0]["mobile_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mail"] != null && ds.Tables[0].Rows[0]["mail"].ToString() != "")
                {
                    model.mail = ds.Tables[0].Rows[0]["mail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["admin"] != null && ds.Tables[0].Rows[0]["admin"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["admin"].ToString() == "1") || (ds.Tables[0].Rows[0]["admin"].ToString().ToLower() == "true"))
                    {
                        model.admin = true;
                    }
                    else
                    {
                        model.admin = false;
                    }
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
        /// 得到一个base_user对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.base_userModel GetModel(string login)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,login,password,name,mobile_phone,mail,remark,admin,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from base_user ");
            strSql.Append(" where login=@login");
            SqlParameter[] parameters = {
					new SqlParameter("@login", SqlDbType.NVarChar,40)
			};
            parameters[0].Value = login;

            Saron.WorkFlowService.Model.base_userModel model = new Saron.WorkFlowService.Model.base_userModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["login"] != null && ds.Tables[0].Rows[0]["login"].ToString() != "")
                {
                    model.login = ds.Tables[0].Rows[0]["login"].ToString();
                }
                if (ds.Tables[0].Rows[0]["password"] != null && ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    model.password = ds.Tables[0].Rows[0]["password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile_phone"] != null && ds.Tables[0].Rows[0]["mobile_phone"].ToString() != "")
                {
                    model.mobile_phone = ds.Tables[0].Rows[0]["mobile_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mail"] != null && ds.Tables[0].Rows[0]["mail"].ToString() != "")
                {
                    model.mail = ds.Tables[0].Rows[0]["mail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["admin"] != null && ds.Tables[0].Rows[0]["admin"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["admin"].ToString() == "1") || (ds.Tables[0].Rows[0]["admin"].ToString().ToLower() == "true"))
                    {
                        model.admin = true;
                    }
                    else
                    {
                        model.admin = false;
                    }
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,login,password,name,mobile_phone,mail,remark,admin,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
            strSql.Append(" FROM base_user ");
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
            strSql.Append(" id,login,password,name,mobile_phone,mail,remark,admin,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
            strSql.Append(" FROM base_user ");
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
            strSql.Append("select count(1) FROM base_user ");
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
            strSql.Append(")AS Row, T.*  from base_user T ");
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

