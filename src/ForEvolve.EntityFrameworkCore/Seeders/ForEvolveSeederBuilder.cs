using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This classrepresents the default <see cref="IForEvolveSeederBuilder" /> implementation. This class cannot be inherited.
    /// Implements the <see cref="IForEvolveSeederBuilder" />
    /// </summary>
    /// <seealso cref="IForEvolveSeederBuilder" />
    public sealed class ForEvolveSeederBuilder : IForEvolveSeederBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForEvolveSeederBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <exception cref="ArgumentNullException">services</exception>
        public ForEvolveSeederBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <inheritdoc />
        public IServiceCollection Services { get; }
    }
}
