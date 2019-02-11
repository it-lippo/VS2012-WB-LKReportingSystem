using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using log4net;

namespace LKReportingSystem.Class
{
    public class clsMaster
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsMaster));

        public static DataTable GetDataProjectCluster()
        {
            log.DebugFormat("GetDataProject() Started..");
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.LippoMasterDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("select   distinct mp.projectcode, mp.projectname, mp.projectcode + ' - ' + mp.projectname as projectlabel, ");
                    sb.Append("         mc.clustercode, mc.clustername, mc.clustercode + ' - ' + mc.clustername as clusterlabel, mu.categoryCode ");
                    sb.Append("from     dbo.ms_unit mu with(nolock) ");
                    sb.Append("join     dbo.ms_project mp with(nolock) on mp.projectcode = mu.projectcode ");
                    sb.Append("join     dbo.ms_cluster mc with(nolock) on mc.clustercode = mu.clustercode ");
                    sb.Append("order by mp.projectname, mc.clustername ");


                    cmd.CommandText = sb.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    log.DebugFormat("GetDataProject() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataProject() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataProject() Finished.");
            return dt;
        }

        public static DataTable GetDataNotifType()
        {
            log.DebugFormat("GetDataNotifType() Started..");
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.LippoMasterDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("select   notiftypecode, notiftypedesc, parentType ");
                    sb.Append("from     MS_NotificationType with(nolock) ");

                    cmd.CommandText = sb.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    log.DebugFormat("GetDataNotifType() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();


                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataNotifType() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            log.DebugFormat("GetDataNotifType() Finished.");
            return dt;
        }
    }
}