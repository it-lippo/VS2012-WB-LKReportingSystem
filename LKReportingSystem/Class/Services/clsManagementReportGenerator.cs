using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using LKReportingSystem.Class;
using LKReportingSystem.Class.Forms;
using log4net;

namespace LKReportingSystem.Class.Services
{
    public class clsManagementReportGenerator
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsManagementReportGenerator));

        private DataTable dtProject;

        private DataTable _DT_SalesSummaryHistory;
        private DataTable _DT_CostContractProjectMapping;
        private DataTable _DT_CostApportionmentDetail;
        private DataTable _DTFinal_MktExpenseDetail;
        private DataTable _DTFinal_CollectionDetail;
        private DataTable _DTFinal_CostContractDetail;

        public clsManagementReportGenerator()
        {
            InitialProcess();

            GetProjectData();

        }

        private void InitialProcess()
        {
            dtProject = new DataTable();
            dtProject.Columns.Add("ProjectCode");
            dtProject.Columns.Add("ProjectName");
        }

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

        private void GetProjectData()
        {
            try
            {
                DataTable dt = clsMaster.GetDataProjectCluster();

                var rawProject = (from DataRow dRow in dt.Rows
                                  select new { projectcode = dRow["projectcode"], projectname = dRow["projectname"] }).Distinct().ToList();

                foreach (var p in rawProject)
                {
                    dtProject.Rows.Add(p.projectcode.ToString(), p.projectname.ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string GetClusterFormattedString(string projectcode)
        {
            string result = "";
            try
            {
                DataTable dt = clsMaster.GetDataProjectCluster();

                var rawCluster = (from DataRow dRow in dt.Rows
                                  where dRow["projectcode"].ToString().ToLower() == projectcode.ToLower()
                                  select new { clustercode = dRow["clustercode"], clustername = dRow["clustername"] }).Distinct().ToList();

                foreach (var p in rawCluster)
                {
                    if (!Constants.clusterExclude.Contains(p.clustercode.ToString()))
                    {
                        result += p.clustercode.ToString() + ",";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool ValidateHasProjectData()
        {
            bool result = true;

            if (dtProject == null) result = false;
            else if (dtProject.Rows.Count <= 0) result = false;

            return result;
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

        private bool CheckIsProjectInException(string projectcode)
        {
            string[] projectExc = ConfigurationManager.AppSettings["projectExclude"].Split(',');

            return projectExc.Contains(projectcode) ? false : true;
        }

        private bool CheckIsProjectInAllowed(string projectcode)
        {
            string[] projectAllowed = ConfigurationManager.AppSettings["projectInclude"].Split(',');

            return projectAllowed.Contains(projectcode) ? true : false;
        }


        public bool RunGenerateReport()
        {
            log.DebugFormat("RunGenerateReport() - Start");
            bool result = true;

            try
            {
                for (int i = 0; i < dtProject.Rows.Count; i++)
                {
                    log.DebugFormat("RunGenerateReport() - Process {0}/{1} for Project {2}", i + 1, dtProject.Rows.Count, dtProject.Rows[i]["projectcode"].ToString());

                    string projectcode = dtProject.Rows[i]["projectcode"].ToString();

                    if (!CheckIsProjectInAllowed(projectcode))
                    {
                        log.DebugFormat("RunGenerateReport() - Process {0}/{1} Stopped because Project {2} is in exception.", i + 1, dtProject.Rows.Count, dtProject.Rows[i]["projectcode"].ToString());
                        continue;
                    }

                    string sCluster = GetClusterFormattedString(projectcode);
                    DateTime asofDate = DateTime.Now;

                    if (string.IsNullOrEmpty(sCluster))
                    {
                        log.DebugFormat("RunGenerateReport() - Process {0}/{1} Failed. Cannot found Cluster for Project {2} ", i + 1, dtProject.Rows.Count, dtProject.Rows[i]["projectcode"].ToString());
                        continue;
                    }


                    int batchid = clsSalesSummary.GenerateSalesSummary(projectcode, sCluster, asofDate, "System Generated");

                    if (batchid != null)
                    {
                        log.DebugFormat("RunGenerateReport() - Process {0}/{1} for Project {2}. BatchID : {3}", i + 1, dtProject.Rows.Count, dtProject.Rows[i]["projectcode"].ToString(), batchid);

                        //update collection
                        initDTFinal_CollectionDetail();
                        _DTFinal_CollectionDetail = clsSalesSummary.GenerateScheduleCollection(projectcode, sCluster);
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
                        initDTFinal_MktExpenseDetail();

                        log.DebugFormat("BudgetMkt Web Service [getBudgetMktActualExpenseDetail] Method Invoked.. Parameter sent: projectcode={0}", projectcode);
                        _DTFinal_MktExpenseDetail = Helper.ConvertXMLToDataset(BudgetMktWS.getBudgetMktActualExpenseDetail(projectcode)).Tables[0];
                        _DTFinal_MktExpenseDetail.Columns.Add("BatchID", System.Type.GetType("System.Int32"), batchid.ToString());
                        _DTFinal_MktExpenseDetail.Columns["BatchID"].SetOrdinal(0);

                        clsSalesSummary.InsertActualBudgetMktDetail(_DTFinal_MktExpenseDetail);


                        //actual Cost Contract
                        CostContractWS.WS_CostAndContract CostContractWS = new LKReportingSystem.CostContractWS.WS_CostAndContract();
                        CostContractWS.Timeout = 600000;
                        log.DebugFormat("CostContract Web Service [RetrieveProjectMapping] Method Invoked.. Parameter sent: projectcode={0}", projectcode);

                        initDT_CostContractProjectMapping();

                        if (CostContractWS.RetrieveProjectMapping(projectcode).Tables.Count != 0)
                        {
                            _DT_CostContractProjectMapping = CostContractWS.RetrieveProjectMapping(projectcode).Tables[0];

                            processActualConstructionCost(_DT_CostContractProjectMapping, batchid, asofDate);

                            clsSalesSummary.UpdateActualCost(_DT_CostContractProjectMapping, "System Generated");

                            clsSalesSummary.InsertActualCostContractDetail(_DTFinal_CostContractDetail);
                        }

                        clsSalesSummary.UpdateProjectByWsValue(batchid, PaidComm, UnpaidComm, BudgetMKTExpense);

                    }
                    else
                    {
                        log.DebugFormat("RunGenerateReport() - Failed to generate BatchId for project {0} and cluster {1}", projectcode, sCluster);
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("RunGenerateReport() - {0}", ex.Message);
                result = false;
            }

            log.DebugFormat("RunGenerateReport() - End");
            return result;
        }
    }
}