using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Maticsoft.DBUtility
{
    /*
     * 存储过程调用助手类。
     * 主要包括两个重要的方法ExcuteForDataTable和ExcuteForDataSet,准备重载这两个方法，
     * 以适应不同情况下的存储过程的调用：
     * 1、返回结果集的存储过程；2、不返回结果集的存储过程；
     * 1、没有任何参数、2、只有输入参数、3、有输入有输出参数、4、有输入有输出有返回值。
     * 注意，在调用方法时，参数值列表中各元素的顺序必须与存储过程中输入参数的顺序相同，否则无法执行存储过程。
     * 
     * Edit By LJW at 2011.08.24
     * 
     *  调用示例一(返回结果集，有输入\输出参数，有返回值)：
     *  //实例化对象
     *   StoreProcedureHelper spHelper = new StoreProcedureHelper("ForTest", "Provider=SQLSql;Data Source=192.168.1.252;Initial Catalog=HeatMeterDB;uid=sa;pwd=123;");
     *  //给输入参数赋值，输出参数不赋值。参数的顺序必须与存储过程中参数顺序一致。 
     *   object[] Para = new object[2];
     *   Para[0] = "55";
     *   //定义存储过程返回值result
     *   string result;
     *   DataTable dt =spHelper.ExecuteForDataTable (out result,Para);
     *   //将结果集绑定到控件中
     *   dataGridView1.DataSource = dt.DefaultView;
     *   
     *   调用示例二：(没有返回结果集，没有任何参数)
     *   //实例化对象
     *   StoreProcedureHelper spHelper = new StoreProcedureHelper("ForTest", "Provider=SQLSql;Data Source=192.168.1.252;Initial Catalog=HeatMeterDB;uid=sa;pwd=123;");
     *   //t为返回的受影响行数,其中将ExecuteNoQuery()方法重载，可以通过传递不同的参数来执行不同类型的存储过程。
     *   int t=spHelper.ExecuteNoQuery();
     *   
     */
    public  class StoreProcedureHelper
    {
        // 存储过程名称。
        private string _name;
        // 数据库连接字符串。

        /// <summary>
        /// 在这里获得sql连接字符串
        /// </summary>
        /// <returns></returns>
        public static  string  GetConnectionStr()
        {
            //在这里将字符串固定，当程序release时，和DB中的字符串一起操作一下。
            string str = "";
            str = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["WaterResource"].ConnectionString;
            return str;
        }
        public static string _conStr ;

        /// <summary>
        ///      构造函数
        /// sprocName: 存储过程名称；
        /// conStr: 数据库连接字符串。
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="conStr"></param>
        public StoreProcedureHelper(string sprocName)
        {
            _conStr =GetConnectionStr();
            _name = sprocName;
        }


        /// <summary>
        ///执行存储过程，返回受影响的行数。带输入参数
        /// / paraValues: 参数值列表。
        ///  return: int 
        /// </summary>
        /// <param name="paraValues"></param>
        public int ExecuteNoQuery(params object[] paraValues)
        {
            int effectRows = -1;
            string commEffect = "";
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                try
                {
                    SqlCommand comm = new SqlCommand(_name, con);
                    comm.CommandType = CommandType.StoredProcedure;
                    AddInParaValues(comm, paraValues);
                    con.Open();
                    effectRows = comm.ExecuteNonQuery();
                    commEffect = comm.Parameters["@RETURN_VALUE"].Value.ToString();
                   effectRows = Convert.ToInt32(commEffect);
                }
                catch (Exception err)
                {
                    throw new Exception(err.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            return effectRows;

        }

        /// <summary>
        /// 执行存储过程，返回受影响行数.该过程没有输入参数。
        /// 返回受影响的行数
        /// </summary>
        /// <returns></returns>
        public int ExecuteNoQuery()
        {
            int effectRows = -1;
            SqlConnection oleConn = new SqlConnection(_conStr);
            try
            {
                if (oleConn.State != ConnectionState.Open)
                {
                    oleConn.Open();
                }
                SqlCommand comm = new SqlCommand(_name, oleConn);
                comm.CommandType = CommandType.StoredProcedure;
                effectRows = comm.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                oleConn.Close();
            }
            return effectRows;

        }

        ///// <summary>
        ///// 执行存储过程，输入参数， 返回returnValue
        ///// </summary>
        ///// <param name="returnValue"></param>
        ///// <param name="paraValues"></param>
        ///// <returns></returns>
        //public void  ExecuteNoQuery(out string returnValue,params object [] paraValues)
        //{
        //    SqlConnection oleConn = new SqlConnection(_conStr);
        //    try
        //    {
        //        if (oleConn.State != ConnectionState.Open)
        //        {
        //            oleConn.Open();
        //        }
        //        SqlCommand comm = new SqlCommand(_name, oleConn);
        //        comm.CommandType = CommandType.StoredProcedure;
        //        AddInParaValues(comm, paraValues);
        //         comm.ExecuteNonQuery();
        //         returnValue = comm.Parameters["@RETURN_VALUE"].Value.ToString();

        //    }
        //    catch (Exception err)
        //    {
        //        throw new Exception(err.Message);
        //    }
        //    finally
        //    {
        //        oleConn.Close();
        //    }
            
        //}
        /// <summary>
        ///执行存储过程返回数据表。该存储过程只有输入参数。
        /// paraValues: 参数值列表。
        /// return: DataTable
        /// </summary>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        public DataTable ExecuteForDataTable(params object[] paraValues)
        {
            SqlConnection sqlConn=new SqlConnection(_conStr);

            DataTable dt = new DataTable();
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }
                SqlCommand comm = new SqlCommand(_name, sqlConn);
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                AddInParaValues(comm, paraValues);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            return dt;
        }


        /// <summary>
        ///执行存储过程返回数据表。该存储过程只有输入参数。其中数据集是输入参数，可以人为累加结果集。
        /// paraValues: 参数值列表。
        /// return: DataTable
        /// </summary>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        public DataTable  ExecuteForDataTable(out string returnValue,DataTable dt, params object[] paraValues)
        {
           // string returnValue = string.Empty;
            SqlConnection sqlConn=new SqlConnection(_conStr);
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }
                SqlCommand comm = new SqlCommand(_name, sqlConn);
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                AddInParaValues(comm, paraValues);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);
                returnValue = comm.Parameters["@RETURN_VALUE"].Value.ToString();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 执行存储过程返回数据表，该存储过程有输入参数，并带有return返回值
        /// </summary>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        public DataTable ExecuteForDataTable(out string returnValue, params object[] paraValues)
        {
            DataTable dt = new DataTable();
            SqlConnection oleConn = new SqlConnection(_conStr);
            try
            {
                if (oleConn.State != ConnectionState.Open)
                {
                    oleConn.Open();
                }
                SqlCommand comm = new SqlCommand(_name, oleConn);
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                AddInParaValues(comm, paraValues);
                SqlDataAdapter oleda = new SqlDataAdapter();
                oleda.SelectCommand = comm;
                oleda.Fill(dt);
                returnValue = comm.Parameters["@RETURN_VALUE"].Value.ToString();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                oleConn.Close();
            }
            return dt;
        }


        /// <summary>
        ///执行存储过程返回一个DataSet。其中在某些情况下，根据实际情况,第一个table为结果集，第二个table为受影响的行数.
        /// paraValues: 参数值列表。
        /// return: DataSet
        /// </summary>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        public DataSet ExecuteForDataSet(params object[] paraValues)
        {
            SqlConnection sqlConn = new SqlConnection(_conStr);
            DataSet ds = new DataSet();
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }
                SqlCommand comm = new SqlCommand(_name, sqlConn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandTimeout = 0;
                AddInParaValues(comm, paraValues);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            return ds;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象，
        /// paraValues: 要传递给给存储过程的参数值列表。
        /// return: SqlDataReader
        /// </summary>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteDataReader(params object[] paraValues)
        {
            SqlConnection con = new SqlConnection(_conStr);
            // SqlDataReader oleDataReader;
            try
            {
                SqlCommand comm = new SqlCommand(_name, con);
                comm.CommandType = CommandType.StoredProcedure;
                AddInParaValues(comm, paraValues);
                con.Open();
                SqlDataReader oleDataReader = comm.ExecuteReader();
                return oleDataReader;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }

        /// <summary>
        ///  获取存储过程的参数列表。
        /// </summary>
        /// <returns></returns>
        private ArrayList GetParas()
        {
            SqlConnection sqlConn=new SqlConnection(_conStr);
            ArrayList al = new ArrayList();
            try
            {
                if (sqlConn .State !=ConnectionState .Open )
                {
                    sqlConn .Open ();
                }
                SqlCommand comm = new SqlCommand("dbo.sp_sproc_columns", sqlConn );
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@procedure_name", (object)_name);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    al.Add(dt.Rows[i][3].ToString());
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally 
            {
                sqlConn .Close ();
            }
            return al;
        }

        /// <summary>
        ///  为 SqlCommand 添加参数及赋值、或者指明参数类型为返回值、输出参数。
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="paraValues"></param>
        private void AddInParaValues(SqlCommand comm, params object[] paraValues)
        {
            try
            {
                //添加返回值参数
                comm.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int ));
                comm.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
                //添加输入输出参数，如果存在的话
                if (paraValues != null)
                {
                    ArrayList al = GetParas();
                    for (int i = 0; i < paraValues.Length; i++)
                    {
                        if (paraValues[i] != null)
                        {
                            //输入参数添加\赋值
                            comm.Parameters.AddWithValue(al[i + 1].ToString(), paraValues[i]);
                        }
                        else
                        {
                            //输出参数添加，并设置其类型
                            //在这里不是很清楚output的参数类型，所以全部置为varchar,可能会遇到问题
                            comm.Parameters.Add(al[i + 1].ToString(), SqlDbType.VarChar , 20);
                            comm.Parameters[al[i + 1].ToString()].Direction = ParameterDirection.Output;

                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }



    }
}
