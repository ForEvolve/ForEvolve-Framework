using ForEvolve.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OperationResultsStartupExtensions
    {
        public static IServiceCollection AddForEvolveOperationResults(this IServiceCollection services)
        {
            services.TryAddSingleton<IOperationResultFactory, DefaultOperationResultFactory>();
            services.AddForEvolveErrorFactory();
            return services;
        }
    }
}
