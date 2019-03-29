using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    /// <summary>
    /// Represents a manager that execute all seeders against the specified 
    /// <typeparamref name="TDbContext"/>. 
    /// Implements the <see cref="ISeederManager{TDbContext}" />
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext that this manager manages.</typeparam>
    /// <seealso cref="ISeederManager{TDbContext}" />
    public class SeederManager<TDbContext> : ISeederManager<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _db;
        private readonly IEnumerable<ISeeder<TDbContext>> _seeders;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeederManager{TDbContext}"/> class.
        /// </summary>
        /// <param name="db">The <typeparamref name="TDbContext"/> used by the seeder.</param>
        /// <param name="seeders">The seeders to execute against the specified <typeparamref name="TDbContext"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// db
        /// or
        /// seeders
        /// </exception>
        public SeederManager(TDbContext db, IEnumerable<ISeeder<TDbContext>> seeders)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _seeders = seeders ?? throw new ArgumentNullException(nameof(seeders));
        }

        /// <summary>
        /// Seeds the <typeparamref name="TDbContext"/> database. 
        /// All seeders are run inside a transaction. A call to 
        /// <see cref="DbContext.SaveChanges()"/> is done after that all 
        /// <see cref="ISeeder{TDbContext}"/> has been called. The 
        /// transaction is rolled back if an exeception arise.
        /// </summary>
        public void Seed()
        {
            var transaction = _db.Database.BeginTransaction();
            try
            {
                foreach (var seeder in _seeders)
                {
                    seeder.Seed(_db);
                }
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
