﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Saron.WorkFlowService.Model;

namespace Saron.WorkFlowService.WebService
{
    /// <summary>
    /// init_statusBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class init_statusBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.init_statusDAL m_init_statusDal = new Saron.WorkFlowService.DAL.init_statusDAL();

        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        [WebMethod(Description = "获取最大ID号")]
        public int GetMaxId()
        {
            return m_init_statusDal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [WebMethod(Description = "是否存在id为id的记录")]
        public bool Exists(int id)
        {
            return m_init_statusDal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        [WebMethod(Description = "增加一条记录")]
        public bool Add(Saron.WorkFlowService.Model.init_statusModel model)
        {
            return m_init_statusDal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        [WebMethod(Description = "更新一条记录")]
        public bool Update(Saron.WorkFlowService.Model.init_statusModel model)
        {
            return m_init_statusDal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [WebMethod(Description = "根据主键id得到一个实体对象")]
        public Saron.WorkFlowService.Model.init_statusModel GetModel(int id)
        {
            return m_init_statusDal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "根据where条件获得数据列表：strWhere（where条件）")]
        public DataSet GetInit_StatusList(string strWhere)
        {
            return m_init_statusDal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        [WebMethod(Description = "获得所有数据列表")]
        public DataSet GetAllInit_StatusList()
        {
            return GetInit_StatusList("");
        }

        #endregion  Method
    }
}
