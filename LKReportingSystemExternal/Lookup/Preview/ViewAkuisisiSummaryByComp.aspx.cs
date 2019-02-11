using LKReportingSystemExternal.Class;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LKReportingSystemExternal.Lookup.Preview
{
    public partial class ViewAkuisisiSummaryByComp : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ViewAkuisisiSummaryByComp));

        protected void Page_Load(object sender, EventArgs e)
        {
            log.Info("ViewAkuisisiSummaryByComp Page_Load.");

            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                string username = Request.QueryString["usr"];
                string company = "";

                decimal totalPerencanaan = 0;
                decimal totalAreaPerencanaan = 0;
                decimal totalPemeriksaan = 0;
                decimal totalAreaPemeriksaan = 0;
                decimal totalProses = 0;
                decimal totalAreaProses = 0;
                decimal totalSudahBayarProses = 0;
                decimal totalJatuhTempoProses = 0;
                decimal totalBelumJatuhTempoProses = 0;
                decimal totalNilaiProses = 0;
                decimal totalSelesai = 0;
                decimal totalAreaSelesai = 0;
                decimal totalSudahBayarSelesai = 0;
                decimal totalJatuhTempoSelesai = 0;
                decimal totalBelumJatuhTempoSelesai = 0;
                decimal totalNilaiSelesai = 0;
                decimal totalSeluruhAkuisisi = 0;
                decimal totalSeluruhArea = 0;
                decimal totalSeluruhNilai = 0;
                   
                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dtParameter = new DataTable();

                log.DebugFormat("Call GetReportParameter() Started. Parameters : id = {0}", id);

                dtParameter = ws.GetReportParameter(id);

                if (dtParameter.Rows.Count > 0)
                {
                    company = dtParameter.Rows[0]["Parameter1"].ToString();
                }

                log.DebugFormat("Call GetReportParameter() Finished. Result {0} rows.", dtParameter.Rows.Count);

                DataTable dtAkuisisi = new DataTable();

                log.DebugFormat("Call GetAkuisisiSummaryByComp() Started. Parameters : Company = {0}", company);

                dtAkuisisi = ws.GetAkuisisiSummaryByComp(company);

                log.DebugFormat("Call GetAkuisisiSummaryByComp() Finished. Result {0} rows.", dtAkuisisi.Rows.Count);

                string html = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/AkuisisiSummaryByComp.html"));

                if (dtAkuisisi.Rows.Count > 0)
                {
                    string HTMLContentReport = "";

                    int no = 1;

                    for (int i = 0; i < dtAkuisisi.Rows.Count; i++)
                    {

                        HTMLContentReport += string.Format(@"<tr>
                            <td style='text-align: center; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{0}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{1}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{3}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{4}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{5}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{6}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{7}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{8}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{9}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{10}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{11}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{12}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{13}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{14}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{15}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{16}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{17}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{18}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{19}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'>{20}</td>

                            </tr>", string.Format("{0:n0}", Convert.ToDecimal(no.ToString())),
                                        dtAkuisisi.Rows[i]["CompanyName"].ToString(),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalPerencanaan"].ToString())),
                                        string.Format("{0:n2}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaPerencanaan"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalPemeriksaan"].ToString())),
                                        string.Format("{0:n2}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaPemeriksaan"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalProses"].ToString())),
                                        string.Format("{0:n2}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaProses"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBayarProses"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalJatuhTempoProses"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBelumJatuhTempoProses"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalNilaiProses"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalSelesai"].ToString())),
                                        string.Format("{0:n2}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaSelesai"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBayarSelesai"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalJatuhTempoSelesai"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBelumJatuhTempoSelesai"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalNilaiSelesai"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAkuisisi"].ToString())),
                                        string.Format("{0:n2}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalArea"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalNilai"].ToString())));

                        totalPerencanaan += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalPerencanaan"].ToString());
                        totalAreaPerencanaan += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaPerencanaan"].ToString());
                        totalPemeriksaan += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalPemeriksaan"].ToString());
                        totalAreaPemeriksaan += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaPemeriksaan"].ToString());
                        totalProses += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalProses"].ToString());
                        totalAreaProses += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaProses"].ToString());
                        totalSudahBayarProses += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBayarProses"].ToString());
                        totalJatuhTempoProses += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalJatuhTempoProses"].ToString());
                        totalBelumJatuhTempoProses += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBelumJatuhTempoProses"].ToString());
                        totalNilaiProses += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalNilaiProses"].ToString());
                        totalSelesai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalSelesai"].ToString());
                        totalAreaSelesai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAreaSelesai"].ToString());
                        totalSudahBayarSelesai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBayarSelesai"].ToString());
                        totalJatuhTempoSelesai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalJatuhTempoSelesai"].ToString());
                        totalBelumJatuhTempoSelesai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalBelumJatuhTempoSelesai"].ToString());
                        totalNilaiSelesai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalNilaiSelesai"].ToString());
                        totalSeluruhAkuisisi += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalAkuisisi"].ToString());
                        totalSeluruhArea += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalArea"].ToString());
                        totalSeluruhNilai += Convert.ToDecimal(dtAkuisisi.Rows[i]["TotalNilai"].ToString());

                        no += 1;
                    }

                    //Total
                    HTMLContentReport += string.Format(@"<tr>
                            <td colspan='2' style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{0}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{1}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{2}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{3}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{4}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{5}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{6}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{7}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{8}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{9}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{10}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{11}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{12}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{13}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{14}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{15}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{16}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{17}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{18}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px;'><b>{19}</b></td>
                            </tr>", "Total",
                                    string.Format("{0:n0}", totalPerencanaan),
                                    string.Format("{0:n2}", totalAreaPerencanaan),
                                    string.Format("{0:n0}", totalPemeriksaan),
                                    string.Format("{0:n2}", totalAreaPemeriksaan),
                                    string.Format("{0:n0}", totalProses),
                                    string.Format("{0:n2}", totalAreaProses),
                                    string.Format("{0:n0}", totalSudahBayarProses),
                                    string.Format("{0:n0}", totalJatuhTempoProses),
                                    string.Format("{0:n0}", totalBelumJatuhTempoProses),
                                    string.Format("{0:n0}", totalNilaiProses),
                                    string.Format("{0:n0}", totalSelesai),
                                    string.Format("{0:n2}", totalAreaSelesai),
                                    string.Format("{0:n0}", totalSudahBayarSelesai),
                                    string.Format("{0:n0}", totalJatuhTempoSelesai),
                                    string.Format("{0:n0}", totalBelumJatuhTempoSelesai),
                                    string.Format("{0:n0}", totalNilaiSelesai),
                                    string.Format("{0:n0}", totalSeluruhAkuisisi),
                                    string.Format("{0:n2}", totalSeluruhArea),
                                    string.Format("{0:n0}", totalSeluruhNilai));

                    html = html.Replace("@ContentReport", HTMLContentReport);
                }
                else
                {
                    html = html.Replace("@ContentReport", "");
                }

                html = html.Replace("@PrintedBy", username + " " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"))
                    .Replace("@AsOf", "As of " + DateTime.Now.ToString("dd/MM/yyyy"));

                ltView.Text = html;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}