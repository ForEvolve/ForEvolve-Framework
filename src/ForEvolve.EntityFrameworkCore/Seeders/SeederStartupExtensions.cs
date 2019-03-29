using ForEvolve.EntityFrameworkCore.Seeders;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Startup extensions to register the seeders.
    /// </summary>
    public static class SeederStartupExtensions
    {
        /// <summary>
        /// Adds the seeders factory and managers and scans the specified assembly to find <see cref="ISeeder{TDbContext}"/>.
        /// </summary>
        /// <typeparam name="TAssembly">A type in the assembly to scan for <see cref="ISeeder{TDbContext}"/>.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveSeeders<TAssembly>(this IServiceCollection services)
        {
            return services.AddForEvolveSeeders(typeof(TAssembly).Assembly);
        }

        /// <summary>
        /// Adds the seeders factory and managers and scans the specified assemblies to find <see cref="ISeeder{TDbContext}"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="assemblies">The assemblies to scan.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddForEvolveSeeders(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<ISeederManagerFactory, SeederManagerFactory>();
            services.AddTransient(typeof(ISeederManager<>), typeof(SeederManager<>));
            services.Scan(s => s
                .FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo(typeof(ISeeder<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );
            return services;
        }
    }
}
