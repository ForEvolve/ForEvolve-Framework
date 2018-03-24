using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public class FilterableTableStorageRepository<TModel> : TableStorageRepository<TModel>, IFilterableTableStorageRepository<TModel>
        where TModel : class, ITableEntity, new()
    {
        public FilterableTableStorageRepository(ITableStorageSettings storageSettings)
            : base(storageSettings)
        {
        }

        public FilterableTableStorageRepository(CloudTable table)
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
