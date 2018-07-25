using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Collections.Generic
{
    public static class KeyValuePairAssertExtensions
    {
        public static void AssertEqual(this KeyValuePair<string, object> property, string expectedKey, object expectedValue)
        {
            Assert.Equal(expectedKey, property.Key);
            Assert.Equal(expectedValue, property.Value);
        }
    }
}