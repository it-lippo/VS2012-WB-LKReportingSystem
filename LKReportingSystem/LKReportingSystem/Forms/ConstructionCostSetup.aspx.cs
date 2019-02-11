using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;

namespace LKReportingSystem.Forms
{
    public partial class ConstructionCostSetup : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(UpdateProjectInformation));

        private DataTable _DT_ConstructionCost;
        private DataTable _DTFinal_ConstructionCost;

        private void initDTFinal_ConstructionCost()
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_ConstructionCost - Started");

            _DTFinal_ConstructionCost = new DataTable();

            _DTFinal_ConstructionCost.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
            _DTFinal_ConstructionCost.Columns.Add("ClusterCode", System.Type.GetType("System.String"));
            _DTFinal_ConstructionCost.Columns.Add("InitBudgetValueConstCostPerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_ConstructionCost.Columns.Add("InitBudgetValueTotal", System.Type.GetType("System.Decimal"));
            _DTFinal_ConstructionCost.Columns.Add("ProjectedValueTotalTillCompletion", System.Type.GetType("System.Decimal"));
            _DTFinal_ConstructionCost.Columns.Add("ProjectedValuePerMSquareAreaTillCompletion", System.Type.GetType("System.Decimal"));

            log.DebugFormat("initDTFinal_ConstructionCost - Finished");

        }

        private void RetouchGridView()
        {

            if (gvConstructionCost.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                gvConstructionCost.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvConstructionCost.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvConstructionCost.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvConstructionCost.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvConstructionCost.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvConstructionCost.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int BatchID = int.Parse(Request.QueryString["batchid"]);
                ViewState["BatchID"] = BatchID;

                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("ConstructionCostSetup - Page_Load() Called.. Is Not PostBack. Parameter Received: BatchID={0}", BatchID);

                BindDataConstructionCost(BatchID);
            }

            RetouchGridView();


        }

        protected void gvConstructionCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox TbInitBudgetValueConstCostPerMSquare = (TextBox)e.Row.FindControl("TbInitBudgetValueConstCostPerMSquare");
                TextBox TbInitBudgetValueTotal = (TextBox)e.Row.FindControl("TbInitBudgetValueTotal");

                TbInitBudgetValueConstCostPerMSquare.Attributes.Add("readonly", "readonly");
                TbInitBudgetValueTotal.Attributes.Add("readonly", "readonly");
            }
        }



        private void BindDataConstructionCost(int batchid)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("BindDataConstructionCost() Called.. Parameter: batchid={0}", batchid);

                _DT_ConstructionCost = new DataTable();

                _DT_ConstructionCost = clsConstructionCost.GetDataConstructionCost(batchid);

                gvConstructionCost.DataSource = _DT_ConstructionCost;
                gvConstructionCost.DataBind();

                RetouchGridView();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (!clsSecurity.HaveAccessAction(this.AppRelativeVirtualPath, "edit"))
            {
                htmlNotificationConstruction.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To Update Construction Cost";

                htmlNotificationConstruction.Attributes.Add("class", "alert alert-danger alert-dismissable");
                updatePanelHtmlNotificationConstruction.Update();
                return;
            }

            try
            {

                initDTFinal_ConstructionCost();

                int BatchID = int.Parse(ViewState["BatchID"].ToString());
                Boolean zeroChecker = false;
                foreach (GridViewRow gr in gvConstructionCost.Rows)
                {
                    string clusterCode = gvConstructionCost.Rows[gr.RowIndex].Cells[0].Text;

                    TextBox TbInitBudgetValueTotal = (TextBox)gvConstructionCost.Rows[gr.RowIndex].Cells[2].FindControl("TbInitBudgetValueTotal");
                    decimal InitBudgetValueTotal = decimal.Parse(TbInitBudgetValueTotal.Text.ToString());

                    TextBox TbInitBudgetValueConstCostPerMSquare = (TextBox)gvConstructionCost.Rows[gr.RowIndex].Cells[3].FindControl("TbInitBudgetValueConstCostPerMSquare");
                    decimal InitBudgetValueConstCostPerMSquare = decimal.Parse(TbInitBudgetValueConstCostPerMSquare.Text.ToString());

                    TextBox TbProjectedValueTotalTillCompletion = (TextBox)gvConstructionCost.Rows[gr.RowIndex].Cells[4].FindControl("TbProjectedValueTotalTillCompletion");
                    decimal ProjectedValueTotalTillCompletion = decimal.Parse(TbProjectedValueTotalTillCompletion.Text.ToString());
                    if (ProjectedValueTotalTillCompletion == 0)
                        zeroChecker = true;

                    TextBox TbProjectedValuePerMSquareAreaTillCompletion = (TextBox)gvConstructionCost.Rows[gr.RowIndex].Cells[5].FindControl("TbProjectedValuePerMSquareAreaTillCompletion");
                    decimal ProjectedValuePerMSquareAreaTillCompletion = decimal.Parse(TbProjectedValuePerMSquareAreaTillCompletion.Text.ToString());
                    if (ProjectedValuePerMSquareAreaTillCompletion == 0)
                        zeroChecker = true;

                    _DTFinal_ConstructionCost.Rows.Add(BatchID, clusterCode, InitBudgetValueTotal, InitBudgetValueConstCostPerMSquare, ProjectedValueTotalTillCompletion, ProjectedValuePerMSquareAreaTillCompletion);
                    //_DTFinal_ConstructionCost.Rows.Add(BatchID, clusterCode, ProjectedValueTotalTillCompletion, ProjectedValuePerMSquareAreaTillCompletion);


                }

                if (zeroChecker)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({title:'Oops!', message:'Following Field Cannot Be Zero or Empty: <ul> <li>Projected Till Completion Rp</li> <li>Projected Till Completion Rp/M2</li><ul>'});", true);
                    return;
                }

                string result;
                result = clsConstructionCost.UpdateConstructionCost(_DTFinal_ConstructionCost, Constants.sessionUsername);

                if (result == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "inf_msg", "bootbox.alert({message:'Update Construction Cost succeed.', title:'Success!'});", true);
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