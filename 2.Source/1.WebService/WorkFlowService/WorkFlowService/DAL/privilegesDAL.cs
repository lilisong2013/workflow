﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:privileges
    /// </summary>
    public class privilegesDAL
    {
        public privilegesDAL()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from privileges");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 某种权限类型下某种权限项目的权限是否已经存在
        /// </summary>
        public bool ExistsItemOfPrivilegesType(int privilegesTypeID,int privilegesItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from privileges");
            strSql.Append(" where privilegetype_id=@privilegetype_id and privilegeitem_id=@privilegeitem_id");
            SqlParameter[] parameters = {
					new SqlParameter("@privilegetype_id", SqlDbType.Int,4),
                    new SqlParameter("@privilegeitem_id", SqlDbType.Int,4)
			};
            parameters[0].Value = privilegesTypeID;
            parameters[1].Value = privilegesItemID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 某系统中是否存在权限名称
        /// </summary>
        /// <param name="privilegeName">权限名称</param>
        /// <param name="appID">系统ID</param>
        /// <returns></returns>
        public bool ExistsPrivilegesName(string privilegeName,int appID,int pm_typeid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from privileges");
            strSql.Append(" where name=@name and app_id=@app_id and privilegetype_id=@privilegetype_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
                    new SqlParameter("@app_id", SqlDbType.Int,4),
                    new SqlParameter("@privilegetype_id",SqlDbType.Int,4)
			};
            parameters[0].Value = privilegeName;
            parameters[1].Value = appID;
            parameters[2].Value = pm_typeid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        //判断权限列表下菜单是否有子菜单
        public bool ExistsChildrenPMenus(int parentID,int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  v_menu_privileges");
            strSql.Append(" where parent_id=@parent_id and app_id=@app_id");
            SqlParameter[] parameters ={
                     new SqlParameter("@parent_id",SqlDbType.Int,4),
                     new SqlParameter("@app_id",SqlDbType.Int,4)
                                      };
            parameters[0].Value = parentID;
            parameters[1].Value = appID;
            return DbHelperSQL.Exists(strSql.ToString(),parameters);
        }

        public DataSet GetItemIDByPID(int ID, int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select item_id from v_menu_privileges");
            strSql.Append(" where p_id=@p_id and app_id=@app_id");
            SqlParameter[] parameters ={
                     new SqlParameter("@p_id",SqlDbType.Int,4),
                     new SqlParameter("@app_id",SqlDbType.Int,4)
                                      };
            parameters[0].Value = ID;
            parameters[1].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
       
        //获得系统某菜单的子菜单
        public DataSet GetChildrenPMenusListOfApp(int parentID, int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  p_id, p_name, pt_id, pt_name, pt_code, item_name, item_code, item_id, parent_id, app_id");
            strSql.Append(" from   v_menu_privileges ");
            strSql.Append(" where parent_id=@parent_id and app_id=@app_id ");
            SqlParameter[] parameters ={
                  new SqlParameter("@parent_id",SqlDbType.Int,4),
                  new SqlParameter("@app_id",SqlDbType.Int,4) 
                                      };
            parameters[0].Value = parentID;
            parameters[1].Value = appID;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Saron.WorkFlowService.Model.privilegesModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into privileges(");
			strSql.Append("name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip)");
			strSql.Append(" values (");
			strSql.Append("@name,@privilegetype_id,@privilegeitem_id,@remark,@app_id,@created_at,@created_by,@created_ip,@updated_at,@updated_by,@updated_ip)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
					new SqlParameter("@privilegetype_id", SqlDbType.Int,4),
					new SqlParameter("@privilegeitem_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,80),
					new SqlParameter("@app_id", SqlDbType.Int,4),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.privilegetype_id;
			parameters[2].Value = model.privilegeitem_id;
			parameters[3].Value = model.remark;
			parameters[4].Value = model.app_id;
			parameters[5].Value = model.created_at;
			parameters[6].Value = model.created_by;
			parameters[7].Value = model.created_ip;
			parameters[8].Value = model.updated_at;
			parameters[9].Value = model.updated_by;
			parameters[10].Value = model.updated_ip;

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
		public bool Update(Saron.WorkFlowService.Model.privilegesModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update privileges set ");
			strSql.Append("name=@name,");
			strSql.Append("remark=@remark,");
			strSql.Append("created_at=@created_at,");
			strSql.Append("created_by=@created_by,");
			strSql.Append("created_ip=@created_ip,");
			strSql.Append("updated_at=@updated_at,");
			strSql.Append("updated_by=@updated_by,");
			strSql.Append("updated_ip=@updated_ip");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,80),
					new SqlParameter("@remark", SqlDbType.NVarChar,80),
					new SqlParameter("@created_at", SqlDbType.DateTime),
					new SqlParameter("@created_by", SqlDbType.Int,4),
					new SqlParameter("@created_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@updated_at", SqlDbType.DateTime),
					new SqlParameter("@updated_by", SqlDbType.Int,4),
					new SqlParameter("@updated_ip", SqlDbType.NVarChar,40),
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@privilegetype_id", SqlDbType.Int,4),
					new SqlParameter("@privilegeitem_id", SqlDbType.Int,4),
					new SqlParameter("@app_id", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.remark;
			parameters[2].Value = model.created_at;
			parameters[3].Value = model.created_by;
			parameters[4].Value = model.created_ip;
			parameters[5].Value = model.updated_at;
			parameters[6].Value = model.updated_by;
			parameters[7].Value = model.updated_ip;
			parameters[8].Value = model.id;
			parameters[9].Value = model.privilegetype_id;
			parameters[10].Value = model.privilegeitem_id;
			parameters[11].Value = model.app_id;

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
			strSql.Append("delete from privileges ");
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
		
        /// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from privileges ");
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
		public Saron.WorkFlowService.Model.privilegesModel GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip from privileges ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

            Saron.WorkFlowService.Model.privilegesModel model = new Saron.WorkFlowService.Model.privilegesModel();
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
				if(ds.Tables[0].Rows[0]["privilegetype_id"]!=null && ds.Tables[0].Rows[0]["privilegetype_id"].ToString()!="")
				{
					model.privilegetype_id=int.Parse(ds.Tables[0].Rows[0]["privilegetype_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["privilegeitem_id"]!=null && ds.Tables[0].Rows[0]["privilegeitem_id"].ToString()!="")
				{
					model.privilegeitem_id=int.Parse(ds.Tables[0].Rows[0]["privilegeitem_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["remark"]!=null && ds.Tables[0].Rows[0]["remark"].ToString()!="")
				{
					model.remark=ds.Tables[0].Rows[0]["remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["app_id"]!=null && ds.Tables[0].Rows[0]["app_id"].ToString()!="")
				{
					model.app_id=int.Parse(ds.Tables[0].Rows[0]["app_id"].ToString());
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
			strSql.Append("select id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
			strSql.Append(" FROM privileges ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        
        //获得权限列表中菜单，操作，功能中元素的权限项目列表信息
        public DataSet GetItemIDOfPrivileges(int appID,int flag)
        {
            StringBuilder strSql = new StringBuilder();
            //菜单ID列表
            if (flag == 1)
            {
                strSql.Append("select id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
                strSql.Append(" FROM privileges ");
                strSql.Append(" where app_id=@app_id and privilegetype_id=1");
                SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			    };
                parameters[0].Value = appID;
                return DbHelperSQL.Query(strSql.ToString(), parameters);
            }
            //元素ID列表
            else  if (flag == 2) 
            {
                strSql.Append("select id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
                strSql.Append(" FROM privileges ");
                strSql.Append(" where app_id=@app_id and privilegetype_id=2");
                SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			    };
                parameters[0].Value = appID;
                return DbHelperSQL.Query(strSql.ToString(), parameters);
            }
            //操作ID列表
            else
            {
                strSql.Append("select id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
                strSql.Append(" FROM privileges ");
                strSql.Append(" where app_id=@app_id and privilegetype_id=3");
                SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4)
			    };
                parameters[0].Value = appID;
                return DbHelperSQL.Query(strSql.ToString(), parameters);
            }
           
        }
       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListByAppID(int appID,int ptID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
            strSql.Append(" FROM privileges ");
            strSql.Append(" where app_id=@app_id and privilegetype_id=@privilegetype_id");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4),
                    new SqlParameter("@privilegetype_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;
            parameters[1].Value = ptID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
   
    
        /// <summary>
        /// 获得某种权限类型下的权限列表
        /// </summary>
        /// <param name="privilegeTypeID">权限类型ID</param>
        /// <param name="appID">系统ID</param>
        /// <returns></returns>
        public DataSet GetListByPrivilegeType(int privilegeTypeID, int appID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
            strSql.Append(" FROM privileges ");
            strSql.Append(" where app_id=@app_id and privilegetype_id=@privilegetype_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@app_id", SqlDbType.Int,4),
                    new SqlParameter("@privilegetype_id", SqlDbType.Int,4)
			};
            parameters[0].Value = appID;
            parameters[1].Value = privilegeTypeID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
			strSql.Append(" id,name,privilegetype_id,privilegeitem_id,remark,app_id,created_at,created_by,created_ip,updated_at,updated_by,updated_ip ");
			strSql.Append(" FROM privileges ");
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
			strSql.Append("select count(1) FROM privileges ");
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
			strSql.Append(")AS Row, T.*  from privileges T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 根据菜单ID获取菜单权限的ID
        /// </summary>
        public int GetMenuPrivilegeIDByMenuID(int menuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id ");
            strSql.Append(" FROM privileges ");
            strSql.Append(" where privilegeitem_id=@privilegeitem_id and privilegetype_id=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@privilegeitem_id", SqlDbType.Int,4)
			};
            parameters[0].Value = menuID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            if (obj == null)
            {
                return 0;//不存在该菜单的权限
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
		
        #endregion  Method
    }
}
