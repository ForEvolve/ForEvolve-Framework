using Markdig;
using System;
using Xunit;

namespace ForEvolve.Markdown
{
    public class MarkdigMarkdownConverterTest
    {
        public class Ctor : MarkdigMarkdownConverterTest
        {
            [Fact]
            public void Should_guard_against_null()
            {
                // Arrange
                var nullMarkdownPipeline = default(MarkdownPipeline);

                // Act & Assert
                Assert.Throws<ArgumentNullException>("pipeline", () => new MarkdigMarkdownConverter(nullMarkdownPipeline));
            }
        }

        public class ConvertToHtml : MarkdigMarkdownConverterTest
        {
            [Theory]
            [InlineData("", "")]
            [InlineData(null, null)]
            [InlineData("Some text", "<p>Some text</p>\n")]
            [InlineData("Some **text**", "<p>Some <strong>text</strong></p>\n")]
            [InlineData("Some *text*", "<p>Some <em>text</em></p>\n")]
            public void Should_convert_input(string input, string expectedOutput)
            {
                // Arrange
                var pipeline = new MarkdownPipelineBuilder().Build();
                var sut = new MarkdigMarkdownConverter(pipeline);

                // Act
                var result = sut.ConvertToHtml(input);

                // Assert
                Assert.Equal(expectedOutput, result);
            }
        }
    }
}
