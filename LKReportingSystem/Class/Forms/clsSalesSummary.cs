using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using log4net;

namespace LKReportingSystem.Class.Forms
{
    public class clsSalesSummary
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsSalesSummary));

        public static DataTable GetDataDetailSalesReport(string batchid)
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataDetailSalesReport() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("select   batchdate, reportasofdate, projectcode, projectname, clustercode, clustername, ");
                    sb.Append("         initbudgetnoofunit, initbudgetmsquare, initbudgetvaluepermsquare, initbudgetvalueperunit, initbudgetvaluetotal, initpctsalesmktexpense, initvaluesalesmktexpense, ");
                    sb.Append("         InitBudgetValueLandCostPerMSquare, InitBudgetValueConstCostPerMSquare, InitBudgetValueCOGSPerMSquare, InitBudgetValueGrossMarginPerMSquare, initpctsalesmktexpense, initvaluesalesmktexpense, ");
                    sb.Append("         salesnoofunit, salesmsquarearea, salesvaluepermsquarearea, salesvaluetotal, salesmktvaluepermsquarearea, salesmktvaluetotal, actualpaidcomm, actualunpaidcomm, actualBudgetMKTExpense, ");
                    sb.Append("         inventorynoofunit, inventorymsquarearea, inventoryvaluepermsquarearea, inventoryvaluetotal, ");
                    sb.Append("         projectednoofunit, projectedmsquarearea, projectedvaluepermsquarearea, projectedvaluetotal, ");
                    //sb.Append("         ConstCostOriginalValueTotal, ConstCostPerMSquareArea, ");
                    sb.Append("         ActualValueDirectCostWithApportionment, ActualValueDirectCostWithApportionmentPerMSquareArea, ");
                    sb.Append("         ProjectedValueTotalTillCompletion, ProjectedValuePerMSquareAreaTillCompletion ");
                    sb.Append("from     dbo.tr_analysisreport with(nolock) ");
                    sb.Append("where    batchid = @batchid");

                    cmd.CommandText = sb.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataDetailSalesReport() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataDetailSalesReport() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataDetailSalesReport() Finished");
            return dt;
        }

        public static DataTable GetAllBatchID() {
            log.DebugFormat("GetAllBatchID() Called");

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select pm.ccprojectgroupcode as projectcode, ar.projectname, ar.batchid 
                                        from
		                                        (
			                                        select projectcode, projectname, max(batchid) as batchid
			                                        from   tr_analysisreport
			                                        group by projectcode, projectname
		                                        ) as ar
		                                        left join
		                                        (
		                                        select distinct 
		                                        stuff(( select distinct ', ' + ccprojectgroupcode
                                                        from lkcentralizeddb..ms_projectmapping              
				                                        where lippomasterprojectcode = a.lippomasterprojectcode
                                                        for xml path(''),type)
                                                        .value('.','nvarchar(max)'),1,2,'') as ccprojectgroupcode,
				                                        a.lippomasterprojectcode 
		                                        from lkcentralizeddb..ms_projectmapping a
		                                        group by a.lippomasterprojectcode
		                                        ) as pm
		                                        on ar.projectcode = pm.lippomasterprojectcode
                                        order by projectname";

                    //StringBuilder sb = new StringBuilder();                    

                    //sb.Append("SELECT ProjectCode, ProjectName, MAX(BatchID) AS BatchID ");
                    //sb.Append("FROM   TR_AnalysisReport ");
                    //sb.Append("GROUP BY ProjectCode, ProjectName");

                    //cmd.CommandText = sb.ToString();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandTimeout = Constants.CmdTimeout;

                    log.DebugFormat("GetAllBatchID() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetAllBatchID() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetAllBatchID() Finished");
            return dt;
        }

        public static DataTable GetHistorySalesSummary(string projectcode)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetHistorySalesSummary() Called.. Parameter sent: projectcode={0}", projectcode);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"select	distinct batchid, batchdate, reportasofdate, projectcode, projectname 
                                       from	    tr_analysisreport with(nolock)
                                       where	projectcode = @projectcode
                                       order by batchid desc";

                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("projectcode", projectcode));

                    log.DebugFormat("GetHistorySalesSummary() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                    return dt;

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetHistorySalesSummary() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                log.DebugFormat("GetHistorySalesSummary() Finished");

            }
        }

        public static int GenerateSalesSummary(string projectcode, string clustercode, DateTime asofdate, string username)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GenerateSalesSummary() Called.. Parameter sent: projectcode={0}, clustercode={1}, asofdate={2}, username={3}", projectcode, clustercode, asofdate, username);

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SPGenerateSalesSummary_Insert";
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("projectCode", projectcode));
                    cmd.Parameters.Add(new SqlParameter("clusterCode", clustercode));
                    cmd.Parameters.Add(new SqlParameter("asofdate", asofdate));
                    cmd.Parameters.Add(new SqlParameter("username", username));

                    SqlParameter batchidRlt = new SqlParameter("batchID", SqlDbType.Int);
                    batchidRlt.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(batchidRlt);

                    log.DebugFormat("GenerateSalesSummary() Query Sent");

                    cmd.ExecuteNonQuery();

                    int batchid = Convert.ToInt32(batchidRlt.Value.ToString());

                    log.DebugFormat("GenerateSalesSummary() Output Result: batchid={0}", batchid);

                    return batchid;
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GenerateSalesSummary() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
                log.DebugFormat("GenerateSalesSummary() Finished");
            }
        }

        public static DataSet GetDataSalesSummary(string projectcode, string clustercode)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataSalesSummary() Called.. Parameter sent: projectcode={0}, clustercode={1}", projectcode, clustercode);

            DataSet dt = new DataSet();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "";
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("projectcode", projectcode));
                    cmd.Parameters.Add(new SqlParameter("clustercode", clustercode));

                    log.DebugFormat("GetDataSalesSummary() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataSalesSummary() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataSalesSummary() Finished");
            return dt;
        }

        public static DataTable GetDataScheduleCollection(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataScheduleCollection() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();               
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select upper(paymentterm) as paymentterm, 
                                               sum(totalschedule) as totalschedule, 
                                               sum(totalcollection) as totalcollection, 
                                               round(sum(totalcollection)/sum(totalschedule) * 100,0) as pctcollection
                                        from tr_analysisreportcollection
                                        where batchid = @batchid
                                        group by paymentterm, projectcode";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));
     
                    log.DebugFormat("GetDataScheduleCollection() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataScheduleCollection() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataScheduleCollection() Finished");
            return dt;
        }

        public static DataTable GenerateScheduleCollection(string projectcode, string clustercode)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GenerateScheduleCollection() Called.. Parameter sent: projectcode={0}, clustercode={1}", projectcode, clustercode);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SPGetScheduleCollectionByProject";
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("projectcode", projectcode));
                    cmd.Parameters.Add(new SqlParameter("clustercode", clustercode));

                    log.DebugFormat("GenerateScheduleCollection() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GenerateScheduleCollection() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GenerateScheduleCollection() Finished");
            return dt;
        }

        public static string UpdateProjectByWsValue(int batchid, decimal paidcomm, decimal unpaidcomm, decimal salesmktexpense)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("UpdateProjectByWsValue() Called.. Parameter sent: batchid={0}, paidcomm={1}, salesmktexpense={2}", batchid, paidcomm, salesmktexpense);

            string result = "";

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"update top (1) tr_analysisreport set actualpaidcomm = @paidcomm, actualunpaidcomm = @unpaidcomm, ActualBudgetMKTExpense = @salesmktexpense where batchid = @batchid";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));
                    cmd.Parameters.Add(new SqlParameter("paidcomm", paidcomm));
                    cmd.Parameters.Add(new SqlParameter("unpaidcomm", unpaidcomm));
                    cmd.Parameters.Add(new SqlParameter("salesmktexpense", salesmktexpense));

                    log.DebugFormat("UpdateProjectByWsValue() Query Sent.");

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("UpdateProjectByWsValue() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("UpdateProjectByWsValue() Finished..");
            return result;
        }

        public static string InsertActualBudgetMktDetail(DataTable salesmktexpensedetail)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("InsertActualBudgetMktDetail() Called..");

            string result = "";

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Constants.PropertySystemDBConn))
                {
                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "TR_AnalysisReportMktExpense";
                    bulkCopy.WriteToServer(salesmktexpensedetail);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("InsertActualBudgetMktDetail() ERROR. Message : {0}", ex.Message);
                throw;
            }

            log.DebugFormat("InsertActualBudgetMktDetail() Finished");            
            return result;
        }

        public static string InsertActualCostContractDetail(DataTable costcontractdetail)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("InsertActualCostContractDetail() Called..");

            string result = "";

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Constants.PropertySystemDBConn))
                {
                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "TR_AnalysisReportCostContract";
                    bulkCopy.WriteToServer(costcontractdetail);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("InsertActualCostContractDetail() ERROR. Message : {0}", ex.Message);
                throw;
            }

            log.DebugFormat("InsertActualCostContractDetail() Finished");
            return result;
        }

        public static string InsertScheduleCollectionDetail(DataTable schedulecollectiondetail)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("InsertScheduleCollectionDetail() Called..");

            string result = "";

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Constants.PropertySystemDBConn))
                {
                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "TR_AnalysisReportCollection";
                    bulkCopy.WriteToServer(schedulecollectiondetail);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("InsertScheduleCollectionDetail() ERROR. Message : {0}", ex.Message);
                throw;
            }

            log.DebugFormat("InsertScheduleCollectionDetail() Finished");
            return result;
        }

        public static DataTable GetDataBudgetMktDetail(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataBudgetMktDetail() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    StringBuilder sb = new StringBuilder();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    cmd.CommandText = @"select mp1.projectName, mp.projectCode + '<br \>' + mp.projectName AS Project, mpb.budgetCode + ' Rev. ' + convert(varchar(max),tarme.budgetrevno) + '<br \>' + mpb.budgetName AS Budget, tarme.itemcode + '<br \>' + tarme.itemname as Item, tarme.amount as Amount,  tarme.IANo + '<br \>' + tarme.IADesc as InternalApproval, convert(varchar(max),tarme.approvedate, 106) + '<br \>By: ' + tarme.approveun as ApproveDate
                                        from tr_analysisreportmktexpense tarme
                                        join budgetmkt..ms_project mp on tarme.projectcode = mp.projectcode
                                        join budgetmkt..ms_projectbudget mpb on tarme.projectcode = mpb.projectcode and tarme.budgetcode = mpb.budgetcode
                                        join lippomaster..ms_company mc on tarme.devcode = mc.cocode
                                        join lippomaster..ms_project mp1 on mp.lippomasterprojectcode = mp1.projectcode
                                        where batchid = @batchid
                                        order by tarme.approvedate desc";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataBudgetMktDetail() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataBudgetMktDetail() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataDetailSalesReport() Finished");
            return dt;
        }

        public static DataTable GetDataScheduleCollectionDetail(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataScheduleCollectionDetail() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select  c.projectname,
                                                upper(a.paymentterm) as paymentterm, 
		                                        b.clustername,
                                                sum(a.totalschedule) as totalschedule, 
                                                sum(a.totalcollection) as totalcollection, 
                                                sum(a.totalschedule) - sum(a.totalcollection) as totaloutstanding,
                                                round(sum(a.totalcollection)/sum(a.totalschedule) * 100,0) as pctcollection
                                        from tr_analysisreportcollection a
                                        join lippomaster..ms_cluster b on a.clustercode = b.clustercode
                                        join lippomaster..ms_project c on a.projectcode = c.projectcode
                                        where a.batchid = @batchid
                                        group by c.projectname,a.paymentterm, a.projectcode, b.clustername
                                        order by a.paymentterm, b.clustername";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataScheduleCollectionDetail() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataScheduleCollectionDetail() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataScheduleCollectionDetail() Finished");
            return dt;
        }

        public static DataTable GetCostContractDetail(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetCostContractDetail() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select *
                                        from (select
	                                        c.projectname,
                                            a.projectcode,
	                                        a.wbscode + '<br \>' + a.wbsname as wbsname,
                                            a.currrate,
	                                        a.currcode  + '<br \> Rate:' + convert(varchar(max),a.currrate) as currcode,
	                                        a.header,
	                                        a.apportionment
                                        from tr_analysisreportcostcontract a
                                        join (select top 1 batchid, projectcode from tr_analysisreport where batchid = @batchid) b on a.batchid = b.batchid
                                        join lippomaster..ms_project c on b.projectcode = c.projectcode
                                        where a.batchid = @batchid and a.apportionment > 0) as s
                                        pivot
                                        (
                                        sum(apportionment)
                                        for header in (directcostwithcostapportionment)
                                        ) as pvt";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetCostContractDetail() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetCostContractDetail() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetCostContractDetail() Finished");
            return dt;
        }

        public static DataTable GetDataCommission(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataCommission() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    cmd.CommandText = @"select max(actualpaidcomm) as actualpaidcomm, max(actualunpaidcomm) as actualunpaidcomm
                                        from   tr_analysisreport
                                        where  batchid = @batchid";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataCommission() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataCommission() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataCommission() Finished");
            return dt;
        }

        public static decimal GetDataPaidSalary(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataPaidSalary() Called.. Parameter sent: batchid={0}", batchid);

            decimal result = 0;

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    cmd.CommandText = @"declare @projectcode varchar(3)
                                        declare @salaryvalue money
                                        set @projectcode = (select top 1 projectcode from tr_analysisreport where batchid = @batchid)
                                        set @salaryvalue = (select sum(salaryvalue) from tr_analysisreportprojectsalary where projectcode = @projectcode)
                                        if @salaryvalue is null
                                            begin 
                                            set @salaryvalue = 0
                                            end
                                        select @salaryvalue";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataPaidSalary() Query Sent");

                    result = (decimal)cmd.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataPaidSalary() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataPaidCommission() Finished");
            return result;
        }

        public static DataTable GetDataPaidSalaryDetail(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataPaidSalaryDetail() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    StringBuilder sb = new StringBuilder();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    cmd.CommandText = @"declare @projectcode varchar(3)
                                        set @projectcode = (select top 1 projectcode from tr_analysisreport where batchid = @batchid)
                                        select salaryyear, datename( month , dateadd( month , salarymonth , 0 ) - 1 ) as salarymonth, salaryvalue
                                        from   tr_analysisreportprojectsalary
                                        where  projectcode = @projectcode";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataPaidSalaryDetail() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataPaidSalaryDetail() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataPaidSalaryDetail() Finished");
            return dt;
        }

        public static string UpdateActualCost(DataTable dtData, string username)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("UpdateActualCost() Called.. Parameter sent: dtData={0}, username={1}", Helper.ConvertDataTableToXML(dtData), username);

            string result = "";

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))

                try
                {
                    string xmlData = Helper.ConvertDataTableToXML(dtData);

                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "SPUpdateActualCost";

                    cmd.Parameters.Add(new SqlParameter("xmlData", xmlData));
                    cmd.Parameters.Add(new SqlParameter("username", username));

                    log.DebugFormat("UpdateActualCost() Query Sent");

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("UpdateActualCost() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            log.DebugFormat("UpdateActualCost() Finished");
            return result;
        }

        public static DataTable GetDataCashflow(string batchid)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataCashflow() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"select CashflowItem, CashflowValue
                                        from   TR_AnalysisReportCashflow
                                        where  batchid = @batchid";

                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataCashflow() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataCashflow() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataCashflow() Finished");
            return dt;
        }

        public static string GenerateAsOfDate()
        {
            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn)) 
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select distinct      ReportAsOfDate from tr_AnalysisReport 
                                        where                batchID = (Select MAX(batchID) 
                                                                        from TR_AnalysisReport)";

                    DateTime date = (DateTime) cmd.ExecuteScalar();

                    return date.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally 
                {
                    conn.Close();
                }
            }
        }

        public static DataTable GetDataTargetSales(int yearperiod)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataTargetSales() Called.. Parameter sent: year={0}", yearperiod);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT		DISTINCT mu.projectCode, mp.projectName, mu.clusterCode, mc.clusterName, 
			                                        mptMKT.monthperiod, mptMKT.yearperiod,  
			                                        mptMKT.JantargetAmt AS JanTargetAmtMKT, mptMKT.FebTargetAmt AS FebTargetAmtMKT, mptMKT.MarTargetAmt AS MarTargetAmtMKT,
			                                        mptMKT.AprTargetAmt AS AprTargetAmtMKT, mptMKT.MayTargetAmt AS MayTargetAmtMKT, mptMKT.JunTargetAmt AS JunTargetAmtMKT,
			                                        mptMKT.JulTargetAmt AS JulTargetAmtMKT, mptMKT.AugTargetAmt AS AugTargetAmtMKT, mptMKT.SepTargetAmt AS SepTargetAmtMKT,
			                                        mptMKT.OctTargetAmt AS OctTargetAmtMKT, mptMKT.NovTargetAmt AS NovTargetAmtMKT, mptMKT.DecTargetAmt AS DecTargetAmtMKT,
			                                        mptCorp.JantargetAmt AS JanTargetAmtCorp, mptCorp.FebTargetAmt AS FebTargetAmtCorp, mptCorp.MarTargetAmt AS MarTargetAmtCorp,
			                                        mptCorp.AprTargetAmt AS AprTargetAmtCorp, mptCorp.MayTargetAmt AS MayTargetAmtCorp, mptCorp.JunTargetAmt AS JunTargetAmtCorp,
			                                        mptCorp.JulTargetAmt AS JulTargetAmtCorp, mptCorp.AugTargetAmt AS AugTargetAmtCorp, mptCorp.SepTargetAmt AS SepTargetAmtCorp,
			                                        mptCorp.OctTargetAmt AS OctTargetAmtCorp, mptCorp.NovTargetAmt AS NovTargetAmtCorp, mptCorp.DecTargetAmt AS DecTargetAmtCorp
                                        FROM		LippoMaster..MS_Unit mu
                                        JOIN		LippoMaster..MS_Project mp ON mu.projectCode = mp.projectCode
                                        JOIN		LippoMaster..MS_Cluster mc ON mu.clusterCode = mc.clusterCode
                                        LEFT JOIN	MS_ProjectTarget mptMKT ON mptMKT.projectCode = mu.projectCode and (mptMKT.clusterCode = mu.clusterCode OR mptMKT.clusterCode = 'ALL') AND mptMKT.targetType = 'MKT'
                                        LEFT JOIN	MS_ProjectTarget mptCorp ON mptCorp.projectCode = mu.projectCode and (mptCorp.clusterCode = mu.clusterCode OR mptCorp.clusterCode = 'ALL') AND mptCorp.targetType = 'CORP'
                                        WHERE		(mptMKT.yearperiod = @yearperiod OR mptMKT.yearperiod IS null )
                                        AND			(mptCorp.yearperiod = @yearperiod OR mptCorp.yearperiod IS null )
                                        ORDER by	mu.projectCode, mu.clusterCode";

                    cmd.Parameters.Add(new SqlParameter("yearperiod", yearperiod));

                    log.DebugFormat("GetDataTargetSales() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataTargetSales() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataTargetSales() Finished");
            return dt;
        }
    }
}