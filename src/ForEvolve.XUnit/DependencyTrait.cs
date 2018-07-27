using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit
{
    public static class DependencyTrait
    {
        public const string Name = "Dependency";
        public static class Values
        {
            public const string SqlServer = "SqlServer";
            public const string AzureStorageTable = "AzureStorageTable";
            public const string AzureStorageBlob = "AzureStorageBlob";
            public const string AzureStorageQueue = "AzureStorageQueue";
            public const string AzureKeyVault = "AzureKeyVault";
        }
    }
}
