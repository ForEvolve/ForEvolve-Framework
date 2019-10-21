using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public class ExceptionMessageTest
    {
        protected virtual Exception ExpectedException { get; } = new ArgumentNullException();
        private readonly ExceptionMessage sut;

        public ExceptionMessageTest()
        {
            sut = new ExceptionMessage(ExpectedException);
        }

        public class Ctor : ExceptionMessageTest
        {
            [Fact]
            public void Should_guard_against_null_exception()
            {
                var nullException = default(Exception);
                Assert.Throws<ArgumentNullException>(
                    "exception", 
                    () => new ExceptionMessage(nullException));
            }

            [Fact]
            public void Should_set_Severity_to_Error()
            {
                Assert.Equal(OperationMessageLevel.Error, sut.Severity);
            }

            [Fact(Skip = "Should be implemented as part of issue #49.")]
            public void Should_load_details_including_innerExceptions()
            {
                // Arrange


                // Act


                // Assert
                throw new NotImplementedException();
            }
        }

        public class Is_TType : ExceptionMessageTest
        {
            [Fact]
            public void Should_return_true_when_TType_is_the_Exception_type()
            {
                // Act
                var result = sut.Is<ArgumentNullException>();

                // Assert
                Assert.True(result);
            }
        }

        public class Is_Type : ExceptionMessageTest
        {
            [Fact]
            public void Should_return_true_when_Type_is_the_Exception_type()
            {
                // Act
                var result = sut.Is(typeof(ArgumentNullException));

                // Assert
                Assert.True(result);
            }
        }

        public class As_TType : ExceptionMessageTest
        {
            [Fact]
            public void Should_return_the_Exception()
            {
                // Act
                var result = sut.As<ArgumentNullException>();

                // Assert
                Assert.Same(ExpectedException, result);
            }
        }

        public class As_Type : ExceptionMessageTest
        {
            [Fact]
            public void Should_return_the_Exception()
            {
                // Act
                var result = sut.As(typeof(ArgumentNullException));

                // Assert
                Assert.Same(ExpectedException, result);
            }
        }
    }
}
