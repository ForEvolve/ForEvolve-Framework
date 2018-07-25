using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.Middleware;
using ForEvolve.AspNetCore.UserIdFinder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ForEvolveValueFindersStartupExtensions
    {
        /// <summary>
        /// Adds the <c>IHttpHeaderValueAccessor</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveHttpHeaderValueAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpHeaderValueAccessor, HttpHeaderValueAccessor>();
            return services;
        }

        /// <summary>
        /// Adds the <c>HttpHeaderUserIdAccessor</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="setupAction">An action used to configure the <c>HttpHeaderUserIdAccessorSettings</c>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveHttpHeaderUserIdAccessor(this IServiceCollection services, Action<HttpHeaderUserIdAccessorSettings> setupAction = null)
        {
            var userIdFinderSettings = new HttpHeaderUserIdAccessorSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.AddForEvolveHttpHeaderValueAccessor();
            services.TryAddSingleton<IUserIdAccessor, HttpHeaderUserIdAccessor>();
            services.TryAddSingleton(userIdFinderSettings);
            return services;
        }

        /// <summary>
        /// Adds the <c>AuthenticatedUserIdAccessor</c> services to the specified <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c>.
        /// </summary>
        /// <param name="services">The <c>Microsoft.Extensions.DependencyInjection.IServiceCollection</c> to add the service to.</param>
        /// <param name="setupAction">An action used to configure the <c>AuthenticatedUserIdAccessorSettings</c>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveAuthenticatedUserIdAccessor(this IServiceCollection services, Action<AuthenticatedUserIdAccessorSettings> setupAction = null)
        {
            var userIdFinderSettings = new AuthenticatedUserIdAccessorSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IUserIdAccessor, AuthenticatedUserIdAccessor>();
            services.TryAddSingleton(userIdFinderSettings);
            return services;
        }
    }
}