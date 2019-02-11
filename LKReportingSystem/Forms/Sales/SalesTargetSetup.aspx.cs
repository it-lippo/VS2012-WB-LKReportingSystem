using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class.Forms;

namespace LKReportingSystem.Forms.Sales
{
    public partial class SalesTargetSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtyearperiod.Text = DateTime.Now.Year.ToString() ;
                BindDataTarget(Convert.ToInt32(txtyearperiod.Text));

                
                TextBox txtFProject = (TextBox)gvSetupTarget.HeaderRow.FindControl("txtFilterProject");
                txtFProject.Attributes.Add("autocomplete", "off");

                TextBox txtFCluster = (TextBox)gvSetupTarget.HeaderRow.FindControl("txtFilterCluster");
                txtFCluster.Attributes.Add("autocomplete", "off");
            }
        }

        private void FilterData()
        {
            try
            {
                TextBox txtFProject = (TextBox)gvSetupTarget.HeaderRow.FindControl("txtFilterProject");
                TextBox txtFCluster = (TextBox)gvSetupTarget.HeaderRow.FindControl("txtFilterCluster");

                DataTable dt = (DataTable)Session["Dt_SalesTarget"];


                String FilterQuery = "1 = 1 ";
                if (txtFProject.Text != "")
                    FilterQuery += string.Format("AND (projectcode LIKE '%{0}%' OR projectname like '%{0}%') ", txtFProject.Text);
                if (txtFCluster.Text != "")
                    FilterQuery += string.Format("AND (clustercode LIKE '%{0}%' OR clustername like '%{0}%') ", txtFCluster.Text);


                ViewState["txtFilterProject_SalesTarget"] = txtFProject.Text;
                ViewState["txtFilterCluster_SalesTarget"] = txtFCluster.Text;

                dt.DefaultView.RowFilter = FilterQuery;
                gvSetupTarget.DataSource = dt;
                gvSetupTarget.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        private void BindDataTarget(int yearPeriod)
        {
            try
            {
                DataTable dt = clsSalesSummary.GetDataTargetSales(yearPeriod);

                Session["Dt_SalesTarget"] = dt;

                gvSetupTarget.DataSource = dt;
                gvSetupTarget.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        public void txtFilterProject_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        public void txtFilterCluster_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        protected void gvSetupTarget_OnDataBound(object sender, EventArgs e)
        {

            TextBox txtFProject = (TextBox)gvSetupTarget.HeaderRow.FindControl("txtFilterProject");
            TextBox txtFCluster = (TextBox)gvSetupTarget.HeaderRow.FindControl("txtFilterCluster");

            txtFProject.Text = ViewState["txtFilterProject_SalesTarget"] != null ? ViewState["txtFilterProject_SalesTarget"].ToString() : "";
            txtFCluster.Text = ViewState["txtFilterCluster_SalesTarget"] != null ? ViewState["txtFilterCluster_SalesTarget"].ToString() : "";


            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableHeaderCell cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "";
            cell.CssClass = "centerAlign fontSize12";
            row.Controls.Add(cell);
            //TextBox txtFilterProject = new TextBox();
            //txtFilterProject.Text = ViewState["txtFilterProject_SalesTarget"] != null ? ViewState["txtFilterProject_SalesTarget"].ToString() : "";
            //txtFilterProject.CssClass = "form-control";
            //txtFilterProject.AutoPostBack = true;
            //txtFilterProject.TextChanged += new EventHandler(txtFilterProject_TextChanged);
            //cell.Controls.Add(txtFilterProject);
            //row.Controls.Add(cell);

            //cell = new TableHeaderCell();
            //TextBox txtFilterCluster = new TextBox();
            //txtFilterCluster.Text = ViewState["txtFilterCluster_SalesTarget"] != null ? ViewState["txtFilterCluster_SalesTarget"].ToString() : "";
            //txtFilterCluster.CssClass = "form-control";
            //txtFilterCluster.AutoPostBack = true;
            //txtFilterCluster.TextChanged += new EventHandler(txtFilterCluster_TextChanged);
            //cell.Controls.Add(txtFilterCluster);
            //row.Controls.Add(cell);

            for (int i = 1; i <= 12; i++)
            {
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.Text = month;
                cell.CssClass = "centerAlign fontSize12";
                row.Controls.Add(cell);
            }


            gvSetupTarget.HeaderRow.Parent.Controls.AddAt(0, row);
            
        }
    }
}