using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class InitStatusIDHelper
    {
        private int m_initStatusID;
        private string m_initStatusName;
        /// <summary>
        /// 初始化状态ID
        /// </summary>
        public int InitStatusID
        {
            get { return m_initStatusID; }
            set { m_initStatusID = value; }
        }

        /// <summary>
        /// 初始化状态名称
        /// </summary>
        public string InitStatusName
        {
            get { return m_initStatusName; }
            set { m_initStatusName = value; }
        }

    }
}