using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PowerBiometrics
{
    public class BusinessClass
    {
        private DataAccess da;
        public void SelectData(string RetrieveColumn, string TableName)
        {
            string query = "SELECT " + RetrieveColumn + " FROM " + TableName;
            da = new DataAccess();
            da.SqlQuery(query);
            //da.SqlQuery(query, da.)
        }
    }
}
//Dim sqlCmd As String = "SELECT " & RetrieveColumn & " FROM " & TableName & " Where CompanyID='" & CompanyID & "' AND DivisionID='" & DivisionID & "' AND DepartmentID='" & DepartmentID & "' AND " & KeyColumn & "=" & KeyValue & ""
//        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("EnterpriseConnectionString").ConnectionString
//        Dim conn As New SqlConnection(ConnectionString)
        
//        Catch e As SqlException
//        Finally
//            conn.Close()
//        End Try
//        Return ds.Tables("GetData")