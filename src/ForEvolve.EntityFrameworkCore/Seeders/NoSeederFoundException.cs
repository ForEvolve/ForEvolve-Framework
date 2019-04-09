using Microsoft.EntityFrameworkCore;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    /// <summary>
    /// This exception is thrown when no seeders are configured in SeederManager.
    /// Implements the <see cref="ForEvolveException" />
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <seealso cref="ForEvolveException" />
    public class NoSeederFoundException<TDbContext> : ForEvolveException
        where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoSeederFoundException{TDbContext}"/> class.
        /// </summary>
        public NoSeederFoundException()
            : base($"No seeder for db context of type '{typeof(TDbContext).Name}' where found. You can define seeders by impleting the 'ISeeder<{typeof(TDbContext).Name}>' interface.")
        {
        }
    }
}
