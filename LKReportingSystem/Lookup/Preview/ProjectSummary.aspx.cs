using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using LKReportingSystem.Class.Forms;
using LKReportingSystem.Class;
using System.Globalization;

namespace LKReportingSystem.Lookup.Preview
{
    public partial class ProjectSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string isGenerate = "";

            try
            { isGenerate = Request.QueryString["isGenerate"];  }
            catch (Exception ex)
            { }

            if (isGenerate != "1")
            {
                if (!clsSecurity.HaveAccess(this.AppRelativeVirtualPath))
                {
                    string HTMLErrorMsg = "<h3><strong>You Do Not Have Permission To Access This Site</strong></h3>";

                    string errHtml = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/ShowErrorMessage.html"));

                    errHtml = errHtml.Replace("@contenthtml", HTMLErrorMsg);

                    ltView.Text = errHtml;
                    return;
                }
            }

            DataTable dt_BatchID = clsSalesSummary.GetAllBatchID();

            if (dt_BatchID.Rows.Count > 0)
            {

                string HTMLContentReport = "";

                string html = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/SalesReportSummary.html"));

                for (int i = 0; i < dt_BatchID.Rows.Count; i++)
                {
                    //Initial Budget

                    decimal totalInitBudgetNoOfUnit = 0;
                    decimal totalInitBudgetValueTotal = 0;

                    //Sales Performance
                    decimal totalMktGold = 0;
                    decimal totalSalesValueTotal = 0;
                    decimal totalSalesCollection = 0;
                    decimal totalSalesPercentageCollection = 0;
                    decimal totalSalesInventoryValueTotal = 0;
                    decimal totalSalesProjectedValueTotal = 0;

                    //Cost
                    decimal totalCostMarketingValueTotal = 0;
                    decimal totalActualPaidComm = 0;
                    decimal totalActualUnpaidComm = 0;
                    decimal totalPaidSalary = 0;
                    decimal totalActualBudgetMKTExpense = 0;

                    decimal totalCostInitialConstructionValueTotal = 0;
                    decimal totalCostActualConstructionValueTotal = 0;


                    //Table Details Sales Report
                    DataTable dt_DetailSalesReport = clsSalesSummary.GetDataDetailSalesReport(dt_BatchID.Rows[i]["BatchID"].ToString());
                    if (dt_DetailSalesReport.Rows.Count > 0)
                    {

                        for (int j = 0; j < dt_DetailSalesReport.Rows.Count; j++)
                        {

                            totalInitBudgetNoOfUnit += Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["InitBudgetNoOfUnit"].ToString());


                            decimal tempTotalInitBudgetValueTotal = Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["InitBudgetValueTotal"].ToString());

                            totalInitBudgetValueTotal += Math.Round(tempTotalInitBudgetValueTotal, 1);


                            totalSalesValueTotal += Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["SalesValueTotal"].ToString());


                            totalSalesInventoryValueTotal += Math.Round(Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["InventoryValueTotal"].ToString()) / 1000000000, 1);

                            totalSalesProjectedValueTotal += Math.Round(Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["ProjectedValueTotal"].ToString()) / 1000000000, 1);

                            totalMktGold = totalMktGold + Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["SalesMKTValueTotal"].ToString());


                            //Marketing Expense
                            totalActualPaidComm += Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["ActualPaidComm"].ToString());
                            totalActualUnpaidComm += Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["ActualUnpaidComm"].ToString());
                            totalActualBudgetMKTExpense += Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["ActualBudgetMKTExpense"].ToString());


                            //Construction Cost
                            decimal tempInitBudgetMsSquare = Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["InitBudgetMSquare"].ToString());
                            decimal tempInitBudgetValueConstCostPerMSquare = Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["InitBudgetValueConstCostPerMSquare"].ToString());

                            if (tempInitBudgetMsSquare != 0 && tempInitBudgetValueConstCostPerMSquare != 0)
                            {
                                totalCostInitialConstructionValueTotal += Math.Round((tempInitBudgetMsSquare * tempInitBudgetValueConstCostPerMSquare) / 1000000000, 1);
                            }

                            totalCostActualConstructionValueTotal += Math.Round(Convert.ToDecimal(dt_DetailSalesReport.Rows[j]["ActualValueDirectCostWithApportionment"].ToString()) / 1000000000, 1);                           

                        }
                    }

                    //Table Schedule Collection
                    DataTable dt_Collection = clsSalesSummary.GetDataScheduleCollection(dt_BatchID.Rows[i]["BatchID"].ToString());
                    if (dt_Collection.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt_Collection.Rows.Count; j++)
                        {
                            totalSalesCollection += Convert.ToDecimal(dt_Collection.Rows[j]["totalcollection"].ToString());
                        }
                    }


                    totalPaidSalary = clsSalesSummary.GetDataPaidSalary(dt_BatchID.Rows[i]["BatchID"].ToString());

                    totalCostMarketingValueTotal = totalActualPaidComm + totalActualUnpaidComm + totalPaidSalary + totalActualBudgetMKTExpense;
                    totalCostMarketingValueTotal = Math.Round(totalCostMarketingValueTotal / 1000000000, 1);


                    if (totalInitBudgetValueTotal != 0)
                    {

                        totalInitBudgetValueTotal = Math.Round(totalInitBudgetValueTotal / 1000000000, 1);
                    }

                    totalSalesValueTotal = Math.Round(totalSalesValueTotal / 1000000000, 1);
                    totalSalesCollection = Math.Round(totalSalesCollection / 1000000000, 1);
                    totalMktGold = Math.Round(totalMktGold / 1000000000, 1);

                    totalSalesPercentageCollection = Math.Round(totalSalesCollection / totalSalesValueTotal * 100, 0);

                    HTMLContentReport += string.Format(@"<tr>
                            <td style='text-align: center; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2:n0}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3:n1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4:n1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5:n1}</td>
                        
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6:n1}</td>
                            
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{7:F2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{8:n1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{9:n1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{10:n1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{11:n1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{12:n1}</td>

                        </tr>", dt_BatchID.Rows[i]["ProjectCode"].ToString(),
                                "<a href='" + Page.ResolveUrl(Constants.rootURL + "Lookup/Preview/ViewSalesSummary.aspx?batchid=" + dt_BatchID.Rows[i]["BatchID"].ToString())  +"' target='_blank'>" + dt_BatchID.Rows[i]["ProjectName"].ToString() +"</a>", 
                                totalInitBudgetNoOfUnit, 
                                totalInitBudgetValueTotal,
                                totalMktGold,
                                totalSalesValueTotal,
                                totalSalesCollection, 
                                string.Format("{0}%", totalSalesPercentageCollection), 
                                totalSalesInventoryValueTotal,
                                totalSalesProjectedValueTotal, 
                                totalCostMarketingValueTotal,
                                totalCostInitialConstructionValueTotal, 
                                totalCostActualConstructionValueTotal);
                }

                html = html.Replace("@ReportAsOfDate", clsSalesSummary.GenerateAsOfDate())
                    .Replace("@ContentReport", HTMLContentReport);

                ltView.Text = html;
            }
        }
    }
}