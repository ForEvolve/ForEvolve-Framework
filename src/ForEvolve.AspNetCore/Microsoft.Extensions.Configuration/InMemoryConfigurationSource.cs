using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public class InMemoryConfigurationSource : IConfigurationSource
    {
        public IDictionary<string, string> Data { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new InMemoryConfigurationProvider(Data);
        }
    }
}
