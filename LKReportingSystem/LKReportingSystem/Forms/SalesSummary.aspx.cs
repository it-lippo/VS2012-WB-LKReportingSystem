using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;
using log4net;

namespace LKReportingSystem.Forms
{
    public partial class SalesSummary : System.Web.UI.Page
    {

        protected static readonly ILog log = LogManager.GetLogger(typeof(SalesSummary));

        private DataTable _DT_SalesSummaryHistory;
        private DataTable _DT_CostContractProjectMapping;
        private DataTable _DT_CostApportionmentDetail;


        private DataTable _DTFinal_MktExpenseDetail;
        private DataTable _DTFinal_CollectionDetail;
        private DataTable _DTFinal_CostContractDetail;    


        private void initDTFinal_MktExpenseDetail()
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_MktExpenseDetail - Started");

            _DTFinal_MktExpenseDetail = new DataTable();

            _DTFinal_MktExpenseDetail.Columns.Add("ProjectCode", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("ClusterCode", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("DevCode", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("IANo", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("IADesc", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            _DTFinal_MktExpenseDetail.Columns.Add("BudgetRevNo", System.Type.GetType("System.Int32"));
            _DTFinal_MktExpenseDetail.Columns.Add("ItemCode", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("ItemName", System.Type.GetType("System.String"));
            _DTFinal_MktExpenseDetail.Columns.Add("ApproveDate", System.Type.GetType("System.DateTime"));
            _DTFinal_MktExpenseDetail.Columns.Add("ApproveUN", System.Type.GetType("System.String"));

            log.DebugFormat("initDTFinal_MktExpenseDetail - Finished");

        }

        private void initDTFinal_CollectionDetail()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_CollectionDetail - Started");

            _DTFinal_CollectionDetail = new DataTable();

            _DTFinal_CollectionDetail.Columns.Add("ProjectCode", System.Type.GetType("System.String"));
            _DTFinal_CollectionDetail.Columns.Add("ClusterCode", System.Type.GetType("System.String"));
            _DTFinal_CollectionDetail.Columns.Add("PaymentTerm", System.Type.GetType("System.String"));
            _DTFinal_CollectionDetail.Columns.Add("TotalSchedule", System.Type.GetType("System.Decimal"));
            _DTFinal_CollectionDetail.Columns.Add("TotalCollection", System.Type.GetType("System.Decimal"));
            _DTFinal_CollectionDetail.Columns.Add("TotalOutstanding", System.Type.GetType("System.Decimal"));

            log.DebugFormat("initDTFinal_CollectionDetail - Finished");

        }

        private void initDT_CostContractProjectMapping()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDT_CostcontractProjectMapping - Started");

            _DT_CostContractProjectMapping = new DataTable();

            _DT_CostContractProjectMapping.Columns.Add("CCProjectCode", System.Type.GetType("System.String"));
            _DT_CostContractProjectMapping.Columns.Add("CCProjectGroupCode", System.Type.GetType("System.String"));
            _DT_CostContractProjectMapping.Columns.Add("CCProjectGroupID", System.Type.GetType("System.Int32"));
            _DT_CostContractProjectMapping.Columns.Add("LippoMasterProjectCode", System.Type.GetType("System.String"));
            _DT_CostContractProjectMapping.Columns.Add("LippoMasterClusterCode", System.Type.GetType("System.String"));

            log.DebugFormat("initDT_CostcontractProjectMapping - Finished");

        }

        private void initDT_CostApportionmentDetail()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDT_CostApportionmentDetail - Started");

            _DT_CostApportionmentDetail = new DataTable();

            _DT_CostApportionmentDetail.Columns.Add("Header", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("HeaderOrderNo", System.Type.GetType("System.Int32"));
            _DT_CostApportionmentDetail.Columns.Add("ProjectCode", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("BuildingTypeName", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("GroupHeader", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("projectCodeSc", System.Type.GetType("System.Int32"));
            _DT_CostApportionmentDetail.Columns.Add("WBSCode", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("WBSName", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("parentWBSCode", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("level", System.Type.GetType("System.Int32"));
            _DT_CostApportionmentDetail.Columns.Add("costTypeCode", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("currCode", System.Type.GetType("System.String"));
            _DT_CostApportionmentDetail.Columns.Add("currRate", System.Type.GetType("System.Decimal"));
            _DT_CostApportionmentDetail.Columns.Add("ContractAmount", System.Type.GetType("System.Decimal"));
            _DT_CostApportionmentDetail.Columns.Add("ProjectGroupID", System.Type.GetType("System.Int32"));
            _DT_CostApportionmentDetail.Columns.Add("ProjectID", System.Type.GetType("System.Int32"));
            _DT_CostApportionmentDetail.Columns.Add("Percentage", System.Type.GetType("System.Decimal"));
            _DT_CostApportionmentDetail.Columns.Add("Apportionment", System.Type.GetType("System.Decimal"));

            log.DebugFormat("initDT_CostApportionmentDetail - Finished");

        }

        private void initDTFinal_CostContractDetail()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("initDTFinal_CostContractDetail - Started");

            _DTFinal_CostContractDetail = new DataTable();

            _DTFinal_CostContractDetail.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
            _DTFinal_CostContractDetail.Columns.Add("ProjectGroupID", System.Type.GetType("System.Int32"));
            _DTFinal_CostContractDetail.Columns.Add("Header", System.Type.GetType("System.String"));
            _DTFinal_CostContractDetail.Columns.Add("ProjectCode", System.Type.GetType("System.String"));
            _DTFinal_CostContractDetail.Columns.Add("WBSCode", System.Type.GetType("System.String"));
            _DTFinal_CostContractDetail.Columns.Add("WBSName", System.Type.GetType("System.String"));
            _DTFinal_CostContractDetail.Columns.Add("parentWBSCode", System.Type.GetType("System.String"));
            _DTFinal_CostContractDetail.Columns.Add("WBSLevel", System.Type.GetType("System.Int32"));
            _DTFinal_CostContractDetail.Columns.Add("currCode", System.Type.GetType("System.String"));
            _DTFinal_CostContractDetail.Columns.Add("currRate", System.Type.GetType("System.Decimal"));
            _DTFinal_CostContractDetail.Columns.Add("ContractAmount", System.Type.GetType("System.Decimal"));
            _DTFinal_CostContractDetail.Columns.Add("Apportionment", System.Type.GetType("System.Decimal"));

            log.DebugFormat("initDTFinal_CostContractDetail - Finished");

        }

        private void processActualConstructionCost(DataTable dtCostcontractProjectMapping, int batchid, DateTime asofdate)
        {

            string CCProjectGroupID = "";
            var DT_CostcontractProjectMapping = (from DataRow dRow in dtCostcontractProjectMapping.Rows
                                                 select new
                                                 {
                                                     CCProjectCode = dRow["CCProjectCode"],
                                                     CCProjectGroupCode = dRow["CCProjectGroupCode"],
                                                     CCProjectGroupID = dRow["CCProjectGroupID"],
                                                     LippoMasterProjectCode = dRow["LippoMasterProjectCode"],
                                                     LippoMasterClusterCode = dRow["LippoMasterClusterCode"]
                                                 }).Distinct().ToList();

            if (DT_CostcontractProjectMapping.Count > 0)
            {
                var SingleRow_DT_CostcontractProjectMapping = DT_CostcontractProjectMapping.FirstOrDefault();
                CCProjectGroupID = SingleRow_DT_CostcontractProjectMapping.CCProjectGroupID.ToString();
            }
            else
            {
                log.DebugFormat("var DT_CostcontractProjectMapping is empty.");
            }


            initDT_CostApportionmentDetail();
            CostContractWS.WS_CostAndContract CostContractWS = new LKReportingSystem.CostContractWS.WS_CostAndContract();
            _DT_CostApportionmentDetail = CostContractWS.RetreiveCostApportionmentBasedOnContract(CCProjectGroupID, asofdate).Tables[0];

            initDTFinal_CostContractDetail();

            string CCProjectCode = "";
            string Header = "";
            int ProjectGroupID = 0;
            string ProjectCode;
            string WBSCode;
            string WBSName;
            string parentWBSCode;
            int WBSLevel = 0;
            string currCode;
            string currRate;
            decimal ContractAmount = 0;
            decimal Apportionment = 0;

            string DirectCostCategory = "DIRECTCOST";
            string DirectCostApportionmentCategory = "DIRECTCOSTWITHCOSTAPPORTIONMENT";

            decimal TotalActualValueDirectCost = 0;
            decimal TotalActualValueDirectCostApportionment = 0;

            //Adding new column
            _DT_CostContractProjectMapping.Columns.Add("BatchID", System.Type.GetType("System.Int32"));
            _DT_CostContractProjectMapping.Columns.Add("DirectCost", System.Type.GetType("System.Decimal"));
            _DT_CostContractProjectMapping.Columns.Add("DirectCostWithApportionment", System.Type.GetType("System.Decimal"));
            _DT_CostContractProjectMapping.Columns["BatchID"].SetOrdinal(0);

            //Looping project
            foreach (var x in DT_CostcontractProjectMapping)
            {

                CCProjectCode = x.CCProjectCode.ToString();

                TotalActualValueDirectCost = 0;
                TotalActualValueDirectCostApportionment = 0;

                var DT_CostApportionmentDetail = (from DataRow dRow in _DT_CostApportionmentDetail.Rows
                                                  where (dRow["ProjectCode"].ToString() == CCProjectCode.ToString())
                                                  select new
                                                  {
                                                      Header = dRow["Header"],
                                                      ProjectGroupID = dRow["ProjectGroupID"],
                                                      ProjectCode = dRow["ProjectCode"],
                                                      WBSCode = dRow["WBSCode"],
                                                      WBSName = dRow["WBSName"],
                                                      parentWBSCode = dRow["parentWBSCode"],
                                                      WBSLevel = dRow["level"],
                                                      currCode = dRow["currCode"],
                                                      currRate = dRow["currRate"],
                                                      ContractAmount = dRow["ContractAmount"],
                                                      Apportionment = dRow["Apportionment"]
                                                  }).Distinct().ToList();

                foreach (var v in DT_CostApportionmentDetail)
                {
                    Header = v.Header.ToString().ToUpper();
                    ProjectGroupID = int.Parse(v.ProjectGroupID.ToString());
                    ProjectCode = v.ProjectCode.ToString();
                    WBSCode = v.WBSCode.ToString();
                    WBSName = v.WBSName.ToString();
                    parentWBSCode = v.parentWBSCode.ToString();
                    WBSLevel = int.Parse(v.WBSLevel.ToString());
                    currCode = v.currCode.ToString();
                    currRate = v.currRate.ToString();
                    ContractAmount = decimal.Parse(v.ContractAmount.ToString());
                    Apportionment = decimal.Parse(v.Apportionment.ToString());

                    if (Header == DirectCostCategory)
                    {
                        TotalActualValueDirectCost = TotalActualValueDirectCost + Apportionment;

                        _DTFinal_CostContractDetail.Rows.Add(batchid, ProjectGroupID, Header, ProjectCode, WBSCode, WBSName, parentWBSCode, WBSLevel, currCode, currRate, ContractAmount, Apportionment);
                    }
                    else if (Header == DirectCostApportionmentCategory)
                    {
                        TotalActualValueDirectCostApportionment = TotalActualValueDirectCostApportionment + Apportionment;

                        _DTFinal_CostContractDetail.Rows.Add(batchid, ProjectGroupID, Header, ProjectCode, WBSCode, WBSName, parentWBSCode, WBSLevel, currCode, currRate, ContractAmount, Apportionment);
                    }

                }


                //update nilai Cost Contract pada datatable
                DataRow oDr = _DT_CostContractProjectMapping.AsEnumerable().Where(r => ((string)r["CCProjectCode"]).Equals(CCProjectCode)).First();
                oDr["BatchID"] = batchid;
                oDr["DirectCost"] = TotalActualValueDirectCost;
                oDr["DirectCostWithApportionment"] = TotalActualValueDirectCostApportionment;

            }


        }

        private void RetouchGridView()
        {
            if (gvDataSalesSummary.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                gvDataSalesSummary.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvDataSalesSummary.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                gvDataSalesSummary.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvDataSalesSummary.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                gvDataSalesSummary.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataProject();

                ddlProject.Items.Insert(0, new ListItem("--Select Project--", ""));
                ddlProject.ClearSelection();

                string isCancel = Request.QueryString["isCancel"];
                if (isCancel != "" && Session["SalesSummaryHistory"] != null && Session["ProjectCode"] != null)
                {
                    ddlProject.SelectedValue = (string)Session["ProjectCode"];
                    gvDataSalesSummary.DataSource = (DataTable)Session["SalesSummaryHistory"];
                    gvDataSalesSummary.DataBind();

                }



            }

            RetouchGridView();


        }

        private void BindDataSalesSummaryHistory()
        {
            log.Info("BindDataSalesSummaryHistory() started.");

            try
            {

                _DT_SalesSummaryHistory = new DataTable();
                _DT_SalesSummaryHistory = clsSalesSummary.GetHistorySalesSummary(ddlProject.SelectedValue);

                Session["SalesSummaryHistory"] = _DT_SalesSummaryHistory;

                gvDataSalesSummary.DataSource = _DT_SalesSummaryHistory;
                gvDataSalesSummary.DataBind();
                RetouchGridView();


            }
            catch (Exception ex)
            {
                log.ErrorFormat("BindDataSalesSummaryHistory() - ERROR: {0}", ex.Message.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }

            log.Info("BindDataSalesSummaryHistory() Ended.");

        }

        private void BindDataProject()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("BindDataProject() Started..");

            try
            {
                ddlProject.Items.Clear();

                DataTable dt = clsMaster.GetDataProjectCluster();

                var rawProject = (from DataRow dRow in dt.Rows
                                  select new { projectcode = dRow["projectCode"], projectlabel = dRow["projectLabel"] }).Distinct().ToList();

                foreach (var s in rawProject)
                {
                    ddlProject.Items.Add(new ListItem(s.projectlabel.ToString(), s.projectcode.ToString()));
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
            log.Info("BindDataProject() Finished..");
        }

        private void BindDataCluster(string projectcode)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("BindDataCluster() Started.. Parameter sent: projectcode={0}", projectcode);

            try
            {
                lbxCluster.Items.Clear();

                DataTable dt = clsMaster.GetDataProjectCluster();

                var rawCluster = (from DataRow dRow in dt.Rows
                                  where dRow["projectCode"].ToString().ToLower() == projectcode.ToLower()
                                  select new { clustercode = dRow["clusterCode"], clusterlabel = dRow["clusterLabel"] }).Distinct().ToList();

                foreach (var s in rawCluster)
                {
                    if (!Constants.clusterExclude.Contains(s.clustercode.ToString())) 
                    {
                        lbxCluster.Items.Add(new ListItem(s.clusterlabel.ToString(), s.clustercode.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }

            log.Info("BindDataCluster() Finished..");

        }

        protected void DdlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataSalesSummaryHistory();
            BindDataCluster(ddlProject.SelectedValue);
            Session["ProjectCode"] = ddlProject.SelectedValue;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {

                string projectcode = ddlProject.SelectedValue.ToString();
                string clustercode = "";

                foreach (ListItem item in lbxCluster.Items)
                {
                    if (item.Selected)
                    {
                        clustercode += item.Value.ToString() + ",";
                    }
                }


                DateTime asofdate = DateTime.ParseExact(txtAsOfDate.Text + " " + DateTime.Now.ToString("hh:mm:ss"), "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                int batchid = clsSalesSummary.GenerateSalesSummary(projectcode, clustercode, asofdate, "System Generated");

                if (batchid != null)
                {
                    //update collection
                    initDTFinal_CollectionDetail();
                    _DTFinal_CollectionDetail = clsSalesSummary.GenerateScheduleCollection(projectcode, clustercode);

                    _DTFinal_CollectionDetail.Columns.Add("BatchID", System.Type.GetType("System.Int32"), batchid.ToString());
                    _DTFinal_CollectionDetail.Columns["BatchID"].SetOrdinal(0);

                    clsSalesSummary.InsertScheduleCollectionDetail(_DTFinal_CollectionDetail);


                    //update paidcomm
                    NewCommWS.NewComm_Services1 NewCommWS = new LKReportingSystem.NewCommWS.NewComm_Services1();
                    NewCommWS.Timeout = 600000;
                    decimal PaidComm = 0;
                    decimal UnpaidComm = 0;
                    log.DebugFormat("NewComm Web Service Invoked.. Parameter sent: projectcode={0}", projectcode);
                    PaidComm = NewCommWS.getCommPaid(projectcode);
                    UnpaidComm = NewCommWS.getCommUnprocessed(projectcode);

                    BudgetMktWS.WS_BudgetMarketing BudgetMktWS = new LKReportingSystem.BudgetMktWS.WS_BudgetMarketing();
                    BudgetMktWS.Timeout = 600000;

                    //update budgetmkt
                    decimal BudgetMKTExpense = 0;
                    log.DebugFormat("BudgetMkt Web Service [getBudgetMktActualExpense] Method Invoked.. Parameter sent: projectcode={0}", projectcode);
                    BudgetMKTExpense = BudgetMktWS.getBudgetMktActualExpense(projectcode);

                    //insert budgetmkt detail
                    //initDTFinal_MktExpenseDetail();

                    //log.DebugFormat("BudgetMkt Web Service [getBudgetMktActualExpenseDetail] Method Invoked.. Parameter sent: projectcode={0}", projectcode);

                    //_DTFinal_MktExpenseDetail = Helper.ConvertXMLToDataset(BudgetMktWS.getBudgetMktActualExpenseDetail(projectcode)).Tables[0];

                    //_DTFinal_MktExpenseDetail.Columns.Add("BatchID", System.Type.GetType("System.Int32"), batchid.ToString());
                    //_DTFinal_MktExpenseDetail.Columns["BatchID"].SetOrdinal(0);

                    //clsSalesSummary.InsertActualBudgetMktDetail(_DTFinal_MktExpenseDetail);



                    //actual Cost Contract
                    CostContractWS.WS_CostAndContract CostContractWS = new LKReportingSystem.CostContractWS.WS_CostAndContract();
                    CostContractWS.Timeout = 600000;
                    log.DebugFormat("CostContract Web Service [RetrieveProjectMapping] Method Invoked.. Parameter sent: projectcode={0}", projectcode);

                    initDT_CostContractProjectMapping();

                    if (CostContractWS.RetrieveProjectMapping(projectcode).Tables.Count != 0) 
                    {
                        _DT_CostContractProjectMapping = CostContractWS.RetrieveProjectMapping(projectcode).Tables[0];

                        processActualConstructionCost(_DT_CostContractProjectMapping, batchid, asofdate);

                        clsSalesSummary.UpdateActualCost(_DT_CostContractProjectMapping, "System Generated");

                        clsSalesSummary.InsertActualCostContractDetail(_DTFinal_CostContractDetail);
                    }




                    



                    clsSalesSummary.UpdateProjectByWsValue(batchid, PaidComm, UnpaidComm, BudgetMKTExpense);

                    BindDataSalesSummaryHistory();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Failed when Generate Sales Summary Report');", true);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("btnGenerate_Click() ERROR: {0}.",ex.Message);   
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('There is an error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }

        }

        protected void gvDataSalesSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateData")
            {
                string link = Page.ResolveUrl("~/Forms/UpdateProjectInformation.aspx?batchid=" + e.CommandArgument.ToString());
                //Response.Redirect(link);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + link + "', '_blank');", true);
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + link + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no, resizable=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            }
            else if (e.CommandName == "ViewData")
            {
                string link = Page.ResolveUrl("~/Lookup/Preview/ViewSalesSummary.aspx?batchid=" + e.CommandArgument.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + link + "', '_blank');", true);
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + link + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no, resizable=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);                

            }
            else if (e.CommandName == "DownloadData")
            {
                string link = Page.ResolveUrl("~/Lookup/Preview/ViewSalesSummary.aspx?batchid=" + e.CommandArgument.ToString());
                string html = "";

                using (var writer = new StringWriter())
                {
                    Server.Execute(link, writer);
                    html = writer.GetStringBuilder().ToString();
                }

                Response.AppendHeader("content-disposition", "attachment;filename=" + "Sales Summary " + DateTime.Now.ToShortDateString() + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                this.EnableViewState = false;
                Response.Write(html);
                Response.Flush();
                Response.End();
            }

            //BindDataSalesSummaryHistory();
        }

        protected void gvDataSalesSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[2].Visible = false;
            }
            catch
            {
            }
        }

        protected void btnProjectSetup_Click(object sender, EventArgs e)
        {
            if (Session["ProjectCode"] != null)
            {

                _DT_SalesSummaryHistory = new DataTable();
                _DT_SalesSummaryHistory = (DataTable)Session["SalesSummaryHistory"];


                int BatchID;
                if (_DT_SalesSummaryHistory.Rows.Count > 0)
                {
                    BatchID = int.Parse(_DT_SalesSummaryHistory.Rows[0][0].ToString());
                }
                else { BatchID = 0; }

                string link = Page.ResolveUrl("~/Forms/UpdateProjectInformation.aspx?batchid=" + BatchID);
                Response.Redirect(link);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Please select project for salary setup.');", true);

            }
        }

        protected void btnSalarySetup_Click(object sender, EventArgs e)
        {

            if (Session["ProjectCode"] != null)
            {

                _DT_SalesSummaryHistory = new DataTable();
                _DT_SalesSummaryHistory = (DataTable)Session["SalesSummaryHistory"];

                string ProjectCode = ddlProject.SelectedValue;

                string link = Page.ResolveUrl("~/Forms/ProjectSalarySetup.aspx?ProjectCode=" + ProjectCode);
                Response.Redirect(link);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Please select project for salary setup.');", true);

            }


        }

        protected void btnInitialBudget_Click(object sender, EventArgs e)
        {
            if (Session["ProjectCode"] != null)
            {

                _DT_SalesSummaryHistory = new DataTable();
                _DT_SalesSummaryHistory = (DataTable)Session["SalesSummaryHistory"];

                int BatchID;
                if (_DT_SalesSummaryHistory.Rows.Count > 0)
                {
                    BatchID = int.Parse(_DT_SalesSummaryHistory.Rows[0][0].ToString());
                }
                else { BatchID = 0; }

                string link = Page.ResolveUrl("~/Forms/InitialBudgetSetup.aspx?batchid=" + BatchID);
                Response.Redirect(link);


            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Please select project for Initial Budget setup.');", true);

            }
        }

        protected void btnConstructionCost_Click(object sender, EventArgs e)
        {
            if (Session["ProjectCode"] != null)
            {

                _DT_SalesSummaryHistory = new DataTable();
                _DT_SalesSummaryHistory = (DataTable)Session["SalesSummaryHistory"];

                int BatchID;
                if (_DT_SalesSummaryHistory.Rows.Count > 0)
                {
                    BatchID = int.Parse(_DT_SalesSummaryHistory.Rows[0][0].ToString());
                }
                else { BatchID = 0; }

                string link = Page.ResolveUrl("~/Forms/ConstructionCostSetup.aspx?batchid=" + BatchID);
                Response.Redirect(link);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Please select project for Initial Budget setup.');", true);

            }
        }

        protected void btnCashflowSetup_Click(object sender, EventArgs e)
        {
            if (Session["ProjectCode"] != null)
            {

                _DT_SalesSummaryHistory = new DataTable();
                _DT_SalesSummaryHistory = (DataTable)Session["SalesSummaryHistory"];

                int BatchID;
                if (_DT_SalesSummaryHistory.Rows.Count > 0)
                {
                    BatchID = int.Parse(_DT_SalesSummaryHistory.Rows[0][0].ToString());
                }
                else { BatchID = 0; }

                string link = Page.ResolveUrl("~/Forms/CashFlowSetup.aspx?batchid=" + BatchID);
                Response.Redirect(link);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "BootboxAlert('Please select project for Initial Budget setup.');", true);

            }
        }

      


    }
}