using System.Collections.Generic;

namespace ForEvolve.EntityFrameworkCore.Seeders.TestData
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Object { get; set; }
        public Dictionary<string, object> Dictionary { get; set; }
    }
}