using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class UnitFormatterTest
    {
        public class Format
        {
            public static TheoryData<Unit, string> Data => new TheoryData<Unit, string>
            {
                { Unit.Pixel, "px" },
                { Unit.Millimeter, "mm" },
                { Unit.Centimeter, "cm" },
                { Unit.Inch, "in" },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_return_the_expected_string(Unit unit, string expectedResult)
            {
                // Act
                var result = UnitFormatter.Format(unit);

                // Assert
                Assert.Equal(expectedResult, result);
            }

            [Fact]
            public void Should_throw_an_ArgumentException_if_the_unit_is_not_supported()
            {
                // Act & Assert
                Assert.Throws<ArgumentException>("unitToFormat", () => UnitFormatter.Format((Unit)100));
            }

        }
    }
}
