using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.OperationResults
{
    public class OperationResultHelperTest
    {
        public class CreateSuccess
        {
            [Fact]
            public void Should_return_an_OperationResult_that_Succeeded()
            {
                // Arrange
                var sut = new OperationResultHelper();

                // Act
                var result = sut.CreateSuccess();

                // Assert
                Assert.True(result.Succeeded);
            }

            [Fact]
            public void Should_return_an_OperationResult_without_error()
            {
                // Arrange
                var sut = new OperationResultHelper();

                // Act
                var result = sut.CreateSuccess();

                // Assert
                Assert.Empty(result.Errors);
                Assert.Empty(result.Exceptions);
            }
        }

        public class CreateFailure
        {
            [Fact]
            public void Should_return_an_OperationResult_that_has_not_Succeeded()
            {
                // Arrange
                var sut = new OperationResultHelper();

                // Act
                var result = sut.CreateFailure();

                // Assert
                Assert.False(result.Succeeded);
            }

            [Fact]
            public void Should_return_an_OperationResult_with_an_error()
            {
                // Arrange
                var sut = new OperationResultHelper();

                // Act
                var result = sut.CreateFailure();

                // Assert
                Assert.NotEmpty(result.Errors);
                Assert.Empty(result.Exceptions);
            }
        }
    }
}
