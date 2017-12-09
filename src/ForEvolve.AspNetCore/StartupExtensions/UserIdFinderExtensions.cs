using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.Middleware;
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
        public static IServiceCollection AddUserIdFinder(this IServiceCollection services, Action<UserIdFinderSettings> setupAction = null)
        {
            var userIdFinderSettings = new UserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton<IUserIdFinder, UserIdFinder>();
            services.TryAddSingleton(userIdFinderSettings);
            return services;
        }

        public static IServiceCollection AddUserIdSetter<TUserIdAccessor>(this IServiceCollection services)
            where TUserIdAccessor : class, IUserIdAccessor
        {
            services.TryAddSingleton<UserIdFinderSettings>();
            services.TryAddSingleton<IUserIdAccessor, TUserIdAccessor>();
            return services;
        }

        public static IServiceCollection AddUserIdSetter<TUserIdAccessor>(this IServiceCollection services, Action<UserIdFinderSettings> setupAction = null)
            where TUserIdAccessor : class, IUserIdAccessor
        {
            var userIdFinderSettings = new UserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton(userIdFinderSettings);
            services.TryAddSingleton<IUserIdAccessor, TUserIdAccessor>();
            return services;
        }

        public static IServiceCollection AddUserIdSetter(this IServiceCollection services, IUserIdAccessor userIdAccessor, Action<UserIdFinderSettings> setupAction = null)
        {
            var userIdFinderSettings = new UserIdFinderSettings();
            setupAction?.Invoke(userIdFinderSettings);

            services.TryAddSingleton(userIdFinderSettings);
            services.TryAddSingleton(userIdAccessor);
            return services;
        }

        public static IServiceCollection AddUserIdSetter(this IServiceCollection services, IUserIdAccessor userIdAccessor)
        {
            services.TryAddSingleton<UserIdFinderSettings>();
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