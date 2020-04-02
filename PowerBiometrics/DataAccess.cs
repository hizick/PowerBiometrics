using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PowerBiometrics
{
    public static class DataAccess
    {
        public static SqlConnection _conn;
        public static SqlCommand sqlCmd;
        public static SqlDataAdapter _sqlDta;
        public static DataTable _dtb;
        public static SqlDataReader dataReader;
        public static string connectionString = Utility.connectionString;
        static DataAccess()
        {
            _conn = new SqlConnection(connectionString);

            if(_conn.State == ConnectionState.Open)
            {
                _conn.Close();
                _conn.Dispose();
            }
            try
            {
                _conn.Open();
            }
            catch(Exception ex)
            {

            }   
        }
        public static DataTable ReturnDataTable()
        {
            _sqlDta = new SqlDataAdapter(sqlCmd);
            _dtb = new DataTable();
            _sqlDta.Fill(_dtb);
            return _dtb;
        }

        public static DataTable ReturnDataTable(string query)
        {
            sqlCmd = new SqlCommand(query, _conn);
            _sqlDta = new SqlDataAdapter(sqlCmd);
            _dtb = new DataTable();
            _sqlDta.Fill(_dtb);
            return _dtb;
        }

        public static SqlDataReader ReturnDataReader(string query)
        {
            sqlCmd = new SqlCommand(query, _conn);
            dataReader = sqlCmd.ExecuteReader();
            return dataReader;
        }
        public static void SqlQuery(string query)
        {
            sqlCmd = new SqlCommand(query, _conn);
            sqlCmd.ExecuteNonQuery();
        }

        public static int SqlQueryInt(string query)
        {
           int i = 0;
           sqlCmd = new SqlCommand(query, _conn);
           i =  Convert.ToInt32(sqlCmd.ExecuteScalar());

           return i;
        }

        public static DataTable SelectDataTable(string RetrieveColumn, string TableName)
        {
            try {
                    string query = "SELECT " + RetrieveColumn + " FROM " + TableName;
                    sqlCmd = new SqlCommand(query, _conn);
                }
            catch (Exception ex)
            {

            }
            return ReturnDataTable();
        }

        public static DataTable SelectMapData(String TableName, String CompanyID, String DivisionID, String DepartmentID, String KeyColumn, String KeyValue, String KeyColumn1, String KeyValue1)
        {
            try
            {
                string query = "SELECT CompanyID, DivisionID, DepartmentID FROM " + TableName + " WHERE CompanyID = '" + CompanyID + "' AND DivisionID = '" + DivisionID
                                + "' AND DepartmentID = '" + DepartmentID + "' AND " + KeyColumn + " = '" + KeyValue +"' AND " + KeyColumn1 + " = '" + KeyValue1 +"'";
            }
            catch (Exception ex)
            {

            }
            return ReturnDataTable();
        }

        public static DataTable GetAttendanceDetail(String TableName, String KeyColumn, String KeyColumn1, string KeyValue1, String RetrieveColumn)
        {
            try
            {
                string query = "SELECT " + RetrieveColumn + " FROM " + TableName + " WHERE CAST(" + KeyColumn1 + " AS DATE) = '" + KeyValue1 + "' AND ISNULL(" + KeyColumn + ", 0) = 0";
                sqlCmd = new SqlCommand(query, _conn);
            }
            catch (Exception ex)
            {

            }
            return ReturnDataTable();
        }

        public static DataTable GetAttendanceHeader(String TableName, String KeyColumn, String KeyColumn1)
        {
            try
            {
                string query = "SELECT DISTINCT CAST(" + KeyColumn + " AS DATE) AS Clock FROM " + TableName + " WHERE ISNULL(" + KeyColumn1 + ", 0) = 0";
                sqlCmd = new SqlCommand(query, _conn);
            }
            catch (Exception ex)
            {

            }
            return ReturnDataTable();
        }
        public static void UpdateSyncDetail(String TableName, String UpdateColumn, String UpdateValue, String UpdateColumn1, String UpdateValue1, String UpdateColumn2, DateTime UpdateValue2)
        {
            try
            {
                string query = "UPDATE " + TableName + " SET " + UpdateColumn + "= '" + UpdateValue + "', " + UpdateColumn1 + "= '" + UpdateValue1 + "', " + UpdateColumn2 + "= '" + UpdateValue2 + "'";
                SqlQuery(query);
            }
            catch (Exception ex)
            {

            }
        }

        public static string DateStringConverter(DateTime dateTime)
        {
            string dateCnvt = "";
            var dd = Convert.ToString(dateTime.Day);
            var mm = Convert.ToString(dateTime.Month);
            var yyyy = Convert.ToString(dateTime.Year);

            dd = Convert.ToInt32(dd) < 10 ? "0" + dd : dd;
            mm = Convert.ToInt32(mm) < 10 ? "0" + mm : mm;
            yyyy = Convert.ToInt32(yyyy) < 10 ? "0" + yyyy : yyyy;

            dateCnvt = yyyy + "/" + mm + "/" + dd;
            return dateCnvt;
        }

    }
}
