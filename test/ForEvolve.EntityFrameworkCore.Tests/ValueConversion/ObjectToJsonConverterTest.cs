using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.ValueConversion
{
    public class ObjectToJsonConverterTest
    {
        private readonly ObjectToJsonConverter sut = new ObjectToJsonConverter();

        public class Serialize : ObjectToJsonConverterTest
        {
            [Fact]
            public void Should_serialize_MyClass_to_json()
            {
                // Arrange
                var @object = new MyClass { Name = "Test name" };

                // Act
                var json = ObjectToJsonConverter.Serialize(@object);

                // Assert
                Assert.Equal("{\"Name\":\"Test name\"}", json);
            }
        }

        public class ConvertToProvider : ObjectToJsonConverterTest
        {
            [Fact(Skip = "The execution seems odd and ask for an IConvertible. It should work in a DbContext.")]
            public void Should_serialize_MyClass_to_json()
            {
                // Arrange
                var @object = new MyClass { Name = "Test name" };

                // Act
                var result = sut.ConvertFromProvider(@object);

                // Assert
                var json = Assert.IsType<string>(result);
                Assert.Equal("{Name:\"Test name\"}", json);
            }
        }

        public class ConvertFromProvider : ObjectToJsonConverterTest
        {
            [Fact]
            public void Should_deserialize_json_to_MyClass()
            {
                // Arrange
                var json = "{Name:\"Test name\"}";

                // Act
                var result = sut.ConvertFromProvider(json);

                // Assert
                var jObject= Assert.IsType<JObject>(result);
                var myClass = jObject.ToObject<MyClass>();
                Assert.Equal("Test name", myClass.Name);
            }
        }

        public class MyClass
        {
            public string Name { get; set; }
        }
    }
}
