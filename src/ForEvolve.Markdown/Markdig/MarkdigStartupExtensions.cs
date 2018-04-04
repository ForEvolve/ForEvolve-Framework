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
    public static class MarkdigStartupExtensions
    {
        public static IServiceCollection AddMarkdig(
            this IServiceCollection services,
            Action<MarkdigOptions> optionsAction = null
            )
        {
            // Setup options
            var options = new MarkdigOptions();
            optionsAction?.Invoke(options);

            // Register services
            services.TryAddSingleton<IMarkdownConverter, MarkdigMarkdownConverter>();
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
