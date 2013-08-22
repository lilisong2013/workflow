using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saron.WorkFlow.Models
{
    public class FlowStepTypeHelper
    {
        private int flowsteptypeid;
        private string flowsteptypename;

        public int Flowsteptypeid
        {
            get { return flowsteptypeid; }
            set { flowsteptypeid = value; }
        }
        public string Flowsteptypename
        {
            get { return flowsteptypename; }
            set { flowsteptypename = value; }
        }

    }
}