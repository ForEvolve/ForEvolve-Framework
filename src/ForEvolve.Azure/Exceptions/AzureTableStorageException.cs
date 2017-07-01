using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure
{
    public class AzureTableStorageException : AzureStorageException
    {
        public AzureTableStorageException()
        {
        }

        public AzureTableStorageException(string message) : base(message)
        {
        }

        public AzureTableStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}