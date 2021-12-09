using System;
namespace ForEvolve.Azure.Storage.Table
{
    public class DevelopmentTableStorageSettings : DevelopmentStorageSettings, ITableStorageSettings
    {
        public DevelopmentTableStorageSettings() { }
        public DevelopmentTableStorageSettings(string tableName)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        }
        public string TableName { get; set; }
    }
}
