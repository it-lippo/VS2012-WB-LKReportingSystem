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
    public partial class ViewAssetDocumentChecklist : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ViewAssetDocumentChecklist));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                log.Info("ViewAssetDocumentChecklist Page_Load.");

                int id = Convert.ToInt32(Request.QueryString["id"]);
                string username = Request.QueryString["usr"];
                string type = Request.QueryString["ty"];
                string namaAset = "";
                string kodeAset = "";
                string status = "";
                 
                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dtParameter = new DataTable();

                log.DebugFormat("Call GetReportParameter() Started. Parameters : id = {0}", id);

                dtParameter = ws.GetReportParameter(id);

                if (dtParameter.Rows.Count > 0)
                {
                    status = dtParameter.Rows[0]["Parameter1"].ToString();
                }

                log.DebugFormat("Call GetReportParameter() Finished. Result {0} rows.", dtParameter.Rows.Count);

                DataTable dtAsset = new DataTable();

                log.DebugFormat("Call GetAssetDocumentChecklist() Started.");

                dtAsset = ws.GetAssetDocumentChecklist(status);

                log.DebugFormat("Call GetAssetDocumentChecklist() Finished. Result {0} rows.", dtAsset.Rows.Count);

                string html = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/AssetDocumentChecklist.html"));
                string HTMLDokumen = "";
                
                if (dtAsset.Rows.Count > 0)
                {
                    if (type == "d")
                    {
                        string HTMLContent = "";

                        HTMLContent += @"<div style='page-break-after: always;'>
                                        <table style='width:100%; font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
	                                        <tr style='font-family: Calibri; font-size: 16px; color: black; text-align: center'>
		                                        <td colspan='2'><b>Asset Document Checklist</b></td>
	                                        </tr>
	
	                                        <tr style='font-family: Calibri; font-size: 12px; color: black; text-align: center'>
		                                        <td colspan='2'><b>@AsOf</b></td>
	                                        </tr>

	                                        <tr style='height:20px'>
		                                        <td colspan='2'></td>
	                                        </tr>

                                            <tr>
	                                            <td style='width:500px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Aset</td>
	                                            <td style='width:900px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Kekurangan</td>
                                            </tr>";

                        var DTAkuisisi = (from DataRow dRow in dtAsset.Rows
                                          select new
                                          {
                                              akuisisiID = dRow["AkuisisiID"],
                                              kodeAkuisisi = dRow["KodeAkuisisi"],
                                              statusAkuisisi = dRow["StatusAkuisisi"]
                                          }).Distinct().ToList();

                        int idx = 1;
                        int idxOri = 1;
                        bool pageBreak = false;

                        foreach (var akuisisi in DTAkuisisi)
                        {
                            var DTAssetID = (from DataRow dRow in dtAsset.Rows
                                             where dRow["AkuisisiID"].ToString() == akuisisi.akuisisiID.ToString()
                                             select new
                                             {
                                                 asetID = dRow["AsetID"],
                                                 akuisisiID = dRow["AkuisisiID"]
                                             }).Distinct().ToList();

                            foreach (var asetid in DTAssetID)
                            {
                                var DTAsset = (from DataRow dRow in dtAsset.Rows
                                               where dRow["AsetID"].ToString() == asetid.asetID.ToString()
                                               && dRow["AkuisisiID"].ToString() == asetid.akuisisiID.ToString()
                                               select new
                                               {
                                                   akuisisiID = dRow["AkuisisiID"],
                                                   kodeAkuisisi = dRow["KodeAkuisisi"],
                                                   kodeAset = dRow["KodeAset"],
                                                   namaAset = dRow["NamaAset"],
                                                   jenisDokumen = dRow["JenisDokumen"],
                                                   keterangan = dRow["Keterangan"]
                                               }).ToList();

                                idx++;

                                if (idx > 115 || (idxOri + DTAsset.Count()) > 115)
                                {
                                    pageBreak = true;

                                    idx = 1;
                                    idxOri = 1;
                                }

                                foreach (var asset in DTAsset)
                                {
                                    if (idx > 115)
                                    {
                                        pageBreak = true;
                                    }

                                    namaAset = asset.namaAset.ToString();
                                    kodeAset = asset.kodeAset.ToString();

                                    if (asset.keterangan.ToString() == "")
                                    {
                                        HTMLDokumen += asset.jenisDokumen.ToString().Trim() + "<br/>";
                                    }
                                    else
                                    {
                                        HTMLDokumen += asset.jenisDokumen.ToString().Trim() + " - <i>" + asset.keterangan.ToString().Trim() + "</i><br/>";
                                    }

                                    idx++;
                                }

                                idxOri = idx;

                                if (pageBreak)
                                {
                                    HTMLContent += @"   </table>
                                             </div>

                                             <div style='page-break-after: always;'>
                                                <table style='width:100%; font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
	                                                <tr style='font-family: Calibri; font-size: 16px; color: black; text-align: center'>
		                                                <td colspan='11'><b>Asset Document Checklist</b></td>
	                                                </tr>
	
	                                                <tr style='font-family: Calibri; font-size: 12px; color: black; text-align: center'>
		                                                <td colspan='21'><b>@AsOf</b></td>
	                                                </tr>

	                                                <tr style='height:20px'>
		                                                <td colspan='11'></td>
	                                                </tr>

                                                    <tr>
	                                                    <td style='width:500px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Aset</td>
	                                                    <td style='width:900px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Kekurangan</td>
                                                    </tr>";

                                    pageBreak = false;
                                }

                                HTMLContent += @"<tr bgcolor=#F7F7F7><td colspan=2 style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>Kode Akuisisi : " + akuisisi.kodeAkuisisi.ToString() + " - " + akuisisi.statusAkuisisi.ToString() + "</b></td></tr>";

                                HTMLContent += string.Format(@"<tr>                            
                                                             <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                                                             <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                                                           </tr>", namaAset, HTMLDokumen);

                                HTMLDokumen = "";
                            }
                        }

                        HTMLContent += @"<table style='width:100%; font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
                                        <tr style='height:20px'>
                                            <td></td>
                                        </tr>
                
                                        <tr style='font-family: Calibri; font-size: 10px; color: black; text-align: center'>
                                            <td>-- End of Report --</td>
                                        </tr>

                                        <tr style='font-family: Calibri; font-size: 10px; color: black; text-align: left'>
                                            <td>Printed by : @PrintedBy</td>
                                        </tr>
                                    </table>";

                        html = html.Replace("@ContentReport", HTMLContent);
                    }
                    else if (type == "v")
                    {
                        string HTMLContent = "";

                        HTMLContent += @"<div style='page-break-after: always;'>
                                        <table style='width:100%; font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
	                                        <tr style='font-family: Calibri; font-size: 16px; color: black; text-align: center'>
		                                        <td colspan='2'><b>Asset Document Checklist</b></td>
	                                        </tr>
	
	                                        <tr style='font-family: Calibri; font-size: 12px; color: black; text-align: center'>
		                                        <td colspan='2'><b>@AsOf</b></td>
	                                        </tr>

	                                        <tr style='height:20px'>
		                                        <td colspan='2'></td>
	                                        </tr>

                                            <tr>
	                                            <td style='width:500px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Nama Aset</td>
	                                            <td style='width:900px; text-align:center; background-color:#E0E0E0; font-weight:bold; padding:5px 15px 5px 15px; height:20px; border:.5pt solid black;'>Kekurangan</td>
                                            </tr>";

                        var DTAkuisisi = (from DataRow dRow in dtAsset.Rows
                                          select new
                                          {
                                              akuisisiID = dRow["AkuisisiID"],
                                              kodeAkuisisi = dRow["KodeAkuisisi"],
                                              statusAkuisisi = dRow["StatusAkuisisi"]
                                          }).Distinct().ToList();

                        foreach (var akuisisi in DTAkuisisi)
                        {
                            var DTAssetID = (from DataRow dRow in dtAsset.Rows
                                             where dRow["AkuisisiID"].ToString() == akuisisi.akuisisiID.ToString()
                                             select new
                                             {
                                                 asetID = dRow["AsetID"],
                                                 akuisisiID = dRow["AkuisisiID"]
                                             }).Distinct().ToList();

                            foreach (var asetid in DTAssetID)
                            {
                                var DTAsset = (from DataRow dRow in dtAsset.Rows
                                               where dRow["AsetID"].ToString() == asetid.asetID.ToString()
                                               && dRow["AkuisisiID"].ToString() == asetid.akuisisiID.ToString()
                                               select new
                                               {
                                                   akuisisiID = dRow["AkuisisiID"],
                                                   kodeAkuisisi = dRow["KodeAkuisisi"],
                                                   kodeAset = dRow["KodeAset"],
                                                   namaAset = dRow["NamaAset"],
                                                   jenisDokumen = dRow["JenisDokumen"],
                                                   keterangan = dRow["Keterangan"]
                                               }).ToList();

                                foreach (var asset in DTAsset)
                                {
                                    namaAset = asset.namaAset.ToString();
                                    kodeAset = asset.kodeAset.ToString();

                                    if (asset.keterangan.ToString() == "")
                                    {
                                        HTMLDokumen += asset.jenisDokumen.ToString().Trim() + "<br/>";
                                    }
                                    else
                                    {
                                        HTMLDokumen += asset.jenisDokumen.ToString().Trim() + " - <i>" + asset.keterangan.ToString().Trim() + "</i><br/>";
                                    }
                                }

                                HTMLContent += @"<tr bgcolor=#F7F7F7><td colspan=2 style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'><b>Kode Akuisisi : " + akuisisi.kodeAkuisisi.ToString() + " - " + akuisisi.statusAkuisisi.ToString() + "</b></td></tr>";

                                HTMLContent += string.Format(@"<tr>                            
                                                             <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{0}</td>
                                                             <td style='text-align: left; padding: 5px 15px 5px 15px; border: 0.5pt solid black; height: 20px; min-width: 50px'>{1}</td>
                                                           </tr>", namaAset, HTMLDokumen);

                                HTMLDokumen = "";
                            }
                        }

                        HTMLContent += @"</table>
                                         </div>

                                        <table style='width:100%; font-family: Arial; font-size: 11px; border-spacing: 0; border-collapse: collapse;'>
                                        <tr style='height:20px'>
                                            <td></td>
                                        </tr>
                
                                        <tr style='font-family: Calibri; font-size: 10px; color: black; text-align: center'>
                                            <td>-- End of Report --</td>
                                        </tr>

                                        <tr style='font-family: Calibri; font-size: 10px; color: black; text-align: left'>
                                            <td>Printed by : @PrintedBy</td>
                                        </tr>
                                        </table>";

                        html = html.Replace("@ContentReport", HTMLContent);
                    }
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