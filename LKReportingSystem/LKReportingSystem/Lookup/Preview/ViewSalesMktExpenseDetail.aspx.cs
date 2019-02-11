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
    public partial class ViewSalesMktExpenseDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string batchid = Request.QueryString["batchid"].ToString();

                decimal _TotalPaidCommission = 0;
                decimal _TotalUnpaidCommission = 0;
                decimal _TotalPaidSalary = 0;
                decimal _TotalMarketingExpense = 0;

                DataTable _DT_CommisionInfo = new DataTable();
                _DT_CommisionInfo = clsSalesSummary.GetDataCommission(batchid);

                _TotalPaidCommission = decimal.Parse(_DT_CommisionInfo.Rows[0]["actualpaidcomm"].ToString());
                _TotalUnpaidCommission = decimal.Parse(_DT_CommisionInfo.Rows[0]["actualunpaidcomm"].ToString());

                //_TotalPaidSalary = clsSalesSummary.GetDataPaidSalary(batchid);

                DataTable dtSalesMkt = clsSalesSummary.GetDataBudgetMktDetail(batchid);
                DataTable dtSalary = clsSalesSummary.GetDataPaidSalaryDetail(batchid);

                if (dtSalesMkt.Rows.Count > 0 || dtSalary.Rows.Count > 0)
                {

                    string HTMLContent1 = "";
                    string HTMLContent2 = "";

                    string message = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/SalesMktExpense.html"));


                    for (int i = 0; i < dtSalary.Rows.Count; i++)
                    {

                        HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td class='expandSalary' style='text-align: center; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td class='expandSalary' style='text-align: center; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td class='expandSalary' style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                                             
                        </tr>", dtSalary.Rows[i]["salaryyear"].ToString(),
                                    dtSalary.Rows[i]["salarymonth"].ToString(),
                                    string.Format("{0:n0}", Convert.ToDecimal(dtSalary.Rows[i]["salaryvalue"].ToString()))
                                    );


                        _TotalPaidSalary = _TotalPaidSalary + Convert.ToDecimal(dtSalary.Rows[i]["salaryvalue"].ToString());

                    }

                    HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                                            <td colspan='2' class='defaultBGcolor expandSalary'></td>
                                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px' class='defaultBGcolor expandSalary'><b>{0}</b></td>                                            
                               
                                        </tr>", string.Format("{0:n0}", _TotalPaidSalary)
                   );


                    for (int i = 0; i < dtSalesMkt.Rows.Count; i++)
                    {

                        HTMLContent2 = HTMLContent2 + string.Format(@"<tr>
                            <td class='expandMarketing' style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td class='expandMarketing' style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td class='expandMarketing' style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td class='expandMarketing' style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td class='expandMarketing' style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{4}</td>                        
                            <td class='expandMarketing' style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{5}</td>
                    
                        </tr>", dtSalesMkt.Rows[i]["Project"].ToString(),
                                    dtSalesMkt.Rows[i]["Budget"].ToString(),
                                    dtSalesMkt.Rows[i]["Item"].ToString(),
                                    string.Format("{0:n0}", Convert.ToDecimal(dtSalesMkt.Rows[i]["Amount"].ToString())),
                                    dtSalesMkt.Rows[i]["InternalApproval"].ToString(),
                                    dtSalesMkt.Rows[i]["ApproveDate"].ToString()
                                    );


                        _TotalMarketingExpense = _TotalMarketingExpense + Convert.ToDecimal(dtSalesMkt.Rows[i]["Amount"].ToString());

                    }


                    HTMLContent2 = HTMLContent2 + string.Format(@"<tr>
                                            <td colspan='3' class='defaultBGcolor expandMarketing'></td>
                                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px' class='defaultBGcolor expandMarketing'><b>{0}</b></td>
                                            <td colspan='2' class='defaultBGcolor expandMarketing'></td>
                               
                                        </tr>", string.Format("{0:n0}", _TotalMarketingExpense)
                                   );



                    message = message.Replace("@ProjectName", dtSalesMkt.Rows[0]["projectName"].ToString())
                                     .Replace("@TotalMarketingExpense", string.Format("{0:n0}", _TotalMarketingExpense))
                                     .Replace("@TotalPaidCommission", string.Format("{0:n0}", _TotalPaidCommission))
                                     .Replace("@TotalUnpaidCommission", string.Format("{0:n0}", _TotalUnpaidCommission))
                                     .Replace("@TotalPaidSalary", string.Format("{0:n0}", _TotalPaidSalary))
                                     .Replace("@GrandTotal", string.Format("{0:n0}", _TotalMarketingExpense + _TotalPaidCommission + _TotalUnpaidCommission + _TotalPaidSalary))
                                     .Replace("@ContentReport1", HTMLContent1)
                                     .Replace("@ContentReport2", HTMLContent2);


                    ltView.Text = message;
                }

            }

        }

    }
}
