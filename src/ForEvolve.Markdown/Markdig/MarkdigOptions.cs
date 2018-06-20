using Markdig;
using System;

namespace ForEvolve.Markdown
{
    public class MarkdigOptions
    {
        public Action<MarkdownPipelineBuilder> Configure { get; set; }
    }
}
