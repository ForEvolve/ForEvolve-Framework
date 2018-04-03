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
        public static IServiceCollection AddMarkdown(
            this IServiceCollection services,
            Action<MarkdownOptions> optionsAction = null
            )
        {
            // Setup options
            var options = new MarkdownOptions();
            optionsAction?.Invoke(options);

            // Register services
            services.TryAddSingleton<IMarkdownConverter, MarkdownConverter>();
            services.AddSingleton(provider => {
                // Customize the pipeline
                var builder = new MarkdownPipelineBuilder();
                if(options.DisableHtml)
                {
                    builder.DisableHtml();
                }
                options.Configure?.Invoke(builder);
                return builder.Build();
            });
            return services;
        }
    }
}
