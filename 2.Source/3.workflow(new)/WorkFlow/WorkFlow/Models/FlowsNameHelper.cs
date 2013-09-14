using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class FlowsNameHelper
    {
        private int flowsID;
        private string flowsName;
        
        /// <summary>
        /// 流程ID
        /// </summary>
        public int FlowsID
        {
            get { return flowsID; }
            set { flowsID = value; }
        }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowsName
        {
            get { return flowsName; }
            set { flowsName = value; }
        }

    }
}