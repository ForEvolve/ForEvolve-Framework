using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure
{
    public class AzureException : ForEvolveException
    {
        public AzureException()
        {
        }

        public AzureException(string message)
            : base(message)
        {
        }

        public AzureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
