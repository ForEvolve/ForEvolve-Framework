using ForEvolve.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveOperationResultsStartupExtensions
    {
        /// <summary>
        /// Adds the <c>IOperationResultFactory</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveOperationResults(this IServiceCollection services)
        {
            services.TryAddSingleton<IOperationResultFactory, DefaultOperationResultFactory>();
            services.AddForEvolveErrorFactory();
            return services;
        }
    }
}
