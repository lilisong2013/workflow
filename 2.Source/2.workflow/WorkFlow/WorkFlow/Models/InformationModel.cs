using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class InformationModel
    {
        public InformationModel()
        { }

        private bool m_success;//是否成功
        private string m_css;//调用css名称
        private string m_message;//返回的信息

        public bool success
        {
            set { m_success = value; }
            get { return m_success; }
        }

        public string css
        {
            set { m_css = value; }
            get { return m_css; }
        }

        public string message
        {
            set { m_message = value; }
            get { return m_message; }
        }
    }
}