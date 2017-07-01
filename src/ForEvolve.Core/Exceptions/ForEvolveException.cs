using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve
{
    public class ForEvolveException : Exception
    {
        public ForEvolveException()
        {
        }

        public ForEvolveException(string message)
            : base(message)
        {
        }

        public ForEvolveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
