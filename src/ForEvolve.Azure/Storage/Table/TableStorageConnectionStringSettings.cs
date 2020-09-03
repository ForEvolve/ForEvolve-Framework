using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public class TableStorageConnectionStringSettings : StorageConnectionStringSettings, ITableStorageSettings
    {
        public string TableName { get; set; }

        public TableStorageConnectionStringSettings(string connectionString, string tableName = null)
            : base(connectionString)
        {
            TableName = tableName;
        }
    }
}
