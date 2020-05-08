using Microsoft.EntityFrameworkCore;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    /// <summary>
    /// Represents an individual seeder that execute operations against 
    /// the specified <typeparamref name="TDbContext" />.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the <see cref="DbContext"/> to seed.</typeparam>
    public interface ISeeder<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Seeds the specified database.
        /// </summary>
        /// <param name="db">The database.</param>
        void Seed(TDbContext db);
    }
}
