using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ForEvolve.Azure.ApplicationInsights;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveApplicationInsightsStartupExtensions
    {
        public static IServiceCollection AddForEvolveApplicationInsights(this IServiceCollection services)
        {
            services.AddTransient<ITelemetryClient, TelemetryClientWrapper>();
            services.AddTransient<TrackExceptionsFilterAttribute>();
            return services;
        }

        public static MvcOptions ConfigureForEvolveApplicationInsights(this MvcOptions options)
        {
            // Register TrackExceptionsFilterAttribute with MVC
            options.Filters.Add(typeof(TrackExceptionsFilterAttribute));
            return options;
        }
    }
}
