using Model.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Utility
{
    public static class SqlDatabaseUtil
    {
        public static void Query(SqlDatabaseConfig config)
        {
            var connectionString = config.ConnectionString;
            var queryString = config.QueryString;
            var handle = config.Handle_DataTable;

            var cn_connection = new SqlConnection(connectionString);
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }

            var table = new DataTable();
            var adapter = new SqlDataAdapter(queryString, cn_connection);

            adapter.Fill(table);
            handle?.Invoke(table);

            if (cn_connection.State != ConnectionState.Closed)
            {
                cn_connection.Close();
            }
        }
    }
}