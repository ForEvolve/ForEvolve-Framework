using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public class MessageTest
    {
        // Arrange
        private readonly OperationMessageLevel _severity = OperationMessageLevel.Information;

        public class Ctor1 : MessageTest
        {
            [Fact]
            public void Should_set_the_level()
            {
                // Act
                var obj = new Message(_severity);

                // Assert
                Assert.Equal(_severity, obj.Severity);
            }

            [Fact]
            public void Should_create_a_default_details_dictionary()
            {
                // Act
                var obj = new Message(_severity);

                // Assert
                Assert.NotNull(obj.Details);
            }

        }

        public class Ctor2 : MessageTest
        {
            // Arrange
            private readonly IDictionary<string, object> _details = new Dictionary<string, object>();

            [Fact]
            public void Should_set_the_level()
            {
                // Act
                var obj = new Message(_severity, _details);

                // Assert
                Assert.Equal(_severity, obj.Severity);
            }

            [Fact]
            public void Should_set_the_details()
            {
                // Act
                var obj = new Message(_severity, _details);

                // Assert
                Assert.Equal(_details, obj.Details);
            }

            [Fact]
            public void Should_throw_an_ArgumentNullException_when_details_is_null()
            {
                Assert.Throws<ArgumentNullException>("details", () => new Message(_severity, null));
            }
        }

        public class Ctor3 : MessageTest
        {
            public abstract class Ctor3TestCases : Ctor3
            {
                protected abstract bool IgnoreNull { get; }

                [Fact]
                public void Should_set_the_level()
                {
                    // Act
                    var obj = new Message(_severity, new { }, IgnoreNull);

                    // Assert
                    Assert.Equal(_severity, obj.Severity);
                }

                [Fact]
                public void Should_load_anonymous_object_into_details()
                {
                    // Arrange
                    var details = new { SomeProp = "Some value", SomeCheck = true };

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.Collection(obj.Details,
                        p => AssertDetailsKeyValue(p, "SomeProp", "Some value"),
                        p => AssertDetailsKeyValue(p, "SomeCheck", true)
                    );
                }

                [Fact]
                public void Should_load_typed_object_into_details()
                {
                    // Arrange
                    var details = new SomeClass
                    {
                        SomeProp = "Some value",
                        SomeCheck = true
                    };

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.Collection(obj.Details,
                        p => AssertDetailsKeyValue(p, "SomeProp", "Some value"),
                        p => AssertDetailsKeyValue(p, "SomeCheck", true)
                    );
                }

                [Fact]
                public void Should_throw_an_ArgumentNullException_when_details_is_null()
                {
                    Assert.Throws<ArgumentNullException>("details", () => new Message(_severity, null, IgnoreNull));
                }

                [Fact]
                public void Should_set_the_Type_when_the_details_is_typed()
                {
                    // Arrange
                    var details = new SomeClass();

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.Equal(typeof(SomeClass), obj.Type);
                }

                [Fact]
                public void Should_set_the_Type_when_the_details_is_anonymous()
                {
                    // Arrange
                    var details = new { SomeProp = true };

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.Equal(details.GetType(), obj.Type);
                }

                [Fact]
                public void Should_set_the_IsAnonymous_to_false_when_the_details_is_typed()
                {
                    // Arrange
                    var details = new SomeClass();

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.False(obj.IsAnonymous);
                }

                [Fact]
                public void Should_set_the_IsAnonymous_to_true_when_the_details_is_an_anonymous_type()
                {
                    // Arrange
                    var details = new { SomeProp = true };

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.True(obj.IsAnonymous);
                }

                [Fact]
                public void Should_set_the_OriginalObject()
                {
                    // Arrange
                    var details = new SomeClass();

                    // Act
                    var obj = new Message(_severity, details, IgnoreNull);

                    // Assert
                    Assert.Same(obj.OriginalObject, details);
                }

                private void AssertDetailsKeyValue(KeyValuePair<string, object> pair, string expectedKey, object expectedValue)
                {
                    Assert.Equal(expectedKey, pair.Key);
                    Assert.Equal(expectedValue, pair.Value);
                }
            }

            public class When_ignoreNull_is_true : Ctor3TestCases
            {
                protected override bool IgnoreNull => true;
            }

            public class When_ignoreNull_is_false : Ctor3TestCases
            {
                protected override bool IgnoreNull => false;
            }
        }

        public class Is_TType : MessageTest
        {
            [Fact(Skip = "This would need a good design to implement.")]
            public void Should_return_true_when_the_types_are_compatible()
            {
                // Arrange


                // Act


                // Assert
                throw new NotImplementedException();
            }

            [Fact]
            public void Should_return_true_when_the_types_are_the_same()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.Is<SomeClass>();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_the_types_are_not_the_same()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.Is<SomeOtherClass>();

                // Assert
                Assert.False(result);
            }
        }

        public class Is_Type : MessageTest
        {
            [Fact(Skip = "This would need a good design to implement.")]
            public void Should_return_true_when_the_types_are_compatible()
            {
                // Arrange


                // Act


                // Assert
                throw new NotImplementedException();
            }

            [Fact]
            public void Should_return_true_when_the_types_are_the_same()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.Is(typeof(SomeClass));

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_the_types_are_not_the_same()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.Is(typeof(SomeOtherClass));

                // Assert
                Assert.False(result);
            }
        }

        public class As_TType : MessageTest
        {
            [Fact]
            public void Should_convert_Details_back_to_the_specified_type()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.As<SomeClass>();

                // Assert
                Assert.Equal(details.SomeCheck, result.SomeCheck);
                Assert.Equal(details.SomeProp, result.SomeProp);
            }

            [Fact]
            public void Should_throw_a_TypeMismatchException_when_types_are_incompatible()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act & Assert
                Assert.Throws<TypeMismatchException>(() => sut.As<SomeOtherClass>());
            }

            [Fact]
            public void Should_return_the_OriginalObject_when_one_exists()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.As<SomeClass>();

                // Assert
                Assert.Same(sut.OriginalObject, result);
            }
        }

        public class As_Type : MessageTest
        {
            [Fact]
            public void Should_convert_Details_back_to_the_specified_type()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.As(typeof(SomeClass));

                // Assert
                var typedResult = Assert.IsType<SomeClass>(result);
                Assert.Equal(details.SomeCheck, typedResult.SomeCheck);
                Assert.Equal(details.SomeProp, typedResult.SomeProp);
            }

            [Fact]
            public void Should_throw_a_TypeMismatchException_when_types_are_incompatible()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act & Assert
                Assert.Throws<TypeMismatchException>(() => sut.As(typeof(SomeOtherClass)));
            }

            [Fact]
            public void Should_return_the_OriginalObject_when_one_exists()
            {
                // Arrange
                var details = new SomeClass { SomeCheck = true, SomeProp = "Value!" };
                var sut = new Message(_severity, details);

                // Act
                var result = sut.As(typeof(SomeClass));

                // Assert
                Assert.Same(sut.OriginalObject, result);
            }
        }

        private class SomeClass
        {
            public string SomeProp { get; set; }
            public bool SomeCheck { get; set; }
        }

        private class SomeOtherClass
        {
            public int SomeProp { get; set; }
            public bool SomeOtherProps { get; set; }
        }
    }
}
