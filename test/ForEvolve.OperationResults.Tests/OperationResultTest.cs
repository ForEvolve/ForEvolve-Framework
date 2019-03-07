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

        public class Success : OperationResultTest
        {
            [Fact]
            public void Should_return_a_successful_OperationResult()
            {
                // Act
                var result = OperationResult.Success();

                // Assert
                Assert.True(result.Succeeded);
            }
        }

        public class Failure : OperationResultTest
        {
            [Fact]
            public void Should_throw_a_ArgumentNullException_when_no_messages_are_supplied()
            {
                Assert.Throws<ArgumentNullException>(
                    "messages",
                    () => OperationResult.Failure()
                );
            }

            [Fact]
            public void Should_throw_a_ArgumentNullException_when_messages_is_null()
            {
                Assert.Throws<ArgumentNullException>(
                    "messages",
                    () => OperationResult.Failure(default(IMessage[]))
                );
            }

            [Fact]
            public void Should_throw_a_ArgumentNullException_when_exception_is_null()
            {
                Assert.Throws<ArgumentNullException>(
                    "exception",
                    () => OperationResult.Failure(default(Exception))
                );
            }

            [Fact]
            public void Should_throw_a_ArgumentNullException_when_problemDetails_is_null()
            {
                Assert.Throws<ArgumentNullException>(
                    "problemDetails",
                    () => OperationResult.Failure(default(ProblemDetails))
                );
                Assert.Throws<ArgumentNullException>(
                    "problemDetails",
                    () => OperationResult.Failure(default(ProblemDetails), OperationMessageLevel.Error)
                );
            }

            public static TheoryData<IMessage[]> FailureData = new TheoryData<IMessage[]>
            {
                new IMessage[] { new Message(OperationMessageLevel.Error) },
                new IMessage[] { new Message(OperationMessageLevel.Error), new Message(OperationMessageLevel.Information) },
                new IMessage[] { new Message(OperationMessageLevel.Error), new Message(OperationMessageLevel.Warning) },
                new IMessage[] { new Message(OperationMessageLevel.Error), new Message(OperationMessageLevel.Warning), new Message(OperationMessageLevel.Information) },
            };

            [Theory]
            [MemberData(nameof(FailureData))]
            public void Should_return_a_not_successful_OperationResult(IMessage[] messages)
            {
                // Act
                var result = OperationResult.Failure(messages);

                // Assert
                Assert.False(result.Succeeded);
                Assert.Equal(messages, result.Messages);
            }

        }
    }

    public class OperationResult_TValue
    {
        private readonly OperationResult<SomeValue> sut = new OperationResult<SomeValue>();

        public class HasValue : OperationResult_TValue
        {
            [Fact]
            public void Should_return_true_when_value_is_not_null()
            {
                // Arrange
                sut.Value = new SomeValue { Prop = 123 };

                // Act
                var result = sut.HasValue();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_value_is_null()
            {
                // Arrange
                sut.Value = null;

                // Act
                var result = sut.HasValue();

                // Assert
                Assert.False(result);
            }

        }

        private class SomeValue
        {
            public int Prop { get; set; }
        }
    }
}
