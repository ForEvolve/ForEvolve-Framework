using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Text;

namespace ForEvolve.Azure.Storage
{
    public class StorageCredentialsSettings : IStorageSettings
    {
        private readonly StorageCredentials _storageCredentials;
        public bool UseHttps { get; set; }

        public StorageCredentialsSettings(StorageCredentials storageCredentials, bool useHttps = true)
        {
            _storageCredentials = storageCredentials ?? throw new ArgumentNullException(nameof(storageCredentials));
            UseHttps = useHttps;
        }

        public CloudStorageAccount CreateCloudStorageAccount()
        {
            return new CloudStorageAccount(_storageCredentials, UseHttps);
        }
    }
}