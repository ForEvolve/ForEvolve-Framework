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

        public class Contains : MessageCollectionTest
        {
            [Fact]
            public void Should_return_true_when_the_collection_contains_a_message_of_the_specified_type()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage2());
                sut.Add(new MyMessage3());

                // Act
                var result = sut.Contains<MyMessage2>();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_the_collection_is_empty()
            {
                // Act
                var result = sut.Contains<MyMessage2>();

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Should_return_false_when_the_collection_does_not_contain_a_message_of_the_specified_type()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage3());

                // Act
                var result = sut.Contains<MyMessage2>();

                // Assert
                Assert.False(result);
            }
        }

        public class GetSingle : MessageCollectionTest
        {
            [Fact]
            public void Should_return_the_message_of_the_specified_type()
            {
                // Arrange
                var expectedMessage = new MyMessage2();
                sut.Add(new MyMessage1());
                sut.Add(expectedMessage);
                sut.Add(new MyMessage3());

                // Act
                var result = sut.GetSingle<MyMessage2>();

                // Assert
                Assert.NotNull(result);
                Assert.Same(expectedMessage, result);
            }

            [Fact]
            public void Should_throw_an_InvalidOperationException_when_no_message_is_found()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage3());

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => sut.GetSingle<MyMessage2>());
            }

            [Fact]
            public void Should_throw_an_InvalidOperationException_when_more_than_one_message_is_found()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage2());
                sut.Add(new MyMessage2());
                sut.Add(new MyMessage3());

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => sut.GetSingle<MyMessage2>());
            }
        }

        public class GetFirst : MessageCollectionTest
        {
            [Fact]
            public void Should_return_the_first_message_of_the_specified_type()
            {
                // Arrange
                var expectedMessage = new MyMessage2();
                sut.Add(new MyMessage1());
                sut.Add(expectedMessage);
                sut.Add(new MyMessage2());
                sut.Add(new MyMessage3());

                // Act
                var result = sut.GetFirst<MyMessage2>();

                // Assert
                Assert.NotNull(result);
                Assert.Same(expectedMessage, result);
            }

            [Fact]
            public void Should_throw_an_InvalidOperationException_when_no_message_is_found()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage3());

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => sut.GetFirst<MyMessage2>());
            }
        }

        public class GetLast : MessageCollectionTest
        {
            [Fact]
            public void Should_return_the_last_message_of_the_specified_type()
            {
                // Arrange
                var expectedMessage = new MyMessage2();
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage2());
                sut.Add(expectedMessage);
                sut.Add(new MyMessage3());

                // Act
                var result = sut.GetLast<MyMessage2>();

                // Assert
                Assert.NotNull(result);
                Assert.Same(expectedMessage, result);
            }

            [Fact]
            public void Should_throw_an_InvalidOperationException_when_no_message_is_found()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage3());

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => sut.GetLast<MyMessage2>());
            }
        }

        public class GetAll : MessageCollectionTest
        {
            [Fact]
            public void Should_return_all_messages_of_the_specified_type()
            {
                // Arrange
                var expectedMessage1 = new MyMessage2();
                var expectedMessage2 = new MyMessage2();
                sut.Add(new MyMessage1());
                sut.Add(expectedMessage1);
                sut.Add(expectedMessage2);
                sut.Add(new MyMessage3());

                // Act
                var result = sut.GetAll<MyMessage2>();

                // Assert
                Assert.Collection(result,
                    x => Assert.Same(expectedMessage1, x),
                    x => Assert.Same(expectedMessage2, x)
                );
            }

            [Fact]
            public void Should_return_an_empty_enumerable_when_no_message_of_the_specified_type_exists()
            {
                // Arrange
                sut.Add(new MyMessage1());
                sut.Add(new MyMessage3());

                // Act
                var result = sut.GetAll<MyMessage2>();

                // Assert
                Assert.Empty(result);
            }
        }

        private class MyMessage1 : Message
        {
            public MyMessage1()
                : base(OperationMessageLevel.Error)
            {
            }
        }

        private class MyMessage2 : Message
        {
            public MyMessage2()
                : base(OperationMessageLevel.Information)
            {
            }
        }

        private class MyMessage3 : Message
        {
            public MyMessage3()
                : base(OperationMessageLevel.Warning)
            {
            }
        }
    }
}
