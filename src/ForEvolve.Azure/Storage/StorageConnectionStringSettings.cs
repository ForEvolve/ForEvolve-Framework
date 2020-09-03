using Microsoft.WindowsAzure.Storage;
using System;

namespace ForEvolve.Azure.Storage
{
    public abstract class StorageConnectionStringSettings : IStorageSettings
    {
        public StorageConnectionStringSettings(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString { get; }

        public CloudStorageAccount CreateCloudStorageAccount()
        {
            return CloudStorageAccount.Parse(ConnectionString);
        }
    }
}
