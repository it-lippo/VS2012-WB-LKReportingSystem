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
    public partial class ViewSertipikatAkanJatuhTempo : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ViewSertipikatAkanJatuhTempo));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                log.Info("ViewSertipikatAkanJatuhTempo Page_Load.");

                string username = Request.QueryString["usr"];

                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dtSertipikat = new DataTable();

                log.DebugFormat("Call GetSertipikatAkanJatuhTempo() Started.");

                dtSertipikat = ws.GetSertipikatAkanJatuhTempo();

                log.DebugFormat("Call GetSertipikatAkanJatuhTempo() Finished. Result {0} rows.", dtSertipikat.Rows.Count);

                string html = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/SertipikatAkanJatuhTempo.html"));

                if (dtSertipikat.Rows.Count > 0)
                {
                    string HTMLContent = "";

                    HTMLContent += @"<div style='page-break-after: always;'>
                                        <table style='font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
	                                        <tr style='font-family: Calibri; font-size: 16px; color: black; text-align: center'>
		                                        <td colspan='11'><b>Sertipikat Akan Jatuh Tempo</b></td>
	                                        </tr>
	
	                                        <tr style='font-family: Calibri; font-size: 12px; color: black; text-align: center'>
		                                        <td colspan='21'><b>@AsOf</b></td>
	                                        </tr>

	                                        <tr style='height:20px'>
		                                        <td colspan='11'></td>
	                                        </tr>

                                            <tr>
	                                            <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Kode</td>
	                                            <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Aset</td>
	                                            <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nomor Sertipikat</td>
	                                            <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Tanggal Penerbitan</td>
	                                            <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Tanggal Berakhir Hak</td>
	                                            <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Sisa Hari Berlaku</td>
	                                            <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Alamat Sertipikat</td>
	                                            <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Luas Tanah Fisik</td>
	                                            <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Luas Tanah Sertipikat</td>
	                                            <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Jenis Hak Atas Tanah</td>
	                                            <td style='width:200px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Pemegang Hak</td>
                                            </tr>";

                    for (int i = 1; i < dtSertipikat.Rows.Count; i++)
                    {
                        HTMLContent += string.Format(@"<tr>
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

                            </tr>", dtSertipikat.Rows[i-1]["Kode"].ToString(),
                                        dtSertipikat.Rows[i - 1]["Nama Aset"].ToString(),
                                        dtSertipikat.Rows[i - 1]["Nomor Sertipikat"].ToString(),
                                        dtSertipikat.Rows[i - 1]["Tanggal Penerbitan"].ToString(),
                                        dtSertipikat.Rows[i - 1]["Tanggal Berakhir Hak"].ToString(),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i - 1]["Sisa Hari Berlaku"].ToString())),
                                        dtSertipikat.Rows[i - 1]["Alamat Sertipikat"].ToString(),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i - 1]["Luas Tanah Fisik"].ToString())),
                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i - 1]["Luas Tanah Sertipikat"].ToString())),
                                        dtSertipikat.Rows[i - 1]["Jenis Hak Atas Tanah"].ToString(),
                                        dtSertipikat.Rows[i - 1]["Nama Pemegang Hak"].ToString());

                        if (i % 50 == 0)
                        {
                            HTMLContent += @"   </table>
                                             </div>

                                             <div style='page-break-after: always;'>
                                                <table style='font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
	                                                <tr style='font-family: Calibri; font-size: 16px; color: black; text-align: center'>
		                                                <td colspan='11'><b>Sertipikat Akan Jatuh Tempo</b></td>
	                                                </tr>
	
	                                                <tr style='font-family: Calibri; font-size: 12px; color: black; text-align: center'>
		                                                <td colspan='21'><b>@AsOf</b></td>
	                                                </tr>

	                                                <tr style='height:20px'>
		                                                <td colspan='11'></td>
	                                                </tr>

                                                    <tr>
	                                                    <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Kode</td>
	                                                    <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Aset</td>
	                                                    <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nomor Sertipikat</td>
	                                                    <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Tanggal Penerbitan</td>
	                                                    <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Tanggal Berakhir Hak</td>
	                                                    <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Sisa Hari Berlaku</td>
	                                                    <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Alamat Sertipikat</td>
	                                                    <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Luas Tanah Fisik</td>
	                                                    <td style='width:100px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Luas Tanah Sertipikat</td>
	                                                    <td style='width:150px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Jenis Hak Atas Tanah</td>
	                                                    <td style='width:200px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Pemegang Hak</td>
                                                    </tr>";
                        }
                    }

                    HTMLContent += @"<table style='font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
                                        <tr style='height:20px'>
                                            <td colspan='11'></td>
                                        </tr>
                
                                        <tr style='font-family: Calibri; font-size: 10px; color: black; text-align: center'>
                                            <td colspan='11'>-- End of Report --</td>
                                        </tr>

                                        <tr style='font-family: Calibri; font-size: 10px; color: black; text-align: left'>
                                            <td colspan='11'>Printed by : @PrintedBy</td>
                                        </tr>
                                    </table>";

                    html = html.Replace("@ContentReport", HTMLContent);
                }
                else
                {
                    html = html.Replace("@ContentReport", "");
                }

                 html = html.Replace("@PrintedBy", username + " " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"))
                    .Replace("@AsOf", "As of " + DateTime.Now.ToString("dd/MM/yyyy"));

//                if (dtSertipikat.Rows.Count > 0)
//                {
//                    string HTMLContentReport = "";

//                    for (int i = 0; i < dtSertipikat.Rows.Count; i++)
//                    {
//                        HTMLContentReport += string.Format(@"<tr>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{2}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{3}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{4}</td>
//                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{5}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{6}</td>
//                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{7}</td>
//                            <td style='text-align: right; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{8}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{9}</td>
//                            <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{10}</td>
//
//                            </tr>", dtSertipikat.Rows[i]["Kode"].ToString(),
//                                        dtSertipikat.Rows[i]["Nama Aset"].ToString(),
//                                        dtSertipikat.Rows[i]["Nomor Sertipikat"].ToString(),
//                                        dtSertipikat.Rows[i]["Tanggal Penerbitan"].ToString(),
//                                        dtSertipikat.Rows[i]["Tanggal Berakhir Hak"].ToString(),
//                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i]["Sisa Hari Berlaku"].ToString())),
//                                        dtSertipikat.Rows[i]["Alamat Sertipikat"].ToString(),
//                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i]["Luas Tanah Fisik"].ToString())),
//                                        string.Format("{0:n0}", Convert.ToDecimal(dtSertipikat.Rows[i]["Luas Tanah Sertipikat"].ToString())),
//                                        dtSertipikat.Rows[i]["Jenis Hak Atas Tanah"].ToString(),
//                                        dtSertipikat.Rows[i]["Nama Pemegang Hak"].ToString());
//                    }

//                    html = html.Replace("@ContentReport", HTMLContentReport);
//                }
//                else
//                {
//                    html = html.Replace("@ContentReport", "");
//                }

//                html = html.Replace("@PrintedBy", username + " " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"))
//                    .Replace("@AsOf", "As of " + DateTime.Now.ToString("dd/MM/yyyy"));

                ltView.Text = html;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            } 
        }
    }
}