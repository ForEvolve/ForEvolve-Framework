using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults.Standardizer
{
    public class DefaultPropertyValueFormatterTest
    {
        private readonly DefaultPropertyValueFormatter sut = new DefaultPropertyValueFormatter();
        public class Format : DefaultPropertyValueFormatterTest
        {
            [Fact]
            public void Should_return_the_input()
            {
                // Arrange
                var input = new { Whatever = "" };

                // Act
                var result = sut.Format(input);

                // Assert
                Assert.Same(input, result);
            }

        }
    }
}
