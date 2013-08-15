using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Saron.DBUtility;//数据库操作

namespace Saron.WorkFlowService.DAL
{
    /// <summary>
    /// 数据访问类:v_app_admin
    /// </summary>
    public partial class v_app_adminDAL
    {
        public v_app_adminDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.v_app_adminModel GetV_AppAdminModelByUserID(int userID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 App_id,App_Name,App_Code,URL,App_Mark,User_ID,User_Login,User_Password,User_Name,employee_no,mobile_phone,mail,User_Remark from v_app_admin ");
            strSql.Append(" where  User_ID=@User_ID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@User_ID", SqlDbType.Int,4)
            };

            parameters[0].Value = userID;

            Saron.WorkFlowService.Model.v_app_adminModel model = new Model.v_app_adminModel();
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
        /// 得到一个对象实体
        /// </summary>
        public Saron.WorkFlowService.Model.v_app_adminModel DataRowToModel(DataRow row)
        {
            Saron.WorkFlowService.Model.v_app_adminModel model = new Model.v_app_adminModel();
            if (row != null)
            {
                if (row["App_id"] != null && row["App_id"].ToString() != "")
                {
                    model.App_id = int.Parse(row["App_id"].ToString());
                }
                if (row["App_Name"] != null)
                {
                    model.App_Name = row["App_Name"].ToString();
                }
                if (row["App_Code"] != null)
                {
                    model.App_Code = row["App_Code"].ToString();
                }
                if (row["URL"] != null)
                {
                    model.URL = row["URL"].ToString();
                }
                if (row["App_Mark"] != null)
                {
                    model.App_Mark = row["App_Mark"].ToString();
                }
                if (row["User_ID"] != null && row["User_ID"].ToString() != "")
                {
                    model.User_ID = int.Parse(row["User_ID"].ToString());
                }
                if (row["User_Login"] != null)
                {
                    model.User_Login = row["User_Login"].ToString();
                }
                if (row["User_Password"] != null)
                {
                    model.User_Password = row["User_Password"].ToString();
                }
                if (row["User_Name"] != null)
                {
                    model.User_Name = row["User_Name"].ToString();
                }
                if (row["employee_no"] != null)
                {
                    model.employee_no = row["employee_no"].ToString();
                }
                if (row["mobile_phone"] != null)
                {
                    model.mobile_phone = row["mobile_phone"].ToString();
                }
                if (row["mail"] != null)
                {
                    model.mail = row["mail"].ToString();
                }
                if (row["User_Remark"] != null)
                {
                    model.User_Remark = row["User_Remark"].ToString();
                }
            }
            return model;
        }

        #endregion
    }
}