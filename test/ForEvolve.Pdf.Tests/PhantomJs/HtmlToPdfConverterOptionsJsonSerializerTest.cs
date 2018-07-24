using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverterOptionsJsonSerializerTest
    {
        private readonly HtmlToPdfConverterOptionsJsonSerializer sut;

        public HtmlToPdfConverterOptionsJsonSerializerTest()
        {
            sut = new HtmlToPdfConverterOptionsJsonSerializer();
        }

        public class Serialize : HtmlToPdfConverterOptionsJsonSerializerTest
        {
            public static TheoryData<HtmlToPdfConverterOptions, string> Data = new TheoryData<HtmlToPdfConverterOptions, string>
            {
                {
                    new HtmlToPdfConverterOptions
                    {
                        PaperSize = PaperSizeFormat.Letter,
                        Margins = Margins.Normal,
                        Orientation = Orientation.Portrait
                    },
                    "{\"paperSize\":{\"format\":\"Letter\",\"orientation\":\"portrait\",\"margin\":{\"top\":\"1in\",\"right\":\"1in\",\"bottom\":\"1in\",\"left\":\"1in\"}}}"
                },
                {
                    new HtmlToPdfConverterOptions
                    {
                        PaperSize = new PaperSizeMeasurements(new Size(10, Unit.Centimeter), new Size(11, Unit.Centimeter)),
                        Margins = new Margins(new Size(1, Unit.Inch),new Size(2, Unit.Inch),new Size(3, Unit.Inch),new Size(4, Unit.Inch)),
                        Orientation = Orientation.Landscape
                    },
                    "{\"paperSize\":{\"width\":\"10cm\",\"height\":\"11cm\",\"orientation\":\"landscape\",\"margin\":{\"top\":\"1in\",\"right\":\"2in\",\"bottom\":\"3in\",\"left\":\"4in\"}}}"
                },
            };

            [Theory]
            [MemberData(nameof(Data))]
            public void Should_convert_options_to_json(HtmlToPdfConverterOptions optionsToSerialize, string expectedJson)
            {
                // Act
                var result = sut.Serialize(optionsToSerialize);

                // Assert
                Assert.Equal(expectedJson, result);
            }
        }
    }
}
