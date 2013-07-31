using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:menus
    /// </summary>
    public class menusDAL
    {
        public menusDAL()
		{}
		
        #region  Method
		
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from menus");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 菜单编码是否已经存在
        /// </summary>
        public bool ExistsMenusCode(string code,int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from menus");
            strSql.Append(" where code=@code and app_id=@app_id and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.NVarChar,80),
                    new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = code;
            parameters[1].Value = appID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 菜单名称是否已经存在
        /// </summary>
        public bool ExistsMenusName(string name, int? parentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from menus");
            strSql.Append(" where name=@name and parent_id=@parent_id and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
                    new SqlParameter("@parent_id", SqlDbType.Int,4)
			};
            parameters[0].Value = name;
            parameters[1].Value = parentID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 菜单名称是否已经存在
        /// </summary>
        public bool ExistsTopMenusName(string name,int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from menus");
            strSql.Append(" where name=@name and parent_id is null and app_id=@app_id and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
                    new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = name;
            parameters[1].Value = appID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
        ///<summary>
        ///parentId不为空，获得某系统的菜单编码,父菜单编码
        /// </summary> 
        public DataSet GetCodeParentOfApp(int app_id, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select code,parent_id from menus");
            strSql.Append(" where app_id=@app_id and id=@id and deleted=0");
            SqlParameter[] parameters = { 
                            new SqlParameter("@app_id",SqlDbType.Int,4),
                            new SqlParameter("@id",SqlDbType.Int,4)
            };
            parameters[0].Value = app_id;
            parameters[1].Value = id;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        
        ///<summary>
        ///获得某系统的系统ID,parent_ID
        /// </summary> 
        public DataSet GetAllParentIdOfApp(int app_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,parent_id from menus");
            strSql.Append(" where app_id=@app_id and deleted=0");
            SqlParameter[] parameters = { 
                            new SqlParameter("@app_id",SqlDbType.Int,4)                        
                                        };
            parameters[0].Value = app_id;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
		
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Saron.WorkFlowService.Model.menusModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into menus(");
			strSql.Append("name,code,url,app_id,parent_id,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip)");
			strSql.Append(" values (");
			strSql.Append("@name,@code,@url,@app_id,@parent_id,@remark,@invalid,@deleted,@created_at,@created_by,@created_ip,@updated_at,@updated_by,@updated_ip)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
					new SqlParameter("@code", SqlDbType.NVarChar,80),
					new SqlParameter("@url", SqlDbType.NVarChar,100),
					new SqlParameter("@app_id", SqlDbType.Int,4),
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.code;
			parameters[2].Value = model.url;
			parameters[3].Value = model.app_id;
			parameters[4].Value = model.parent_id;
			parameters[5].Value = model.remark;
			parameters[6].Value = model.invalid;
			parameters[7].Value = model.deleted;
			parameters[8].Value = model.created_at;
			parameters[9].Value = model.created_by;
			parameters[10].Value = model.created_ip;
			parameters[11].Value = model.updated_at;
			parameters[12].Value = model.updated_by;
			parameters[13].Value = model.updated_ip;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(Saron.WorkFlowService.Model.menusModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update menus set ");
			strSql.Append("name=@name,");
			strSql.Append("code=@code,");
			strSql.Append("url=@url,");
			strSql.Append("app_id=@app_id,");
			strSql.Append("parent_id=@parent_id,");
			strSql.Append("remark=@remark,");
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
					new SqlParameter("@name", SqlDbType.NVarChar,80),
					new SqlParameter("@code", SqlDbType.NVarChar,80),
					new SqlParameter("@url", SqlDbType.NVarChar,100),
					new SqlParameter("@app_id", SqlDbType.Int,4),
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,200),
					new SqlParameter("@invalid", SqlDbType.Bit,1),
					new SqlParameter("@deleted", SqlDbType.Bit,1),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.code;
			parameters[2].Value = model.url;
			parameters[3].Value = model.app_id;
			parameters[4].Value = model.parent_id;
			parameters[5].Value = model.remark;
			parameters[6].Value = model.invalid;
			parameters[7].Value = model.deleted;
			parameters[8].Value = model.created_at;
			parameters[9].Value = model.created_by;
			parameters[10].Value = model.created_ip;
			parameters[11].Value = model.updated_at;
			parameters[12].Value = model.updated_by;
			parameters[13].Value = model.updated_ip;
			parameters[14].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from menus ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
        ///<summary>
        ///删除记录，令delete=1
        /// </summary>
        public bool DeleteMenus(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update menus set deleted=1 where id=@id");
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from menus ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Saron.WorkFlowService.Model.menusModel GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,name,code,url,app_id,parent_id,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from menus ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

            Saron.WorkFlowService.Model.menusModel model = new Saron.WorkFlowService.Model.menusModel();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["name"]!=null && ds.Tables[0].Rows[0]["name"].ToString()!="")
				{
					model.name=ds.Tables[0].Rows[0]["name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["code"]!=null && ds.Tables[0].Rows[0]["code"].ToString()!="")
				{
					model.code=ds.Tables[0].Rows[0]["code"].ToString();
				}
				if(ds.Tables[0].Rows[0]["url"]!=null && ds.Tables[0].Rows[0]["url"].ToString()!="")
				{
					model.url=ds.Tables[0].Rows[0]["url"].ToString();
				}
				if(ds.Tables[0].Rows[0]["app_id"]!=null && ds.Tables[0].Rows[0]["app_id"].ToString()!="")
				{
					model.app_id=int.Parse(ds.Tables[0].Rows[0]["app_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["parent_id"]!=null && ds.Tables[0].Rows[0]["parent_id"].ToString()!="")
				{
					model.parent_id=int.Parse(ds.Tables[0].Rows[0]["parent_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["remark"]!=null && ds.Tables[0].Rows[0]["remark"].ToString()!="")
				{
					model.remark=ds.Tables[0].Rows[0]["remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["invalid"]!=null && ds.Tables[0].Rows[0]["invalid"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["invalid"].ToString()=="1")||(ds.Tables[0].Rows[0]["invalid"].ToString().ToLower()=="true"))
					{
						model.invalid=true;
					}
					else
					{
						model.invalid=false;
					}
				}
				if(ds.Tables[0].Rows[0]["deleted"]!=null && ds.Tables[0].Rows[0]["deleted"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["deleted"].ToString()=="1")||(ds.Tables[0].Rows[0]["deleted"].ToString().ToLower()=="true"))
					{
						model.deleted=true;
					}
					else
					{
						model.deleted=false;
					}
				}
				if(ds.Tables[0].Rows[0]["created_at"]!=null && ds.Tables[0].Rows[0]["created_at"].ToString()!="")
				{
					model.created_at=DateTime.Parse(ds.Tables[0].Rows[0]["created_at"].ToString());
				}
				if(ds.Tables[0].Rows[0]["created_by"]!=null && ds.Tables[0].Rows[0]["created_by"].ToString()!="")
				{
					model.created_by=int.Parse(ds.Tables[0].Rows[0]["created_by"].ToString());
				}
				if(ds.Tables[0].Rows[0]["created_ip"]!=null && ds.Tables[0].Rows[0]["created_ip"].ToString()!="")
				{
					model.created_ip=ds.Tables[0].Rows[0]["created_ip"].ToString();
				}
				if(ds.Tables[0].Rows[0]["updated_at"]!=null && ds.Tables[0].Rows[0]["updated_at"].ToString()!="")
				{
					model.updated_at=DateTime.Parse(ds.Tables[0].Rows[0]["updated_at"].ToString());
				}
				if(ds.Tables[0].Rows[0]["updated_by"]!=null && ds.Tables[0].Rows[0]["updated_by"].ToString()!="")
				{
					model.updated_by=int.Parse(ds.Tables[0].Rows[0]["updated_by"].ToString());
				}
				if(ds.Tables[0].Rows[0]["updated_ip"]!=null && ds.Tables[0].Rows[0]["updated_ip"].ToString()!="")
				{
					model.updated_ip=ds.Tables[0].Rows[0]["updated_ip"].ToString();
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,name,code,url,app_id,parent_id,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
			strSql.Append(" FROM menus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 获得系统菜单列表
        /// </summary>
        /// <param name="appId">系统ID</param>
        /// <returns></returns>
        public DataSet GetListOfApp(int appId)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,name,code,url,app_id,parent_id,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
			strSql.Append(" FROM menus ");
            strSql.Append(" where app_id=@app_id and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appId;
			return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 获得系统菜单名称
        /// </summary>
        /// <param name="appId">系统ID</param>
        /// <returns></returns>
        public DataSet GetMenuNameOfAppID(int appId,int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name");
            strSql.Append(" FROM menus ");
            strSql.Append(" where app_id=@app_id and id=@Id and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4),
                    new SqlParameter("@Id",SqlDbType.Int,4)
			};
            parameters[0].Value = appId;
            parameters[1].Value = Id;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得系统菜单名称
        /// </summary>
        /// <param name="appId">应用系统ID</param>
        /// <returns></returns>
        public DataSet GetMenuNameOfAppParent(int appId, int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select name");
            strSql.Append(" FROM menus ");
            strSql.Append(" where app_id=@app_id and parent_id=@parent_id and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4),
                    new SqlParameter("@parent_id",SqlDbType.Int,4)
			};
            parameters[0].Value = appId;
            parameters[1].Value = parentId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得系统某菜单的子菜单
        /// </summary>
        /// <param name="parentId">父菜单ID</param>
        /// <returns>子菜数据列表</returns>
        public DataSet GetChildrenMenusListOfApp(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,code,url,app_id,parent_id,remark,invalid ");
            strSql.Append(" FROM menus ");
            strSql.Append(" where parent_id=@parent_id and deleted=0 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@parent_id", SqlDbType.Int,4)
			};
            parameters[0].Value = parentId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获得某系统的顶级菜单
        /// </summary>
        /// <param name="appId">系统ID</param>
        /// <param name="parentId">父菜单ID</param>
        /// <returns>子菜数据列表</returns>
        public DataSet GetTopMenusListOfApp(int appId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,code,url,app_id,parent_id,remark,invalid ");
            strSql.Append(" FROM menus ");
            strSql.Append(" where app_id=@app_id and parent_id is null and deleted=0 ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4),
			};
            parameters[0].Value = appId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 菜单是否存在子菜单
        /// </summary>
        /// <param name="parentId">父菜单ID</param>
        /// <returns></returns>
        public bool ExistsChildrenMenus(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from menus");
            strSql.Append(" where parent_id=@parent_id and deleted=0");
            SqlParameter[] parameters = {
					new SqlParameter("@parent_id", SqlDbType.Int,4)
			};
            parameters[0].Value = parentId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,name,code,url,app_id,parent_id,remark,invalid,deleted,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
			strSql.Append(" FROM menus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM menus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from menus T ");
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
