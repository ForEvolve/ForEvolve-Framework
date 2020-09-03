using System;

namespace ForEvolve.Azure.Storage.Blob
{
    public class BlobStorageConnectionStringSettings : StorageConnectionStringSettings, IObjectStorageSettings
    {
        public BlobStorageConnectionStringSettings(string connectionString, string containerName)
            : base(connectionString)
        {
            ContainerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
        }

        public string ContainerName { get; set; }
    }
}
