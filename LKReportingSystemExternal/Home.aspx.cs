using LKReportingSystemExternal.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKReportingSystemExternal
{ 
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Constants.sessionUsername == "")
            {
                Response.Redirect(Page.ResolveClientUrl("~/Login.aspx"));
            }
        }
    }
}