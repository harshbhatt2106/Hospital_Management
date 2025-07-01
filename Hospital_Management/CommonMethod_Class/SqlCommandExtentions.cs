using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod_Class
{
    public static class SqlCommandExtentions
    {
        static SqlConnection? sqlConnection = null;

        private static string ConnectionString = "Data Source=DESKTOP-LCMNUM6\\SQLEXPRESS;Initial Catalog=Hospital_Management;Integrated Security=True;Encrypt=False";

        static SqlCommandExtentions()
        {
            sqlConnection = new SqlConnection(SqlCommandExtentions.ConnectionString);
        }

        public static SqlDataReader ExecuteProceduteReader(this SqlCommand sqlCommand, string ProcedureName, SqlParameter[]? parameters = null)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = ProcedureName;            

            if (parameters != null)
                sqlCommand.Parameters.AddRange(parameters);

            return sqlCommand.ExecuteReader();
        }
        public static bool ExecuteProcedureNonQuery(this SqlCommand command, string ProcedureName, SqlParameter[]? parameters = null)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();


            command.Connection = sqlConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = ProcedureName;

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            command.ExecuteNonQuery();
            return true;
        }



    }
}

