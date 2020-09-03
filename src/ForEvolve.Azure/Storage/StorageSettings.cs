using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure.Storage
{
    public abstract class StorageSettings : IStorageSettings
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public bool UseHttps { get; set; } = true;

        public CloudStorageAccount CreateCloudStorageAccount()
        {
            return new CloudStorageAccount(new StorageCredentials(
                AccountName,
                AccountKey
            ), UseHttps);
        }
    }
}
