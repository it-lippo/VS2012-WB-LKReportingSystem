using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using OfficeOpenXml;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;

namespace LKReportingSystem.Forms
{
    public partial class CashFlowSetup : System.Web.UI.Page
    {

        protected static readonly ILog log = LogManager.GetLogger(typeof(CashFlowSetup));

        private DataTable _DTFinal_Cashflow;

        private void RetouchGridView()
        {

            if (DGV.Rows.Count > 0)
            {


                //Adds THEAD and TBODY to GridView.
                DGV.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int BatchID = int.Parse(Request.QueryString["batchid"]);
                ViewState["BatchID"] = BatchID;

                //log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("CashFlowSetup - Page_Load() Called.. Is Not PostBack. Parameter Received: BatchID={0}", BatchID);

                BindDataCashFlow(BatchID);
            }
            RetouchGridView();
        }

        private void BindDataCashFlow(int batchid)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.DebugFormat("BindDataCashFlow() Called.. Parameter: batchid={0}", batchid);

                _DTFinal_Cashflow = new DataTable();

                _DTFinal_Cashflow = clsCashflow.GetDataCashflow(batchid);

                DGV.DataSource = _DTFinal_Cashflow;
                DGV.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true);
            }
        }


        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && Path.GetExtension(FileUpload1.FileName) == ".xlsx")
            {
                
                try
                {

                    _DTFinal_Cashflow = new DataTable();

                    using (var excel = new ExcelPackage(FileUpload1.PostedFile.InputStream))
                    {
                        //var tbl = new DataTable();
                        var ws = excel.Workbook.Worksheets.First();
                        var hasHeader = true;  // adjust accordingly
                        // add DataColumns to DataTable
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            _DTFinal_Cashflow.Columns.Add(hasHeader ? firstRowCell.Text
                                : String.Format("Column {0}", firstRowCell.Start.Column));

                        // add DataRows to DataTable
                        int startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = _DTFinal_Cashflow.NewRow();
                            foreach (var cell in wsRow)
                                row[cell.Start.Column - 1] = cell.Text;
                            _DTFinal_Cashflow.Rows.Add(row);
                        }

                        _DTFinal_Cashflow.Columns[0].ColumnName = "CashflowItem";
                        _DTFinal_Cashflow.Columns[1].ColumnName = "CashflowValue";

                        ViewState["varDataTable"] = _DTFinal_Cashflow;


                        DGV.DataSource = _DTFinal_Cashflow;
                        DGV.DataBind();

                        RetouchGridView();

                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true);
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!clsSecurity.HaveAccessAction(this.AppRelativeVirtualPath, "edit"))
            {
                htmlNotificationCashFlow.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To Update Cash Flow";

                htmlNotificationCashFlow.Attributes.Add("class", "alert alert-danger alert-dismissable");
                updatePanelHtmlNotificationCashFlow.Update();
                return;
            }

            int BatchID = int.Parse(ViewState["BatchID"].ToString());

            _DTFinal_Cashflow = new DataTable();

            _DTFinal_Cashflow = (DataTable)ViewState["varDataTable"];
            


            if (_DTFinal_Cashflow.Rows.Count > 0)

            _DTFinal_Cashflow.Columns.Add("modifTime", System.Type.GetType("System.DateTime"));
            _DTFinal_Cashflow.Columns.Add("modifUN", System.Type.GetType("System.String"));
            _DTFinal_Cashflow.Columns.Add("inputTime", System.Type.GetType("System.DateTime"));
            _DTFinal_Cashflow.Columns.Add("inputUN", System.Type.GetType("System.String"));

            _DTFinal_Cashflow.Columns[0].ColumnName = "CashflowItem";
            _DTFinal_Cashflow.Columns[1].ColumnName = "CashflowValue";

            _DTFinal_Cashflow.Columns.Add("BatchID", System.Type.GetType("System.Int32"), BatchID.ToString());
            _DTFinal_Cashflow.Columns["BatchID"].SetOrdinal(0);

            {
                try
                {
                    
                    for (int i = 0; i < _DTFinal_Cashflow.Rows.Count - 1; i++)
                    {
                        DataRow dr = _DTFinal_Cashflow.Rows[i];

                        //if (i == 0)
                        //    dr.Delete();
                        if (string.IsNullOrEmpty(dr["CashflowItem"].ToString()) == true || dr["CashflowItem"].ToString().Trim() == "")
                        {                        
                            dr.Delete();
                            i -= 1;
                        }


                        dr["modifTime"] = DateTime.Now;
                        dr["inputTime"] = DateTime.Now;

                        dr["modifUN"] = Constants.sessionUsername;
                        dr["inputUN"] = Constants.sessionUsername;


                    }

                    _DTFinal_Cashflow.AcceptChanges();

                    string result;

                    Boolean isCashflowExist = false;
                    isCashflowExist = clsCashflow.CashflowIsExist(BatchID);

                    if (isCashflowExist == false)
                    {
                        result = clsCashflow.InsertCashflow(_DTFinal_Cashflow);
                    }
                    else
                    {
                        result = clsCashflow.UpdateCashflow(_DTFinal_Cashflow, Constants.sessionUsername);
                    }


                    if (result == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "inf_msg", "bootbox.alert({message:'Update Cashflow succeed.', title:'Success!'});", true);
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


            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //string link = Page.ResolveUrl("~/Forms/SalesSummary.aspx?isCancel=1");
            string link = Page.ResolveUrl("~/Forms/ProjectManagement.aspx?isCancel=1");
            Response.Redirect(link);
        }

        protected void TemplateButton_Click(object sender, EventArgs e)
        {

            string link = Page.ResolveUrl("~/Template/TemplateDownloader.aspx?Type=Cashflow");
            string s = "window.location.assign('" + link + "');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", s, true);
        }

        protected void DGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "ITEM";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[0].Width = 500;

                e.Row.Cells[1].Text = "VALUE";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            { e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right; }

        }


    }
}