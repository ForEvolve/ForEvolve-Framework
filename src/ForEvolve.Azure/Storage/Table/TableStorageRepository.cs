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

        public async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            var query = new TableQuery<TModel>();
            return await QueryTable(query);
        }

        public async Task<IEnumerable<TModel>> ReadPartitionAsync(string partitionKey)
        {
            var query = new TableQuery<TModel>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            return await QueryTable(query);
        }

        public async Task<TModel> ReadOneAsync(string partitionKey, string rowkey)
        {
            var typedResult = await ASFindAsync(partitionKey, rowkey);
            return typedResult ?? default(TModel);
        }

        public async Task<TModel> InsertOrMergeAsync(TModel item)
        {
            var insertOperation = TableOperation.InsertOrMerge(item);
            var result = await ASExecuteTableOperation(insertOperation);
            return result.Result as TModel;
        }

        public async Task<TModel> InsertOrReplaceAsync(TModel item)
        {
            var insertOperation = TableOperation.InsertOrReplace(item);
            var result = await ASExecuteTableOperation(insertOperation);
            return result.Result as TModel;
        }

        public async Task<TModel> RemoveAsync(string partitionKey, string rowkey)
        {
            var entity = await ASFindAsync(partitionKey, rowkey);
            if (entity == null) { throw new AzureTableStorageException($"No entity found for keys {partitionKey} + {rowkey}."); }
            var deleteOperation = TableOperation.Delete(entity);
            var result = await ASExecuteTableOperation(deleteOperation);
            return entity;
        }

        public async Task<IEnumerable<TModel>> RemoveAsync(string partitionKey)
        {
            var models = await ReadPartitionAsync(partitionKey);
            var tasks = models.Select(m => RemoveAsync(partitionKey, m.RowKey));
            var removedElements = await Task.WhenAll(tasks);
            return removedElements;
        }


        protected async Task<CloudTable> GetTableAsync()
        {
            var storageAccount = StorageSettings.CreateCloudStorageAccount();
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(StorageSettings.TableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }

        protected async Task<TModel> ASFindAsync(string partitionKey, string rowkey)
        {
            var retrieveOperation = TableOperation.Retrieve<TModel>(partitionKey, rowkey);
            var result = await ASExecuteTableOperation(retrieveOperation);
            ValidateTableResult(result);
            return result.Result as TModel;
        }

        protected async Task<IEnumerable<TModel>> QueryTable(TableQuery<TModel> query)
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

        protected async Task<TableResult> ASExecuteTableOperation(TableOperation operation)
        {
            var table = await GetTableAsync();
            var result = await table.ExecuteAsync(operation);
            ValidateTableResult(result);
            return result;
        }

        protected void ValidateTableResult(TableResult result)
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
