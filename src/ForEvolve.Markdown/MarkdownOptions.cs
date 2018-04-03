using Markdig;
using System;

namespace ForEvolve.Markdown
{
    public class MarkdownOptions
    {
        public bool DisableHtml { get; set; } = true;

        public Action<MarkdownPipelineBuilder> Configure { get; set; }
    }
}
