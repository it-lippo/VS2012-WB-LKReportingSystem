using LKReportingSystemExternal.Class;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LKReportingSystemExternal.Forms
{
    public partial class AssetSummaryByComp : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(AssetSummaryByComp));
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Constants.sessionUsername == "")
            {
                Response.Redirect(Page.ResolveClientUrl("~/Login.aspx"));
            }

            log.Info("AssetSummaryByComp Page_Load().");

            //if (!clsSecurity.HaveAccess(this.AppRelativeVirtualPath))
            //{
            //    log.InfoFormat("User {0} have no permission.", Constants.sessionUsername);

            //    htmlNotifMain.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
            //            + "<i class=\"fa fa-info-circle\"> </i>"
            //            + "<Strong> Warning! </Strong> You Have No Permission To View this Page";

            //    htmlNotifMain.Attributes.Add("class", "alert alert-danger alert-dismissable");
            //    upHtmlNotifMain.Update();

            //    divContent.Visible = false;
            //    return;
            //}

            if (!IsPostBack)
            {
                BindDataCompany();
                BindDataProvince();
            }
        }

        private void BindDataCompany()
        { 
            log4net.Config.XmlConfigurator.Configure();
            log.Info("BindDataCompany() Started..");

            try
            {
                lbxCompany.Items.Clear();

                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dt = ws.GetCompany();

                lbxCompany.DataSource = dt;
                lbxCompany.DataTextField = "Nama";
                lbxCompany.DataValueField = "Id";
                lbxCompany.DataBind();
            }
            catch (Exception ex)
            {
                log.Info("BindDataCompany() Error. " + ex.Message);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true);
            }

            log.Info("BindDataCompany() Finished..");
        }
         
        private void BindDataProvince()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("BindDataProvince() Started..");

            try
            {
                lbxProvince.Items.Clear();

                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                DataTable dt = ws.GetProvince();

                lbxProvince.DataSource = dt;
                lbxProvince.DataTextField = "Nama";
                lbxProvince.DataValueField = "Id";
                lbxProvince.DataBind();
            }
            catch (Exception ex)
            {
                log.Info("BindDataProvince() Error. " + ex.Message);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "bootbox.alert({message:'<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "', title:'Oops!'});", true);
            }

            log.Info("BindDataProvince() Finished..");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

            string id = ws.InsertReportParameter(GetListBoxItem(lbxCompany), GetListBoxItem(lbxProvince), "", Constants.sessionUsername);

            Response.Redirect(Page.ResolveUrl("~/Lookup/Preview/ViewAssetSummaryByComp.aspx?id=" + id + "&usr=" + Constants.sessionUsername));
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                //after publish need to change it
                string rooturl = Constants.rootURL;

                SIMAWS.WS_SIMA ws = new SIMAWS.WS_SIMA();

                string id = ws.InsertReportParameter(GetListBoxItem(lbxCompany), GetListBoxItem(lbxProvince), "", Constants.sessionUsername);

                string url = rooturl + "Lookup/Preview/ViewAssetSummaryByComp.aspx?id=" + id + "&usr=" + Constants.sessionUsername;

                string fileConfig = "AssetSummaryByCompany_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

                string filename = "\"" + Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig) + "\"";

                if (File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig)))
                {
                    File.Delete(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig));
                }

                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "exe\\") + "wkhtmltopdf.exe";
                proc.StartInfo.Arguments = "--print-media-type --page-width 250mm --page-height 323mm --margin-top 0 --margin-right 0 --margin-bottom 0 --margin-left 0 --dpi 200 --header-spacing 0 --footer-spacing 0  --disable-smart-shrinking --zoom 1.33 " + url + "  " + filename;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit();

                byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig));

                Page.Response.ContentType = "application/pdf";
                Page.Response.AddHeader("content-disposition", "attachment;filename=" + fileConfig);
                Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Page.Response.BinaryWrite(bytes);

                if (File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig)))
                {
                    File.Delete(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PDF\\" + fileConfig));
                }

                log.Info("Download " + fileConfig + " finished.");

                Page.Response.End();
            }
            catch (Exception ex)
            {
                log.Info("Download error. " + ex.Message);
            } 
        }

        private string GetListBoxItem(ListBox lbx)
        {
            string result = "";
            int i = 0;

            foreach (ListItem li in lbx.Items)
            {
                if (li.Selected)
                {
                    if (i > 0)
                    {
                        result += ",";
                    }

                    result += "'" + li.Value + "'";
                    i += 1;
                }
            }

            return result;
        }
    }
}