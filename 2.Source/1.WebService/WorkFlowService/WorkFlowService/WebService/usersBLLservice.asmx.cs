using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Saron.WorkFlowService.Model;
namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// usersBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class usersBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.usersDAL m_usersdal = new Saron.WorkFlowService.DAL.usersDAL();

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id，登录名为login的记录")]
        public bool Exists(int id, string login)
        {
            return m_usersdal.Exists(id, login);
        }

        /// <summary>
        /// （用户登录）是否存在用户或密码
        /// </summary>
        [WebMethod(Description = "是否存在用户名login且密码password的用户")]
        public bool LoginValidator(string login,string password)
        {
            bool flag = m_usersdal.Exists(login, password);
            if (flag)
            {
                Saron.WorkFlowService.Model.usersModel m_userModel = new usersModel();
                Saron.WorkFlowService.Model.appsModel m_appModel = new appsModel();
                Saron.WorkFlowService.DAL.appsDAL m_appDal=new DAL.appsDAL();
                m_userModel = GetModelByLogin(login);
                m_appModel = m_appDal.GetModel((int)m_userModel.app_id);
                if (m_appModel.invalid)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
      
        /// <summary>
        /// 是否存在该用户
        /// </summary>
        [WebMethod(Description = "系统ID为appId是否存在登录名为login的记录")]
        public bool ExistsLoginAndAppID(string login,int? appId)
        {
            return m_usersdal.ExistsLogin(login,appId);
        }

        /// <summary>
        /// 是否存在该用户
        /// </summary>
        [WebMethod(Description = "是否存在登录名为login的记录")]
        public bool ExistsLogin(string login)
        {
            return m_usersdal.ExistsLogin(login);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.usersModel model)
        {
            if (!ExistsLogin(model.login))
            {
                return m_usersdal.Add(model);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.usersModel model)
        {
            return m_usersdal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id)
        {
            return m_usersdal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        [WebMethod(Description = "删除id为id，登录名为login的记录")]
        public bool DeleteIdAndLogin(int id, string login)
        {
            return m_usersdal.Delete(id, login);
        }
        
        /// <summary>
        /// 删除多条数据
        /// </summary>
        [WebMethod(Description = "删除多条数据")]
        public bool DeleteList(string idlist)
        {
            return m_usersdal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByID(int id)
        {
            return m_usersdal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据登录名login得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByLogin(string login)
        {
            return m_usersdal.GetModel(login);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据系统ID得到一个实体对象")]
        public Saron.WorkFlowService.Model.usersModel GetModelByAppID(int appID)
        {
            return m_usersdal.GetModelByAppID(appID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetUsersList(string strWhere)
        {
            return m_usersdal.GetUsersList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        [WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        public DataSet GetUsersTopList(int Top, string strWhere, string filedOrder)
        {
            return m_usersdal.GetUsersList(Top, strWhere, filedOrder);
        }
        

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllUsersList()
        {
            return GetUsersList("");
        }

        /// <summary>
        /// 获得某系统的数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllUsersListOfApp(int appID)
        {
            return m_usersdal.GetAllUsersListOfApp(appID);
        }
        /// <summary>
        /// 获得记录总数
        /// </summary>
        [WebMethod(Description = "获得记录总条数")]
        public int GetRecordCount(string strWhere)
        {
            return m_usersdal.GetRecordCount(strWhere);
        }
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        [WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return m_usersdal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  Method
    }
}
