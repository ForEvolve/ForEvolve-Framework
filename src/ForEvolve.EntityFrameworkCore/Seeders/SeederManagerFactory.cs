using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    /// <summary>
    /// Default <see cref="ISeederManagerFactory"/> implementation.
    /// Implements the <see cref="ISeederManagerFactory" />
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="ForEvolve.EntityFrameworkCore.Seeders.ISeederManagerFactory" />
    /// <seealso cref="System.IDisposable" />
    public class SeederManagerFactory : ISeederManagerFactory, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _serviceScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeederManagerFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider that contains the <see cref="ISeederManager{TDbContext}"/> instances.</param>
        /// <exception cref="ArgumentNullException">serviceProvider</exception>
        public SeederManagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _serviceScope = _serviceProvider.CreateScope();
        }

        /// <summary>
        /// Creates the <see cref="ISeederManager{TDbContext}"/> instance.
        /// </summary>
        /// <typeparam name="TDbContext">The type of the DbContext used by the <see cref="ISeederManager{TDbContext}"/>.</typeparam>
        /// <returns>The requested <see cref="ISeederManager{TDbContext}"/> instance.</returns>
        public ISeederManager<TDbContext> Create<TDbContext>() where TDbContext : DbContext
        {
            return _serviceScope.ServiceProvider.GetService<ISeederManager<TDbContext>>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
