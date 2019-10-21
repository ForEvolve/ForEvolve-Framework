using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public class TypeMismatchExceptionTest
    {
        public class Ctor
        {
            private readonly Mock<IMessage> _messageMock;
            private readonly Type _type;
            public Ctor()
            {
                _messageMock = new Mock<IMessage>();
                _type = typeof(object);
            }

            [Fact]
            public void Should_guard_against_null_sourceMessage()
            {
                // Arrange
                IMessage nullMessage = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "sourceMessage",
                    () => new TypeMismatchException(nullMessage, _type)
                );
            }

            [Fact]
            public void Should_guard_against_null_type()
            {
                // Arrange
                Type nullType = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "type",
                    () => new TypeMismatchException(_messageMock.Object, nullType)
                );
            }

            [Fact]
            public void Should_set_SourceMessage()
            {
                // Act
                var sut = new TypeMismatchException(_messageMock.Object, _type);

                // Assert
                Assert.Same(_messageMock.Object, sut.SourceMessage);
            }

            [Fact]
            public void Should_set_Type()
            {
                // Act
                var sut = new TypeMismatchException(_messageMock.Object, _type);

                // Assert
                Assert.Same(_type, sut.Type);
            }
        }
    }
}
