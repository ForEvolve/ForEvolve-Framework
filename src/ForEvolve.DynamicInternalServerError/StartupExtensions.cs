using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForEvolve.DynamicInternalServerError;
using Microsoft.Extensions.Logging;

namespace System
{
    public static class DynamicInternalServerErrorCoreStartupExtensions
    {
        public static IServiceCollection AddDynamicInternalServerError(this IServiceCollection services, params IDynamicExceptionResultFilter[] filters)
        {
            // Register services with container
            services.TryAddSingleton(filters ?? Enumerable.Empty<IDynamicExceptionResultFilter>());
            services.AddForEvolveErrorFactory();
            return services;
        }

        public static MvcOptions ConfigureDynamicInternalServerError(this MvcOptions options)
        {
            // Register DynamicInternalServerErrorFilterAttribute with MVC
            options.Filters.Add(typeof(DynamicInternalServerErrorFilterAttribute));
            options.Filters.Add(typeof(DynamicValidationActionFilter));
            return options;
        }
    }
}