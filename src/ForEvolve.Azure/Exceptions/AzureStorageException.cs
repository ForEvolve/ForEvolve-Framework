using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure
{
    public class AzureStorageException : AzureException
    {
        public AzureStorageException()
        {
        }

        public AzureStorageException(string message)
            : base(message)
        {
        }

        public AzureStorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
