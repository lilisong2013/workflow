using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Saron.WorkFlowService.Model;

namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// rolesBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class rolesBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.rolesDAL m_rolesDal = new Saron.WorkFlowService.DAL.rolesDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region  Method

        //[WebMethod(Description = "是否存在id为id的记录")]
        //public bool Exists(int id)
        //{
        //    return m_rolesDal.Exists(id);
        //}


        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条记录")]
        public int Add(Saron.WorkFlowService.Model.rolesModel model,out string msg)
        {
            int result = 0;
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                result = -1;
                //webservice用户未授权，msg提示信息
                return result;
            }

            result = m_rolesDal.Add(model);

            if (result == 0)
            {
                msg = "添加失败";
            }
            else
            {
                msg = "";
            }

            return result;
        }


        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.rolesModel model,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_rolesDal.Update(model);
        }


        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除id为id的记录")]
        public bool Delete(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }

            return m_rolesDal.Delete(id);
        }


        //[WebMethod(Description = "删除多条数据")]
        //public bool DeleteList(string idlist)
        //{
        //    return m_rolesDal.DeleteList(idlist);
        //}

        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.rolesModel GetModel(int id,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_rolesDal.GetModel(id);
        }

        
        //[SoapHeader("m_securityContext")]
        //[WebMethod(Description = "根据Deleted标志显示有效内容")]
        //public DataSet GetValidRolesList(out string msg)
        //{
        //    DataSet ds = new DataSet();
        //    //对webservice进行授权验证,系统管理员才可访问
        //    if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
        //    {
        //        ds = null;
        //        //webservice用户未授权，msg提示信息
        //        return ds;
        //    }
            
        //    ds=m_rolesDal.GetValidRolesList();

        //    return ds;
        //}

        
        //[SoapHeader("m_securityContext")]
        //[WebMethod(Description = "获得deleted=false的角色名称")]
        //public DataSet GetDeletedRoles(out string msg)
        //{
        //    DataSet ds = new DataSet();
        //    //对webservice进行授权验证,系统管理员才可访问
        //    if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
        //    {
        //        ds = null;
        //        //webservice用户未授权，msg提示信息
        //        return ds;
        //    }

        //    ds = m_rolesDal.DeletedRolesName();
        //    return ds;
        //}


        //[SoapHeader("m_securityContext")]
        //[WebMethod(Description = "获得deleted=false且rolename不等于name的角色名称")]
        //public DataSet GetDistinctRoles(string rolename,out string msg)
        //{
        //    DataSet ds = new DataSet();
        //    //对webservice进行授权验证,系统管理员才可访问
        //    if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
        //    {
        //        ds = null;
        //        //webservice用户未授权，msg提示信息
        //        return ds;
        //    }

        //    ds = m_rolesDal.DistinctRolesName(rolename);

        //    return ds;
        //}
       

        //[WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        //public DataSet GetRolesList(string strWhere)
        //{
        //    return m_rolesDal.GetRolesList(strWhere);
        //}
        

        //[WebMethod(Description = "获得前几行数据：top（前top行），strWhere（where条件），filedOrder（排序）")]
        //public DataSet GetRolesTopList(int Top, string strWhere, string filedOrder)
        //{
        //    return m_rolesDal.GetRolesList(Top, strWhere, filedOrder);
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //[WebMethod(Description = "获得所有数据列表")]
        //public DataSet GetAllRolesList()
        //{
        //    return GetRolesList("");
        //}


        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某应用系统的数据列表")]
        public DataSet GetAllRolesListOfApp(int appID,out string msg)
        {
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, m_securityContext.AppID, out msg))
            {
                //webservice用户未授权，msg提示信息
                return null;
            }

            return m_rolesDal.GetAllRolesListOfApp(appID);
        }
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //[WebMethod(Description = "获得记录总条数")]
        //public int GetRecordCount(string strWhere)
        //{
        //    return m_rolesDal.GetRecordCount(strWhere);
        //}
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //[WebMethod(Description = "分页获取数据列表：strWhere（where条件），orderby（排序方式），startIndex（开头索引），endIndex（结尾索引）")]
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return m_rolesDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        //}

        #endregion  Method
    }
}
