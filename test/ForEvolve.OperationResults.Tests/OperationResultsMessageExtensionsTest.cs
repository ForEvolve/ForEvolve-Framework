using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public class OperationResultsMessageExtensionsTest
    {
        public class HavingDetailsOfType : OperationResultsMessageExtensionsTest
        {
            [Fact]
            public void Should_return_all_messages_that_are_of_the_specified_type()
            {
                // Arrange
                var sut = new MessageCollection();
                var messageMock1 = new Mock<IMessage>();
                var messageMock2 = new Mock<IMessage>();
                var messageMock3 = new Mock<IMessage>();
                messageMock1.Setup(x => x.Is<ArgumentNullException>()).Returns(true);
                messageMock2.Setup(x => x.Is<ArgumentNullException>()).Returns(false);
                messageMock3.Setup(x => x.Is<ArgumentNullException>()).Returns(true);
                sut.AddRange(new[] { messageMock1.Object, messageMock2.Object, messageMock3.Object });

                // Act
                var result = sut.HavingDetailsOfType<IMessage, ArgumentNullException>();

                // Assert
                Assert.Collection(result,
                    message => Assert.Same(messageMock1.Object, message),
                    message => Assert.Same(messageMock3.Object, message)
                );
            }
        }

        public class ContainsDetails : OperationResultsMessageExtensionsTest
        {
            [Fact]
            public void Should_return_true_when_a_message_Is_of_the_specified_type()
            {
                // Arrange
                var sut = new MessageCollection();
                var messageMock1 = new Mock<IMessage>();
                var messageMock2 = new Mock<IMessage>();
                var messageMock3 = new Mock<IMessage>();
                messageMock1.Setup(x => x.Is<ArgumentNullException>()).Returns(true);
                messageMock2.Setup(x => x.Is<ArgumentNullException>()).Returns(false);
                messageMock3.Setup(x => x.Is<ArgumentNullException>()).Returns(true);
                sut.AddRange(new[] { messageMock1.Object, messageMock2.Object, messageMock3.Object });

                // Act
                var result = sut.ContainsDetails<IMessage, ArgumentNullException>();

                // Assert
                Assert.True(result);
            }
        }

        public class HavingDetailsOfTypeAs : OperationResultsMessageExtensionsTest
        {
            [Fact]
            public void Should_return_all_messages_details_as_their_Details_type()
            {
                // Arrange
                var sut = new MessageCollection();
                var exception1 = new ArgumentNullException();
                var exception2 = new ArgumentNullException();
                var messageMock1 = new Mock<IMessage>();
                var messageMock2 = new Mock<IMessage>();
                var messageMock3 = new Mock<IMessage>();
                messageMock1.Setup(x => x.Is<ArgumentNullException>()).Returns(true);
                messageMock2.Setup(x => x.Is<ArgumentNullException>()).Returns(false);
                messageMock3.Setup(x => x.Is<ArgumentNullException>()).Returns(true);
                messageMock1.Setup(x => x.As<ArgumentNullException>()).Returns(exception1);
                messageMock3.Setup(x => x.As<ArgumentNullException>()).Returns(exception2);
                sut.AddRange(new[] { messageMock1.Object, messageMock2.Object, messageMock3.Object });

                // Act
                var result = sut.HavingDetailsOfTypeAs<ArgumentNullException>();

                // Assert
                Assert.Collection(result,
                    ex => Assert.Same(exception1, ex),
                    ex => Assert.Same(exception2, ex)
                );
            }
        }

        public class GetExceptionsOfType : OperationResultsMessageExtensionsTest
        {
            [Fact]
            public void Should_return_all_ExceptionMessage_Exception()
            {
                // Arrange
                var sut = new MessageCollection();
                var exception1 = new Exception();
                var exception2 = new ArgumentNullException();
                var exception3 = new ArgumentException();
                var exception4 = new ArgumentNullException();
                sut.Add(new ExceptionMessage(exception1));
                sut.Add(new ExceptionMessage(exception2));
                sut.Add(new ExceptionMessage(exception3));
                sut.Add(new ExceptionMessage(exception4));

                // Act
                var result = sut.GetExceptionsOfType<ArgumentNullException>();

                // Assert
                Assert.Collection(result,
                    ex => Assert.Same(exception2, ex),
                    ex => Assert.Same(exception4, ex)
                );
            }

        }
    }
}
