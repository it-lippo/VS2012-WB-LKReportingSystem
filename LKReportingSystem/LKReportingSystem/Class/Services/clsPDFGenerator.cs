using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using log4net;
using System.Configuration;
using System.Diagnostics;
using LKReportingSystem.Class;

namespace LKReportingSystem.Class.Services
{
    public class clsPDFGenerator
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsManagementReportGenerator));

        public static string generateProjectSummaryPDF() {
           
            string result = "";

            try
            {
                //after publish need to change it
                string rooturl = Constants.rootURL;
                string url = rooturl + "Lookup/Preview/ProjectSummary.aspx?isGenerate=1";

                string fileConfig = "ProjectSummary_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

                string filename = "\"" + Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig) + "\"";

                if (File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig)))
                {
                    File.Delete(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig));
                }

                result = filename;
                result = result.Replace("\"", "");

                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "exe\\") + "wkhtmltopdf.exe";
                proc.StartInfo.Arguments = "--zoom 1 --orientation Landscape " + url + "  " + filename;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex) {
                result = ex.Message;
            }
            return result;

        }

        public static string InsertMailAttachment(string emailTo, string emailCC, string emailBCC, string subject, string message, string nik,
        string counterCode, string namaFile, byte[] image, string emailFrom, string emailFromDisplayName, string emailID)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("InsertMailAttachment() CALLED.. ");

            log.DebugFormat("Parameter values passed: \n " +
                            " emailTo = {0}, emailCC = {0}, emailBCC = {0}, subject = {0}, \n " +
                            " message = {0}, nik = {0}, counterCode = {0}, namaFile = {0}, \n " +
                            " emailFrom = {0}, emailFromDisplayName = {0}, emailID = {0} ",
                            emailTo, emailCC, emailBCC, subject, message, nik, counterCode, namaFile, emailFrom, emailFromDisplayName, emailID);

            using (SqlConnection conn = new SqlConnection(Constants.OthersAppDBConn))

                try
                {

                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "sp_SendMailToOutbox";
                    cmd.Parameters.Add(new SqlParameter("emailTo", emailTo));
                    cmd.Parameters.Add(new SqlParameter("emailCC", emailCC));
                    cmd.Parameters.Add(new SqlParameter("emailBCC", emailBCC));
                    cmd.Parameters.Add(new SqlParameter("emailSubject", subject));
                    cmd.Parameters.Add(new SqlParameter("emailMessage", message));
                    cmd.Parameters.Add(new SqlParameter("nik", string.Empty));
                    cmd.Parameters.Add(new SqlParameter("counterCode", counterCode));
                    cmd.Parameters.Add(new SqlParameter("namaFile", namaFile));

                    SqlParameter param = new SqlParameter("Attachment", SqlDbType.Image, image.Length);
                    param.Value = image;
                    cmd.Parameters.Add(param);
                    cmd.Parameters.Add(new SqlParameter("emailFrom", emailFrom));
                    cmd.Parameters.Add(new SqlParameter("emailFromDisplayName", emailFromDisplayName));
                    cmd.Parameters.Add(new SqlParameter("inputUN", ""));

                    log.Info("sp_SendMailToOutbox SQL Query SENT");
                    cmd.ExecuteNonQuery();

                    return "";
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Error. " + ex.Message);
                    return ex.Message;
                }
                finally
                {
                    log.Info("sp_SendMailToOutbox SQL Query RESPONDED");
                    conn.Close();
                }
        }


        public static string sendProjectSummaryEmail(byte[] file, string filename)
        {
            string result = "";

            string mailBody = "";

            mailBody = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Template/HTML/MailMessage.html"));


            result = InsertMailAttachment(Constants.emailTo, Constants.emailCC, Constants.emailBCC, "Project Summary Report As Of " + DateTime.Now.ToString("d MMM yyyy"),
               mailBody, "", "PropSystem", filename, file, Constants.emailFrom, Constants.emailFromDisplayName, "");

            return result;
        }
    }
}