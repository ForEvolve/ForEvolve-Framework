using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class SizeTest
    {
        public class Ctor
        {
            [Fact]
            public void Should_set_Value_and_Unit()
            {
                // Arrange
                var expectedValue = 10F;
                var expectedUnit = Unit.Millimeter;

                // Act
                var sut = new Size(expectedValue, expectedUnit);

                // Assert
                Assert.Equal(expectedValue, sut.Value);
                Assert.Equal(expectedUnit, sut.Unit);
            }

        }

        public class Equals_Size
        {
            public static TheoryData<Size, Size, bool> Data = new TheoryData<Size, Size, bool>
            {
                { new Size(), new Size(), true },
                { new Size(123, Unit.Centimeter), new Size(124, Unit.Centimeter), false },
                { new Size(123, Unit.Centimeter), new Size(123, Unit.Inch), false },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_return_expected_result(Size sut, Size other, bool expectedResult)
            {
                // Act
                var result = sut.Equals(other);

                // Assert
                Assert.Equal(expectedResult, result);
            }
        }

        public class ToString_override
        {
            [Fact]
            public void Should_return_expected_string()
            {
                // Arrange
                var expectedResult = "12px";
                var sut = new Size(12, Unit.Pixel);

                // Act
                var result = sut.ToString();

                // Assert
                Assert.Equal(expectedResult, result);
            }

        }
    }
}
