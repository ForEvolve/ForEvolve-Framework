using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public class FilterableTableStorageReader<TModel> : TableStorageRepository<TModel>, IFilterableTableStorageReader<TModel>
        where TModel : class, ITableEntity, new()
    {
        public FilterableTableStorageReader(ITableStorageSettings storageSettings)
            : base(storageSettings)
        {
        }

        public FilterableTableStorageReader(CloudTable table)
            : base(table)
        {
        }

        public async Task<IEnumerable<TModel>> ReadAsync(Func<TableQuery<TModel>, TableQuery<TModel>> filter)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }

            var query = filter(new TableQuery<TModel>());
            return await QueryTableAsync(query);
        }
    }
}
