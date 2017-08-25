using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;

namespace ForEvolve.Azure.Storage
{
    public class DevelopmentStorageSettings : IStorageSettings
    {
        public CloudStorageAccount CreateCloudStorageAccount()
        {
            return CloudStorageAccount.DevelopmentStorageAccount;
        }
    }
}
