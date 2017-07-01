using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure
{
    public class AzureObjectStorageException : AzureStorageException
    {
        public AzureObjectStorageException()
        {
        }

        public AzureObjectStorageException(string message) : base(message)
        {
        }

        public AzureObjectStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}