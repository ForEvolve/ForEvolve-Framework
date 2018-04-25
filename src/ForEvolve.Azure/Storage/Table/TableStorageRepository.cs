using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public class TableStorageRepository<TModel> : StorageRepository<ITableStorageSettings>, ITableStorageRepository<TModel>
        where TModel : class, ITableEntity, new()
    {
        public TableStorageRepository(ITableStorageSettings storageSettings)
            : base(storageSettings)
        {
        }

        public TableStorageRepository(CloudTable table)
            : this(new CloudTableSettings(table))
        {
            
        }

        public virtual async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            var query = new TableQuery<TModel>();
            return await QueryTableAsync(query);
        }

        public virtual async Task<IEnumerable<TModel>> ReadPartitionAsync(string partitionKey)
        {
            var query = new TableQuery<TModel>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            return await QueryTableAsync(query);
        }

        public virtual async Task<TModel> ReadOneAsync(string partitionKey, string rowkey)
        {
            var typedResult = await FindEntityAsync(partitionKey, rowkey);
            return typedResult ?? default(TModel);
        }

        public virtual async Task<TModel> InsertOrMergeAsync(TModel item)
        {
            var insertOperation = TableOperation.InsertOrMerge(item);
            var result = await ExecuteTableOperationAsync(insertOperation);
            return result.Result as TModel;
        }

        public virtual async Task<TModel> InsertOrReplaceAsync(TModel item)
        {
            var insertOperation = TableOperation.InsertOrReplace(item);
            var result = await ExecuteTableOperationAsync(insertOperation);
            return result.Result as TModel;
        }

        public virtual async Task<TModel> DeleteOneAsync(string partitionKey, string rowkey)
        {
            var entity = await FindEntityAsync(partitionKey, rowkey);
            if (entity == null) { throw new AzureTableStorageException($"No entity found for keys {partitionKey} + {rowkey}."); }
            var deleteOperation = TableOperation.Delete(entity);
            var result = await ExecuteTableOperationAsync(deleteOperation);
            return entity;
        }

        public virtual async Task<IEnumerable<TModel>> DeletePartitionAsync(string partitionKey)
        {
            var models = await ReadPartitionAsync(partitionKey);
            var tasks = models.Select(m => DeleteOneAsync(partitionKey, m.RowKey));
            var removedElements = await Task.WhenAll(tasks);
            return removedElements;
        }

        public virtual async Task<TModel> InsertAsync(TModel item)
        {
            var insertOperation = TableOperation.Insert(item, true);
            var result = await ExecuteTableOperationAsync(insertOperation);
            return result.Result as TModel;
        }

        public virtual async Task<TModel> ReplaceAsync(TModel item)
        {
            var insertOperation = TableOperation.Replace(item);
            var result = await ExecuteTableOperationAsync(insertOperation);
            return result.Result as TModel;
        }

        public virtual async Task<TModel> MergeAsync(TModel item)
        {
            var insertOperation = TableOperation.Merge(item);
            var result = await ExecuteTableOperationAsync(insertOperation);
            return result.Result as TModel;
        }

        protected virtual async Task<CloudTable> GetTableAsync()
        {
            var storageAccount = StorageSettings.CreateCloudStorageAccount();
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(StorageSettings.TableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }

        protected virtual async Task<TModel> FindEntityAsync(string partitionKey, string rowkey)
        {
            var retrieveOperation = TableOperation.Retrieve<TModel>(partitionKey, rowkey);
            var result = await ExecuteTableOperationAsync(retrieveOperation);
            ValidateTableResult(result);
            return result.Result as TModel;
        }

        protected virtual async Task<IEnumerable<TModel>> QueryTableAsync(TableQuery<TModel> query)
        {
            var table = await GetTableAsync();
            TableContinuationToken continuationToken = null;
            var list = new List<TModel>();
            do
            {
                var segment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = segment.ContinuationToken;
                list.AddRange(segment.Select(x => x));
            } while (continuationToken != null);
            return list;
        }

        protected virtual async Task<TableResult> ExecuteTableOperationAsync(TableOperation operation)
        {
            var table = await GetTableAsync();
            var result = await table.ExecuteAsync(operation);
            ValidateTableResult(result);
            return result;
        }

        protected virtual void ValidateTableResult(TableResult result)
        {
            if (result == null)
            {
                throw new AzureTableStorageException(
                    "An error occured, no result were received.",
                    new ArgumentNullException(nameof(result))
                );
            }
            if (result.HttpStatusCode < 200 && result.HttpStatusCode >= 300)
            {
                throw new AzureTableStorageException($"An error occured, status code {result.HttpStatusCode}.");
            }
        }
    }
}
