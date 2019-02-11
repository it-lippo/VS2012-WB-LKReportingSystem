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
using System.Globalization;

namespace LKReportingSystem.Forms
{
    public partial class InitialBudgetSetup : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(UpdateProjectInformation));

        private DataTable _DT_InitialBudget;
        private DataTable _DTFinal_InitialBudget;

        private void initDTFinal_InitialBudget()
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_InitialBudget - Started");

            _DTFinal_InitialBudget = new DataTable();

            _DTFinal_InitialBudget.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
            _DTFinal_InitialBudget.Columns.Add("ClusterCode", System.Type.GetType("System.String"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetNoOfUnit", System.Type.GetType("System.Int32"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValuePerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValuePerUnit", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValueTotal", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValueLandCostPerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValueConstCostPerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValueCOGSPerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitBudgetValueGrossMarginPerMSquare", System.Type.GetType("System.Decimal"));
            _DTFinal_InitialBudget.Columns.Add("InitPctSalesMktExpense", System.Type.GetType("System.Double"));
            _DTFinal_InitialBudget.Columns.Add("InitPctMKTExpense", System.Type.GetType("System.Double"));
            _DTFinal_InitialBudget.Columns.Add("InitPctSalesExpense", System.Type.GetType("System.Double"));
            _DTFinal_InitialBudget.Columns.Add("InitPctCapexSubsidyExpense", System.Type.GetType("System.Double"));

            log.DebugFormat("initDTFinal_InitialBudget - Finished");

        }

        private void RetouchGridView()
        {
            if (gvInitialBudget.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                gvInitialBudget.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvInitialBudget.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvInitialBudget.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvInitialBudget.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvInitialBudget.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                gvInitialBudget.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvInitialBudget.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int BatchID = int.Parse(Request.QueryString["batchid"]);
                ViewState["BatchID"] = BatchID;

                //log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("UpdateIn itialBudgetSetup - Page_Load() Called.. Is Not PostBack. Parameter Received: BatchID={0}", BatchID);

                BindDataInitialBudget(BatchID);
            }

            RetouchGridView();


        }

        protected void gvInitialBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //0 - ClusterCode
            e.Row.Cells[0].Visible = false;
            //14 - % Sales MKT Expense
            e.Row.Cells[14].Visible = false;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox TbInitBudgetNoOfUnit = (TextBox)e.Row.FindControl("TbInitBudgetNoOfUnit");

                TextBox TbInitBudgetMSquare = (TextBox)e.Row.FindControl("TbInitBudgetMSquare");

                TextBox TbInitBudgetValuePerMSquare = (TextBox)e.Row.FindControl("TbInitBudgetValuePerMSquare");

                TextBox TbInitBudgetValuePerUnit = (TextBox)e.Row.FindControl("TbInitBudgetValuePerUnit");

                TextBox TbInitBudgetValueTotal = (TextBox)e.Row.FindControl("TbInitBudgetValueTotal");

                TextBox TbInitBudgetValueLandCostPerMSquare = (TextBox)e.Row.FindControl("TbInitBudgetValueLandCostPerMSquare");

                TextBox TbInitBudgetValueConstCostPerMSquare = (TextBox)e.Row.FindControl("TbInitBudgetValueConstCostPerMSquare");

                TextBox TbInitBudgetValueCOGSPerMSquare = (TextBox)e.Row.FindControl("TbInitBudgetValueCOGSPerMSquare");

                TextBox TbInitBudgetValueGrossMarginPerMSquare = (TextBox)e.Row.FindControl("TbInitBudgetValueGrossMarginPerMSquare");

                TbInitBudgetMSquare.Attributes.Add("onkeyup", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");
                TbInitBudgetMSquare.Attributes.Add("onchange", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");

                TbInitBudgetValuePerMSquare.Attributes.Add("onkeyup", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");
                TbInitBudgetValuePerMSquare.Attributes.Add("onchange", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");

                TbInitBudgetNoOfUnit.Attributes.Add("onkeyup", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");
                TbInitBudgetNoOfUnit.Attributes.Add("onchange", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");

                TbInitBudgetValueLandCostPerMSquare.Attributes.Add("onkeyup", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");
                TbInitBudgetValueLandCostPerMSquare.Attributes.Add("onchange", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");

                TbInitBudgetValueConstCostPerMSquare.Attributes.Add("onkeyup", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");
                TbInitBudgetValueConstCostPerMSquare.Attributes.Add("onchange", "CalculateAmount('" + TbInitBudgetNoOfUnit.ClientID + "','" + TbInitBudgetMSquare.ClientID + "','" + TbInitBudgetValuePerMSquare.ClientID + "','" + TbInitBudgetValueTotal.ClientID + "','" + TbInitBudgetValuePerUnit.ClientID + "','" + TbInitBudgetValueLandCostPerMSquare.ClientID + "','" + TbInitBudgetValueConstCostPerMSquare.ClientID + "','" + TbInitBudgetValueCOGSPerMSquare.ClientID + "','" + TbInitBudgetValueGrossMarginPerMSquare.ClientID + "')");

                TbInitBudgetValuePerUnit.Attributes.Add("readonly", "readonly");
                TbInitBudgetValueTotal.Attributes.Add("readonly", "readonly");
                TbInitBudgetValueCOGSPerMSquare.Attributes.Add("readonly", "readonly");
                TbInitBudgetValueGrossMarginPerMSquare.Attributes.Add("readonly", "readonly");
            }
        }

        private void BindDataInitialBudget(int batchid)
        {
            try
            {
                //log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("BindDataInitialBudget() Called.. Parameter: batchid={0}", batchid);

                _DT_InitialBudget = new DataTable();

                _DT_InitialBudget = clsInitialBudgetSetup.GetDataInitialBudget(batchid);

                gvInitialBudget.DataSource = _DT_InitialBudget;
                gvInitialBudget.DataBind();

                RetouchGridView();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //log4net.Config.XmlConfigurator.Configure();

            if (!clsSecurity.HaveAccessAction(this.AppRelativeVirtualPath, "edit"))
            {
                htmlNotificationBudget.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To Update Initial Budget";

                htmlNotificationBudget.Attributes.Add("class", "alert alert-danger alert-dismissable");
                updatePanelHtmlNotificationBudget.Update();
                return;
            }

            try
            {

                initDTFinal_InitialBudget();

                int BatchID = int.Parse(ViewState["BatchID"].ToString());

                Boolean zeroChecker=false;

                foreach (GridViewRow gr in gvInitialBudget.Rows)
                {

                    string clusterCode = gvInitialBudget.Rows[gr.RowIndex].Cells[0].Text;

                    TextBox TbInitBudgetNoOfUnit = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[2].FindControl("TbInitBudgetNoOfUnit");
                    int InitBudgetNoOfUnit = int.Parse(TbInitBudgetNoOfUnit.Text.ToString());
                    if (InitBudgetNoOfUnit == 0)
                        zeroChecker = true;

                    TextBox TbInitBudgetMSquare = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[3].FindControl("TbInitBudgetMSquare");
                    double InitBudgetMSquare = double.Parse(TbInitBudgetMSquare.Text.ToString());
                    if (InitBudgetMSquare == 0)
                        zeroChecker = true;

                    TextBox TbInitBudgetValuePerMSquare = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[4].FindControl("TbInitBudgetValuePerMSquare");
                    decimal InitBudgetValuePerMSquare = decimal.Parse(TbInitBudgetValuePerMSquare.Text.ToString());
                    if (InitBudgetValuePerMSquare == 0)
                        zeroChecker = true;

                    TextBox TbInitBudgetValuePerUnit = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[5].FindControl("TbInitBudgetValuePerUnit");
                    decimal InitBudgetValuePerUnit = decimal.Parse(TbInitBudgetValuePerUnit.Text.ToString());

                    TextBox TbInitBudgetValueTotal = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[6].FindControl("TbInitBudgetValueTotal");
                    decimal InitBudgetValueTotal = decimal.Parse(TbInitBudgetValueTotal.Text.ToString());

                    TextBox TbInitBudgetValueLandCostPerMSquare = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[7].FindControl("TbInitBudgetValueLandCostPerMSquare");
                    decimal InitBudgetValueLandCostPerMSquare = decimal.Parse(TbInitBudgetValueLandCostPerMSquare.Text.ToString());
                    if (InitBudgetValueLandCostPerMSquare == 0)
                        zeroChecker = true;

                    TextBox TbInitBudgetValueConstCostPerMSquare = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[8].FindControl("TbInitBudgetValueConstCostPerMSquare");
                    decimal InitBudgetValueConstCostPerMSquare = decimal.Parse(TbInitBudgetValueConstCostPerMSquare.Text.ToString());
                    if (InitBudgetValueConstCostPerMSquare == 0)
                        zeroChecker = true;

                    TextBox TbInitBudgetValueCOGSPerMSquare = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[9].FindControl("TbInitBudgetValueCOGSPerMSquare");
                    decimal InitBudgetValueCOGSPerMSquare = decimal.Parse(TbInitBudgetValueCOGSPerMSquare.Text.ToString());

                    TextBox TbInitBudgetValueGrossMarginPerMSquare = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[10].FindControl("TbInitBudgetValueGrossMarginPerMSquare");
                    decimal InitBudgetValueGrossMarginPerMSquare = decimal.Parse(TbInitBudgetValueGrossMarginPerMSquare.Text.ToString());

                    TextBox TbInitPctMKTExpense = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[11].FindControl("TbInitPctMKTExpense");
                    double InitPctMKTExpense = double.Parse(TbInitPctMKTExpense.Text.ToString());

                    TextBox TbInitPctSalesExpense = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[12].FindControl("TbInitPctSalesExpense");
                    double InitPctSalesExpense = double.Parse(TbInitPctSalesExpense.Text.ToString());

                    TextBox TbInitPctCapexSubsidyExpense = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[13].FindControl("TbInitPctCapexSubsidyExpense");
                    double InitPctCapexSubsidyExpense = double.Parse(TbInitPctCapexSubsidyExpense.Text.ToString());


                    //TextBox TbInitPctSalesMktExpense = (TextBox)gvInitialBudget.Rows[gr.RowIndex].Cells[11].FindControl("TbInitPctSalesMktExpense");
                    //double InitPctSalesMktExpense = double.Parse(TbInitPctSalesMktExpense.Text.ToString());
                    double InitPctSalesMktExpense = 0;

                    _DTFinal_InitialBudget.Rows.Add(BatchID, clusterCode, InitBudgetNoOfUnit, InitBudgetMSquare, InitBudgetValuePerMSquare, InitBudgetValuePerUnit, InitBudgetValueTotal,
                                                    InitBudgetValueLandCostPerMSquare, InitBudgetValueConstCostPerMSquare, InitBudgetValueCOGSPerMSquare, InitBudgetValueGrossMarginPerMSquare, 
                                                    InitPctSalesMktExpense, InitPctMKTExpense, InitPctSalesExpense, InitPctCapexSubsidyExpense);

                }

                if (zeroChecker)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({title:'Oops!', message:'Following Field Cannot Be Zero or Empty: <ul> <li>No Of Unit</li> <li>M2 (SGA)</li> <li>Rp/M2 (SGA) (Revenue)</li> <li>Land Cost/M2</li> <li>Const Cost/M2</li> <ul>'});", true);
                    return;
                }


                string result;
                result = clsInitialBudgetSetup.UpdateInitialBudget(_DTFinal_InitialBudget, Constants.sessionUsername);

                if (result == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "inf_msg", "bootbox.alert({message:'Update Initial Budget succeed.', title:'Success!'});", true);
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