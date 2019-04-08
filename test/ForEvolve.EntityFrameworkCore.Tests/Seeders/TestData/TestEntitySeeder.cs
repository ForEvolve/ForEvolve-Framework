using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.EntityFrameworkCore.Seeders.TestData
{
    public class TestEntitySeeder : ISeeder<SeederDbContext>
    {
        private Action<SeederDbContext> _seedDelegate;

        public TestEntitySeeder(Action<SeederDbContext> seedDelegate)
        {
            _seedDelegate = seedDelegate ?? throw new ArgumentNullException(nameof(seedDelegate));
        }

        public void Seed(SeederDbContext db)
        {
            _seedDelegate(db);
        }
    }
}
