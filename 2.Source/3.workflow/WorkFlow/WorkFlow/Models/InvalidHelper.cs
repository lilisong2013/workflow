using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Saron.WorkFlow.Models
{
    public class InvalidHelper
    {
        private string m_invalidID;
        private string m_invalidName;
        public string InvalidID
        {
            get { return m_invalidID; }
            set { m_invalidID = value; }
        }

        public string InvalidName
        {
            get { return m_invalidName; }
            set { m_invalidName = value; }
        }


    }
}