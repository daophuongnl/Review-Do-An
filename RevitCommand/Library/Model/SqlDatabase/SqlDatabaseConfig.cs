using System;
using System.Data;

namespace Model.Entity
{
    public class SqlDatabaseConfig
    {
        public string? ConnectionString { get; set; }

        public string? QueryString { get; set; }

        public Action<DataTable>? Handle_DataTable { get; set; }
    }
}