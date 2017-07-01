using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using System.Text;

namespace ForEvolve.Azure.Storage.Table
{

    public interface ITableStorageSettings : IStorageSettings
    {
        string TableName { get; set; }
    }

}