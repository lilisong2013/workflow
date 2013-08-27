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
        private readonly Saron.WorkFlowService.DAL.v_stepsDAL m_v_stepsdal = new DAL.v_stepsDAL();

        public SecurityContext m_securityContext = new SecurityContext();

        #region Method
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条流程步骤记录(order_no、repeat_count自动赋值)，<h4>（需要授权验证，系统管理员用户）</h4>")]
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

            //判断一下是否存在并序的步骤
            if (m_stepsdal.ExistStpeType(Convert.ToInt32(stepmodel.flow_id)) == 0)//不存在并序
            {
                //设置repeat_count,order_no

                //将要添加的是顺序(顺序->顺序)
                if (stepmodel.step_type_id == 1)
                {
                    stepmodel.repeat_count = 0;
                    stepmodel.order_no = Convert.ToInt32(m_stepsdal.GetFlowMaxOrderNum((int)stepmodel.flow_id))+1;
                    #region 添加流程步骤
                    int m_stepID = 0;
                    //添加
                    try
                    {
                        m_stepID = m_stepsdal.AddStep(stepmodel);
                    }
                    catch (Exception ex)
                    {
                        msg = ex.ToString();
                        return false;
                    }

                    if (m_stepID == 0)
                    {
                        msg = "流程添加失败";
                        return false;
                    }
                    #endregion

                    Saron.WorkFlowService.Model.flow_usersModel m_flow_usersModel = new flow_usersModel();
                    m_flow_usersModel.step_id = m_stepID;
                    m_flow_usersModel.user_id = userID;

                    #region 添加步骤用户
                    bool flag = false;
                    try
                    {
                        flag = m_flow_usersdal.AddFlow_User(m_flow_usersModel);
                    }
                    catch (Exception ex)
                    {
                        //步骤为添加对应的用户，将已添加的步骤删除
                        m_stepsdal.DeleteStep(m_stepID);
                        msg = "webservice异常";
                        return false;
                    }

                    if (flag)
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
                    #endregion

                }
                //将要添加的是并序(顺序->并序)
                else
                {
                    stepmodel.repeat_count = 1;
                    stepmodel.order_no = Convert.ToInt32(m_stepsdal.GetFlowMaxOrderNum((int)stepmodel.flow_id)) + 1;
                    #region 添加流程步骤
                    int m_stepID = 0;
                    //添加
                    try
                    {
                        m_stepID = m_stepsdal.AddStep(stepmodel);
                    }
                    catch (Exception ex)
                    {
                        msg = ex.ToString();
                        return false;
                    }

                    if (m_stepID == 0)
                    {
                        msg = "流程添加失败";
                        return false;
                    }
                    #endregion

                    Saron.WorkFlowService.Model.flow_usersModel m_flow_usersModel = new flow_usersModel();
                    m_flow_usersModel.step_id = m_stepID;
                    m_flow_usersModel.user_id = userID;

                    #region 添加步骤用户
                    bool flag = false;
                    try
                    {
                        flag = m_flow_usersdal.AddFlow_User(m_flow_usersModel);
                    }
                    catch (Exception ex)
                    {
                        //步骤为添加对应的用户，将已添加的步骤删除
                        m_stepsdal.DeleteStep(m_stepID);
                        msg = "webservice异常";
                        return false;
                    }

                    if (flag)
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
                    #endregion

                }
            }
            else
            { //存在并序

                //将要添加的是并序(并序->并序)
                
                stepmodel.repeat_count = Convert.ToInt32(m_stepsdal.GetFlowMaxNumOfUnorder(Convert.ToInt32(stepmodel.flow_id)))+1;
                stepmodel.order_no = Convert.ToInt32(m_stepsdal.GetFlowMaxOrderNum((int)stepmodel.flow_id))+1;

                #region 添加流程步骤
                int m_stepID = 0;
                //添加
                try
                {
                    m_stepID = m_stepsdal.AddStep(stepmodel);
                }
                catch (Exception ex)
                {
                    msg = ex.ToString();
                    return false;
                }

                if (m_stepID == 0)
                {
                    msg = "流程添加失败";
                    return false;
                }
                #endregion

                Saron.WorkFlowService.Model.flow_usersModel m_flow_usersModel = new flow_usersModel();
                m_flow_usersModel.step_id = m_stepID;
                m_flow_usersModel.user_id = userID;

                #region 添加步骤用户
                bool flag = false;
                try
                {
                    flag = m_flow_usersdal.AddFlow_User(m_flow_usersModel);
                }
                catch (Exception ex)
                {
                    //步骤为添加对应的用户，将已添加的步骤删除
                    m_stepsdal.DeleteStep(m_stepID);
                    msg = "webservice异常";
                    return false;
                }

                if (flag)
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
                #endregion

            }

        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "增加一条流程步骤记录，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public bool AddStepAndAllInfo(Saron.WorkFlowService.Model.stepsModel stepmodel, int userID, out string msg)
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
            int m_stepID = 0;
            //添加
            try
            {
                m_stepID = m_stepsdal.AddStep(stepmodel);
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return false;
            }

            if (m_stepID == 0)
            {
                msg = "流程添加失败";
                return false;
            }
            #endregion

            Saron.WorkFlowService.Model.flow_usersModel m_flow_usersModel = new flow_usersModel();
            m_flow_usersModel.step_id = m_stepID;
            m_flow_usersModel.user_id = userID;

            #region 添加步骤用户
            bool flag = false;
            try
            {
                flag = m_flow_usersdal.AddFlow_User(m_flow_usersModel);
            }
            catch (Exception ex)
            {
                //步骤为添加对应的用户，将已添加的步骤删除
                m_stepsdal.DeleteStep(m_stepID);
                msg = "webservice异常";
                return false;
            }

            if (flag)
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
            #endregion

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

        [WebMethod(Description = "获得流程中的最大排序码(-1：webservice未授权，0：流程中不存在步骤)，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public DataSet GetFlowMaxNum(int flowID)
        {
            return m_stepsdal.GetFlowMaxOrderNumOfUnorder(flowID);
        }

        [WebMethod(Description = "判断某流程下是否存在并行的列表以及如果存在，返回存在的个数(-1：webservice未授权，0：流程中不存在步骤)，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public int ExistStpeType(int flowID)
        {
            return m_stepsdal.ExistStpeType(flowID);
        }

        /// <summary>
        /// 作者：朱建刚
        /// </summary>
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得流程的v_steps视图步骤列表，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public DataSet GetFlowStepListByFlowID(int flowID, out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion

            return m_v_stepsdal.GetFlowStepListByFlowID(flowID);
        }

        /// <summary>
        /// 作者：朱建刚
        /// 时间：2013-8-27  11：51
        /// </summary>
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得流程的v_steps视图步骤排序码列表，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public DataSet GetFlowStepOrder_noListByFlowID(int flowID, out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion

            return m_v_stepsdal.GetFlowStepOrder_noListByFlowID(flowID);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得某系统下流程的步骤列表，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public DataSet GetFlowStepListByAppID(int appID,out string msg)
        {
             #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion
               
            return m_v_stepsdal.GetFlowStepListByAppID(appID);
        }
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "获得一个v_stepModel的对象实体，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public Saron.WorkFlowService.Model.v_stepsModel GetV_StepsModel(int stepID, out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion

            return m_v_stepsdal.GetV_StepsModel(stepID);

        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "通过ID获得一个stepsModel的对象实体，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public Saron.WorkFlowService.Model.stepsModel GetModelByID(int id,out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion

            return m_stepsdal.GetModel(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "删除步骤，<h4>（需要授权验证，系统管理员用户）</h4>")]
        public bool DeleteStep(int stepID, out string msg)
        {
           
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return false;
            }
            #endregion
           
            bool flag = false;
            flag = m_flow_usersdal.DeleteFlow_User(stepID);
           
            if (flag)
            {
                try
                {
                    if (m_stepsdal.DeleteStep(stepID))
                    {
                        msg = "删除流程步骤成功!";
                        return true;
                    }
                    else
                    {
                        msg = "删除失败!";
                        return false;
                    }
                }
                catch (Exception ex) {
                    msg = ex.ToString();
                    return false;
                }
               
            }
            else
            {
                msg = "删除失败!";
                return false;
            }
        }

        [WebMethod(Description = "判断是否存在用户id为user_id,步骤id为step_id的记录")]
        public bool ExistsFlowUser(int user_id, int step_id)
        {
            return m_flow_usersdal.ExistsFlowUser(user_id,step_id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据ID获取step列表,<h4>（需要授权验证，系统管理员用户）</h4>")]
        public DataSet GetStepListByID(int id,out string msg)
        {
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion

            return m_stepsdal.GetStepListByID(id);
        }

        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "根据flowID获取step列表,<h4>（需要授权验证，系统管理员用户）</h4>")]
        public DataSet GetStepListOfFlowID(int flowID,out string msg)
        {  
            #region webservice授权判断
            //是否有权限访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                return null;
            }
            #endregion

            return m_stepsdal.GetStepListOfFlowID(flowID);
        }
        
        [SoapHeader("m_securityContext")]
        [WebMethod(Description = "更新一条数据,<h4>（需要授权验证，系统管理员用户）</h4>")]
        public bool Update(Saron.WorkFlowService.Model.stepsModel model,out string msg)
        {  
            //对webservice进行授权验证,系统管理员才可访问
            if (!m_securityContext.AdminIsValid(m_securityContext.UserName, m_securityContext.PassWord, out msg))
            {
                //webservice用户未授权，msg提示信息
                return false;
            }
           return m_stepsdal.Update(model);
        }

       #endregion 
  }

}
