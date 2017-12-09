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
using ForEvolve.AspNetCore.Emails;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveAspNetCoreStartupExtensions
    {
        public static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services,
            IConfiguration configuration,
            Action<ForEvolveAspNetCoreSettings> setupAction = null)
        {
            var settings = new ForEvolveAspNetCoreSettings
            {
                Configuration = configuration
            };
            setupAction?.Invoke(settings);
            return services.AddForEvolveAspNetCore(settings);
        }

        public static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services,
            EmailOptions emailOptions,
            Action<ForEvolveAspNetCoreSettings> setupAction = null)
        {
            var settings = new ForEvolveAspNetCoreSettings();
            setupAction?.Invoke(settings);
            return services.AddSingleton(emailOptions)
                .AddForEvolveAspNetCore(settings);
        }

        private static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services, ForEvolveAspNetCoreSettings settings)
        {
            // Setup configs
            services.AddSingleton(settings);

            // Error and OperationResults
            services
                .AddForEvolveErrorFactory()
                .AddForEvolveOperationResults()
                ;

            // Others
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IHttpRequestValueFinder, HttpRequestValueFinder>();

            // Emails
            services.TryAddSingleton<IEmailSender, DefaultEmailSender>();
            var emailOptions = new EmailOptions();
            settings.Configuration?.Bind(settings.EmailOptionsConfigurationKey, emailOptions);
            services.TryAddSingleton(emailOptions);

            return services;
        }
    }

    public class ForEvolveAspNetCoreSettings
    {
        public const string DefaultEmailOptionsConfigurationKey = "EmailOptions";
        public ForEvolveAspNetCoreSettings()
        {
            EmailOptionsConfigurationKey = DefaultEmailOptionsConfigurationKey;
        }

        public IConfiguration Configuration { get; set; }
        public string EmailOptionsConfigurationKey { get; set; }
    }
}
