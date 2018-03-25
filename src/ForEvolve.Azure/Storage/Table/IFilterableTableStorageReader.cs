using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public interface IFilterableTableStorageReader<TModel>
        where TModel : class, ITableEntity, new()
    {
        Task<IEnumerable<TModel>> ReadAsync(Func<TableQuery<TModel>, TableQuery<TModel>> filter);
    }
}