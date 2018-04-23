using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public class TableStorageReader : ITableStorageReader
    {
        private readonly ITableStorageFactory _tableStorageFactory;

        public TableStorageReader(ITableStorageFactory tableStorageFactory)
        {
            _tableStorageFactory = tableStorageFactory ?? throw new ArgumentNullException(nameof(tableStorageFactory));
        }

        public Task<IEnumerable<TModel>> ReadAsync<TModel>(Func<TableQuery<TModel>, TableQuery<TModel>> filter)
            where TModel : class, ITableEntity, new()
        {
            var reader = _tableStorageFactory.CreateReader<TModel>();
            return reader.ReadAsync(filter);
        }

        public Task<IEnumerable<TModel>> ReadAsync<TModel>(ITableStorageSettings tableStorageSettings, Func<TableQuery<TModel>, TableQuery<TModel>> filter)
            where TModel : class, ITableEntity, new()
        {
            var reader = _tableStorageFactory.CreateReader<TModel>(tableStorageSettings);
            return reader.ReadAsync(filter);
        }
    }
}
