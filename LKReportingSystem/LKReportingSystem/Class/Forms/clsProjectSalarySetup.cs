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
    public class clsProjectSalarySetup
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsProjectSalarySetup));

        public static DataTable GetDataDetailProjecSalary(string projectcode)
        {
            //log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataDetailProjecSalary() Called.. Parameter sent: projectcode={0}", projectcode);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"select projectcode, salaryyear, salarymonth, salaryvalue	
                                        from   TR_AnalysisReportProjectSalary
                                        where  projectcode = @projectcode
                                        order by salaryyear desc, salarymonth desc";

                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.Parameters.Add(new SqlParameter("projectcode", projectcode));

                    log.DebugFormat("GetDataDetailProjectInformation() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataDetailProjecSalary() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataDetailProjecSalary() Finished");
            return dt;
        }

        public static string UpdateProjectSalary(string projectcode, int salaryyear, int salarymonth, decimal salaryvalue, string username)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("UpdateProjectSalary() Called.. Parameter sent: projectcode={0}, salarymonth={1}, salaryyear={2}, username={3}", projectcode, salaryyear, salaryvalue, username);

            string result = "";

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))

                try
                {                   

                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "SPUpdateProjectSalary";

                    cmd.Parameters.Add(new SqlParameter("projectcode", projectcode));
                    cmd.Parameters.Add(new SqlParameter("salarymonth", salarymonth));
                    cmd.Parameters.Add(new SqlParameter("salaryyear", salaryyear));
                    cmd.Parameters.Add(new SqlParameter("salaryvalue", salaryvalue));
                    cmd.Parameters.Add(new SqlParameter("username", username));

                    log.DebugFormat("UpdateProjectSalary() Query Sent");

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("UpdateProjectSalary() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            log.DebugFormat("UpdateProjectSalary() Finished");
            return result;
        }
    
    }
}