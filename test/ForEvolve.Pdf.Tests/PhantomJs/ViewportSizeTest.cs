using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class ViewportSizeTest
    {
        public class Equals_ViewportSize
        {
            public static TheoryData<ViewportSize, ViewportSize, bool> Data = new TheoryData<ViewportSize, ViewportSize, bool>
            {
                { new ViewportSize(), new ViewportSize(), true },
                { new ViewportSize { Width = 600, Height = 600 }, ViewportSize.Default, true },
                { new ViewportSize { Width = 500, Height = 600 }, new ViewportSize { Width = 600, Height = 600 }, false },
                { new ViewportSize { Width = 600, Height = 500 }, new ViewportSize { Width = 600, Height = 600 }, false },
                { new ViewportSize { Width = 500, Height = 500 }, new ViewportSize { Width = 600, Height = 600 }, false },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_be_tested(ViewportSize sut, ViewportSize other, bool expectedResult)
            {
                // Act
                var result = sut.Equals(other);

                // Assert
                Assert.Equal(expectedResult, result);
            }
        }

        public class SerializeTo
        {
            [Fact]
            public void Should_return_the_expected_values()
            {
                // Arrange
                var sut = new ViewportSize { Width = 1, Height = 2 };
                var properties = new Dictionary<string, object>();

                // Act
                var result = sut.SerializeTo(properties);

                // Assert
                Assert.Same(properties, result);
                Assert.Collection(properties,
                    property => property.AssertEqual("width", 1),
                    property => property.AssertEqual("height", 2)
                );
            }
        }
    }
}
