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
    public partial class UpdateProjectInformation : System.Web.UI.Page
    {

        protected static readonly ILog log = LogManager.GetLogger(typeof(UpdateProjectInformation));

        private DataTable _DT_ProjectInformation;
        private DataTable _DTFinal_ProjectInformation;
        private DataTable _DTFinal_ConstCost;

        private void initDTFinal_ProjectInformation()
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_ProjectInformation - Started");

            _DTFinal_ProjectInformation = new DataTable();

            _DTFinal_ProjectInformation.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
            _DTFinal_ProjectInformation.Columns.Add("ClusterCode", System.Type.GetType("System.String"));
            _DTFinal_ProjectInformation.Columns.Add("InitBudgetNoOfUnit", System.Type.GetType("System.Int32"));
            _DTFinal_ProjectInformation.Columns.Add("InitBudgetMSquare", System.Type.GetType("System.Double"));
            _DTFinal_ProjectInformation.Columns.Add("InitBudgetValuePerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_ProjectInformation.Columns.Add("InitBudgetValuePerUnit", System.Type.GetType("System.Decimal"));
            _DTFinal_ProjectInformation.Columns.Add("InitBudgetValueTotal", System.Type.GetType("System.Decimal"));
            _DTFinal_ProjectInformation.Columns.Add("InitPctSalesMktExpense", System.Type.GetType("System.Double"));

            log.DebugFormat("initDTFinal_ProjectInformation - Finished");

        }

        private void initDTFinal_ConstCost()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_ConstCost - Started");

            _DTFinal_ConstCost = new DataTable();

            _DTFinal_ConstCost.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
            _DTFinal_ConstCost.Columns.Add("ClusterCode", System.Type.GetType("System.String"));
            _DTFinal_ConstCost.Columns.Add("ConstCostOriginalValueTotal", System.Type.GetType("System.Decimal"));
            _DTFinal_ConstCost.Columns.Add("ConstCostPerMSquareArea", System.Type.GetType("System.Decimal"));
            _DTFinal_ConstCost.Columns.Add("ProjectedValueTotalTillCompletion", System.Type.GetType("System.Decimal"));
            _DTFinal_ConstCost.Columns.Add("ProjectedValuePerMSquareAreaTillCompletion", System.Type.GetType("System.Decimal"));

            log.DebugFormat("initDTFinal_ConstCost - Finished");

        }

        private void RetouchGridView()
        {
            if (gvDataProjectInformation.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                gvDataProjectInformation.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDataProjectInformation.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDataProjectInformation.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvDataProjectInformation.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvDataProjectInformation.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                gvDataProjectInformation.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDataProjectInformation.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (gvDataConstCost.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                gvDataConstCost.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDataConstCost.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvDataConstCost.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvDataConstCost.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvDataConstCost.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDataConstCost.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int BatchID = int.Parse(Request.QueryString["batchid"]);
                ViewState["BatchID"] = BatchID;

                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("UpdateProjectInformation - Page_Load() Called.. Is Not PostBack. Parameter Received: BatchID={0}", BatchID);

                BindDataProjectInformation(BatchID);
            }

            RetouchGridView();


        }

        protected void gvDataProjectInformation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void gvDataConstCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        private void BindDataProjectInformation(int batchid)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("BindDataProjectInformation() Called.. Parameter: batchid={0}", batchid);

                _DT_ProjectInformation = new DataTable();

                _DT_ProjectInformation = clsUpdateProjectInformation.GetDataDetailProjectInformation(batchid);

                gvDataProjectInformation.DataSource = _DT_ProjectInformation;
                gvDataProjectInformation.DataBind();

                gvDataConstCost.DataSource = _DT_ProjectInformation;
                gvDataConstCost.DataBind();

                RetouchGridView();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            try
            {

                initDTFinal_ProjectInformation();
                initDTFinal_ConstCost();

                int BatchID = int.Parse(ViewState["BatchID"].ToString());

                foreach (GridViewRow gr in gvDataProjectInformation.Rows)
                {

                    string clusterCode = gvDataProjectInformation.Rows[gr.RowIndex].Cells[0].Text;

                    TextBox TbInitBudgetNoOfUnit = (TextBox)gvDataProjectInformation.Rows[gr.RowIndex].Cells[2].FindControl("TbInitBudgetNoOfUnit");
                    int InitBudgetNoOfUnit = int.Parse(TbInitBudgetNoOfUnit.Text.ToString());

                    TextBox TbInitBudgetMSquare = (TextBox)gvDataProjectInformation.Rows[gr.RowIndex].Cells[3].FindControl("TbInitBudgetMSquare");
                    double InitBudgetMSquare = double.Parse(TbInitBudgetMSquare.Text.ToString());

                    TextBox TbInitBudgetValuePerMSquare = (TextBox)gvDataProjectInformation.Rows[gr.RowIndex].Cells[4].FindControl("TbInitBudgetValuePerMSquare");
                    decimal InitBudgetValuePerMSquare = decimal.Parse(TbInitBudgetValuePerMSquare.Text.ToString());

                    TextBox TbInitBudgetValuePerUnit = (TextBox)gvDataProjectInformation.Rows[gr.RowIndex].Cells[5].FindControl("TbInitBudgetValuePerUnit");
                    decimal InitBudgetValuePerUnit = decimal.Parse(TbInitBudgetValuePerUnit.Text.ToString());

                    TextBox TbInitBudgetValueTotal = (TextBox)gvDataProjectInformation.Rows[gr.RowIndex].Cells[6].FindControl("TbInitBudgetValueTotal");
                    decimal InitBudgetValueTotal = decimal.Parse(TbInitBudgetValueTotal.Text.ToString());

                    TextBox TbInitPctSalesMktExpense = (TextBox)gvDataProjectInformation.Rows[gr.RowIndex].Cells[3].FindControl("TbInitPctSalesMktExpense");
                    double InitPctSalesMktExpense = double.Parse(TbInitPctSalesMktExpense.Text.ToString());

                    _DTFinal_ProjectInformation.Rows.Add(BatchID, clusterCode, InitBudgetNoOfUnit, InitBudgetMSquare, InitBudgetValuePerMSquare, InitBudgetValuePerUnit, InitBudgetValueTotal, InitPctSalesMktExpense);

                }


                foreach (GridViewRow gr in gvDataConstCost.Rows)
                {
                    string clusterCode = gvDataConstCost.Rows[gr.RowIndex].Cells[0].Text;

                    TextBox TbConstCostOriginalValueTotal = (TextBox)gvDataConstCost.Rows[gr.RowIndex].Cells[2].FindControl("TbConstCostOriginalValueTotal");
                    decimal ConstCostOriginalValueTotal = decimal.Parse(TbConstCostOriginalValueTotal.Text.ToString());

                    TextBox TbConstCostPerMSquareArea = (TextBox)gvDataConstCost.Rows[gr.RowIndex].Cells[3].FindControl("TbConstCostPerMSquareArea");
                    decimal ConstCostPerMSquareArea = decimal.Parse(TbConstCostPerMSquareArea.Text.ToString());

                    TextBox TbProjectedValueTotalTillCompletion = (TextBox)gvDataConstCost.Rows[gr.RowIndex].Cells[4].FindControl("TbProjectedValueTotalTillCompletion");
                    decimal ProjectedValueTotalTillCompletion = decimal.Parse(TbProjectedValueTotalTillCompletion.Text.ToString());

                    TextBox TbProjectedValuePerMSquareAreaTillCompletion = (TextBox)gvDataConstCost.Rows[gr.RowIndex].Cells[5].FindControl("TbProjectedValuePerMSquareAreaTillCompletion");
                    decimal ProjectedValuePerMSquareAreaTillCompletion = decimal.Parse(TbProjectedValuePerMSquareAreaTillCompletion.Text.ToString());

                    _DTFinal_ConstCost.Rows.Add(BatchID, clusterCode, ConstCostOriginalValueTotal, ConstCostPerMSquareArea, ProjectedValueTotalTillCompletion, ProjectedValuePerMSquareAreaTillCompletion);

                }

                var tables = new[] { _DTFinal_ProjectInformation, _DTFinal_ConstCost };
                DataTable dtRes = new DataTable();
                dtRes = DataTableHelper.MergeAll(tables, "clusterCode");

                string result;
                result = clsUpdateProjectInformation.UpdateProjectInformation(dtRes, Constants.sessionUsername);

                if (result == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "inf_msg", "BootboxAlert('Update project information succeed.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + result.Replace("'", "\\'") + "');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
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