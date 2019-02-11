using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKReportingSystem.Template
{
    public partial class TemplateDownloader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string PropertyType = "";
            string FileName = "";
            PropertyType = Request.QueryString["Type"];

            if (PropertyType == "Cashflow")
                FileName = Server.MapPath("~/Template/Excel/TemplateCashFlow.xlsx");         

            //Write it back to the client
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TemplateCashflow.xlsx");
            Response.TransmitFile(FileName);
            Response.Flush();
            Response.End();

        }
    }
}