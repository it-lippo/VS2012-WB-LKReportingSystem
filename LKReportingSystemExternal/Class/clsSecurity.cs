using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using log4net;

namespace LKReportingSystemExternal.Class
{ 

    public class clsSecurity
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(clsSecurity));

        public string UserName { get; set; }
        public DataTable ProjectCode { get; set; }
        public List<string> UserRoles { get; set; }
        public DataTable UserPermission { get; set; }

        public static DataTable GetUserPermission(decimal logID, int orgID, string appID, string userName)
        {
            //log4net.Config.XmlConfigurator.Configure();

            log.DebugFormat("Start GetUserPermission(). Param Received : {0}" +
                            "       LogID : {1}{0}" +
                            "       OrgID : {2}{0}" +
                            "       AppID : {3}{0}" +
                            "       Username : {4}{0}", Environment.NewLine, logID, orgID, appID, userName);

            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(Constants.LKAppConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SPUserPermission_Get";
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrgID", orgID));
                    cmd.Parameters.Add(new SqlParameter("AppID", appID));
                    cmd.Parameters.Add(new SqlParameter("UserName", userName));

                    log.DebugFormat("GetUserPermission(). Retrieve data Permission for username {0}", userName);

                    SqlDataAdapter oAdapter = new SqlDataAdapter();
                    oAdapter.SelectCommand = cmd;

                    oAdapter.Fill(data);
                    oAdapter.Dispose();

                }
                catch (Exception ex)
                {
                    log.ErrorFormat("ERROR GetUserPermission(). Message : {0}", ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }


            log.DebugFormat("End GetUserPermission() for Username : {0}", userName);

            return data;
        }

        public static clsSecurity GetUser(decimal logID, int orgID, string[] appID, string userName)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.DebugFormat("Start GetUser(). Param Received : {0}" +
                            "       LogID : {1}{0}" +
                            "       OrgID : {2}{0}" +
                            "       AppID : {3}{0}" +
                            "       Username : {4}{0}", Environment.NewLine, logID, orgID, appID, userName);

            try
            {
                clsSecurity oUser = null;
                DataTable dtUserPermission = new DataTable();

                for (int i = 0; i < appID.Length; i++)
                {
                    DataTable dtTemp = GetUserPermission(logID, orgID, appID[i], userName);

                    dtUserPermission.Merge(dtTemp);
                }

                log.DebugFormat("There are {0} data Permission for username {1}", dtUserPermission.Rows.Count, userName);

                if (dtUserPermission.Rows.Count > 0)
                {
                    log.DebugFormat("Create object clsSecurity for username {0} and Total Page Allowed {1}", userName, dtUserPermission.Rows.Count);

                    oUser = new clsSecurity();
                    oUser.UserName = dtUserPermission.Rows[0]["UserName"].ToString();
                    oUser.UserPermission = dtUserPermission;

                    oUser.UserRoles = new List<string>();
                    foreach (DataRow row in dtUserPermission.Rows)
                    {
                        oUser.UserRoles.Add(row["RoleName"].ToString());
                    }
                }

                log.DebugFormat("End GetUser().");

                return oUser;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error GetUser(). Message : {0}", ex.Message);
                throw;
            }

            log.DebugFormat("End GetUser().");
        }

        public static void InsertLoginUser(string tableNo, string projectCode)
        {
            using (SqlConnection conn = new SqlConnection(Constants.LKAppConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SPTEMP_TableUser_Insert";
                    cmd.CommandTimeout = Constants.CmdTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("UserName", Helper.GetUserName()));
                    cmd.Parameters.Add(new SqlParameter("TableNo", tableNo));
                    cmd.Parameters.Add(new SqlParameter("ProjectCode", projectCode));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public static bool HaveAccess(string pageUrl)
        {
            DataTable dtUserPermission = Helper.GetLoginUser().UserPermission;
            DataRow[] rows = dtUserPermission.Select("AppPageURL='" + pageUrl + "'");

            return rows.Length > 0;
        }

        public static bool HaveAccessAction(string pageUrl, string action)
        {
            string actiontype = "";

            if (action == "delete") actiontype = "isVoid";
            else if (action == "save") actiontype = "isSave";
            else if (action == "edit") actiontype = "isEdit";
            else if (action == "print") actiontype = "isPrint";

            DataTable dtUserPermission = Helper.GetLoginUser().UserPermission;
            DataRow[] rows = dtUserPermission.Select("AppPageURL='" + pageUrl + "' AND " + actiontype + " = 1");

            return rows.Length > 0;
        }

        public static bool ValidateUserRole(string userRole)
        {
            clsSecurity oUser = Helper.GetLoginUser();

            return oUser.UserRoles.Contains(userRole, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}