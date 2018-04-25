using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public interface ITableStorageReader
    {
        Task<IEnumerable<TModel>> ReadAsync<TModel>(Func<TableQuery<TModel>, TableQuery<TModel>> filter)
            where TModel : class, ITableEntity, new();
        Task<IEnumerable<TModel>> ReadAsync<TModel>(ITableStorageSettings tableStorageSettings, Func<TableQuery<TModel>, TableQuery<TModel>> filter)
            where TModel : class, ITableEntity, new();
    }
}