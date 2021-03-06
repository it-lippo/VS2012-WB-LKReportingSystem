﻿using log4net;
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
    public partial class ViewSertipikatSudahJatuhTempo : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ViewSertipikatSudahJatuhTempo));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                log.Info("ViewSertipikatSudahJatuhTempo Page_Load.");

                string username = Request.QueryString["usr"];

                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dtSertipikat = new DataTable();

                log.DebugFormat("Call GetSertipikatSudahJatuhTempo() Started.");

                dtSertipikat = ws.GetSertipikatSudahJatuhTempo();

                log.DebugFormat("Call GetSertipikatSudahJatuhTempo() Finished. Result {0} rows.", dtSertipikat.Rows.Count);

                string html = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/SertipikatSudahJatuhTempo.html"));

                if (dtSertipikat.Rows.Count > 0)
                {
                    string HTMLContentReport = "";

                    for (int i = 0; i < dtSertipikat.Rows.Count; i++)
                    {
                        HTMLContentReport += string.Format(@"<tr>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{7}</td>
                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{8}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{9}</td>
                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{10}</td>

                            </tr>", dtSertipikat.Rows[i]["Kode"].ToString(),
                                        dtSertipikat.Rows[i]["Nama Aset"].ToString(),
                                        dtSertipikat.Rows[i]["Nomor Sertipikat"].ToString(),
                                        dtSertipikat.Rows[i]["Tanggal Penerbitan"].ToString(),
                                        dtSertipikat.Rows[i]["Tanggal Berakhir Hak"].ToString(),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i]["Lewat Hari"].ToString())),
                                        dtSertipikat.Rows[i]["Alamat Sertipikat"].ToString(),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i]["Luas Tanah Fisik"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i]["Luas Tanah Sertipikat"].ToString())),
                                        dtSertipikat.Rows[i]["Jenis Hak Atas Tanah"].ToString(),
                                        dtSertipikat.Rows[i]["Nama Pemegang Hak"].ToString());
                    }

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