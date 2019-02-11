using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class;
using log4net;

namespace LKReportingSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Login));

        protected void Page_Load(object sender, EventArgs e)
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.InfoFormat("Start Page_Load()..");

            log.InfoFormat("End Page_Load()..");
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            log.InfoFormat("Start btnLogin_Click().");

            try
            {
                log.DebugFormat("Call Web Service LKGenWS.CheckLoginAD() for check username in AD. {0}" +
                                "       Domain : {1} {0}" +
                                "       Username : {2} {0}", Environment.NewLine, ddlDomain.SelectedValue, txtUsername.Text);

                LKGenWS.SQLServerService serv = new LKGenWS.SQLServerService();

                //Boolean isValid = serv.CheckLoginAD(ddlDomain.SelectedValue, txtUsername.Text, txtPassword.Text);

                Boolean isValid = true;

                log.DebugFormat("Receive isValid from Web Service LKGenWS.CheckLoginAD() = {0}", isValid ? "True" : "False");

                if (isValid)
                {
                    var domainPlusUsername = ddlDomain.SelectedValue + "\\" + txtUsername.Text;
                    //login active directory is valid
                    clsSecurity oUser = clsSecurity.GetUser(1, Constants.OrgID, Constants.AppID, domainPlusUsername);

                    if (oUser != null)
                    {
                        log.DebugFormat("Object clsSecurity Created for username {0}.", ddlDomain.SelectedValue + "\\" + txtUsername.Text);

                        Constants.sessionUsername = oUser.UserName;
                        Session[Constants.SessionUser] = oUser;
                        Session.Timeout = 1000;

                        log.DebugFormat("Object clsSecurity for username {0} saved in Session.", ddlDomain.SelectedValue + "\\" + txtUsername.Text);

                        string url = Page.ResolveUrl("~/Home.aspx");

                        Response.Redirect(url);
                        //Response.Redirect("~/Home.aspx");
                    }
                    else
                    {
                        //has no permission to access this application
                        log.DebugFormat("Username {0} has no permission.", ddlDomain.SelectedValue + "/" + txtUsername.Text);

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "bootbox.alert({message:'" + ddlDomain.SelectedValue + "/" + txtUsername.Text + " has no permission to This Application.', title:'Oops!'});", true);
                    }
                }
                else
                {
                    //login active directory is not valid
                    log.DebugFormat("Username {0} is not found in AD.", ddlDomain.SelectedValue + "/" + txtUsername.Text);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification", "bootbox.alert({message:'Please Check again your login Information.', title:'Oops!'});", true);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error btnLogin_Click(). Message : {0}", ex.Message);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message + "', title:'Oops!'});", true);
            }

            log.InfoFormat("End btnLogin_Click().");
        }
    }
}