using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Blob
{
    public class BlockBlobFileRepository : StorageRepository<IObjectStorageSettings>, IFileRepository
    {
        public BlockBlobFileRepository(IObjectStorageSettings storageSettings)
            : base(storageSettings)
        {
        }

        public CloudBlockBlob Find(string fileName)
        {
            // Create a blob client.
            var blobClient = CloudStorageAccount.CreateCloudBlobClient();

            // Get a reference to a container 
            var container = blobClient.GetContainerReference(StorageSettings.ContainerName);

            // Get a reference to a blob 
            return container.GetBlockBlobReference(fileName);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            // Get a reference to a blob 
            var blockBlob = Find(fileName);

            // Create or overwrite the blob with the contents of a local file
            using (var fileStream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return blockBlob.Uri.AbsoluteUri;
        }

        public async Task<bool> RemoveFileAsync(string fileName)
        {
            // Get a reference to a blob 
            var blockBlob = Find(fileName);

            // Delete the blob if it is existing
            return await blockBlob.DeleteIfExistsAsync();
        }

        public Task<Stream> OpenReadAsync(string fileName)
        {
            return Find(fileName)
                .OpenReadAsync();
        }
    }
}
