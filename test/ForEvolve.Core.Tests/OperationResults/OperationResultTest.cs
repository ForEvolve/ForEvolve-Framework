using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public class OperationResultTest
    {
        private readonly OperationResult sut = new OperationResult();

        public class Messages : OperationResultTest
        {
            [Fact]
            public void Should_be_initialized()
            {
                // Assert
                Assert.NotNull(sut.Messages);
            }
        }

        public class HasMessages : OperationResultTest
        {
            [Fact]
            public void Should_return_false_when_Messages_is_empty()
            {
                // Act
                var result = sut.HasMessages();

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Should_return_true_when_Messages_contains_at_least_one_message()
            {
                // Arrange
                sut.Messages.Add(new Message(OperationMessageLevel.Error));

                // Act
                var result = sut.HasMessages();

                // Assert
                Assert.True(result);
            }
        }

        public class Succeeded : OperationResultTest
        {
            public static readonly TheoryData<List<IMessage>> TrueMessages = new TheoryData<List<IMessage>>
            {
                new List<IMessage>(),
                new List<IMessage>{ new Message(OperationMessageLevel.Information) },
                new List<IMessage>{ new Message(OperationMessageLevel.Warning) },
                new List<IMessage>{ new Message(OperationMessageLevel.Information), new Message(OperationMessageLevel.Warning) },
            };
            public static readonly TheoryData<List<IMessage>> FalseMessages = new TheoryData<List<IMessage>>
            {
                new List<IMessage>{ new Message(OperationMessageLevel.Error) },
                new List<IMessage>{ new Message(OperationMessageLevel.Error), new Message(OperationMessageLevel.Information) },
                new List<IMessage>{ new Message(OperationMessageLevel.Error), new Message(OperationMessageLevel.Warning) },
                new List<IMessage>{ new Message(OperationMessageLevel.Error), new Message(OperationMessageLevel.Information), new Message(OperationMessageLevel.Warning) },
            };

            [Theory]
            [MemberData(nameof(TrueMessages))]
            public void Should_be_true_when_Messages_contains_no_error(List<IMessage> messages)
            {
                // Arrange
                messages.ForEach(message => sut.Messages.Add(message));

                // Act
                var result = sut.Succeeded;

                // Assert
                Assert.True(result);
            }

            [Theory]
            [MemberData(nameof(FalseMessages))]
            public void Should_be_false_when_Messages_contains_at_least_an_error(List<IMessage> messages)
            {
                // Arrange
                messages.ForEach(message => sut.Messages.Add(message));

                // Act
                var result = sut.Succeeded;

                // Assert
                Assert.False(result);
            }

        }
    }
}
