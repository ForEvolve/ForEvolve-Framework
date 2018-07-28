using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.Contracts.Errors
{
    public class InnerErrorTest
    {
        [Fact]
        public void Should_be_serialized_as_expected()
        {
            // Arrange
            var model = new InnerError
            {
                Code = "Some error code"
            };
            model.Add("someProp", "some value");
            model.Add("Foo", "Bar");
            var expectedString = @"{""code"":""Some error code"",""someProp"":""some value"",""Foo"":""Bar""}";

            // Act
            var result = JsonConvert.SerializeObject(model);

            // Assert
            Assert.Equal(expectedString, result);
        }

    }
}
