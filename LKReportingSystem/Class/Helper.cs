using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using log4net;
using System.Web.UI;

namespace LKReportingSystem.Class
{
    public class Helper
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Helper));

        public static void JQueryErrMsg(string StrMessage, object Page)
        {
            Control Ctr = (Control)Page;
            ScriptManager.RegisterStartupScript(Ctr, Ctr.GetType(), "BootboxAlert" + StrMessage, "bootbox.alert('" + StrMessage.Replace("'", "\\'") + "');", true);
        }

        public static String GetFormattedErrorMessage(string message) {
            return String.Format("<div class='panel panel-red'> <div class='panel-heading'> <div class='row'> <div class='col-xs-3'><i class='fa fa-comment fa-5x'></i></div><div class='col xs-9 text-right'> <div style='font-weight:bold'> Oops! </div> <div>Some Field Empty</div></div></div></div> <a href='#'> <div class='panel-footer'> <span class='pull-left'>More Details</span> <span class='pull-right'><i class='fa fa-arrow-circle-right'></i></span> <div class='clearfix'></div></div></a></div>");
        }

        public static string GetUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static clsSecurity GetLoginUser()
        {
            clsSecurity oUser = HttpContext.Current.Session[Constants.SessionUser] as clsSecurity;  
            if (oUser == null)
            {
                HttpContext.Current.Response.Redirect(Constants.HomeUrl);
            }
            return oUser;
        }

        public static DataSet ConvertXMLToDataset(string xmlData)
        {
            StringReader theReader = new StringReader(xmlData);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet;
        }

        public static string ConvertDataTableToXML(DataTable dtData)
        {
            DataSet dsData = new DataSet();
            StringBuilder sbSQL = null;
            StringWriter swSQL = null;
            string XMLformat = null;
            try
            {
                sbSQL = new StringBuilder();
                swSQL = new StringWriter(sbSQL);
                dsData.Merge(dtData, true, MissingSchemaAction.AddWithKey);
                dsData.Tables[0].TableName = "SampleDataTable";
                foreach (DataColumn col in dsData.Tables[0].Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dsData.WriteXml(swSQL, XmlWriteMode.WriteSchema);
                XMLformat = sbSQL.ToString();
                return XMLformat;
            }
            catch (Exception sysException)
            {
                throw sysException;
            }
        }

        public static DataTable GetDataFromExcel(string filePath)
        {
            string result = string.Empty;
            string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1""";
            DataTable data = new DataTable() { TableName = "Unit" };

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                try
                {
                    conn.Open();

                    //Get the name of First Sheet
                    DataTable dtExcelSchema;
                    dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                    OleDbDataAdapter da = new OleDbDataAdapter();

                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * From [" + SheetName + "]";
                    da.SelectCommand = cmd;
                    da.Fill(data);
                    da.Dispose();

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            return data;
        }

        public static string GetLoginName
        {
            get
            {
                clsSecurity oUser = GetLoginUser();

                return oUser.UserName;
            }
        }

        public static bool isSmallScreenBrowser(decimal screenWidth, decimal screenHeight)
        {
            bool result = false;

            if (screenWidth <= 800 && screenHeight <= 700) result = true;

            return result;
        }
   
    }

    public static class DataTableHelper
    {
        public static DataTable MergeAll(this IList<DataTable> tables, String primaryKeyColumn)
        {
            if (!tables.Any())
                throw new ArgumentException("Tables must not be empty", "tables");
            if (primaryKeyColumn != null)
                foreach (DataTable t in tables)
                    if (!t.Columns.Contains(primaryKeyColumn))
                        throw new ArgumentException("All tables must have the specified primarykey column " + primaryKeyColumn, "primaryKeyColumn");

            if (tables.Count == 1)
                return tables[0];

            DataTable table = new DataTable("TblUnion");
            table.BeginLoadData(); // Turns off notifications, index maintenance, and constraints while loading data
            foreach (DataTable t in tables)
            {
                table.Merge(t); // same as table.Merge(t, false, MissingSchemaAction.Add);
            }
            table.EndLoadData();

            if (primaryKeyColumn != null)
            {
                // since we might have no real primary keys defined, the rows now might have repeating fields
                // so now we're going to "join" these rows ...
                var pkGroups = table.AsEnumerable()
                    .GroupBy(r => r[primaryKeyColumn]);
                var dupGroups = pkGroups.Where(g => g.Count() > 1);
                foreach (var grpDup in dupGroups)
                {
                    // use first row and modify it
                    DataRow firstRow = grpDup.First();
                    foreach (DataColumn c in table.Columns)
                    {
                        if (firstRow.IsNull(c))
                        {
                            DataRow firstNotNullRow = grpDup.Skip(1).FirstOrDefault(r => !r.IsNull(c));
                            if (firstNotNullRow != null)
                                firstRow[c] = firstNotNullRow[c];
                        }
                    }
                    // remove all but first row
                    var rowsToRemove = grpDup.Skip(1);
                    foreach (DataRow rowToRemove in rowsToRemove)
                        table.Rows.Remove(rowToRemove);
                }
            }

            return table;
        }
    }
}