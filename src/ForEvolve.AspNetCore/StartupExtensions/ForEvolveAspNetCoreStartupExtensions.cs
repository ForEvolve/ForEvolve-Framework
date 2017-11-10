using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace System
{
    public static class ForEvolveAspNetCoreStartupExtensions
    {
        public static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services)
        {
            services.AddErrorFactory();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IHttpRequestValueFinder, HttpRequestValueFinder>();

            return services;
        }
    }
}
