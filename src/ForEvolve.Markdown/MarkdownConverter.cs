using Markdig;
using System;

namespace ForEvolve.Markdown
{
    public class MarkdownConverter : IMarkdownConverter
    {
        private readonly MarkdownPipeline _pipeline;

        public MarkdownConverter(MarkdownPipeline pipeline)
        {
            _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        }

        public string ConvertToHtml(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return markdown;
            }
            return Markdig.Markdown.ToHtml(markdown, _pipeline);
        }
    }
}
