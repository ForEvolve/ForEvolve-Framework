using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public interface ITableStorageRepository<TModel>
        where TModel : class, ITableEntity, new()
    {
        Task<IEnumerable<TModel>> ReadPartitionAsync(string partitionKey);
        Task<TModel> ReadOneAsync(string partitionKey, string rowkey);
        Task<IEnumerable<TModel>> ReadAllAsync();
        Task<TModel> InsertOrMergeAsync(TModel item);
        Task<TModel> InsertOrReplaceAsync(TModel item);
        Task<TModel> DeleteOneAsync(string partitionKey, string rowkey);
        Task<IEnumerable<TModel>> DeletePartitionAsync(string partitionKey);
        Task<TModel> InsertAsync(TModel item);
        Task<TModel> ReplaceAsync(TModel item);
        Task<TModel> MergeAsync(TModel item);
    }
}