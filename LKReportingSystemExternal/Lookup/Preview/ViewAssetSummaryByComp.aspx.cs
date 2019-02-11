using LKReportingSystemExternal.Class;
using LKReportingSystemExternal.Forms;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKReportingSystemExternal.Lookup.Preview
{
    public partial class ViewAssetSummaryByComp : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ViewAssetSummaryByComp));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                log.Info("ViewAssetSummaryByComp Page_Load.");

                string company = "";
                string province = "";
                string username = Request.QueryString["usr"];
                int id = Convert.ToInt32(Request.QueryString["id"]);

                decimal totalAsset = 0;
                decimal totalSertipikat = 0;
                decimal totalGirik = 0;
                decimal totalLainnya = 0;
                decimal totalArea = 0;

                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dtParameter = new DataTable();

                log.DebugFormat("Call GetReportParameter() Started. Parameters : id = {0}", id);

                dtParameter = ws.GetReportParameter(id);

                if (dtParameter.Rows.Count > 0)
                {
                    company = dtParameter.Rows[0]["Parameter1"].ToString();
                    province = dtParameter.Rows[0]["Parameter2"].ToString();
                }

                log.DebugFormat("Call GetReportParameter() Finished. Result {0} rows.", dtParameter.Rows.Count);

                DataTable dtAsset = new DataTable();

                log.DebugFormat("Call GetAssetSummaryByComp() Started. Parameters : Company = {0}, Province = {0}", company, province);

                dtAsset = ws.GetAssetSummaryByComp(company, province);

                log.DebugFormat("Call GetAssetSummaryByComp() Finished. Result {0} rows.", dtAsset.Rows.Count);

                string html = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/AssetSummaryByComp.html"));

                if (dtAsset.Rows.Count > 0)
                {
                    string HTMLContentReport = "";

                    for (int i = 0; i < dtAsset.Rows.Count; i++)
                    {

                        HTMLContentReport += string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{7}</td>

                            </tr>", dtAsset.Rows[i]["CompanyName"].ToString(),
                                        dtAsset.Rows[i]["ProvinsiName"].ToString(),
                                        dtAsset.Rows[i]["AsetTypeName"].ToString(),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAsset.Rows[i]["hasAsset"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAsset.Rows[i]["hasSertipikat"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAsset.Rows[i]["hasGirik"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAsset.Rows[i]["Lainnya"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtAsset.Rows[i]["LuasTanah"].ToString())));

                        totalAsset += Convert.ToDecimal(dtAsset.Rows[i]["hasAsset"].ToString());
                        totalSertipikat += Convert.ToDecimal(dtAsset.Rows[i]["hasSertipikat"].ToString());
                        totalGirik += Convert.ToDecimal(dtAsset.Rows[i]["hasGirik"].ToString());
                        totalLainnya += Convert.ToDecimal(dtAsset.Rows[i]["Lainnya"].ToString());
                        totalArea += Convert.ToDecimal(dtAsset.Rows[i]["LuasTanah"].ToString());
                    }

                    //Total
                    HTMLContentReport += string.Format(@"<tr>
                            <td colspan='3' style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{0}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{1}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{2}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{3}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{4}</b></td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>{5}</b></td>
                            </tr>", "Total",
                                    string.Format("{0:n0}", totalAsset),
                                    string.Format("{0:n0}", totalSertipikat),
                                    string.Format("{0:n0}", totalGirik),
                                    string.Format("{0:n0}", totalLainnya),
                                    string.Format("{0:n0}", totalArea));

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