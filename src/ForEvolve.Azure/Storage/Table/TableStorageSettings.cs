using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure.Storage.Table
{
    public class TableStorageSettings : StorageSettings, ITableStorageSettings
    {
        public string TableName { get; set; }
    }
}
