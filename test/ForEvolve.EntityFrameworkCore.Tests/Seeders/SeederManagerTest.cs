using ForEvolve.EntityFrameworkCore.Seeders.TestData;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.Seeders
{
    public class SeederManagerTest
    {
        private readonly SeederManager<SeederDbContext> sut;
        private readonly SeederDbContext _db;
        private readonly List<ISeeder<SeederDbContext>> _seeders;

        public SeederManagerTest()
        {
            _db = SeederDbContext.Create("SeederManagerTest");
            _seeders = new List<ISeeder<SeederDbContext>>();
            sut = new SeederManager<SeederDbContext>(_db, _seeders);
        }

        public class Ctor : SeederManagerTest
        {
            [Fact]
            public void Should_guard_against_null_db()
            {
                // Arrange
                var nullDbContext = default(SeederDbContext);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "db",
                    () => new SeederManager<SeederDbContext>(nullDbContext, _seeders));
            }

            [Fact]
            public void Should_guard_against_null_seeders()
            {
                // Arrange
                var nullSeeders = default(IEnumerable<ISeeder<SeederDbContext>>);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(
                    "seeders",
                    () => new SeederManager<SeederDbContext>(_db, nullSeeders));
            }
        }

        public class Seed : SeederManagerTest
        {
            [Fact]
            public void Should_rollback_the_transaction_when_a_seeder_throws_an_exception()
            {
                // Arrange
                var expectedException = new Exception();
                var seederMock = new Mock<ISeeder<SeederDbContext>>();
                seederMock.Setup(x => x.Seed(_db)).Throws(expectedException);
                _seeders.Add(seederMock.Object);

                // Act
                var ex = Assert.Throws<Exception>(() => sut.Seed());

                // Assert
                Assert.Same(expectedException, ex);
                Assert.Equal(1, _db.BeginTransactionCalls);
                Assert.Equal(0, _db.SaveChangesCalls);
                Assert.Equal(1, _db.RollbackTransactionCalls);
            }

            [Fact]
            public void Should_rollback_the_transaction_when_SaveChanges_throws_an_exception()
            {
                // Arrange
                var expectedException = new Exception();
                _db.SaveChangesException = expectedException;
                _seeders.Add(new Mock<ISeeder<SeederDbContext>>().Object);

                // Act
                var ex = Assert.Throws<Exception>(() => sut.Seed());

                // Assert
                Assert.Same(expectedException, ex);
                Assert.Equal(1, _db.BeginTransactionCalls);
                Assert.Equal(0, _db.SaveChangesCalls);
                Assert.Equal(1, _db.RollbackTransactionCalls);
            }

            [Fact]
            public void Should_throw_a_NoSeederFoundException_when_no_seeder_are_found()
            {
                Assert.Throws<NoSeederFoundException<SeederDbContext>>(() => sut.Seed());
            }


            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            public void Should_commit_the_transaction_when_all_seeders_succeeds(int amountOfSeeders)
            {
                // Arrange
                var seederCalls = 0;
                for (int i = 0; i < amountOfSeeders; i++)
                {
                    var seederMock = new Mock<ISeeder<SeederDbContext>>();
                    seederMock.Setup(x => x.Seed(_db)).Callback(() => seederCalls++);
                    _seeders.Add(seederMock.Object);
                }

                // Act
                sut.Seed();

                // Assert
                Assert.Equal(amountOfSeeders, seederCalls);
                Assert.Equal(1, _db.BeginTransactionCalls);
                Assert.Equal(1, _db.SaveChangesCalls);
                Assert.Equal(1, _db.CommitTransactionCalls);
            }
        }
    }

    namespace TestData
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

        public class TestDatabaseFacade : DatabaseFacade
        {
            public TestDatabaseFacade(DbContext context) : base(context) { }

            public int BeginTransactionCalls { get; private set; }
            public int CommitTransactionCalls { get; private set; }
            public int RollbackTransactionCalls { get; private set; }

            public override IDbContextTransaction BeginTransaction()
            {
                var transactionMock = new Mock<IDbContextTransaction>();
                transactionMock
                    .Setup(s => s.Commit())
                    .Callback(() => CommitTransactionCalls++);
                transactionMock
                    .Setup(s => s.Rollback())
                    .Callback(() => RollbackTransactionCalls++);
                BeginTransactionCalls++;
                return transactionMock.Object;
            }

            public override void CommitTransaction()
            {
                CommitTransactionCalls++;
            }

            public override void RollbackTransaction()
            {
                RollbackTransactionCalls++;
            }
        }

        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
