using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Object
{
    public class ObjectStorageRepository<TModel> : StorageRepository<IObjectStorageSettings>, IObjectStorageRepository<TModel>
    {
        public ObjectStorageRepository(IObjectStorageSettings storageSettings)
            : base(storageSettings)
        {

        }

        public async Task InsertOrUpdateAsync(string fileName, TModel model)
        {
            var serializedModel = JsonConvert.SerializeObject(model);
            var container = await GetContainerAsync();
            var blob = container.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = "application/json";
            await blob.UploadTextAsync(serializedModel);
        }

        public async Task DeleteAsync(string fileName)
        {
            var container = await GetContainerAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<TModel> ReadOneAsync(string fileName)
        {
            var container = await GetContainerAsync();
            var blockBlob = container.GetBlockBlobReference(fileName);
            var obj = await ReadOneBlockAsync(blockBlob);
            return obj;
        }

        public async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            var result = new List<TModel>();
            var container = await GetContainerAsync();
            BlobContinuationToken continuationToken = null;
            do
            {
                var blobs = await container.ListBlobsSegmentedAsync(continuationToken);
                var blocks = blobs.Results.OfType<CloudBlockBlob>();
                foreach (var block in blocks)
                {
                    var obj = await ReadOneBlockAsync(block);
                    result.Add(obj);
                }
            } while (continuationToken != null);
            return result;
        }

        public async Task<IEnumerable<TModel>> ReadAllInDirectoryAsync(string directoryName)
        {
            var result = new List<TModel>();
            var container = await GetContainerAsync();
            BlobContinuationToken continuationToken = null;
            do
            {
                var directory = container.GetDirectoryReference(directoryName);
                //var blobs = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, null, continuationToken, new BlobRequestOptions(), new OperationContext());
                var blobs = await directory.ListBlobsSegmentedAsync(continuationToken);
                var blocks = blobs.Results.OfType<CloudBlockBlob>();
                foreach (var block in blocks)
                {
                    var obj = await ReadOneBlockAsync(block);
                    result.Add(obj);
                }
            } while (continuationToken != null);
            return result;
        }

        protected static async Task<TModel> ReadOneBlockAsync(CloudBlockBlob block)
        {
            var text = await block.DownloadTextAsync();
            var model = JsonConvert.DeserializeObject<TModel>(text);
            return model;
        }

        protected async Task<CloudBlobContainer> GetContainerAsync()
        {
            var blobClient = CloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(StorageSettings.ContainerName);
            await container.CreateIfNotExistsAsync();
            return container;
        }
    }
}
