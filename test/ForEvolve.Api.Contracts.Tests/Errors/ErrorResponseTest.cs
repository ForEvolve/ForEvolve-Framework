using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Api.Contracts.Errors
{
    public class ErrorResponseTest
    {
        [Fact]
        public void Should_be_serialized_as_expected()
        {
            // Arrange
            var model = new ErrorResponse
            {
                Error = new Error()
            };
            var expectedString = @"{""error"":{}}";

            // Act
            var result = JsonConvert.SerializeObject(
                model, 
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            // Assert
            Assert.Equal(expectedString, result);
        }
    }
}
