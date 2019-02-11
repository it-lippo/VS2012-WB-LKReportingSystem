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
    public class clsCashflow
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsCashflow));



        public static Boolean CashflowIsExist(int batchid)
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("CashflowIsExist() Called.. Parameter sent: batchid={0}", batchid);

            int count = 0;
            Boolean resultDB = false;

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"select  count(*)
                                        from    TR_AnalysisReportCashflow 
                                        where  batchid = @batchid";

                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("CashflowIsExist() Query Sent.");

                    count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        resultDB = true;
                    }
                    else
                    {
                        resultDB = false;
                    }

                    log.DebugFormat("CashflowIsExist() Query Received.");
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("CashflowIsExist() - {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            log.Info("GetCashflowIsExist() Finished.. ");
            return resultDB;
        }

        public static string InsertCashflow(DataTable dtcashflow)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("InsertCashflow() Called..");

            string result = "";

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Constants.PropertySystemDBConn))
                {
                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "TR_AnalysisReportCashflow";
                    bulkCopy.WriteToServer(dtcashflow);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("InsertCashflow() ERROR. Message : {0}", ex.Message);
                throw;
            }

            log.DebugFormat("InsertCashflow() Finished");
            return result;
        }

        public static string UpdateCashflow(DataTable dtcashflow, string username)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("UpdateCashflow() Called.. Parameter sent: dtData={0}, username={1}", Helper.ConvertDataTableToXML(dtcashflow), username);

            string result = "";

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))

                try
                {
                    string xmlData = Helper.ConvertDataTableToXML(dtcashflow);

                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "SPUpdateCashflow";

                    cmd.Parameters.Add(new SqlParameter("xmlData", xmlData));
                    cmd.Parameters.Add(new SqlParameter("username", username));

                    log.DebugFormat("UpdateCashflow() Query Sent");

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("UpdateCashflow() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            log.DebugFormat("UpdateCashflow() Finished");
            return result;
        }

        public static DataTable GetDataCashflow(int batchid)
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

    }
}