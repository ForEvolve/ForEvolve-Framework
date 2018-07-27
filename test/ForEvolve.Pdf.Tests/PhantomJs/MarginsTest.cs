using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class MarginsTest
    {

        public class Ctor : MarginsTest
        {
            [Fact]
            public void Should_set_default_margins()
            {
                // Arrange
                var expectedMargin = new Size();

                // Act
                var sut = new Margins();

                // Assert
                Assert.Equal(expectedMargin, sut.Top);
                Assert.Equal(expectedMargin, sut.Right);
                Assert.Equal(expectedMargin, sut.Bottom);
                Assert.Equal(expectedMargin, sut.Left);
            }

            [Fact]
            public void Should_set_all_margins()
            {
                // Arrange
                var expectedMargin = new Size(12F, Unit.Pixel);

                // Act
                var sut = new Margins(expectedMargin);

                // Assert
                Assert.Equal(expectedMargin, sut.Top);
                Assert.Equal(expectedMargin, sut.Right);
                Assert.Equal(expectedMargin, sut.Bottom);
                Assert.Equal(expectedMargin, sut.Left);
            }

            [Fact]
            public void Should_set_TopAndBottom_and_RightAndLeft()
            {
                // Arrange
                var expectedTopAndBottomMargin = new Size(12F, Unit.Pixel);
                var expectedRightAndLeftMargin = new Size(13F, Unit.Pixel);

                // Act
                var sut = new Margins(expectedTopAndBottomMargin, expectedRightAndLeftMargin);

                // Assert
                Assert.Equal(expectedTopAndBottomMargin, sut.Top);
                Assert.Equal(expectedRightAndLeftMargin, sut.Right);
                Assert.Equal(expectedTopAndBottomMargin, sut.Bottom);
                Assert.Equal(expectedRightAndLeftMargin, sut.Left);
            }

            [Fact]
            public void Should_set_Top_RightAndLeft_and_Bottom()
            {
                // Arrange
                var expectedTopMargin = new Size(12F, Unit.Pixel);
                var expectedRightAndLeftMargin = new Size(13F, Unit.Pixel);
                var expectedBottomMargin = new Size(14F, Unit.Pixel);

                // Act
                var sut = new Margins(expectedTopMargin, expectedRightAndLeftMargin, expectedBottomMargin);

                // Assert
                Assert.Equal(expectedTopMargin, sut.Top);
                Assert.Equal(expectedRightAndLeftMargin, sut.Right);
                Assert.Equal(expectedBottomMargin, sut.Bottom);
                Assert.Equal(expectedRightAndLeftMargin, sut.Left);
            }

            [Fact]
            public void Should_set_Top_Right_Bottom_and_Left()
            {
                // Arrange
                var expectedTopMargin = new Size(12F, Unit.Pixel);
                var expectedRightMargin = new Size(13F, Unit.Pixel);
                var expectedBottomMargin = new Size(14F, Unit.Pixel);
                var expectedLeftMargin = new Size(15F, Unit.Pixel);

                // Act
                var sut = new Margins(expectedTopMargin, expectedRightMargin, expectedBottomMargin, expectedLeftMargin);

                // Assert
                Assert.Equal(expectedTopMargin, sut.Top);
                Assert.Equal(expectedRightMargin, sut.Right);
                Assert.Equal(expectedBottomMargin, sut.Bottom);
                Assert.Equal(expectedLeftMargin, sut.Left);
            }

            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var margin = new Size(12F, Unit.Pixel);
                var nullMargin = default(Size);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("top", () => new Margins(nullMargin, margin, margin, margin));
                Assert.Throws<ArgumentNullException>("right", () => new Margins(margin, nullMargin, margin, margin));
                Assert.Throws<ArgumentNullException>("bottom", () => new Margins(margin, margin, nullMargin, margin));
                Assert.Throws<ArgumentNullException>("left", () => new Margins(margin, margin, margin, nullMargin));
            }
        }

        public class Equals_Margins : MarginsTest
        {
            public static TheoryData<Margins, Margins, bool> Data => new TheoryData<Margins, Margins, bool>
            {
                { new Margins(), new Margins(), true },
                { Margins.Normal, new Margins(new Size(1F, Unit.Inch)), true },
                { Margins.Narrow, new Margins(new Size(0.5F, Unit.Inch)), true },
                { Margins.Moderate, new Margins(new Size(1F, Unit.Inch), new Size(0.75F, Unit.Inch)), true },
                { Margins.Wide, new Margins(new Size(1F, Unit.Inch), new Size(2F, Unit.Inch)), true },
                { Margins.Normal, Margins.Narrow, false },
                { Margins.Normal, Margins.Moderate, false },
                { Margins.Normal, Margins.Wide, false },
                { new Margins(), new Margins(new Size(), new Size(), new Size(321, Unit.Millimeter), new Size()), false },
                { new Margins(), new Margins(new Size(), new Size(), new Size(), new Size(321, Unit.Pixel)), false },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_equal_other(Margins sut, Margins other, bool expectedResult)
            {
                // Act
                var result = sut.Equals(other);

                // Assert
                Assert.Equal(expectedResult, result);
            }

        }
    }
}
