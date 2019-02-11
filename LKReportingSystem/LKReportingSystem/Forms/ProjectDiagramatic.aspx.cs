using LKReportingSystem.Class;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreLinq;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace LKReportingSystem.Forms
{
    public partial class ProjectDiagramatic : System.Web.UI.Page
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ProjectDiagramatic));

        class Response
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<Project> arrResult { get; set; }
            public string xmlFloor { get; set; }
            public string xmlUnitModular { get; set; }
            public string xmlAllUnitModular { get; set; }
            public string xmlSummary { get; set; }
        }

        class Project
        {
            public string projectCode { get; set; }
            public string clusterCode { get; set; }
            public string projectName { get; set; }
            public string projectDesc { get; set; }
            public string clusterName { get; set; }
            public bool isShowInputPP { get; set; }
            public string productType { get; set; }
        }

        class Param_ProjectCluster
        {
            public string projectCode { get; set; }
            public string clusterCode { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsSecurity.HaveAccess(this.AppRelativeVirtualPath))
            {
                htmlNotifMain.InnerHtml = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">x</button>"
                        + "<i class=\"fa fa-info-circle\"> </i>"
                        + "<Strong> Warning! </Strong> You Have No Permission To View this Page";

                htmlNotifMain.Attributes.Add("class", "alert alert-danger alert-dismissable");
                upHtmlNotifMain.Update();

                divContent.Visible = false;
                return;
            }

            if (!IsPostBack)
            {
                BindDataProject();
            }
        }

        private void BindDataProject()
        {
            try
            {
                ddlProject.Items.Clear();

                DataTable dt = clsMaster.GetDataProjectCluster();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlProject.Items.Add(new ListItem("- Choose Project -", ""));

                    var projectDisct = (from DataRow dRow in dt.Rows
                                        select new { projectcode = dRow["projectCode"], projectname = dRow["projectName"] }).Distinct().ToList();

                    foreach (var p in projectDisct)
                    {
                        ddlProject.Items.Add(new ListItem(p.projectname.ToString(), p.projectcode.ToString()));
                    }

                    string defaultProject = "";

                    try
                    {
                        ddlProject.SelectedValue = defaultProject;
                        BindDataCluster(defaultProject);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorFormat("BindDataProject() - Default Project {1} : {2}. [Invoked By : {0}]", Session["memberCode"], defaultProject, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("BindDataProject() - {1}. [Invoked By : {0}]", Session["memberCode"], ex.Message);
                Helper.JQueryErrMsg(ex.Message, this);
            }
        }

        private void BindDataCluster(string projectcode)
        {
            try
            {
                ddlCluster.Items.Clear();

                DataTable dt = clsMaster.GetDataProjectCluster();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlCluster.Items.Add(new ListItem("- Choose Cluster -", ""));

                    var resultDistinct = (from DataRow dRow in dt.Rows
                                      where dRow["projectCode"].ToString().ToLower() == projectcode.ToLower()
                                      select new { clustercode = dRow["clusterCode"], clustername = dRow["clusterName"] }).Distinct().ToList();
                     
                    foreach (var p in resultDistinct)
                    { 
                        ddlCluster.Items.Add(new ListItem(p.clustername.ToString(), p.clustercode.ToString()));
                    }
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat("BindDataCluster() - {1}. [Invoked By : {0}]", Session["memberCode"], ex.Message);
                Helper.JQueryErrMsg(ex.Message, this);
            }
        }

        private void ClearSummaryData()
        {
            lblSumRed.Text = "";
            lblSumYellow.Text = "";
            lblSumGreen.Text = "";
            lblSumBlue.Text = "";

            lblJatuhTempo1Bln.Text = "";
            lblJatuhTempo3Bln.Text = "";
            lblJatuhTempo6Bln.Text = "";
            lblJatuhTempo9Bln.Text = "";
            lblJatuhTempo12Bln.Text = "";
            lblJatuhTempo12BlnPlus.Text = "";

            lblLewatJatuhTempo1Bln.Text = "";
            lblLewatJatuhTempo3Bln.Text = "";
            lblLewatJatuhTempo6Bln.Text = "";
            lblLewatJatuhTempo9Bln.Text = "";
            lblLewatJatuhTempo12Bln.Text = "";
            lblLewatJatuhTempo12BlnPlus.Text = "";
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltDiagramatic1.Text = "";
            ltDiagramatic2.Text = "";
            ddlCluster.Items.Clear();
            pnlDiagramatic.Visible = false;
            pnlSummary.Visible = false;
            ClearSummaryData();

            if (ddlProject.SelectedValue != "")
            {
                BindDataCluster(ddlProject.SelectedValue);
            }
            else
            {
                Helper.JQueryErrMsg("Data Project is Required. Please choose Project.", this);
            }
        }

        protected void btnShowDiagramatic_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProject.SelectedValue == "")
                {
                    Helper.JQueryErrMsg("Please choose Project.", this);
                    return;
                }

                if (ddlCluster.SelectedValue == "")
                {
                    Helper.JQueryErrMsg("Please choose Cluster.", this);
                    return;
                }

                DataTable dtBlock1 = new DataTable();
                DataTable dtUnit1 = new DataTable();
                DataTable dtUnitAll1 = new DataTable();
                DataTable dtSummary = new DataTable();

                WS_OnlineBooking.WS_OnlineBooking ws = new WS_OnlineBooking.WS_OnlineBooking();
                WS_OnlineBooking.AuthHeader wsAuth = new WS_OnlineBooking.AuthHeader();

                wsAuth.domainName = Constants.domainName;
                wsAuth.userName = Constants.username;
                wsAuth.password = Constants.password;
                ws.AuthHeaderValue = wsAuth;

                string clusterCode1 = ddlCluster.SelectedValue;
                string clusterName1 = "";
                string categoryCode = "";
                 
                Param_ProjectCluster param1 = new Param_ProjectCluster();
                param1.projectCode = ddlProject.SelectedValue;
                param1.clusterCode = clusterCode1;

                string jsonResult1 = ws.retrieveComponentDiagramaticForProjectDiagramatic(JsonConvert.SerializeObject(param1));

                Response resp1 = new Response();
                resp1 = JsonConvert.DeserializeObject<Response>(jsonResult1);

                if (resp1.code == 1)
                {
                    dtBlock1 = (Helper.ConvertXMLToDataset(resp1.xmlFloor)).Tables[0];
                    dtUnit1 = (Helper.ConvertXMLToDataset(resp1.xmlUnitModular)).Tables[0];
                    dtUnitAll1 = (Helper.ConvertXMLToDataset(resp1.xmlAllUnitModular)).Tables[0];
                    dtSummary = (Helper.ConvertXMLToDataset(resp1.xmlSummary)).Tables[0];
                }
                else
                {
                    Helper.JQueryErrMsg(string.Format("Failed to Get Response. {0}", resp1.message), this);
                }

                pnlDiagramatic.Visible = true;
                pnlSummary.Visible = true;

                DataTable dt = clsMaster.GetDataProjectCluster();

                var result = (from DataRow dRow in dt.Rows
                                      where dRow["clusterCode"].ToString().ToLower() == clusterCode1.ToLower()
                                      select new { categoryCode = dRow["categoryCode"] }).Distinct().ToList();

                foreach (var res in result)
                {
                    categoryCode = res.categoryCode.ToString();
                }

                ltTitleDiagramatic.Text = "<b>" + ddlCluster.SelectedItem.Text + "</b>";

                if (Constants.ApartmentCategory.Contains(categoryCode))
                {
                    ltDiagramatic1.Text = GetDiagramApartment(dtBlock1, dtUnit1, dtUnitAll1, dtSummary, clusterCode1, clusterName1);
                }
                else if (Constants.LandedCategory.Contains(categoryCode))
                {
                    ltDiagramatic1.Text = GetDiagramLanded(dtBlock1, dtUnit1, dtUnitAll1, dtSummary, clusterCode1, clusterName1);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("btnShowDiagramatic_Click() - {1}. [Invoked By : {0}]", Session["memberCode"], ex.Message);
                Helper.JQueryErrMsg(ex.Message, this);
            }
        }

        private string GetDiagramLanded(DataTable dtBlock, DataTable dtUnit, DataTable dtUnitAll, DataTable dtSummary, string clusterCode1, string clusterName1)
        {
            StringBuilder result = new StringBuilder();

            decimal screenWidth = 0;
            decimal screenHeight = 0;

            try
            {
                screenWidth = Convert.ToDecimal(hfScreenWidth.Value);
                screenHeight = Convert.ToDecimal(hfScreenHeight.Value);
            }
            catch (Exception ex)
            { }

            bool isMobileVersion = Helper.isSmallScreenBrowser(screenWidth, screenHeight);

            if (dtUnitAll.Rows.Count > 0)
            {
                result.Append("<table style='width:100%'>");
                result.Append(" <thead><tr>");
                result.Append("  <th style='width:60px !important; padding-left: 10px !important; padding-right:10px !important; background-color: #5C5A5A'>Units</th>");


                var rawUnitCode = (from DataRow dRow in dtUnitAll.Rows
                                   select new { unitCode = dRow["unitCode"], unitName = dRow["unitName"] }).Distinct().OrderBy(c => c.unitCode).ToList();



                foreach (var u in rawUnitCode)
                {
                    string HeaderText = "<div class='diag_h'>" + u.unitName.ToString() + "</div>";

                    result.Append("  <th  style = 'text-align:center;padding-left: 10px !important; padding-right:10px !important; background-color: #838383'>" + HeaderText + "</th>");
                }

                result.Append(" </tr></thead>");

                result.Append("<tbody>");

                var rawUnitNo = (from DataRow dRow in dtUnitAll.Rows
                                 select new { unitno = dRow["unitNo"] }).Distinct().OrderBy(c => c.unitno).ToList();

                //for (int a = 0; a <= dtBlock.Rows.Count - 1; a++)
                foreach (var c in rawUnitNo)
                {
                    result.Append(" <tr style=\"padding: 0px !important\">");
                    result.Append("  <th class=\"tblCol\" style=\"padding: 0px !important; text-align:center; background-color: #838383\">" + c.unitno + "</th>");

                    DataTable dtRawBlockUnit = new DataTable();
                    var rawUnit = (from DataRow dRow in dtUnitAll.Rows
                                   where dRow["unitNo"].ToString().ToLower() == c.unitno.ToString().ToLower()
                                   select dRow).Distinct().ToList();

                    DataTable dtBlockUnit = new DataTable();

                    if (rawUnit.Count > 0) dtBlockUnit = rawUnit.CopyToDataTable();

                    var rawDistUnitCode = (from DataRow dRow in dtUnitAll.Rows
                                           select new { unitCode = dRow["unitCode"], unitName = dRow["unitName"] }).Distinct().OrderBy(d => d.unitCode).ToList();

                    //for (int j = 0; j <= rawDistUnitCode.Count - 1; j++)
                    foreach (var u in rawDistUnitCode)
                    {
                        if (dtBlockUnit.Rows.Count > 0)
                        {
                            var rawSUnit = (from DataRow dRow in dtUnitAll.Rows
                                            where dRow["unitCode"].ToString() == u.unitCode.ToString()
                                            && dRow["unitNo"].ToString() == c.unitno.ToString()
                                            select dRow).Distinct().ToList();

                            if (rawSUnit != null && rawSUnit.Count > 0)
                            {
                                DataTable dtSUnit = rawSUnit.CopyToDataTable();

                                string id = "tbl" + u.unitCode.ToString() + "X" + c.unitno.ToString();

                                string color = "#ffffff";

                                if (dtSUnit.Rows[0]["unitStatusCode"].ToString() == "S")
                                {
                                    color = dtBlockUnit.Rows[0]["cellColor"].ToString();

                                    if (isMobileVersion == false)
                                    {
                                        result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\" onmouseout=\"HideDetailUnitHover();\" onmouseover=\"ShowDetailUnitHover(event,'" +
                                                    dtBlockUnit.Rows[0]["bookCode"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookAge"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["itemName"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stpDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stfDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuNo"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    "')\"><input type=\"checkbox\" name=\"cbDiagramatic\" onclick=\"selectOnlyThis(this)\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtSUnit.Rows[0]["unitCode"].ToString() + "#" + dtSUnit.Rows[0]["unitNo"].ToString() + "#" + dtSUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode1 + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtSUnit.Rows[0]["unitNo"].ToString() + "</label></td>");

                                    }
                                    else
                                    {
                                        result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\" onclick=\"ShowPopUpDetailUnitHover(event,'" +
                                                    dtBlockUnit.Rows[0]["bookCode"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookAge"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["itemName"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stpDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stfDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuNo"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    "');return false\"><input type=\"checkbox\" name=\"cbDiagramatic\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtBlockUnit.Rows[0]["unitCode"].ToString() + "#" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "#" + dtBlockUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode1 + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "</label></td>");
                                    }
                                }
                                else
                                {
                                    if (isMobileVersion == false)
                                    {
                                        result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\"><input type=\"checkbox\" name=\"cbDiagramatic\" onclick=\"selectOnlyThis(this)\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtSUnit.Rows[0]["unitCode"].ToString() + "#" + dtSUnit.Rows[0]["unitNo"].ToString() + "#" + dtSUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode1 + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtSUnit.Rows[0]["unitNo"].ToString() + "</label></td>");

                                    }
                                    else
                                    {
                                        result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\"><input type=\"checkbox\" name=\"cbDiagramatic\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtBlockUnit.Rows[0]["unitCode"].ToString() + "#" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "#" + dtBlockUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode1 + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "</label></td>");
                                    }
                                }
                            }
                            else
                            {
                                result.Append("  <td class=\"label-diagram\" style=\"width:auto\"></td>");
                            }
                        }
                        else
                        {
                            result.Append("  <td class=\"label-diagram\" style=\"width:auto\"></td>");
                        }
                    }
                    result.Append(" </tr>");
                }

                result.Append(" <tr>");
                result.Append("  <td class=\"tblLeftCol\"></td>");
                result.Append("  <td class=\"tblLeftSep\"></td>");
                result.Append("  <td colspan=\"" + dtUnit.Rows.Count.ToString() + "\" class=\"tblTopSep\"></td>");
                result.Append(" </tr>");

                result.Append("</tbody>");

                result.Append("</table>");

                if (dtSummary != null && dtSummary.Rows.Count > 0)
                {
                    lblSumRed.Text = dtSummary.Rows[0]["countRed"].ToString();
                    lblSumYellow.Text = dtSummary.Rows[0]["countYellow"].ToString();
                    lblSumGreen.Text = dtSummary.Rows[0]["countGreen"].ToString();
                    lblSumBlue.Text = dtSummary.Rows[0]["countBlue"].ToString();

                    lblJatuhTempo1Bln.Text = dtSummary.Rows[0]["countJatuhTempo1Bln"].ToString();
                    lblJatuhTempo3Bln.Text = dtSummary.Rows[0]["countJatuhTempo3Bln"].ToString();
                    lblJatuhTempo6Bln.Text = dtSummary.Rows[0]["countJatuhTempo6Bln"].ToString();
                    lblJatuhTempo9Bln.Text = dtSummary.Rows[0]["countJatuhTempo9Bln"].ToString();
                    lblJatuhTempo12Bln.Text = dtSummary.Rows[0]["countJatuhTempo12Bln"].ToString();
                    lblJatuhTempo12BlnPlus.Text = dtSummary.Rows[0]["countJatuhTempo12BlnPlus"].ToString();

                    lblLewatJatuhTempo1Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo1Bln"].ToString();
                    lblLewatJatuhTempo3Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo3Bln"].ToString();
                    lblLewatJatuhTempo6Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo6Bln"].ToString();
                    lblLewatJatuhTempo9Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo9Bln"].ToString();
                    lblLewatJatuhTempo12Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo12Bln"].ToString();
                    lblLewatJatuhTempo12BlnPlus.Text = dtSummary.Rows[0]["countLewatJatuhTempo12BlnPlus"].ToString();
                }
            }
            else
            {
                result.Append("No Data Unit Available");
            }

            return result.ToString();
        }

        private string GetDiagramApartment(DataTable dtBlock, DataTable dtUnit, DataTable dtUnitAll, DataTable dtSummary, string clusterCode, string clusterName)
        {
            StringBuilder result = new StringBuilder();

            decimal screenWidth = 0;
            decimal screenHeight = 0;

            try
            {
                screenWidth = Convert.ToDecimal(hfScreenWidth.Value);
                screenHeight = Convert.ToDecimal(hfScreenHeight.Value);
            }
            catch (Exception ex)
            { }

            bool isMobileVersion = Helper.isSmallScreenBrowser(screenWidth, screenHeight);

            if (dtUnitAll.Rows.Count > 0)
            {
                dtUnitAll.Columns.Add("rowGroup");
                dtUnitAll.Columns.Add("colGroup");

                foreach (DataRow dr in dtUnitAll.Rows)
                {
                    string unitcode = dr["unitCode"].ToString();
                    string unitno = dr["unitno"].ToString();

                    string rowGroup = unitcode.Substring(clusterCode.Length, unitcode.Length - clusterCode.Length);
                    string colGroup = unitno.Substring(rowGroup.Length, unitno.Length - rowGroup.Length);

                    dr["rowGroup"] = rowGroup;
                    dr["colGroup"] = colGroup;
                }

                result.Append("<table style='width:100%'>");
                result.Append(" <thead><tr>");
                result.Append("  <th style='width:60px !important; padding-left: 10px !important; padding-right:10px !important; background-color: #5C5A5A'>Units</th>");

                var rawColumn = (from DataRow dRow in dtUnitAll.Rows
                                 select new { colGroup = dRow["colGroup"].ToString() }).Distinct().OrderBy(c => c.colGroup).ToList();

                foreach (var u in rawColumn)
                {
                    string HeaderText = "<div class='diag_h'>" + u.colGroup.ToString() + "</div>";

                    result.Append("  <th  style ='  text-align:center;padding-left: 10px !important; padding-right:10px !important; background-color: #838383'>" + HeaderText + "</th>");
                }

                result.Append(" </tr></thead>");

                result.Append("<tbody>");


                var rawUnitNo = (from DataRow dRow in dtUnitAll.Rows
                                 select new { rowGroup = dRow["rowGroup"] }).Distinct().OrderByDescending(c => c.rowGroup).ToList();

                foreach (var c in rawUnitNo)
                {
                    result.Append(" <tr style=\"padding: 0px !important\">");
                    result.Append("  <th class=\"tblCol\" style=\"padding: 0px !important; text-align:center; background-color: #838383\">" + c.rowGroup + "</th>");


                    foreach (var u in rawColumn)
                    {
                        var rawUnit = (from DataRow dRow in dtUnitAll.Rows
                                       where dRow["rowGroup"].ToString().ToLower() == c.rowGroup.ToString().ToLower() &&
                                             dRow["colGroup"].ToString().ToLower() == u.colGroup.ToString().ToLower()
                                       select dRow).Distinct().ToList();

                        DataTable dtBlockUnit = new DataTable();

                        if (rawUnit.Count > 0)
                        {
                            dtBlockUnit = rawUnit.CopyToDataTable();

                            string unitcode = dtBlockUnit.Rows[0]["unitCode"].ToString();
                            string unitno = dtBlockUnit.Rows[0]["unitno"].ToString();

                            string id = "tbl" + unitcode + "X" + unitno;

                            string color = "#ffffff";

                            if (dtBlockUnit.Rows[0]["unitStatusCode"].ToString() == "S")
                            {
                                color = dtBlockUnit.Rows[0]["cellColor"].ToString();

                                if (isMobileVersion == false)
                                {
                                    result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\" onmouseout=\"HideDetailUnitHover();\" onmouseover=\"ShowDetailUnitHover(event,'" +
                                                    dtBlockUnit.Rows[0]["bookCode"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookAge"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["itemName"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stpDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stfDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuNo"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    "')\"><input type=\"checkbox\" name=\"cbDiagramatic\" onclick=\"selectOnlyThis(this)\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtBlockUnit.Rows[0]["unitCode"].ToString() + "#" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "#" + dtBlockUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "</label></td>");
                                }
                                else
                                {
                                    result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\" onclick=\"ShowPopUpDetailUnitHover(event,'" +
                                                    dtBlockUnit.Rows[0]["bookCode"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["bookAge"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["itemName"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stpDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["stfDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuNo"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    dtBlockUnit.Rows[0]["pppuDate"].ToString().Replace("'", "").Replace("\"", "") + "', '" +
                                                    "');return false\"><input type=\"checkbox\" name=\"cbDiagramatic\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtBlockUnit.Rows[0]["unitCode"].ToString() + "#" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "#" + dtBlockUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "</label></td>");
                                }
                            }
                            else
                            {
                                if (isMobileVersion == false)
                                {
                                    result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\"><input type=\"checkbox\" name=\"cbDiagramatic\" onclick=\"selectOnlyThis(this)\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtBlockUnit.Rows[0]["unitCode"].ToString() + "#" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "#" + dtBlockUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "</label></td>");
                                }
                                else
                                {
                                    result.Append("  <td align=\"center\" id=\"" + id + "\" class=\"tblCol\" style=\"width:auto; padding: 0px !important\"><input type=\"checkbox\" name=\"cbDiagramatic\" id=\"cb" + id + "\" class=\"cbUnit\" value=\"" + dtBlockUnit.Rows[0]["unitCode"].ToString() + "#" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "#" + dtBlockUnit.Rows[0]["unitName"].ToString() + "#" + clusterCode + "\"><label for=\"cb" + id + "\" style=\"background-color:" + color + "\" class=\"label-diagram\" >" + dtBlockUnit.Rows[0]["unitNo"].ToString() + "</label></td>");
                                }
                            }
                        }
                        else
                        {
                            result.Append("  <td class=\"label-diagram\" style=\"width:auto;\"></td>");
                        }

                    }

                    result.Append(" </tr>");
                }

                result.Append(" <tr>");
                result.Append("  <td class=\"tblLeftCol\"></td>");
                result.Append("  <td class=\"tblLeftSep\"></td>");
                result.Append("  <td colspan=\"" + dtUnit.Rows.Count.ToString() + "\" class=\"tblTopSep\"></td>");
                result.Append(" </tr>");

                result.Append("</tbody>");

                result.Append("</table>");

                if (dtSummary != null && dtSummary.Rows.Count > 0)
                {
                    lblSumRed.Text = dtSummary.Rows[0]["countRed"].ToString();
                    lblSumYellow.Text = dtSummary.Rows[0]["countYellow"].ToString();
                    lblSumGreen.Text = dtSummary.Rows[0]["countGreen"].ToString();
                    lblSumBlue.Text = dtSummary.Rows[0]["countBlue"].ToString();

                    lblJatuhTempo1Bln.Text = dtSummary.Rows[0]["countJatuhTempo1Bln"].ToString();
                    lblJatuhTempo3Bln.Text = dtSummary.Rows[0]["countJatuhTempo3Bln"].ToString();
                    lblJatuhTempo6Bln.Text = dtSummary.Rows[0]["countJatuhTempo6Bln"].ToString();
                    lblJatuhTempo9Bln.Text = dtSummary.Rows[0]["countJatuhTempo9Bln"].ToString();
                    lblJatuhTempo12Bln.Text = dtSummary.Rows[0]["countJatuhTempo12Bln"].ToString();
                    lblJatuhTempo12BlnPlus.Text = dtSummary.Rows[0]["countJatuhTempo12BlnPlus"].ToString();

                    lblLewatJatuhTempo1Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo1Bln"].ToString();
                    lblLewatJatuhTempo3Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo3Bln"].ToString();
                    lblLewatJatuhTempo6Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo6Bln"].ToString();
                    lblLewatJatuhTempo9Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo9Bln"].ToString();
                    lblLewatJatuhTempo12Bln.Text = dtSummary.Rows[0]["countLewatJatuhTempo12Bln"].ToString();
                    lblLewatJatuhTempo12BlnPlus.Text = dtSummary.Rows[0]["countLewatJatuhTempo12BlnPlus"].ToString();
                }
            }
            else
            {
                result.Append("No Data Unit Available");
            }

            return result.ToString();
        }
    }
}