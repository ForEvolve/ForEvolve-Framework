using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class PaperSizeMeasurementsTest
    {
        public class Ctor
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var size = new Size();
                var nullSize = default(Size);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("width", () => new PaperSizeMeasurements(nullSize, size));
                Assert.Throws<ArgumentNullException>("height", () => new PaperSizeMeasurements(size, nullSize));
            }

            [Fact]
            public void Should_set_Width_and_Height()
            {
                // Arrange
                var width = new Size(123, Unit.Centimeter);
                var height = new Size(234, Unit.Inch);

                // Act
                var sut = new PaperSizeMeasurements(width, height);

                // Assert
                Assert.Equal(width, sut.Width);
                Assert.Equal(height, sut.Height);
            }

        }
    }
}
