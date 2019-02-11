using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;
using Newtonsoft.Json;

namespace LKReportingSystem.Forms.MailPromo
{
    public partial class MailPromoSummaryReport : System.Web.UI.Page
    {
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
                    txtStartDate1.Text = DateTime.Now.ToString("M-yyyy");
                    txtEndDate1.Text = DateTime.Now.ToString("M-yyyy");

                    btnGenerate_Click(null, null);
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartDate1.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info", "BootboxAlert('Start Period Must be filled.');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtEndDate1.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info", "BootboxAlert('End Period Must be filled.');", true);
                return;
            }

            lblStartPeriod.Text = lblStartPeriod2.Text = lblStartPeriod3.Text = DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null).ToString("MMMM yyyy");
            lblEndPeriod.Text = lblEndPeriod2.Text = lblEndPeriod3.Text = DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null).ToString("MMMM yyyy");

            //BindDataReportSendPerAdsPerMonth();
            //BindDataReportClickPerAdsPerMonth();
            BindDataReportTotalClickAndSentPerMonth();
            //BindDataDetailSentPerAdsPerMonth();
            BindDataDetailClickedPerAdsPerMonth();
            BindDataDetailAds();
        }

        private void BindDataReportClickPerAdsPerMonth()
        {
            List<DataTable> dtList = new List<DataTable>();

            DataTable dtMain = clsMailPromo.GetDataSendPromo();

            var rawAds = (from DataRow dRow in dtMain.Rows
                          select new { promoid = dRow["promoid"], promotitle = dRow["promotitle"] }).Distinct().ToList();

            foreach (var a in rawAds)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("promotitle");
                dt.Columns.Add("monthperiod");
                dt.Columns.Add("yearperiod");
                dt.Columns.Add("totalclick");



                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["promoid"].ToString() == a.promoid.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null) 
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"], month = dRow["monthperiod"], year = dRow["yearperiod"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalclick"))
                                    }).Distinct().OrderBy(c => c.key.year).ThenBy(c => c.key.month).ToList();

                foreach (var c in rawDetailAds)
                {
                    dt.Rows.Add(c.key.promoid + " - " + c.key.promotitle, c.key.month, c.key.year, c.totalsend);
                }

                dtList.Add(dt);
            }

            var json = JsonConvert.SerializeObject(dtList);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info2", "CreateChartClickPerMonth(" + json.ToString() + ");", true);
        }

        private void BindDataReportSendPerAdsPerMonth()
        {
            List<DataTable> dtList = new List<DataTable>();

            DataTable dtMain = clsMailPromo.GetDataSendPromo();

            var rawAds = (from DataRow dRow in dtMain.Rows
                          select new { promoid = dRow["promoid"], promotitle = dRow["promotitle"] }).Distinct().ToList();

            foreach (var a in rawAds)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("promotitle");
                dt.Columns.Add("monthperiod");
                dt.Columns.Add("yearperiod");
                dt.Columns.Add("totalsend");

                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["promoid"].ToString() == a.promoid.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null) 
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"], month = dRow["monthperiod"], year = dRow["yearperiod"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalsend"))
                                    }).Distinct().OrderBy(c => c.key.year).ThenBy(c => c.key.month).ToList();

                foreach (var c in rawDetailAds)
                {
                    dt.Rows.Add(c.key.promotitle, c.key.month, c.key.year, c.totalsend);
                }

                dtList.Add(dt);
            }

            var json = JsonConvert.SerializeObject(dtList);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info", "CreateChartSendPerMonth(" + json.ToString() + ");", true);
        }

        private void BindDataReportTotalClickAndSentPerMonth()
        {
            List<DataTable> dtList = new List<DataTable>();

            DataTable dtMain = clsMailPromo.GetDataSendPromo();

            DataTable dt = new DataTable();
            dt.Columns.Add("monthperiod");
            dt.Columns.Add("yearperiod");
            dt.Columns.Add("totalclick");



            var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                where 
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                group dRow by new {month = dRow["monthperiod"], year = dRow["yearperiod"] } into g
                                select new
                                {
                                    key = g.Key,
                                    totalsend = g.Sum(x => x.Field<int>("totalclick"))
                                }).Distinct().ToList();

            foreach (var c in rawDetailAds)
            {
                dt.Rows.Add(c.key.month, c.key.year, c.totalsend);
            }

            dtList.Add(dt);


            List<DataTable> dtListSent = new List<DataTable>();
            DataTable dtSent = new DataTable();
            dtSent.Columns.Add("monthperiod");
            dtSent.Columns.Add("yearperiod");
            dtSent.Columns.Add("totalsend");



            var rawDetailSentAds = (from DataRow dRow in dtMain.Rows
                                where
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                group dRow by new { month = dRow["monthperiod"], year = dRow["yearperiod"] } into g
                                select new
                                {
                                    key = g.Key,
                                    totalsend = g.Sum(x => x.Field<int>("totalsend"))
                                }).Distinct().OrderBy(c => c.key.year).ThenBy(c => c.key.month).ToList();

            foreach (var c in rawDetailSentAds)
            {
                dtSent.Rows.Add(c.key.month, c.key.year, c.totalsend);
            }

            dtListSent.Add(dtSent);


            var json = JsonConvert.SerializeObject(dtList);
            var jsonSent = JsonConvert.SerializeObject(dtListSent);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info3", "CreateStackedChartClickAndSent(" + json.ToString() + ", " + jsonSent.ToString() + ");", true);
        }

        private void BindDataDetailSentPerAdsPerMonth()
        {
            DataTable dtMain = clsMailPromo.GetDataSendPromo();

            var rawAds = (from DataRow dRow in dtMain.Rows
                          select new { month = dRow["monthperiod"], year = dRow["yearperiod"] }).Distinct().OrderBy(c => c.year).ThenBy(c => c.month).ToList();

            int ct = 0;
            string htmlcontent = "";
            foreach (var a in rawAds)
            {

                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["monthperiod"].ToString() == a.month.ToString() && dRow["yearperiod"].ToString() == a.year.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalsend"))
                                    }).Distinct().ToList();

                if (rawDetailAds.Count > 0)
                {
                    htmlcontent = htmlcontent + string.Format("<div id='chartDetail{0}' style='width: 50%; height: 300px;display: inline-block;'></div>", ct);

                    ct++;
                }
            }

            ltDetailAds.Text = htmlcontent;

            ct = 0;
            foreach (var a in rawAds)
            {
                string title = "Advertisement Sent on " + a.month.ToString() + " / " + a.year.ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("promotitle");
                dt.Columns.Add("totalsend");

                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["monthperiod"].ToString() == a.month.ToString() && dRow["yearperiod"].ToString() == a.year.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalsend"))
                                    }).Distinct().ToList();

                foreach (var c in rawDetailAds)
                {
                    dt.Rows.Add(c.key.promotitle, c.totalsend);
                }


                if (dt != null && dt.Rows.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(dt);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info" + (ct + 4), "CreateChartSentPerAds('" + title + "','chartDetail" + ct + "', " + json.ToString() + ");", true);

                    ct++;
                }
            }
        }

        private void BindDataDetailClickedPerAdsPerMonth()
        {
            DataTable dtMain = clsMailPromo.GetDataSendPromo();

            var rawAds = (from DataRow dRow in dtMain.Rows
                          select new { month = dRow["monthperiod"], year = dRow["yearperiod"] }).Distinct().OrderBy(c => c.year).ThenBy(c => c.month).ToList();

            int ct = 0;
            string htmlcontent = "";
            foreach (var a in rawAds)
            {

                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["monthperiod"].ToString() == a.month.ToString() && dRow["yearperiod"].ToString() == a.year.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalclick"))
                                    }).Distinct().ToList();

                if (rawDetailAds.Count > 0)
                {
                    htmlcontent = htmlcontent + string.Format("<div id='chartClick{0}' style='width: 50%; height: 300px;display: inline-block;'></div>", ct);

                    ct++;
                }
            }

            ltDetailClickedAds.Text = htmlcontent;

            ct = 0;
            foreach (var a in rawAds)
            {
                string title = "Advertisement Clicked on " + a.month.ToString() + " / " + a.year.ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("promotitle");
                dt.Columns.Add("totalclick");

                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["monthperiod"].ToString() == a.month.ToString() && dRow["yearperiod"].ToString() == a.year.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalclick"))
                                    }).Distinct().ToList();

                foreach (var c in rawDetailAds)
                {
                    dt.Rows.Add(c.key.promotitle, c.totalsend);
                }


                if (dt != null && dt.Rows.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(dt);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "info" + (ct + 4), "CreateChartClickPerAds('" + title + "','chartClick" + ct + "', " + json.ToString() + ");", true);

                    ct++;
                }
            }
        }

        private void BindDataDetailAds()
        {
            DataTable dtMain = clsMailPromo.GetDataSendPromo();
            DataTable dt = new DataTable();
            dt.Columns.Add("promotitle");
            dt.Columns.Add("totalclick");
            dt.Columns.Add("totalsend");
            dt.Columns.Add("monthperiod");
            dt.Columns.Add("yearperiod");
            dt.Columns.Add("promoArchiveImage");

            var rawAds = (from DataRow dRow in dtMain.Rows
                          select new { month = dRow["monthperiod"], year = dRow["yearperiod"] }).Distinct().ToList();

            int ct = 0;
            foreach (var a in rawAds)
            {

                var rawDetailAds = (from DataRow dRow in dtMain.Rows
                                    where dRow["monthperiod"].ToString() == a.month.ToString() && dRow["yearperiod"].ToString() == a.year.ToString() &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) >= DateTime.ParseExact("01-" + txtStartDate1.Text, "dd-M-yyyy", null) &&
                                    DateTime.ParseExact("01-" + dRow["monthperiod"].ToString() + "-" + dRow["yearperiod"].ToString(), "dd-M-yyyy", null) <= DateTime.ParseExact("01-" + txtEndDate1.Text, "dd-M-yyyy", null)
                                    group dRow by new { promoid = dRow["promoid"], promotitle = dRow["promotitle"], promoArchiveImage = dRow["promoArchiveImage"] } into g
                                    select new
                                    {
                                        key = g.Key,
                                        totalsend = g.Sum(x => x.Field<int>("totalsend")),
                                        totalclick = g.Sum(x => x.Field<int>("totalclick")),
                                    }).Distinct().ToList();

                foreach (var c in rawDetailAds)
                {
                    dt.Rows.Add(c.key.promotitle, c.totalclick, c.totalsend, a.month, a.year, c.key.promoArchiveImage);
                }
            }

            gvMailPromo.DataSource = dt;
            gvMailPromo.DataBind();
        }
    }
}