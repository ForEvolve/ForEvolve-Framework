using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.Seeders.TestData
{
    public class LocalDbSeederDbContextFactory : IDisposable
    {
        private readonly List<string> _databaseNames = new List<string>();

        public SeederDbContext Create(string databaseName)
        {
            var context = SeederDbContext.CreateLocalDb(databaseName);
            context.Database.EnsureCreated();
            if (!_databaseNames.Contains(databaseName))
            {
                _databaseNames.Add(databaseName);
            }
            return context;
        }

        public void Dispose()
        {
            for (int i = 0; i < _databaseNames.Count; i++)
            {
                var name = _databaseNames[i];
                var context = Create(name);
                context.Database.EnsureDeleted();
            }
        }
    }

    [CollectionDefinition(Name)]
    public class SeederDbContextCollection : ICollectionFixture<LocalDbSeederDbContextFactory>
    {
        public const string Name = "SeederDbContext Collection";
    }
}
