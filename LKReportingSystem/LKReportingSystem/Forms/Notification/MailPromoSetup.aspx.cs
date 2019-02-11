using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;
using log4net;

namespace LKReportingSystem.Forms.MailPromo
{
    public partial class MailPromoSetup : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(MailPromoSetup));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsSecurity.HaveAccess(this.AppRelativeVirtualPath))
            {
                htmlNotificationHistory.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To View this Page";

                htmlNotificationHistory.Attributes.Add("class", "alert alert-danger alert-dismissable");
                updatePanelHtmlNotificationHistory.Update();

                divContent.Visible = false;
                return;
            }
            else
            {
                if (!IsPostBack)
                {
                    BindDataPromo();
                }
            }
        }

        private void BindDataPromo()
        {
            try
            {
                string img1 = "";
                string img2 = "";
                string img3 = "";

                DataTable dt = clsMailPromo.GetDataPromo();

                var rawPromo = (from DataRow dRow in dt.Rows
                               where Convert.ToBoolean(dRow["isactive"].ToString()) == true &&
                                       dRow["promotype"].ToString().ToLower() == "a"
                                select dRow).Distinct().ToList();

                if (rawPromo.Count > 0)
                {
                    DataTable dtFiltered = rawPromo.CopyToDataTable();

                    txtTitlePromo1.Text = dtFiltered.Rows[0]["promotitle"].ToString();
                    txtStartDate1.Text = dtFiltered.Rows[0]["startdate"].ToString();
                    txtEndDate1.Text = dtFiltered.Rows[0]["enddate"].ToString();
                    txtURLPromo1.Text = dtFiltered.Rows[0]["promourl"].ToString();
                    string url1 = dtFiltered.Rows[0]["promoimage"].ToString();
                    string archiveUrl1 = dtFiltered.Rows[0]["promoarchiveimage"].ToString();

                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(url1);
                    img1 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                    hfImage1.Value = url1;
                    hfArchiveImage1.Value = archiveUrl1;

                    ViewState["MailPromo_Promo1"] = dtFiltered;
                }

                    
                var rawPromo2 = (from DataRow dRow in dt.Rows
                               where Convert.ToBoolean(dRow["isactive"].ToString()) == true &&
                                       dRow["promotype"].ToString().ToLower() == "b"
                                select dRow).Distinct().ToList();

                if (rawPromo2.Count > 0)
                {
                    DataTable dtFiltered = rawPromo2.CopyToDataTable();

                    txtTitlePromo2.Text = dtFiltered.Rows[0]["promotitle"].ToString();
                    txtStartDate2.Text = dtFiltered.Rows[0]["startdate"].ToString();
                    txtEndDate2.Text = dtFiltered.Rows[0]["enddate"].ToString();
                    txtURLPromo2.Text = dtFiltered.Rows[0]["promourl"].ToString();
                    string url2 = dtFiltered.Rows[0]["promoimage"].ToString();
                    string archiveUrl2 = dtFiltered.Rows[0]["promoarchiveimage"].ToString();

                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(url2);
                    img2 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                    hfImage2.Value = url2;
                    hfArchiveImage2.Value = archiveUrl2;

                    ViewState["MailPromo_Promo2"] = dtFiltered;
                }


                var rawPromo3 = (from DataRow dRow in dt.Rows
                                 where Convert.ToBoolean(dRow["isactive"].ToString()) == true &&
                                         dRow["promotype"].ToString().ToLower() == "c"
                                 select dRow).Distinct().ToList();

                if (rawPromo3.Count > 0)
                {
                    DataTable dtFiltered = rawPromo3.CopyToDataTable();

                    txtTitlePromo3.Text = dtFiltered.Rows[0]["promotitle"].ToString();
                    txtStartDate3.Text = dtFiltered.Rows[0]["startdate"].ToString();
                    txtEndDate3.Text = dtFiltered.Rows[0]["enddate"].ToString();
                    txtURLPromo3.Text = dtFiltered.Rows[0]["promourl"].ToString();
                    string url3 = dtFiltered.Rows[0]["promoimage"].ToString();
                    string archiveUrl3 = dtFiltered.Rows[0]["promoarchiveimage"].ToString();

                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(url3);
                    img3 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                    hfImage3.Value = url3;
                    hfArchiveImage3.Value = archiveUrl3;

                    ViewState["MailPromo_Promo3"] = dtFiltered;
                }


                ScriptManager.RegisterStartupScript(this, this.GetType(), "setup", "SetImage('" + img1 + "', '" + img2 + "', '" + img3 + "');", true);
              
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        private bool ValidatePromo1()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtTitlePromo1.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Title Promo 1 can not be empty.');", true);
                result = false;
            }

            if (string.IsNullOrEmpty(txtStartDate1.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Start Date Promo 1 can not be empty.');", true);
                result = false;
            }

            if (string.IsNullOrEmpty(txtEndDate1.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('End Date Promo 1 can not be empty.');", true);
                result = false;
            }

            if (!fuImage1.HasFile && string.IsNullOrEmpty(hfImage1.Value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Image Promo 1 can not be empty.');", true);
                result = false;
            }

            DataTable dt = (DataTable) ViewState["MailPromo_Promo1"];

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["promotitle"].ToString() == txtTitlePromo1.Text && dt.Rows[0]["promourl"].ToString() == txtURLPromo1.Text &&
                    Convert.ToDateTime(dt.Rows[0]["startdate"].ToString()).ToString("dd/MM/yyyy") == Convert.ToDateTime(txtStartDate1.Text).ToString("dd/MM/yyyy") &&
                    Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToString("dd/MM/yyyy") == Convert.ToDateTime(txtEndDate1.Text).ToString("dd/MM/yyyy") &&
                    !fuImage1.HasFile)
                {
                    result = false;
                }
            }

            return result;
        }

        private bool ValidatePromo2()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtTitlePromo2.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Title Promo 2 can not be empty.');", true);
                result = false;
            }

            if (string.IsNullOrEmpty(txtStartDate2.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Start Date Promo 2 can not be empty.');", true);
                result = false;
            }

            if (string.IsNullOrEmpty(txtEndDate2.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('End Date Promo 2 can not be empty.');", true);
                result = false;
            }

            if (!fuImage2.HasFile && string.IsNullOrEmpty(hfImage2.Value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Image Promo 2 can not be empty.');", true);
                result = false;
            }

            DataTable dt = (DataTable)ViewState["MailPromo_Promo2"];

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["promotitle"].ToString() == txtTitlePromo2.Text && dt.Rows[0]["promourl"].ToString() == txtURLPromo2.Text &&
                    Convert.ToDateTime(dt.Rows[0]["startdate"].ToString()).ToString("dd/MM/yyyy") == Convert.ToDateTime(txtStartDate2.Text).ToString("dd/MM/yyyy") &&
                    Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToString("dd/MM/yyyy") == Convert.ToDateTime(txtEndDate2.Text).ToString("dd/MM/yyyy") &&
                    !fuImage2.HasFile)
                {
                    result = false;
                }
            }

            return result;
        }

        private bool ValidatePromo3()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtTitlePromo3.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Title Promo 3 can not be empty.');", true);
                result = false;
            }

            if (string.IsNullOrEmpty(txtStartDate3.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Start Date Promo 3 can not be empty.');", true);
                result = false;
            }

            if (string.IsNullOrEmpty(txtEndDate3.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('End Date Promo 3 can not be empty.');", true);
                result = false;
            }

            if (!fuImage3.HasFile && string.IsNullOrEmpty(hfImage3.Value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Image Promo 3 can not be empty.');", true);
                result = false;
            }

            DataTable dt = (DataTable)ViewState["MailPromo_Promo3"];

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["promotitle"].ToString() == txtTitlePromo3.Text && dt.Rows[0]["promourl"].ToString() == txtURLPromo3.Text &&
                    Convert.ToDateTime(dt.Rows[0]["startdate"].ToString()).ToString("dd/MM/yyyy") == Convert.ToDateTime(txtStartDate3.Text).ToString("dd/MM/yyyy") &&
                    Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToString("dd/MM/yyyy") == Convert.ToDateTime(txtEndDate3.Text).ToString("dd/MM/yyyy") &&
                    !fuImage3.HasFile)
                {
                    result = false;
                }
            }

            return result;
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

        private void SaveJpeg(string path, System.Drawing.Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        private bool SaveImageToServer(string filename, string path, byte[] image)
        {
            bool result = true;

            try
            {
                MemoryStream ms = new MemoryStream(image);
                System.Drawing.Image myImage = System.Drawing.Image.FromStream(ms);

                if (File.Exists(path)) File.Delete(path);

                if (!File.Exists(path)) { SaveJpeg(path, myImage, 100); }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
                
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //process Promo 1
                if (ValidatePromo1() == true)
                {
                    log.DebugFormat("Start Save Mail Promo 1");

                    bool result = true;
                    string imagePath = "";
                    string archiveImagePath = "";

                    if (fuImage1.HasFile)
                    {
                        byte[] img = fuImage1.FileBytes;

                        archiveImagePath = txtTitlePromo1.Text + "_A_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".png";
                        string archivePath = Constants.pathImgArcPromoLiberty + archiveImagePath;

                        result = SaveImageToServer(archiveImagePath, archivePath, img);

                        log.DebugFormat("Result save image Mail promo 1 : {0}", result);

                        if (result == true)
                        {
                            log.DebugFormat("Save Image Mail Promo 1 to Server success");

                            imagePath = "Check1.png";
                            string path = Constants.pathImgPromoLiberty + imagePath;

                            result = SaveImageToServer(imagePath, path, img);

                        }
                    }
                    else
                    {
                        imagePath = hfImage1.Value;
                        archiveImagePath = hfArchiveImage1.Value;
                    }

                    if (result == true)
                    {
                        DateTime startdate = DateTime.ParseExact(txtStartDate1.Text, "dd/MM/yyyy", null);
                        DateTime enddate = DateTime.ParseExact(txtEndDate1.Text, "dd/MM/yyyy", null);

                        imagePath = imagePath.Replace(Constants.urlLKUploader + "MailPromo/", "");
                        archiveImagePath = archiveImagePath.Replace(Constants.urlLKUploader + "MailPromo/Archive/", "");

                        //save new
                        string sResult = clsMailPromo.InsertDataSetupPromo("A", txtTitlePromo1.Text, txtURLPromo1.Text, startdate, enddate, 
                                Constants.urlLKUploader + "MailPromo/" + imagePath, Constants.urlLKUploader + "MailPromo/Archive/" + archiveImagePath, Helper.GetLoginName);

                        if (sResult == "")
                        {
                            log.DebugFormat("Success Save Mail Promo 1");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info1", "BootboxAlert('<strong>Success</strong> Advertisement 1 has been updated to all Email Notification.');", true);
                        }
                        else
                        {
                            log.DebugFormat("Failed Save Mail Promo 1. {0}", sResult);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info1", "BootboxAlert('<strong>Failed</strong> " + sResult + "');", true);
                        }
                    }

                    log.DebugFormat("End Save Mail Promo 1");
                }

                if (ValidatePromo2() == true)
                {
                    log.DebugFormat("Start Save Mail Promo 2");

                    bool result = true;
                    string imagePath = "";
                    string archiveImagePath = "";

                    if (fuImage2.HasFile)
                    {
                        byte[] img = fuImage2.FileBytes;

                        archiveImagePath = txtTitlePromo2.Text + "_B_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".png";
                        string archivePath = Constants.pathImgArcPromoLiberty + archiveImagePath;

                        result = SaveImageToServer(archiveImagePath, archivePath, img);

                        if (result == true)
                        {
                            log.DebugFormat("Save Image Mail Promo 2 to Server success");

                            imagePath = "Check2.png";
                            string path = Constants.pathImgPromoLiberty + imagePath;

                            result = SaveImageToServer(imagePath, path, img);

                        }
                    }
                    else
                    {
                        imagePath = hfImage2.Value;
                        archiveImagePath = hfArchiveImage2.Value;
                    }

                    if (result == true)
                    {
                        DateTime startdate = DateTime.ParseExact(txtStartDate2.Text, "dd/MM/yyyy", null);
                        DateTime enddate = DateTime.ParseExact(txtEndDate2.Text, "dd/MM/yyyy", null);

                        //save new
                        string sResult = clsMailPromo.InsertDataSetupPromo("B", txtTitlePromo2.Text, txtURLPromo2.Text, startdate, enddate,
                                    Constants.urlLKUploader + "MailPromo/" + imagePath, Constants.urlLKUploader + "MailPromo/Archive/" + archiveImagePath, Helper.GetLoginName);

                        if (sResult == "")
                        {
                            log.DebugFormat("Success Save Mail Promo 2");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info2", "BootboxAlert('<strong>Success</strong> Advertisement 2 has been updated to all Email Notification.');", true);
                        }
                        else
                        {
                            log.DebugFormat("Failed Save Mail Promo 2. {0}", sResult);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info2", "BootboxAlert('<strong>Failed</strong> " + sResult + "');", true);
                        }
                    }

                    log.DebugFormat("End Save Mail Promo 2");
                }

                if (ValidatePromo3() == true)
                {
                    log.DebugFormat("Start Save Mail Promo 3");

                    bool result = true;
                    string imagePath = "";
                    string archiveImagePath = "";

                    if (fuImage3.HasFile)
                    {
                        byte[] img = fuImage3.FileBytes;

                        archiveImagePath = txtTitlePromo3.Text + "_C_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".png";
                        string archivePath = Constants.pathImgArcPromoLiberty + archiveImagePath;

                        result = SaveImageToServer(archiveImagePath, archivePath, img);

                        if (result == true)
                        {
                            log.DebugFormat("Save Image Mail Promo 3 to Server success");

                            imagePath = "Check3.png";
                            string path = Constants.pathImgPromoLiberty + imagePath;

                            result = SaveImageToServer(imagePath, path, img);

                        }
                    }
                    else
                    {
                        imagePath = hfImage3.Value;
                        archiveImagePath = hfArchiveImage3.Value;
                    }

                    if (result == true)
                    {
                        DateTime startdate = DateTime.ParseExact(txtStartDate3.Text, "dd/MM/yyyy", null);
                        DateTime enddate = DateTime.ParseExact(txtEndDate3.Text, "dd/MM/yyyy", null);

                        log.DebugFormat("Start save data Mail Promo 3 to db ");

                        //save new
                        string sResult = clsMailPromo.InsertDataSetupPromo("C", txtTitlePromo3.Text, txtURLPromo3.Text, startdate, enddate,
                                Constants.urlLKUploader + "MailPromo/" + imagePath, Constants.urlLKUploader + "MailPromo/Archive/" + archiveImagePath, Helper.GetLoginName);

                        if (sResult == "")
                        {
                            log.DebugFormat("Success Save Mail Promo 3");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info3", "BootboxAlert('<strong>Success</strong> Advertisement 3 has been updated to all Email Notification.');", true);
                        }
                        else
                        {
                            log.DebugFormat("Failed Save Mail Promo 3. {0}", sResult);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info3", "BootboxAlert('<strong>Failed</strong> " + sResult + "');", true);
                        }
                    }

                    log.DebugFormat("End Save Mail Promo 3");
                }


                BindDataPromo();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('<strong>There is an error:</strong> " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }
    }
}