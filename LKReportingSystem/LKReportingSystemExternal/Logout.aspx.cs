using LKReportingSystemExternal.Class;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKReportingSystemExternal
{
    public partial class Logout : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Logout));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                log.DebugFormat("Page_Load Logout, userName: {0}", Constants.sessionUsername);
                Session.Clear();
                Constants.sessionUsername = "";
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Page_Load Logout, Error: {0}", ex.ToString());
            }

            Response.Redirect(Page.ResolveClientUrl("~/Login.aspx"));
        }
    }
}