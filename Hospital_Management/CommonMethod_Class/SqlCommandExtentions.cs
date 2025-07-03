using System.Data;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod_Class
{
    public static class SqlCommandExtentions
    {

        private static string ConnectionString = "Data Source=DESKTOP-LCMNUM6\\SQLEXPRESS;Initial Catalog=Hospital_Management;Integrated Security=True;Encrypt=False";

        

        public static SqlDataReader ExecuteProceduteReader(this SqlCommand sqlCommand, string ProcedureName, SqlParameter[]? parameters = null)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = ProcedureName;


            if (parameters != null)
                sqlCommand.Parameters.AddRange(parameters);

            if (sqlConnection?.State != ConnectionState.Open)
                sqlConnection?.Open();

            return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static bool ExecuteProcedureNonQuery(this SqlCommand command, string procedureName, SqlParameter[]? parameters = null)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return true;
        }

    }
}

