using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace ForEvolve.EntityFrameworkCore.Seeders.TestData
{
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
}