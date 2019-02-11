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
    public class clsConstructionCost
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsInitialBudgetSetup));

        public static DataTable GetDataConstructionCost(int batchid)
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataConstructionCost() Called.. Parameter sent: batchid={0}", batchid);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"select clustercode, clustername, 
                                               InitBudgetValueConstCostPerMSquare,
                                               InitBudgetValueConstCostPerMSquare * InitBudgetMSquare as InitBudgetValueTotal, 
                                               ProjectedValueTotalTillCompletion, 
                                               ProjectedValuePerMSquareAreaTillCompletion
                                        from   tr_analysisreport
                                        where  batchid = @batchid";

                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.Parameters.Add(new SqlParameter("batchid", batchid));

                    log.DebugFormat("GetDataConstructionCost() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataConstructionCost() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataConstructionCost() Finished");
            return dt;
        }

        public static string UpdateConstructionCost(DataTable dtData, string username)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("UpdateConstructionCost() Called.. Parameter sent: dtData={0}, username={1}", Helper.ConvertDataTableToXML(dtData), username);

            string result = "";

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))

                try
                {
                    string xmlData = Helper.ConvertDataTableToXML(dtData);

                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "SPUpdateConstructionCost";

                    cmd.Parameters.Add(new SqlParameter("xmlData", xmlData));
                    cmd.Parameters.Add(new SqlParameter("username", username));

                    log.DebugFormat("UpdateConstructionCost() Query Sent");

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("UpdateConstructionCost() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            log.DebugFormat("UpdateConstructionCost() Finished");
            return result;
        }
    }
}