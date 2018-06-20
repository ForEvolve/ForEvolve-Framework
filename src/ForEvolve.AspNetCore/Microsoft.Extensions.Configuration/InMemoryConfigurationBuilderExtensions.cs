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
            return configurationBuilder.Add(new InMemoryConfigurationSource
            {
                Data = new Dictionary<string, string> { { key, value } }
            });
        }

        public static IConfigurationBuilder AddDictionary(this IConfigurationBuilder configurationBuilder, IDictionary<string, string> configurationValues)
        {
            return configurationBuilder.Add(new InMemoryConfigurationSource
            {
                Data = configurationValues
            });
        }
    }
}

