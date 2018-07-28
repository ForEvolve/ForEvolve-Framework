using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Contracts.Errors
{
    public class ErrorTest
    {
        [Fact]
        public void Should_be_serialized_as_expected()
        {
            // Arrange
            var model = new Error
            {
                Code = "NastyError",
                Message = "This is bad!",
                Target = "Evil target",
                Details = new List<Error>(),
                InnerError = new InnerError()
            };
            var expectedString = @"{""code"":""NastyError"",""message"":""This is bad!"",""target"":""Evil target"",""details"":[],""innerError"":{}}";

            // Act
            var result = JsonConvert.SerializeObject(model);

            // Assert
            Assert.Equal(expectedString, result);
        }

        public class Ctor
        {
            [Fact]
            public void Should_set_Code()
            {
                // Arrange
                var errorCode = "MyErrorCode";

                // Act
                var error = new Error(errorCode: errorCode);

                // Assert
                Assert.Equal(errorCode, error.Code);
            }

            [Fact]
            public void Should_set_Message()
            {
                // Arrange
                var errorMessage = "MyErrorMessage";

                // Act
                var error = new Error(errorMessage: errorMessage);


                // Assert
                Assert.Equal(errorMessage, error.Message);
            }

            [Fact]
            public void Should_set_Target()
            {
                // Arrange
                var errorTarget = "MyErrorTarget";

                // Act
                var error = new Error(errorTarget: errorTarget);

                // Assert
                Assert.Equal(errorTarget, error.Target);
            }
        }

    }
}
