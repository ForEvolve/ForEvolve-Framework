using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverterOptionsTest
    {
        private readonly HtmlToPdfConverterOptions sut;
        public HtmlToPdfConverterOptionsTest()
        {
            sut = new HtmlToPdfConverterOptions();
        }

        public class Ctor : HtmlToPdfConverterOptionsTest
        {
            [Fact]
            public void Should_set_the_default_properties()
            {
                // Arrange
                var currentDirectory = Directory.GetCurrentDirectory();
                var expectedRoot = Path.Combine(currentDirectory, "PhantomJs", "Root");

                // Act
                var sut = new HtmlToPdfConverterOptions();

                // Assert
                Assert.Equal(expectedRoot, sut.PhantomRootDirectory);
                AssertDefaults(sut);
            }
        }

        public class Ctor_with_phantomRootDirectory : HtmlToPdfConverterOptionsTest
        {
            [Fact]
            public void Should_set_the_PhantomRootDirectory()
            {
                // Arrange
                var currentDirectory = Directory.GetCurrentDirectory();
                var expectedRoot = Path.Combine(currentDirectory, "PhantomJs", "TestRoot");

                // Act
                var sut = new HtmlToPdfConverterOptions(expectedRoot);

                // Assert
                Assert.Equal(expectedRoot, sut.PhantomRootDirectory);
                AssertDefaults(sut);
            }

            [Fact]
            public void Should_throw_an_ArgumentNullException_when_phantomRootDirectory_is_null()
            {
                // Arrange
                var phantomRootDirectory = default(string);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("phantomRootDirectory", () => new HtmlToPdfConverterOptions(phantomRootDirectory));
            }

            [Fact]
            public void Should_throw_an_ArgumentException_when_phantomRootDirectory_does_not_exist()
            {
                // Arrange
                var phantomRootDirectory = "z:\\some-unexisting-directory\\phantom-js\\root\\";

                // Act & Assert
                var ex = Assert.Throws<ArgumentException>("phantomRootDirectory", () => new HtmlToPdfConverterOptions(phantomRootDirectory));
                Assert.StartsWith(PhantomJsConstants.DirectoryDoesNotExist, ex.Message);
            }
        }

        private static void AssertDefaults(HtmlToPdfConverterOptions sut)
        {
            var paperSizeFormat = Assert.IsType<PaperSizeFormat>(sut.PaperSize);
            Assert.Equal(PaperSizeFormat.Letter.Format, paperSizeFormat.Format);
            Assert.Equal(Orientation.Portrait, sut.PaperSize.Orientation);
            Assert.Equal(Margins.Normal, sut.PaperSize.Margins);
            Assert.Equal(ViewportSize.Default, sut.ViewportSize);
            Assert.Equal(1, sut.ZoomFactor);
            Assert.Equal(ClipRectangle.Null, sut.ClipRectangle);
        }
    }
}
