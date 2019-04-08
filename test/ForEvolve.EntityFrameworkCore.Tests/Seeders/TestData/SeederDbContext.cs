using ForEvolve.EntityFrameworkCore.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace ForEvolve.EntityFrameworkCore.Seeders.TestData
{
    public class SeederDbContext : DbContext
    {
        private readonly ObjectToJsonConverter _objectToJsonConverter = new ObjectToJsonConverter();
        private readonly DictionaryToJsonConverter _dictionaryToJsonConverter = new DictionaryToJsonConverter();

        internal SeederDbContext(DbContextOptions options) : base(options)
        {
            _database = new TestDatabaseFacade(this);
        }

        public DbSet<TestEntity> TestEntities { get; set; }

        public static SeederDbContext CreateInMemory(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<SeederDbContext>()
                .UseInMemoryDatabase(databaseName);
            return new SeederDbContext(builder.Options);
        }

        public static SeederDbContext CreateLocalDb(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<SeederDbContext>()
                .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;ConnectRetryCount=0");
            return new SeederDbContext(builder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<TestEntity>()
               .Property(e => e.Object)
               .HasConversion(_objectToJsonConverter);
            modelBuilder
               .Entity<TestEntity>()
               .Property(e => e.Dictionary)
               .HasConversion(_dictionaryToJsonConverter);

            base.OnModelCreating(modelBuilder);
        }

        public int BeginTransactionCalls => _database.BeginTransactionCalls;
        public int CommitTransactionCalls => _database.CommitTransactionCalls;
        public int RollbackTransactionCalls => _database.RollbackTransactionCalls;
        public int SaveChangesCalls { get; private set; }


        private readonly TestDatabaseFacade _database;
        public override DatabaseFacade Database => _database;

        public bool SaveChangesShouldThrow => SaveChangesException != default;
        public Exception SaveChangesException { get; set; }

        public bool SaveChangesShouldSave { get; set; }


        public override int SaveChanges()
        {
            if (SaveChangesShouldThrow)
            {
                throw SaveChangesException;
            }
            SaveChangesCalls++;
            if (SaveChangesShouldSave)
            {
                return base.SaveChanges();
            }
            return 1;
        }
    }
}
