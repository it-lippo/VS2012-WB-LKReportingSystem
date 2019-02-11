using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;
using LKReportingSystem.Class.Services;
using log4net;
using System.IO;


namespace LKReportingSystem.Services    
{
    /// <summary>
    /// Summary description for ManagementReportServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ManagementReportServices : System.Web.Services.WebService
    {

        protected static readonly ILog log = LogManager.GetLogger(typeof(ManagementReportServices));


        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}


        [WebMethod]
        public string GenerateManagementReport()
        {
            log.Info("GenerateManagementReport() service INVOKED.. ");

            string result = "";

            clsManagementReportGenerator objGenerateReport = new clsManagementReportGenerator();

            if (objGenerateReport.ValidateHasProjectData())
            {
                objGenerateReport.RunGenerateReport();
            }


            log.Info("GenerateManagementReport() service FINISHED.. ");
            return result;
        }

        [WebMethod]
        public string GenerateAndSendProjectSummaryPDF() 
        {
            log.Info("GenerateProjectSummaryPDF() service INVOKED.. ");
            string filepath = "";

            filepath = clsPDFGenerator.generateProjectSummaryPDF();

            string filename = Path.GetFileName(filepath);

            byte[] file = System.IO.File.ReadAllBytes(filepath);

            string result = clsPDFGenerator.sendProjectSummaryEmail(file, filename);

            log.Info("GenerateProjectSummaryPDF() service FINISHED.. ");
            return result;
        }
    }
}
