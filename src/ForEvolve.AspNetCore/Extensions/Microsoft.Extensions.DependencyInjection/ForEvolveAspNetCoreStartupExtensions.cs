using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// Adds most ForEvolve.AspNetCore services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="configuration">The <c>IConfiguration</c> to be used to bind automatic settings, if any.</param>
        /// <param name="setupAction">An action used to configure the <c>ForEvolveAspNetCoreSettings</c>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services,
            IConfiguration configuration,
            Action<ForEvolveAspNetCoreSettings> setupAction = null)
        {
            // ForEvolve.AspNetCore
            var settings = new ForEvolveAspNetCoreSettings { Configuration = configuration };
            setupAction?.Invoke(settings);
            return services
                .InternalAddForEvolveAspNetCore(settings)

                // Emails
                .AddEmailSender(settings)
                ;
        }

        /// <summary>
        /// Adds most ForEvolve.AspNetCore services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="emailOptions">The <c>EmailOptions</c> used to configure the email services.</param>
        /// <param name="setupAction">An action used to configure the <c>ForEvolveAspNetCoreSettings</c>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services,
            EmailOptions emailOptions,
            Action<ForEvolveAspNetCoreSettings> setupAction = null)
        {
            // ForEvolve.AspNetCore
            var settings = new ForEvolveAspNetCoreSettings();
            setupAction?.Invoke(settings);
            return services
                .InternalAddForEvolveAspNetCore(settings)

                // Emails
                .AddEmailSender(emailOptions)
                ;
        }

        /// <summary>
        /// Adds most ForEvolve.AspNetCore services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="setupAction">An action used to configure the <c>ForEvolveAspNetCoreSettings</c>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveAspNetCore(this IServiceCollection services,
            Action<ForEvolveAspNetCoreSettings> setupAction = null)
        {
            // ForEvolve.AspNetCore
            var settings = new ForEvolveAspNetCoreSettings();
            setupAction?.Invoke(settings);
            return services
                .InternalAddForEvolveAspNetCore(settings)

                // Emails
                .AddEmailSender(settings)
                ;
        }

        private static IServiceCollection InternalAddForEvolveAspNetCore(this IServiceCollection services, ForEvolveAspNetCoreSettings settings)
        {
            services
                // Setup configs
                .AddSingleton(settings)

                // Error and OperationResults
                .AddForEvolveErrorFactory()
                .AddForEvolveOperationResults()

                // HttpHeaderValueAccessor
                .AddHttpHeaderValueAccessor()

                // ViewRenderer
                .AddViewRenderer()
                ;
            return services;
        }
    }
}