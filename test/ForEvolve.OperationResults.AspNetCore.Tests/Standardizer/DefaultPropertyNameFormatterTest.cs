using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults.Standardizer
{
    public class DefaultPropertyNameFormatterTest
    {
        private readonly DefaultPropertyNameFormatter sut = new DefaultPropertyNameFormatter();

        public class Format : DefaultPropertyNameFormatterTest
        {
            [Theory]
            [InlineData("_someString", "_someString")]
            [InlineData("someString", "someString")]
            [InlineData("SomeString", "someString")]
            public void Should_convert_the_first_character_to_lowercase(string input, string expected)
            {
                // Act
                var actual = sut.Format(input);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void Should_throw_ArgumentNullException_when_input_is_null_or_empty(string input)
            {
                // Act & Assert
                Assert.Throws<ArgumentNullException>("name", () => sut.Format(input));
            }
        }
    }
}
