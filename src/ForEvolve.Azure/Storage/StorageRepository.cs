using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure.Storage
{
    public abstract class StorageRepository<TStorageSettings> : IStorageRepository
        where TStorageSettings : IStorageSettings
    {
        protected TStorageSettings StorageSettings { get; }

        protected CloudStorageAccount CloudStorageAccount { get; }

        public StorageRepository(TStorageSettings storageSettings)
        {
            if (storageSettings == null) { throw new ArgumentNullException(nameof(storageSettings)); }
            StorageSettings = storageSettings;
            CloudStorageAccount = StorageSettings.CreateCloudStorageAccount();
        }
    }
}
