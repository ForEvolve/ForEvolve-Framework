namespace ForEvolve.Azure.Storage.Table
{
    public class DevelopmentTableStorageSettings : DevelopmentStorageSettings, ITableStorageSettings
    {
        public string TableName { get; set; }
    }
}
