using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class.Forms;
using System.Globalization;

namespace LKReportingSystem.Lookup.Preview
{
    public partial class ViewSalesSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string batchid = Request.QueryString["batchid"].ToString();

            DataTable dt = clsSalesSummary.GetDataDetailSalesReport(batchid);

            if (dt.Rows.Count > 0)
            {

                Boolean errorChecker = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dt.Rows[i]["InitBudgetNoOfUnit"].ToString()) == 0) { errorChecker = true; }
                    if (Convert.ToDecimal(dt.Rows[i]["InitBudgetMSquare"].ToString()) == 0) { errorChecker = true; }
                    if (Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerMSquare"].ToString()) == 0) { errorChecker = true; }
                    if (Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerUnit"].ToString()) == 0) { errorChecker = true; }
                    if (Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) == 0) { errorChecker = true; }
                    if (Convert.ToDecimal(dt.Rows[i]["InitBudgetValueConstCostPerMSquare"].ToString()) == 0) { errorChecker = true; }
                }

                if (errorChecker)
                {
                    string errMsg = "<strong>[Initial Budget]</strong> - Following Field Cannot Be Zero or Empty:  <ul> <li>No Of Unit</li> <li>M2 (SGA)</li> <li>Rp/M2 (SGA) (Revenue)</li> <li>Land Cost/M2</li> <li>Const Cost/M2</li> </ul>";

                    string messageErr = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/ShowErrorMessage.html"));

                    messageErr = messageErr.Replace("@contenthtml", errMsg);

                    ltView.Text = messageErr;

                    return;
                }

                string HTMLContent1 = "";
                string HTMLContent2 = "";
                string HTMLContent3 = "";
                string HTMLContent4 = "";
                string HTMLContent5 = "";



                string message = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/SalesSummary.html"));

                decimal _TotalInitBudgetNoOfUnit = 0;
                decimal _TotalInitBudgetMSquare = 0;
                decimal _TotalInitBudgetValuePerMSquare = 0;
                decimal _TotalInitBudgetValuePerUnit = 0;
                decimal _TotalInitBudgetValueTotal = 0;
                decimal _TotalSalesNoOfUnit = 0;
                decimal _TotalSalesMSquareArea = 0;
                decimal _TotalSalesValuePerMSquareArea = 0;
                decimal _TotalSalesValueTotal = 0;
                decimal _TotalSalesMKTValuePerMSquareArea = 0;
                decimal _TotalSalesMKTValueTotal = 0;
                decimal _TotalActualPaidComm = 0;
                decimal _TotalActualUnpaidComm = 0;
                decimal _TotalActualBudgetMKTExpense = 0;
                decimal _TotalInventoryNoOfUnit = 0;
                decimal _TotalInventoryMSquareArea = 0;
                decimal _TotalInventoryValuePerMSquareArea = 0;
                decimal _TotalInventoryValueTotal = 0;
                decimal _TotalProjectedNoOfUnit = 0;
                decimal _TotalProjectedMSquareArea = 0;
                decimal _TotalProjectedValuePerMSquareArea = 0;
                decimal _TotalProjectedValueTotal = 0;
                decimal _TotalInitValueSalesMKTExpense = 0;
                decimal _TotalActualValueSalesMKTExpense = 0;

                decimal _TotalVarianceProjectedToInitialValuePerMSquareArea = 0;
                decimal _TotalVarianceProjectedToInitialValueTotal = 0;
                decimal _TotalVariancePercentageProjectedToInitial = 0;


                decimal _TotalInitBudgetValueConstCost = 0;
                decimal _TotalActualValueDirectCostWithApportionment = 0;
                decimal _TotalProjectedValueTotalTillCompletion = 0;

                decimal _TotalCollSales = 0;
                decimal _TotalCollCollection = 0;

                decimal _TotalPaidSalary = 0;


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4}</td>
                        
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5}</td>
                            
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{7}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{8}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{9}</td>

                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{10}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{11}</td>

                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{12}</td>

                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{13}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{14}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{15}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{16}</td>
                    
                        </tr>", dt.Rows[i]["ClusterName"].ToString(),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["InitBudgetNoOfUnit"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["InitBudgetMSquare"].ToString())),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerMSquare"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerUnit"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) / 1000000000, 1)),

                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["SalesNoOfUnit"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["SalesMSquareArea"].ToString())),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["SalesValuePerMSquareArea"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["SalesValueTotal"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["SalesMKTValuePerMSquareArea"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["SalesMKTValueTotal"].ToString()) / 1000000000, 1)),
                                string.Format("{0:F2} %", (Math.Round(Convert.ToDecimal(dt.Rows[i]["SalesValueTotal"].ToString()) / 1000000000, 1) / Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotalTillCompletion"].ToString()) / 1000000000, 1)) * 100),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["InventoryNoOfUnit"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["InventoryMSquareArea"].ToString())),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InventoryValuePerMSquareArea"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InventoryValueTotal"].ToString()) / 1000000000, 1))
                                );



                    _TotalInitBudgetNoOfUnit = _TotalInitBudgetNoOfUnit + Convert.ToDecimal(dt.Rows[i]["InitBudgetNoOfUnit"].ToString());
                    _TotalInitBudgetMSquare = _TotalInitBudgetMSquare + Convert.ToDecimal(dt.Rows[i]["InitBudgetMSquare"].ToString());
                    _TotalInitBudgetValuePerUnit = _TotalInitBudgetValuePerUnit + Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerUnit"].ToString());
                    _TotalInitBudgetValueTotal = _TotalInitBudgetValueTotal + Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString());

                    _TotalSalesNoOfUnit = _TotalSalesNoOfUnit + Convert.ToDecimal(dt.Rows[i]["SalesNoOfUnit"].ToString());
                    _TotalSalesMSquareArea = _TotalSalesMSquareArea + Convert.ToDecimal(dt.Rows[i]["SalesMSquareArea"].ToString());
                    _TotalSalesValueTotal = _TotalSalesValueTotal + Convert.ToDecimal(dt.Rows[i]["SalesValueTotal"].ToString());
                    _TotalSalesMKTValueTotal = _TotalSalesMKTValueTotal + Convert.ToDecimal(dt.Rows[i]["SalesMKTValueTotal"].ToString());
                    _TotalActualPaidComm = _TotalActualPaidComm + Convert.ToDecimal(dt.Rows[i]["ActualPaidComm"].ToString());
                    _TotalActualUnpaidComm = _TotalActualUnpaidComm + Convert.ToDecimal(dt.Rows[i]["ActualUnpaidComm"].ToString());
                    _TotalActualBudgetMKTExpense = _TotalActualBudgetMKTExpense + Convert.ToDecimal(dt.Rows[i]["ActualBudgetMKTExpense"].ToString());

                    _TotalInventoryNoOfUnit = _TotalInventoryNoOfUnit + Convert.ToDecimal(dt.Rows[i]["InventoryNoOfUnit"].ToString());
                    _TotalInventoryMSquareArea = _TotalInventoryMSquareArea + Convert.ToDecimal(dt.Rows[i]["InventoryMSquareArea"].ToString());
                    _TotalInventoryValueTotal = _TotalInventoryValueTotal + Convert.ToDecimal(dt.Rows[i]["InventoryValueTotal"].ToString());

                    _TotalProjectedNoOfUnit = _TotalProjectedNoOfUnit + Convert.ToDecimal(dt.Rows[i]["ProjectedNoOfUnit"].ToString());
                    _TotalProjectedMSquareArea = _TotalProjectedMSquareArea + Convert.ToDecimal(dt.Rows[i]["ProjectedMSquareArea"].ToString());
                    _TotalProjectedValueTotal = _TotalProjectedValueTotal + Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString());

                    _TotalInitValueSalesMKTExpense = _TotalInitValueSalesMKTExpense + Convert.ToDecimal(dt.Rows[i]["InitValueSalesMKTExpense"].ToString());



                    HTMLContent2 = HTMLContent2 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6}</td>
                                            
                        </tr>",
                                dt.Rows[i]["ClusterName"].ToString(),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetMSquare"].ToString()) * Convert.ToDecimal(dt.Rows[i]["InitBudgetValueConstCostPerMSquare"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueConstCostPerMSquare"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ActualValueDirectCostWithApportionment"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ActualValueDirectCostWithApportionmentPerMSquareArea"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotalTillCompletion"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValuePerMSquareAreaTillCompletion"].ToString()) / 1000000, 1))
                                );


                    _TotalVarianceProjectedToInitialValuePerMSquareArea = _TotalVarianceProjectedToInitialValuePerMSquareArea + Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValuePerMSquareArea"].ToString()) / 1000000, 1) - Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerMSquare"].ToString()) / 1000000, 1);
                    _TotalVarianceProjectedToInitialValueTotal = _TotalVarianceProjectedToInitialValueTotal + Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString()) / 1000000000, 1) - Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) / 1000000000, 1);



                    if (Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString()) / 1000000000, 1) != 0 && Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) / 1000000000, 1) != 0)
                    {
                        _TotalVarianceProjectedToInitialValueTotal = Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString()) / 1000000000, 1) / Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) / 1000000000, 1);
                    }
                    else
                    {
                        _TotalVarianceProjectedToInitialValueTotal = 0;
                    }


                    //table projected and variance
                    HTMLContent4 = HTMLContent4 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4}</td>
                        
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5}</td>                          
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{7}</td>                            
                    
                        </tr>", dt.Rows[i]["ClusterName"].ToString(),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["ProjectedNoOfUnit"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["ProjectedMSquareArea"].ToString())),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValuePerMSquareArea"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValuePerMSquareArea"].ToString()) / 1000000, 1) - Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValuePerMSquare"].ToString()) / 1000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString()) / 1000000000, 1) - Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) / 1000000000, 1)),
                                string.Format("{0:F2} %", (Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotal"].ToString()) / 1000000000, 1) / Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetValueTotal"].ToString()) / 1000000000, 1)) * 100)
                                );




                    _TotalInitBudgetValueConstCost = _TotalInitBudgetValueConstCost + Math.Round(Convert.ToDecimal(dt.Rows[i]["InitBudgetMSquare"].ToString()) * Convert.ToDecimal(dt.Rows[i]["InitBudgetValueConstCostPerMSquare"].ToString()) / 1000000000, 1);
                    _TotalActualValueDirectCostWithApportionment = _TotalActualValueDirectCostWithApportionment + Math.Round(Convert.ToDecimal(dt.Rows[i]["ActualValueDirectCostWithApportionment"].ToString()) / 1000000000, 1);
                    _TotalProjectedValueTotalTillCompletion = _TotalProjectedValueTotalTillCompletion + Math.Round(Convert.ToDecimal(dt.Rows[i]["ProjectedValueTotalTillCompletion"].ToString()) / 1000000000, 1);

                }





                //table schedule collection
                DataTable dtCollection = clsSalesSummary.GetDataScheduleCollection(batchid);
                if (dtCollection.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCollection.Rows.Count; i++)
                    {
                        HTMLContent3 = HTMLContent3 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                                            
                        </tr>", dtCollection.Rows[i]["PaymentTerm"].ToString(),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dtCollection.Rows[i]["totalschedule"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Math.Round(Convert.ToDecimal(dtCollection.Rows[i]["totalcollection"].ToString()) / 1000000000, 1)),
                                string.Format("{0:n1}", Convert.ToDecimal(dtCollection.Rows[i]["pctcollection"].ToString()))
                                );

                        _TotalCollSales = _TotalCollSales + Convert.ToDecimal(dtCollection.Rows[i]["totalschedule"].ToString());
                        _TotalCollCollection = _TotalCollCollection + Convert.ToDecimal(dtCollection.Rows[i]["totalcollection"].ToString());

                    }
                }




                if (_TotalInitBudgetValueTotal != 0 && _TotalInitBudgetMSquare != 0)
                {
                    _TotalInitBudgetValuePerMSquare = _TotalInitBudgetValueTotal / _TotalInitBudgetMSquare;
                }

                if (_TotalSalesValueTotal != 0 && _TotalSalesMSquareArea != 0)
                {
                    _TotalSalesValuePerMSquareArea = _TotalSalesValueTotal / _TotalSalesMSquareArea;
                }

                if (_TotalSalesMKTValueTotal != 0 && _TotalSalesMSquareArea != 0)
                {
                    _TotalSalesMKTValuePerMSquareArea = _TotalSalesMKTValueTotal / _TotalSalesMSquareArea;
                }

                if (_TotalInventoryValueTotal != 0 && _TotalInventoryMSquareArea != 0)
                {
                    _TotalInventoryValuePerMSquareArea = _TotalInventoryValueTotal / _TotalInventoryMSquareArea;
                }

                if (_TotalProjectedValueTotal != 0 && _TotalProjectedMSquareArea != 0)
                {
                    _TotalProjectedValuePerMSquareArea = _TotalProjectedValueTotal / _TotalProjectedMSquareArea;
                }


                //Summary ContentReport1
                HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td style='text-align: left; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{0}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{1}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{2}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{3}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{4}</td>
                        
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{5}</td>
                            
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{6}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{7}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{8}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{9}</td>

                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{10}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{11}</td>

                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{12}</td>

                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{13}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{14}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{15}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{16}</td>
                    
                        </tr>", "",
                              string.Format("{0:n0}", _TotalInitBudgetNoOfUnit),
                              string.Format("{0:n0}", _TotalInitBudgetMSquare),
                              string.Format("{0:n1}", Math.Round(_TotalInitBudgetValuePerMSquare / 1000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalInitBudgetValuePerUnit / 1000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalInitBudgetValueTotal / 1000000000, 1)),
                              string.Format("{0:n0}", _TotalSalesNoOfUnit),
                              string.Format("{0:n0}", _TotalSalesMSquareArea),
                              string.Format("{0:n1}", Math.Round(_TotalSalesValuePerMSquareArea / 1000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalSalesValueTotal / 1000000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalSalesMKTValuePerMSquareArea / 1000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalSalesMKTValueTotal / 1000000000, 1)),
                              "",
                              string.Format("{0:n0}", _TotalInventoryNoOfUnit),
                              string.Format("{0:n0}", _TotalInventoryMSquareArea),
                              string.Format("{0:n1}", Math.Round(_TotalInventoryValuePerMSquareArea / 1000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalInventoryValueTotal / 1000000000, 1)),

                              string.Format("{0:n0}", _TotalProjectedNoOfUnit),
                              string.Format("{0:n0}", _TotalProjectedMSquareArea),
                              string.Format("{0:n1}", Math.Round(_TotalProjectedValuePerMSquareArea / 1000000, 1)),
                              string.Format("{0:n1}", Math.Round(_TotalProjectedValueTotal / 1000000000, 1))
                              );


                //Summary ContentReport2
                HTMLContent2 = HTMLContent2 + string.Format(@"<tr>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'><b>{0}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px: background-color:#FFFFFF' class='defaultBGcolor'></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'><b>{1}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'><b>{2}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'></td>
           
                            </tr>", string.Format("{0:n1}", string.Format("{0:n1}", _TotalInitBudgetValueConstCost)),
                                    string.Format("{0:n1}", string.Format("{0:n1}", _TotalActualValueDirectCostWithApportionment)),
                                    string.Format("{0:n1}", string.Format("{0:n1}", _TotalProjectedValueTotalTillCompletion))
                            );


                //Summary ContentReport3
                HTMLContent3 = HTMLContent3 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'><b>{0}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'><b>{1}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF' class='defaultBGcolor'></td>                           
                                            
                             </tr>", string.Format("{0:n1}", Math.Round(_TotalCollSales / 1000000000, 1)),
                                     string.Format("{0:n1}", Math.Round(_TotalCollCollection / 1000000000, 1))
                               );


                //Summary ContentReport4
                HTMLContent4 = HTMLContent4 + string.Format(@"<tr>

                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'></td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{0}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{1}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{2}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{3}</td>

                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{4}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'>{5}</td>
                            <td style='text-align: right; font-weight:bold; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px; background-color:#FFFFFF'></td>
           
                            </tr>", string.Format("{0:n0}", _TotalProjectedNoOfUnit),
                                    string.Format("{0:n0}", _TotalProjectedMSquareArea),
                                    string.Format("{0:n1}", Math.Round(_TotalProjectedValuePerMSquareArea / 1000000, 1)),
                                    string.Format("{0:n1}", Math.Round(_TotalProjectedValueTotal / 1000000000, 1)),
                                    string.Format("{0:n1}", string.Format("")),
                                    string.Format("{0:n1}", string.Format(""))
                            );



                decimal __PctInitialSalesMktExpense = 0;

                if (_TotalInitValueSalesMKTExpense == 0 || _TotalInitBudgetValueTotal == 0)
                {
                    __PctInitialSalesMktExpense = 0;
                }
                else
                {
                    __PctInitialSalesMktExpense = (_TotalInitValueSalesMKTExpense / _TotalInitBudgetValueTotal) * 100;
                }


                //semua biaya expense ditambahkan ke var ini (paidcomm+salary+electricity+budgetmkt)
                _TotalPaidSalary = clsSalesSummary.GetDataPaidSalary(batchid);

                _TotalActualValueSalesMKTExpense = _TotalActualPaidComm + _TotalActualUnpaidComm + _TotalPaidSalary + _TotalActualBudgetMKTExpense;


                decimal __PctActualSalesMktExpense = 0;
                __PctActualSalesMktExpense = (_TotalActualValueSalesMKTExpense / _TotalSalesValueTotal) * 100;



                //table Cashflow
                DataTable dtCashflow = clsSalesSummary.GetDataCashflow(batchid);
                if (dtCashflow.Rows.Count > 0)
                {

                    string _CashFlowItem = "";
                    string _CashFlowValue = "";

                    for (int i = 0; i < dtCashflow.Rows.Count; i++)
                    {

                        try
                        {
                            _CashFlowValue = Convert.ToString(string.Format("{0:n0}", decimal.Parse(dtCashflow.Rows[i]["CashflowValue"].ToString())));
                        }
                        catch
                        {
                            _CashFlowValue = "";
                        }

                        _CashFlowItem = dtCashflow.Rows[i]["CashflowItem"].ToString().TrimStart();




                        HTMLContent5 = HTMLContent5 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px; background-color:#FFFFFF'>{0}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px; background-color:#FFFFFF'>{1}</td>
                                                                     
                        </tr>",  _CashFlowItem,
                                 _CashFlowValue
                                );

                    }
                }



                string URLActualSalesMktExpense = Page.ResolveUrl("~/Lookup/Preview/ViewSalesMktExpenseDetail.aspx?batchid=" + Request.QueryString["batchid"].ToString());
                string URLScheduleCollection = Page.ResolveUrl("~/Lookup/Preview/ViewScheduleCollectionDetail.aspx?batchid=" + Request.QueryString["batchid"].ToString());
                string URLActualCostContract = Page.ResolveUrl("~/Lookup/Preview/ViewActualCostContract.aspx?batchid=" + Request.QueryString["batchid"].ToString());



                message = message.Replace("@ProjectName", dt.Rows[0]["projectName"].ToString().ToUpper())
                                .Replace("@ReportAsOfDate", DateTime.Parse(dt.Rows[0]["ReportAsOfDate"].ToString()).ToShortDateString())
                                .Replace("@BatchDate", DateTime.Parse(dt.Rows[0]["BatchDate"].ToString()).ToShortDateString())
                                .Replace("@ContentReport1", HTMLContent1)
                                .Replace("@ContentReport2", HTMLContent2)
                                .Replace("@ContentReport3", HTMLContent3)
                                .Replace("@ContentReport4", HTMLContent4)
                                .Replace("@ContentReport5", HTMLContent5)
                                .Replace("@InitialSalesMarketingExpenses", string.Format("{0:n1}", Math.Round(_TotalInitValueSalesMKTExpense) / 1000000000, 1))
                                .Replace("@PctInitialSalesMarketingExpenses", string.Format("{0:F2} %", __PctInitialSalesMktExpense))
                                .Replace("@ActualSalesMarketingExpenses", string.Format("{0:n1}", Math.Round(_TotalActualValueSalesMKTExpense) / 1000000000, 1))
                                .Replace("@PctActualSalesMarketingExpenses", string.Format("{0:F2} %", __PctActualSalesMktExpense))
                                .Replace("@URLActualSalesMktExpense", URLActualSalesMktExpense)
                                .Replace("@URLScheduleCollection", URLScheduleCollection)
                                .Replace("@URLActualCostContract", URLActualCostContract);



                ltView.Text = message;
            }
        }
    }
}