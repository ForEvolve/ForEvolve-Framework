using ForEvolve.OperationResults;
using ForEvolve.OperationResults.Standardizer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OperationResultStartupExtensions
    {
        /// <summary>
        /// Adds the default ForEvolve operation result standardizer filters.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddForEvolveOperationResultStandardizer(this IServiceCollection services)
        {
            services
                .AddLogging()
                .AddSingleton<IPropertyNameFormatter, DefaultPropertyNameFormatter>()
                .AddSingleton<IPropertyValueFormatter, DefaultPropertyValueFormatter>()
                .AddSingleton<IOperationResultStandardizer, DefaultOperationResultStandardizer>()
                .AddOptions<DefaultOperationResultStandardizerOptions>()
            ;
            services
                .Configure<MvcOptions>(options =>
                {
                    options.Filters.Add<OperationResultStandardizerActionFilter<CreatedAtActionResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<CreatedAtRouteResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<CreatedResult>>();

                    options.Filters.Add<OperationResultStandardizerActionFilter<OkObjectResult>>();

                    options.Filters.Add<OperationResultStandardizerActionFilter<BadRequestObjectResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<ConflictObjectResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<NotFoundObjectResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<UnprocessableEntityObjectResult>>();
                });
            return services;
        }
    }
}
