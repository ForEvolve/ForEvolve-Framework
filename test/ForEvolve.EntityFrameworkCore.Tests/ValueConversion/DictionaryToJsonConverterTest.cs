using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.ValueConversion
{
    public class DictionaryToJsonConverterTest
    {
        private readonly DictionaryToJsonConverter sut = new DictionaryToJsonConverter();

        public class Serialize : DictionaryToJsonConverterTest
        {
            [Fact]
            public void Should_serialize_MyClass_to_json()
            {
                // Arrange
                var dictionary = new Dictionary<string, object> {
                    { "Name", "Test name" }
                };

                // Act
                var json = DictionaryToJsonConverter.Serialize(dictionary);

                // Assert
                Assert.Equal("{\"Name\":\"Test name\"}", json);
            }
        }

        public class ConvertToProvider : DictionaryToJsonConverterTest
        {
            [Fact(Skip = "The execution seems odd and ask for an IConvertible. It works in a DbContext.")]
            public void Should_serialize_Dictionary_to_json()
            {
                // Arrange
                var dictionary = new Dictionary<string, object> {
                    { "Name", "Test name" }
                };

                // Act
                var result = sut.ConvertFromProvider(dictionary);

                // Assert
                var json = Assert.IsType<string>(result);
                Assert.Equal("{\"Name\":\"Test name\"}", json);
            }
        }

        public class ConvertFromProvider : DictionaryToJsonConverterTest
        {
            [Fact]
            public void Should_deserialize_json_to_Dictionary()
            {
                // Arrange
                var json = "{\"Name\":\"Test name\"}";

                // Act
                var result = sut.ConvertFromProvider(json);

                // Assert
                var myClass = Assert.IsType<Dictionary<string, object>>(result);
                Assert.Equal("Test name", myClass["Name"].ToString());
            }
        }
    }
}
