using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    /// <summary>
    /// Represents a manager that execute individual seeders against a specific DbContext.
    /// </summary>
    public interface ISeederManagerFactory
    {
        /// <summary>
        /// Creates a <see cref="ISeederManager{TDbContext}"/> instance.
        /// </summary>
        /// <typeparam name="TDbContext">The type of the <see cref="DbContext"/> managed by the <see cref="ISeederManager{TDbContext}"/>.</typeparam>
        /// <returns>The created <see cref="ISeederManager{TDbContext}"/> instance.</returns>
        ISeederManager<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext;
    }
}
