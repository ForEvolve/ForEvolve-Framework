using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class ForEvolveInMemoryConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds the specified key/value pair the the specified <c>IConfigurationBuilder</c>.
        /// </summary>
        /// <param name="configurationBuilder">The <c>IConfigurationBuilder</c> to add the key/value to.</param>
        /// <param name="key">The configuration key to add.</param>
        /// <param name="value">The configuration value to associate to the specified key.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IConfigurationBuilder AddKeyValue(this IConfigurationBuilder configurationBuilder, string key, string value)
        {
            return configurationBuilder.Add(new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string> { { key, value } }
            });
        }
    }
}

