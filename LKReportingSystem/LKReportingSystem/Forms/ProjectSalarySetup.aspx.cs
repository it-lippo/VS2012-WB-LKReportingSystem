using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;
using log4net;
using System.Globalization;


namespace LKReportingSystem.Forms
{
    public partial class ProjectSalarySetup : System.Web.UI.Page
    {

        protected static readonly ILog log = LogManager.GetLogger(typeof(ProjectSalarySetup));


        private DataTable _DT_ProjectSalary;
        private DataTable _DTFinal_ProjectSalary;

        private void RetouchGridView()
        {
            if (gvSalary.Rows.Count > 0)
            {
                //Attribute to hide column in Phone.
                gvSalary.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                gvSalary.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                gvSalary.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvSalary.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        private void initDTFinal_ProjectSalary()
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_ProjectSalary - Started");

            _DTFinal_ProjectSalary = new DataTable();

            _DTFinal_ProjectSalary.Columns.Add("ProjectCode", System.Type.GetType("System.String"));
            _DTFinal_ProjectSalary.Columns.Add("SalaryYear", System.Type.GetType("System.Int"));
            _DTFinal_ProjectSalary.Columns.Add("SalaryMonth", System.Type.GetType("System.String"));
            _DTFinal_ProjectSalary.Columns.Add("SalaryValue", System.Type.GetType("System.Decimal"));
            _DTFinal_ProjectSalary.Columns.Add("ModifTime", System.Type.GetType("System.DateTime"));
            _DTFinal_ProjectSalary.Columns.Add("ModifUN", System.Type.GetType("System.String"));
            _DTFinal_ProjectSalary.Columns.Add("InputTime", System.Type.GetType("System.DateTime"));
            _DTFinal_ProjectSalary.Columns.Add("InputUN", System.Type.GetType("System.String"));

            log.DebugFormat("initDTFinal_ProjectSalary - Finished");

        }

        private void BindDataProjectSalary(string projectcode)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("BindDataProjectSalary() Called.. Parameter: projectcode={0}", projectcode);

                _DT_ProjectSalary = new DataTable();

                _DT_ProjectSalary = clsProjectSalarySetup.GetDataDetailProjecSalary(projectcode);

                gvSalary.DataSource = _DT_ProjectSalary;
                gvSalary.DataBind();

                RetouchGridView();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string projectcode = Request.QueryString["ProjectCode"];
            
                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("UpdateProjectInformation - Page_Load() Called.. Is Not PostBack. Parameter Received: projectcode={0}", projectcode);

                LblCurrentMonth.Text = "Salary for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month) + " " + DateTime.Today.Year.ToString();

                BindDataProjectSalary(projectcode);
            }
            string projectcode1 = Request.QueryString["ProjectCode"];

            LblCurrentMonth.Text = "Salary for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month) + " " + DateTime.Today.Year.ToString();
            BindDataProjectSalary(projectcode1);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (!clsSecurity.HaveAccessAction(this.AppRelativeVirtualPath, "edit"))
            {
                htmlNotificationSalary.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To Update Project Expense";

                htmlNotificationSalary.Attributes.Add("class", "alert alert-danger alert-dismissable");
                updatePanelHtmlNotificationSalary.Update();
                return;
            }

            try
            {
                Boolean zeroChecker = false;

                string ProjectCode = Request.QueryString["ProjectCode"];
                int SalaryYear = DateTime.Today.Year;
                int SalaryMonth = DateTime.Today.Month;
                decimal SalaryValue = decimal.Parse(txtSalaryValue.Text);
                if (SalaryValue == 0)
                    zeroChecker = true;

                if (zeroChecker) {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({title:'Oops!', message:'Following Field Cannot Be Zero or Empty: <ul> <li>Salary Value</li> <ul>'});", true);
                    return;
                }

                string result;
                result = clsProjectSalarySetup.UpdateProjectSalary(ProjectCode, SalaryYear, SalaryMonth, SalaryValue, Constants.sessionUsername);

                if (result == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "inf_msg", "bootbox.alert({message:'Update project salary succeed.', title:'Oops!'});", true);
                    
                    BindDataProjectSalary(ProjectCode);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + result.Replace("'", "\\'") + "', title:'Oops!'});", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //string link = Page.ResolveUrl("~/Forms/SalesSummary.aspx?isCancel=1");
            string link = Page.ResolveUrl("~/Forms/ProjectManagement.aspx?isCancel=1");
            Response.Redirect(link);
        }


    }
}