using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    public static class PhantomJsStartupExtensions
    {
        public static IServiceCollection AddPhantomJsHtmlToPdfConverter(
            this IServiceCollection services,
            Action<HtmlToPdfConverterOptions> optionsAction = null
            )
        {
            return services;
        }
    }
}
