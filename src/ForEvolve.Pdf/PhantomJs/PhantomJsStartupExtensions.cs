using ForEvolve.Pdf.Abstractions;
using ForEvolve.Pdf.PhantomJs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PhantomJsStartupExtensions
    {
        public static IServiceCollection AddPhantomJsHtmlToPdfConverter(
            this IServiceCollection services,
            Action<HtmlToPdfConverterOptions> optionsAction = null
            )
        {
            var options = new HtmlToPdfConverterOptions();
            optionsAction?.Invoke(options);
            services.TryAddSingleton(options);
            services.TryAddSingleton<IHtmlToPdfConverter, HtmlToPdfConverter>();
            services.TryAddSingleton<IOperatingSystemFinder, OperatingSystemFinder>();
            return services;
        }
    }
}
