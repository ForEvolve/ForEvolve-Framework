using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public class MessageCollectionTest
    {
        private readonly MessageCollection sut = new MessageCollection();

        public class HasError : MessageCollectionTest
        {
            public static TheoryData<List<IMessage>> trueMessages = new TheoryData<List<IMessage>>
            {
                new List<IMessage>{
                    new Message(OperationMessageLevel.Error)
                },
                new List<IMessage>{
                    new Message(OperationMessageLevel.Error),
                    new Message(OperationMessageLevel.Error),
                    new Message(OperationMessageLevel.Error)
                },
                new List<IMessage>{
                    new Message(OperationMessageLevel.Information),
                    new Message(OperationMessageLevel.Warning),
                    new Message(OperationMessageLevel.Error)
                },
            };

            [Theory]
            [MemberData(nameof(trueMessages))]
            public void Should_return_true_when_at_least_a_message_is_an_error(List<IMessage> messages)
            {
                // Arrange
                messages.ForEach(message => sut.Add(message));

                // Act
                var result = sut.HasError();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_no_message_is_an_error()
            {
                // Arrange
                sut.Add(new Message(OperationMessageLevel.Information));
                sut.Add(new Message(OperationMessageLevel.Warning));

                // Act
                var result = sut.HasError();

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Should_return_false_when_the_collection_is_empty()
            {
                // Act
                var result = sut.HasError();

                // Assert
                Assert.False(result);
            }
        }

        public class HasWarning : MessageCollectionTest
        {
            public static TheoryData<List<IMessage>> trueMessages = new TheoryData<List<IMessage>>
            {
                new List<IMessage>{
                    new Message(OperationMessageLevel.Warning)
                },
                new List<IMessage>{
                    new Message(OperationMessageLevel.Warning),
                    new Message(OperationMessageLevel.Warning),
                    new Message(OperationMessageLevel.Warning)
                },
                new List<IMessage>{
                    new Message(OperationMessageLevel.Information),
                    new Message(OperationMessageLevel.Warning),
                    new Message(OperationMessageLevel.Error)
                },
            };

            [Theory]
            [MemberData(nameof(trueMessages))]
            public void Should_return_true_when_at_least_a_message_is_a_warning(List<IMessage> messages)
            {
                // Arrange
                messages.ForEach(message => sut.Add(message));

                // Act
                var result = sut.HasWarning();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_no_message_is_a_warning()
            {
                // Arrange
                sut.Add(new Message(OperationMessageLevel.Information));
                sut.Add(new Message(OperationMessageLevel.Error));

                // Act
                var result = sut.HasWarning();

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Should_return_false_when_the_collection_is_empty()
            {
                // Act
                var result = sut.HasWarning();

                // Assert
                Assert.False(result);
            }
        }

        public class HasInformation : MessageCollectionTest
        {
            public static TheoryData<List<IMessage>> trueMessages = new TheoryData<List<IMessage>>
            {
                new List<IMessage>{
                    new Message(OperationMessageLevel.Information)
                },
                new List<IMessage>{
                    new Message(OperationMessageLevel.Information),
                    new Message(OperationMessageLevel.Information),
                    new Message(OperationMessageLevel.Information)
                },
                new List<IMessage>{
                    new Message(OperationMessageLevel.Information),
                    new Message(OperationMessageLevel.Warning),
                    new Message(OperationMessageLevel.Error)
                },
            };

            [Theory]
            [MemberData(nameof(trueMessages))]
            public void Should_return_true_when_at_least_a_message_is_a_warning(List<IMessage> messages)
            {
                // Arrange
                messages.ForEach(message => sut.Add(message));

                // Act
                var result = sut.HasInformation();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_no_message_is_a_warning()
            {
                // Arrange
                sut.Add(new Message(OperationMessageLevel.Warning));
                sut.Add(new Message(OperationMessageLevel.Error));

                // Act
                var result = sut.HasInformation();

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Should_return_false_when_the_collection_is_empty()
            {
                // Act
                var result = sut.HasInformation();

                // Assert
                Assert.False(result);
            }
        }
    }
}
