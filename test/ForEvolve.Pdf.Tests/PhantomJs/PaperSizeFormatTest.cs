using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class PaperSizeFormatTest
    {
        public class Ctor
        {
            public static TheoryData<PaperSizeFormat, string> Data => new TheoryData<PaperSizeFormat, string>
            {
                { PaperSizeFormat.A3, "A3" },
                { PaperSizeFormat.A4, "A4" },
                { PaperSizeFormat.A5, "A5" },
                { PaperSizeFormat.Legal, "Legal" },
                { PaperSizeFormat.Letter, "Letter" },
                { PaperSizeFormat.Tabloid, "Tabloid" },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_set_format(PaperSizeFormat sut, string expectedFormat)
            {
                // Assert
                Assert.Equal(expectedFormat, sut.Format);
            }
        }
    }
}
