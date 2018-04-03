using ForEvolve.Markdown;
using Markdig;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MarkdownStartupExtensions
    {
        public static IServiceCollection AddMarkdown(this IServiceCollection services, Action<MarkdownPipelineBuilder> configure = null)
        {
            services.TryAddSingleton<IMarkdownConverter, MarkdownConverter>();
            services.AddSingleton(provider => {
                var pipeline = new MarkdownPipelineBuilder();
                configure?.Invoke(pipeline);
                return pipeline.Build();
            });
            return services;
        }
    }
}
