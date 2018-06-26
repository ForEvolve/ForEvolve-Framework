using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class InMemoryConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddKeyValue(this IConfigurationBuilder configurationBuilder, string key, string value)
        {
            return configurationBuilder.Add(new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string> { { key, value } }
            });
        }
    }
}

