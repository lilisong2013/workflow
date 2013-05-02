using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlowService
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Label1.Text = Saron.Common.ConfigHelper.WebGetConnection("DataBaseName");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionStr = "server=" + this.TextBox1.Text.Trim() + ";user id=" + this.TextBox2.Text.Trim() + 
                ";pwd=" + this.TextBox3.Text.Trim() + ";database=" + this.TextBox4.Text.Trim();
            Saron.Common.ConfigHelper.WebSetConnection("DataBaseName", connectionStr);
        }
    }
}
