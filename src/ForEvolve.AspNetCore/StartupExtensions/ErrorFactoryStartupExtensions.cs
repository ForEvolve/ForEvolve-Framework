using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.ErrorFactory.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ErrorFactoryStartupExtensions
    {
        public static IServiceCollection AddErrorFactory(this IServiceCollection services)
        {
            services.AddSingleton<IErrorFromExceptionFactory, DefaultErrorFromExceptionFactory>();
            services.AddSingleton<IErrorFromDictionaryFactory, DefaultErrorFromDictionaryFactory>();
            services.AddSingleton<IErrorFromKeyValuePairFactory, DefaultErrorFromKeyValuePairFactory>();
            services.AddSingleton<IErrorFromRawValuesFactory, DefaultErrorFromRawValuesFactory>();
            services.AddSingleton<IErrorFactory, DefaultErrorFactory>();

            return services;
        }
    }
}
