using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace ForEvolve.EntityFrameworkCore.Seeders.TestData
{
    public class SeederDbContext : DbContext
    {
        private SeederDbContext(DbContextOptions options) : base(options)
        {
            _database = new TestDatabaseFacade(this);
        }

        public DbSet<TestEntity> TestEntities { get; set; }

        public static SeederDbContext Create(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<SeederDbContext>()
                .UseInMemoryDatabase(databaseName);
            return new SeederDbContext(builder.Options);
        }

        public int BeginTransactionCalls => _database.BeginTransactionCalls;
        public int CommitTransactionCalls => _database.CommitTransactionCalls;
        public int RollbackTransactionCalls => _database.RollbackTransactionCalls;
        public int SaveChangesCalls { get; private set; }


        private readonly TestDatabaseFacade _database;
        public override DatabaseFacade Database => _database;

        public bool SaveChangesShouldThrow => SaveChangesException != default;
        public Exception SaveChangesException { get; set; }

        public override int SaveChanges()
        {
            if (SaveChangesShouldThrow)
            {
                throw SaveChangesException;
            }
            SaveChangesCalls++;
            return 1;
        }
    }
}
