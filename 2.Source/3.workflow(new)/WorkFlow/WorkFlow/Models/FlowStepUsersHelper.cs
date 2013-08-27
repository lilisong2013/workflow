using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class FlowStepUsersHelper
    {
        private int stepuserID;
        private string stepuserName;

        public int StepuserID
        {
            get { return stepuserID; }
            set { stepuserID = value; }
        }      
        public string StepuserName
        {
            get { return stepuserName; }
            set { stepuserName = value; }
        }

    }
}