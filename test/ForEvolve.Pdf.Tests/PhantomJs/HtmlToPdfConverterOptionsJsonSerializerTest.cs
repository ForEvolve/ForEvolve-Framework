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
            public static TheoryData<HtmlToPdfConverterOptions, string> Data;

            static Serialize()
            {
                Data = new TheoryData<HtmlToPdfConverterOptions, string>();

                var data1 = new HtmlToPdfConverterOptions();
                data1.PaperSize.Margins = Margins.Normal;
                data1.PaperSize.Orientation = Orientation.Portrait;
                Data.Add(data1, "{\\\"paperSize\\\":{\\\"format\\\":\\\"Letter\\\",\\\"orientation\\\":\\\"portrait\\\",\\\"margin\\\":{\\\"top\\\":\\\"1in\\\",\\\"right\\\":\\\"1in\\\",\\\"bottom\\\":\\\"1in\\\",\\\"left\\\":\\\"1in\\\"}},\\\"viewportSize\\\":{\\\"width\\\":600,\\\"height\\\":600},\\\"zoomFactor\\\":1}");

                var data2 = new HtmlToPdfConverterOptions
                {
                    PaperSize = new PaperSizeMeasurements(new Size(10, Unit.Centimeter), new Size(11, Unit.Centimeter)),
                    ZoomFactor = 2,
                    ViewportSize = new ViewportSize { Width = 1, Height = 2 },
                    ClipRectangle = new ClipRectangle { Top = 1, Left = 2, Width = 3, Height = 4 }
                };
                data2.PaperSize.Margins = new Margins(new Size(1, Unit.Inch), new Size(2, Unit.Inch), new Size(3, Unit.Inch), new Size(4, Unit.Inch));
                data2.PaperSize.Orientation = Orientation.Landscape;
                Data.Add(data2, "{\\\"paperSize\\\":{\\\"width\\\":\\\"10cm\\\",\\\"height\\\":\\\"11cm\\\",\\\"orientation\\\":\\\"landscape\\\",\\\"margin\\\":{\\\"top\\\":\\\"1in\\\",\\\"right\\\":\\\"2in\\\",\\\"bottom\\\":\\\"3in\\\",\\\"left\\\":\\\"4in\\\"}},\\\"viewportSize\\\":{\\\"width\\\":1,\\\"height\\\":2},\\\"zoomFactor\\\":2,\\\"clipRect\\\":{\\\"top\\\":1,\\\"left\\\":2,\\\"width\\\":3,\\\"height\\\":4}}");
            }

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
