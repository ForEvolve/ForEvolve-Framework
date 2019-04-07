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
}
