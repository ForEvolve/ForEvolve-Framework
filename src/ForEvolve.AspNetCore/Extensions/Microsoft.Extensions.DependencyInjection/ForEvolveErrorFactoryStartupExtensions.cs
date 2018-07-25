using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.ErrorFactory.Implementations;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveErrorFactoryStartupExtensions
    {
        /// <summary>
        /// Adds the <c>IErrorFactory</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveErrorFactory(this IServiceCollection services)
        {
            services.TryAddSingleton<IErrorFromOperationResultFactory, DefaultErrorFromOperationResultFactory>();
            services.TryAddSingleton<IErrorFromSerializableErrorFactory, DefaultErrorFromSerializableErrorFactory>();
            services.TryAddSingleton<IErrorFromIdentityErrorFactory, DefaultErrorFromIdentityErrorFactory>();
            services.TryAddSingleton<IErrorFromExceptionFactory, DefaultErrorFromExceptionFactory>();
            services.TryAddSingleton<IErrorFromDictionaryFactory, DefaultErrorFromDictionaryFactory>();
            services.TryAddSingleton<IErrorFromKeyValuePairFactory, DefaultErrorFromKeyValuePairFactory>();
            services.TryAddSingleton<IErrorFromRawValuesFactory, DefaultErrorFromRawValuesFactory>();
            services.TryAddSingleton<IErrorFactory, DefaultErrorFactory>();

            DefaultErrorFactory.Current = services
                .BuildServiceProvider()
                .GetService<IErrorFactory>();

            return services;
        }
    }
}
