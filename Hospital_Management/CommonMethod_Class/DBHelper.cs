using System.Data.SqlClient;
using Hospital_Management.CommonMethod_Class;

namespace Hospital_Management.CommonMethod
{
    public static class DBHelper
    {             
        public static SqlDataReader ExecuteReder(string procedure, SqlParameter[]? parameters = null)
        {           
            try
            {
                var sqlCommand = new SqlCommand();
                return sqlCommand.ExecuteProceduteReader(procedure,parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Database Connection Failed", ex);
            }
        }
        public static bool ExecuteNonQuery(string Procedure, SqlParameter[]? parameters = null)
        {
            try
            {
                var sqlCommand = new SqlCommand();
                return sqlCommand.ExecuteProcedureNonQuery(Procedure, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Database Connection Failed", ex);
            }
        }
    }
}
