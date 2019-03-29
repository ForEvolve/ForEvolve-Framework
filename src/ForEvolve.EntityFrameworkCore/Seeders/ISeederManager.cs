namespace ForEvolve.EntityFrameworkCore.Seeders
{
    /// <summary>
    /// Represents a manager that executes individual seeders 
    /// against a specific <typeparamref name="TDbContext" />.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the <see cref="DbContext"/> to execute seeders against.</typeparam>
    public interface ISeederManager<TDbContext>
    {
        /// <summary>
        /// Run all seeders agaisnt the <typeparamref name="TDbContext"/>.
        /// </summary>
        void Seed();
    }
}
