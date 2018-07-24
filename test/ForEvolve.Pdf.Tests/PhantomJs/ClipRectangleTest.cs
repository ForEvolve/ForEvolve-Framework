using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class ClipRectangleTest
    {
        public class Equals_ClipRectangle : ClipRectangleTest
        {
            public static TheoryData<ClipRectangle, ClipRectangle, bool> Data = new TheoryData<ClipRectangle, ClipRectangle, bool>
            {
                { new ClipRectangle(), new ClipRectangle(), true },
                { ClipRectangle.Null, new ClipRectangle(), true },
                { new ClipRectangle { Top = 5, Left = 2, Width = 3, Height = 4 }, new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, false },
                { new ClipRectangle { Top = 1, Left = 5, Width = 3, Height = 4 }, new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, false },
                { new ClipRectangle { Top = 1, Left = 2, Width = 5, Height = 4 }, new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, false },
                { new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 5 }, new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, false },
                { new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, true },
                { new ClipRectangle { Top = 5, Left = 5, Width = 5, Height = 5 }, new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }, false },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_be_tested(ClipRectangle sut, ClipRectangle other, bool expectedResult)
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
                var sut = new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 };
                var properties = new Dictionary<string, object>();

                // Act
                var result = sut.SerializeTo(properties);

                // Assert
                Assert.Same(properties, result);
                Assert.Collection(properties,
                    property => property.AssertEqual("top", 1),
                    property => property.AssertEqual("left", 2),
                    property => property.AssertEqual("width", 3),
                    property => property.AssertEqual("height", 4)
                );
            }
        }
    }
}