using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.AspNetCore.OperationResults
{
    public class DefaultOperationResultFactoryTest
    {
        public class Ctor
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                IErrorFactory nullErrorFactory = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    () => new DefaultOperationResultFactory(nullErrorFactory)
                );
            }
        }

        public class Create
        {
            [Fact]
            public void Should_return_an_instance_of_type_DefaultOperationResult()
            {
                // Arrange
                var errorFactoryMock = new Mock<IErrorFactory>();
                var factoryUnderTest = new DefaultOperationResultFactory(errorFactoryMock.Object);

                // Act
                var result = factoryUnderTest.Create();

                // Assert
                Assert.IsType<DefaultOperationResult>(result);
            }
        }

        public class Create_TResult_WithResult
        {
            [Fact]
            public void Should_return_an_instance_of_type_DefaultOperationResultWithResult()
            {
                // Arrange
                var errorFactoryMock = new Mock<IErrorFactory>();
                var factoryUnderTest = new DefaultOperationResultFactory(errorFactoryMock.Object);
                var expectedResult = new object();

                // Act
                var result = factoryUnderTest.Create(expectedResult);

                // Assert
                Assert.IsType<DefaultOperationResultWithResult<object>>(result);
                Assert.Same(expectedResult, result.Value);
            }
        }

        public class Create_TResult
        {
            [Fact]
            public void Should_return_an_instance_of_type_DefaultOperationResultWithResult()
            {
                // Arrange
                var errorFactoryMock = new Mock<IErrorFactory>();
                var factoryUnderTest = new DefaultOperationResultFactory(errorFactoryMock.Object);

                // Act
                var result = factoryUnderTest.Create<object>();

                // Assert
                Assert.IsType<DefaultOperationResultWithResult<object>>(result);
            }
        }
    }
}
