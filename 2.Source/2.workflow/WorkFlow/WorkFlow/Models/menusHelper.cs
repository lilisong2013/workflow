using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class menusHelper
    {
        private int m_menusID;//菜单ID
        private string m_menusName;
        private int m_parentID;
        private int m_menusLevel;

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int menusID
        {
            set { m_menusID = value; }
            get { return m_menusID; }
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menusName
        {
            set { m_menusName = value; }
            get { return m_menusName; }
        }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public int parentID
        {
            set { m_parentID = value; }
            get { return m_parentID; }
        }

        /// <summary>
        /// 菜单级别
        /// </summary>
        public int menusLevel
        {
            set { m_menusLevel = value; }
            get { return m_menusLevel; }
        }



    }
}