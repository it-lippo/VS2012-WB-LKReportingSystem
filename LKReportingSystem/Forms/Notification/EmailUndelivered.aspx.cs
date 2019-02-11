using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;

namespace LKReportingSystem.Forms.Notification
{
    public partial class EmailUndelivered : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsSecurity.HaveAccess(this.AppRelativeVirtualPath))
            {
                htmlNotificationHistory.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To View this Page";

                htmlNotificationHistory.Attributes.Add("class", "alert alert-danger alert-dismissable");
                updatePanelHtmlNotificationHistory.Update();

                divFilter.Visible = false;
                return;
            }
            else
            {
                if (!IsPostBack)
                {
                    BindDataNotifType();
                }
            }
        }

        private void BindDataNotifType()
        {
            try
            {
                DataTable dt = clsMaster.GetDataNotifType();
                
                string query = "parentType = 'EM1'";
                dt.DefaultView.RowFilter = query;


                lbNotifType.DataSource = dt;
                lbNotifType.DataTextField = "notiftypedesc";
                lbNotifType.DataValueField = "notiftypecode";
                lbNotifType.DataBind();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            { 
                DateTime startPeriod = DateTime.ParseExact(txtStartDate1.Text, "dd/MM/yyyy", null);
                DateTime endPeriod = DateTime.ParseExact(txtEndDate1.Text, "dd/MM/yyyy", null);

                string notifType = "";
                foreach (ListItem item in lbNotifType.Items)
                {
                    if (item.Selected)
                    {
                        notifType += item.Value.ToString() + ",";
                    }
                }

                if (string.IsNullOrEmpty(notifType))
                {
                    htmlNotificationHistory.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> Notification Type is required.";

                    htmlNotificationHistory.Attributes.Add("class", "alert alert-danger alert-dismissable");
                    updatePanelHtmlNotificationHistory.Update();

                    return;
                }

                DataTable dt = clsMailPromo.GetDataUndeliveredEmail(notifType, startPeriod, endPeriod);

                Session["Rpt_EmailUndeliv"] = dt;

                gvReportUndeliv.DataSource = dt;
                gvReportUndeliv.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true); 
            }
        }

        protected void gvReportUndeliv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string notifcode = e.CommandArgument.ToString();

            DataTable dt = (DataTable)Session["Rpt_EmailUndeliv"];

            if (e.CommandName == "ContentEmail")
            {
                string query = string.Format("notifcode = {0}", notifcode);
                dt.DefaultView.RowFilter = query;

                string content = dt.DefaultView[0]["message"].ToString();

                ltContentEmail.Text = content;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "openModalEmail();", true);
            }
            else if (e.CommandName == "ResultEmail")
            {
                string query = string.Format("notifcode = {0}", notifcode);
                dt.DefaultView.RowFilter = query;

                string content = dt.DefaultView[0]["result"].ToString();

                ltContentEmail.Text = content;

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", "openModalEmail();", true);
            }
        }
    }
}