using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKReportingSystem.Lookup.Preview
{
    public partial class EmailNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string notifcode = Request.QueryString["nc"];

        }
    }
}