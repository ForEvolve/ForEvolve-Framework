using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore.OperationResults
{
    public class DefaultOperationResultWithResultTest
    {
        private Mock<IErrorFactory> _errorFactoryMock;
        private DefaultOperationResultWithResult<object> _resultUnderTest;

        public DefaultOperationResultWithResultTest()
        {
            // Arrange
            _errorFactoryMock = new Mock<IErrorFactory>();
            _resultUnderTest = new DefaultOperationResultWithResult<object>(_errorFactoryMock.Object);
        }

        public class HasResult : DefaultOperationResultWithResultTest
        {
            [Fact]
            public void Should_return_true_if_Result_is_not_null()
            {
                // Arrange
                _resultUnderTest.Value = new object();

                // Act
                var result = _resultUnderTest.HasResult();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_if_Result_is_null()
            {
                // Arrange
                _resultUnderTest.Value = null;

                // Act
                var result = _resultUnderTest.HasResult();

                // Assert
                Assert.False(result);
            }
        }

        public class Result : DefaultOperationResultWithResultTest
        {
            [Fact]
            public void Should_get_and_set_result()
            {
                // Arrange
                var expectedObject = new object();

                // Act
                _resultUnderTest.Value = expectedObject;

                // Assert
                Assert.Same(expectedObject, _resultUnderTest.Value);
            }
        }
    }
}
