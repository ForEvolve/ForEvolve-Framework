using ForEvolve.EntityFrameworkCore.Seeders;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains IForEvolveSeederBuilder extensions.
    /// </summary>
    public static class ForEvolveSeederBuilderExtensions
    {
        /// <summary>
        /// Scans the specified assembly and registers all <see cref="ISeeder{TDbContext}"/> found.
        /// </summary>
        /// <typeparam name="TAssembly">The type in which assembly that should be scanned.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IForEvolveSeederBuilder Scan<TAssembly>(this IForEvolveSeederBuilder builder)
        {
            return builder.Scan(typeof(TAssembly).Assembly);
        }

        /// <summary>
        /// Scans the specified assemblies and registers all <see cref="ISeeder{TDbContext}"/> found.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="assemblies">The assemblies to scan.</param>
        /// <returns>The builder.</returns>
        public static IForEvolveSeederBuilder Scan(this IForEvolveSeederBuilder builder, params Assembly[] assemblies)
        {
            builder.Services.Scan(s => s
                .FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo(typeof(ISeeder<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );
            return builder;
        }
    }
}
