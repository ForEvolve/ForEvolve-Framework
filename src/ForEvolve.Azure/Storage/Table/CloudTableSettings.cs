using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using System.Text;

namespace ForEvolve.Azure.Storage.Table
{
    public class CloudTableSettings : StorageCredentialsSettings, ITableStorageSettings
    {
        public string TableName { get; set; }

        public CloudTableSettings(CloudTable table)
            : base(table.ServiceClient.Credentials)
        {
            if (table == null) { throw new ArgumentNullException(nameof(table)); }
            TableName = table.Name;
        }
    }
}