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
    public static class UserIdFinderExtensions
    {
        public static IServiceCollection AddHttpHeaderUserIdFinder(this IServiceCollection services, Action<HttpHeaderUserIdFinderSettings> setupAction = null)
        {
            var userIdFinderSettings = new HttpHeaderUserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton<IUserIdFinder, HttpHeaderUserIdFinder>();
            services.TryAddSingleton(userIdFinderSettings);
            return services;
        }

        public static IServiceCollection AddAuthenticatedUserIdFinder(this IServiceCollection services, Action<AuthenticatedUserIdFinderSettings> setupAction = null)
        {
            var userIdFinderSettings = new AuthenticatedUserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton<IUserIdFinder, AuthenticatedUserIdFinder>();
            services.TryAddSingleton(userIdFinderSettings);
            return services;
        }

        public static IServiceCollection AddUserIdSetter<TUserIdAccessor>(this IServiceCollection services)
            where TUserIdAccessor : class, IUserIdAccessor
        {
            services.TryAddSingleton<HttpHeaderUserIdFinderSettings>();
            services.TryAddSingleton<IUserIdAccessor, TUserIdAccessor>();
            return services;
        }

        public static IServiceCollection AddUserIdSetter<TUserIdAccessor>(this IServiceCollection services, Action<HttpHeaderUserIdFinderSettings> setupAction = null)
            where TUserIdAccessor : class, IUserIdAccessor
        {
            var userIdFinderSettings = new HttpHeaderUserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton(userIdFinderSettings);
            services.TryAddSingleton<IUserIdAccessor, TUserIdAccessor>();
            return services;
        }

        public static IServiceCollection AddUserIdSetter(this IServiceCollection services, IUserIdAccessor userIdAccessor, Action<HttpHeaderUserIdFinderSettings> setupAction = null)
        {
            var userIdFinderSettings = new HttpHeaderUserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton(userIdFinderSettings);
            services.TryAddSingleton(userIdAccessor);
            return services;
        }

        public static IServiceCollection AddUserIdSetter(this IServiceCollection services, IUserIdAccessor userIdAccessor)
        {
            services.TryAddSingleton<HttpHeaderUserIdFinderSettings>();
            services.TryAddSingleton(userIdAccessor);
            return services;
        }
    }
}
namespace Microsoft.AspNetCore.Builder
{
    public static class UserIdFinderExtensions
    {
        public static IApplicationBuilder UseUserIdSetter(this IApplicationBuilder app)
        {
            app.UseMiddleware<SetUserIdHeaderMiddleware>();
            return app;
        }
    }
}