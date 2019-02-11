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
    public partial class ViewActualCostContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string batchid = Request.QueryString["batchid"].ToString();

            DataTable dt = clsSalesSummary.GetCostContractDetail(batchid);

            if (dt.Rows.Count > 0)
            {

                string HTMLContent1 = "";

                string message = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/ActualCostContract.html"));

                decimal _TotalContract = 0;
                decimal _TotalContractWithApportionment = 0;


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{3}</td>                            
     
                    
                        </tr>", dt.Rows[i]["projectcode"].ToString(),
                                dt.Rows[i]["wbsname"].ToString(),
                                dt.Rows[i]["currcode"].ToString(),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["currrate"].ToString()) * Convert.ToDecimal(dt.Rows[i]["directcostwithcostapportionment"].ToString())));

                    //_TotalContract = _TotalContract + (Convert.ToDecimal(dt.Rows[i]["currrate"].ToString()) * Convert.ToDecimal(dt.Rows[i]["contractamount"].ToString()));
                    _TotalContractWithApportionment = _TotalContractWithApportionment + (Convert.ToDecimal(dt.Rows[i]["currrate"].ToString()) * Convert.ToDecimal(dt.Rows[i]["directcostwithcostapportionment"].ToString()));

                }

                HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td colspan='3' class='defaultBGcolor'></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px' class='defaultBGcolor'><b>{0}</b></td>                 
               
                        </tr>", string.Format("{0:n0}", _TotalContractWithApportionment)
                              );



                message = message.Replace("@ProjectName", dt.Rows[0]["projectName"].ToString())
                                .Replace("@ContentReport1", HTMLContent1);


                ltView.Text = message;
            }
        }
    }
}