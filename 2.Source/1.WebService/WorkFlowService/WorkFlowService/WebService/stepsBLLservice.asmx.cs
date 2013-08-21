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
    /// stepsBLLservice 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://saron.workflowservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class stepsBLLservice : System.Web.Services.WebService
    {
        private readonly Saron.WorkFlowService.DAL.stepsDAL m_stepsdal = new DAL.stepsDAL();
        private readonly Saron.WorkFlowService.DAL.flowsDAL m_flowsdal = new DAL.flowsDAL();
        private readonly Saron.WorkFlowService.DAL.flow_usersDAL m_flow_usersdal = new DAL.flow_usersDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region Method
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条流程步骤记录，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public bool AddStep(Saron.WorkFlowService.Model.stepsModel stepmodel,int userID,out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }
            #endregion

            #region 判断流程步骤是否重名
            if (m_stepsdal.ExistStepName(stepmodel.name, (int)stepmodel.flow_id))
            {
                msg = "流程：" + m_flowsdal.GetFlowName((int)stepmodel.flow_id) + ",已经存在该步骤名称";
                return false;
            }
            #endregion


            //设定步骤的重复次数
            stepmodel.repeat_count = m_stepsdal.GetRepeatCount(stepmodel);


            #region 添加流程步骤
            //添加
            int m_stepID = m_stepsdal.AddStep(stepmodel);

            if (m_stepID == 0)
            {
                msg = "流程添加失败";
                return false;
            }
            #endregion

            Saron.WorkFlowService.Model.flow_usersModel m_flow_usersModel = new flow_usersModel();
            m_flow_usersModel.step_id = m_stepID;
            m_flow_usersModel.user_id = userID;

            if (m_flow_usersdal.AddFlow_User(m_flow_usersModel))
            {
                return true;
            }
            else
            {
                //步骤为添加对应的用户，将已添加的步骤删除
                m_stepsdal.DeleteStep(m_stepID);
                msg = "流程添加失败";
                return false;
            }


        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得流程中的最大排序码(-1：webservice未授权，0：流程中不存在步骤)，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public int GetFlowMaxOrderNum(int flowID, out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return -1;
            }
            #endregion

            return m_stepsdal.GetFlowMaxOrderNum(flowID);
        }

       
        #endregion

    }
}
