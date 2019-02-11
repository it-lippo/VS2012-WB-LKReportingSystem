using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LKReportingSystemExternal.Class
{
    public class Constants
    { 
        public static string LKAppConn = ConfigurationManager.ConnectionStrings["LKAppConn"].ConnectionString;
        public static string LippoMasterDBConn = ConfigurationManager.ConnectionStrings["LippoMasterDBConn"].ConnectionString;
        public static string PropertySystemDBConn = ConfigurationManager.ConnectionStrings["PropertySystemDBConn"].ConnectionString;
        public static string OthersAppDBConn = ConfigurationManager.ConnectionStrings["OthersAppDBConn"].ToString();

        public static string rootURL = ConfigurationManager.AppSettings["rootUrl"];
        public static string pathImgPromoLiberty = ConfigurationManager.AppSettings["PathImgPromoLiberty"];
        public static string pathImgArcPromoLiberty = ConfigurationManager.AppSettings["PathImgArcPromoLiberty"];
        public static int CmdTimeout = 0;
        public const string SessionUser = "";
        public static string[] AppID = new string[] { "APP00207" };
        public const string HomeUrl = "~/Login.aspx";
        public const int OrgID = 1;
        public const string urlLKUploader = "https://www.lippokarawaci.co.id/LKUploader/";


        public static string emailTo = ConfigurationManager.AppSettings["emailTo"];
        public static string emailCC = ConfigurationManager.AppSettings["emailCC"];
        public static string emailBCC = ConfigurationManager.AppSettings["emailBCC"];
        public static string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
        public static string emailFromDisplayName = ConfigurationManager.AppSettings["emailFromDisplayName"];

        public static string clusterExclude = ConfigurationManager.AppSettings["clusterExclude"];


        public const string usernameServer = "WebAdmin";
        public const string passwordServer = "W3bAdministrator1!";

        public static string sessionUsername
        {
            get
            {
                object value = HttpContext.Current.Session["sessionUsername"];
                return value == null ? "" : (string)value;
            }
            set
            {
                HttpContext.Current.Session["sessionUsername"] = value;
            }
        }       

    }
}