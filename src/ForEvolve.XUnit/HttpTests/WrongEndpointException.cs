using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.HttpTests
{
    public class WrongEndpointException : ForEvolveException
    {
        public WrongEndpointException(
            string expectedMethod, string expectedPath,
            string actualMethod, string actualPath
            )
            :base($"Expected: {expectedMethod} {expectedPath} | Actual: {actualMethod} {actualPath}")
        {
            ExpectedMethod = expectedMethod;
            ExpectedPath = expectedPath;
            ActualMethod = actualMethod;
            ActualPath = actualPath;
        }

        public string ExpectedMethod { get; }
        public string ExpectedPath { get; }
        public string ActualMethod { get; }
        public string ActualPath { get; }
    }
}
