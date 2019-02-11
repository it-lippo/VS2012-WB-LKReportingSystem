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
    public class clsMailPromo
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsMailPromo));

        public static DataTable GetDataPromo()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataPromo() Called.. ");

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT promoid, promotype, promotitle, promoimage, promoarchiveimage, promourl, startdate, enddate, isactive, modiftime, modifun, inputtime, inputun ");
                    sb.AppendLine("FROM TR_MailPromo ");

                    cmd.CommandText = sb.ToString();
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    log.DebugFormat("GetDataPromo() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataPromo() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataPromo() Finished");
            return dt;
        }


        public static DataTable GetDataSendPromo()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataSendPromo() Called.. ");

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT tsmp.promoid, tmp.promotitle, tmp.promoArchiveImage, tsmp.notifTypeCode, monthperiod, yearperiod, totalsend, totalclick ");
                    sb.AppendLine("FROM TR_SendMailPromo tsmp ");
                    sb.AppendLine("JOIN TR_MailPromo tmp ON tsmp.promoid = tmp.promoid ");

                    cmd.CommandText = sb.ToString();
                    cmd.CommandTimeout = Constants.CmdTimeout;

                    log.DebugFormat("GetDataSendPromo() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataSendPromo() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataSendPromo() Finished");
            return dt;
        }

        public static string InsertDataSetupPromo(string promotype, string promotitle, string promourl, DateTime startdate, 
                                    DateTime enddate, string promoimage, string archiveimage, string username)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("Start InsertDataSetupPromo(). Param Received {0}" +
                            "       Promo Type : {1}{0}" +
                            "       Promo Title : {2}{0}" +
                            "       Promo URL : {3}{0}" +
                            "       Start Date : {4}{0}" +
                            "       End Date : {5}{0}" +
                            "       Promo Image : {6}{0}" +
                            "       Archive Image : {7}{0}" +
                            "       Username : {8}", Environment.NewLine, promotype, promotitle, promourl, startdate, enddate, promoimage, archiveimage, username);

            using (SqlConnection conn = new SqlConnection(Constants.PropertySystemDBConn))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction("MailPromoSetup");

                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE TR_MailPromo SET isactive = 0 WHERE promotype = @promotype");

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.Add(new SqlParameter("promotype", promotype));
                    cmd.ExecuteNonQuery();


                    cmd.Parameters.Clear();
                    sb.Clear();

                    sb.Append("INSERT INTO TR_MailPromo (promotype, promotitle, promourl, startdate, enddate, promoimage, promoarchiveimage, isactive, ");
                    sb.Append("                     modifTime,modifUN,inputTime,inputUN) ");
                    sb.Append("VALUES (@promotype, @promotitle, @promourl, @startdate, @enddate, @promoimage, @promoarchiveimage, @isactive, ");
                    sb.Append("        getdate(), @username, getdate(), @username)");


                    cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.Add(new SqlParameter("promotype", promotype));
                    cmd.Parameters.Add(new SqlParameter("promotitle", promotitle));
                    cmd.Parameters.Add(new SqlParameter("promourl", promourl));
                    cmd.Parameters.Add(new SqlParameter("startdate", startdate));
                    cmd.Parameters.Add(new SqlParameter("enddate", enddate));
                    cmd.Parameters.Add(new SqlParameter("promoimage", promoimage));
                    cmd.Parameters.Add(new SqlParameter("promoarchiveimage", archiveimage));
                    cmd.Parameters.Add(new SqlParameter("isactive", "1"));
                    cmd.Parameters.Add(new SqlParameter("username", username));

                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    return "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    log.ErrorFormat("Error InsertDataSetupPromo(). Message : {0} . Invoked by {1}", ex.Message, Helper.GetLoginName);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        public static DataTable GetDataUndeliveredEmail(string notifTypeCode, DateTime startPeriod, DateTime endPeriod)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataUndeliveredEmail() Called.. ");

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.LippoMasterDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();

                    sb.Append("SELECT	tn.notifCode, tpu.unitCode, tpu.unitNo, tpu.psCode, name as psname, tn.notifTypeCode, mnt.notifTypeDesc, tpu.sentTime, ");
                    sb.AppendLine("        tpu.emailAddress, tpu.emailSubject, tpu.resultMessage AS result, tn.message ");
                    sb.AppendLine("FROM	LKMailDB..TR_PSASNotif_UndeliveredEmail tpu ");
                    sb.AppendLine("JOIN	LippoMaster..TR_Notification tn on tpu.notifCode = tn.notifCode ");
                    sb.AppendLine("JOIN	LippoMaster..MS_NotificationType mnt on tn.notifTypeCode = mnt.notifTypeCode ");
                    sb.AppendLine("JOIN	Personals..PERSONALS p ON tpu.psCode = p.psCode ");
                    sb.AppendLine("WHERE	DATEDIFF(DAY, tpu.sentTime, @startperiod) <= 0 ");
                    sb.AppendLine("AND		DATEDIFF(DAY, tpu.sentTime, @endperiod) >= 0 ");
                    sb.AppendLine("AND		tn.notiftypecode IN (Select ltrim(rtrim(Param)) from fn_MVParam (@notiftypecode,',')) ");
                    sb.AppendLine("ORDER by tpu.sentTime ");

                    cmd.CommandText = sb.ToString();
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.Parameters.Add(new SqlParameter("startperiod", startPeriod));
                    cmd.Parameters.Add(new SqlParameter("endperiod", endPeriod));
                    cmd.Parameters.Add(new SqlParameter("notiftypecode", notifTypeCode));

                    log.DebugFormat("GetDataUndeliveredEmail() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataUndeliveredEmail() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataUndeliveredEmail() Finished");
            return dt;
        }

        public static DataTable GetDataUndeliveredSMS(string notifTypeCode, DateTime startPeriod, DateTime endPeriod)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("GetDataUndeliveredSMS() Called.. ");

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.LippoMasterDBConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    StringBuilder sb = new StringBuilder();

                    sb.Append("SELECT	tn.notifCode, tpu.unitCode, tpu.unitNo, tpu.psCode, name as psname, tn.notifTypeCode, mnt.notifTypeDesc, tpu.sentTime, ");
                    sb.AppendLine("        tpu.phoneNo, tpu.smsSubject, tpu.resultMessage AS result, tn.message ");
                    sb.AppendLine("FROM	LKMailDB..TR_PSASNotif_UndeliveredSMS tpu ");
                    sb.AppendLine("JOIN	LippoMaster..TR_Notification tn on tpu.notifCode = tn.notifCode ");
                    sb.AppendLine("JOIN	LippoMaster..MS_NotificationType mnt on tn.notifTypeCode = mnt.notifTypeCode ");
                    sb.AppendLine("JOIN	Personals..PERSONALS p ON tpu.psCode = p.psCode ");
                    sb.AppendLine("WHERE	DATEDIFF(DAY, tpu.sentTime, @startperiod) <= 0 ");
                    sb.AppendLine("AND		DATEDIFF(DAY, tpu.sentTime, @endperiod) >= 0 ");
                    sb.AppendLine("AND		tn.notiftypecode IN (Select ltrim(rtrim(Param)) from fn_MVParam (@notiftypecode,',')) ");
                    sb.AppendLine("ORDER by tpu.sentTime ");

                    cmd.CommandText = sb.ToString();
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.Parameters.Add(new SqlParameter("startperiod", startPeriod));
                    cmd.Parameters.Add(new SqlParameter("endperiod", endPeriod));
                    cmd.Parameters.Add(new SqlParameter("notiftypecode", notifTypeCode));

                    log.DebugFormat("GetDataUndeliveredSMS() Query Sent");

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(dt);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("GetDataUndeliveredSMS() ERROR. Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            log.DebugFormat("GetDataUndeliveredSMS() Finished");
            return dt;
        }
    }
}