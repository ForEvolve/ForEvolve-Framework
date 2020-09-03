namespace ForEvolve.Azure.Storage.Table
{
    public class CosmosDbLocalEmulatorSettings : TableStorageConnectionStringSettings
    {
        public const string DefaultEmulatorConnectionString = "DefaultEndpointsProtocol=http;AccountName=localhost;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;TableEndpoint=http://localhost:8902/;";
        public CosmosDbLocalEmulatorSettings(string tableName = null)
            : base(DefaultEmulatorConnectionString, tableName) { }
    }
}
