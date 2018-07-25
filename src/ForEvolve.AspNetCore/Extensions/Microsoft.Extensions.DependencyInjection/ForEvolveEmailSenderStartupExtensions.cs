using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.Emails;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveEmailSenderStartupExtensions
    {
        /// <summary>
        /// Adds the <c>IEmailSender</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// Default <c>EmailOptions</c> will be used.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="emailOptionsAction">The action used to configure the <c>EmailOptions</c>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEmailSender(this IServiceCollection services, Action<EmailOptions> emailOptionsAction = null)
        {
            var emailOptions = new EmailOptions();
            emailOptionsAction?.Invoke(emailOptions);
            return services.AddEmailSender(emailOptions);
        }

        /// <summary>
        /// Adds the <c>IEmailSender</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// <c>EmailOptions</c> will be loaded from the the settings configuration, 
        /// if any; otherwise, default <c>EmailOptions</c> will be used instead.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="settings">
        /// The settings used to bind configurations to the <c>EmailOptions</c>.
        /// Make sure the Configuration and EmailOptionsConfigurationKey property are set.
        /// </param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEmailSender(this IServiceCollection services, ForEvolveAspNetCoreSettings settings)
        {
            var emailOptions = new EmailOptions();
            settings.Configuration?.Bind(settings.EmailOptionsConfigurationKey, emailOptions);
            return services.AddEmailSender(emailOptions);
        }

        /// <summary>
        /// Adds the <c>IEmailSender</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>, configured by the specified <c>EmailOptions</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="emailOptions">The <c>EmailOptions</c> to be used.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEmailSender(this IServiceCollection services, EmailOptions emailOptions)
        {
            services.AddSingleton(emailOptions);
            services.TryAddSingleton<IEmailSender, DefaultEmailSender>();
            return services;
        }
    }
}