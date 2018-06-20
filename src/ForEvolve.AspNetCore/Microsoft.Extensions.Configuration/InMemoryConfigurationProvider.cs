using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public class InMemoryConfigurationProvider : ConfigurationProvider
    {
        public InMemoryConfigurationProvider(IDictionary<string, string> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}
