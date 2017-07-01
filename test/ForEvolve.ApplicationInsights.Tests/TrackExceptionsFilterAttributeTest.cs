using System;
using Xunit;

namespace ForEvolve.ApplicationInsights
{
    public class TrackExceptionsFilterAttributeTest
    {
        protected TrackExceptionsFilterAttribute AttributeUnderTest { get; }

        [Fact]
        public void Test1()
        {
            AttributeUnderTest = new TrackExceptionsFilterAttribute(null);
        }
    }
}
