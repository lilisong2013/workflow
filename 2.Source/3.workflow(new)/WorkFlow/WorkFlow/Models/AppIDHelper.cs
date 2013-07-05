using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class AppIDHelper
    {
        private int m_AppID;
        private string m_AppName;
        /// <summary>
        /// 系统ID
        /// </summary>
        public int AppID
        {
            get { return m_AppID; }
            set { m_AppID = value; }
        }
        /// <summary>
        /// 系统ID名称
        /// </summary>
        public string AppName
        {
            get { return m_AppName; }
            set { m_AppName = value; }
        }

    }
}