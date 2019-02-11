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
    public partial class ViewScheduleCollectionDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string batchid = Request.QueryString["batchid"].ToString();

            DataTable dt = clsSalesSummary.GetDataScheduleCollectionDetail(batchid);

            if (dt.Rows.Count > 0)
            {

                string HTMLContent1 = "";

                string message = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/ScheduleCollection.html"));

                decimal _TotalSchedule = 0;
                decimal _TotalCollection = 0;
                decimal _TotalOutstanding = 0;


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{4}</td>                        
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: .5pt solid black; height: 20px; min-width: 50px'>{5}</td>
                    
                        </tr>", dt.Rows[i]["paymentterm"].ToString(),
                                dt.Rows[i]["clustername"].ToString(),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["totalschedule"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["totalcollection"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["totaloutstanding"].ToString())),
                                string.Format("{0:n0}", Convert.ToDecimal(dt.Rows[i]["pctcollection"].ToString())));


                    _TotalSchedule = _TotalSchedule + Convert.ToDecimal(dt.Rows[i]["totalschedule"].ToString());
                    _TotalCollection = _TotalCollection + Convert.ToDecimal(dt.Rows[i]["totalcollection"].ToString());
                    _TotalOutstanding = _TotalOutstanding + Convert.ToDecimal(dt.Rows[i]["totaloutstanding"].ToString());


                }

                HTMLContent1 = HTMLContent1 + string.Format(@"<tr>
                            <td colspan='2' class='defaultBGcolor'></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px' class='defaultBGcolor'><b>{0}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px' class='defaultBGcolor'><b>{1}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0px solid #f5f5f5; height: 20px; min-width: 50px' class='defaultBGcolor'><b>{2}</b></td>
                            <td class='defaultBGcolor'></td>
               
                        </tr>", string.Format("{0:n0}", _TotalSchedule),
                                string.Format("{0:n0}", _TotalCollection),
                                string.Format("{0:n0}", _TotalOutstanding)
                              );



                message = message.Replace("@ProjectName", dt.Rows[0]["projectName"].ToString())
                                .Replace("@ContentReport1", HTMLContent1);


                ltView.Text = message;
            }
        }
    }
}